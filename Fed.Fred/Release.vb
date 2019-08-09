

Imports System
Imports System.Collections.Generic
Imports System.Text


Public Class Release

    Friend _Release_Id As Integer
    Friend _Name As String
    Friend _PressRelease As String
    Friend _RealtimeStart As Date
    Friend _RealtimeEnd As Date
    Friend _Link As String

    Public Enum Release
        Release_id
        Name
        Press_release
        RealtimeStart
        RealtimeEnd
        Link
    End Enum

    Public Enum OrderBy

        ''' <summary>
        ''' Default Option
        ''' </summary>
        release_id

        name
        press_release
        realtime_start
        realtime_end
    End Enum

    Public ReadOnly Property Release_Id() As Integer
        Get
            Return _Release_Id
        End Get
        'Set(ByVal value As Integer)
        '    prelease_Id = value
        'End Set
    End Property

    Public ReadOnly Property Name() As String
        Get
            Return _Name
        End Get
        'Set(ByVal value As String)
        '    pName = value
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

    Public ReadOnly Property Link() As String
        Get
            Return _Link
        End Get
        'Set(ByVal value As String)
        '    pLink = value
        'End Set
    End Property

End Class
