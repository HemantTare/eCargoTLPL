<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmScheduleDriverSalary.aspx.cs"
    Inherits="Operations_Outward_FrmScheduleDriverSalary" EnableViewState="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc1" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript">

function Check_Single(chkid,gridname)
    {
       	var chk = document.getElementById(chkid);
        var row = chk.parentElement.parentElement;
        
        if(chk.checked == true)
        {
            row.style.backgroundColor = "Orange";
        }
        else
        {
            row.style.backgroundColor = "White";
        }
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Schedule Driver Salary</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" style="width: 100%">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Schedule Driver Salary"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 100%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 528px;">
                        <div style="height: 510px; width: 2000px;">
                            <asp:Panel ID="pnl_1" runat="server" Height="500px" ScrollBars="Auto">
                                <asp:DataGrid ID="dgGrid" AutoGenerateColumns="False" ShowFooter="False" CellPadding="3"
                                    OnItemDataBound="dgGrid_ItemDataBound" CssClass="Grid" runat="server">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Att">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Chk_Att" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                                                    OnClick="Check_Single(this.id,'dgGrid');" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Days">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoOfDays" CssClass="LABEL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NoOfDays")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Vehicle">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnVehicleID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Vehicle_ID")%>' />
                                                <asp:Label ID="lblVehicleNo" CssClass="LABEL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Vehicle_No")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Driver Name" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDriverName" CssClass="LABEL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DriverName")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="New Driver" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNewDriver" runat="server" CssClass="TEXTBOX" onblur="txtbox_onlostfocus(this);"
                                                    onfocus="txtbox_onfocus(this)" Text='<%# DataBinder.Eval(Container.DataItem, "NewDriver")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="From Date" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <ComponentArt:Calendar ID="dtp_DriverFromDate" runat="server" PickerFormat="Custom"
                                                    PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                                    AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" SelectedDate='<%# DataBinder.Eval(Container.DataItem, "DriverFromDate")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="To Date" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <ComponentArt:Calendar ID="dtp_DriverToDate" runat="server" PickerFormat="Custom"
                                                    PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                                    AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" SelectedDate='<%# DataBinder.Eval(Container.DataItem, "DriverToDate")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Remark1" ItemStyle-Width="300px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemark1" runat="server" CssClass="TEXTBOX" onblur="txtbox_onlostfocus(this);"
                                                    onfocus="txtbox_onfocus(this)" Text='<%# DataBinder.Eval(Container.DataItem, "Remark1")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Remark2" ItemStyle-Width="300px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemark2" runat="server" CssClass="TEXTBOX" onblur="txtbox_onlostfocus(this);"
                                                    onfocus="txtbox_onfocus(this)" Text='<%# DataBinder.Eval(Container.DataItem, "Remark2")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Remark3" ItemStyle-Width="300px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemark3" runat="server" CssClass="TEXTBOX" onblur="txtbox_onlostfocus(this);"
                                                    onfocus="txtbox_onfocus(this)" Text='<%# DataBinder.Eval(Container.DataItem, "Remark3")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Cleaner Name" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCleanerName" CssClass="LABEL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CleanerName")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="New Cleaner" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNewCleaner" runat="server" CssClass="TEXTBOX" onblur="txtbox_onlostfocus(this);"
                                                    onfocus="txtbox_onfocus(this)" Text='<%# DataBinder.Eval(Container.DataItem, "NewCleaner")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="From Date" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <ComponentArt:Calendar ID="dtp_CleanerFromDate" runat="server" PickerFormat="Custom"
                                                    PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                                    AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" SelectedDate='<%# DataBinder.Eval(Container.DataItem, "CleanerFromDate")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="To Date" HeaderStyle-Width="8%">
                                            <ItemTemplate>
                                                <ComponentArt:Calendar ID="dtp_CleanerToDate" runat="server" PickerFormat="Custom"
                                                    PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                                    AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" SelectedDate='<%# DataBinder.Eval(Container.DataItem, "CleanerToDate")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Remark4" ItemStyle-Width="300px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemark4" runat="server" CssClass="TEXTBOX" onblur="txtbox_onlostfocus(this);"
                                                    onfocus="txtbox_onfocus(this)" Text='<%# DataBinder.Eval(Container.DataItem, "Remark4")%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 100%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" style="width: 100%">
                        &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save And Exit" CssClass="BUTTON"
                            OnClick="btnSave_Click" />
                        <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" has_last_row_as_total="false"
                            runat="server"></uc1:Wuc_Export_To_Excel>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 100%">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
