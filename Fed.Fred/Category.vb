
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq


Public Class Category


    Friend _Category_Id As Integer
    Friend _Category_Parent_Id As String
    Friend _Category_Name As Integer

    Public ReadOnly Property Category_Id() As Integer
        Get
            Return _Category_Id
        End Get
        'Set(ByVal value As Integer)
        '    pID = value
        'End Set
    End Property

    Public ReadOnly Property Category_Parent_Id() As Integer
        Get
            Return _Category_Parent_Id
        End Get
        'Set(ByVal value As Integer)
        '    pParentID = value
        'End Set
    End Property

    Public ReadOnly Property Category_Name() As String
        Get
            Return _Category_Name
        End Get
        'Set(ByVal value As String)
        '    pName = value
        'End Set
    End Property

End Class
