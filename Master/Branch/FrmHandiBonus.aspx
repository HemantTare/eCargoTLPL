<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmHandiBonus.aspx.cs"
    Inherits="Master_Branch_FrmHandiBonus" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>
<script language="javascript" type="text/javascript" src="../../Javascript/Master/Branch/HandiBonus.js"></script>


<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Handi Bonus</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="HANDI BONUS"></asp:Label>
                    </td>
                </tr>
                <tr runat="server">
                    <td class="TD1" style="width: 190px">
                    </td>
                    <td style="width: 29%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 29%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 190px">
                        Branch</td>
                    <td style="width: 29%">
                        <cc1:DDLSearch ID="ddlBranch" runat="server" AllowNewText="False" IsCallBack="True"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" CallBackAfter="2"
                            PostBack="True" InjectJSFunction="" Text="" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 20%">
                        </td>
                    <td style="width: 29%">
                        </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 190px">
                        Month</td>
                    <td>
                        &nbsp;<asp:DropDownList ID="ddl_Month" runat="server" CssClass="DROPDOWN" Width="173px">
                        </asp:DropDownList></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1">
                        Year</td>
                    <td>
                        &nbsp;<asp:DropDownList ID="ddl_Year" runat="server" CssClass="DROPDOWN" Width="173px">
                        </asp:DropDownList></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 190px; height: 15px">
                        </td>
                    <td style="height: 15px">
                        </td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                        </td>
                    <td class="TD1" style="height: 15px">
                        &nbsp;</td>
                    <td style="height: 15px"></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                </tr>
                
                <tr>
                    <td style="width: 190px; height: 15px">
                        Destination</td>
                    <td style="width: 190px;height: 15px">
                        Target Destination As</td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                        </td>
                    <td class="TD1" style="height: 15px">
                        Target
                        No of LR/Day&nbsp;</td>
                    <td class="TD1" style="height: 15px">
                        Bonus Amount / Add LR</td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                </tr>
                
                <tr>
                    <td style="height: 15px">
                        <asp:DropDownList ID="ddl_Desti1" onChange="ddlDesti1Change();" runat="server" CssClass="DROPDOWN" Width="250px" >
                    </asp:DropDownList></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    <asp:DropDownList ID="ddl_Desti1As"  runat="server" CssClass="DROPDOWN" Width="150px" >
                    </asp:DropDownList></td>
                    <td class="TD1" style="width: 150px; height: 15px">
                    </td>
                    
                    <td class="TD1" style="height: 15px">
                        <asp:TextBox ID="txtLR1" runat="server" CssClass="TEXTBOXNOS" 
                            onfocus="this.select()" onkeypress="return Only_Numbers(this,event)" Text="0"
                            Width="100px"></asp:TextBox>
                            <asp:HiddenField ID="hdn_LR1" runat="server" />&nbsp;</td>
                    <td class="TD1"style="height: 15px">
                        <asp:TextBox ID="txtBonus1" runat="server" CssClass="TEXTBOXNOS" 
                            onfocus="this.select()" onkeypress="return Only_Numbers(this,event)" Text="0"
                            Width="100px"></asp:TextBox>
                            <asp:HiddenField ID="hdn_Bonus1" runat="server" /></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                </tr>

                <tr>
                    <td style="height: 15px">
                        <asp:DropDownList ID="ddl_Desti2" onChange="ddlDesti2Change();" runat="server" CssClass="DROPDOWN" Width="250px">
                    </asp:DropDownList></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    <asp:DropDownList ID="ddl_Desti2As"  runat="server" CssClass="DROPDOWN" Width="150px" >
                    </asp:DropDownList></td>
                    <td class="TD1" style="width: 150px; height: 15px">
                    </td>
                        
                    <td class="TD1" style="height: 15px">
                        <asp:TextBox ID="txtLR2" runat="server" CssClass="TEXTBOXNOS" 
                            onfocus="this.select()" onkeypress="return Only_Numbers(this,event)" Text="0"
                            Width="100px"></asp:TextBox>
                            <asp:HiddenField ID="hdn_LR2" runat="server" />&nbsp;</td>
                    <td class="TD1" style="height: 15px">
                        <asp:TextBox ID="txtBonus2" runat="server" CssClass="TEXTBOXNOS" 
                            onfocus="this.select()" onkeypress="return Only_Numbers(this,event)" Text="0"
                            Width="100px"></asp:TextBox>
                            <asp:HiddenField ID="hdn_Bonus2" runat="server" /></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                </tr>

                <tr>

                    <td style="height: 15px">
                        <asp:DropDownList ID="ddl_Desti3" runat="server" CssClass="DROPDOWN" Width="250px">
                    </asp:DropDownList></td>

                    <td class="TD1" style="width: 190px; height: 15px">
                        </td>                    
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                        </td>
                    <td class="TD1" style="height: 15px">
                        <asp:TextBox ID="txtLR3" runat="server" CssClass="TEXTBOXNOS" 
                            onfocus="this.select()" onkeypress="return Only_Numbers(this,event)" Text="0"
                            Width="100px"></asp:TextBox>
                            <asp:HiddenField ID="hdn_LR3" runat="server" />&nbsp;</td>
                    <td class="TD1" style="height: 15px">
                        <asp:TextBox ID="txtBonus3" runat="server" CssClass="TEXTBOXNOS" 
                            onfocus="this.select()" onkeypress="return Only_Numbers(this,event)" Text="0"
                            Width="100px"></asp:TextBox>
                            <asp:HiddenField ID="hdn_Bonus3" runat="server" /></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                </tr>
                                
                <tr>
                    <td style="width: 190px" class="TD1">
                        &nbsp;&nbsp;</td>
                    <td colspan="5" width="400px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClick="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6">
                        <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                                <asp:HiddenField ID="hdnKeyID" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" /> 
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
 
<script type="text/javascript">
  ddlDesti1Change();
  ddlDesti2Change();
</script>
