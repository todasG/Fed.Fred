
# Fed.Fred

A .Net library for interacting with the Federal Reserve Economic Data (FRED) API.

## Donation
If this project help you reduce time to develop, you can give me a cup of coffee. It encourages us to keep making the package better. Thank you.

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=MQ8JUTVXDMMTG&source=url)

## Installation:

Install package via Nuget: https://www.nuget.org/packages/Fed.Fred/<br><br>
PM> Install-Package Fed.Fred<br><br>
FRED API key is required. Obtain key from http://api.stlouisfed.org/api_key.html

## Instructions:

Create a Fed.Fred object.<br><br>
var fred = new Fred("FRED API key");

The library does not cache calls from the FRED database. You can change cache option.<br><br>
var fred = new Fred("api key", RequestCacheLevel.BypassCache); //Default Option

## Sample Calls

<i>Releases</i>
  
<b><u>GetReleases</u></b><br>
List\<Release\> releases = fred.GetReleases();<br>
foreach (var release in releases){}

<b><u>GetReleasesDates</u></b><br>
List\<ReleaseDate\> GetReleasesDates();

<b><u>GetRelease</u></b><br>
var release = fred.GetRelease(int releaseId);

<b><u>GetReleaseDates</u></b><br>
List\<ReleaseDate\> GetReleaseDates(int releaseId);

<b><u>GetReleaseSeries</u></b><br>
IEnumerable\<Series\> GetReleaseSeries(int releaseId);

<b><u>GetReleaseSources</u></b><br>
List\<Series\> GetReleaseSources(int releaseId);

<b><u>GetReleaseTags</u></b><br>
List\<Tags\> GetReleaseTags(int releaseId);

<b><u>GetReleaseRelatedTags</u></b><br>
List\<Tags\> GetReleaseRelatedTags(int releaseId);


<i>Series</i>
  
<b><u>GetSeries</u></b><br>
var release = fred.GetSeries(string seriesId);

<b><u>GetSeriesCategories</u></b><br>
List\<Category\> GetSeriesCategories(string seriesId);

<b><u>GetSeriesRelease</u></b><br>
var series = fred.GetSeriesRelease(string seriesId);


<i>Sources</i>
  
<b><u>GetSources</u></b><br>
List\<Source\> GetSources();

<b><u>GetSource</u></b><br>
List\<Source\> GetSource(int sourceID);

<b><u>GetSourceReleases</u></b><br>
List\<Source\> GetSource(int sourceID);


<i>Tags</i>
  
<b><u>GetTags</u></b><br>
List\<Tags\> GetTags();

<b><u>GetRelatedTags</u></b><br>
List\<Tags\> GetSource(string tag_Names);

<b><u>GetTagSeries</u></b><br>
List\<Series\> GetTagSeries(string tag_Names);

