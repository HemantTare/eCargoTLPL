Imports Microsoft.VisualBasic

Partial Class Message
    Inherits System.Web.UI.Page

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Application("Message") = TextBox1.Text
    End Sub

    Private Function GetMonthName(ByVal monthnumber As Integer) As String
        Dim monthName As String = ""

        If monthnumber = 1 Then
            monthName = "January"
        ElseIf monthnumber = 2 Then
            monthName = "Febuary"
        ElseIf monthnumber = 3 Then
            monthName = "March"
        ElseIf monthnumber = 4 Then
            monthName = "April"
        ElseIf monthnumber = 5 Then
            monthName = "May"
        ElseIf monthnumber = 6 Then
            monthName = "June"
        ElseIf monthnumber = 7 Then
            monthName = "July"
        ElseIf monthnumber = 8 Then
            monthName = "August"
        ElseIf monthnumber = 9 Then
            monthName = "September"
        ElseIf monthnumber = 10 Then
            monthName = "October"
        ElseIf monthnumber = 11 Then
            monthName = "November"
        ElseIf monthnumber = 12 Then
            monthName = "December"
        End If

        Return monthName
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Dim day As Integer = Date.Now.Day
            Dim monthname As String = GetMonthName(Date.Now.Month)
            Dim year As Integer = Date.Now.Year

            TextBox1.Text = "eCargo Service will be Unavailable on " & day & "th " & monthname & " " & year & ", from 1.00 PM to 1.20 PM for maintenance purpose - By System Admin"
        End If
    End Sub
End Class
