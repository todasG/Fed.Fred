

Public Class Source

        Private pID As Integer
        Private pName As String
        Private pPressRelease As String
        Private pRealtimeStart As Date
        Private pRealtimeEnd As Date
        Private pLink As String

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

    Public Property Id() As Integer
            Get
                Return pID
            End Get
            Set(ByVal value As Integer)
                pID = value
            End Set
        End Property

        Public Property Name() As String
            Get
                Return pName
            End Get
            Set(ByVal value As String)
                pName = value
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

        Public Property Link() As String
            Get
                Return pLink
            End Get
            Set(ByVal value As String)
                pLink = value
            End Set
        End Property

    End Class
