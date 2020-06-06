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

## Sample Fed.Fred API Calls

<i>Categories</i>

<b><u>GetCategory</u></b><br>
var release = fred.GetCategory(int categoryId);

<b><u>GetCategoryChildren</u></b><br>
IEnumerable\<Category\> GetCategoryChildren(int categoryId);

<b><u>GetCategoryRelated</u></b><br>
IEnumerable\<Category\> GetCategoryRelated(int categoryId);

<b><u>GetCategorySeries</u></b><br>
IEnumerable\<Series\> GetCategorySeries(int seriesId);

<b><u>GetCategoryTags</u></b><br>
IEnumerable\<Tags\> GetCategoryTags(int categoryId);

<b><u>GetCategoryRelatedTags</u></b><br>
IEnumerable\<Tags\> GetCategoryRelatedTags(int categoryId);


<i>Releases</i>
  
<b><u>GetReleases</u></b><br>
IEnumerable\<Release\> releases = fred.GetReleases();<br>
foreach (var release in releases){}

<b><u>GetReleasesDates</u></b><br>
IEnumerable\<ReleaseDate\> GetReleasesDates();

<b><u>GetRelease</u></b><br>
var release = fred.GetRelease(int releaseId);

<b><u>GetReleaseDates</u></b><br>
IEnumerable\<ReleaseDate\> GetReleaseDates(int releaseId);

<b><u>GetReleaseSeries</u></b><br>
IEnumerable\<Series\> GetReleaseSeries(int releaseId);

<b><u>GetReleaseSources</u></b><br>
IEnumerable\<Series\> GetReleaseSources(int releaseId);

<b><u>GetReleaseTags</u></b><br>
IEnumerable\<Tags\> GetReleaseTags(int releaseId);

<b><u>GetReleaseRelatedTags</u></b><br>
IEnumerable\<Tags\> GetReleaseRelatedTags(int releaseId);


<i>Series</i>
  
<b><u>GetSeries</u></b><br>
var release = fred.GetSeries(string seriesId);

<b><u>GetSeriesCategories</u></b><br>
IEnumerable\<Category\> GetSeriesCategories(string seriesId);

<b><u>GetSeriesObservations</u></b><br>
IEnumerable\<Observation\> GetSeriesObservations(string seriesId);

<b><u>GetSeriesRelease</u></b><br>
var series = fred.GetSeriesRelease(string seriesId);

<b><u>GetSeriesSearch</u></b><br>
IEnumerable\<Series\> GetSeriesSearch(string searchText);

<b><u>GetSeriesSearchTags</u></b><br>
IEnumerable\<Tags\> GetSeriesSearchTags(string searchText);

<b><u>GetSeriesSearchRelatedTags</u></b><br>
IEnumerable\<Tags\> GetSeriesSearchRelatedTags(string searchText, string tag_names);

<b><u>GetSeriesTags</u></b><br>
IEnumerable\<Tags\> GetSeriesTags(string seriesId);

<b><u>GetSeriesUpdates</u></b><br>
IEnumerable\<Series\> GetSeriesUpdates();

<b><u>GetVintageDates</u></b><br>
IEnumerable\<VintageDate\> GetVintageDates(string seriesId);


<i>Sources</i>
  
<b><u>GetSources</u></b><br>
IEnumerable\<Source\> GetSources();

<b><u>GetSource</u></b><br>
IEnumerable\<Source\> GetSource(int sourceID);

<b><u>GetSourceReleases</u></b><br>
IEnumerable\<Source\> GetSource(int sourceID);


<i>Tags</i>
  
<b><u>GetTags</u></b><br>
IEnumerable\<Tags\> GetTags();

<b><u>GetRelatedTags</u></b><br>
IEnumerable\<Tags\> GetSource(string tag_Names);

<b><u>GetTagSeries</u></b><br>
IEnumerable\<Series\> GetTagSeries(string tag_Names);
