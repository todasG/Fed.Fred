
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq

Public Class Series

        Private pID As String
        Private pTitle As String
        Private pPressRelease As String
        Private pRealtimeStart As DateTime
        Private pRealtimeEnd As DateTime

        Private pObservation_start As DateTime
        Private pObservation_end As DateTime
        Private pFrequency As String
        Private pFrequency_short As String

        Private punits As String
        Private punits_short As String
        Private pseasonal_adjustment As String
        Private pseasonal_adjustment_short As String

        Private pLast_updated As String
        Private pPopularity As Integer
        Private pGroup_popularity As Integer
        Private pNotes As String

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


    Public Property Id() As String
            Get
                Return pID
            End Get
            Set(ByVal value As String)
                pID = value
            End Set
        End Property

        Public Property Title() As String
            Get
                Return pTitle
            End Get
            Set(ByVal value As String)
                pTitle = value
            End Set
        End Property

        Public Property PressRelease() As String
            Get
                Return pPressRelease
            End Get
            Set(ByVal value As String)
                pPressRelease = value
            End Set
        End Property

        Public Property RealtimeStart() As DateTime
            Get
                Return pRealtimeStart
            End Get
            Set(ByVal value As DateTime)
                pRealtimeStart = value
            End Set
        End Property

        Public Property RealtimeEnd() As DateTime
            Get
                Return pRealtimeEnd
            End Get
            Set(ByVal value As DateTime)
                pRealtimeEnd = value
            End Set
        End Property

        Public Property Observation_start() As DateTime
            Get
                Return pObservation_start
            End Get
            Set(ByVal value As DateTime)
                pObservation_start = value
            End Set
        End Property

        Public Property Observation_end() As DateTime
            Get
                Return pObservation_end
            End Get
            Set(ByVal value As DateTime)
                pObservation_end = value
            End Set
        End Property

        Public Property Frequency() As String
            Get
                Return pFrequency
            End Get
            Set(ByVal value As String)
                pFrequency = value
            End Set
        End Property

        Public Property Frequency_short() As String
            Get
                Return pFrequency_short
            End Get
            Set(ByVal value As String)
                pFrequency_short = value
            End Set

        End Property

        Public Property units() As String
            Get
                Return punits
            End Get
            Set(ByVal value As String)
                punits = value
            End Set
        End Property

        Public Property units_short() As String
            Get
                Return punits_short
            End Get
            Set(ByVal value As String)
                punits_short = value
            End Set
        End Property

        Public Property seasonal_adjustment() As String
            Get
                Return pseasonal_adjustment
            End Get
            Set(ByVal value As String)
                pseasonal_adjustment = value
            End Set

        End Property


        Public Property seasonal_adjustment_short() As String
            Get
                Return pseasonal_adjustment_short
            End Get
            Set(ByVal value As String)
                pseasonal_adjustment_short = value
            End Set
        End Property

        Public Property last_updated() As String
            Get
                Return pLast_updated
            End Get
            Set(ByVal value As String)
                pLast_updated = value
            End Set
        End Property

        Public Property popularity() As Integer
            Get
                Return pPopularity
            End Get
            Set(ByVal value As Integer)
                pPopularity = value
            End Set

        End Property

        Public Property group_popularity() As Integer
            Get
                Return pGroup_popularity
            End Get
            Set(ByVal value As Integer)
                pGroup_popularity = value
            End Set
        End Property

        Public Property notes() As String
            Get
                Return pNotes
            End Get
            Set(ByVal value As String)
                pNotes = value
            End Set

        End Property

    End Class
