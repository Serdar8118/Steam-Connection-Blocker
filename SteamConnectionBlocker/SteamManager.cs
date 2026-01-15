using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace SteamConnectionBlocker
{
    public class SteamManager
    {
        private const string FirewallRuleName = "Steam Connection Block";

        /// <summary>
        /// Detects the path to Steam.exe by querying running processes
        /// </summary>
        public static async Task<string?> DetectSteamPath()
        {
            return await Task.Run(() =>
            {
                try
                {
                    // Try to find running Steam process first
                    var query = new SelectQuery("SELECT ProcessId, Name, ExecutablePath FROM Win32_Process WHERE Name LIKE '%steam%'");
                    using var searcher = new ManagementObjectSearcher(query);
                    
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        var executablePath = obj["ExecutablePath"]?.ToString();
                        if (!string.IsNullOrEmpty(executablePath) && 
                            executablePath.EndsWith("Steam.exe", StringComparison.OrdinalIgnoreCase))
                        {
                            return executablePath;
                        }
                    }

                    // If Steam is not running, try common installation paths
                    string[] commonPaths = new[]
                    {
                        @"C:\Program Files (x86)\Steam\Steam.exe",
                        @"C:\Program Files\Steam\Steam.exe",
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Steam", "Steam.exe"),
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Steam", "Steam.exe")
                    };

                    foreach (var path in commonPaths)
                    {
                        if (File.Exists(path))
                        {
                            return path;
                        }
                    }

                    return null;
                }
                catch
                {
                    return null;
                }
            });
        }

        /// <summary>
        /// Checks if the firewall rule exists
        /// </summary>
        public static async Task<bool> IsFirewallRuleInstalled()
        {
            return await Task.Run(() =>
            {
                try
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = $"-Command \"Get-NetFirewallRule -DisplayName '{FirewallRuleName}' -ErrorAction SilentlyContinue | Select-Object -First 1\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using var process = Process.Start(startInfo);
                    if (process == null) return false;

                    var output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    return !string.IsNullOrWhiteSpace(output) && output.Contains(FirewallRuleName);
                }
                catch
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// Checks if the firewall rule is enabled
        /// </summary>
        public static async Task<bool> IsFirewallRuleEnabled()
        {
            return await Task.Run(() =>
            {
                try
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = $"-Command \"Get-NetFirewallRule -DisplayName '{FirewallRuleName}' -ErrorAction SilentlyContinue | Select-Object -ExpandProperty Enabled\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using var process = Process.Start(startInfo);
                    if (process == null) return false;

                    var output = process.StandardOutput.ReadToEnd().Trim();
                    process.WaitForExit();

                    return output.Equals("True", StringComparison.OrdinalIgnoreCase);
                }
                catch
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// Creates the firewall rule for Steam
        /// </summary>
        public static async Task<(bool success, string message)> InstallFirewallRule(string steamPath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    if (!File.Exists(steamPath))
                    {
                        return (false, "Steam.exe bulunamadı!");
                    }

                    // Check if rule already exists
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = $"-Command \"New-NetFirewallRule -DisplayName '{FirewallRuleName}' -Direction Outbound -Program '{steamPath}' -Action Block -Enabled False\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        Verb = "runas"
                    };

                    using var process = Process.Start(startInfo);
                    if (process == null)
                    {
                        return (false, "PowerShell başlatılamadı!");
                    }

                    var output = process.StandardOutput.ReadToEnd();
                    var error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        return (true, "Firewall kuralı başarıyla oluşturuldu!");
                    }
                    else
                    {
                        return (false, $"Kural oluşturulamadı: {error}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Hata: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Removes the firewall rule
        /// </summary>
        public static async Task<(bool success, string message)> UninstallFirewallRule()
        {
            return await Task.Run(() =>
            {
                try
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = $"-Command \"Remove-NetFirewallRule -DisplayName '{FirewallRuleName}' -ErrorAction Stop\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using var process = Process.Start(startInfo);
                    if (process == null)
                    {
                        return (false, "PowerShell başlatılamadı!");
                    }

                    var error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        return (true, "Firewall kuralı başarıyla kaldırıldı!");
                    }
                    else
                    {
                        return (false, $"Kural kaldırılamadı: {error}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Hata: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Enables the firewall rule (blocks Steam)
        /// </summary>
        public static async Task<(bool success, string message)> EnableBlock()
        {
            return await Task.Run(() =>
            {
                try
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = $"-Command \"Set-NetFirewallRule -DisplayName '{FirewallRuleName}' -Enabled True\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using var process = Process.Start(startInfo);
                    if (process == null)
                    {
                        return (false, "PowerShell başlatılamadı!");
                    }

                    var error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        return (true, "Steam bağlantısı engellendi!");
                    }
                    else
                    {
                        return (false, $"Engelleme başarısız: {error}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Hata: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Disables the firewall rule (unblocks Steam)
        /// </summary>
        public static async Task<(bool success, string message)> DisableBlock()
        {
            return await Task.Run(() =>
            {
                try
                {
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = $"-Command \"Set-NetFirewallRule -DisplayName '{FirewallRuleName}' -Enabled False\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using var process = Process.Start(startInfo);
                    if (process == null)
                    {
                        return (false, "PowerShell başlatılamadı!");
                    }

                    var error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        return (true, "Steam bağlantısı açıldı!");
                    }
                    else
                    {
                        return (false, $"Açma başarısız: {error}");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Hata: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Restarts Steam to apply the connection block
        /// </summary>
        public static async Task<(bool success, string message)> RestartSteam(string steamPath)
        {
            return await Task.Run(() =>
            {
                try
                {
                    // Close Steam gracefully
                    var steamProcesses = Process.GetProcessesByName("steam");
                    foreach (var process in steamProcesses)
                    {
                        try
                        {
                            process.CloseMainWindow();
                            process.WaitForExit(5000);
                            
                            if (!process.HasExited)
                            {
                                process.Kill();
                            }
                            process.Dispose();
                        }
                        catch
                        {
                            // Continue with other processes
                        }
                    }

                    // Wait a moment for processes to fully close
                    System.Threading.Thread.Sleep(2000);

                    // Start Steam again
                    if (File.Exists(steamPath))
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = steamPath,
                            UseShellExecute = true
                        });

                        return (true, "Steam yeniden başlatıldı!");
                    }
                    else
                    {
                        return (false, "Steam.exe bulunamadı!");
                    }
                }
                catch (Exception ex)
                {
                    return (false, $"Yeniden başlatma başarısız: {ex.Message}");
                }
            });
        }

        /// <summary>
        /// Checks if Steam is running
        /// </summary>
        public static bool IsSteamRunning()
        {
            return Process.GetProcessesByName("steam").Length > 0;
        }
    }
}
