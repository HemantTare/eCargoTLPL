<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleRTOPenalty.aspx.cs"
    Inherits="Operations_Inward_FrmVehicleRTOPenalty" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/TimePicker.ascx" TagName="TimePicker" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Vehicle RTO Penalty</title>
    
<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
    <script type="text/javascript" src="../../JQuery/jquery-1.12.4.js"></script>
  <script type="text/javascript" src="../../JQuery/jquery-1.12.-ui.js"></script>
     <script type="text/javascript">
  $(document).ready(function () {
        initializeCalender();
  });
  function initializeCalender()
  {
        $(".penaltyDate").datepicker();
        $(".penaltyDate").datepicker('setDate', new Date());       
        $(".penaltyDate").datepicker("option", "dateFormat", 'dd-MM-yy');
  }
  function initializeCalenderonedit(dd)
  {
        $(".penaltyDate").datepicker();
        if(dd != undefined)
            $(".penaltyDate").datepicker('setDate', dd);  
        else
            $(".penaltyDate").datepicker('setDate', new Date());  
        $(".penaltyDate").datepicker("option", "dateFormat", 'dd-MM-yy');
  }
  </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scmdelarea" runat="server" />
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                </Triggers>
                <ContentTemplate>
                    <table class="TABLE" width="100%">
                        <tr>
                            <td class="TDGRADIENT" colspan="6">
                                <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Vehicle RTO Penalty"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="TD1" align="right" style="width: 20%">
                                Vehicle No.
                            </td>
                            <td align="left" style="width: 30%">
                                <uc2:WucVehicleSearch ID="DDLVehicle" runat="server" />
                            </td>
                            <td colspan="4" style="width: 50%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                &nbsp;</td>
                        </tr>
                        <tr runat="server" id="trPerConsignment">
                            <td class="TD1" style="width: 20%">
                            </td>
                            <td colspan="4">
                                <asp:UpdatePanel ID="upd_pnl_dgGrid" runat="server" UpdateMode="Always">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="dgGrid" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:DataGrid ID="dgGrid" AutoGenerateColumns="False" ShowFooter="True" CellPadding="3"
                                            CssClass="Grid" runat="server" OnCancelCommand="dgGrid_CancelCommand" OnDeleteCommand="dgGrid_DeleteCommand"
                                            OnEditCommand="dgGrid_EditCommand" OnItemCommand="dgGrid_ItemCommand" OnItemDataBound="dgGrid_ItemDataBound"
                                            OnUpdateCommand="dgGrid_UpdateCommand">
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                                                HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                                                BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                                            </HeaderStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Penalty Date" HeaderStyle-Width="8%">
                                                    <FooterTemplate>
                                                            <asp:TextBox id="dtp_PenaltyDate" autocomplete="off" CssClass="penaltyDate" runat="server"/>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Penalty_Date", "{0:dd-MM-yy}")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                            <asp:TextBox id="dtp_PenaltyDate" autocomplete="off" CssClass="penaltyDate" runat="server"/>
                                                    </EditItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Time" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Penalty_Time")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <uc1:TimePicker ID="PenaltyTime" runat="server" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <uc1:TimePicker ID="PenaltyTime" runat="server" />
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateColumn>
                                                
                                                <asp:TemplateColumn HeaderText="ChallanNo" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "ChallanNo")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtChallanNo" MaxLength="50" runat="server" onfocus="this.select()"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "ChallanNo") %>' CssClass="TEXTBOX" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtChallanNo" MaxLength="50" runat="server" onfocus="this.select()"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "ChallanNo") %>' CssClass="TEXTBOX" />
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateColumn>
                                                
                                                <asp:TemplateColumn HeaderText="Place" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Place")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtPlace" MaxLength="50" runat="server" onfocus="this.select()"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "Place") %>' CssClass="TEXTBOX" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtPlace" MaxLength="50" runat="server" onfocus="this.select()"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "Place") %>' CssClass="TEXTBOX" />
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Offence" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="35%">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Offence")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtOffence" runat="server" onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "Offence") %>'
                                                            CssClass="TEXTBOX" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtOffence" runat="server" onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "Offence") %>'
                                                            CssClass="TEXTBOX" />
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Amount" HeaderStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Amount")%>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtAmount" MaxLength="10" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                            onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'
                                                            CssClass="TEXTBOXNOS" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtAmount" MaxLength="10" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                            onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'
                                                            CssClass="TEXTBOXNOS" />
                                                    </FooterTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                </asp:TemplateColumn>
                                                <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit">
                                                    <HeaderStyle Width="10%" />
                                                </asp:EditCommandColumn>
                                                <asp:TemplateColumn HeaderText="Delete">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" />
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="5%" />
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="6">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="6">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                                <asp:HiddenField ID="hdnKeyID" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
