<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmUploadExcelSheet.aspx.cs" Inherits="FA_Common_Utilities_FrmUploadExcelSheet" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Excel Sheet</title>

    <script type="text/javascript">
    
     function checkFileExtension(elem) {
        var filePath = elem.value;

        if(filePath.indexOf('.') == -1)
            return false;
        
        var validExtensions ;
        var ext = filePath.substring(filePath.lastIndexOf('.') + 1).toLowerCase();
       
        validExtensions = 'xls';
            if(ext == validExtensions)
                return true;
            else
            {
             var fileUpExcelSheet  =  document.getElementById('fileUpExcelSheet');
                elem.value=' ';
                alert('The file extension ' + ext.toUpperCase() + ' is not allowed!');
            return false;
            }
    }
    
    </script>


    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />  
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Panel ID="pnl_Attachment" runat="server" GroupingText="Attachment" CssClass="PANEL"
                Width="400px">
                <table class="TABLE">
                    <tr>
                        <td class="TDGRADIENT" colspan="6">
                            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Uploading Excel Sheet"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="6" style="width: 100%" align="left">
                            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                                <tr>
                                    <td>
                                        &nbsp;<asp:FileUpload ID="file_ExcelSheet" runat="server" Width="350px" /></td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btn_Upload" CssClass="BUTTON" runat="server" Text="Upload" OnClick="btn_Upload_Click" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False"
                                Text="Please Select Excel File" />&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
