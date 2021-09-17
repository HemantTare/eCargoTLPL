<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPODSentBy.ascx.cs" Inherits="CommonControls_WucPODSentBy" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch" TagPrefix="uc1" %>

<script src="../Javascript/Common.js" type="text/javascript"></script>

<script src="../Javascript/ddlsearch.js" type="text/javascript"></script>

 <script type="text/javascript" language="javascript">
 
         function Hide_Control()
        {
            var ddl_SentBy = document.getElementById('<%=ddl_SentBy.ClientID %>');
               
            var tr_Courier = document.getElementById('<%=tr_Courier.ClientID %>');
            var tr_Employee = document.getElementById('<%=tr_Employee.ClientID %>');
            var tr_VehicleType = document.getElementById('<%=tr_VehicleType.ClientID %>');
          
            var txt_CourierName = document.getElementById('<%=txt_CourierName.ClientID %>');
            var txt_CourierDocketNo = document.getElementById('<%=txt_CourierDocketNo.ClientID %>'); 
                 
                        
         
            if(ddl_SentBy.value == '0')
            {
            
            tr_Courier.style.display = 'none';
            tr_Employee.style.display = 'none';
            tr_VehicleType.style.display = 'none';
            txt_CourierName.value = '';
            txt_CourierDocketNo.value = '';
         
            
            }
            if(ddl_SentBy.value == '1')
            {
            
            tr_Courier.style.display = 'inline';
            tr_Employee.style.display = 'none';
            tr_VehicleType.style.display = 'none';
                    
            }
            if(ddl_SentBy.value == '2')
            {
            
            tr_Courier.style.display = 'none';
            tr_Employee.style.display = 'inline';
            tr_VehicleType.style.display = 'none';
            txt_CourierName.value = '';
            txt_CourierDocketNo.value = '';
            }
            if(ddl_SentBy.value == '3')
            {
            
            tr_Courier.style.display = 'none';
            tr_Employee.style.display = 'none';
            tr_VehicleType.style.display = 'inline';
            txt_CourierName.value = '';
            txt_CourierDocketNo.value = '';
        
            }
            
        }
 
 
         function ValidateWucPODDetails(lbl_Error)
            {
           
            var ddl_SentBy = document.getElementById('<%=ddl_SentBy.ClientID %>');
            var txt_CourierName = document.getElementById('<%=txt_CourierName.ClientID %>');
//            var txt_Employee = document.getElementById('WucPODSentBy1_ddl_Employee_txtBoxddl_Employee');
            var txt_CourierDocketNo = document.getElementById('<%=txt_CourierDocketNo.ClientID %>');
            var hdn_callfrom = document.getElementById('<%=hdn_callfrom.ClientID %>');

            var vehicleID = getvehicleid();         

                if (ddl_SentBy.value <= '0')
                {
                    if(hdn_callfrom.value == 'PODDD')
                    {
                        lbl_Error.innerText = 'Please Select Received By';
                    }
                    else
                    {
                        lbl_Error.innerText = 'Please Select Sent By';
                    }
                    ddl_SentBy.focus()
                    return false;
                }
                else if(ddl_SentBy.value == '1' && Trim(txt_CourierName.value) == '')
                {
                    lbl_Error.innerText = 'Please Enter Courier Name';
                    txt_CourierName.focus();
                    return false;
                }
                else if (ddl_SentBy.value == '1' && Trim(txt_CourierDocketNo.value) == '')
                {
                    lbl_Error.innerText = 'Please Enter Courier Docket No';
                    txt_CourierDocketNo.focus();
                    return false;
                }
//                else if (ddl_SentBy.value == '2' && txt_Employee.value == '')
//                {
//                    lbl_Error.innerText = 'Please Select Employee';
//                    txt_Employee.focus();
//                    return false;
//                }
                else if (ddl_SentBy.value == '3' && vehicleID <= 0)
                {
                    lbl_Error.innerText = 'Please Select Vehicle';
                    return false;
                }
                else
                {
                    return true;
                }
                
            }
 </script>
 
<%--<asp:Panel ID="Panel1" runat="server" GroupingText="POD Details" Width="100%">
--%><table width="100%">
    
    <tr>
        <td style="width: 100%" colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label id="lbl_SentBy" runat="server" Text="Sent By :" CssClass="LABEL" ></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:DropDownList id="ddl_SentBy" runat="server" CssClass="DROPDOWN" onchange="Hide_Control();"></asp:DropDownList>
        </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
        <td style="width: 50%" colspan="3">&nbsp;</td>
    </tr>
    <tr id="tr_Courier" runat="server">
        <td style="width: 20%" class="TD1">
            <asp:Label id="lbl_CourierName" runat="server" Text="Courier Name :" CssClass="LABEL" ></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:TextBox id="txt_CourierName" runat="server" CssClass="TEXTBOX" MaxLength="100"></asp:TextBox>
        </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
        <td style="width: 20%" class="TD1">
            <asp:Label id="lbl_CourierDocketNo" runat="server" Text="Courier Docket No :" CssClass="LABEL"></asp:Label>
        </td>
        <td style="width: 29%">
            <asp:TextBox id="txt_CourierDocketNo" runat="server" CssClass="TEXTBOX" MaxLength="20"></asp:TextBox>
        </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
    </tr>
    <tr id="tr_Employee" runat="server">
        <td style="width: 20%" class="TD1">
            <asp:Label id="lbl_EmployeeName" runat="server" Text="Employee Name :" CssClass="LABEL" ></asp:Label>
        </td>
        <td style="width: 29%" align="left">
            <cc1:DDLSearch ID="ddl_Employee" runat="server" AllowNewText="False" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchEmployeeWithHierarchyWise" CallBackAfter="2" InjectJSFunction="" PostBack="False" />
        </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
        <td style="width: 50%" colspan="3">&nbsp;</td>
    </tr>
        <tr id="tr_VehicleType" runat="server">
        <td style="width:20%" class="TD1">
            <asp:Label ID="lbl_VehicleNo" runat="server" CssClass="LABEL" Text="Vehicle No :"></asp:Label>
        </td>
        <td style="width: 29%">
            <uc1:WucVehicleSearch ID="WucVehicleSearch1" runat="server"/></td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
        <td style="width: 50%" colspan="3">&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 100%" colspan="6">
            <asp:HiddenField ID="hdn_callfrom" runat="server" />
          <asp:CheckBox ID="chk_is_ddl_sent_by_already_binded"  Visible="false" runat="server" />
        </td>
    </tr>
</table>
    <%-- </asp:Panel> --%>
     
<script type="text/javascript">
Hide_Control();
</script>