<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmDesktopInformationDispaly.aspx.cs" Inherits="Admin_Users_frmDesktopInformationDispaly" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="TABLE">
            <tr>
                <td class="TDGradient" style="width: 100%">&nbsp;
                <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Desktop Message Information"></asp:Label></td>
            </tr>
            <tr><td style="width:100%">&nbsp;</td></tr>
            <tr>
                <td style="width:100%">
                    <fieldset>
                        <legend><b> Sevice Information Message </b></legend>
                        <table width="100%">
                            <tr><td style="width:100%" colspan="2">&nbsp;</td></tr>
                            <tr>
                                <td style="width:15%">Message Line 1 :</td>
                                <td style="width:85%">
                                    <asp:TextBox ID="txt_SIMLine1" runat="server" MaxLength="100" CssClass="TEXTBOX"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:15%">Message Line 2 :</td>
                                <td style="width:85%">
                                    <asp:TextBox ID="txt_SIMLine2" runat="server" MaxLength="100" CssClass="TEXTBOX"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:15%">Message Line 3 :</td>
                                <td style="width:85%">
                                    <asp:TextBox ID="txt_SIMLine3" runat="server" MaxLength="100" CssClass="TEXTBOX"></asp:TextBox>
                                </td>
                            </tr>
                            <tr><td colspan="2">&nbsp;</td></tr>
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td style="width:100%">
                    <fieldset>
                        <legend ><b> General Information Message </b></legend>
                        <table width="100%">
                            <tr><td style="width:100%" colspan="2">&nbsp;</td></tr>
                            <tr>
                                <td style="width:15%">Message Line 1 :</td>
                                <td style="width:85%">
                                    <asp:TextBox ID="txt_GIMLine1" runat="server" MaxLength="100" CssClass="TEXTBOX"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:15%">Message Line 2 :</td>
                                <td style="width:85%">
                                    <asp:TextBox ID="txt_GIMLine2" runat="server" MaxLength="100" CssClass="TEXTBOX"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:15%">Message Line 3 :</td>
                                <td style="width:85%">
                                    <asp:TextBox ID="txt_GIMLine3" runat="server" MaxLength="100" CssClass="TEXTBOX"></asp:TextBox>
                                </td>
                            </tr>
                            <tr><td colspan="2">&nbsp;</td></tr>
                            
                        </table>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td align="center" style="width:100%">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClick="On_btnSave_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>
</body>
</html>
