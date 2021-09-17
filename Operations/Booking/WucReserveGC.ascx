<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucReserveGC.ascx.cs"
  Inherits="Operations_Booking_WucReserveGC" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script language="javascript" type="text/javascript">

 function Allow_To_Save()
 {
    var lbl_Errors=document.getElementById('<%=lbl_Errors.ClientID%>');
    var ddl_GcType=document.getElementById('<%=ddl_GcType.ClientID%>');
    var txt_DDLConsignor=document.getElementById('<%=DDLConsignor.TextBoxClientID%>');
    var ATS = false;
    
   var objResource=new Resource('<%=hdf_ResourecString.ClientID%>');
  
   if(parseInt(ddl_GcType.value)== 0)
   {
       lbl_Errors.innerText = objResource.GetMsg("Msg_ddl_GcType");
       ddl_GcType.focus();
   }
   else if (txt_DDLConsignor.value == '')
   {
      lbl_Errors.innerText = objResource.GetMsg("Msg_ddl_Consignor");
      txt_DDLConsignor.focus();
   }
   else
   {
      ATS = true;
   }
   
   return ATS;
 }  
</script>

<table class="TABLE">
  <tr>
    <td class="TDGRADIENT" colspan="6">
      &nbsp;
      <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Reserve GC"
        meta:resourcekey="lbl_HeadingResource1"></asp:Label>
    </td>
  </tr>
  <tr>
    <td class="TDUnderline" colspan="6">
      &nbsp;</td>
  </tr>
  <%--<tr>
        <td class="TD1" style="width: 20%;">
        </td>
        <td style="width: 29%;">
        </td>
        <td style="width: 1%" class="TDMANDATORY">
        </td>
        <td class="TD1" style="width: 50%;" colspan="3">
        </td>
    </tr>--%>
  <tr>
    <td class="TD1" style="width: 20%;">
      <asp:Label ID="lbl_Branch1" runat="server" CssClass="LABEL" Text="Branch :" meta:resourcekey="lbl_Branch1Resource1"></asp:Label>
    </td>
    <td style="width: 29%;">
      <asp:Label ID="lbl_Branch" runat="server" CssClass="LABEL" Font-Bold="True" meta:resourcekey="lbl_BranchResource1"></asp:Label>
    </td>
    <td style="width: 1%" class="TDMANDATORY">
      *</td>
    <td class="TD1" style="width: 20%;">
      <asp:Label ID="lbl_Reason" runat="server" CssClass="LABEL" meta:resourcekey="lbl_ReasonResource1"
        Text="Reserved Reason :"></asp:Label></td>
    <td style="width: 29%;">
      <asp:DropDownList ID="ddl_Reason" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_ReasonResource1" />
    </td>
    <td style="width: 1%" class="TDMANDATORY">
      *</td>
  </tr>
  <tr id="tr_va" visible="false" runat="server">
    <td class="TD1" style="width: 20%">
      <asp:Label ID="lbl_VA" runat="server" Text="Select VA :"></asp:Label></td>
    <td style="width: 29%">
      <asp:DropDownList ID="ddl_VA" runat="server">
      </asp:DropDownList></td>
    <td class="TDMANDATORY" style="width: 1%">
    </td>
    <td class="TD1" colspan="3" style="width: 50%">
    </td>
  </tr>
  <tr>
    <td class="TD1" style="width: 20%;">
      <asp:Label ID="lbl_GcType" runat="server" CssClass="LABEL" Text="GC Type :" meta:resourcekey="lbl_GcTypeResource1"></asp:Label>
    </td>
    <td style="width: 29%;">
      <asp:DropDownList ID="ddl_GcType" AutoPostBack="true" runat="server" CssClass="DROPDOWN"
        meta:resourcekey="ddl_GcTypeResource1" OnSelectedIndexChanged="ddl_GcType_SelectedIndexChanged" />
    </td>
    <td style="width: 1%" class="TDMANDATORY">
      *</td>
    <td class="TD1" style="width: 20%;">
      <asp:Label ID="lbl_Consignor" runat="server" CssClass="LABEL" Text="Consignor :"
        meta:resourcekey="lbl_ConsignorResource1"></asp:Label>
    </td>
    <td style="width: 29%;">
      <cc1:DDLSearch ID="DDLConsignor" runat="server" AllowNewText="False" IsCallBack="True"
        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchAllClientBranchCity" CallBackAfter="2"
        Text="" InjectJSFunction="" PostBack="False" />
    </td>
    <td style="width: 1%" >
      </td>
  </tr>
  <tr id="tr_txtGCFromandTo" runat="server">
    <td class="TD1" style="width: 20%;">
      <asp:Label ID="lbl_GCNoFrom" runat="server" CssClass="LABEL" Text="GC No From:" meta:resourcekey="lbl_GCNoFromResource1"></asp:Label>
    </td>
    <td style="width: 29%;">
      <asp:TextBox ID="txt_GcNoFrom" runat="server" onkeypress="return Only_Integers(this,event)"
        CssClass="TEXTBOXNOS" meta:resourcekey="txt_GCNoFromResource1"></asp:TextBox>
    </td>
    <td style="width: 1%" class="TDMANDATORY">
      *</td>
    <td class="TD1" style="width: 20%;">
      <asp:Label ID="lbl_GCNoTo" runat="server" CssClass="LABEL" Text="GC No To:" meta:resourcekey="lbl_GCNoToResource1"></asp:Label>
    </td>
    <td style="width: 29%;">
      <asp:TextBox ID="txt_GCNoTo" runat="server" onkeypress="return Only_Integers(this,event)"
        CssClass="TEXTBOXNOS" meta:resourcekey="txt_GCNoToResource1"></asp:TextBox>
    </td>
    <td style="width: 1%" class="TDMANDATORY">
      *</td>
  </tr>
  <tr id="tr_ReserveGCFromandTo" runat="server">
    <td class="TD1" style="width: 20%;">
      <asp:Label ID="lbl_CurrentGC" runat="server" CssClass="LABEL" Text="Current GC:"></asp:Label>
    </td>
    <td style="width: 29%;">
      <asp:Label ID="lbl_CurrentGCDisplay" runat="server" CssClass="LABEL"></asp:Label>
    </td>
    <td style="width: 1%" class="TDMANDATORY">
    </td>
    <td class="TD1" style="width: 20%;">
      <asp:Label ID="lbl_NoOfGC" runat="server" CssClass="LABEL" Text="No Of CN:"></asp:Label>
    </td>
    <td style="width: 29%;">
      <asp:TextBox ID="txt_NoOfGC" runat="server" MaxLength="3" onkeypress="return Only_Integers(this,event)"
        CssClass="TEXTBOXNOS"></asp:TextBox>
    </td>
    <td style="width: 1%" class="TDMANDATORY">
      *</td>
  </tr>
  <tr>
    <td colspan="6">
      &nbsp;</td>
  </tr>
  <tr>
    <td class="TD1" colspan="6">
      <asp:HiddenField ID="hdf_ResourecString" runat="server" />
      <asp:HiddenField ID="hdn_Document_Allocation_Id" runat="server" />
      <asp:HiddenField ID="hdn_Start_GC_No" runat="server" />
      <asp:HiddenField ID="hdn_End_GC_No" runat="server" />
      <asp:HiddenField ID="hdn_GC_No_Length" runat="server" />
      <asp:HiddenField ID="hdn_No_For_Padd" runat="server" />
    </td>
  </tr>
  <tr>
    <td style="text-align: center" colspan="6">
      <asp:Button ID="btn_Save" runat="Server" CssClass="BUTTON" OnClientClick="return Allow_To_Save()"
        Text="Save" meta:resourcekey="btn_SaveResource1" OnClick="btn_Save_Click" />
    </td>
  </tr>
  <tr>
    <td class="TD1" style="text-align: left;" colspan="6">
      <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
    </td>
  </tr>
</table>
