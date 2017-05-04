# IPScan

IPScan is a multi-threaded .NET library written in C# that was created to generate IP addresses using specified masks and ranges.

IPScan allows you to provide IP masks (e.g 192.168.X.X) and will then generate all IP addresses that fall within that range:

* 192.168.0.0 - 192.168.255.255

It has a built-in event handler that allows you to use the IP addresses as they are generated.

## Getting Started

Simply clone the git repository to your computer and build the solution.

### Prerequisites

IPScan requires version 4.5 of the .NET framework which can be downloaded from:

```
https://www.microsoft.com/en-gb/download/details.aspx?id=30653
```

### Installing

Once you have built the solution you will need the reference IPScan.dll in any projects you wish to use the library.

## Usage

IPScan has been created to be as user friendly as possible. You can choose to specify custom masks or use the built-in classes to target the public/private IP address ranges.

### Generic IP Mask Example

The below example will start a scan using an IP mask of 192.X.X.X. This will generate all IP addresses between the following range:

* 192.0.0.0 - 192.255.255.255 

```cs
private void DefaultScan()
{
    var scan = new DefaultScan();
    scan.IPAddressGenerated += IPGenerated;
    
    scan.Scan("192.X.X.X");
}

private void IPGenerated(object sender, ScanArgs e)
{
    Console.WriteLine(e.IPAddress);
}
```

### Public IP Range Example

The below example scans the entire public IP range, this can be useful for automated tools such as uptime calculators.

```cs
private void PublicScan()
{
    var scan = new PublicScan(IPGenerated);
    scan.Scan();
}

private void IPGenerated(object sender, ScanArgs e)
{
    Console.WriteLine(e.IPAddress);
}
```

### IP Mask Blacklist Example

The below example will scan the below range but will exclude any addresses that fall within the blacklisted masks (192.168.X.X)

* 192.168.0.0 - 192.168.255.255

```cs
private void BlacklistScan()
{
    var blacklist = new List<string>()
    {
        "192.168.0.X"
    };

    var scan = new BlacklistScan(blacklist);
    scan.IPAddressGenerated += IPGenerated;

    scan.Scan("192.168.X.X");
}

private void IPGenerated(object sender, ScanArgs e)
{
    Console.WriteLine(e.IPAddress);
}
```

For more examples please see the IPScan.Examples project.

## Contributing

See the [CONTRIBUTING.md](CONTRIBUTING.md) file for details.

## Authors

* **Matthew Croston** - *Original Developer* - [mc-soft](https://github.com/mc-soft) - [http://m-croston.co.uk/](http://www.m-croston.co.uk/)

## License

This project is licensed under the GNUv3 License - see the [LICENSE.md](LICENSE.md) file for details
