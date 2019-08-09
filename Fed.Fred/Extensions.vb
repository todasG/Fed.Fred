
Imports System
Imports System.Globalization
Imports System.Runtime.CompilerServices

Module Extensions

        Private Const DateFormat As String = "yyyy-MM-dd"
        Private Const DateTimeFormat As String = "yyyy-MM-dd HH:mm:sszz"

    ''' <summary>
    ''' Converts a string to a FRED formated date string.
    ''' </summary>
    ''' <param name="value">The date to convert.</param>
    ''' <returns></returns>
    <Extension()>
    Public Function ToFredDateString(value As DateTime)

        Return value.ToString(DateFormat)

    End Function

    ''' <summary>
    ''' Converts a FRED formatted date string to a DateTime.
    ''' </summary>
    ''' <param name="value">The date string to create the date from</param>
    ''' <returns></returns>
    <Extension()>
    Public Function ToFredDate(value As DateTime)

        Return DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture)

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    <Extension()>
    Public Function ToFredDateTime(value As String)

        Return DateTime.ParseExact(value, DateFormat, CultureInfo.InvariantCulture)

    End Function

    <Extension()>
    Public Function ToCurrentDate(value As DateTime)

        Return DateTime.Now

    End Function


End Module