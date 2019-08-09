
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq


Public Class Tags

    Private pName As String
    Private pGroupId As String
    Private pNotes As String
    Private pCreatedDate As String
    Private pPopularity As Integer
    Private pSeriesCount As Integer


    Public Enum Tags
        Name
        Group_id
        Notes
        Created
        Popularity
        Series_Count
    End Enum

    Public Enum OrderBy
        name
        group_id
        created
        popularity

        ''' <summary>
        ''' Default Option
        ''' </summary>
        series_count
    End Enum

    Public Enum Tag_group_id

        ''' <summary>
        ''' Frequency
        ''' </summary>
        freq

        ''' <summary>
        ''' General or Concept
        ''' </summary>
        gen

        ''' <summary>
        ''' Geography
        ''' </summary>
        geo

        ''' <summary>
        ''' Geography Type
        ''' </summary>
        geot

        ''' <summary>
        ''' Release
        ''' </summary>
        rls

        ''' <summary>
        ''' Seasonal Adjustment
        ''' </summary>
        seas

        ''' <summary>
        ''' Source
        ''' </summary>
        src
    End Enum

    Public Property Name() As String
        Get
            Return pName
        End Get
        Set(ByVal value As String)
            pName = value
        End Set
    End Property

    Public Property Notes() As String
        Get
            Return pNotes
        End Get
        Set(ByVal value As String)
            pNotes = value
        End Set
    End Property

    Public Property GroupId() As String
        Get
            Return pGroupId
        End Get
        Set(ByVal value As String)
            pGroupId = value
        End Set
    End Property

    Public Property CreatedDate() As String
        Get
            Return pCreatedDate
        End Get
        Set(ByVal value As String)
            pCreatedDate = value
        End Set
    End Property

    Public Property Popularity() As Integer
        Get
            Return pPopularity
        End Get
        Set(ByVal value As Integer)
            pPopularity = value
        End Set
    End Property

    Public Property SeriesCount() As Integer
        Get
            Return pSeriesCount
        End Get
        Set(ByVal value As Integer)
            pSeriesCount = value
        End Set
    End Property

End Class