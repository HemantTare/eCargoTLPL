
Partial Class TimePicker
    Inherits System.Web.UI.UserControl
    Private _ds As Data.DataSet = Nothing


    Private Sub fillMinutes()
        Dim i As Integer
        For i = 0 To 59
            ddl_min.Items.Add(Format(i, "00"))
        Next
    End Sub
    
    Private Sub set24Hrs()
        ddl_ampm.Visible = False
        Dim i As Integer
        For i = 0 To 23
            ddl_hr.Items.Add(Format(i, "00"))
        Next
        fillMinutes()
    End Sub

    Private Sub set12Hrs()
        ddl_ampm.Visible = True
        Dim i As Integer
        For i = 1 To 12
            ddl_hr.Items.Add(Format(i, "00"))
        Next
        fillMinutes()
        ddl_ampm.Items.Add("AM")
        ddl_ampm.Items.Add("PM")
    End Sub

    Private Sub clearAll()
        ddl_hr.Items.Clear()
        ddl_min.Items.Clear()
        ddl_ampm.Items.Clear()
    End Sub

    
    Public Function getTime() As String
        If ddl_ampm.Visible Then

            Return getHours() & ":" & getMins() & ":" & getAMPM()
        Else
            Return getHours() & ":" & getMins()
        End If
    End Function
    Public WriteOnly Property SetEnabled() As Boolean
        Set(ByVal value As Boolean)
            ddl_hr.Enabled = value
            ddl_min.Enabled = value
            ddl_ampm.Enabled = value
        End Set
    End Property
    Public Sub setTime(ByVal tStr As String)
        Dim hr, min As Integer
        Dim ampm As String
        Dim a As Array
        a = Split(tStr, ":")
        Try
            hr = CInt(a(0))
            min = CInt(a(1))

            If UBound(a) = 2 Then
                ampm = a(2)
                ddl_ampm.Visible = True

                If ampm = "AM" Then
                    setAMPM(0)
                Else
                    setAMPM(1)
                End If
                setHours(hr - 1)
            Else
                setHours(hr)
            End If

            setMins(min)
        Catch ex As Exception
            setFormat("24")
        End Try

    End Sub
    Public Function setHours(ByVal index As Integer) As Integer
        ddl_hr.SelectedIndex = index
    End Function
    Public Function setMins(ByVal index As Integer) As Integer
        ddl_min.SelectedIndex = index
    End Function
    Public Function setAMPM(ByVal index As String) As Integer
        ddl_ampm.SelectedIndex = index
    End Function

    Public Function getHours() As String
        Return ddl_hr.Items(ddl_hr.SelectedIndex).Value
    End Function
    Public Function getMins() As String
        Return ddl_min.Items(ddl_min.SelectedIndex).Value
    End Function
    Public Function getAMPM() As String
        If ddl_ampm.SelectedIndex = 0 Then
            Return "AM"
        Else
            Return "PM"
        End If
    End Function

    Public Sub setFormat(ByVal format As String)

        clearAll()
        Select Case format
            Case "12"
                set12Hrs()
                Exit Select

            Case "24"
                set24Hrs()
                Exit Select

            Case Else
                set24Hrs()
                Exit Select

        End Select



    End Sub
   
End Class
