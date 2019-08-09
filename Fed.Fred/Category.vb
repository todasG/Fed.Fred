
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq


Public Class Category


    Private pID As Integer
    Private pName As String
    Private pParentID As Integer

    Public Property Category_Id() As Integer
        Get
            Return pID
        End Get
        Set(ByVal value As Integer)
            pID = value
        End Set
    End Property

    Public Property Category_Parent_Id() As Integer
        Get
            Return pParentID
        End Get
        Set(ByVal value As Integer)
            pParentID = value
        End Set
    End Property

    Public Property Category_Name() As String
        Get
            Return pName
        End Get
        Set(ByVal value As String)
            pName = value
        End Set
    End Property

End Class
