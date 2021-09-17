<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCompanyParameters.ascx.cs"
    Inherits="Master_Location_WucCompanyParameters" %>
<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_ActivateDivision" runat="Server" Text="Is Activate Divison?" CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsActivateDivision" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_AccountTransferRequired" runat="Server" Text="Is Account Transfer Required?"
                CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsAccTransferRequired" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_ActivateCoLoaderBusiness" runat="Server" Text="Is Activate Co-Loader Business?"
                CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsActivateColoaderBusiness" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_StdBasicFreightUnit" runat="server" CssClass="LABEL" Text="Standard Basic Frieght Unit" /></td>
        <td style="width: 25%">
            <asp:DropDownList ID="ddl_StdBasicFrieghtUnit" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList></td>
        <td style="width: 25%" />
    </tr>
    <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_BookwnTruckHire" runat="Server" Text="Is Book Own Truck Hire?"
                CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsBookOwnTruckHire" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_IsMarketTruckLedgerAccTruckWise" runat="Server" Text="Is Market Truck Ledger Account Truck Wise?"
                CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsMarketTruckLedgerAccTruckWise" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_IsAttachedTruckLedgerAccTruckWise" runat="Server" Text="Is Attached Truck Ledger Account Truck Wise?"
                CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsAttachedTruckLedgerAccTruckWise" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_IsManagedTruckLedgerAccTruckWise" runat="Server" Text="Is Managed Truck Ledger Account Truck Wise?"
                CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsManagedTruckLedgerAccTruckWise" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_StdFreightRate" runat="server" Text="Standard Basic Freight Rate Per:"
                CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:TextBox ID="txt_StdFrieghtRateForSundry" MaxLength="9" CssClass="TEXTBOXNOS"
                onkeypress="return Only_Numbers(this,event)" runat="server" />
        </td>
        <td style="width: 25%">
            Kg</td>
    </tr>
    
    <tr> 
    
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_IsPartLoadingRequired" runat="Server" Text="Is Part Loading Required?" CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsPartLoadingRequired" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    
    <tr>
    
        <td class="TD1" style="width:50%;">
        <asp:Label ID="lbl_MinDiffMemo" runat="server" Text="Minutes Difference Between MEMO  And TAS :" CssClass="LABEL" /></td>
        <td style="width: 25%">
          <asp:TextBox ID="txt_MinDiffMemo" MaxLength= "50" CssClass="TEXTBOXNOS" runat="server" />
         </td>
         <td style="width:25%"></td>
           
    </tr>
    
     <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_IsGCNumberEditable" runat="Server" Text="Is GC Number Editable?"
                CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsGCNumberEditable" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_IsContractRequiredForTBBGC" runat="Server" Text="Is Contract Required For TBB GC?"
                CssClass="LABEL" /></td>
        <td style="width: 25%">
            <asp:CheckBox ID="Chk_IsContractRequiredForTBBGC" CssClass="CHECKBOX" runat="server" />
        </td>
        <td style="width: 25%" />
    </tr>
    <tr>
        <td>
            <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="False"></asp:Label>
        </td>
    </tr>
    
    <asp:HiddenField ID="hdn_CompanyId" runat="server" />
</table>
