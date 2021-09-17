<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frm_Hazardous_Commodity.aspx.vb" Inherits="Operations_VT_Booking_frm_Hazardous_Commodity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Hazardous Commodity</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">

function btn_Cancel_onclick() 
{
    self.close();
}
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="TABLE">
    
    <tr>
    <td class="TDGRADIENT" colspan="2" style="width: 13%">
    &nbsp;
    <asp:Label ID="label" runat="server" CssClass="HEADINGLABEL" Text="HAZARDOUS CHEMICALS LIST"></asp:Label>
    </td>
    </tr>

    <tr>
    <td class="TDUnderline" colspan="2"></td>
    </tr>

    <tr>
    <td>&nbsp</td>
    </tr>

    <tr>
    <td style="width:10%">1</td>
    <td style="width:90%;font-weight:bold">Hydrogen Peroxide</td>
    </tr>
    
    <tr>
    <td style="width:10%">2</td>
    <td style="width:90%;font-weight:bold">Sodium</td>
    </tr>
    
    <tr>
    <td style="width:10%">3</td>
    <td style="width:90%;font-weight:bold">Sodium Hydro Sulphate</td>
    </tr>
    
    <tr>
    <td style="width:10%">4</td>
    <td style="width:90%;font-weight:bold">Laboratory Chemicals</td>
    </tr>
    
    <tr>
    <td style="width:10%">5</td>
    <td style="width:90%;font-weight:bold">Benzoyl Peroxide</td>
    </tr>
    
    <tr>
    <td style="width:10%">6</td>
    <td style="width:90%;font-weight:bold">Bromine</td>
    </tr>
    
    <tr>
    <td style="width:10%">7</td>
    <td style="width:90%;font-weight:bold">Formic Acid</td>
    </tr>
    
    <tr>
    <td style="width:10%">8</td>
    <td style="width:90%;font-weight:bold">Ferro Silicon</td>
    </tr>
    
    <tr>
    <td style="width:10%">9</td>
    <td style="width:90%;font-weight:bold">PNA</td>
    </tr>
    
    <tr>
    <td style="width:10%">10</td>
    <td style="width:90%;font-weight:bold">Sulphuric Acid</td>
    </tr>
    
    <tr>
    <td style="width:10%">11</td>
    <td style="width:90%;font-weight:bold">Carbon Black</td>
    </tr>
    
    <tr>
    <td style="width:10%">12</td>
    <td style="width:90%;font-weight:bold">Phosphoric acid</td>
    </tr>

    <tr>
    <td>&nbsp</td>
    </tr>
    
    <tr>
    <td colspan="2" style="text-align: center">
    <input id="btn_Cancel" class="BUTTON" type="button" value="Exit" accesskey="E" onclick="return btn_Cancel_onclick()" />
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
