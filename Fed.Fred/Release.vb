

Imports System
Imports System.Collections.Generic
Imports System.Text


Public Class Release

        Private prelease_Id As Integer
        Private pName As String
        Private pPressRelease As String
        Private pRealtimeStart As Date
        Private pRealtimeEnd As Date
        Private pLink As String

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

    Public Property Release_Id() As Integer
            Get
                Return prelease_Id
            End Get
            Set(ByVal value As Integer)
                prelease_Id = value
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
