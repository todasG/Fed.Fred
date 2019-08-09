

Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq

Public Class VintageDate


    Private pVintageDate As DateTime

    Public Property Vintage_Date() As DateTime
        Get
            Return pVintageDate
        End Get
        Set(ByVal value As DateTime)
            pVintageDate = value
        End Set
    End Property

End Class
