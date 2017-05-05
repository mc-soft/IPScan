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

The below example will scan the below range but will exclude any addresses that fall within the blacklisted masks (192.168.0.X)

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

### Setting ThreadPool values to allow more concurrent threads

IPScan now supports modified ThreadPool values which allows you to increase/decrease the number of active concurrent worker threads.

A word of caution: 

* Setting the minimum number of threads too high can cause the program to terminate.

#### DefaultScan modified thread count

Here we run a default mask scan with the minimum number of concurrent threads set to 50 and the maximum set to 100.

```cs
private void DefaultScanWithModifiedThreadCount()
{
    var scan = new DefaultScan();
    scan.IPAddressGenerated += IPGenerated;
    scan.MinimumThreadCount = 50;
    scan.MaximumThreadCount = 100;

    scan.Scan("192.X.X.X");
}

private void IPGenerated(object sender, ScanArgs e)
{
    Console.WriteLine(e.IPAddress);
}
```

#### PublicScan modified thread count

Here we perform a public mask scan using modified ThreadPool values. The PrivateScan type also takes the same constructor arguments.

```cs
private static void PublicScanWithIncreasedThreads()
{
    // Sets the minimum number of active worker threads to 100.
    var scan = new PublicScan(IPGenerated, 100);
    scan.Scan();

    // Sets the minimum number of active worker threads to 100 and the maximum to 150.
    var scan2 = new PublicScan(IPGenerated, 100, 150);
    scan2.Scan();
}
```

For more examples please see the IPScan.Examples project.

## Contributing

See the [CONTRIBUTING.md](CONTRIBUTING.md) file for details.

## Authors

* **Matthew Croston** - *Original Developer* - [mc-soft](https://github.com/mc-soft) - [http://m-croston.co.uk/](http://www.m-croston.co.uk/)

## License

This project is licensed under the GNUv3 License - see the [LICENSE.md](LICENSE.md) file for details

## Change Log

v1.1.0:

* Added support for modified ThreadPool values.
