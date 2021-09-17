<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_Operation_Muster_Daily_Entry.ascx.cs" Inherits="Operations_Muster_wuc_Operation_Muster_Daily_Entry" %>
<%--<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="KeySortDropDownList" Namespace="KeySortDropDownList.Thycotic.Web.UI.WebControls"   TagPrefix="cc1" %>

<%--<script type ="text/javascript" language="javascript" src =""></script>--%>
<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>
<script language="javascript" type="text/javascript" src="../../Javascript/NumCheck.js"></script>

  <script type="text/javascript">

    function Open_Popup_Window(EmployeeName,EmployeeCode,Day,OtHrs,ExtHrs,Dates,Employee_ID)
    {

        var Path='../Muster/frm_Operation_Muster_Daily_Details.aspx?EmployeeName='+EmployeeName+'&EmployeeCode='+EmployeeCode+'&Day='+Day+'&OtHrs='+OtHrs+'&ExtHrs='+ExtHrs+'&Dates='+Dates+'&Employee_ID='+Employee_ID;
        var w = screen.availWidth;;
        var h = screen.availHeight;
        var popW = (w-750);
        var popH = (h-580);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
       
        window.open(Path, 'MainPopUp5', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
        
    }
    function Uppercase(Txt_To_Be_Changed, Lbl_Error_Control)
        {
               
                   Txt_To_Be_Changed.value=Txt_To_Be_Changed.value.toUpperCase();
                if(Txt_To_Be_Changed.value>=0)
                { 
                }
                else if(Txt_To_Be_Changed.readOnly == true )
                {}
                else
                {
                    if( Txt_To_Be_Changed.value=='P' ||  Txt_To_Be_Changed.value=='W' || Txt_To_Be_Changed.value=='H' || Txt_To_Be_Changed.value =='A' || Txt_To_Be_Changed.value =='L' || Txt_To_Be_Changed.value =='T'  || Txt_To_Be_Changed.value =='F' || Txt_To_Be_Changed.value =='LWP')
                    {Txt_To_Be_Changed.select();}
                   else
                   {
                      Txt_To_Be_Changed.value='P';
                      Txt_To_Be_Changed.select();
                   }
                }
        }
    function SelectControl(Txt_To_Be_Changed)
{
  Txt_To_Be_Changed.select();
}

function disable_button()
{
 var btn_Save= document.getElementById('Wuc_Operation_Muster_Daily_Entry1_btn_Save');
 var hdn_check= document.getElementById('Wuc_Operation_Muster_Daily_Entry1_hdn_check');
 btn_Save.value = 'Save in progress...';
 btn_Save.disabled = true;
 hdn_check.value=1;
__doPostBack('Wuc_Operation_Muster_Daily_Entry1_btn_Save','');

}

 window.onload = function() { document.onkeydown = register;	document.onkeyup = register;}
                function register(e)
                {
	                if (!e) e = window.event;
	                var keyInfo = String.fromCharCode(e.keyCode) ;
                	
	                if(e.keyCode==27 || e.keyCode==13 )  
	                {return false;}	
                }


    
  </script>

<table class="TABLE" style="width: 120%;" >
       <tr>
        <td style="height: 20px" colspan="6" class="TDGRADIENT">
                <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="MUSTER ENTRY DAILY"></asp:Label>
        </td>
     </tr>
      
    <tr>
        <td colspan="6" style="width: 10%">
            &nbsp;</td>
    </tr>
   
  <%--   //ADDED ON 8-JAN-2009 By Tausif--%>
    <tr>
         <td colspan="6" style="width: 100%">
            <table width="100%">
                <tr>
                    <td colspan="2" style="font-size: 11px; color: maroon; font-family: Verdana">
                        &nbsp;                        &nbsp;
                    </td>
               </tr>
               <tr>
                   <td style="font-size: 11px; color: maroon; font-family: Verdana;  width: 20%;" >
                      &nbsp;&nbsp;&nbsp;Select Criteria :
                        <asp:DropDownList ID="ddl_Search" runat="server" Font-Names="Verdana" Font-Size="11px" AutoPostBack="True" OnSelectedIndexChanged="ddl_Search_SelectedIndexChanged" >
                            <asp:ListItem Selected="True" Value="Emp_Code">Employee Code</asp:ListItem>
                            <asp:ListItem Value="Employee_Name">First Name</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txt_Search" runat="server" CssClass="TEXTBOXSEARCH" Width="90px" BorderWidth="1px" OnTextChanged="txt_Search_TextChanged"  ></asp:TextBox>
                    
                        <asp:Button ID="btn_Search" runat="server" Text="Search" CssClass="BUTTON" width="50px" Height="19px" OnClick="btn_Search_Click" />
                        
                   </td>
                   <td style="font-size: 11px; color: maroon; font-family: Verdana; height: 26px; width: 50%;">
                     <asp:UpdatePanel ID="DDL_panal" runat="server" UpdateMode ="Conditional" >
                         <ContentTemplate >
                             Select Employee :&nbsp;<cc1:KeySortDropDownList ID="ddl_Employee_Name" runat="server" Width="250px" CssClass="DROPDOWN" Font-Names="Verdana" Font-Size="11px" AutoPostBack="True" OnSelectedIndexChanged="ddl_Employee_Name_SelectedIndexChanged" >
                               </cc1:KeySortDropDownList>                                                                                                                                                                               
                          </ContentTemplate>
                            <Triggers >
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Search"/> 
                                <asp:AsyncPostBackTrigger ControlID="txt_Search"/> 
                                 <asp:AsyncPostBackTrigger ControlID="btn_Search"/>
                               <asp:AsyncPostBackTrigger ControlID="btn_save"/>
                            </Triggers> 
                     </asp:UpdatePanel>
                   </td>
          </tr>
          <tr>
               <td colspan="2" style="font-size: 11px; color: maroon; font-family: Verdana">
                <asp:UpdatePanel ID="UpdatePanel_records" runat="server" UpdateMode ="Conditional" >
                         <ContentTemplate >
                   <asp:Label ID="lbl_records" runat="server" EnableViewState="False" Font-Bold="True" ForeColor="Red" ></asp:Label>
                     </ContentTemplate>
                            <Triggers >
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Search"/> 
                                <asp:AsyncPostBackTrigger ControlID="txt_Search"/> 
                                 <asp:AsyncPostBackTrigger ControlID="btn_Search"/>
                               <asp:AsyncPostBackTrigger ControlID="btn_save"/>
                            </Triggers> 
                     </asp:UpdatePanel>
                   </td>
          </tr>
                <tr>
                    <td colspan="2" style="font-size: 11px; color: maroon; font-family: Verdana">
                    </td>
                </tr>
        </table>       
         </td>
   </tr>
  <%--   //ADDED ON 8-JAN-2009 By Tausif END////////////////////////////--%>        
         
    
<tr>
</tr>
        <tr>
                 <td colspan="6">
                     </td>
       </tr>
<tr>
<td colspan="6" style="width:100%;">
<fieldset style="font-size: 12px; font-style: normal; font-family: Verdana; font-variant: normal">
                            <legend>MUSTER DETAILS    (P : Present A:Absent H:Half Day)</legend>
<table width="100%" class="TABLE">
<tr>
        <td>
        <asp:UpdatePanel ID="UpdatePanelNext" runat="server">
                <Triggers>
                 
                                 <asp:AsyncPostBackTrigger ControlID="ddl_Search"/> 
                                <asp:AsyncPostBackTrigger ControlID="txt_Search"/> 
                                 <asp:AsyncPostBackTrigger ControlID="btn_Search"/>
                               <asp:AsyncPostBackTrigger ControlID="btn_save"/>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Employee_Name"/>
                            </Triggers> 
                <ContentTemplate>
                
         <asp:Repeater ID="rptPages" Runat="server"  OnItemCommand="rptPages_ItemCommand" >
                      <HeaderTemplate>
                          <table  class = "TABLE" cellpadding="0" width="100%"  >
                          <tr>
                             <td style="width:15%"><b>Page :</b>&nbsp;</td>
                             <td style="width :85%;text-align:left;">
                             
                      </HeaderTemplate>
                      <ItemTemplate>
                         <asp:LinkButton ID="btnPage"
                                         CommandName="Page"
                                         CommandArgument="<%#Container.DataItem %>"
                                         CssClass="TEXTBOX"
                                         Runat="server"><%# Container.DataItem %>
                                         </asp:LinkButton>
                      </ItemTemplate>
                      <FooterTemplate>
                         </td>
                      </tr>
                      </table>
                      </FooterTemplate>
                </asp:Repeater>
             </ContentTemplate>
            </asp:UpdatePanel>
        </td>
</tr>
<tr><td style="width:100%">
<div id="Div1" runat="server" class="Div" style="height:100%">
<asp:UpdatePanel ID="UPRepeater" runat="server" >
                                         <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="rptPages" />
                                               
                                         </Triggers>
                                         <ContentTemplate>
<asp:Repeater ID ="Rpt_MusterDaily"  runat ="server" EnableViewState="true" OnItemDataBound="Rpt_MusterDaily_ItemDataBound">
        <HeaderTemplate >
              <table  border="1" width="116%" class="GRIDHEADERCSS" >
                    <tr>
                        <%--  <td style="width:0%" visible="false">
                             <table border="0">
                                  <tr><td ><asp:Label ID="lbl_Employee" CssClass="LABEL" Visible="false" runat="server" ></asp:Label></td></tr>
                              </table>
                          </td>--%>
                      <td style="width: 120px" id="TD_MainDay_Header_0" visible="true" runat="server">
                         <table border="0.5">
                              <%-- <tr><td align="center">DATE</td></tr>
                                <tr><td align="center">Day</td></tr>--%>
                              <tr><td id="TD1" runat="server" align="left" style="width:100%;">Employee Name &nbsp;&nbsp;&nbsp;</td></tr>
                           </table> 
                       </td>
                          <td style="width: 94px" id="TD4" visible="true" runat="server">
                         <table border="0">
                              <%-- <tr><td align="center">DATE</td></tr>
                                <tr><td align="center">Day</td></tr>--%>
                              <tr><td id="TD5" runat="server" align="left" style="width:100%;">Employee Code</td></tr>
                           </table> 
                       </td>    
                         <td style="width: auto; background-color:Yellow" align="center"  id="TD_MainDay_Header_Current" visible="true" runat="server">
                                   <table border="1" width="100%">
                                   <tr><td align="center" colspan="3"><asp:Label id="lbl_Current_Date" runat="server" ></asp:Label></td></tr>
                                   <tr><td align="center" colspan="3"><asp:Label ID="lbl_Current_Day" CssClass="LABEL"  runat="server" ></asp:Label></td></tr>
                                   <tr><td id="TD_Day_Current" visible="true">Day</td><td runat="server" id="TD_Ot_Hrs_Current" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_Current" visible="false">EXT Hrs</td></tr>
                                  </table>
                          </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_1" visible="true" runat="server" >
                               <table border="1">
                               <tr><td align="center" colspan="3">1</td></tr>
                               <tr><td align="center" colspan="3"><asp:Label ID="lbl_d1" CssClass="LABEL"  runat="server" ></asp:Label></td></tr>
                               <tr><td id="TD_Day_1" visible="true">Day</td>
<%--                               <td runat="server" id="TD_Ot_Hrs_1" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_1" visible="false">Ext Hrs</td>
--%>                               </tr>
                              </table>
                         </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_2" visible="true" runat="server">
                              <table border="1">
                                  <tr><td align="center" colspan="3" >2</td></tr>
                                  <tr><td align="center" colspan="3"><asp:Label ID="lbl_d2" CssClass="LABEL" runat="server" ></asp:Label></td></tr>
                                  <tr>
                                  <td id="TD_Day_2" visible="true">Day</td>
                                  <%--<td runat="server" id="TD_Ot_Hrs_2" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_2" visible="false">Ext Hrs</td>--%>
                                  </tr>
                              </table>
                         </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_3" visible="true" runat="server"> 
                                <table border="1">
                                  <tr><td align="center" colspan="3">3</td></tr>
                                   <tr><td align="center" colspan="3"><asp:Label ID="lbl_d3" CssClass="LABEL" runat="server" ></asp:Label></td></tr>
                                  <tr><td id="TD_Day_3" visible="true">Day</td>
                                  <%--<td runat="server" id="TD_Ot_Hrs_3" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_3" visible="false">Ext Hrs</td>--%>
                                  </tr>
                              </table>
                         </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_4" visible="true" runat="server"> 
                                <table border="1">
                                  <tr><td align="center" colspan="3">4</td></tr>
                                  <tr><td align="center" colspan="3"><asp:Label ID="lbl_d4" CssClass="LABEL" runat="server" ></asp:Label></td></tr>
                                  <tr><td id="TD_Day_4" visible="true">Day</td>
                                 <%-- <td runat="server" id="TD_Ot_Hrs_4" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_4" visible="false">Ext Hrs</td>--%>
                                  </tr>
                              </table>
                         </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_5" visible="true" runat="server">
                                <table border="1">
                                  <tr><td align="center" colspan="3">5</td></tr>
                                   <tr><td align="center" colspan="3"><asp:Label ID="lbl_d5" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                  <tr><td id="TD_Day_5" visible="true">Day</td>
                                 <%-- <td runat="server" id="TD_Ot_Hrs_5" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_5" visible="false">Ext Hrs</td>--%>
                                  </tr>
                                </table>
                         </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_6" visible="true" runat="server" >
                           <table border="1">
                                  <tr><td align="center" colspan="3">6</td></tr>
                                  <tr><td align="center" colspan="3"><asp:Label ID="lbl_d6" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                  <tr>
                                  <td id="TD_Day_6" visible="true">Day</td>
                                  <%--<td runat="server" id="TD_Ot_Hrs_6" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_6" visible="false">Ext Hrs</td>--%>
                                  </tr>
                              </table>
                         </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_7" visible="true" runat="server">
                                <table border="1">
                                   <tr><td align="center" colspan="3">7</td></tr>
                                   <tr><td align="center" colspan="3"><asp:Label ID="lbl_d7" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                  <tr><td id="TD_Day_7" visible="true">Day</td>
                                 <%-- <td runat="server" id="TD_Ot_Hrs_7" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_7" visible="false">Ext Hrs</td>--%>
                                  </tr>
                              </table>
                         </td>
                        <td style="width:auto;" align="center" id="TD_MainDay_Header_8" visible="true" runat="server">
                                <table border="1">
                                  <tr><td align="center" colspan="3">8</td></tr>
                                  <tr><td align="center" colspan="3"><asp:Label ID="lbl_d8" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                  <tr><td id="TD_Day_8" visible="true">Day</td><tr><%--</tr><td runat="server" id="TD_Ot_Hrs_8" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_8" visible="false">Ext Hrs</td></tr>--%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_9" visible="true" runat="server">
                                <table border="1">
                                   <tr><td align="center" colspan="3">9</td></tr>
                                   <tr><td align="center" colspan="3"><asp:Label ID="lbl_d9" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                   <tr><td id="TD_Day_9" visible="true">Day</td><tr><%--<td runat="server" id="TD_Ot_Hrs_9" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_9" visible="false">Ext Hrs</td></tr>--%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_10" visible="true" runat="server">
                                <table border="1">
                                      <tr><td align="center" colspan="3" >10</td></tr>  
                                     <tr><td align="center" colspan="3"><asp:Label ID="lbl_d10" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                     <tr><td id="TD_Day_10" visible="true">Day</td><tr>
                                    <%-- <td runat="server" id="TD_Ot_Hrs_10" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_10" visible="false">Ext Hrs</td></tr>--%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_11" visible="true" runat="server">
                                <table border="1">
                                    <tr><td align="center" colspan="3">11</td></tr>
                                  <tr><td align="center" colspan="3"><asp:Label ID="lbl_d11" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                  <tr><td id="TD_Day_11" visible="true">Day</td><tr>
                                  <%--<td runat="server" id="TD_Ot_Hrs_11" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_11" visible="false">Ext Hrs</td></tr>   --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_12" visible="true" runat="server">
                                <table border="1">
                                    <tr><td align="center" colspan="3">12</td></tr>    
                                    <tr><td align="center" colspan="3"><asp:Label ID="lbl_d12" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                    <tr><td id="TD_Day_12" visible="true">Day</td><tr>
                                    <%--<td runat="server" id="TD_Ot_Hrs_12" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_12" visible="false">Ext Hrs</td></tr>    --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_13" visible="true" runat="server">
                                <table border="1">
                                      <tr><td align="center" colspan="3">13</td></tr>
                                  <tr><td align="center" colspan="3"><asp:Label ID="lbl_d13" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                   <tr><td id="TD_Day_13" visible="true">Day</td><tr>
                                   <%--<td runat="server" id="TD_Ot_Hrs_13" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_13" visible="false">Ext Hrs</td></tr>     --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_14" visible="true" runat="server">
                                <table border="1">
                                   
                                  <tr><td align="center" colspan="3">14</td></tr>
                                   <tr><td align="center" colspan="3"><asp:Label ID="lbl_d14" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                     <tr><td id="TD_Day_14" visible="true">Day</td><tr>
                                    <%-- <td runat="server" id="TD_Ot_Hrs_14" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_14" visible="false">Ext Hrs</td></tr>    --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_15" visible="true" runat="server">
                                <table border="1">
                                  <tr><td align="center" colspan="3">15</td></tr>
                                  <tr><td align="center" colspan="3"><asp:Label ID="lbl_d15" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                 <tr><td id="TD_Day_15" visible="true">Day</td><tr>
                                 <%--<td runat="server" id="TD_Ot_Hrs_15" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_15" visible="false">Ext Hrs</td></tr>    --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_16" visible="true" runat="server">
                                <table border="1">
                                     <tr><td align="center" colspan="3">16</td></tr>
                                     <tr><td align="center" colspan="3"><asp:Label ID="lbl_d16" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                     <tr><td id="TD_Day_16" visible="true">Day</td><tr>
                                     <%--<td runat="server" id="TD_Ot_Hrs_16" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_16" visible="false">Ext Hrs</td></tr>    --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_17" visible="true" runat="server">
                                <table border="1">
                                       <tr><td align="center" colspan="3">17</td></tr>
                                      <tr><td align="center" colspan="3"><asp:Label ID="lbl_d17" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                      <tr><td id="TD_Day_17" visible="true">Day</td><tr>
                                      <%--<td runat="server" id="TD_Ot_Hrs_17" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_17" visible="false">Ext Hrs</td></tr>   --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_18" visible="true" runat="server">
                                <table border="1">
                                     <tr><td align="center" colspan="3">18</td></tr> 
                                    <tr><td align="center" colspan="3"><asp:Label ID="lbl_d18" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                    <tr><td id="TD_Day_18" visible="true">Day</td><tr>
                                    <%--<td runat="server" id="TD_Ot_Hrs_18" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_18" visible="false">Ext Hrs</td></tr>   --%>
                                </table>
                           </td>
                        <td style="width:auto;" align="center" id="TD_MainDay_Header_19" visible="true" runat="server">
                                <table border="1">
                                          <tr><td align="center" colspan="3">19</td></tr>
                                         <tr><td align="center" colspan="3"><asp:Label ID="lbl_d19" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                         <tr><td id="TD_Day_19" visible="true">Day</td><tr>
                                         <%--<td runat="server" id="TD_Ot_Hrs_19" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_19" visible="false">Ext Hrs</td></tr>   --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_20" visible="true" runat="server">
                                <table border="1">
                                  <tr><td align="center" colspan="3">20</td></tr>
                                  <tr><td align="center" colspan="3"><asp:Label ID="lbl_d20" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                  <tr><td id="TD_Day_20" visible="true">Day</td>
                                 </tr>  
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_21" visible="true" runat="server" >
                                <table border="1">
                                        <tr><td align="center" colspan="3" id="TD_Date21" runat="server">21</td></tr>
                                     <tr><td align="center" colspan="3"><asp:Label ID="lbl_d21" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                               
                                  <tr><td id="TD_Day_21" visible="true" runat="Server">Day</td><tr>
                                  <%--<td runat="server" id="TD_Ot_Hrs_21" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_21" visible="false">Ext Hrs</td></tr>   --%>
                                </table>
                           </td>
                        <td style="width:auto;" align="center" id="TD_MainDay_Header_22" visible="true" runat="server">
                                <table border="1">
                                      <tr><td align="center" colspan="3">22</td></tr>
                                     <tr><td align="center" colspan="3"><asp:Label ID="lbl_d22" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                     <tr><td id="TD_Day_22" visible="true">Day</td><tr>
                                     <%--<td runat="server" id="TD_Ot_Hrs_22" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_22" visible="false">Ext Hrs</td></tr>  --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_23" visible="true" runat="server">
                                <table border="1">
                                     <tr><td align="center" colspan="3">23</td></tr>
                                     <tr><td align="center" colspan="3"><asp:Label ID="lbl_d23" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                     <tr><td id="TD_Day_23" visible="true">Day</td><tr>
                                     <%--<td runat="server" id="TD_Ot_Hrs_23" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_23" visible="false">Ext Hrs</td></tr>--%>
                                </table>
                           </td>
                        <td style="width:auto;" align="center" id="TD_MainDay_Header_24" visible="true" runat="server">
                                <table border="1">
                                    <tr><td align="center" colspan="3">24</td></tr>
                                     <tr><td align="center" colspan="3"><asp:Label ID="lbl_d24" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                      <tr><td id="TD_Day_24" visible="true">Day</td><tr>
                                      <%--<td runat="server" id="TD_Ot_Hrs_24" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_24" visible="false">Ext Hrs</td></tr>  --%>
                                </table>
                           </td>
                        <td style="width:auto;" align="center" id="TD_MainDay_Header_25" visible="true" runat="server">
                                <table border="1">
                                    <tr><td align="center" colspan="3">25</td></tr>
                                     <tr><td align="center" colspan="3"><asp:Label ID="lbl_d25" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                      <tr><td id="TD_Day_25" visible="true">Day</td><tr>
                                      <%--<td runat="server" id="TD_Ot_Hrs_25" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_25" visible="false">Ext Hrs</td></tr>  --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_26" visible="true" runat="server">
                                <table border="1">
                                     <tr><td align="center" colspan="3">26</td></tr>
                                     <tr><td align="center" colspan="3"><asp:Label ID="lbl_d26" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                     <tr><td id="TD_Day_26" visible="true">Day</td><tr>
                                     <%--<td runat="server" id="TD_Ot_Hrs_26" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_26" visible="false">Ext Hrs</td></tr>  --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_27" visible="true" runat="server">
                                <table border="1">
                                    <tr><td align="center" colspan="3">27</td></tr>
                                     <tr><td align="center" colspan="3"><asp:Label ID="lbl_d27" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                       <tr><td id="TD_Day_27" visible="true">Day</td><tr>
                                      <%-- <td runat="server" id="TD_Ot_Hrs_27" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_27" visible="false">Ext Hrs</td></tr>  --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_28" visible="true" runat="server">
                                <table border="1">
                                    <tr><td align="center" colspan="3">28</td></tr>
                                     <tr><td align="center" colspan="3"><asp:Label ID="lbl_d28" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                     <tr><td id="TD_Day_28" visible="true">Day</td><tr>
                                    <%-- <td runat="server" id="TD_Ot_Hrs_28" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_28" visible="false">Ext Hrs</td></tr>  --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_29" visible="true" runat="server">
                                <table border="1">
                                        <tr><td align="center" colspan="3" id="TD_29" visible="true" runat="server" >29</td></tr>
                                       <tr><td align="center" colspan="3" id="TD_lblDay_29" visible="true" runat="server"><asp:Label ID="lbl_d29" CssClass="LABEL"  runat="server" ></asp:Label></td></tr>
                                        <tr><td id="TD_Day_29" visible="true" runat="server" >Day</td><tr>
                                        <%--<td runat="server" id="TD_Ot_Hrs_29" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_29" visible="false">Ext Hrs</td></tr>  --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_30" visible="true" runat="server">
                                <table border="1">
                                         <tr><td align="center" colspan="3" id="TD_30" visible="true" runat="server" >30</td></tr>
                                         <tr><td align="center" colspan="3" id="TD_lblDay_30" visible="true" runat="server"><asp:Label ID="lbl_d30" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                         <tr><td id="TD_Day_30" visible="true" runat="server" >Day</td><tr><tr>
                                         <%--<td runat="server" id="TD_Ot_Hrs_30" visible="false">OT Hrs</td><td  runat="server" id="TD_Ext_Hrs_30" visible="false">Ext Hrs</td></tr>  --%>
                                </table>
                           </td>
                        <td style="width: auto;" align="center" id="TD_MainDay_Header_31" visible="true" runat="server">
                                <table border="1">
                                        <tr><td align="center" colspan="3" id="TD_31" visible="true" runat="server" >31</td></tr>
                                        <tr><td align="center" colspan="3" id="TD_lblDay_31" visible="true" runat="server"><asp:Label ID="lbl_d31" CssClass="LABEL"   runat="server" ></asp:Label></td></tr>
                                        <tr><td id="TD_Day_31" runat="server"  visible="true">Day</td><tr>
                                        <%--<td runat="server" id="TD_Ot_Hrs_31" visible="false">OT Hrs</td><td runat="server" id="TD_Ext_Hrs_31" visible="false">Ext Hrs</td></tr>  --%>
                                </table>
                           </td>
                           </tr>
                      </table>
                    
         </HeaderTemplate>
 
      <ItemTemplate >
                <table   width="116%" border="0">
                    <tr id="Tr_ItemRepeater" runat="server">
                        <td style="width:0%" visible="false">
                             <table border="0">
                                  <tr><td ><asp:Label ID="lbl_Employee_ID" CssClass="LABEL" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Employee_ID")%>'></asp:Label></td></tr>
                              </table>
                          </td> 
           
                        <td style="width:121px;"  id="TD_MainDay_Name" visible="true" runat="server">
                                <table border="1" width="100%">
                                      <tr><td id="TD_EmployeeName" style="text-align:left;width:100%" runat="server" ><asp:Label ID="lbl_Employee_Name" CssClass="LABEL" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Employee_Name")%>'></asp:Label></td></tr>
                                </table>
                        </td>
                        <td style="width:94px;"  id="TD6" visible="true" runat="server">
                                <table border="1" width="100%">
                                      <tr><td id="TD7" style="text-align:left;width:100%" runat="server" ><asp:Label ID="Label1" CssClass="LABEL" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Emp_Code")%>'></asp:Label></td></tr>
                                </table>
                        </td>
                         <td style="width:auto;"  align="center"  id="TD_MainDay_Current" visible="true" runat="server">
                            <table width="100%" border="1">
                                  <tr>
                                      <td style="background-color:Yellow">
                                        <asp:TextBox ID="txt_currentDay" runat="server" MaxLength="3" CssClass="TEXTBOX" Width="90%" style="text-align:left;" EnableViewState="true"
                                           BorderWidth="1Px"   onfocus="SelectControl(this)" onblur="Uppercase(this)" ></asp:TextBox>
                                           <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender32" runat="server"   ValidChars = "p,a,w,h,P,A,W,H,LWP,lwp" TargetControlID="txt_currentDay"></ajaxToolkit:FilteredTextBoxExtender>
                                      </td>
                                     <td runat="server" id="TD2" visible="false" style="background-color:Yellow">
                                        <asp:TextBox ID="txt_CurrentOtHrs" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="90%" style="text-align:center;" EnableViewState="true"
                                         BorderWidth="1Px"  onkeypress="return Only_Numbers(this,event)"></asp:TextBox>
                                         
                                      </td>
                                      <td runat="server" id="TD3" visible="false" style="background-color:Yellow">
                                        <asp:TextBox ID="txt_Current_Ext_Hrs" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="90%" style="text-align:center;" EnableViewState="true"
                                         BorderWidth="1Px" onkeypress="return Only_Numbers(this,event)"></asp:TextBox>
                                      </td>
                                   </tr>  
                              </table>
                           
                        </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_1" visible="true" runat="server">
                            <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_1" visible="true">
                                        <asp:TextBox ID="txt_d1" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;" 
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d1")%>' ReadOnly="true" onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                       </td>
                                   </tr>  
                              </table>
                           
                        </td>
                        <td  style="width:auto;"  align="right" id="TD_MainDay_2" visible="true" runat="server" >
                                <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_2" visible="true">
                                        <asp:TextBox ID="txt_d2" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d2")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                         <%-- <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"   ValidChars = "p,a,w,h,F,f,P,A,W,H,0,1,2,3,4,5,6,7,8,9" TargetControlID="txt_d2"></ajaxToolkit:FilteredTextBoxExtender>--%>
                                      </td>
                                       </tr>  
                                </table> 
                                 
                        </td >
                        <td style="width:auto;" align="right" id="TD_MainDay_3" visible="true" runat="server" > 
                                <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_3" visible="true">
                                        <asp:TextBox ID="txt_d3" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d3")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                          
                                      </td>
                                 
                                   </tr>  
                              </table>
                        </td >
                        <td style="width:auto;" align="right"  id="TD_MainDay_4" visible="true" runat="server" >
                               <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_4" visible="true">
                                        <asp:TextBox ID="txt_d4" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d4")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                         
                                      </td>
                               
                                   </tr>  
                              </table>
                        </td>
                         <td style="width:auto;" align="right" id="TD_MainDay_5" visible="true" runat="server" >
                                 <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_5" visible="true">
                                        <asp:TextBox ID="txt_d5" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d5")%>' ReadOnly="true"   onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                                  
                                   </tr>  
                              </table>                             
                        </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_6" visible="true" runat="server">
                                    <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_6" visible="true">
                                        <asp:TextBox ID="txt_d6" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d6")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                                 
                                   </tr>  
                              </table>       
                                                                     
                        </td>
                        <td style="width:auto"  align="right" id="TD_MainDay_7" visible="true" runat="server">
                                   <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_7" visible="true">
                                        <asp:TextBox ID="txt_d7" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d7")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                                  
                                   </tr>  
                              </table>                             
                        </td>
                        <td  style="width:auto;"  align="right" id="TD_MainDay_8" visible="true" runat="server">
                                  <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_8" visible="true">
                                        <asp:TextBox ID="txt_d8" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d8")%>' ReadOnly="true"   onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                                   
                                   </tr>  
                              </table>                           
                        </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_9" visible="true" runat="server">
                                   <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_9" visible="true">
                                        <asp:TextBox ID="txt_d9" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d9")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                                
                                   </tr>  
                              </table>                             
                        </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_10" visible="true" runat="server">
                                   <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_10" visible="true">
                                        <asp:TextBox ID="txt_d10" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d10")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                          
                                      </td>
                             
                                   </tr>  
                              </table>                                
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_11" visible="true" runat="server">
                                   <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_11" visible="true">
                                        <asp:TextBox ID="txt_d11" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d11")%>' ReadOnly="true"   onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                                
                                   </tr>  
                              </table>        
                                                                     
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_12" visible="true" runat="server" >
                                     <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_12" visible="true">
                                        <asp:TextBox ID="txt_d12" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d12")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                                    
                                   </tr>  
                              </table>                              
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_13" visible="true" runat="server">
                                   <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_13" visible="true">
                                        <asp:TextBox ID="txt_d13" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d13")%>' ReadOnly="true"   onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                                
                                   </tr>  
                              </table>                                
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_14" visible="true" runat="server" > 
                        <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_14" visible="true">
                                        <asp:TextBox ID="txt_d14" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d14")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server"   ValidChars = "p,a,w,h,F,f,P,A,W,H,0,1,2,3,4,5,6,7,8,9" TargetControlID="txt_d14"></ajaxToolkit:FilteredTextBoxExtender>
                                      </td>
                                 
                                   </tr>  
                              </table>                                 
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_15" visible="true" runat="server">
                         <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_15" visible="true">
                                        <asp:TextBox ID="txt_d15" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d15")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                                
                                   </tr>  
                              </table>                                 
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_16" visible="true" runat="server">
                                   <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_16" visible="true">
                                        <asp:TextBox ID="txt_d16" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d16")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                                    
                                   </tr>  
                              </table>                               
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_17" visible="true" runat="server">
                                 <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_17" visible="true">
                                        <asp:TextBox ID="txt_d17" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d17")%>' ReadOnly="true"   onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                                   
                                   </tr>  
                              </table>                               
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_18" visible="true" runat="server"> 
                        <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_18" visible="true">
                                        <asp:TextBox ID="txt_d18" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d18")%>' ReadOnly="true"   onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                               
                                   </tr>  
                              </table>                              
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_19" visible="true" runat="server">
                                 <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_19" visible="true">
                                        <asp:TextBox ID="txt_d19" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%"  style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d19")%>' ReadOnly="true"   onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                               
                                   </tr>  
                              </table>                                
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_20" visible="true" runat="server"> 
                        <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_20" visible="true">
                                        <asp:TextBox ID="txt_d20" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d20")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                                    
                                   </tr>  
                              </table>                                
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_21" visible="true" runat="server"  > 
                        <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_21" visible="true">
                                        <asp:TextBox ID="txt_d21" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d21")%>' ReadOnly="true"   onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                          
                                      </td>
                                
                                   </tr>  
                              </table>                               
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_22" visible="true" runat="server">
                                  <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_22" visible="true">
                                        <asp:TextBox ID="txt_d22" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;" 
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d22")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                          
                                      </td>
                                 
                                   </tr>  
                              </table>                                
                         </td>
                       <td style="width:auto;"  align="right" id="TD_MainDay_23" visible="true" runat="server">
                         <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_23" visible="true">
                                        <asp:TextBox ID="txt_d23" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d23")%>' ReadOnly="true"   onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                          
                                      </td>
                                  
                                   </tr>  
                              </table>                               
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_24" visible="true" runat="server">
                                    <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_24" visible="true">
                                        <asp:TextBox ID="txt_d24" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d24")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                          
                                      </td>
                                  </tr>  
                              </table>                              
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_25" visible="true" runat="server">
                                    <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_25" visible="true">
                                        <asp:TextBox ID="txt_d25" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d25")%>' ReadOnly="true"   onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                                   </tr>  
                              </table>                               
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_26" visible="true" runat="server">
                                  <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_26" visible="true">
                                        <asp:TextBox ID="txt_d26" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d26")%>' ReadOnly="true"   onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                          
                                      </td>
                                   </tr>  
                              </table>                            
                         </td>
                        <td  style="width:auto;"  align="right" id="TD_MainDay_27" visible="true" runat="server">
                                 <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_27" visible="true">
                                        <asp:TextBox ID="txt_d27" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d27")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                          
                                      </td>
                                  </tr>  
                              </table>       
                                                
                                                                     
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_28" visible="true" runat="server">
                                   <table width="100%" border="1">
                                  <tr>
                                      <td runat="server" id="TD_Item_Day_28" visible="true">
                                        <asp:TextBox ID="txt_d28" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d28")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                           
                                      </td>
                                   </tr>  
                              </table>                             
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_29" visible="true" runat="server">
                                    <table width="100%" border="1">
                                  <tr>
                                      <td id="TD_Item_Day_29" visible="true" runat="server" >
                                        <asp:TextBox ID="txt_d29" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d29")%>' ReadOnly="true" onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                       </td>
                                   </tr>  
                              </table>                              
                         </td>
                        <td  style="width:auto;"  align="right" id="TD_MainDay_30" visible="true" runat="server">
                                    <table width="100%" border="1">
                                  <tr>
                                      <td id="TD_Item_Day_30" visible="true" runat="server" >
                                        <asp:TextBox ID="txt_d30" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d30")%>' ReadOnly="true"  onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                          
                                      </td>
                                   </tr>  
                              </table>                             
                         </td>
                        <td style="width:auto;"  align="right" id="TD_MainDay_31" visible="true" runat="server">
                                 <table width="100%" border="1">
                                  <tr>
                                      <td id="TD_Item_Day_31" visible="true" runat="server" >
                                        <asp:TextBox ID="txt_d31" runat="server" MaxLength="2" CssClass="TEXTBOX" Width="85%" style="text-align:center;"
                                           BorderWidth="1Px" Text=' <%#DataBinder.Eval(Container.DataItem, "d31")%>' ReadOnly="true" onfocus="SelectControl(this)" onkeyup="Uppercase(this)"></asp:TextBox>
                                          
                                      </td>
                                  </tr>  
                               </table>                                
                         </td>
                         
                         
                    </tr>
               </table>
        </ItemTemplate>
 </asp:Repeater>
   </ContentTemplate>
   </asp:UpdatePanel>
   </div>

</td>
</tr>
</table>
</fieldset>
 </td>
</tr>
 
  <tr>
       <td style="width: 100%" align="center" colspan="6">
         <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
          <Triggers>
          <asp:AsyncPostBackTrigger ControlID="btn_Save" />
          </Triggers>
          <ContentTemplate>
        <asp:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Save" OnClientClick="disable_button();"  CssClass="BUTTON" />
         </ContentTemplate>
   </asp:UpdatePanel>
        </td>
   </tr>
   <tr>
         <td style="width: 100%"  colspan="6">
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
          <Triggers>
          <asp:AsyncPostBackTrigger ControlID="btn_Save" />
          </Triggers>
          <ContentTemplate>
         <asp:Label ID="lbl_error" runat="server" ForeColor="Red" Font-Bold="True" ></asp:Label>
          </ContentTemplate>
   </asp:UpdatePanel>
    <asp:UpdatePanel ID="Update_hdn_check" runat="server" >
          <Triggers>
          <asp:AsyncPostBackTrigger ControlID="btn_Save" />
          </Triggers>
          <ContentTemplate>
             <asp:HiddenField ID="hdn_check" runat="server" />
                  </ContentTemplate>
   </asp:UpdatePanel>
         
         </td>
   </tr>
                   
</table> 


<asp:UpdateProgress ID="UpdateProgress1" runat="server">
<ProgressTemplate>
 <div style="position: absolute; bottom: 20%; left: 50%; font-size: 11px; font-family: Verdana; z-index:100">
  <span id="ajaxloading">
	<table>
	  <tr>
	    <td><asp:image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
	  </tr>
	  <tr>
	    <td align="center" >Wait!...</td>
	  </tr>
	</table>
            </span>
             </div>
             
   </ProgressTemplate>
   </asp:UpdateProgress>          




 

