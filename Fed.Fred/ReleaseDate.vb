
Imports System

Public Class ReleaseDate

    Friend _ReleaseId As Integer
    Friend _ReleaseName As String
    Friend _ReleaseDate As DateTime


    Public Enum OrderBy
        release_id

        release_name
        ''' <summary>
        ''' Default Option
        ''' </summary>
        release_date
    End Enum

    Public ReadOnly Property ReleaseId() As Integer
        Get
            Return _ReleaseId
        End Get
        'Set(ByVal value As Integer)
        '    pReleaseId = value
        'End Set
    End Property

    Public ReadOnly Property ReleaseName() As String
        Get
            Return _ReleaseName
        End Get
        'Set(ByVal value As String)
        '    pReleaseName = value
        'End Set
    End Property

    Public ReadOnly Property ReleaseDate() As DateTime
        Get
            Return _ReleaseDate
        End Get
        'Set(ByVal value As DateTime)
        '    pReleaseDate = value
        'End Set
    End Property


End Class
