# Changelog

All notable changes to Steam Connection Blocker will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2024-01-15

### Added
- Initial release of Steam Connection Blocker
- Automatic Steam installation path detection
- Windows Firewall rule creation and management
- One-click Steam connection blocking/unblocking
- Automatic Steam restart when enabling block
- Modern WPF UI with animations
- Turkish language interface
- Status indicators with color coding
- Troubleshooting menu with automated fixes:
  - Steam restart functionality
  - Firewall rule verification
  - Steam path re-detection
- Installation and uninstallation features
- Loading animations for all async operations
- Auto-hiding notifications
- Comprehensive error handling
- Windows 10 and Windows 11 support
- Administrator privilege elevation
- Detailed user instructions in UI

### Technical Features
- Async/await pattern to prevent UI blocking
- WMI/CIM queries for Steam process detection
- PowerShell integration for Firewall management
- Graceful Steam process termination
- Fallback to common Steam installation paths
- Single-file executable support

### Documentation
- Comprehensive README with usage instructions
- FAQ document with common questions and solutions
- CONTRIBUTING guide for developers
- MIT License
- Build and publish batch scripts

### Security
- Requires administrator privileges for firewall operations
- No data collection or internet connectivity (except Steam detection)
- Open source code for transparency
- Uses official Windows Firewall APIs

[1.0.0]: https://github.com/Serdar8118/Steam-Connection-Blocker/releases/tag/v1.0.0
