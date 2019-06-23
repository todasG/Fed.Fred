# Fed.Fred

A .Net library for interacting with the Federal Reserve Economic Data (FRED) API.

## Donation
If this project help you reduce time to develop, you can give me a cup of coffee. It encourages us to keep making the package better. Thank you.

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=MQ8JUTVXDMMTG&source=url)


## Installation:

Install package via Nuget: https://www.nuget.org/packages/Fed.Fred/

PM> Install-Package Fed.Fred

FRED API key is required. Obtain key from http://api.stlouisfed.org/api_key.html


## Instructions:

Create a Fed.Fred object.

var fred = new Fred("FRED API key");

The library does not cache calls from the FRED database. You can change cache option.

var fred = new Fred("api key", RequestCacheLevel.BypassCache); //Default Option


## Sample Calls

Sample API Calls:

<b>GetReleases</b> >

IEnumerable<Release> releases = fred.GetReleases();

foreach (var release in releases){}

<b>GetRelease</b> >

var release = fred.GetRelease(int);


