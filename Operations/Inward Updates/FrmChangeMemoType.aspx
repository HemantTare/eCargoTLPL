<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmChangeMemoType.aspx.cs" Inherits="Operations_Inward_Updates_FrmChangeMemoType" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc2" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript"  src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/Operations/Inward Update/ManifestTypeChange.js"></script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Manifest Type Change</title>
        <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="scm_memoType" runat="server"></asp:ScriptManager>
            <table class="TABLE">
              <tr>
                <td class="TDGRADIENT" colspan="4">
                <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="MANIFEST TYPE CHANGE"></asp:Label>
                </td>
              </tr>
              <tr>
                <td class="TD1" style="width: 20%;">
                  <asp:Label ID="lbl_vehicle_no" runat="server" CssClass="LABEL" Text="Vehicle No:"></asp:Label>
                </td>
                <td style="width: 29%;">
                  <uc2:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
                </td>
                <td class="TD1" style="width: 20%"></td>
                <td style="width: 29%;"></td>
              </tr>
              </table>
              
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <ContentTemplate>
                    <table class="TABLE">
                    <tr>
                      <td class="TD1" style="width: 20%" runat="server">
                        <asp:Label ID="lbl_ManifestNo" runat="server" CssClass="LABEL" Text="Manifest No:"></asp:Label>
                      </td>
                      <td style="width: 29%;">
                        <asp:DropDownList ID="ddl_ManifestNo" AutoPostBack="true" runat="server" CssClass="DROPDOWN"  OnSelectedIndexChanged="ddl_ManifestNo_SelectedIndexChanged"/>
                        <asp:HiddenField ID="hdn_GCCaption" runat="server" />
                        <asp:HiddenField ID="hdn_MemoTypeId" runat="server" />
                        <asp:HiddenField ID="hdn_MemoFromBranchId" runat="server" />
                        <asp:HiddenField ID="hdn_MemoPreviousTypeId" runat="server" />
                      </td>
                    <td class="TD1" style="width: 20%"></td>
                    <td style="width: 29%;"></td>
                    </tr>
                    <tr>
                      <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_MemoDate" runat="Server" CssClass="LABEL" Text="Manifest Date:"></asp:Label>
                      </td>
                      <td style="width: 29%;">
                        <asp:TextBox ID="txt_MemoDate" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOX" runat="server" Font-Bold="True"
                        Width="95%"/>
                      </td>
                      <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_MemoFromBranch" runat="server" CssClass="LABEL" Text="Manifest From Branch:"></asp:Label>
                      </td>
                      <td style="width: 29%;">
                        <asp:TextBox ID="txt_MemoFromBranch" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOX" runat="server" Font-Bold="True" Width="95%"/>
                      </td>
                    </tr>
                      
                    <tr>
                      <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_MemoType" runat="Server" CssClass="LABEL" Text="Previous Manifest Type:"></asp:Label>
                      </td>
                      <td style="width: 29%;">
                        <asp:TextBox ID="txt_MemoType" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOX" runat="server" Font-Bold="True"
                        Width="95%"/>
                      </td>
                      <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_MemoToBranch" runat="server" CssClass="LABEL" Text="Manifest To Branch:"></asp:Label>
                      </td>
                      <td style="width: 29%;">
                        <asp:TextBox ID="txt_memoToBranch" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOX" runat="server" Font-Bold="True" Width="95%"/>
                          <asp:HiddenField ID="hdn_MemoToBranchId" runat="server" />
                      </td>
                    </tr>
                      
                    <tr>
                      <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_VehicleCategory" runat="Server" CssClass="LABEL" Text="Vehicle Category:"></asp:Label>
                      </td>
                      <td style="width: 29%;">
                        <asp:TextBox ID="txt_VehicleCategory" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOX" runat="server" Font-Bold="True"
                        Width="95%"/>
                      </td>
                      <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_TotalNoOfGC" runat="server" CssClass="LABEL" Text="Total No Of GC:"></asp:Label>
                      </td>
                      <td style="width: 29%;">
                        <asp:TextBox ID="txt_TotalNoOfGC" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOX" runat="server" Font-Bold="True"
                        Width="95%"/>
                      </td>
                    </tr>
                    <tr>
                      <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_TotalNoOfLoadedArticles" runat="Server" CssClass="LABEL" Text="Total Loaded Articles:"></asp:Label>
                      </td>
                      <td style="width: 29%;">
                        <asp:TextBox ID="txt_TotalNoOfLoadedArticles" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOX" runat="server" Font-Bold="True" Width="95%"/>
                      </td>
                      <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_total_loaded_wt" runat="Server" CssClass="LABEL" Text="Total Loaded Weight:"></asp:Label>
                      </td>
                      <td style="width: 29%;">
                        <asp:TextBox ID="txt_Total_Loaded_Wt" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOX" runat="server" Font-Bold="True" Width="95%"/>
                      </td>
                    </tr>
                    <tr>
                      <td class="TD1" style="width: 20%">
                        <asp:Label ID="Label1" runat="Server" CssClass="LABEL" Text="New Manifest Type:"></asp:Label>
                      </td>
                      <td style="width: 29%;">
                          <asp:DropDownList ID="ddlMemoType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMemoType_SelectedIndexChanged">
                          </asp:DropDownList>
                        <asp:TextBox ID="txt_New_Memo_Type" BackColor="Transparent" BorderColor="Transparent"
                        BorderStyle="Solid" ReadOnly="True" CssClass="TEXTBOX" runat="server" Font-Bold="True" Width="95%" Visible="False"/>
                      </td>
                      <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_menifest_to" runat="server" CssClass="LABEL" Text="Manifest To:"/>
                      </td>
                      <td style="width: 29%">
                        <table class="TABLE">
                          <tr id="tr_ddl_memo_to" runat="server">
                            <td>
                              <cc1:DDLSearch ID="ddl_MenifestTo" runat="server" AllowNewText="True" IsCallBack="True"
                              CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetManifestTypeToBranch" CallBackAfter="2" Text="" />
                            </td>
                          </tr>
                        
                          <tr>
                            <td id="tr_txt_memo_to" runat="server">
                              <asp:TextBox ID="txt_MenifestTo" runat="server" CssClass="TEXTBOX"
                              Width="85%" BorderWidth="1px" MaxLength="50"/>
                            </td>
                          </tr>
                        </table>
                      </td>
                    </tr>
                </table>
           </ContentTemplate>
           <Triggers>                                     
               <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
           </Triggers>
        </asp:UpdatePanel>
       
        <table class="TABLE">
         <tr>
             <td colspan="2">
                 <asp:UpdatePanel ID="up_Grid" runat="server">
                     <ContentTemplate>
                         <div id="Div_Memoinformation" class="DIV" style="height: 330px">
                             <asp:DataGrid ID="dg_MemoInformation" runat="server" 
                             CssClass="Grid" AutoGenerateColumns="False" OnItemDataBound="dg_MemoInformation_ItemDataBound">
                             <FooterStyle CssClass="GRIDFOOTERCSS" />
                             <PagerStyle Mode="NumericPages" CssClass="GRIDVIEWPAGERCSS" />
                            <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                             <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                 <Columns>
                                     <asp:TemplateColumn>
                                         <HeaderStyle Width="5%" />
                                         <HeaderTemplate>
                                             <asp:CheckBox ID="chk_AllSelect" runat="server" onclick="Check_All(this)" />
                                         </HeaderTemplate>
                                         <ItemTemplate>
                                             <asp:CheckBox ID="chk_Attach" runat="server" Checked='<%#Convert.ToBoolean(Eval("attached"))%>' />
                                         </ItemTemplate>
                                     </asp:TemplateColumn>
                                     <asp:BoundColumn DataField="gc_no" HeaderText=" GC No"></asp:BoundColumn>
                                     <asp:BoundColumn DataField="gc_date" HeaderText="Booking Date"></asp:BoundColumn>
                                     <asp:BoundColumn DataField="Loaded_Articles" HeaderText="Loaded Articles"></asp:BoundColumn>
                                     <asp:BoundColumn DataField="Loaded_Actual_Wt" HeaderText="Loaded Actual Wt"></asp:BoundColumn>
                                 </Columns>
                             </asp:DataGrid>

                           <asp:HiddenField ID="hdn_total_attached_gc" runat="server" />
                         </div>
                     </ContentTemplate>
                     <Triggers>                                     
                         <asp:AsyncPostBackTrigger ControlID="ddl_ManifestNo" />
                     </Triggers>
                 </asp:UpdatePanel>
             </td>
         </tr>
          <tr>
            <td class="TD1" style="width: 7%;">Reason:</td>
            <td>
             <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                 <ContentTemplate>
                      <asp:TextBox ID="txt_reason" CssClass="TEXTBOX" TextMode="MultiLine" MaxLength="250" runat="server"></asp:TextBox>
                 </ContentTemplate>
                 <Triggers>                                     
                     <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                 </Triggers>
             </asp:UpdatePanel>
            </td>
          </tr>
        <tr>
          <td align="center" colspan="2">
             <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                 <ContentTemplate>
                    <asp:Button ID="btn_Save" OnClientClick="return Allow_To_Save()" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click" />
                 </ContentTemplate>
                 <Triggers>                                     
                     <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                 </Triggers>
             </asp:UpdatePanel>
            
          </td>
        </tr>
      <tr>
        <td colspan="2">
           <asp:UpdatePanel ID="UpdatePanel2" runat="server">
               <ContentTemplate>
                  <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:Label>
                  <asp:HiddenField ID="hdn_saved_status" Value="" runat="server" />
               </ContentTemplate>
               <Triggers>                                     
                   <asp:AsyncPostBackTrigger ControlID="btn_Save" />
               </Triggers>
           </asp:UpdatePanel>
        </td>
      </tr>
    </table>
        </div>
    </form>
</body>
</html>
