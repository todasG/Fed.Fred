
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq


Public Class Tags

    Friend _Name As String
    Friend _GroupId As String
    Friend _Notes As String
    Friend _CreatedDate As String
    Friend _Popularity As Integer
    Friend _SeriesCount As Integer


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

    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
        'Set(ByVal value As String)
        '        pName = value
        '    End Set
    End Property

    Public ReadOnly Property Notes() As String
        Get
            Return _Notes
        End Get
        'Set(ByVal value As String)
        '    pNotes = value
        'End Set
    End Property

    Public ReadOnly Property GroupId() As String
        Get
            Return _GroupId
        End Get
        'Set(ByVal value As String)
        '    pGroupId = value
        'End Set
    End Property

    Public ReadOnly Property CreatedDate() As String
        Get
            Return _CreatedDate
        End Get
        'Set(ByVal value As String)
        '    pCreatedDate = value
        'End Set
    End Property

    Public ReadOnly Property Popularity() As Integer
        Get
            Return _Popularity
        End Get
        'Set(ByVal value As Integer)
        '    pPopularity = value
        'End Set
    End Property

    Public ReadOnly Property SeriesCount() As Integer
        Get
            Return _SeriesCount
        End Get
        'Set(ByVal value As Integer)
        '    pSeriesCount = value
        'End Set
    End Property

End Class