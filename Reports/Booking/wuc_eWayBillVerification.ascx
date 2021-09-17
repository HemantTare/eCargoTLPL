<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_eWayBillVerification.ascx.cs"
    Inherits="Reports_wuc_eWayBillVerification" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript">


//**********************************************************************8

function CheckAllDataGridCheckBoxes(chk) 
{
   
    for(i = 0; i < document.forms[0].elements.length; i++) 
    {
        elm = document.forms[0].elements[i];

        if (elm.name != undefined)
        {
            if (elm.type == 'checkbox')
             {
                var elm_name = elm.name;
                var arr = elm_name.split("$");
                
                if (arr[3] == "Chk_Verified" ) elm.checked = chk.checked;
            }
        }
    }
} 

function viewwindow_general(GC_ID)
{
 
//        var Path='../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=GC' +'&Doc_No=' + GC_ID;
        var Path='../../Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=MwAwAA==&Mode=NAA=&Id=' + GC_ID;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUp23GCCosting', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_Duplicate_eWayBill(eWayBillNo)
{
 
        var Path='../../Reports/Booking/FrmDuplicate_eWayBills.aspx?eWayBillNo=' + eWayBillNo;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 1000;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'Duplicate_eWayBill', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_UpdateMultipleeWayBills(GC_ID,GC_No,eWayBillNo)
{
 
//        var Path='../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type=GC' +'&Doc_No=' + GC_ID;
        var Path='../../Operations/Booking/FrmMultiple_eWayBillUpdate.aspx?GC_Id=' + GC_ID + '&GC_No=' + GC_No + '&eWayBillNo=' + eWayBillNo;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUp23GCCosting', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}


function viewwindow_PendingReason(GC_Id,GC_No,eWayBillNo,Reason_Id)
{   
       
       var Path='../../Reports/Booking/Frm_eWayBillVerification_UnverifiedReason.aspx?GC_Id=' + GC_Id + '&GC_No=' + GC_No + '&eWayBillNo=' + eWayBillNo +  '&Reason_Id=' + Reason_Id;

        var popW = 700;
        var popH = 300;
        var leftPos = (popW)/2;
        var topPos = (popH)/2;
 
        window.open(Path, 'PopUpPedingReason', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_PartBUpdate(GC_Id,GC_No,eWayBillNo,ValidUpToDate,chkverified)
{   
    
    var ChkVerified1 = document.getElementById(chkverified);
   
    var ValidUpToDate = document.getElementById(ValidUpToDate);
    
    
    if(ChkVerified1.checked)
    {
       
       var Path='../../Reports/Booking/Frm_eWayBillVerification_PartBUpdate.aspx?GC_Id=' + GC_Id + '&GC_No=' + GC_No + '&eWayBillNo=' + eWayBillNo +  '&ValidUpToDate=' + ValidUpToDate.value;

        var popW = 700;
        var popH = 300;
        var leftPos = (popW)/2;
        var topPos = (popH)/2;
 
        window.open(Path, 'PopUpPedingPartB', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        
    }
    return false;
}

function viewwindow_eWayBillUpdate(GC_Id,GC_No,eWayBillNo)
{   
       
       var Path='../../Reports/Booking/Frm_eWayBillVerification_Update_eWayBill_In_GC.aspx?GC_Id=' + GC_Id + '&GC_No=' + GC_No + '&eWayBillNo=' + eWayBillNo;

        var popW = 700;
        var popH = 300;
        var leftPos = (popW)/2;
        var topPos = (popH)/2;
 
        window.open(Path, 'PopUpUpdate_eWayBill_In_GC', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function copyToClipboard(string) 
{

    var isIE = /*@cc_on!@*/false || !!document.documentMode;

    if (isIE == true)
    {
        clipboardData.setData("Text", string);
    }
    else
    {
        function handler (event)
        {
            event.clipboardData.setData('text/plain', string);
            event.preventDefault();
            document.removeEventListener('copy', handler, true);
        }

        document.addEventListener('copy', handler, true);
        document.execCommand('copy');
    }
    
    return false;
}


function checkuncheckIsVerify(ChkVerified,txtDistance)
{
    var ChkVerified1 = document.getElementById(ChkVerified);    
    var txtDistance1 = document.getElementById(txtDistance);
    
    if(ChkVerified1.checked)
    {
        txtDistance1.disabled = false;
    }
    else
    {
        txtDistance1.value = "0"
        txtDistance1.disabled = true;
    }
}


   
</script>

<style type="text/css">
  .DataGridFixedHeader { POSITION: relative; ; TOP: expression(this.offsetParent.scrollTop); BACKGROUND-COLOR: white }
</style>
<asp:ScriptManager ID="ScriptManager" runat="server" />
<table class="TABLE" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr class="HIDEGRIDCOL">
        <td style="width: 50%; height: 22px;">
            <asp:DropDownList ID="ddl_Serach" runat="server" Font-Names="Verdana" Font-Size="11px">
            </asp:DropDownList>
            <asp:TextBox ID="txt_Search" Width="200px" runat="server" CssClass="TEXTBOXSEARCH"
                OnTextChanged="txt_Search_TextChanged" BorderWidth="1px"></asp:TextBox>
            <asp:Button ID="btn_Search" runat="server" Text="Search" CssClass="BUTTON" Width="50px"
                Height="19px" />
        </td>
        <td style="width: 50%; text-align: right; height: 22px;">
        </td>
    </tr>
    <tr>
        <td style="width: 50%">
            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
            <br />
        </td>
    </tr>
    <tr>
        <td style="width: 60%">
            <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
        </td>
        <td style="width: 40%">
            <asp:Button ID="btn_view" OnClick="btn_view_Click" runat="server" Text="View" CssClass="BUTTON">
            </asp:Button>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="vertical-align: top; text-align: left;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div id="Div_PDS" class="Div" style="height: auto">
                        <asp:DataGrid ID="datagrid1" runat="server" AutoGenerateColumns="False" DataKeyField="GC_ID"
                            CellPadding="3" CssClass="GRID" AllowPaging="True" PageSize="15" Style="width: 98%"
                            OnItemDataBound="datagrid1_ItemDataBound" OnPageIndexChanged="datagrid1_PageIndexChanged">
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" Height="20px" Font-Size="11px" Font-Names="Verdana"
                                Font-Bold="True" HorizontalAlign="Center" ForeColor="Black" BorderStyle="Solid"
                                BorderColor="#9495A2" BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1">
                            </HeaderStyle>
                            <Columns>
                                <asp:BoundColumn DataField="GC_Id" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Branch" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "BkgBranch")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="BkgDate" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "BkgDate")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="eWayBillNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_eWayBillNo" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "eWayBillNo") %>'
                                            Height="15px" />
                                        &nbsp;
                                        <asp:ImageButton ID="btnCopy" runat="server" onmouseout="this.src='../../Images/Copy.png'"
                                            ImageUrl="~/Images/Copy.png" AlternateText="Click To Copy eWayBill No." title="Click To Copy eWayBill No." />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Duplicate?" HeaderStyle-HorizontalAlign="center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_IsDuplicate" runat="server" CssClass="LABEL" Text='No' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CreatedOn" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "GCCreatedTime")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="LR No." HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_GC_No" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "LRNo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Consignor" HeaderStyle-HorizontalAlign="Left" Visible="False">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "Consignor_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Consignor Mobile" HeaderStyle-HorizontalAlign="Left"
                                    Visible="False">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "Consignor_Mobile_No")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Consignee" HeaderStyle-HorizontalAlign="Left" Visible="False">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "Consignee_Name")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Consignee Mobile" HeaderStyle-HorizontalAlign="Left"
                                    Visible="False">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "Consignee_Mobile_No")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="IsMultiple?" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtn_MultipleEWayBill" Text='Update' Font-Bold="True" Font-Underline="True"
                                            runat="server" CommandName="Description" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_Verified" Checked='<%#DataBinder.Eval(Container.DataItem,"IseWayBillVerified")%>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Distance" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Distance" Text='<%#DataBinder.Eval(Container.DataItem,"Distance")%>'
                                            runat="server" Enabled="false" onkeypress="return Only_Numbers(this,event)" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PartB Updated" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_PartBUpdated" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "IsPartBUpdated") %>' />
                                        <asp:HiddenField ID="hdn_PartBUpdated" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IsPartBUpdated") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="IsVerifiedPrevious?" Visible="false">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_VerifiedPrevious" Checked='<%#DataBinder.Eval(Container.DataItem,"IseWayBillVerifiedPrevious")%>'
                                            runat="server" Style="text-align: center" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Unverified Reason">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Pending_Reason" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "UnVerifiedReason") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Verified By" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "VerifiedBy")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Verified On" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "eWayBillVerifiedOn")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NumericPages" HorizontalAlign="Left" PageButtonCount="30" />
                        </asp:DataGrid>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="Btn_Save" />--%>
                    <%--<asp:AsyncPostBackTrigger ControlID="datagrid1" EventName="ItemCommand" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="font-size: xx-small; width: 100%; text-align: left">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" style="width: 100%; height: 15px; text-align: left; color: red;">
            <asp:Label ID="Label1" runat="server" Style="font-weight: bold" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2" style="font-size: xx-small; width: 100%; height: 15px; text-align: center">
            <asp:Button ID="Btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="Btn_Save_Click" /></td>
    </tr>
    <tr>
        <td colspan="2" style="width: 100%; height: 25px; text-align: left;">
            <asp:Label ID="lbl_RemarkHead" runat="server" Style="font-weight: bold; font-size: large;"
                Text="Note : "></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2" style="width: 100%; height: 15px; text-align: left; color: Black;
            background-color: Yellow;">
            <asp:Label ID="lbl_Remarks" runat="server" Style="font-weight: bold" Text="Rows In Yellow Color Indicates That The Particular LR Is Having Multiple eWay Bills But Still Not Updated."></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2" style="width: 100%; height: 15px; text-align: left; color: White;
            background-color: #ff0066;">
            <asp:Label ID="lbl_Remarks2" runat="server" Style="font-weight: bold" Text="Rows In Pink Color Indicates That In The Particular LR Multiple eWay Bills Updated But Still Not Verified."></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2" style="width: 100%; height: 15px; text-align: left; color: White;
            background-color: #ff4500;">
            <asp:Label ID="lbl_Remark3" runat="server" Style="font-weight: bold" Text="Rows In OrangeRed Color Indicates That The Same eWay Bill Is Appeared In Another LR."></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2" style="width: 100%; height: 15px; text-align: left; color: red;">
            <asp:Label ID="lbl_errors" runat="server" Visible="false" Style="font-weight: bold;
                font-size: large"></asp:Label></td>
    </tr>
</table>

<script type="text/javascript" language="javascript">

function call_ValidUptoDate(GC_No,eWayBillNo,ValidUpToDate)
{
    SetValidUptoDate(GC_No,eWayBillNo,ValidUpToDate);
}

function SetValidUptoDate(GC_No,eWayBillNo,ValidUpToDate)
{  
    var grid = document.getElementById('<%=datagrid1.ClientID%>') 
    
    var lnk_PartBUpdated, lnk_GC_No, lnk_eWayBillNo, hdn_PartBUpdated;
    
    var i,j=0;

    var max = (grid.rows.length - 1);

    for(i=1;i<grid.rows.length;i++)
    {
        
        lnk_GC_No = grid.rows[i].cells[5].getElementsByTagName('a');
    
        lnk_eWayBillNo = grid.rows[i].cells[2].getElementsByTagName('a');

        lnk_PartBUpdated = grid.rows[i].cells[9].getElementsByTagName('a');
        
        hdn_PartBUpdated = grid.rows[i].cells[9].getElementsByTagName('input');
        
        
        if (lnk_GC_No[0].innerHTML == GC_No && lnk_eWayBillNo[0].innerHTML == eWayBillNo)
        {
            lnk_PartBUpdated[0].innerHTML = ValidUpToDate;
            hdn_PartBUpdated[0].value = ValidUpToDate;
            
        }
    }
}


</script>

