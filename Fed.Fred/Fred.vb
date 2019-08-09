


Imports System.Net
Imports System.Xml
Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Imports System.String
Imports System.Net.Cache
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Collections.Generic
Imports System.Linq

Public Class Fred

#Region "Private Parameters"

    Private pFredKey As String
    Private pCacheLevel As RequestCacheLevel
    Const pLimit As Integer = 1000
    Const pOffset As Integer = 0
    Private pTodayDate As DateTime

#End Region


#Region "Fred Object"

    ''' <summary>
    ''' Create New FRED Object
    ''' </summary>
    ''' <param name="FredKey">FRED API Key</param>
    ''' <param name="CacheLevel">Cache Option. Default: BypassCache</param>
    Public Sub New(FredKey As String, Optional CacheLevel As RequestCacheLevel = RequestCacheLevel.BypassCache)
        pFredKey = FredKey.Trim.ToLower
        pCacheLevel = CacheLevel
        pTodayDate = DateTime.Now
    End Sub

    ''' <summary>
    ''' Clear Cache
    ''' </summary>
    Public Sub ClearCache()
        pCacheLevel = RequestCacheLevel.NoCacheNoStore
    End Sub

#End Region


#Region "Category Section"

    ''' <summary>
    ''' fred/category - Get a category.
    ''' </summary>
    ''' <param name="category_id">The id for a category. default: 0 (root category)</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetCategory(Optional category_id As Integer = 0, Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Category)

        Dim sGetCategory As New List(Of Category)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "categories"
        Dim tagName_2 As String = "category"

        Dim rel As Category = Nothing

        Dim url As String = String.Format(Urls.Category, {pFredKey, category_id, filetype})

        Try

            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient()
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url.ToString)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        rel = New Category
                        With rel
                            If item("id") IsNot Nothing Then .Category_Id = Convert.ToInt32(item("id"))
                            If item("name") IsNot Nothing Then .Category_Name = Convert.ToString(item("name"))
                            If item("parent_id") IsNot Nothing Then .Category_Parent_Id = Convert.ToInt32(item("parent_id"))
                        End With
                        sGetCategory.Add(rel)
                        rel = Nothing
                    End If
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        rel = New Category
                        With rel
                            If item.Attributes("id") IsNot Nothing Then .Category_Id = Convert.ToInt32(item.Attributes("id").InnerText)
                            If item.Attributes("name") IsNot Nothing Then .Category_Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("parent_id") IsNot Nothing Then .Category_Parent_Id = Convert.ToInt32(item.Attributes("parent_id").InnerText)
                        End With
                        sGetCategory.Add(rel)
                        rel = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If



        Catch ex As Exception

        End Try


EndM:
        rel = Nothing

        Return sGetCategory.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/category/children - Get the child categories for a specified parent category.
    ''' </summary>
    '''  <param name="category_id">The id for a category. default: 0 (root category)</param>
    ''' <param name="realtimeStart">The start of the real-time period (YYYY-MM-DDD). optional default: Today's date</param>
    ''' <param name="realtimeEnd">The end of the real-time period (YYYY-MM-DD). optional default: Today's date</param>
    ''' <param name="filetype"></param>
    ''' <returns></returns>
    Public Function GetCategoryChildren(Optional category_id As Integer = 0, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Category)

        Dim sGetCategoryChildren As New List(Of Category)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "categories"
        Dim tagName_2 As String = "category"

        Dim rel As Category = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))


        Dim url As String = String.Format(Urls.CategoryChildern, {pFredKey, category_id, realtimeStart, realtimeEnd, filetype})

        Try

            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient()
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url.ToString)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        rel = New Category
                        With rel
                            If item("id") IsNot Nothing Then .Category_Id = Convert.ToInt32(item("id"))
                            If item("name") IsNot Nothing Then .Category_Name = Convert.ToString(item("name"))
                            If item("parent_id") IsNot Nothing Then .Category_Parent_Id = Convert.ToInt32(item("parent_id"))
                        End With
                        sGetCategoryChildren.Add(rel)
                        rel = Nothing
                    End If
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        rel = New Category
                        With rel
                            If item.Attributes("id") IsNot Nothing Then .Category_Id = Convert.ToInt32(item.Attributes("id").InnerText)
                            If item.Attributes("name") IsNot Nothing Then .Category_Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("parent_id") IsNot Nothing Then .Category_Parent_Id = Convert.ToInt32(item.Attributes("parent_id").InnerText)
                        End With
                        sGetCategoryChildren.Add(rel)
                        rel = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        rel = Nothing
        Return sGetCategoryChildren.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/category/related - Get the related categories for a category.
    ''' </summary>
    '''  <param name="category_id">The id for a category.</param>
    ''' <param name="realtimeStart">The start of the real-time period (YYYY-MM-DDD). optional default: Today's date</param>
    ''' <param name="realtimeEnd">The end of the real-time period (YYYY-MM-DD). optional default: Today's date</param>
    ''' <param name="filetype"></param>
    ''' <returns></returns>
    Public Function GetCategoryRelated(category_id As Integer, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Category)

        Dim sGetCategoryRelated As New List(Of Category)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "categories"
        Dim tagName_2 As String = "category"

        Dim rel As Category = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))


        Dim url As String = String.Format(Urls.CategoryRelated, {pFredKey, category_id, realtimeStart, realtimeEnd, filetype})

        Try

            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient()
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url.ToString)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        rel = New Category
                        With rel
                            If item("id") IsNot Nothing Then .Category_Id = Convert.ToInt32(item("id"))
                            If item("name") IsNot Nothing Then .Category_Name = Convert.ToString(item("name"))
                            If item("parent_id") IsNot Nothing Then .Category_Parent_Id = Convert.ToInt32(item("parent_id"))
                        End With
                        sGetCategoryRelated.Add(rel)
                        rel = Nothing
                    End If
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        rel = New Category
                        With rel
                            If item.Attributes("id") IsNot Nothing Then .Category_Id = Convert.ToInt32(item.Attributes("id").InnerText)
                            If item.Attributes("name") IsNot Nothing Then .Category_Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("parent_id") IsNot Nothing Then .Category_Parent_Id = Convert.ToInt32(item.Attributes("parent_id").InnerText)
                        End With
                        sGetCategoryRelated.Add(rel)
                        rel = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        rel = Nothing
        Return sGetCategoryRelated.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/category/series - Get the series in a category.
    ''' </summary>
    '''  <param name="category_id">The id for a category.</param>
    ''' <param name="realtimeStart">The start of the real-time period (YYYY-MM-DDD). optional default: Today's date</param>
    ''' <param name="realtimeEnd">The end of the real-time period (YYYY-MM-DD). optional default: Today's date</param>
    ''' <param name="Limit">The maximum number of results to return. integer between 1 and 1000, optional, default: 1000</param>
    ''' <param name="Offset">non-negative integer, optional, default: 0</param>
    ''' <param name="SortOrder">Sort results is ascending or descending order for attribute values specified by order_by.  'asc', 'desc'. Default: 'asc'</param>
    ''' <param name="filter_variable">The attribute to filter results by. optional, no filter by default</param>
    ''' <param name="filter_value">The value of the filter_variable attribute to filter results by. optional, no filter by default</param>
    ''' <param name="tag_names">A semicolon delimited list of tag names that series match all of. optional, no filtering by tags by default</param>
    ''' <param name="exclude_tag_names">A semicolon delimited list of tag names that series match none of. optional, no filtering by tags by default.</param>
    ''' <param name="filetype"></param>
    ''' <returns></returns>
    Public Function GetCategorySeries(category_id As Integer, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                          Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset,
                                          Optional SortOrder As SortOrder = SortOrder.asc,
                                          Optional filter_variable As Series.Filter_Variable = vbNullString, Optional filter_value As String = vbNullString,
                                          Optional tag_names As String = vbNullString, Optional exclude_tag_names As String = vbNullString,
                                          Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Series)

        Dim sGetCategorySeries As New List(Of Series)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "seriess"
        Dim tagName_2 As String = "series"

        Dim rel As Series = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))


        Dim url As String = String.Format(Urls.CategorySeries, {pFredKey, category_id, realtimeStart, realtimeEnd, Limit, Offset, SortOrder,
                                          filter_variable, filter_value, tag_names, exclude_tag_names, filetype})

        Try

            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient()
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url.ToString)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        rel = New Series
                        With rel
                            If item("id") IsNot Nothing Then .Id = Convert.ToString(item("id"))
                            If item("title") IsNot Nothing Then .Title = Convert.ToString(item("title"))

                            If item("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item("realtime_start"))
                            If item("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item("realtime_end"))

                            If item("observation_start") IsNot Nothing Then .Observation_start = Convert.ToDateTime(item("observation_start"))
                            If item("observation_end") IsNot Nothing Then .Observation_end = Convert.ToDateTime(item("observation_end"))

                            If item("frequency") IsNot Nothing Then .Frequency = Convert.ToString(item("frequency"))
                            If item("frequency_short") IsNot Nothing Then .Frequency_short = Convert.ToString(item("frequency_short"))

                            If item("units") IsNot Nothing Then .units = Convert.ToString(item("units"))
                            If item("units_short") IsNot Nothing Then .units_short = Convert.ToString(item("units_short"))

                            If item("seasonal_adjustment") IsNot Nothing Then .seasonal_adjustment = Convert.ToString(item("seasonal_adjustment"))
                            If item("seasonal_adjustment_short") IsNot Nothing Then .seasonal_adjustment_short = Convert.ToString(item("seasonal_adjustment_short"))

                            If item("last_updated") IsNot Nothing Then .last_updated = Convert.ToString(item("last_updated"))
                            If item("popularity") IsNot Nothing Then .popularity = Convert.ToInt32(item("popularity"))

                            If item("notes") IsNot Nothing Then .notes = Convert.ToString(item("notes"))
                        End With
                        sGetCategorySeries.Add(rel)
                        rel = Nothing
                    End If
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        rel = New Series
                        With rel
                            If item.Attributes("id") IsNot Nothing Then .Id = Convert.ToString(item.Attributes("id").InnerText)
                            If item.Attributes("title") IsNot Nothing Then .Title = Convert.ToString(item.Attributes("title").InnerText)

                            If item.Attributes("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item.Attributes("realtime_start").InnerText)
                            If item.Attributes("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item.Attributes("realtime_end").InnerText)

                            If item.Attributes("observation_start") IsNot Nothing Then .Observation_start = Convert.ToDateTime(item.Attributes("observation_start").InnerText)
                            If item.Attributes("observation_end") IsNot Nothing Then .Observation_end = Convert.ToDateTime(item.Attributes("observation_end").InnerText)

                            If item.Attributes("frequency") IsNot Nothing Then .Frequency = Convert.ToString(item.Attributes("frequency").InnerText)
                            If item.Attributes("frequency_short") IsNot Nothing Then .Frequency_short = Convert.ToString(item.Attributes("frequency_short").InnerText)

                            If item.Attributes("units") IsNot Nothing Then .units = Convert.ToString(item.Attributes("units").InnerText)
                            If item.Attributes("units_short") IsNot Nothing Then .units_short = Convert.ToString(item.Attributes("units_short").InnerText)

                            If item.Attributes("seasonal_adjustment") IsNot Nothing Then .seasonal_adjustment = Convert.ToString(item.Attributes("seasonal_adjustment").InnerText)
                            If item.Attributes("seasonal_adjustment_short") IsNot Nothing Then .seasonal_adjustment_short = Convert.ToString(item.Attributes("seasonal_adjustment_short").InnerText)

                            If item.Attributes("last_updated") IsNot Nothing Then .last_updated = Convert.ToString(item.Attributes("last_updated").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)

                            If item.Attributes("notes") IsNot Nothing Then .notes = Convert.ToString(item.Attributes("notes").InnerText)
                        End With
                        sGetCategorySeries.Add(rel)
                        rel = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        rel = Nothing
        Return sGetCategorySeries.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/category/tags - Get the tags for a category.
    ''' </summary>
    ''' <param name="category_id"></param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="Limit">Maximum number of results to return: integer between 1 and 1000, optional default: 1000</param>
    ''' <param name="Offset">Non-negative integer. default: 0</param>
    ''' <param name="OrderBy">One of the following: 'release_id', 'name', 'press_release', 'realtime_start', 'realtime_end'. default: 'release_id'</param>
    ''' <param name="SortOrder">One of the following: 'asc', 'desc'. default: 'asc'</param>
    ''' <param name="tag_Names">A semicolon delimited list of tag names to only include in the response. optional, no filtering by tag names by default</param>
    ''' <param name="tag_group_id">A tag group id to filter tags by type. no filtering by tag group by default.</param>
    ''' <param name="searchText">The words to find matching tags with. optional, no filtering by search words by default.</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetCategoryTags(category_id As Integer, tag_Names As String, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                            Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset,
                            Optional OrderBy As Tags.OrderBy = Tags.OrderBy.series_count, Optional SortOrder As SortOrder = SortOrder.asc,
                             Optional tag_group_id As Tags.Tag_group_id = vbNullString,
                            Optional searchText As String = vbNullString,
                            Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Tags)

        Dim sGetCategoryTags As New List(Of Tags)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "tags"
        Dim tagName_2 As String = "tag"

        Dim rel As Tags = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))


        Dim url As String = String.Format(Urls.CategoryTags, {pFredKey, category_id, realtimeStart, realtimeEnd, Limit, Offset, OrderBy, SortOrder,
                                          searchText, tag_Names, tag_group_id, filetype})

        Try

            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient()
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url.ToString)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        rel = New Tags
                        With rel
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item("group_id"))
                            If item("notes") IsNot Nothing Then .Notes = Convert.ToString(item("notes"))
                            If item("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item("created"))
                            If item("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item("popularity"))
                            If item("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item("series_count"))
                        End With
                        sGetCategoryTags.Add(rel)
                    End If
                    rel = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        rel = New Tags
                        With rel
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item.Attributes("group_id").InnerText)
                            If item.Attributes("notes") IsNot Nothing Then .Notes = Convert.ToString(item.Attributes("notes").InnerText)
                            If item.Attributes("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item.Attributes("created").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)
                            If item.Attributes("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item.Attributes("series_count").InnerText)
                        End With
                        sGetCategoryTags.Add(rel)
                        rel = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If

        Catch ex As Exception

        End Try


EndM:
        rel = Nothing
        Return sGetCategoryTags.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/category/related_tags - Get the related tags for a category.
    ''' </summary>
    ''' <param name="category_id"></param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="Limit">Maximum number of results to return: integer between 1 and 1000, optional default: 1000</param>
    ''' <param name="Offset">Non-negative integer. default: 0</param>
    ''' <param name="OrderBy">One of the following: 'release_id', 'name', 'press_release', 'realtime_start', 'realtime_end'. default: 'release_id'</param>
    ''' <param name="SortOrder">One of the following: 'asc', 'desc'. default: 'asc'</param>
    ''' <param name="searchText">The words to find matching tags with. optional, no filtering by search words by default.</param>
    ''' <param name="tag_group_id">A tag group id to filter tags by type. no filtering by tag group by default.</param>
    ''' <param name="tag_Names">A semicolon delimited list of tag names to only include in the response. optional, no filtering by tag names by default</param>
    ''' <param name="exclude_tag_names">A semicolon delimited list of tag names that series match none of. optional, no default value.</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetCategoryRelatedTags(category_id As Integer, tag_Names As String, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                            Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset,
                            Optional OrderBy As Tags.OrderBy = Tags.OrderBy.series_count, Optional SortOrder As SortOrder = SortOrder.asc,
                            Optional searchText As String = vbNullString, Optional tag_group_id As Tags.Tag_group_id = vbNullString,
                            Optional exclude_tag_names As String = vbNullString,
                            Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Tags)

        Dim sGetCategoryRelatedTags As New List(Of Tags)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "tags"
        Dim tagName_2 As String = "tag"

        Dim rel As Tags = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))


        Dim url As String = String.Format(Urls.CategoryRelatedTags, {pFredKey, category_id, tag_Names, realtimeStart, realtimeEnd, Limit, Offset, OrderBy, SortOrder,
                                          searchText, tag_group_id, exclude_tag_names, filetype})

        Try

            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient()
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url.ToString)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        rel = New Tags
                        With rel
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item("group_id"))
                            If item("notes") IsNot Nothing Then .Notes = Convert.ToString(item("notes"))
                            If item("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item("created"))
                            If item("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item("popularity"))
                            If item("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item("series_count"))
                        End With
                        sGetCategoryRelatedTags.Add(rel)
                    End If
                    rel = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        rel = New Tags
                        With rel
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item.Attributes("group_id").InnerText)
                            If item.Attributes("notes") IsNot Nothing Then .Notes = Convert.ToString(item.Attributes("notes").InnerText)
                            If item.Attributes("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item.Attributes("created").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)
                            If item.Attributes("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item.Attributes("series_count").InnerText)
                        End With
                        sGetCategoryRelatedTags.Add(rel)
                        rel = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If

        Catch ex As Exception

        End Try


EndM:
        rel = Nothing
        Return sGetCategoryRelatedTags.AsEnumerable

    End Function


#End Region


#Region "Releases Section"


    ''' <summary>
    ''' fred/releases - Get all releases of economic data.
    ''' </summary>
    ''' <param name="realtimeStart">The start of the real-time period (YYYY-MM-DDD). optional default: Today's date</param>
    ''' <param name="realtimeEnd">The end of the real-time period (YYYY-MM-DD). optional default: Today's date</param>
    ''' <param name="Limit">Maximum number of results to return: integer between 1 and 1000, optional default: 1000</param>
    ''' <param name="OrderBy">One of the following: 'release_id', 'name', 'press_release', 'realtime_start', 'realtime_end'. default: 'release_id'</param>
    ''' <param name="SortOrder">One of the following: 'asc', 'desc'. default: 'asc'</param>
    ''' <param name="Offset">Non-negative integer. default: 0</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetReleases(Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                    Optional Limit As Integer = pLimit, Optional OrderBy As Release.OrderBy = Release.OrderBy.release_id,
                                    Optional SortOrder As SortOrder = SortOrder.asc, Optional Offset As Integer = pOffset,
                                    Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Release)

        Dim sGetReleases As New List(Of Release)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "releases"
        Dim tagName_2 As String = "release"

        Dim rel As Release = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.Releases, {pFredKey, realtimeStart, realtimeEnd, Limit, Offset, OrderBy, SortOrder, filetype})

        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient()
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url.ToString)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        rel = New Release
                        With rel
                            If item("id") IsNot Nothing Then .Release_Id = Convert.ToInt32(item("id"))
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item("realtime_start"))
                            If item("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item("realtime_end"))
                        End With
                        sGetReleases.Add(rel)
                    End If
                    rel = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        rel = New Release
                        With rel
                            If item.Attributes("id") IsNot Nothing Then .Release_Id = Convert.ToInt32(item.Attributes("id").InnerText)
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item.Attributes("realtime_start").InnerText)
                            If item.Attributes("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item.Attributes("realtime_end").InnerText)
                        End With
                        sGetReleases.Add(rel)
                        rel = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        rel = Nothing
        Return sGetReleases.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/releases/dates - Get release dates for all releases of economic data.
    ''' </summary>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="Limit">Maximum number of results to return: integer between 1 and 1000, optional default: 1000</param>
    ''' <param name="Offset">Non-negative integer. default: 0</param>
    ''' <param name="OrderBy">One of the following: 'release_id', 'release_name', 'release_date'. default: 'release_date'</param>
    ''' <param name="SortOrder">One of the following: 'asc', 'desc'. default: 'asc'</param>
    ''' <param name="Include_Release_Date_with_No_Data">Determines whether release dates with no data available are returned. Default: False</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetReleasesDates(Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                        Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset,
                                        Optional OrderBy As ReleaseDate.OrderBy = ReleaseDate.OrderBy.release_date, Optional SortOrder As SortOrder = SortOrder.asc,
                                        Optional Include_Release_Date_with_No_Data As Boolean = False, Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of ReleaseDate)

        Dim sGetReleasesDates As New List(Of ReleaseDate)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "release_dates"
        Dim tagName_2 As String = "release_date"

        Dim ser As ReleaseDate = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.ReleasesDates, {pFredKey, realtimeStart, realtimeEnd, Limit, Offset, OrderBy, SortOrder, Include_Release_Date_with_No_Data.ToString.ToLower, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New ReleaseDate
                        With ser
                            If item("release_id") IsNot Nothing Then .ReleaseId = Convert.ToInt32(item("id"))
                            If item("release_name") IsNot Nothing Then .ReleaseName = Convert.ToString(item("release_name"))
                            If item("date") IsNot Nothing Then .ReleaseDate = Convert.ToDateTime(item("date"))
                        End With
                        sGetReleasesDates.Add(ser)
                    End If
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New ReleaseDate
                        With ser
                            If item.Attributes("release_id") IsNot Nothing Then .ReleaseId = Convert.ToInt32(item.Attributes("id").InnerText)
                            If item.Attributes("release_name") IsNot Nothing Then .ReleaseName = Convert.ToString(item.Attributes("release_name").InnerText)
                            If item.Attributes("date") IsNot Nothing Then .ReleaseDate = Convert.ToDateTime(item.Attributes("date").InnerText)
                        End With
                        sGetReleasesDates.Add(ser)
                        ser = Nothing
                    End If
                    Exit For
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetReleasesDates.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/release - Get a release of economic data.
    ''' </summary>
    ''' <param name="ReleaseID">Release ID</param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetRelease(ReleaseID As Integer, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                           Optional filetype As FileType.FileType = FileType.FileType.json) As Release

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "releases"
        Dim tagName_2 As String = "release"

        Dim ser As Release = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.Release, {pFredKey, ReleaseID, realtimeStart, realtimeEnd, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New Release
                        With ser
                            If item("id") IsNot Nothing Then .Release_Id = Convert.ToInt32(item("id"))
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item("realtime_start"))
                            If item("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item("realtime_end"))

                            If item("press_release") IsNot Nothing Then .PressRelease = Convert.ToString(item("press_release"))
                            If item("link") IsNot Nothing Then .Link = Convert.ToString(item("link"))

                        End With
                    End If
                    Exit For
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New Release
                        With ser
                            If item.Attributes("id") IsNot Nothing Then .Release_Id = Convert.ToInt32(item.Attributes("id").InnerText)
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item.Attributes("realtime_start").InnerText)
                            If item.Attributes("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item.Attributes("realtime_end").InnerText)

                            If item.Attributes("press_release") IsNot Nothing Then .PressRelease = Convert.ToString(item.Attributes("press_release").InnerText)
                            If item.Attributes("link") IsNot Nothing Then .Link = Convert.ToString(item.Attributes("link").InnerText)
                        End With
                    End If
                    Exit For
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        Return ser

    End Function


    ''' <summary>
    ''' fred/release/dates - Get release dates for a release of economic data.
    ''' </summary>
    ''' <param name="ReleaseID">Release ID (Required)</param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="Limit">Maximum number of results to return: integer between 1 and 1000, optional default: 1000</param>
    ''' <param name="Offset">Non-negative integer. default: 0</param>
    ''' <param name="SortOrder">One of the following: 'asc', 'desc'. default: 'asc'</param>
    ''' <param name="Include_Release_Date_with_No_Data">Determines whether release dates with no data available are returned. Default: False</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetReleaseDates(ReleaseID As Integer, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                        Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset, Optional SortOrder As SortOrder = SortOrder.asc,
                                        Optional Include_Release_Date_with_No_Data As Boolean = False, Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of ReleaseDate)

        Dim sGetReleaseDates As New List(Of ReleaseDate)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "release_dates"
        Dim tagName_2 As String = "release_date"

        Dim ser As ReleaseDate = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.ReleaseDates, {pFredKey, ReleaseID, realtimeStart, realtimeEnd, Limit, Offset, SortOrder, Include_Release_Date_with_No_Data.ToString.ToLower, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New ReleaseDate
                        With ser
                            If item("release_id") IsNot Nothing Then .ReleaseId = Convert.ToInt32(item("id"))
                            If item("date") IsNot Nothing Then .ReleaseDate = Convert.ToDateTime(item("date"))
                        End With
                        sGetReleaseDates.Add(ser)
                    End If
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New ReleaseDate
                        With ser
                            If item.Attributes("release_id") IsNot Nothing Then .ReleaseId = Convert.ToInt32(item.Attributes("id").InnerText)
                            If item.Attributes("date") IsNot Nothing Then .ReleaseDate = Convert.ToDateTime(item.Attributes("date").InnerText)
                        End With
                        sGetReleaseDates.Add(ser)
                        ser = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetReleaseDates.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/release/series - Get the series on a release of economic data.
    ''' </summary>
    ''' <param name="ReleaseID">Release ID</param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="Limit">Maximum number of results to return: integer between 1 and 1000, optional default: 1000</param>
    ''' <param name="OrderBy">One of the following: 'release_id', 'name', 'press_release', 'realtime_start', 'realtime_end'. default: 'release_id'</param>
    ''' <param name="SortOrder">One of the following: 'asc', 'desc'. default: 'asc'</param>
    ''' <param name="filter_variable">One of the following: 'frequency', 'units', 'seasonal_adjustment'. default: no filter</param>
    ''' <param name="filter_value">Value attribute to filter results by. Default: no filter</param>
    ''' <param name="Offset">Non-negative integer. default: 0</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetReleaseSeries(ReleaseID As Integer, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                          Optional Limit As Integer = pLimit, Optional OrderBy As Series.OrderBy = Series.OrderBy.series_id, Optional SortOrder As SortOrder = SortOrder.asc,
                                          Optional filter_variable As String = vbNullString, Optional filter_value As Series.Filter_Variable = vbNullString,
                                          Optional Offset As Integer = pOffset, Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Series)

        Dim sGetReleaseSeries As New List(Of Series)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "seriess"
        Dim tagName_2 As String = "series"

        Dim ser As Series = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.ReleaseSeries, {pFredKey, ReleaseID, realtimeStart, realtimeEnd, Limit, Offset, OrderBy, SortOrder, filter_variable, filter_value, filetype})

        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New Series
                        With ser
                            If item("id") IsNot Nothing Then .Id = Convert.ToString(item("id"))
                            If item("title") IsNot Nothing Then .Title = Convert.ToString(item("title"))
                            If item("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item("realtime_start"))
                            If item("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item("realtime_end"))

                            If item("observation_start") IsNot Nothing Then .Observation_start = Convert.ToDateTime(item("observation_start"))
                            If item("observation_end") IsNot Nothing Then .Observation_end = Convert.ToDateTime(item("observation_end"))
                            If item("frequency") IsNot Nothing Then .Frequency = Convert.ToString(item("frequency"))
                            If item("frequency_short") IsNot Nothing Then .Frequency_short = Convert.ToString(item("frequency_short"))

                            If item("units") IsNot Nothing Then .units = Convert.ToString(item("units"))
                            If item("units_short") IsNot Nothing Then .units_short = Convert.ToString(item("units_short"))
                            If item("seasonal_adjustment") IsNot Nothing Then .seasonal_adjustment = Convert.ToString(item("seasonal_adjustment"))
                            If item("seasonal_adjustment_short") IsNot Nothing Then .seasonal_adjustment_short = Convert.ToString(item("seasonal_adjustment_short"))

                            If item("last_updated") IsNot Nothing Then .last_updated = Convert.ToString(item("last_updated"))
                            If item("popularity") IsNot Nothing Then .popularity = Convert.ToInt32(item("popularity"))
                            If item("group_popularity") IsNot Nothing Then .group_popularity = Convert.ToInt32(item("group_popularity"))
                            If item("notes") IsNot Nothing Then .notes = Convert.ToString(item("notes"))
                        End With
                        sGetReleaseSeries.Add(ser)
                    End If
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New Series
                        With ser
                            If item.Attributes("id") IsNot Nothing Then .Id = Convert.ToString(item.Attributes("id").InnerText)
                            If item.Attributes("title") IsNot Nothing Then .Title = Convert.ToString(item.Attributes("title").InnerText)

                            If item.Attributes("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item.Attributes("realtime_start").InnerText)
                            If item.Attributes("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item.Attributes("realtime_end").InnerText)

                            If item.Attributes("observation_start") IsNot Nothing Then .Observation_start = Convert.ToDateTime(item.Attributes("observation_start").InnerText)
                            If item.Attributes("observation_end") IsNot Nothing Then .Observation_end = Convert.ToDateTime(item.Attributes("observation_end").InnerText)

                            If item.Attributes("frequency") IsNot Nothing Then .Frequency = Convert.ToString(item.Attributes("frequency").InnerText)
                            If item.Attributes("frequency_short") IsNot Nothing Then .Frequency_short = Convert.ToString(item.Attributes("frequency_short").InnerText)

                            If item.Attributes("units") IsNot Nothing Then .units = Convert.ToString(item.Attributes("units").InnerText)
                            If item.Attributes("units_short") IsNot Nothing Then .units_short = Convert.ToString(item.Attributes("units_short").InnerText)

                            If item.Attributes("seasonal_adjustment") IsNot Nothing Then .seasonal_adjustment = Convert.ToString(item.Attributes("seasonal_adjustment").InnerText)
                            If item.Attributes("seasonal_adjustment_short") IsNot Nothing Then .seasonal_adjustment_short = Convert.ToString(item.Attributes("seasonal_adjustment_short").InnerText)

                            If item.Attributes("last_updated") IsNot Nothing Then .last_updated = Convert.ToString(item.Attributes("last_updated").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)

                            If item.Attributes("notes") IsNot Nothing Then .notes = Convert.ToString(item.Attributes("notes").InnerText)
                        End With
                        sGetReleaseSeries.Add(ser)
                        ser = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetReleaseSeries.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/release/sources - Get the sources for a release of economic data.
    ''' </summary>
    ''' <param name="ReleaseID">Release ID (Required)</param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetReleaseSources(ReleaseID As Integer, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                          Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Source)

        Dim sGetReleaseSources As New List(Of Source)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "sources"
        Dim tagName_2 As String = "source"

        Dim ser As Source = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.ReleaseSources, {pFredKey, ReleaseID, realtimeStart, realtimeEnd, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New Source
                        With ser
                            If item("id") IsNot Nothing Then .Id = Convert.ToInt32(item("id"))
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item("realtime_start"))
                            If item("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item("realtime_end"))
                            If item("link") IsNot Nothing Then .Link = Convert.ToString(item("link"))
                        End With
                        sGetReleaseSources.Add(ser)
                    End If
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New Source
                        With ser
                            If item.Attributes("id") IsNot Nothing Then .Id = Convert.ToInt32(item.Attributes("id").InnerText)
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item.Attributes("realtime_start").InnerText)
                            If item.Attributes("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item.Attributes("realtime_end").InnerText)
                        End With
                        sGetReleaseSources.Add(ser)
                        ser = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetReleaseSources.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/release/tags  - Get the tags for a release.
    ''' </summary>
    ''' <param name="ReleaseID">Release ID</param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="Limit">Maximum number of results to return: integer between 1 and 1000, optional default: 1000</param>
    ''' <param name="OrderBy">One of the following: 'release_id', 'name', 'press_release', 'realtime_start', 'realtime_end'. default: 'release_id'</param>
    ''' <param name="SortOrder">One of the following: 'asc', 'desc'. default: 'asc'</param>
    ''' <param name="Offset">Non-negative integer. default: 0</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetReleaseTags(ReleaseID As Integer, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                          Optional Limit As Integer = pLimit, Optional OrderBy As Tags.OrderBy = Tags.OrderBy.series_count, Optional SortOrder As SortOrder = SortOrder.asc,
                                          Optional Offset As Integer = pOffset, Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Tags)

        Dim sGetReleaseTags As New List(Of Tags)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "tags"
        Dim tagName_2 As String = "tag"
        Dim ser As Tags = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.ReleaseTags, {pFredKey, ReleaseID, realtimeStart, realtimeEnd, Limit, Offset, OrderBy, SortOrder, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New Tags
                        With ser
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item("group_id"))
                            If item("notes") IsNot Nothing Then .Notes = Convert.ToString(item("notes"))
                            If item("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item("created"))
                            If item("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item("popularity"))
                            If item("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item("series_count"))
                        End With
                        sGetReleaseTags.Add(ser)
                    End If
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New Tags
                        With ser
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item.Attributes("group_id").InnerText)
                            If item.Attributes("notes") IsNot Nothing Then .Notes = Convert.ToString(item.Attributes("notes").InnerText)
                            If item.Attributes("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item.Attributes("created").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)
                            If item.Attributes("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item.Attributes("series_count").InnerText)
                        End With
                        sGetReleaseTags.Add(ser)
                        ser = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetReleaseTags.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/release/related_tags - Get the related tags for a release.
    ''' </summary>
    ''' <param name="ReleaseID">Release ID</param>
    ''' <param name="tag_names">A semicolon delimited list of tag names that series match all of.</param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="Limit">Maximum number of results to return: integer between 1 and 1000, optional default: 1000</param>
    ''' <param name="OrderBy">One of the following: 'release_id', 'name', 'press_release', 'realtime_start', 'realtime_end'. default: 'release_id'</param>
    ''' <param name="SortOrder">One of the following: 'asc', 'desc'. default: 'asc'</param>
    ''' <param name="Offset">Non-negative integer. default: 0</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetReleaseRelatedTags(ReleaseID As Integer, tag_names As String, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                          Optional Limit As Integer = pLimit, Optional OrderBy As Tags.OrderBy = Tags.OrderBy.series_count, Optional SortOrder As SortOrder = SortOrder.asc,
                                          Optional Offset As Integer = pOffset, Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Tags)

        Dim sGetReleaseRelatedTags As New List(Of Tags)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "tags"
        Dim tagName_2 As String = "tag"
        Dim ser As Tags = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.ReleaseRelatedTags, {pFredKey, ReleaseID, tag_names, realtimeStart, realtimeEnd, Limit, Offset, OrderBy, SortOrder, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New Tags
                        With ser
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item("group_id"))
                            If item("notes") IsNot Nothing Then .Notes = Convert.ToString(item("notes"))
                            If item("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item("created"))
                            If item("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item("popularity"))
                            If item("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item("series_count"))
                        End With
                        sGetReleaseRelatedTags.Add(ser)
                    End If
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New Tags
                        With ser
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item.Attributes("group_id").InnerText)
                            If item.Attributes("notes") IsNot Nothing Then .Notes = Convert.ToString(item.Attributes("notes").InnerText)
                            If item.Attributes("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item.Attributes("created").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)
                            If item.Attributes("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item.Attributes("series_count").InnerText)
                        End With
                        sGetReleaseRelatedTags.Add(ser)
                        ser = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetReleaseRelatedTags.AsEnumerable

    End Function


#End Region


#Region "Series Section"

    ''' <summary>
    ''' fred/series - Get an economic data series.
    ''' </summary>
    ''' <param name="SeriesID">The id for a series</param>
    ''' <param name="realtimeStart">The start of the real-time period (YYYY-MM-DDD). optional default: Today's date</param>
    ''' <param name="realtimeEnd">The end of the real-time period (YYYY-MM-DD). optional default: Today's date</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetSeries(SeriesID As String, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                    Optional filetype As FileType.FileType = FileType.FileType.json) As Series

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "seriess"
        Dim tagName_2 As String = "series"

        Dim rel As Series = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.Series, {pFredKey, SeriesID, realtimeStart, realtimeEnd, filetype})

        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient()
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url.ToString)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        rel = New Series
                        With rel
                            If item("id") IsNot Nothing Then .Id = Convert.ToString(item("id"))
                            If item("title") IsNot Nothing Then .Title = Convert.ToString(item("title"))

                            If item("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item("realtime_start"))
                            If item("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item("realtime_end"))

                            If item("observation_start") IsNot Nothing Then .Observation_start = Convert.ToDateTime(item("observation_start"))
                            If item("observation_end") IsNot Nothing Then .Observation_end = Convert.ToDateTime(item("observation_end"))

                            If item("frequency") IsNot Nothing Then .Frequency = Convert.ToString(item("frequency"))
                            If item("frequency_short") IsNot Nothing Then .Frequency_short = Convert.ToString(item("frequency_short"))

                            If item("units") IsNot Nothing Then .units = Convert.ToString(item("units"))
                            If item("units_short") IsNot Nothing Then .units_short = Convert.ToString(item("units_short"))

                            If item("seasonal_adjustment") IsNot Nothing Then .seasonal_adjustment = Convert.ToString(item("seasonal_adjustment"))
                            If item("seasonal_adjustment_short") IsNot Nothing Then .seasonal_adjustment_short = Convert.ToString(item("seasonal_adjustment_short"))

                            If item("last_updated") IsNot Nothing Then .last_updated = Convert.ToString(item("last_updated"))
                            If item("popularity") IsNot Nothing Then .popularity = Convert.ToInt32(item("popularity"))

                            If item("notes") IsNot Nothing Then .notes = Convert.ToString(item("notes"))
                        End With
                    End If
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        rel = New Series
                        With rel
                            If item.Attributes("id") IsNot Nothing Then .Id = Convert.ToString(item.Attributes("id").InnerText)
                            If item.Attributes("title") IsNot Nothing Then .Title = Convert.ToString(item.Attributes("title").InnerText)

                            If item.Attributes("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item.Attributes("realtime_start").InnerText)
                            If item.Attributes("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item.Attributes("realtime_end").InnerText)

                            If item.Attributes("observation_start") IsNot Nothing Then .Observation_start = Convert.ToDateTime(item.Attributes("observation_start").InnerText)
                            If item.Attributes("observation_end") IsNot Nothing Then .Observation_end = Convert.ToDateTime(item.Attributes("observation_end").InnerText)

                            If item.Attributes("frequency") IsNot Nothing Then .Frequency = Convert.ToString(item.Attributes("frequency").InnerText)
                            If item.Attributes("frequency_short") IsNot Nothing Then .Frequency_short = Convert.ToString(item.Attributes("frequency_short").InnerText)

                            If item.Attributes("units") IsNot Nothing Then .units = Convert.ToString(item.Attributes("units").InnerText)
                            If item.Attributes("units_short") IsNot Nothing Then .units_short = Convert.ToString(item.Attributes("units_short").InnerText)

                            If item.Attributes("seasonal_adjustment") IsNot Nothing Then .seasonal_adjustment = Convert.ToString(item.Attributes("seasonal_adjustment").InnerText)
                            If item.Attributes("seasonal_adjustment_short") IsNot Nothing Then .seasonal_adjustment_short = Convert.ToString(item.Attributes("seasonal_adjustment_short").InnerText)

                            If item.Attributes("last_updated") IsNot Nothing Then .last_updated = Convert.ToString(item.Attributes("last_updated").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)

                            If item.Attributes("notes") IsNot Nothing Then .notes = Convert.ToString(item.Attributes("notes").InnerText)
                        End With
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        Return rel

    End Function


    ''' <summary>
    ''' fred/series/categories - Get the categories for an economic data series.
    ''' </summary>
    ''' <param name="SeriesID">The id for a series</param>
    ''' <param name="realtimeStart">The start of the real-time period (YYYY-MM-DDD). optional default: Today's date</param>
    ''' <param name="realtimeEnd">The end of the real-time period (YYYY-MM-DD). optional default: Today's date</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetSeriesCategories(SeriesID As String, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                    Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Category)


        Dim sGetSeriesCategories As New List(Of Category)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "categories"
        Dim tagName_2 As String = "category"

        Dim rel As Category = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.SeriesCategories, {pFredKey, SeriesID, realtimeStart, realtimeEnd, filetype})

        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient()
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url.ToString)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        rel = New Category
                        With rel
                            If item("id") IsNot Nothing Then .Category_Id = Convert.ToInt32(item("id"))
                            If item("name") IsNot Nothing Then .Category_Name = Convert.ToString(item("name"))
                            If item("parent_id") IsNot Nothing Then .Category_Parent_Id = Convert.ToInt32(item("parent_id"))
                        End With
                        sGetSeriesCategories.Add(rel)
                    End If
                    rel = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        rel = New Category
                        With rel
                            If item.Attributes("id") IsNot Nothing Then .Category_Id = Convert.ToInt32(item.Attributes("id").InnerText)
                            If item.Attributes("name") IsNot Nothing Then .Category_Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("parent_id") IsNot Nothing Then .Category_Parent_Id = Convert.ToInt32(item.Attributes("parent_id").InnerText)
                        End With
                        sGetSeriesCategories.Add(rel)
                        rel = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        rel = Nothing
        Return sGetSeriesCategories.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/series/observations - Get the observations or data values for an economic data series.
    ''' </summary>
    ''' <param name="SeriesID">The id for a series.</param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted string, optional, default: today's date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted string, optional, default: today's date</param>
    ''' <param name="ObservationStart">The start of the observation period. YYYY-MM-DD formatted string, optional, default: 1776-07-04 (earliest available)</param>
    ''' <param name="ObservationEnd">The end of the observation period. YYYY-MM-DD formatted string, optional, default: 9999-12-31 (latest available)</param>
    ''' <param name="units">A key that indicates a data value transformation. optional, default: lin (No transformation)</param>
    ''' <param name="Frequency">An optional parameter that indicates a lower frequency to aggregate values to. optional, default: no value for no frequency aggregation</param>
    ''' <param name="aggregation_method"> A key that indicates the aggregation method used for frequency aggregation. This parameter has no affect if the frequency parameter is not set. string, optional, default: avg </param>
    ''' <param name="outputType">An integer that indicates an output type. integer, optional, default: 1</param>
    ''' <param name="vintage_dates">A comma separated string of YYYY-MM-DD formatted dates in history (e.g. 2000-01-01,2005-02-24). optional, no vintage dates are set by default.</param>
    ''' <param name="Limit">The maximum number of results to return. integer between 1 and 100000, optional, default: 100000</param>
    ''' <param name="Offset">non-negative integer, optional, default: 0</param>
    ''' <param name="SortOrder">Sort results is ascending or descending observation_date order. 'asc' Or 'desc'. optional, default: asc</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetSeriesObservations(SeriesID As String, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                          Optional ObservationStart As String = vbNullString, Optional ObservationEnd As String = vbNullString,
                                          Optional units As Observation.Units = Observation.Units.lin, Optional Frequency As String = vbNullString,
                                          Optional aggregation_method As Observation.Aggregation_Method = Observation.Aggregation_Method.avg,
                                          Optional outputType As Observation.Output_Type = Observation.Output_Type.One, Optional vintage_dates As String() = Nothing,
                                          Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset, Optional SortOrder As SortOrder = SortOrder.asc,
                                          Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Observation)


        Dim sGetSeriesObservations As New List(Of Observation)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""
        Dim tagName As String = "observations"
        Dim tagName_2 As String = "observation"

        Dim JoutputType As Integer = 1
        'Dim sFrequency As String = ""

        Dim rel As Observation = Nothing

        Dim str As String = ""

        Dim JVintage_dates As String = vbNullString

        If vintage_dates IsNot Nothing Then

            Dim rSplit As String() = vintage_dates.ToString.Split(",")
            Dim mList As New List(Of String)

            For i As Integer = 0 To rSplit.Length - 1
                sr = rSplit(i)
                mList.Add(str.Trim.ToFredDateTime)
            Next

            JVintage_dates = mList.ToString
            mList = Nothing
        End If

        sr = ""

        If outputType = Observation.Output_Type.One Then
            JoutputType = 1
        ElseIf outputType = Observation.Output_Type.Two Then
            JoutputType = 2
        ElseIf outputType = Observation.Output_Type.Three Then
            JoutputType = 3
        ElseIf outputType = Observation.Output_Type.Four Then
            JoutputType = 4
        End If

        'If Frequency = vbNullString Then
        '    sFrequency = vbNullString
        'Else
        '    sFrequency = Frequency
        'End If

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        If ObservationStart = vbNullString Then
            ObservationStart = "1776-07-04"
        Else
            ObservationStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If ObservationEnd = vbNullString Then
            ObservationEnd = "9999-12-31"
        Else
            ObservationEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If


        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))
        ObservationStart = ToFredDateString(CDate(ObservationStart))
        ObservationEnd = ToFredDateString(CDate(ObservationEnd))

        Dim url As String = String.Format(Urls.SeriesObservations, {pFredKey, SeriesID, realtimeStart, realtimeEnd, Limit, Offset, SortOrder,
                                          ObservationStart, ObservationEnd, units, Frequency, aggregation_method, JoutputType, JVintage_dates, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient()
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url.ToString)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        rel = New Observation
                        With rel
                            If item("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item("realtime_start"))
                            If item("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item("realtime_end"))
                            If item("date") IsNot Nothing Then .Observation_Date = Convert.ToDateTime(item("date"))
                            If item("value") IsNot Nothing Then .Observation_Value = Convert.ToString(item("value"))
                        End With
                        sGetSeriesObservations.Add(rel)
                    End If
                    rel = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        rel = New Observation
                        With rel
                            If item.Attributes("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item.Attributes("realtime_start").InnerText)
                            If item.Attributes("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item.Attributes("realtime_end").InnerText)
                            If item.Attributes("date") IsNot Nothing Then .Observation_Date = Convert.ToDateTime(item.Attributes("date").InnerText)
                            If item.Attributes("value") IsNot Nothing Then .Observation_Value = Convert.ToString(item.Attributes("value").InnerText)
                        End With
                        sGetSeriesObservations.Add(rel)
                        rel = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:


        rel = Nothing
        Return sGetSeriesObservations.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/series/release - Get the release for an economic data series.
    ''' </summary>
    ''' <param name="SeriesID">The id for a series</param>
    ''' <param name="realtimeStart">The start of the real-time period (YYYY-MM-DDD). optional default: Today's date</param>
    ''' <param name="realtimeEnd">The end of the real-time period (YYYY-MM-DD). optional default: Today's date</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetSeriesRelease(SeriesID As String, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                    Optional filetype As FileType.FileType = FileType.FileType.json) As Release

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "releases"
        Dim tagName_2 As String = "release"

        Dim rel As Release = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.SeriesRelease, {pFredKey, SeriesID, realtimeStart, realtimeEnd, filetype})

        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient()
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url.ToString)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        rel = New Release
                        With rel
                            If item("id") IsNot Nothing Then .Release_Id = Convert.ToInt32(item("id"))
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))

                            If item("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item("realtime_start"))
                            If item("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item("realtime_end"))

                            If item("press_release") IsNot Nothing Then .PressRelease = Convert.ToString(item("press_release"))
                            If item("link") IsNot Nothing Then .Link = Convert.ToString(item("link"))

                        End With
                    End If
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        rel = New Release
                        With rel
                            If item.Attributes("id") IsNot Nothing Then .Release_Id = Convert.ToInt32(item.Attributes("id").InnerText)
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)

                            If item.Attributes("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item.Attributes("realtime_start").InnerText)
                            If item.Attributes("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item.Attributes("realtime_end").InnerText)

                            If item.Attributes("press_release") IsNot Nothing Then .PressRelease = Convert.ToString(item.Attributes("press_release").InnerText)
                            If item.Attributes("link") IsNot Nothing Then .Link = Convert.ToString(item.Attributes("link").InnerText)
                        End With
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        Return rel

    End Function


    ''' <summary>
    ''' fred/series/search - Get economic data series that match keywords.
    ''' </summary>
    ''' <param name="search_text">The words to match against economic data series.</param>
    ''' <param name="search_type">Determines the type of search to perform. 
    ''' 'full_text' searches series attributes title, units, frequency, and tags by parsing words into stems. This makes it possible for searches like 'Industry' to match series containing related words such as 'Industries'. Encode spaces with %20
    ''' Encode spaces with %20
    ''' 'series_id' performs a substring search on series IDs. Searching for 'ex' will find series containing 'ex' anywhere in a series ID. '*' can be used to anchor searches and match 0 or more of any character.
    ''' </param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted string, optional, default: today's date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted string, optional, default: today's date</param>
    ''' <param name="Limit">The maximum number of results to return. integer between 1 and 1000, optional, default: 1000</param>
    ''' <param name="Offset">non-negative integer, optional, default: 0</param>
    ''' <param name="filter_variable">The attribute to filter results by. optional, no filter by default</param>
    ''' <param name="filter_value">The value of the filter_variable attribute to filter results by. optional, no filter by default</param>
    ''' <param name="OrderBy">Order results by values of the specified attribute.</param>
    ''' <param name="SortOrder">Sort results is ascending or descending order for attribute values specified by order_by.  'asc', 'desc'. Default: 'asc'</param>
    ''' <param name="tag_names">A semicolon delimited list of tag names that series match all of. optional, no filtering by tags by default</param>
    ''' <param name="exclude_tag_names">A semicolon delimited list of tag names that series match none of. optional, no filtering by tags by default.
    ''' Parameter exclude_tag_names requires that parameter tag_names also be set to limit the number of matching series.
    ''' </param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetSeriesSearch(search_text As String, search_type As Series.Search_Type, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                          Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset,
                                          Optional filter_variable As Series.Filter_Variable = vbNullString, Optional filter_value As String = vbNullString,
                                          Optional OrderBy As Series.Search_OrderBy = Series.Search_OrderBy.search_rank, Optional SortOrder As SortOrder = SortOrder.asc,
                                          Optional tag_names As String = vbNullString, Optional exclude_tag_names As String = vbNullString,
                                          Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Series)


        Dim sGetSeriesSearch As New List(Of Series)

        If search_type = Series.Search_Type.full_text Then
            OrderBy = Series.Search_OrderBy.search_rank
        ElseIf search_type = Series.Search_Type.series_id Then
            OrderBy = Series.Search_OrderBy.series_id
        Else
            Return Nothing
        End If

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim rel As Series = Nothing
        Dim tagName As String = "seriess"
        Dim tagName_2 As String = "series"
        Dim str As String = ""

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))


        Dim url As String = String.Format(Urls.SeriesSearch, {pFredKey, search_text, search_type, realtimeStart, realtimeEnd, Limit, Offset, OrderBy, SortOrder,
                                          filter_variable, filter_value, tag_names, exclude_tag_names, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient()
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url.ToString)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        rel = New Series
                        With rel
                            If item("id") IsNot Nothing Then .Id = Convert.ToString(item("id"))
                            If item("title") IsNot Nothing Then .Title = Convert.ToString(item("title"))

                            If item("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item("realtime_start"))
                            If item("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item("realtime_end"))

                            If item("observation_start") IsNot Nothing Then .Observation_start = Convert.ToDateTime(item("observation_start"))
                            If item("observation_end") IsNot Nothing Then .Observation_end = Convert.ToDateTime(item("observation_end"))

                            If item("frequency") IsNot Nothing Then .Frequency = Convert.ToString(item("frequency"))
                            If item("frequency_short") IsNot Nothing Then .Frequency_short = Convert.ToString(item("frequency_short"))

                            If item("units") IsNot Nothing Then .units = Convert.ToString(item("units"))
                            If item("units_short") IsNot Nothing Then .units_short = Convert.ToString(item("units_short"))

                            If item("seasonal_adjustment") IsNot Nothing Then .seasonal_adjustment = Convert.ToString(item("seasonal_adjustment"))
                            If item("seasonal_adjustment_short") IsNot Nothing Then .seasonal_adjustment_short = Convert.ToString(item("seasonal_adjustment_short"))

                            If item("last_updated") IsNot Nothing Then .last_updated = Convert.ToString(item("last_updated"))
                            If item("popularity") IsNot Nothing Then .popularity = Convert.ToInt32(item("popularity"))

                            If item("notes") IsNot Nothing Then .notes = Convert.ToString(item("notes"))
                        End With
                        sGetSeriesSearch.Add(rel)
                        rel = Nothing
                    End If
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        rel = New Series
                        With rel
                            If item.Attributes("id") IsNot Nothing Then .Id = Convert.ToString(item.Attributes("id").InnerText)
                            If item.Attributes("title") IsNot Nothing Then .Title = Convert.ToString(item.Attributes("title").InnerText)

                            If item.Attributes("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item.Attributes("realtime_start").InnerText)
                            If item.Attributes("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item.Attributes("realtime_end").InnerText)

                            If item.Attributes("observation_start") IsNot Nothing Then .Observation_start = Convert.ToDateTime(item.Attributes("observation_start").InnerText)
                            If item.Attributes("observation_end") IsNot Nothing Then .Observation_end = Convert.ToDateTime(item.Attributes("observation_end").InnerText)

                            If item.Attributes("frequency") IsNot Nothing Then .Frequency = Convert.ToString(item.Attributes("frequency").InnerText)
                            If item.Attributes("frequency_short") IsNot Nothing Then .Frequency_short = Convert.ToString(item.Attributes("frequency_short").InnerText)

                            If item.Attributes("units") IsNot Nothing Then .units = Convert.ToString(item.Attributes("units").InnerText)
                            If item.Attributes("units_short") IsNot Nothing Then .units_short = Convert.ToString(item.Attributes("units_short").InnerText)

                            If item.Attributes("seasonal_adjustment") IsNot Nothing Then .seasonal_adjustment = Convert.ToString(item.Attributes("seasonal_adjustment").InnerText)
                            If item.Attributes("seasonal_adjustment_short") IsNot Nothing Then .seasonal_adjustment_short = Convert.ToString(item.Attributes("seasonal_adjustment_short").InnerText)

                            If item.Attributes("last_updated") IsNot Nothing Then .last_updated = Convert.ToString(item.Attributes("last_updated").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)

                            If item.Attributes("notes") IsNot Nothing Then .notes = Convert.ToString(item.Attributes("notes").InnerText)
                        End With
                        sGetSeriesSearch.Add(rel)
                        rel = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If

        Catch ex As Exception

        End Try


EndM:


        rel = Nothing
        Return sGetSeriesSearch.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/series/search/tags - Get the tags for a series search.
    ''' </summary>
    ''' <param name="series_search_text">The words to match against economic data series.</param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted string, optional, default: today's date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted string, optional, default: today's date</param>
    ''' <param name="Limit">The maximum number of results to return. integer between 1 and 1000, optional, default: 1000</param>
    ''' <param name="Offset">non-negative integer, optional, default: 0</param>
    ''' <param name="SortOrder">Sort results is ascending or descending order for attribute values specified by order_by. 'asc' Or 'desc'. default: asc</param>
    ''' <param name="tag_names">A semicolon delimited list of tag names to only include in the response. no filtering by tag names by default</param>
    ''' <param name="tag_group_id">A tag group id to filter tags by type. no filtering by tag group by default.</param>
    ''' <param name="tag_search_text">The words to find matching tags with. no filtering by search words by default.</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetSeriesSearchTags(series_search_text As String, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                          Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset,
                                          Optional SortOrder As SortOrder = SortOrder.asc,
                                          Optional tag_names As String = vbNullString, Optional tag_group_id As Tags.Tag_group_id = vbNullString,
                                          Optional tag_search_text As String = vbNullString,
                                          Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Tags)

        Dim sGetSeriesSearchTags As New List(Of Tags)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "tags"
        Dim tagName_2 As String = "tags"
        Dim ser As Tags = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.SeriesSearchTags, {pFredKey, series_search_text, realtimeStart, realtimeEnd, Limit, Offset, SortOrder,
                                           tag_names, tag_group_id, tag_search_text, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New Tags
                        With ser
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item("group_id"))
                            If item("notes") IsNot Nothing Then .Notes = Convert.ToString(item("notes"))
                            If item("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item("created"))
                            If item("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item("popularity"))
                            If item("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item("series_count"))
                        End With
                        sGetSeriesSearchTags.Add(ser)
                    End If
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New Tags
                        With ser
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item.Attributes("group_id").InnerText)
                            If item.Attributes("notes") IsNot Nothing Then .Notes = Convert.ToString(item.Attributes("notes").InnerText)
                            If item.Attributes("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item.Attributes("created").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)
                            If item.Attributes("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item.Attributes("series_count").InnerText)
                        End With
                        sGetSeriesSearchTags.Add(ser)
                        ser = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetSeriesSearchTags.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/series/search/related_tags - Get the related tags for a series search.
    ''' </summary>
    ''' <param name="series_search_text">The words to match against economic data series.</param>
    ''' <param name="tag_names">A semicolon delimited list of tag names to only include in the response. required, no default value.</param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted string, optional, default: today's date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted string, optional, default: today's date</param>
    ''' <param name="Limit">The maximum number of results to return. integer between 1 and 1000, optional, default: 1000</param>
    ''' <param name="Offset">non-negative integer, optional, default: 0</param>
    ''' <param name="SortOrder">Sort results is ascending or descending order for attribute values specified by order_by. 'asc' Or 'desc'. default: asc</param>
    ''' <param name="tag_group_id">A tag group id to filter tags by type. no filtering by tag group by default.</param>
    ''' <param name="tag_search_text">The words to find matching tags with. no filtering by search words by default.</param>
    ''' <param name="exclude_tag_names">A semicolon delimited list of tag names that series match none of. optional, no default value.</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetSeriesSearchRelatedTags(series_search_text As String, tag_names As String, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                          Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset,
                                          Optional SortOrder As SortOrder = SortOrder.asc,
                                          Optional tag_group_id As Tags.Tag_group_id = vbNullString,
                                          Optional tag_search_text As String = vbNullString, Optional exclude_tag_names As String = vbNullString,
                                          Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Tags)

        Dim sGetSeriesSearchRelatedTags As New List(Of Tags)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "tags"
        Dim tagName_2 As String = "tags"

        Dim ser As Tags = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.SeriesSearchRelatedTags, {pFredKey, series_search_text, tag_names, realtimeStart, realtimeEnd, Limit, Offset, SortOrder,
                                            tag_group_id, tag_search_text, exclude_tag_names, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New Tags
                        With ser
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item("group_id"))
                            If item("notes") IsNot Nothing Then .Notes = Convert.ToString(item("notes"))
                            If item("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item("created"))
                            If item("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item("popularity"))
                            If item("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item("series_count"))
                        End With
                        sGetSeriesSearchRelatedTags.Add(ser)
                    End If
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New Tags
                        With ser
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item.Attributes("group_id").InnerText)
                            If item.Attributes("notes") IsNot Nothing Then .Notes = Convert.ToString(item.Attributes("notes").InnerText)
                            If item.Attributes("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item.Attributes("created").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)
                            If item.Attributes("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item.Attributes("series_count").InnerText)
                        End With
                        sGetSeriesSearchRelatedTags.Add(ser)
                        ser = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetSeriesSearchRelatedTags.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/series/tags - Get the tags for an economic data series.
    ''' </summary>
    ''' <param name="SeriesID">Series ID</param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="OrderBy">One of the following: 'release_id', 'name', 'press_release', 'realtime_start', 'realtime_end'. default: 'release_id'</param>
    ''' <param name="SortOrder">One of the following: 'asc', 'desc'. default: 'asc'</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetSeriesTags(SeriesID As String, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                  Optional OrderBy As Tags.OrderBy = Tags.OrderBy.series_count, Optional SortOrder As SortOrder = SortOrder.asc,
                                  Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Tags)

        Dim sGetSeriesTags As New List(Of Tags)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "tags"
        Dim tagName_2 As String = "tags"

        Dim ser As Tags = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.SeriesTags, {pFredKey, SeriesID, realtimeStart, realtimeEnd, OrderBy, SortOrder, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New Tags
                        With ser
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item("group_id"))
                            If item("notes") IsNot Nothing Then .Notes = Convert.ToString(item("notes"))
                            If item("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item("created"))
                            If item("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item("popularity"))
                            If item("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item("series_count"))
                        End With
                        sGetSeriesTags.Add(ser)
                    End If
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New Tags
                        With ser
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item.Attributes("group_id").InnerText)
                            If item.Attributes("notes") IsNot Nothing Then .Notes = Convert.ToString(item.Attributes("notes").InnerText)
                            If item.Attributes("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item.Attributes("created").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)
                            If item.Attributes("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item.Attributes("series_count").InnerText)
                        End With
                        sGetSeriesTags.Add(ser)
                        ser = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetSeriesTags.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/series/updates - Get economic data series sorted by when observations were updated on the FRED® server.
    ''' </summary>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted string, optional, default: today's date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted string, optional, default: today's date</param>
    ''' <param name="Limit">The maximum number of results to return. integer between 1 and 1000, optional, default: 1000</param>
    ''' <param name="Offset">non-negative integer, optional, default: 0</param>
    ''' <param name="filter_value">The value of the filter_variable attribute to filter results by. optional, no filter by default</param>
    ''' <param name="start_time">Start time for limiting results for a time range, can filter down to minutes. YYYYMMDDHhmm formatted string, optional, end_time is required if start_time is set</param>
    ''' <param name="end_time">End time for limiting results for a time range, can filter down to minutes. YYYYMMDDHhmm formatted string, optional, , start_time is required if end_time is set</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetSeriesUpdates(Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                          Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset,
                                          Optional filter_value As String = vbNullString,
                                          Optional start_time As String = vbNullString, Optional end_time As String = vbNullString,
                                          Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Series)


        Dim sGetSeriesUpdates As New List(Of Series)


        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim rel As Series = Nothing
        Dim tagName As String = "seriess"
        Dim tagName_2 As String = "series"
        Dim str As String = ""

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))


        Dim url As String = String.Format(Urls.SeriesUpdates, {pFredKey, realtimeStart, realtimeEnd, Limit, Offset, filter_value, start_time, end_time, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient()
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url.ToString)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        rel = New Series
                        With rel
                            If item("id") IsNot Nothing Then .Id = Convert.ToString(item("id"))
                            If item("title") IsNot Nothing Then .Title = Convert.ToString(item("title"))

                            If item("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item("realtime_start"))
                            If item("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item("realtime_end"))

                            If item("observation_start") IsNot Nothing Then .Observation_start = Convert.ToDateTime(item("observation_start"))
                            If item("observation_end") IsNot Nothing Then .Observation_end = Convert.ToDateTime(item("observation_end"))

                            If item("frequency") IsNot Nothing Then .Frequency = Convert.ToString(item("frequency"))
                            If item("frequency_short") IsNot Nothing Then .Frequency_short = Convert.ToString(item("frequency_short"))

                            If item("units") IsNot Nothing Then .units = Convert.ToString(item("units"))
                            If item("units_short") IsNot Nothing Then .units_short = Convert.ToString(item("units_short"))

                            If item("seasonal_adjustment") IsNot Nothing Then .seasonal_adjustment = Convert.ToString(item("seasonal_adjustment"))
                            If item("seasonal_adjustment_short") IsNot Nothing Then .seasonal_adjustment_short = Convert.ToString(item("seasonal_adjustment_short"))

                            If item("last_updated") IsNot Nothing Then .last_updated = Convert.ToString(item("last_updated"))
                            If item("popularity") IsNot Nothing Then .popularity = Convert.ToInt32(item("popularity"))

                            If item("notes") IsNot Nothing Then .notes = Convert.ToString(item("notes"))
                        End With
                        sGetSeriesUpdates.Add(rel)
                        rel = Nothing
                    End If
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        rel = New Series
                        With rel
                            If item.Attributes("id") IsNot Nothing Then .Id = Convert.ToString(item.Attributes("id").InnerText)
                            If item.Attributes("title") IsNot Nothing Then .Title = Convert.ToString(item.Attributes("title").InnerText)

                            If item.Attributes("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item.Attributes("realtime_start").InnerText)
                            If item.Attributes("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item.Attributes("realtime_end").InnerText)

                            If item.Attributes("observation_start") IsNot Nothing Then .Observation_start = Convert.ToDateTime(item.Attributes("observation_start").InnerText)
                            If item.Attributes("observation_end") IsNot Nothing Then .Observation_end = Convert.ToDateTime(item.Attributes("observation_end").InnerText)

                            If item.Attributes("frequency") IsNot Nothing Then .Frequency = Convert.ToString(item.Attributes("frequency").InnerText)
                            If item.Attributes("frequency_short") IsNot Nothing Then .Frequency_short = Convert.ToString(item.Attributes("frequency_short").InnerText)

                            If item.Attributes("units") IsNot Nothing Then .units = Convert.ToString(item.Attributes("units").InnerText)
                            If item.Attributes("units_short") IsNot Nothing Then .units_short = Convert.ToString(item.Attributes("units_short").InnerText)

                            If item.Attributes("seasonal_adjustment") IsNot Nothing Then .seasonal_adjustment = Convert.ToString(item.Attributes("seasonal_adjustment").InnerText)
                            If item.Attributes("seasonal_adjustment_short") IsNot Nothing Then .seasonal_adjustment_short = Convert.ToString(item.Attributes("seasonal_adjustment_short").InnerText)

                            If item.Attributes("last_updated") IsNot Nothing Then .last_updated = Convert.ToString(item.Attributes("last_updated").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)

                            If item.Attributes("notes") IsNot Nothing Then .notes = Convert.ToString(item.Attributes("notes").InnerText)
                        End With
                        sGetSeriesUpdates.Add(rel)
                        rel = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:


        rel = Nothing
        Return sGetSeriesUpdates.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/series/tags - Get the tags for an economic data series.
    ''' </summary>
    ''' <param name="SeriesID">Seriese ID</param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="Limit">Maximum number of results to return: integer between 1 and 1000, optional default: 1000</param>
    ''' <param name="SortOrder">One of the following: 'asc', 'desc'. default: 'asc'</param>
    ''' <param name="Offset">Non-negative integer. default: 0</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetSeriesVintageDates(SeriesID As String, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                  Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset,
                                  Optional SortOrder As SortOrder = SortOrder.asc,
                                  Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of VintageDate)


        Dim sGetSeriesVintageDates As New List(Of VintageDate)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "vintage_dates"
        Dim tagName_2 As String = "vintage_date"

        Dim ser As VintageDate = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.SeriesVintageDates, {pFredKey, SeriesID, realtimeStart, realtimeEnd, Limit, Offset, SortOrder, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For i As Integer = 0 To arr.Count - 1
                    ser = New VintageDate
                    With ser
                        If arr(i) IsNot Nothing Then .Vintage_Date = Convert.ToDateTime(arr(i))
                    End With
                    sGetSeriesVintageDates.Add(ser)
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For i As Integer = 0 To nodeList.Count - 1
                    ser = New VintageDate
                    With ser
                        If nodeList(i) IsNot Nothing Then .Vintage_Date = Convert.ToDateTime(nodeList(i).InnerText)
                    End With
                    sGetSeriesVintageDates.Add(ser)
                    ser = Nothing
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetSeriesVintageDates.AsEnumerable

    End Function


#End Region


#Region "Sources Section"

    ''' <summary>
    ''' fred/sources - Get all sources of economic data.
    ''' </summary>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="Limit">Maximum number of results to return: integer between 1 and 1000, optional default: 1000</param>
    ''' <param name="Offset">Non-negative integer. default: 0</param>
    ''' <param name="OrderBy">One of the following: 'release_id', 'release_name', 'release_date'. default: 'release_date'</param>
    ''' <param name="SortOrder">One of the following: 'asc', 'desc'. default: 'asc'</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetSources(Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                               Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset, Optional OrderBy As Source.Source_OrderBy = Source.Source_OrderBy.source_id,
                               Optional SortOrder As SortOrder = SortOrder.asc,
                               Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Source)

        Dim sGetSources As New List(Of Source)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "sources"
        Dim tagName_2 As String = "source"

        Dim ser As Source = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.Sources, {pFredKey, realtimeStart, realtimeEnd, Limit, Offset, OrderBy, SortOrder, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New Source
                        With ser
                            If item("id") IsNot Nothing Then .Id = Convert.ToInt32(item("id"))
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item("realtime_start"))
                            If item("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item("realtime_end"))
                            If item("link") IsNot Nothing Then .Link = Convert.ToDateTime(item("Link"))
                        End With
                        sGetSources.Add(ser)
                    End If
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New Source
                        With ser
                            If item.Attributes("id") IsNot Nothing Then .Id = Convert.ToInt32(item.Attributes("id").InnerText)
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item.Attributes("realtime_start").InnerText)
                            If item.Attributes("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item.Attributes("realtime_end").InnerText)
                        End With
                        sGetSources.Add(ser)
                        ser = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetSources.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/source - Get a source of economic data.
    ''' </summary>
    ''' <param name="SourceID">The id for a source.</param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetSource(SourceID As Integer, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                        Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Source)

        Dim sGetSource As New List(Of Source)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "sources"
        Dim tagName_2 As String = "source"
        Dim ser As Source = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.Source, {pFredKey, SourceID, realtimeStart, realtimeEnd, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New Source
                        With ser
                            If item("id") IsNot Nothing Then .Id = Convert.ToInt32(item("id"))
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item("realtime_start"))
                            If item("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item("realtime_end"))
                            If item("link") IsNot Nothing Then .Link = Convert.ToDateTime(item("Link"))
                        End With
                        sGetSource.Add(ser)
                    End If
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New Source
                        With ser
                            If item.Attributes("id") IsNot Nothing Then .Id = Convert.ToInt32(item.Attributes("id").InnerText)
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item.Attributes("realtime_start").InnerText)
                            If item.Attributes("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item.Attributes("realtime_end").InnerText)
                        End With
                        sGetSource.Add(ser)
                        ser = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetSource.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/source/releases - Get the releases for a source.
    ''' </summary>
    ''' <param name="SourceID">The id for a source.</param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="Limit">Maximum number of results to return: integer between 1 and 1000, optional default: 1000</param>
    ''' <param name="Offset">Non-negative integer. default: 0</param>
    ''' <param name="OrderBy">One of the following: 'release_id', 'release_name', 'release_date'. default: 'release_date'</param>
    ''' <param name="SortOrder">One of the following: 'asc', 'desc'. default: 'asc'</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetSourceReleases(SourceID As Integer, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                                        Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset, Optional OrderBy As Source.SourceRelease_OrderBy = Source.SourceRelease_OrderBy.release_id,
                                        Optional SortOrder As SortOrder = SortOrder.asc,
                                        Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Source)

        Dim sGetSourceReleases As New List(Of Source)

        Dim arr As JArray
        Dim obj As JObject

        Dim sr As String = ""

        Dim tagName As String = "releases"
        Dim tagName_2 As String = "release"
        Dim ser As Source = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.SourceReleases, {pFredKey, SourceID, realtimeStart, realtimeEnd, Limit, Offset, OrderBy, SortOrder, filetype})


        Try
            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New Source
                        With ser
                            If item("id") IsNot Nothing Then .Id = Convert.ToInt32(item("id"))
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item("realtime_start"))
                            If item("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item("realtime_end"))
                            If item("press_release") IsNot Nothing Then .PressRelease = Convert.ToString(item("press_release"))
                            If item("link") IsNot Nothing Then .Link = Convert.ToString(item("link"))
                        End With
                        sGetSourceReleases.Add(ser)
                    End If
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New Source
                        With ser
                            If item.Attributes("id") IsNot Nothing Then .Id = Convert.ToInt32(item.Attributes("id").InnerText)
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item.Attributes("realtime_start").InnerText)
                            If item.Attributes("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item.Attributes("realtime_end").InnerText)
                            If item.Attributes("press_release") IsNot Nothing Then .PressRelease = Convert.ToString(item.Attributes("press_release").InnerText)
                            If item.Attributes("link") IsNot Nothing Then .Link = Convert.ToString(item.Attributes("link").InnerText)
                        End With
                        sGetSourceReleases.Add(ser)
                        ser = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetSourceReleases.AsEnumerable

    End Function


#End Region


#Region "Tags Section"

    ''' <summary>
    ''' fred/series/tags - Get the tags for an economic data series.
    ''' </summary>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="Limit">Maximum number of results to return: integer between 1 and 1000, optional default: 1000</param>
    ''' <param name="Offset">Non-negative integer. default: 0</param>
    ''' <param name="OrderBy">One of the following: 'release_id', 'name', 'press_release', 'realtime_start', 'realtime_end'. default: 'release_id'</param>
    ''' <param name="SortOrder">One of the following: 'asc', 'desc'. default: 'asc'</param>
    ''' <param name="tag_Names">A semicolon delimited list of tag names to only include in the response. optional, no filtering by tag names by default</param>
    ''' <param name="tag_group_id">A tag group id to filter tags by type. no filtering by tag group by default.</param>
    ''' <param name="searchText">The words to find matching tags with. optional, no filtering by search words by default.</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetTags(Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                            Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset,
                            Optional OrderBy As Tags.OrderBy = Tags.OrderBy.series_count, Optional SortOrder As SortOrder = SortOrder.asc,
                            Optional tag_Names As String = vbNullString, Optional tag_group_id As Tags.Tag_group_id = vbNullString,
                            Optional searchText As String = vbNullString,
                            Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Tags)

        Dim sGetTags As New List(Of Tags)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "tags"
        Dim tagName_2 As String = "tag"

        Dim ser As Tags = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.Tags, {pFredKey, realtimeStart, realtimeEnd, Limit, Offset, OrderBy, SortOrder,
                                          tag_Names, tag_group_id, searchText, filetype})


        Try

            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New Tags
                        With ser
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item("group_id"))
                            If item("notes") IsNot Nothing Then .Notes = Convert.ToString(item("notes"))
                            If item("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item("created"))
                            If item("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item("popularity"))
                            If item("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item("series_count"))
                        End With
                        sGetTags.Add(ser)
                    End If
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then

                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New Tags
                        With ser
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item.Attributes("group_id").InnerText)
                            If item.Attributes("notes") IsNot Nothing Then .Notes = Convert.ToString(item.Attributes("notes").InnerText)
                            If item.Attributes("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item.Attributes("created").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)
                            If item.Attributes("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item.Attributes("series_count").InnerText)
                        End With
                        sGetTags.Add(ser)
                        ser = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetTags.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/related_tags - Get the related tags for one or more tags.
    ''' </summary>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted String. Default: Today's Date</param>
    ''' <param name="Limit">Maximum number of results to return: integer between 1 and 1000, optional default: 1000</param>
    ''' <param name="Offset">Non-negative integer. default: 0</param>
    ''' <param name="OrderBy">One of the following: 'release_id', 'name', 'press_release', 'realtime_start', 'realtime_end'. default: 'release_id'</param>
    ''' <param name="SortOrder">One of the following: 'asc', 'desc'. default: 'asc'</param>
    ''' <param name="tag_Names">A semicolon delimited list of tag names to only include in the response. optional, no filtering by tag names by default</param>
    ''' <param name="tag_group_id">A tag group id to filter tags by type. no filtering by tag group by default.</param>
    ''' <param name="searchText">The words to find matching tags with. optional, no filtering by search words by default.</param>
    ''' <param name="exclude_tag_names">A semicolon delimited list of tag names that series match none of. optional, no default value.</param>
    ''' <param name="filetype">File extension that indicates the type of file to send. (xml, json). default: json</param>
    ''' <returns></returns>
    Public Function GetRelatedTags(tag_Names As String, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                            Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset,
                            Optional OrderBy As Tags.OrderBy = Tags.OrderBy.series_count, Optional SortOrder As SortOrder = SortOrder.asc,
                            Optional tag_group_id As Tags.Tag_group_id = vbNullString,
                            Optional searchText As String = vbNullString, Optional exclude_tag_names As String = vbNullString,
                            Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Tags)

        Dim sGetRelatedTags As New List(Of Tags)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "tags"
        Dim tagName_2 As String = "tag"
        Dim ser As Tags = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.RelatedTags, {pFredKey, tag_Names, realtimeStart, realtimeEnd, Limit, Offset, OrderBy, SortOrder,
                                           tag_group_id, searchText, exclude_tag_names, filetype})


        Try

            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        ser = New Tags
                        With ser
                            If item("name") IsNot Nothing Then .Name = Convert.ToString(item("name"))
                            If item("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item("group_id"))
                            If item("notes") IsNot Nothing Then .Notes = Convert.ToString(item("notes"))
                            If item("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item("created"))
                            If item("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item("popularity"))
                            If item("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item("series_count"))
                        End With
                        sGetRelatedTags.Add(ser)
                    End If
                    ser = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        ser = New Tags
                        With ser
                            If item.Attributes("name") IsNot Nothing Then .Name = Convert.ToString(item.Attributes("name").InnerText)
                            If item.Attributes("group_id") IsNot Nothing Then .GroupId = Convert.ToString(item.Attributes("group_id").InnerText)
                            If item.Attributes("notes") IsNot Nothing Then .Notes = Convert.ToString(item.Attributes("notes").InnerText)
                            If item.Attributes("created") IsNot Nothing Then .CreatedDate = Convert.ToString(item.Attributes("created").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .Popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)
                            If item.Attributes("series_count") IsNot Nothing Then .SeriesCount = Convert.ToInt32(item.Attributes("series_count").InnerText)
                        End With
                        sGetRelatedTags.Add(ser)
                        ser = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        ser = Nothing
        Return sGetRelatedTags.AsEnumerable

    End Function


    ''' <summary>
    ''' fred/tags/series - Get the series matching tags.
    ''' </summary>
    ''' <param name="tag_Names">A semicolon delimited list of tag names that series match all of. required, no default value.</param>
    ''' <param name="realtimeStart">The start of the real-time period. YYYY-MM-DD formatted string, optional, default: today's date</param>
    ''' <param name="realtimeEnd">The end of the real-time period. YYYY-MM-DD formatted string, optional, default: today's date</param>
    ''' <param name="Limit">The maximum number of results to return. integer between 1 and 1000, optional, default: 1000</param>
    ''' <param name="Offset">non-negative integer, optional, default: 0</param>
    ''' <param name="OrderBy">Order results by values of the specified attribute. optional, default: series_id</param>
    ''' <param name="SortOrder">Sort results is ascending or descending. 'asc' Or 'desc'. optional, default: asc</param>
    ''' <param name="exclude_tag_names">A semicolon delimited list of tag names that series match none of. String, optional, no default value.</param>
    ''' <param name="filetype"></param>
    ''' <returns></returns>
    Public Function GetTagSeries(tag_Names As String, Optional realtimeStart As String = vbNullString, Optional realtimeEnd As String = vbNullString,
                            Optional Limit As Integer = pLimit, Optional Offset As Integer = pOffset,
                            Optional OrderBy As Series.OrderBy = Series.OrderBy.series_id, Optional SortOrder As SortOrder = SortOrder.asc,
                            Optional exclude_tag_names As String = vbNullString,
                            Optional filetype As FileType.FileType = FileType.FileType.json) As IEnumerable(Of Series)


        Dim sGetTagSeries As New List(Of Series)

        Dim arr As JArray
        Dim obj As JObject
        Dim sr As String = ""

        Dim tagName As String = "seriess"
        Dim tagName_2 As String = "series"

        Dim rel As Series = Nothing

        If realtimeStart = vbNullString Then
            realtimeStart = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeStart = Format(Date.Parse(realtimeStart), "yyyy-MM-dd")
        End If

        If realtimeEnd = vbNullString Then
            realtimeEnd = Format(Date.Parse(pTodayDate), "yyyy-MM-dd")
        Else
            realtimeEnd = Format(Date.Parse(realtimeEnd), "yyyy-MM-dd")
        End If

        realtimeStart = ToFredDateString(CDate(realtimeStart))
        realtimeEnd = ToFredDateString(CDate(realtimeEnd))

        Dim url As String = String.Format(Urls.TagsSeries, {pFredKey, tag_Names, realtimeStart, realtimeEnd,
                                          Limit, Offset, OrderBy, SortOrder, exclude_tag_names, filetype})

        Try

            If filetype.ToString.ToUpper = "JSON" Then
                Using wc As New WebClient()
                    wc.CachePolicy = New System.Net.Cache.RequestCachePolicy(pCacheLevel)
                    sr = wc.DownloadString(url.ToString)
                End Using

                obj = JObject.Parse(sr)
                If obj Is Nothing Then GoTo EndM
                arr = obj(tagName)
                If arr Is Nothing Then GoTo EndM

                For Each item As JObject In arr
                    If item IsNot Nothing Then
                        rel = New Series
                        With rel
                            If item("id") IsNot Nothing Then .Id = Convert.ToString(item("id"))
                            If item("title") IsNot Nothing Then .Title = Convert.ToString(item("title"))

                            If item("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item("realtime_start"))
                            If item("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item("realtime_end"))

                            If item("observation_start") IsNot Nothing Then .Observation_start = Convert.ToDateTime(item("observation_start"))
                            If item("observation_end") IsNot Nothing Then .Observation_end = Convert.ToDateTime(item("observation_end"))

                            If item("frequency") IsNot Nothing Then .Frequency = Convert.ToString(item("frequency"))
                            If item("frequency_short") IsNot Nothing Then .Frequency_short = Convert.ToString(item("frequency_short"))

                            If item("units") IsNot Nothing Then .units = Convert.ToString(item("units"))
                            If item("units_short") IsNot Nothing Then .units_short = Convert.ToString(item("units_short"))

                            If item("seasonal_adjustment") IsNot Nothing Then .seasonal_adjustment = Convert.ToString(item("seasonal_adjustment"))
                            If item("seasonal_adjustment_short") IsNot Nothing Then .seasonal_adjustment_short = Convert.ToString(item("seasonal_adjustment_short"))

                            If item("last_updated") IsNot Nothing Then .last_updated = Convert.ToString(item("last_updated"))
                            If item("popularity") IsNot Nothing Then .popularity = Convert.ToInt32(item("popularity"))

                            If item("notes") IsNot Nothing Then .notes = Convert.ToString(item("notes"))
                        End With
                        sGetTagSeries.Add(rel)
                    End If
                    rel = Nothing
                Next
            ElseIf filetype.ToString.ToUpper = "XML" Then
                Dim doc As New XmlDocument
                doc.Load(url.ToString)

                Dim nodeList As XmlNodeList = doc.GetElementsByTagName(tagName_2)
                For Each item As XmlNode In nodeList
                    If item IsNot Nothing Then
                        rel = New Series
                        With rel
                            If item.Attributes("id") IsNot Nothing Then .Id = Convert.ToString(item.Attributes("id").InnerText)
                            If item.Attributes("title") IsNot Nothing Then .Title = Convert.ToString(item.Attributes("title").InnerText)

                            If item.Attributes("realtime_start") IsNot Nothing Then .RealtimeStart = Convert.ToDateTime(item.Attributes("realtime_start").InnerText)
                            If item.Attributes("realtime_end") IsNot Nothing Then .RealtimeEnd = Convert.ToDateTime(item.Attributes("realtime_end").InnerText)

                            If item.Attributes("observation_start") IsNot Nothing Then .Observation_start = Convert.ToDateTime(item.Attributes("observation_start").InnerText)
                            If item.Attributes("observation_end") IsNot Nothing Then .Observation_end = Convert.ToDateTime(item.Attributes("observation_end").InnerText)

                            If item.Attributes("frequency") IsNot Nothing Then .Frequency = Convert.ToString(item.Attributes("frequency").InnerText)
                            If item.Attributes("frequency_short") IsNot Nothing Then .Frequency_short = Convert.ToString(item.Attributes("frequency_short").InnerText)

                            If item.Attributes("units") IsNot Nothing Then .units = Convert.ToString(item.Attributes("units").InnerText)
                            If item.Attributes("units_short") IsNot Nothing Then .units_short = Convert.ToString(item.Attributes("units_short").InnerText)

                            If item.Attributes("seasonal_adjustment") IsNot Nothing Then .seasonal_adjustment = Convert.ToString(item.Attributes("seasonal_adjustment").InnerText)
                            If item.Attributes("seasonal_adjustment_short") IsNot Nothing Then .seasonal_adjustment_short = Convert.ToString(item.Attributes("seasonal_adjustment_short").InnerText)

                            If item.Attributes("last_updated") IsNot Nothing Then .last_updated = Convert.ToString(item.Attributes("last_updated").InnerText)
                            If item.Attributes("popularity") IsNot Nothing Then .popularity = Convert.ToInt32(item.Attributes("popularity").InnerText)

                            If item.Attributes("notes") IsNot Nothing Then .notes = Convert.ToString(item.Attributes("notes").InnerText)
                        End With
                        sGetTagSeries.Add(rel)
                        rel = Nothing
                    End If
                Next
                doc = Nothing
                nodeList = Nothing
            End If
        Catch ex As Exception

        End Try


EndM:
        rel = Nothing
        Return sGetTagSeries.AsEnumerable

    End Function


#End Region


End Class






