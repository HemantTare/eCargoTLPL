<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_GTLB_Loading_DateWise_Details.aspx.cs"
    Inherits="Operations_GTLB_Loading_Frm_GTLB_Loading_DateWise_Details" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript">

 
    function refreshParentPage() 
     {

        window.opener.document.getElementById('btnView').click();

    }
    

    function Check_Single(chk,gridname,callfrom)
    {

        var grid = document.getElementById(gridname);
        
        var row = chk.parentElement.parentElement;

        Chk_OnlyLoading  = row.cells[1].getElementsByTagName('input');
        
        txtTotalWt = row.cells[3].getElementsByTagName('input');
        txtOutsidePickupWt  = row.cells[6].getElementsByTagName('input');
        
        txtEmptyVehWt = row.cells[4].getElementsByTagName('input');
        txtLoadWt = row.cells[5].getElementsByTagName('input');
        
        txtLoading = row.cells[7].getElementsByTagName('input');
        txtThappiWt = row.cells[8].getElementsByTagName('input');
        
        txtThappiWeightMaster = row.cells[9].getElementsByTagName('input');
        
        Chk_OnlyLoading = row.cells[1].getElementsByTagName('input');
       
        if(callfrom == 1)
        {
            if(chk.checked == true)
            {
                txtTotalWt[0].disabled = false;
                txtOutsidePickupWt[0].disabled = false;
                
                txtLoading[0].disabled = false;
                txtThappiWt[0].disabled = false;
                
                Chk_OnlyLoading[0].disabled = false;
                
                if ( val(txtTotalWt[0].value) > 0)
                {
                    txtLoadWt[0].value = val(txtTotalWt[0].value) - val(txtEmptyVehWt[0].value);
                
                    if (val(txtOutsidePickupWt[0].value) >= 3000)
                    {
                        txtLoading[0].value = val(txtLoadWt[0].value) - val(txtOutsidePickupWt[0].value);
                    }
                    else
                    {
                        txtLoading[0].value = txtLoadWt[0].value;
                    }                
                }
                else
                {
                    txtLoadWt[0].value = '0';
                    txtLoading[0].value = '0';
                }                
                
                if (val(txtOutsidePickupWt[0].value) == 0 || val(txtOutsidePickupWt[0].value) >= 3000)
                {
                    txtThappiWt[0].value = txtThappiWeightMaster[0].value;
                }
                else
                {
                    txtThappiWt[0].value = val(txtThappiWeightMaster[0].value) - val(txtOutsidePickupWt[0].value);            
                }   
                
                if (Chk_OnlyLoading[0].checked == true)
                {
                    txtTotalWt[0].disabled = true;
                    txtOutsidePickupWt[0].disabled = true;
                }
                else
                {
                    txtTotalWt[0].disabled = false;
                    txtOutsidePickupWt[0].disabled = false;               
                }
                
            }
            else
            {

                Chk_OnlyLoading[0].disabled = true;

                txtTotalWt[0].disabled = true;
                txtOutsidePickupWt[0].disabled = true;
                
                txtLoading[0].disabled = true;
                txtThappiWt[0].disabled = true;
              
                txtLoadWt[0].value = '0';
                txtLoading[0].value = '0';
                txtThappiWt[0].value = '0';
            }
        }
        else if(callfrom == 2)
        {
        
            if ( val(txtTotalWt[0].value) > 0)
            {
                txtLoadWt[0].value = val(txtTotalWt[0].value) - val(txtEmptyVehWt[0].value);
            
                if (val(txtOutsidePickupWt[0].value) >= 3000)
                {
                    txtLoading[0].value = val(txtLoadWt[0].value) - val(txtOutsidePickupWt[0].value);
                }
                else
                {
                    txtLoading[0].value = txtLoadWt[0].value;
                }
            }
            else
            {
                txtLoadWt[0].value = '0';
                txtLoading[0].value = '0';
                
            }                
            
            
            if (val(txtOutsidePickupWt[0].value) == 0 || val(txtOutsidePickupWt[0].value) >= 3000)
            {
                txtThappiWt[0].value = txtThappiWeightMaster[0].value;
            }
            else
            {
                txtThappiWt[0].value = val(txtThappiWeightMaster[0].value) - val(txtOutsidePickupWt[0].value);            
            }    
        }
    }



    function Check_OnlyLoading(chk,gridname)
    {

        var grid = document.getElementById(gridname);
        
        var row = chk.parentElement.parentElement;

        
        txtTotalWt = row.cells[3].getElementsByTagName('input');
        txtOutsidePickupWt  = row.cells[6].getElementsByTagName('input');
        
        txtEmptyVehWt = row.cells[4].getElementsByTagName('input');
        txtLoadWt = row.cells[5].getElementsByTagName('input');
        
        txtLoading = row.cells[7].getElementsByTagName('input');
        txtThappiWt = row.cells[8].getElementsByTagName('input');
        
        txtThappiWeightMaster = row.cells[9].getElementsByTagName('input');
       
        if(chk.checked == true)
        {
            txtTotalWt[0].value= '0';
            txtOutsidePickupWt[0].value= '0';
            txtLoadWt[0].value = '0';
            txtLoading[0].value = '0';
                
            txtTotalWt[0].disabled = true;
            txtOutsidePickupWt[0].disabled = true;
            
            txtThappiWt[0].value = txtThappiWeightMaster[0].value;
                
            txtLoading[0].readOnly = false;
                
        }
        else
        {
            txtLoading[0].readOnly = true;
            
            txtTotalWt[0].disabled = false;
            txtOutsidePickupWt[0].disabled = false;
            
            txtLoading[0].value = '0';


        }
    }
    
   
    function Onblur_Loading(txtLoading,hdnLoading)
    {
        var txtLoading=document.getElementById(txtLoading);
        var hdnLoading=document.getElementById(hdnLoading);

        hdnLoading.value = txtLoading.value;
                                      
   } 
   
   
   function OpenWindowMemoList(Path)
    {

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-700);
        var popH = h-1000;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'MainPopUp_InwardDetails', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    
function Open_FORM1_Window(Path)
{ 
  window.open(Path,'GTLBFORM1','width=1000,height=800,top=50,left=50,menubar=no,resizable=yes,scrollbars=yes');
  return false;
}   



function jsCalculateTotal(gridname)
    {        

        var grid = document.getElementById(gridname);
        var i,j=0;
        var sum_Loading=0,sum_Thappi=0;
        var Chk_Attach,Chk_OnlyLoading,txt_Loading,txtThappiWt;

        var lbl_TotalLoading = document.getElementById('lbl_TotalLoading');
        var lbl_TotalThappi = document.getElementById('lbl_TotalThappi');
        
        var max = (grid.rows.length - 1);
        
        for(i=1;i<grid.rows.length;i++)
        {            
            Chk_Attach = grid.rows[i].cells[0].getElementsByTagName('input');
            Chk_OnlyLoading = grid.rows[i].cells[1].getElementsByTagName('input');
            txt_Loading = grid.rows[i].cells[7].getElementsByTagName('input');
            txtThappiWt = grid.rows[i].cells[8].getElementsByTagName('input');
            
            if(Chk_Attach[0].checked == true)
            {         
            
                if(txt_Loading[0].type =='text')
                {
                    sum_Loading = sum_Loading + val(txt_Loading[0].value);
                }
                if(txtThappiWt[0].type =='text')
                {
                    sum_Thappi = sum_Thappi + val(txtThappiWt[0].value);
                }
            }
            
        }
        
       lbl_TotalLoading.innerHTML  = sum_Loading;
       lbl_TotalThappi.innerHTML  = sum_Thappi;
    } 
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GTLB Loading DateWise Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body onunload="refreshParentPage();">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="3">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="GTLB Loading DateWise Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 10%">
                        Date :</td>
                    <td style="width: 20%">
                        <asp:Label ID="lbl_Date" runat="server" CssClass="LABELERROR" EnableViewState="False"
                            Text="01 Jul 2020"></asp:Label></td>
                    <td style="width: 70%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                    </td>
                </tr>
                <tr style="height: 500px" valign="top">
                    <td style="width: 40%" align="left" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="Div_GRID" class="DIV" style="height: 500px;">
                                    <asp:DataGrid ID="dg_Details" runat="server" AutoGenerateColumns="False" CssClass="GRID"
                                        Style="border-top-style: none" Width="50%" OnItemDataBound="dg_Details_ItemDataBound">
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Att" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                                                        OnClick="Check_Single(this,'dg_Details','1'); jsCalculateTotal('dg_Details');" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                                <HeaderStyle Width="10px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Chk_OnlyLoading" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "OnlyLoading").ToString()) %>'
                                                        OnClick="Check_OnlyLoading(this,'dg_Details'); jsCalculateTotal('dg_Details');" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                                <HeaderStyle Width="10px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Vehicle">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtn_Vehicle" Text='<%# DataBinder.Eval(Container, "DataItem.Vehicle_No") %>'
                                                        Font-Bold="True" Font-Underline="True" runat="server" CommandName="VehicleNo"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Vehicle_ID") %>' />
                                                    <asp:HiddenField ID="hdn_Vehicle_ID" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Vehicle_ID") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                                <HeaderStyle Width="100px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Total Wt.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTotalWt" Text='<%# DataBinder.Eval(Container.DataItem, "TotalWt") %>'
                                                        runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                                        Width="95%" MaxLength="8" Onblur="Check_Single(this,'dg_Details','2'); jsCalculateTotal('dg_Details');"  onfocus="this.select();"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                                <HeaderStyle Width="100px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Empty Veh Wt.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEmptyVehWt" Text='<%# DataBinder.Eval(Container.DataItem, "EmptyVehWt") %>'
                                                        runat="server" Width="95%" ReadOnly="true" BackColor="Transparent" BorderStyle="None"
                                                        BorderColor="Transparent" Style="text-align: center"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Load Wt.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtLoadWt" Text='<%# DataBinder.Eval(Container.DataItem, "LoadWt") %>'
                                                        runat="server" Width="95%" ReadOnly="true" BackColor="Transparent" BorderStyle="None"
                                                        BorderColor="Transparent" Style="text-align: center"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Outside Pickup Wt">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOutsidePickupWt" Text='<%# DataBinder.Eval(Container.DataItem, "OutSidePickupWt") %>'
                                                        runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                                        Width="95%" MaxLength="8" onfocus="this.select();" Onblur="Check_Single(this,'dg_Details','2'); jsCalculateTotal('dg_Details');"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                                <HeaderStyle Width="100px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Loading">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtLoading" Text='<%# DataBinder.Eval(Container.DataItem, "Loading") %>'
                                                        runat="server" Width="95%" MaxLength="8" ReadOnly="true" CssClass="TEXTBOXNOS" onfocus="this.select();"
                                                        onkeypress="return Only_Numbers(this,event)" Style="text-align: center"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnLoading" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Loading") %>' />
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Thappi Wt.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtThappiWt" Text='<%# DataBinder.Eval(Container.DataItem, "ThappiWt") %>'
                                                        runat="server" Width="95%" ReadOnly="true" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                                        Style="text-align: center"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="100px" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtThappiWeightMaster" Text='<%# DataBinder.Eval(Container.DataItem, "ThappiWeightMaster") %>'
                                                        runat="server" Width="0%" CssClass="HIDEGRIDCOL"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="0px" HorizontalAlign="Center" />
                                                <HeaderStyle Width="0px" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                <asp:AsyncPostBackTrigger ControlID="dg_Details" EventName="ItemCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" align="left" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td align="right" style="width: 57%">
                                           <asp:CheckBox ID="chk_Is_Complete" runat="server" CssClass="CHECKBOX" Text="Is Complete ?" />
                                        </td>
                                        <td align="right" style="width: 10%">
                                            <asp:Label ID="lbl_TotalLoading" runat="server" Text="0.00" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 6%">
                                            <asp:Label ID="lbl_TotalThappi" runat="server" Text="0.00" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="right" style="width: 27%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_Details" EventName="ItemCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text=""></asp:Label>
                                <asp:HiddenField ID="hdnKeyID" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                <asp:AsyncPostBackTrigger ControlID="dg_Details" EventName="ItemCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <%--<tr>
                    <td colspan="3" align="center">
                        <asp:CheckBox ID="chk_Is_Complete" runat="server" CssClass="CHECKBOX" Text="Is Complete ?" /></td>
                </tr>--%>
                <tr>
                    <td colspan="3" align="center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <asp:Button ID="btnSave" runat="server" CssClass="BUTTON" OnClick="btnSave_Click"
                            Text="Save" />
                        &nbsp; &nbsp;
                        <asp:Button ID="btn_FORM1" runat="server" CssClass="BUTTON" OnClick="btnFORM1_Click"
                            Text="Print FORM 1" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
