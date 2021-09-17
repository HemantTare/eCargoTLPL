<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmTNTBillingOtherChargeDetails.aspx.cs" Inherits="TrackNTrace_frmTNTBillingOtherChargeDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Billing Other Charge</title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
       <div>
       <table width="100%" class="TABLE">
            <tr>
               <td style="width:100%">
                   <asp:DataGrid ID="dg_TransportOtherCharges" runat="server" AutoGenerateColumns="False"
                        CellPadding="3" CssClass="GRID" Width="100%">
                        <AlternatingItemStyle CssClass ="GRIDALTERNATEROWCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS"/>
                        <Columns>
                            <asp:BoundColumn DataField="GC_Other_Charge_Head" HeaderText = "Other Charges Head"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Description" HeaderText = "Description"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Amount" HeaderText = "Amount"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
               </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td class="TD1" colspan="3" style="text-align: center">
                &nbsp;<asp:Button ID="btn_Exit" runat="server" CssClass="SMALLBUTTON" Text="Exit" AccessKey="E" OnClick="btn_Exit_Click" />
                </td>
            </tr>
           </table>
        </div>
    </form>
</body>
</html>
