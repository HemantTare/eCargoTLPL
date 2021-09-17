<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucShortExcessSettelment.ascx.cs" Inherits="Operations_ShortExcess_WucShortExcessSettelment" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript"  src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/CommonReports.js"></script>
<script type="text/javascript"  src="../../Javascript/ddlsearch.js"></script>

<asp:ScriptManager ID="SCM_SES" runat="Server"></asp:ScriptManager>
<script type="text/javascript" >
function get_button_nullsession_clientid()
{
	btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}

function Allow_To_Save()
{
    var ATS = true;
     return ATS; 
}
</script>
<table class="TABLE" border="0">
    <tr>
        <td class="TDGRADIENT" style="width: 100%" colspan="6">&nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Short Excess Settlement"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>        
    <tr>
        <td colspan="6">
            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />        
        </td>
    </tr>
     <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="6"> 
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:Panel ID="pnl_shortexcess" runat="server" GroupingText="Short Excess Details" ScrollBars="Auto">
              <div id ="Div_SES"  runat="server" class="DIV" style="height:400px">
                    <asp:DataGrid ID="dg_SESetelment" runat="server" AutoGenerateColumns="False"
                        CssClass="GRID" style="border-top-style:none" Width="97%">
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <Columns> 
                            <asp:TemplateColumn>
                               <ItemTemplate>
                                   <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>' runat="server"/>
                               </ItemTemplate>
                                <HeaderStyle Width="2%" />
                            </asp:TemplateColumn>
                            
                            <asp:BoundColumn DataField="GC_No" HeaderText=" No">
                                <HeaderStyle Width="10%" />
                            </asp:BoundColumn>
                            
                            <asp:TemplateColumn HeaderText="Exess Articles">
                            <HeaderStyle Width="10%" />
                                <ItemTemplate>
                                     <asp:Label ID="lbl_Exess_Articles" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Balance_Articles") %>' CssClass="LABEL"></asp:Label> 
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Short At">
                                <ItemTemplate>
                                     <cc1:DDLSearch ID="ddl_Short_At" runat="server" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetShortExcessBranch" CallBackAfter="2" PostBack="true" OnTxtChange="ddl_Short_At_TxtChange"/>
                                </ItemTemplate>
                                <HeaderStyle Width="15%" />
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="No">
                                <ItemTemplate>
                                     <asp:DropDownList ID="ddl_GC_No" AutoPostBack="true" runat="server" CssClass="DROPDOWN" Width="90%" OnSelectedIndexChanged="ddl_GC_No_SelectedIndexChanged"></asp:DropDownList> 
                                </ItemTemplate>
                                <HeaderStyle Width="10%" />
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Short Articles">
                            <HeaderStyle Width="10%" />
                                <ItemTemplate>
                                     <asp:Label ID="lbl_Short_Articles" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Short_Articles") %>' CssClass="LABEL"></asp:Label> 
                                     <asp:HiddenField ID="hdn_ExcessUnknownId" Value='<%# DataBinder.Eval(Container.DataItem, "Excess_UnKnown_Id") %>' runat="server"></asp:HiddenField> 
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="Settled Articles">
                                <ItemTemplate>
                                     <asp:TextBox ID="txt_Settled_Articles" Width="90%" runat="server" onkeypress="return Only_Integers(this,event)" MaxLength="5" CssClass="TEXTBOXNOS"></asp:TextBox> 
                                </ItemTemplate>
                                <HeaderStyle Width="10%" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </asp:Panel>
             </ContentTemplate>
       </asp:UpdatePanel>
        </td>        
    </tr>   
    <tr>
        <td colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" AccessKey="N" OnClick="btn_Save_Click"/>
            <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window" OnClick="btn_null_session_Click" />
        </td>
    </tr>   
     <tr>
        <td colspan="6">
         <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
             </ContentTemplate>
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_SESetelment" />
             </Triggers>
       </asp:UpdatePanel>
        </td>
    </tr>   
     <tr><td colspan="6">
     </td></tr>
    <tr><td colspan="6">&nbsp;</td></tr>    
</table>