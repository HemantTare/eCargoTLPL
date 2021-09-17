<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmGCConsigneeUpdate.aspx.cs"
    Inherits="Operations_Inward_Updates_FrmGCConsigneeUpdate" %>

<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Operations/Inward Update/GCConsigneeUpdate.js"></script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Consignee Update</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" language="javascript">
    
function Allow_To_Save()
{
    var ATS = true;
     return ATS; 
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="scm_GCConsigneeUpdate" runat="server">
            </asp:ScriptManager>
            <table class="TABLE" border="0">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="Consignee Update"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_GC_No" runat="server" CssClass="LABEL" Text="GC No :" Font-Bold="True"></asp:Label></td>
                    <td style="width: 29%">
                        <asp:Label ID="lbl_GCno_Display" runat="server" Font-Bold="True"></asp:Label></td>
                    <td style="width: 1%">
                    </td>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_ConsigneeName" runat="server" CssClass="LABEL" Text="Consignee Name :"
                            Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:Label ID="lbl_ConsigneeNameDisplay" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 1%">
                    </td>
                </tr>
                <%-- <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_Client" runat="server" Text="Consignee Name :" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <cc1:DDLSearch ID="ddl_Client" runat="server" AllowNewText="false" PostBack="True"
                            IsCallBack="True" CallBackAfter="2" OnTxtChange="ddl_Client_TxtChange" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchAllClientBranchCity" />
                    </td>
                    <td class="TDMANDATORY" style="width:1%">*</td>                        
                    <td style="width:50%" colspan="3"></td>
                </tr>--%>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_Client" runat="server" Text="Consignee Name :" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txt_ConsigneeName" runat="server" AutoCompleteType="Disabled" CssClass="TEXTBOX"
                            EnableViewState="False" MaxLength="100" onblur="Client_LostFocus(this,'lst_Consignees')"
                            onfocus="On_Focus('txt_ConsigneeName','lst_Consignees','Consignee');" onkeydown="return on_keydown(event,this,'lst_Consignees'),hotKey(event,this,'Consignee');"
                            onkeyup="NewGC_AllSearch(event,this,'lst_Consignees','Consignee',2);" Width="90%"></asp:TextBox>
                        <asp:ListBox ID="lst_Consignees" runat="server" Height="230px" onfocus="listboxonfocus('txt_ConsigneeName')"
                            Style="z-index: 1000; left: 207px; position: absolute; top: 120px" TabIndex="80">
                        </asp:ListBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td colspan="3" style="width: 50%">
                        <asp:HiddenField ID="hdn_ToLocationId" runat="server" />
                    </td>
                </tr>
                <tr id="tr_ContactPerson" runat="server">
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_ContactPerson" runat="server" Text="Contact Person :" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_ContactPerson" runat="server" CssClass="TEXTBOX" MaxLength="100"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_ConsigneeName" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%" class="TDMANDATORY">
                        *</td>
                    <td style="width: 50%" colspan="3">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_Address1" runat="server" Text="Address1 :" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 79%" colspan="4">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_Address1" runat="server" CssClass="TEXTBOX" Width="100%" MaxLength="100"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_ConsigneeName" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_Address2" runat="server" Text="Address2 :" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 79%" colspan="4">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_Address2" runat="server" CssClass="TEXTBOX" Width="100%" MaxLength="100"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_ConsigneeName" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_Pincode" runat="server" CssClass="LABEL" Text="PinCode :"></asp:Label></td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_Pincode" runat="server" CssClass="TEXTBOX" MaxLength="60" onkeypress="return Only_Integers(this,event)"
                                    onblur="valid(this)"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_ConsigneeName" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_Stdcode" runat="server" CssClass="LABEL" Text="STD Code :"></asp:Label></td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_StdCode" runat="server" CssClass="TEXTBOX" Width="100%" MaxLength="15"
                                    onkeypress="return Only_Integers(this,event)" onblur="valid(this)"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_ConsigneeName" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_Phone" runat="server" CssClass="LABEL" Text="Phone :"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_Phone" runat="server" CssClass="TEXTBOX" MaxLength="15" onkeypress="return Only_Integers(this,event)"
                                    onblur="valid(this)"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_ConsigneeName" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_Mobile" runat="server" Text="Mobile :" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_Mobile" runat="server" CssClass="TEXTBOX" Width="100%" MaxLength="10"
                                    onkeypress="return Only_Integers(this,event)" onblur="valid(this)"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_ConsigneeName" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%">
                    </td>
                </tr>
                <tr id="tr_csttinno" runat="server">
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_CSTTinNo" runat="server" Text="CST TIN No :" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_CSTTinNo" runat="server" CssClass="TEXTBOX" MaxLength="50"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_ConsigneeName" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%">
                    </td>
                    <td style="width: 50%" colspan="3">
                        &nbsp;</td>
                </tr>
                <tr id="tr_serviceTaxNo" runat="server">
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_ServiceTAXNo" runat="server" Text="Service TAX No :" CssClass="LABEL"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_ServiceTaxNo" runat="server" CssClass="TEXTBOX" MaxLength="50"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_ConsigneeName" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%">
                    </td>
                    <td style="width: 50%" colspan="3">
                    </td>
                </tr>
                <tr id="Tr1" runat="server">
                    <td class="TD1" style="width: 20%">
                        <asp:Label ID="lbl_Reason" runat="server" CssClass="LABEL" Text="Reason :"></asp:Label></td>
                    <td style="width: 79%" colspan="4">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_Reason" runat="server" CssClass="TEXTBOX" Height="40px" Width="100%"
                                    MaxLength="250" TextMode="MultiLine"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txt_ConsigneeName" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%">
                        <asp:Label ID="lbl_MD_Reason" runat="server" Text="*" CssClass="TDMANDATORY"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:HiddenField ID="hdn_IsRegularConsignee" runat="server" />
                        <asp:HiddenField ID="hdn_ConsigneeId" runat="server" Value="0" />
                        <asp:HiddenField ID="hdn_Consignee_CSTTINNo" runat="server" />
                        <asp:HiddenField ID="hdn_ConsigneeDDAddressLine1" runat="server" />
                        <asp:HiddenField ID="hdn_ConsigneeDDAddressLine2" runat="server" />
                        <asp:HiddenField ID="hdn_IsServiceTaxApplicableForConsignee" runat="server" />
                        <asp:HiddenField ID="hdn_ConsigneeDeliveryAreaID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" /></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:Label ID="lbl_Error" runat="server" Text="Fields with * mark are mandatory"
                            CssClass="LABELERROR"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

<script type="text/javascript" language="javascript">

On_PageUnLoad();
  
function call_ConsigneeDDAddress(add1,add2)
{
    SetConsigneeDDAddress(add1,add2);
}
function Set_Consignor_Consignee_Details(Client_Id,Is_Consignor)
{
    SetClientDetails(Client_Id,Is_Consignor); 
}

</script>

