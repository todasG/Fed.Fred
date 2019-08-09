

Imports System
Imports System.Collections.Generic
Imports System.Text

Module Urls

    Public Const BaseUrl As String = "https://api.stlouisfed.org/fred/"

#Region "Category Section"

    Public Const Category As String = BaseUrl + "category?api_key={0}&category_id={1}&file_type={2}"

    Public Const CategoryRelated As String =
                BaseUrl + "category/related?api_key={0}&category_id={1}&realtime_start={2}&realtime_end={3}&file_type={4}"

    Public Const CategoryChildern As String =
                BaseUrl + "category/children?api_key={0}&category_id={1}&realtime_start={2}&realtime_end={3}&file_type={4}"

    Public Const CategorySeries As String =
                BaseUrl +
                "category/series?api_key={0}&category_id={1}&realtime_start={2}&realtime_end={3}&limit={4}&offset={5}&sort_order={6}&filter_variable={7}&filter_value={8}&tag_names={9}&exclude_tag_names={10}&file_type={11}"

    Public Const CategoryTags As String =
                BaseUrl +
                "category/tags?api_key={0}&category_id={1}&realtime_start={2}&realtime_end={3}&limit={4}&offset={5}&order_by={6}&sort_order={7}&search_text={8}&tag_names={9}&tag_group_id={10}&file_type={11}"

    Public Const CategoryRelatedTags As String =
                BaseUrl +
                "category/related_tags?api_key={0}&category_id={1}&tag_names={2}&realtime_start={3}&realtime_end={4}&limit={5}&offset={6}&order_by={7}&sort_order={8}&search_text={9}&tag_group_id={10}&exclude_tag_names={11}&file_type={12}"


#End Region

#Region "Release Section"

    Public Const Releases As String =
                BaseUrl +
                "releases?api_key={0}&realtime_start={1}&realtime_end={2}&limit={3}&offset={4}&order_by={5}&sort_order={6}&file_type={7}"

    Public Const Release As String =
                BaseUrl +
                "release?api_key={0}&release_id={1}&realtime_start={2}&realtime_end={3}&file_type={4}"

    Public Const ReleaseSeries As String =
                BaseUrl +
                "release/series?api_key={0}&release_id={1}&realtime_start={2}&realtime_end={3}&limit={4}&offset={5}&order_by={6}&sort_order={7}&filter_variable={8}&filter_value={9}&file_type={10}"

    Public Const ReleasesDates As String =
                BaseUrl +
                "releases/dates?api_key={0}&realtime_start={1}&realtime_end={2}&limit={3}&offset={4}&order_by={5}&sort_order={6}&include_release_dates_with_no_data={7}&file_type={8}"

    Public Const ReleaseDates As String =
                BaseUrl +
                "release/dates?api_key={0}&release_id={1}&realtime_start={2}&realtime_end={3}&limit={4}&offset={5}&sort_order={6}&include_release_dates_with_no_data={7}&file_type={8}"

    Public Const ReleaseSources As String =
                BaseUrl +
                "release/sources?api_key={0}&release_id={1}&realtime_start={2}&realtime_end={3}&file_type={4}"

    Public Const ReleaseTags As String =
                BaseUrl +
                "release/tags?api_key={0}&release_id={1}&realtime_start={2}&realtime_end={3}&limit={4}&offset={5}&order_by={6}&sort_order={7}&file_type={8}"

    Public Const ReleaseRelatedTags As String =
                BaseUrl +
                "release/related_tags?api_key={0}&release_id={1}&tag_names={2}&realtime_start={3}&realtime_end={4}&limit={5}&offset={6}&order_by={7}&sort_order={8}&file_type={9}"
#End Region


#Region "Series Section"

    Public Const Series As String =
                BaseUrl + "series?api_key={0}&series_id={1}&realtime_start={2}&realtime_end={3}&file_type={4}"

    Public Const SeriesCategories As String =
                BaseUrl + "series/categories?api_key={0}&series_id={1}&realtime_start={2}&realtime_end={3}&file_type={4}"

    Public Const SeriesRelease As String =
                BaseUrl + "series/release?api_key={0}&series_id={1}&realtime_start={2}&realtime_end={3}&file_type={4}"

    Public Const SeriesSearch As String =
                BaseUrl +
                "series/search?api_key={0}&search_text={1}&search_type={2}&realtime_start={3}&realtime_end={4}&limit={5}&offset={6}&order_by={7}&sort_order={8}&filter_variable={9}&filter_value={10}&tag_names={11}&exclude_tag_names={12}&file_type={13}"

    Public Const SeriesSearchTags As String =
                BaseUrl +
                "series/search/tags?api_key={0}&series_search_text={1}&realtime_start={2}&realtime_end={3}&limit={4}&offset={5}&sort_order={6}&tag_names={7}&tag_group_id={8}&tag_search_text={9}&file_type={10}"

    Public Const SeriesSearchRelatedTags As String =
                BaseUrl +
                "series/search/related_tags?api_key={0}&series_search_text={1}&tag_names={2}&realtime_start={3}&realtime_end={4}&limit={5}&offset={6}&sort_order={7}&tag_group_id={8}&tag_search_text={9}&exclude_tag_names={10}&file_type={11}"

    Public Const SeriesUpdates As String =
                BaseUrl +
                "series/updates?api_key={0}&realtime_start={1}&realtime_end={2}&limit={3}&offset={4}&filter_value={5}&start_time={6}&end_time={7}&file_type={8}"

    Public Const SeriesVintageDates As String =
                BaseUrl +
                "series/vintagedates?api_key={0}&series_id={1}&realtime_start={2}&realtime_end={3}&limit={4}&offset={5}&sort_order={6}&file_type={7}"

    Public Const SeriesObservations As String =
                BaseUrl +
                "series/observations?api_key={0}&series_id={1}&realtime_start={2}&realtime_end={3}&limit={4}&offset={5}&sort_order={6}&observation_start={7}&observation_end={8}&units={9}&frequency={10}&aggregation_method={11}&output_type={12}&vintage_dates={13}&file_type={14}"

    Public Const SeriesTags As String =
                BaseUrl +
                "series/tags?api_key={0}&series_id={1}&realtime_start={2}&realtime_end={3}&order_by={4}&sort_order={5}&file_type={6}"

#End Region


#Region "Sources Section"

    Public Const Sources As String =
                BaseUrl +
                "sources?api_key={0}&realtime_start={1}&realtime_end={2}&limit={3}&offset={4}&order_by={5}&sort_order={6}&file_type={7}"

    Public Const Source As String =
                BaseUrl + "source?api_key={0}&source_id={1}&realtime_start={2}&realtime_end={3}&file_type={4}"

    Public Const SourceReleases As String =
                BaseUrl +
                "source/releases?api_key={0}&source_id={1}&realtime_start={2}&realtime_end={3}&limit={4}&offset={5}&order_by={6}&sort_order={7}&file_type={8}"

#End Region


#Region "Tags"

    Public Const Tags As String =
                BaseUrl +
                "tags?api_key={0}&realtime_start={1}&realtime_end={2}&limit={3}&offset={4}&order_by={5}&sort_order={6}&tag_names={7}&tag_group_id{8}&search_text={9}&file_type={10}"

    Public Const RelatedTags As String =
                BaseUrl +
                "related_tags?api_key={0}&tag_names={1}&realtime_start={2}&realtime_end={3}&limit={4}&offset={5}&order_by={6}&sort_order={7}&tag_group_id{8}&search_text={9}&exclude_tag_names={10}&file_type={11}"

    Public Const TagsSeries As String =
                BaseUrl +
                "tags/series?api_key={0}&tag_names={1}&realtime_start={2}&realtime_end={3}&limit={4}&offset={5}&order_by={6}&sort_order={7}&exclude_tag_names{8}&file_type={9}"

#End Region

End Module
