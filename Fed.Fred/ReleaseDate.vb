
Imports System

Public Class ReleaseDate

    Private pReleaseId As Integer
    Private pReleaseName As String
    Private pReleaseDate As DateTime


    Public Enum OrderBy
        release_id

        release_name
        ''' <summary>
        ''' Default Option
        ''' </summary>
        release_date
    End Enum

    Public Property ReleaseId() As Integer
        Get
            Return pReleaseId
        End Get
        Set(ByVal value As Integer)
            pReleaseId = value
        End Set
    End Property

    Public Property ReleaseName() As String
        Get
            Return pReleaseName
        End Get
        Set(ByVal value As String)
            pReleaseName = value
        End Set
    End Property

    Public Property ReleaseDate() As DateTime
        Get
            Return pReleaseDate
        End Get
        Set(ByVal value As DateTime)
            pReleaseDate = value
        End Set
    End Property


End Class
