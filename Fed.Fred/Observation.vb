

Imports System
Imports System.Collections.Generic
Imports System.Text

Public Class Observation

    Friend _RealtimeStart As DateTime
    Friend _RealtimeEnd As DateTime
    Friend _Observation_Date As DateTime
    Friend _Observation_Value As Double


    Public Enum Aggregation_Method

        ''' <summary>
        ''' Average
        ''' </summary>
        avg

        ''' <summary>
        ''' Sum
        ''' </summary>
        sum

        ''' <summary>
        ''' End of Period
        ''' </summary>
        eop
    End Enum

    Public Enum OrderBy

        ''' <summary>
        ''' Observation Date. Default Option.
        ''' </summary>
        observation_date
    End Enum

    Public Enum Units

        ''' <summary>
        ''' Levels (No transformation). Default Option.
        ''' </summary>
        lin

        ''' <summary>
        ''' Change
        ''' </summary>
        chg

        ''' <summary>
        ''' Change from Year Ago
        ''' </summary>
        ch1

        ''' <summary>
        ''' Percent Change
        ''' </summary>
        pch

        ''' <summary>
        ''' Percent Change from Year Ago
        ''' </summary>
        pc1

        ''' <summary>
        ''' Compounded Annual Rate of Change
        ''' </summary>
        pca

        ''' <summary>
        ''' Continuously Compounded Rate of Change
        ''' </summary>
        cch

        ''' <summary>
        ''' Continuously Compounded Annual Rate of Change
        ''' </summary>
        cca

        ''' <summary>
        ''' Natural Log
        ''' </summary>
        log

    End Enum
    Public Enum Output_Type As Integer

        ''' <summary>
        '''  Observations by Real-Time Period
        ''' </summary>
        One = 1

        ''' <summary>
        ''' Observations by Vintage Date, All Observations
        ''' </summary>
        Two = 2

        ''' <summary>
        ''' Observations by Vintage Date, New and Revised Observations Only
        ''' </summary>
        Three = 3

        ''' <summary>
        ''' Observations, Initial Release Only
        ''' </summary>
        Four = 4

    End Enum
    ''' <summary>
    ''' The start of the real-time period.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RealtimeStart() As DateTime
        Get
            Return _RealtimeStart
        End Get
        'Set(ByVal value As DateTime)
        '    pRealtimeStart = value
        'End Set
    End Property

    ''' <summary>
    ''' The end of the real-time period. 
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property RealtimeEnd() As DateTime
        Get
            Return _RealtimeEnd
        End Get
        'Set(ByVal value As DateTime)
        '    pRealtimeEnd = value
        'End Set
    End Property

    ''' <summary>
    ''' The date the observation covers.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Observation_Date() As DateTime
        Get
            Return _Observation_Date
        End Get
        'Set(ByVal value As DateTime)
        '    pDate = value
        'End Set
    End Property

    ''' <summary>
    ''' The value of the observation.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Observation_Value() As Double
        Get
            Return _Observation_Value
        End Get
        'Set(ByVal value As Double)
        '    pValue = value
        'End Set
    End Property

End Class