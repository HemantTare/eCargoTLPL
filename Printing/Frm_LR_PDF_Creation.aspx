<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_LR_PDF_Creation.aspx.cs"
    Inherits="Printing_Frm_LR_PDF_Creation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems"
    TagPrefix="uc2" %>

<script type="text/javascript">

    function Open_PDF_Window(Path,IsDocumentNoWise,DocumentTypeId,DocumentNo,GCNos)
    {
        
        if (IsDocumentNoWise.value =='')
        {
            IsDocumentNoWise.value = 'true';
        }

        if (DocumentTypeId.value =='')
        {
            DocumentTypeId.value = '0';
        }

        if (DocumentNo.value =='')
        {
            DocumentNo.value = '';
        } 

        if (GCNos.value =='')
        {
            GCNos.value = '';
        } 

        window.open(Path + "IsDocumentNoWise=" + IsDocumentNoWise.value + "&DocumentTypeId=" + DocumentTypeId.value + "&DocumentNo=" + DocumentNo.value + "&GCNos=" + GCNos.value,'PDFCreate','width=1000,height=800,top=50,left=50,menubar=no,resizable=yes,scrollbars=yes')
    
    }   
    
    function Onblur_DocumentNo()
    {
        
        var txt_Document_No = document.getElementById('txt_Document_No');
        var hdn_DocumentNo = document.getElementById('hdn_DocumentNo');

        hdn_DocumentNo.value = txt_Document_No.value;

   } 
   
    function Onblur_GCNos()
    {
        var txt_GCNos = document.getElementById('txt_GCNos');
        var hdn_GCNos = document.getElementById('hdn_GCNos');

        hdn_GCNos.value = txt_GCNos.value;

   } 
   
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LR PDF Creation</title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 5px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="LR PDF Creation"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 20%" class="TD1">
                    &nbsp;</td>
                <td style="width: 30%">
                    <asp:RadioButtonList ID="rbl_DocumentWiseLRWise" AutoPostBack="true" runat="server"
                        RepeatDirection="Horizontal" OnSelectedIndexChanged="rbl_DocumentWiseLRWise_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Text="Document Wise" Value="1"></asp:ListItem>
                        <asp:ListItem Text="LR Wise" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td style="width: 20%">
                    &nbsp;
                </td>
                <td style="width: 30%">
                    &nbsp;
                </td>
            </tr>
            <tr id="tr1" runat="server">
                <td style="width: 100%" colspan="4">
                    &nbsp;</td>
            </tr>
            <tr id="tr_DocumentWise" runat="server">
                <td style="width: 20%" class="TD1">
                    Document Type. :
                </td>
                <td style="width: 30%">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddl_DocumentType" runat="server" CssClass="DROPDOWN" AutoPostBack="true" OnSelectedIndexChanged="ddl_DocumentType_SelectedIndexChanged">
                                <asp:ListItem Value="30">Invoice No</asp:ListItem>
                                <asp:ListItem Value="0">LR No</asp:ListItem>
                                <asp:ListItem Value="143">Bill</asp:ListItem>
                                <asp:ListItem Value="77">PDS</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="ddl_DocumentType" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 20%" class="TD1">
                    Document No. :
                </td>
                <td style="width: 30%">
                    <asp:TextBox ID="txt_Document_No" runat="server" CssClass="TEXTBOX" MaxLength="20"
                        Onblur="Onblur_DocumentNo();">
                    </asp:TextBox>
                </td>
            </tr>
            <tr id="tr2" runat="server">
                <td style="width: 100%" colspan="4">
                    &nbsp;</td>
            </tr>
            <tr id="tr_GCWise" runat="server">
                <td style="width: 20%" class="TD1">
                    Enter LR Nos. :
                </td>
                <td style="width: 80%" colspan="3">
                    <asp:TextBox ID="txt_GCNos" runat="server" CssClass="TEXTBOX" MaxLength="250" TextMode="MultiLine"
                        Height="60px" Onblur="Onblur_GCNos();"></asp:TextBox>
                </td>
            </tr>
            <tr id="tr_GCWise2" runat="server">
                <td style="width: 70%" colspan="4">
                    &nbsp;</td>
            </tr>
            <tr id="tr3" runat="server">
                <td style="width: 100%" colspan="4">
                    &nbsp;</td>
            </tr>
            <tr id="tr4">
                <td style="width: 100%" colspan="4" align="center">
                    <asp:Button ID="btnCreatePDF" runat="server" CssClass="BUTTON" Text="Create PDF" />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:HiddenField ID="hdn_IsDocumentNoWise" runat="server" />
                            <asp:HiddenField ID="hdn_DocumentTypeId" runat="server" />
                            <asp:HiddenField ID="hdn_DocumentNo" runat="server" />
                            <asp:HiddenField ID="hdn_GCNos" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="rbl_DocumentWiseLRWise" />
                            <asp:PostBackTrigger ControlID="ddl_DocumentType" />
                            <asp:PostBackTrigger ControlID="txt_Document_No" />
                            <asp:PostBackTrigger ControlID="txt_GCNos" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>

</body>
</html>
