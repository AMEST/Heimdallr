![GitHub release (latest by date)](https://img.shields.io/github/v/release/amest/Heimdallr)
![GitHub](https://img.shields.io/github/license/amest/Heimdallr)
# Heimdallr
## Description
Heimdall is a stateless password manager / generator.
Tired of constantly remembering your password for a website or application? 
Do you forget to write a new account into the password manager or forget to sync it with another device?
But what if you can get rid of these problems and remember only one master password, but it does not lead to password leakage from other services?

This password manager solves these problems! You only need to remember one master password, and you can get passwords for other sites based on it.

## Components of the Heimdallr project
1. Heimdallr.CLI - Console application that can be run under windows, linux and get the service password right in the console
1. Heimdallr.Host - Web application (API and SPA (soon)) with which you can get a password for the service you need right in the browser, without installing any software

## Supported platform
* Command Line
   * Windows
   * Linux
* WebApp
   * Hosting (any OS where works dotnet core 3.1 or Docker)
   * User Interface and API (any browser, curl, http client)

## How to use

### Heimdallr.CLI

**CommandLine arguments**:
|Argument|Description|
|--------|-----------|
|-s, --service|Required. Service name for which the password is generated|
|-n, --name|Required. Service account or password ID|
|-p, --mpwd|Required. Master password to generate an idempotent password based on the service name and common name and the dictionary used|
|--hasnumeric|(Default: true) Numeric dictionary for password generation|
|--hasletters|(Default: true) Letter dictionary for password generation|
|--hasspecialsymbols|(Default: true) Symbolic dictionary for password generation|
|-l|(Default: 16) Length of generated password|
|-v|(Default: 1) Password version (if you need a new password for the same service and account)|
|--help|Display this help screen.|
|--version|Display version information.|

**Example generate password (windows cmd):**
```cmd
Heimdallr.Cli.exe -s github -n user -p VerySecretMasterPassword -l 12
```

### Heimdallr.Host (WebApp)
*Comming soon*