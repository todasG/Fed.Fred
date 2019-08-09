

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq

Public Class VintageDate


    Friend _Vintage_Date As DateTime

    Public ReadOnly Property Vintage_Date() As DateTime
        Get
            Return _Vintage_Date
        End Get
        'Set(ByVal value As DateTime)
        '    pVintageDate = value
        'End Set
    End Property

End Class
