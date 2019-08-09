

Public Class Source

    Friend _ID As Integer
    Friend _Name As String
    Friend _PressRelease As String
    Friend _RealtimeStart As Date
    Friend _RealtimeEnd As Date
    Friend _Link As String

    Public Enum Source
        Id
        Name
        PressRelease
        RealtimeStart
        RealtimeEnd
        Link
    End Enum


    Public Enum Source_OrderBy
        source_id
        name
        press_release
        realtime_start
        realtime_end
    End Enum

    Public Enum SourceRelease_OrderBy
        release_id
        name
        press_release
        realtime_start
        realtime_end
    End Enum

    Public ReadOnly Property Id() As Integer
        Get
            Return _ID
        End Get
        'Set(ByVal value As Integer)
        '    pID = value
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
