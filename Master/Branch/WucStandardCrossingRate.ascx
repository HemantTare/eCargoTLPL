<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucStandardCrossingRate.ascx.cs" Inherits="Master_Branch_WucStandardCrossingRate" %>
<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>

<asp:ScriptManager ID="scm_StandardCrossingRate" runat="server"></asp:ScriptManager>


<script type="text/javascript">


    function NewWindow()
    {
        
       var Path='FrmStandardCrossingRateCopy.aspx';
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-20);
        var popH = (h-250);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
          window.open(Path, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
          return false;
    }
    
    
            function calculate_rate(txt_HireRate,txt_Hamali,txt_Total)
                {
                        var txt_HireRate=document.getElementById(txt_HireRate);
                        var txt_Hamali=document.getElementById(txt_Hamali);
                        var txt_Total = document.getElementById(txt_Total);
                        valid(txt_HireRate);       
                        valid(txt_Hamali);
                        
                        txt_Total.value = val(txt_HireRate.value) + val(txt_Hamali.value)
                                      
               } 

    

</script>




<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            &nbsp;<asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="STANDARD CROSSING RATE" meta:resourcekey="lbl_HeadingResource1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_FromCity" runat="server" Text="From Branch:" CssClass="LABEL" meta:resourcekey="lbl_FromCityResource1"></asp:Label></td>
        <td style="width: 28%">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_StandardCrossingRate" />
                </Triggers>
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_Branch" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged" meta:resourcekey="ddl_BranchResource1">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ToState" runat="server" Text="To Area:" CssClass="LABEL" meta:resourcekey="lbl_ToStateResource1"></asp:Label></td>
        <td style="width: 28%">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_StandardCrossingRate" />
                </Triggers>
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_Area" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_Area_SelectedIndexChanged" meta:resourcekey="ddl_AreaResource1">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%">
            <asp:Button ID="btn_Copy" runat="server" CssClass="BUTTON" OnClick="btn_Copy_Click"
                OnClientClick="return NewWindow()" Text="Copy" meta:resourcekey="btn_CopyResource1" /></td>
    </tr>
    <tr>
        <td style="width: 20%">
        </td>
        <td style="width: 28%">
        </td>
        <td style="width: 2%">
        </td>
        <td style="width: 20%" class="TD1">
        </td>
        <td style="width: 28%">
        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_dg_StandardCrossingRate" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Area" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Branch" />
                </Triggers>
                <ContentTemplate>
                    <asp:DataGrid ID="dg_StandardCrossingRate" runat="server" AutoGenerateColumns="False"
                        CssClass="GRID" AllowPaging="True" AllowSorting="True" Width="100%" CellPadding="2"
                        PageSize="15" OnCancelCommand="dg_StandardCrossingRate_CancelCommand" OnEditCommand="dg_StandardCrossingRate_EditCommand"
                        OnItemDataBound="dg_StandardCrossingRate_ItemDataBound" OnPageIndexChanged="dg_StandardCrossingRate_PageIndexChanged"
                        OnUpdateCommand="dg_StandardCrossingRate_UpdateCommand" meta:resourcekey="dg_StandardCrossingRateResource1">
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <Columns>
                            <asp:BoundColumn DataField="From_Branch_Id">
                                <ItemStyle CssClass="HIDEGRIDCOL" />
                                <HeaderStyle CssClass="HIDEGRIDCOL" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_ToBranchId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "To_Branch_Id") %>' meta:resourcekey="lbl_ToBranchIdResource1"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="HIDEGRIDCOL" />
                                <HeaderStyle CssClass="HIDEGRIDCOL" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_FreightId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FreightId") %>' meta:resourcekey="lbl_FreightIdResource1"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lbl_FreightId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FreightId") %>' meta:resourcekey="lbl_FreightIdResource2"></asp:Label>
                                </EditItemTemplate>
                                <ItemStyle CssClass="HIDEGRIDCOL" />
                                <HeaderStyle CssClass="HIDEGRIDCOL" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="To Branch">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Branch_Name") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Distance (in Kms)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Distance" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Distance_In_Km") %>'
                                        Font-Names="Verdana" meta:resourcekey="lbl_DistanceResource1"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Distance" runat="server" onkeypress="return Only_Integers(this,event)"
                                        onblur="return valid(this)" Text='<%# DataBinder.Eval(Container.DataItem,"Distance_In_Km") %>'
                                        Font-Names="Verdana" BorderWidth="1px" CssClass="TEXTBOXNOS" MaxLength="4" meta:resourcekey="txt_DistanceResource1"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Hire Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_HireRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "HireRate") %>'
                                        Font-Names="Verdana" meta:resourcekey="lbl_HireRateResource1"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_HireRate" runat="server" onkeypress="return Only_Numbers(this,event)"
                                        onblur ="return valid(this)" Text='<%# DataBinder.Eval(Container.DataItem, "HireRate") %>'
                                        Font-Names="Verdana" BorderWidth="1px" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_HireRateResource1"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Hamali">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Hamali" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Hamali") %>'
                                        Font-Names="Verdana" meta:resourcekey="lbl_HamaliResource1"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Hamali" runat="server" onkeypress="return Only_Numbers(this,event)"
                                        onblur="return valid(this)"  Text='<%# DataBinder.Eval(Container.DataItem, "Hamali") %>'
                                        Font-Names="Verdana" BorderWidth="1px" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_HamaliResource1"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Total">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Total" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Total") %>'
                                        Font-Names="Verdana" meta:resourcekey="lbl_TotalResource1"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Total" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Total") %>'
                                        Font-Names="Verdana" BorderWidth="1px" CssClass="TEXTBOXNOS" MaxLength="9" onkeypress="return Only_Numbers(this,event)"
                                        onblur="return valid(this)" meta:resourcekey="txt_TotalResource1"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update" HeaderText="Edit" meta:resourcekey="EditCommandColumnResource1">
                            </asp:EditCommandColumn>
                            <asp:TemplateColumn HeaderText="View">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_btn_view" runat="server" meta:resourcekey="lnk_btn_viewResource1">View</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages" />
                    </asp:DataGrid>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="font-weight: bold; font-size: 11px; font-family: Verdana">
            <asp:Label ID="lbl_Updated" runat="server" BorderStyle="Solid" BorderWidth="1px"
                CssClass="UPDATEDLBL" Width="50px" meta:resourcekey="lbl_UpdatedResource1"></asp:Label>&nbsp; Updated &nbsp;
            <asp:Label ID="lbl_NotUpdated" runat="server" BorderStyle="Solid" BorderWidth="1px"
                CssClass="NOTUPDATEDLBL" Width="50px" meta:resourcekey="lbl_NotUpdatedResource1"></asp:Label>&nbsp; Not Updated
        </td>
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="up_error" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>

