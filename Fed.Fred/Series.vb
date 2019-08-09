
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq

Public Class Series

    Friend _Id As String
    Friend _Title As String
    Friend _PressRelease As String
    Friend _RealtimeStart As DateTime
    Friend _RealtimeEnd As DateTime

    Friend _Observation_start As DateTime
    Friend _Observation_end As DateTime
    Friend _Frequency As String
    Friend _Frequency_short As String

    Friend _units As String
    Friend _units_short As String
    Friend _seasonal_adjustment As String
    Friend _seasonal_adjustment_short As String

    Friend _Last_updated As String
    Friend _Popularity As Integer
    Friend _Group_popularity As Integer
    Friend _Notes As String

    Public Enum OrderBy
        ''' <summary>
        ''' series id
        ''' </summary>
        series_id

        ''' <summary>
        ''' title
        ''' </summary>
        title
        units
        frequency
        seasonal_adjustment
        realtime_start
        realtime_end
        last_updated
        observation_start
        observation_end
        popularity
        group_popularity
    End Enum

    Public Enum Search_OrderBy
        ''' <summary>
        ''' series id
        ''' </summary>
        series_id

        ''' <summary>
        ''' title
        ''' </summary>
        title
        units
        frequency
        seasonal_adjustment
        realtime_start
        realtime_end
        last_updated
        observation_start
        observation_end
        popularity
        group_popularity
        search_rank
    End Enum

    Public Enum Search_Type

        ''' <summary>
        ''' 'full_text' searches series attributes title, units, frequency, and tags by parsing words into stems. This makes it possible for searches like 'Industry' to match series containing related words such as 'Industries'. Encode spaces with %20. Default Option.
        ''' </summary>
        full_text

        ''' <summary>
        ''' 'series_id' performs a substring search on series IDs. Searching for 'ex' will find series containing 'ex' anywhere in a series ID. '*' can be used to anchor searches and match 0 or more of any character.
        ''' </summary>
        series_id
    End Enum

    Public Enum Filter_Variable
        frequency
        units
        seasonal_adjustment
    End Enum

    Public Enum Release
        Id
        Title
        PressRelease
        RealtimeStart
        RealtimeEnd
        Observation_start
        Observation_end
        Frequency
        Frequency_short
        Units
        Units_short
        Seasonal_adjustment
        Seasonal_adjustment_short
        Last_updated
        Popularity
        Group_popularity
        Notes
    End Enum


    Public ReadOnly Property Id() As String
        Get
            Return _ID
        End Get
        'Set(ByVal value As String)
        '    pID = value
        'End Set
    End Property

    Public ReadOnly Property Title() As String
        Get
            Return _Title
        End Get
        'Set(ByVal value As String)
        '    pTitle = value
        'End Set
    End Property

    Public ReadOnly Property PressRelease() As String
        Get
            Return _PressRelease
        End Get
        'Set(ByVal value As String)
        '    pPressRelease = value
        'End Set
    End Property

    Public ReadOnly Property RealtimeStart() As DateTime
        Get
            Return _RealtimeStart
        End Get
        'Set(ByVal value As DateTime)
        '    pRealtimeStart = value
        'End Set
    End Property

    Public ReadOnly Property RealtimeEnd() As DateTime
        Get
            Return _RealtimeEnd
        End Get
        'Set(ByVal value As DateTime)
        '    pRealtimeEnd = value
        'End Set
    End Property

    Public ReadOnly Property Observation_start() As DateTime
        Get
            Return _Observation_start
        End Get
        'Set(ByVal value As DateTime)
        '    pObservation_start = value
        'End Set
    End Property

    Public ReadOnly Property Observation_end() As DateTime
        Get
            Return _Observation_end
        End Get
        'Set(ByVal value As DateTime)
        '    pObservation_end = value
        'End Set
    End Property

    Public ReadOnly Property Frequency() As String
        Get
            Return _Frequency
        End Get
        'Set(ByVal value As String)
        '    pFrequency = value
        'End Set
    End Property

    Public ReadOnly Property Frequency_short() As String
        Get
            Return _Frequency_short
        End Get
        'Set(ByVal value As String)
        '    pFrequency_short = value
        'End Set

    End Property

    Public ReadOnly Property units() As String
        Get
            Return _units
        End Get
        'Set(ByVal value As String)
        '    punits = value
        'End Set
    End Property

    Public ReadOnly Property units_short() As String
        Get
            Return _units_short
        End Get
        'Set(ByVal value As String)
        '    punits_short = value
        'End Set
    End Property

    Public ReadOnly Property seasonal_adjustment() As String
        Get
            Return _seasonal_adjustment
        End Get
        'Set(ByVal value As String)
        '    pseasonal_adjustment = value
        'End Set

    End Property


    Public ReadOnly Property seasonal_adjustment_short() As String
        Get
            Return _seasonal_adjustment_short
        End Get
        'Set(ByVal value As String)
        '    pseasonal_adjustment_short = value
        'End Set
    End Property

    Public ReadOnly Property last_updated() As String
        Get
            Return _Last_updated
        End Get
        'Set(ByVal value As String)
        '    pLast_updated = value
        'End Set
    End Property

    Public ReadOnly Property popularity() As Integer
        Get
            Return _Popularity
        End Get
        'Set(ByVal value As Integer)
        '    pPopularity = value
        'End Set

    End Property

    Public ReadOnly Property group_popularity() As Integer
        Get
            Return _Group_popularity
        End Get
        'Set(ByVal value As Integer)
        '    pGroup_popularity = value
        'End Set
    End Property

    Public ReadOnly Property notes() As String
        Get
            Return _Notes
        End Get
        'Set(ByVal value As String)
        '    pNotes = value
        'End Set

    End Property

End Class
