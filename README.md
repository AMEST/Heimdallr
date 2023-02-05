![Heimdallr Build](https://github.com/AMEST/Heimdallr/workflows/Heimdallr%20Build/badge.svg?branch=master)
![hub.docker.com](https://img.shields.io/docker/pulls/eluki/heimdallr-password-manager.svg)
![GitHub release (latest by date)](https://img.shields.io/github/v/release/amest/Heimdallr)
![GitHub](https://img.shields.io/github/license/amest/Heimdallr)
# Heimdallr

- [Heimdallr](#heimdallr)
  - [Links](#links)
  - [Description](#description)
  - [Components of the Heimdallr project](#components-of-the-heimdallr-project)
  - [Supported platform](#supported-platform)
  - [How to use](#how-to-use)
    - [Heimdallr.CLI](#heimdallrcli)
    - [Heimdallr.Host (WebApp) Docker](#heimdallrhost-webapp-docker)

## Links
* **[Heimdallr app](https://heimdallr.nb-47.su)**  
* **[Docker image](https://hub.docker.com/r/eluki/heimdallr-password-manager)**
## Description
Heimdall is a stateless password manager / generator.
Tired of constantly remembering your password for a website or application? 
Do you forget to write a new account into the password manager or forget to sync it with another device?
But what if you can get rid of these problems and remember only one master password, but it does not lead to password leakage from other services?

This password manager solves these problems! You only need to remember one master password, and you can get passwords for other sites based on it.

## Components of the Heimdallr project
1. Heimdallr.CLI - Console application that can be run under windows, linux and get the service password right in the console

[![Heimdallr CLI](https://i.postimg.cc/6Q24QSRV/image.png)](https://postimg.cc/1gS3Cvv4)

2. Heimdallr.Host - Web application with which you can get a password for the service you need right in the browser, without installing any software

[![Heimdallr WebApp](https://i.postimg.cc/zvmC5NcR/2020-12-08-08-55-36-localhost-bfe2d743ed69.png)](https://postimg.cc/XG8GcR3V)

## Supported platform
* Command Line
   * [Windows x86](https://github.com/AMEST/Heimdallr/releases/latest/download/Heimdallr.Cli-win-x86.zip)
   * [Linux x64](https://github.com/AMEST/Heimdallr/releases/latest/download/Heimdallr.Cli-linux-x64.zip)
   * [OS X x64](https://github.com/AMEST/Heimdallr/releases/latest/download/Heimdallr.Cli-osx-x64.zip)
* WebApp
   * Hosting (any OS where works dotnet core 3.1 or Docker)
   * User Interface and API (any browser, curl, http client)

## How to use

### Heimdallr.CLI
**Download build binary:**
* [Windows x86](https://github.com/AMEST/Heimdallr/releases/latest/download/Heimdallr.Cli-win-x86.zip)
* [Linux x64](https://github.com/AMEST/Heimdallr/releases/latest/download/Heimdallr.Cli-linux-x64.zip)
* [OS X x64](https://github.com/AMEST/Heimdallr/releases/latest/download/Heimdallr.Cli-osx-x64.zip)

**CommandLine arguments**:
|      Argument       |                                                     Description                                                      |
| ------------------- | -------------------------------------------------------------------------------------------------------------------- |
| -s, --service       | Required. Service name for which the password is generated                                                           |
| -n, --name          | Required. Service account or password ID                                                                             |
| -p, --mpwd          | Master password to generate an idempotent password based on the service name and common name and the dictionary used |
| --hasnumeric        | (Default: true) Numeric dictionary for password generation                                                           |
| --hasletters        | (Default: true) Letter dictionary for password generation                                                            |
| --hasspecialsymbols | (Default: true) Symbolic dictionary for password generation                                                          |
| -l                  | (Default: 16) Length of generated password                                                                           |
| -v                  | (Default: 1) Password version (if you need a new password for the same service and account)                          |
| --help              | Display help screen.                                                                                                 |
| --version           | Display version information.                                                                                         |

**Example (windows cmd):**
```cmd
Heimdallr.Cli.exe -s github -n user -p VerySecretMasterPassword -l 12
```
Output: `ZH2C41OqnF93`

**Example (linux bash):**
```bash
DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 ./Heimdallr.Cli -s github -n user -p VerySecretMasterPassword -l 12
```
Output: `ZH2C41OqnF93`
### Heimdallr.Host (WebApp) Docker

**Docker Compose:**

```yml
version: '3.8'

services:
  host:
    image: eluki/heimdallr-password-manager
    ports:
     - target: 80
       published: 30003
       protocol: tcp
       mode: host
    deploy:
      replicas: 1
    logging:
      driver: "json-file"
      options:
        max-size: "3m"
        max-file: "3"
```

Run stack:
```docker
docker stack deploy -c deploy.yml heimdallr
```

**Docker Run:**
```docker
docker run -d \
           -p 30003:80 \
           --restart always \
           --name heimdallr \
           eluki/heimdallr-password-manager
```
