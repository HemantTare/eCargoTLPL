<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVehicleSearch.ascx.cs"
  Inherits="CommonControls_WucVehicleSearch" %>

<script type="text/javascript" src="../JavaScript/Common.js"></script>

<script type="text/javascript">

function isValidTruckNumber(txt_vehicle_digit)
{
var txt_Vehicle_Last_4_Digits = document.getElementById(txt_vehicle_digit);

var Truck_Last_4_digits = parseFloat(txt_Vehicle_Last_4_Digits.value);
if (isNaN(Truck_Last_4_digits)) Truck_Last_4_digits = 0;

if (txt_Vehicle_Last_4_Digits.value.length < 1)
  {
  alert ("Please Enter Vehicle No.");
  txt_Vehicle_Last_4_Digits.focus();
  return false;
  }

return true;
}

function getvehicleid()
{
var ddl_Vehicle = document.getElementById('<%=ddl_Vehicle.ClientID %>');

if (ddl_Vehicle.options.length > 0)
  return ddl_Vehicle.value
else
  return 0
}
</script>

<table border="0" style="width: 100%">
  <tr>
    <td style="width: 25%;">
      <asp:TextBox ID="txt_Vehicle_Last_4_Digits" ToolTip="Enter Vehicle's Last Digits"
        onkeypress="return Only_Numbers(this,event)" runat="server" BorderWidth="1px" CssClass="TEXTBOXSEARCH"
        MaxLength="4" /></td>
    <td style="width: 25%;">
      <asp:ImageButton ID="btn_Vehicle_Search" runat="server" ImageAlign="TextTop" ImageUrl="~/Images/Search.GIF"
        OnClick="btn_Search_Click" /></td>
    <td align="left" style="width: 50%;" colspan="2">
      <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
          <asp:DropDownList ID="ddl_Vehicle" AutoPostBack="true" Width="98%" runat="server"
            Font-Size="11px" OnSelectedIndexChanged="ddl_Vehicle_SelectedIndexChanged" />
          <asp:HiddenField ID="hdn_vehicle_category_ids" Value="" runat="server" />
          <asp:HiddenField ID="hdn_vehicle_vendor_id" Value="" runat="server" />
          <asp:HiddenField ID="hdnMenuItemID" Value="" runat="server" />
          <asp:HiddenField ID="hdnTransactionDate" Value="" runat="server" />
          <asp:HiddenField ID="hdn_LoginBranch_Id" runat="server" />
          <asp:HiddenField ID="hdn_Ledger_ID" Value="" runat="server" />
          <asp:HiddenField ID="hdn_Credit_Limit" Value="" runat="server" />
        </ContentTemplate>
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="btn_Vehicle_Search" />
        </Triggers>
      </asp:UpdatePanel>
    </td>
  </tr>
  <tr>
    <td colspan="4" style="width: 100%;">
      <table>
        <tr>
          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <td style="width: 50%" id="td_lbtn_AddVehicle" runat="server" visible="false">
                <asp:LinkButton ID="lbtn_AddVehicle" Font-Bold="true" OnClientClick="return Add_Vehicle_Window()"
                  runat="server" Text="Add New Vehicle"></asp:LinkButton>
                &nbsp;
                <asp:HiddenField ID="hdn_vehicle_path" runat="server" />
              </td>
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="btn_Vehicle_Search" />
              <asp:AsyncPostBackTrigger ControlID="ddl_Vehicle" />
            </Triggers>
          </asp:UpdatePanel>
          
          <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
              <td style="width: 50%" class="TD1" id="td_lbtn_viewVehicle" colspan="2" runat="server"
                visible="false">
                <asp:LinkButton ID="lbtn_VehicleView" OnClientClick="return Open_View_Window()" Font-Bold="true"
                  runat="server" Text="View Details"></asp:LinkButton>
                <asp:HiddenField ID="hdn_encrypted_vehicle_id" runat="server" />
              </td>
            </ContentTemplate>
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="btn_Vehicle_Search" />
              <asp:AsyncPostBackTrigger ControlID="ddl_Vehicle" />
            </Triggers>
          </asp:UpdatePanel>
        </tr>
      </table>
    </td>
  </tr>
</table>

<script type="text/javascript">
function Open_View_Window()
{
  var hdn_encrypted_vehicle_id = document.getElementById('<%=hdn_encrypted_vehicle_id.ClientID %>');

  var w = screen.availWidth;
  var h = screen.availHeight;
  var popW = (w-100);
  var popH = (h-100);
  var leftPos = (w-popW)/2;
  var topPos = (h-popH)/2;
  
  if(hdn_encrypted_vehicle_id.value == '')
    {
    alert('Please Select Vehicle');
    }
  else
    {
    window.open(hdn_encrypted_vehicle_id.value, 'memo', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
    return false;
    }

  return false;
}

//    for add new vehicle

function Add_Vehicle_Window()
{
var hdn_vehicle_path = document.getElementById('<%=hdn_vehicle_path.ClientID %>');

var w = screen.availWidth;
var h = screen.availHeight;
var popW = (w-100);
var popH = (h-100);
var leftPos = (w-popW)/2;
var topPos = (h-popH)/2;

if(hdn_vehicle_path.value == '')
  {
  alert('you don"t have rights to add vehicle');
  }
else
  {
  window.open(hdn_vehicle_path.value, 'memo', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
  return false;
  }

return false;
}
</script>

