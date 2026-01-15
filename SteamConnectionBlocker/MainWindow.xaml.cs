using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SteamConnectionBlocker
{
    public partial class MainWindow : Window
    {
        private string? _steamPath;
        private bool _isBlocked = false;
        private bool _isInstalled = false;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Start fade-in animation
            var fadeIn = (Storyboard)Resources["FadeInAnimation"];
            fadeIn.Begin(MainContent);

            await InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            ShowLoading("Başlatılıyor...");

            try
            {
                // Detect Steam path
                _steamPath = await SteamManager.DetectSteamPath();
                
                if (!string.IsNullOrEmpty(_steamPath))
                {
                    SteamPathText.Text = $"Steam konumu: {_steamPath}";
                    
                    // Check if firewall rule is installed
                    _isInstalled = await SteamManager.IsFirewallRuleInstalled();
                    
                    if (_isInstalled)
                    {
                        // Check if rule is enabled
                        _isBlocked = await SteamManager.IsFirewallRuleEnabled();
                        UpdateUI();
                    }
                    else
                    {
                        UpdateStatus("Kurulum yapılmamış", Colors.Orange);
                        ShowNotification("Lütfen önce 'Kurulum Yap' butonuna tıklayın", NotificationType.Info);
                    }
                }
                else
                {
                    SteamPathText.Text = "Steam konumu: Bulunamadı!";
                    UpdateStatus("Steam bulunamadı", Colors.Red);
                    ShowNotification("Steam algılanamadı. Lütfen Steam'in yüklü olduğundan emin olun.", NotificationType.Error);
                }
            }
            catch (Exception ex)
            {
                ShowNotification($"Başlatma hatası: {ex.Message}", NotificationType.Error);
            }
            finally
            {
                HideLoading();
            }
        }

        private void UpdateUI()
        {
            if (_isInstalled)
            {
                InstallButton.IsEnabled = false;
                UninstallButton.IsEnabled = true;
                ToggleBlockButton.IsEnabled = true;

                if (_isBlocked)
                {
                    UpdateStatus("Steam bağlantısı ENGELLENDİ", Colors.Red);
                    ToggleBlockButton.Content = "Bağlantıyı Aç";
                    ToggleBlockButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50"));
                }
                else
                {
                    UpdateStatus("Steam bağlantısı AÇIK", Colors.LimeGreen);
                    ToggleBlockButton.Content = "Steam Bağlantısını Engelle";
                    ToggleBlockButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F44336"));
                }
            }
            else
            {
                InstallButton.IsEnabled = !string.IsNullOrEmpty(_steamPath);
                UninstallButton.IsEnabled = false;
                ToggleBlockButton.IsEnabled = false;
                UpdateStatus("Kurulum yapılmamış", Colors.Orange);
            }
        }

        private void UpdateStatus(string text, Color color)
        {
            StatusText.Text = text;
            StatusIndicator.Fill = new SolidColorBrush(color);
        }

        private async void Install_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_steamPath))
            {
                ShowNotification("Steam konumu bulunamadı!", NotificationType.Error);
                return;
            }

            ShowLoading("Firewall kuralı oluşturuluyor...");

            try
            {
                var result = await SteamManager.InstallFirewallRule(_steamPath);
                
                if (result.success)
                {
                    _isInstalled = true;
                    UpdateUI();
                    ShowNotification(result.message, NotificationType.Success);
                }
                else
                {
                    ShowNotification(result.message, NotificationType.Error);
                }
            }
            catch (Exception ex)
            {
                ShowNotification($"Hata: {ex.Message}", NotificationType.Error);
            }
            finally
            {
                HideLoading();
            }
        }

        private async void Uninstall_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Firewall kuralını kaldırmak istediğinizden emin misiniz?", 
                "Onay", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            ShowLoading("Firewall kuralı kaldırılıyor...");

            try
            {
                var uninstallResult = await SteamManager.UninstallFirewallRule();
                
                if (uninstallResult.success)
                {
                    _isInstalled = false;
                    _isBlocked = false;
                    UpdateUI();
                    ShowNotification(uninstallResult.message, NotificationType.Success);
                }
                else
                {
                    ShowNotification(uninstallResult.message, NotificationType.Error);
                }
            }
            catch (Exception ex)
            {
                ShowNotification($"Hata: {ex.Message}", NotificationType.Error);
            }
            finally
            {
                HideLoading();
            }
        }

        private async void ToggleBlock_Click(object sender, RoutedEventArgs e)
        {
            if (_isBlocked)
            {
                // Unblock
                ShowLoading("Bağlantı açılıyor...");

                try
                {
                    var result = await SteamManager.DisableBlock();
                    
                    if (result.success)
                    {
                        _isBlocked = false;
                        UpdateUI();
                        ShowNotification(result.message, NotificationType.Success);
                    }
                    else
                    {
                        ShowNotification(result.message, NotificationType.Error);
                    }
                }
                catch (Exception ex)
                {
                    ShowNotification($"Hata: {ex.Message}", NotificationType.Error);
                }
                finally
                {
                    HideLoading();
                }
            }
            else
            {
                // Block
                var confirmResult = MessageBox.Show(
                    "Steam bağlantısı engellenecek ve Steam yeniden başlatılacak.\n\nDevam etmek istiyor musunuz?",
                    "Onay",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (confirmResult != MessageBoxResult.Yes)
                    return;

                ShowLoading("Bağlantı engelleniyor ve Steam yeniden başlatılıyor...");

                try
                {
                    // First enable the block
                    var blockResult = await SteamManager.EnableBlock();
                    
                    if (blockResult.success)
                    {
                        _isBlocked = true;
                        
                        // Then restart Steam
                        if (!string.IsNullOrEmpty(_steamPath))
                        {
                            var restartResult = await SteamManager.RestartSteam(_steamPath);
                            
                            if (restartResult.success)
                            {
                                UpdateUI();
                                ShowNotification("Steam bağlantısı engellendi ve yeniden başlatıldı!", NotificationType.Success);
                            }
                            else
                            {
                                UpdateUI();
                                ShowNotification($"Engelleme başarılı ama Steam yeniden başlatılamadı: {restartResult.message}", NotificationType.Warning);
                            }
                        }
                        else
                        {
                            UpdateUI();
                            ShowNotification("Engelleme başarılı!", NotificationType.Success);
                        }
                    }
                    else
                    {
                        ShowNotification(blockResult.message, NotificationType.Error);
                    }
                }
                catch (Exception ex)
                {
                    ShowNotification($"Hata: {ex.Message}", NotificationType.Error);
                }
                finally
                {
                    HideLoading();
                }
            }
        }

        private async void RestartSteam_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_steamPath))
            {
                ShowNotification("Steam konumu bulunamadı!", NotificationType.Error);
                return;
            }

            ShowLoading("Steam yeniden başlatılıyor...");

            try
            {
                var result = await SteamManager.RestartSteam(_steamPath);
                
                if (result.success)
                {
                    ShowNotification(result.message, NotificationType.Success);
                }
                else
                {
                    ShowNotification(result.message, NotificationType.Error);
                }
            }
            catch (Exception ex)
            {
                ShowNotification($"Hata: {ex.Message}", NotificationType.Error);
            }
            finally
            {
                HideLoading();
            }
        }

        private async void CheckFirewall_Click(object sender, RoutedEventArgs e)
        {
            ShowLoading("Firewall kontrol ediliyor...");

            try
            {
                var isInstalled = await SteamManager.IsFirewallRuleInstalled();
                var isEnabled = isInstalled ? await SteamManager.IsFirewallRuleEnabled() : false;

                string message = isInstalled
                    ? $"Firewall kuralı mevcut ve {(isEnabled ? "AKTİF" : "DEVRE DIŞI")}"
                    : "Firewall kuralı bulunamadı";

                _isInstalled = isInstalled;
                _isBlocked = isEnabled;
                UpdateUI();

                ShowNotification(message, isInstalled ? NotificationType.Success : NotificationType.Warning);
            }
            catch (Exception ex)
            {
                ShowNotification($"Hata: {ex.Message}", NotificationType.Error);
            }
            finally
            {
                HideLoading();
            }
        }

        private async void DetectSteam_Click(object sender, RoutedEventArgs e)
        {
            ShowLoading("Steam algılanıyor...");

            try
            {
                _steamPath = await SteamManager.DetectSteamPath();
                
                if (!string.IsNullOrEmpty(_steamPath))
                {
                    SteamPathText.Text = $"Steam konumu: {_steamPath}";
                    ShowNotification("Steam başarıyla algılandı!", NotificationType.Success);
                    UpdateUI();
                }
                else
                {
                    SteamPathText.Text = "Steam konumu: Bulunamadı!";
                    ShowNotification("Steam algılanamadı!", NotificationType.Error);
                }
            }
            catch (Exception ex)
            {
                ShowNotification($"Hata: {ex.Message}", NotificationType.Error);
            }
            finally
            {
                HideLoading();
            }
        }

        private void ShowLoading(string message)
        {
            LoadingText.Text = message;
            LoadingPanel.Visibility = Visibility.Visible;
            
            var loadingAnimation = (Storyboard)Resources["LoadingAnimation"];
            loadingAnimation.Begin();

            // Disable all buttons during loading
            ToggleBlockButton.IsEnabled = false;
            InstallButton.IsEnabled = false;
            UninstallButton.IsEnabled = false;
        }

        private void HideLoading()
        {
            LoadingPanel.Visibility = Visibility.Collapsed;
            
            var loadingAnimation = (Storyboard)Resources["LoadingAnimation"];
            loadingAnimation.Stop();

            // Re-enable buttons based on state
            UpdateUI();
        }

        private void ShowNotification(string message, NotificationType type)
        {
            NotificationText.Text = message;
            
            switch (type)
            {
                case NotificationType.Success:
                    NotificationText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4CAF50"));
                    break;
                case NotificationType.Error:
                    NotificationText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F44336"));
                    break;
                case NotificationType.Warning:
                    NotificationText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF9800"));
                    break;
                case NotificationType.Info:
                    NotificationText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#66C0F4"));
                    break;
            }

            // Auto-hide after 5 seconds
            Task.Delay(5000).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (NotificationText.Text == message)
                    {
                        NotificationText.Text = "";
                    }
                });
            });
        }

        private enum NotificationType
        {
            Success,
            Error,
            Warning,
            Info
        }
    }
}
