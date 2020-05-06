## :warning: Development Warning

This project targets the development of the API client for the [DNSimple API v2](https://developer.dnsimple.com/v2/).

This version is currently under development, therefore the methods and the implementation should he considered a work-in-progress. Changes in the method naming, method signatures, public or internal APIs may happen at any time.

The code is tested with an automated test suite connected to a continuous integration tool, therefore you should not expect :bomb: bugs to be merged into master. Regardless, use this library at your own risk. :boom:


# DNSimple C# Client

A C# client for the [DNSimple API v2](https://developer.dnsimple.com/v2/).

## Installation

Where `<version>` denotes the version of the client you want to install.

### Package Manager

```shell
PM> Install-Package DNSimple -Version <version>
```

### .NET CLI

```shell
> dotnet add package DNSimple --version <version>
```

### .NET CLI

```xml
<PackageReference Include="DNSimple" Version="0.8.3-alpha" />
```

## Usage

This library is a C# client you can use to interact with the [DNSimple API v2](https://developer.dnsimple.com/v2/). Here are some examples.

```c#
using dnsimple;

var client = new Client();
var credentials = new OAuth2Credentials("...");
client.AddCredentials(credentials);

# Fetch your details
var response = client.Identity.Whoami();   // execute the call
var account = response.Data.Account;       // extract the relevant data from the response or
client.Identity.Whoami().Data.Account;     // execute the call and get the data in one line

# You can also fetch it from the whoami response
# as long as you authenticate with an Account access token
var whoami = client.Identity.Whoami();
var accountId = whoami.Account.Id;
```

# List your domains

```c#
using dnsimple;

var client = new Client();
client.AddCredentials(new OAuth2Credentials("..."));

var accountId = client.Identity.Whoami().Data.Account.Id;
var domains = client.Domains.ListDomains(accountId).Data.Domains;

```

# Create a domain

```c#
using dnsimple;

var client = new Client();
client.AddCredentials(new OAuth2Credentials("..."));

var accountId = client.Identity.Whoami().Data.Account.Id;
var domain = client.Domains.CreateDomain(accountId, "example.com").Data;
```

# Get a domain

```c#
using dnsimple;

var client = new Client();
client.AddCredentials(new OAuth2Credentials("..."));

var accountId = client.Identity.Whoami().Data.Account.Id;
var domainId = client.Domains.ListDomains(accountId).Data.First().Id;
var domain = client.Domains.GetDomain(accountId, domainId).Data;
```

For the full library documentation visit: **COMING SOON**

## Sandbox Environment

We highly recommend testing against our [sandbox environment](https://developer.dnsimple.com/sandbox/) before using our production environment. 
This will allow you to avoid real purchases, live charges on your credit card, and reduce the chance of your running up against rate limits.

The client supports both the production and sandbox environment. 
To switch to sandbox pass the sandbox API host using the `ChangeBaseUrlTo(...)` method when you construct the client:

```c#
var client = new Client();
client.ChangeBaseUrlTo("https://api.sandbox.dnsimple.com");

var credentials = new OAuth2Credentials("...");
client.AddCredentials(credentials);
```

You will need to ensure that you are using an access token created in the sandbox environment. Production tokens will *not* work in the sandbox environment.

## License

Copyright (c) 2010-2020 DNSimple Corporation. This is Free Software distributed under the MIT license.
