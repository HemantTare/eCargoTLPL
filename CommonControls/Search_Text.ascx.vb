
Partial Class Search_Text
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btn_Search.Attributes.Add("onclick", "return Search_Text(" & SearchText.ClientID & ")")

    End Sub
End Class
