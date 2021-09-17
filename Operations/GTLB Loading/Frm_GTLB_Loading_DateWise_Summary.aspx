<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_GTLB_Loading_DateWise_Summary.aspx.cs"
    Inherits="Operations_GTLB_Loading_Frm_GTLB_Loading_DateWise_Summary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript">


function checkuncheckIsHoliday(chk_IsHoliday,ddl_Remark)
{
        
    var chk_IsHoliday1 = document.getElementById(chk_IsHoliday);    
    var ddl_Remark1 = document.getElementById(ddl_Remark);
    
    if(chk_IsHoliday1.checked)
    {
        ddl_Remark1.disabled = false;
    }
    else
    {
        ddl_Remark1.value = '0';
        ddl_Remark1.disabled = true;
    }
}

function GTLB_LoadingDetails(Path)
{
          
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-50);
        var popH = h-20;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        
        window.open(Path, 'GTLB_LoadingDetails', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

   function OpenReport(Path)
    {
    
        var ddl_Month = document.getElementById('ddl_Month');
        var ddl_Period = document.getElementById('ddl_Period');
    
        
    
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path + 'MonthId=' + ddl_Month.value + '&Period=' + ddl_Period.value , 'GTLBLoadingReport', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }    
    
    function Open_FORM1_Window(Path)
    { 
        window.open(Path,'GTLBFORM1','width=1000,height=800,top=50,left=50,menubar=no,resizable=yes,scrollbars=yes');
        return false;
    }   
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GTLB Loading DateWise Summary</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="3">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="GTLB Loading DateWise Summary"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 10%">
                        Period :</td>
                    <td style="width: 20%">
                        <asp:DropDownList ID="ddl_Month" runat="server" CssClass="DROPDOWN" Width="90%">
                        </asp:DropDownList></td>
                    <td style="width: 70%">
                        <asp:DropDownList ID="ddl_Period" runat="server" CssClass="DROPDOWN" Width="50%">
                            <asp:ListItem Value="1">1 To 15</asp:ListItem>
                            <asp:ListItem Value="2">16 To 31</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="TD1" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <asp:Button ID="btnView" runat="server" CssClass="BUTTON" OnClick="btnView_Click"
                            Text="View" />&nbsp; &nbsp;
                        <asp:Button ID="btnReport" runat="server" CssClass="BUTTON" Text="Report" /></td>
                </tr>
                <tr>
                    <td align="left" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                                <asp:HiddenField ID="hdnKeyID" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnView" />
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr style="height: 250px" valign="top">
                    <td style="width: 40%" colspan="3">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel12">
                            <ContentTemplate>
                                <asp:Panel ID="pnl_Details" runat="server" Height="450px" ScrollBars="None">
                                    <asp:DataGrid ID="dg_Details" runat="server" ShowFooter="False" AllowPaging="False"
                                        CssClass="GRID" AllowSorting="False" AllowCustomPaging="False" AutoGenerateColumns="False"
                                        PageSize="15" OnItemDataBound="dg_Details_ItemDataBound">
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="GRIDHEADERCSS" BackColor="Orange" />
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Date" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Date")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="IsHoliday" Visible="true" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_IsHoliday" Checked='<%#DataBinder.Eval(Container.DataItem,"IsHoliday")%>'
                                                        runat="server" Style="text-align: center" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Remark" Visible="true" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddl_Remark" Width="100%" runat="server" Style="text-align: Left"
                                                        OnSelectedIndexChanged="ddl_Remark_SelectedIndexChanged" Enabled="false" AutoPostBack="True" />
                                                    <asp:TextBox ID="txt_Remark" Width="98%" runat="server" Style="text-align: Left"
                                                        Text="Sunday" Enabled="false" Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="ToBeIncluded">
                                                <ItemTemplate>
                                                    <ComponentArt:Calendar ID="dtp_DateToBeIncluded" runat="server" CellPadding="2" ControlType="Picker"
                                                        PickerCssClass="PICKER" PickerCustomFormat="dd MMM yyyy" PickerFormat="Custom"
                                                        SelectedDate="2008-10-20" Visible="false">
                                                    </ComponentArt:Calendar>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                             <asp:TemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnFORM1" runat="server" Text='Print FORM 1'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"  />
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnView" />
                                <asp:AsyncPostBackTrigger ControlID="dg_Details" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <asp:Button ID="btnSave" runat="server" CssClass="BUTTON" OnClick="btnSave_Click"
                            Text="Save" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
