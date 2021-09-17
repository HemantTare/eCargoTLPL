<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_Bank_Reco_Report.ascx.cs" Inherits="Master_Accounting_Masters_wuc_Bank_Reco_Report" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<script language="javascript" type="text/javascript">

    function hideload()
    {
        var loading = document.getElementById('loading');     
        loading.style.visibility = 'hidden'; 
    }
        
    function displayload()
    {
        var loading = document.getElementById('loading');     
        loading.style.visibility = 'visible'; 
        loading.style.position='absolute';
        loading.style.left=(document.body.clientWidth/2)-20+'px';
        loading.style.top=(document.body.clientHeight/2)-60+'px';
    }   
    
    function ChangePeriod()
    {
       var Path='../../Controls/frm_Date_Range.aspx'
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-600);
        var popH = (h-500);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    
  function SelectColoumn(chk,Dt,Cr,flag) 
    {  
      alert('as');
      for(i = 0; i < document.forms[0].elements.length; i++) 
        {
            elm = document.forms[0].elements[i];
            
            var elm_id = document.getElementById(elm.id);
            if(elm_id.id=='Wuc_Bank_Reco_Report1_fst'){continue;}
            
            var elm_name = elm.name;
            var arr = elm_name.split("$");
            
                    if(arr[3]=='chk_Select')
                {
                    if (elm.type == 'checkbox') 
                    {
                        elm.checked = chk.checked;   
                    }
                }
         }
//         SetOnSelectAll(chk);
    }
    
    
    function SetAmountNotCleared(chk_Select,Dt,Cr)
    {
   
      var lbl_Amt_Not_cleared_dr =document.getElementById('<%=lbl_Amt_Not_cleared_dr.ClientID%>');
      var lbl_Amt_Not_cleared_cr =document.getElementById('<%=lbl_Amt_Not_cleared_cr.ClientID%>');
      
      var hdf_Updated_Amt_Not_cleared_dr=document.getElementById('<%=hdf_Updated_Amt_Not_cleared_dr.ClientID%>');
      var hdf_Updated_Amt_Not_cleared_cr=document.getElementById('<%=hdf_Updated_Amt_Not_cleared_cr.ClientID%>');

        
        if(chk_Select.checked)
        {
           lbl_Amt_Not_cleared_dr.innerText=round(Math.abs(lbl_Amt_Not_cleared_dr.innerText)-Dt);
           lbl_Amt_Not_cleared_cr.innerText=round(Math.abs(lbl_Amt_Not_cleared_cr.innerText)-Cr);
        }
        else
        {
           lbl_Amt_Not_cleared_dr.innerText=round(Math.abs(lbl_Amt_Not_cleared_dr.innerText)+Dt);
           lbl_Amt_Not_cleared_cr.innerText=round(Math.abs(lbl_Amt_Not_cleared_cr.innerText)+Cr);
        }
        
      hdf_Updated_Amt_Not_cleared_dr.value=lbl_Amt_Not_cleared_dr.innerText;
      hdf_Updated_Amt_Not_cleared_cr.value=lbl_Amt_Not_cleared_cr.innerText;
       
       SetBlanceAsPerBank(chk_Select,Dt,Cr);
       
     
     }
    
    
    
   function SetBlanceAsPerBank(chk_Select,Dt,Cr)
   {
   
        var lbl_Bal_As_Bank_dr =document.getElementById('<%=lbl_Bal_As_Bank_dr.ClientID%>');
        var lbl_Bal_As_Bank_cr =document.getElementById('<%=lbl_Bal_As_Bank_cr.ClientID%>');
        var hdf_Initial_BalAsBank_dr=document.getElementById('<%=hdf_Initial_BalAsBank_dr.ClientID%>');
        var hdf_Initial_BalAsBank_cr=document.getElementById('<%=hdf_Initial_BalAsBank_cr.ClientID%>');
        
        var hdf_Initial_BalAsBank_dr=document.getElementById('<%=hdf_Updated_BalAsBank_dr.ClientID%>');
        var hdf_Initial_BalAsBank_cr=document.getElementById('<%=hdf_Updated_BalAsBank_cr.ClientID%>');


        if(chk_Select.checked)
        {
            hdf_Initial_BalAsBank_dr.value=round(Math.abs(hdf_Initial_BalAsBank_dr.value)+Dt);
            hdf_Initial_BalAsBank_cr.value=round(Math.abs(hdf_Initial_BalAsBank_cr.value)+Cr);


           var result = hdf_Initial_BalAsBank_cr.value *1 - hdf_Initial_BalAsBank_dr.value *1;
           if(result<0)
           {
                lbl_Bal_As_Bank_dr.innerText=round(Math.abs(result));
                lbl_Bal_As_Bank_cr.innerText='0.000'
           }
           else
           {
                lbl_Bal_As_Bank_cr.innerText=round(Math.abs(result));
                lbl_Bal_As_Bank_dr.innerText='0.000'
           }  
           
        }
        else
        {
           hdf_Initial_BalAsBank_dr.value=round(Math.abs(hdf_Initial_BalAsBank_dr.value)-Dt);
           hdf_Initial_BalAsBank_cr.value=round(Math.abs(hdf_Initial_BalAsBank_cr.value)-Cr);
             
           var result = hdf_Initial_BalAsBank_cr.value*1 - hdf_Initial_BalAsBank_dr.value*1;
           if(result<0)
           {
               lbl_Bal_As_Bank_dr.innerText=round(Math.abs(result));
               lbl_Bal_As_Bank_cr.innerText='0.000';
           }
           else
           {
               lbl_Bal_As_Bank_cr.innerText=round(Math.abs(result));
               lbl_Bal_As_Bank_dr.innerText='0.000';
           } 
           
         }
         
         hdf_Initial_BalAsBank_dr.value=lbl_Bal_As_Bank_dr.innerText;
         hdf_Initial_BalAsBank_cr.value=lbl_Bal_As_Bank_cr.innerText;
 
   }
     
     
    
  function SetOnSelectAll(chk_SelectAll)
  {
        var lbl_Amt_Not_cleared_dr =document.getElementById('<%=lbl_Amt_Not_cleared_dr.ClientID%>');
         var lbl_Amt_Not_cleared_cr =document.getElementById('<%=lbl_Amt_Not_cleared_cr.ClientID%>');
         var lbl_Bal_As_Bank_dr =document.getElementById('<%=lbl_Bal_As_Bank_dr.ClientID%>');
         var lbl_Bal_As_Bank_cr =document.getElementById('<%=lbl_Bal_As_Bank_cr.ClientID%>');

        var Initial_AmountNotBank_dr=round(<%=Initial_AmountNotBank_dr%>);
        var Initial_AmountNotBank_cr=round(<%=Initial_AmountNotBank_cr%>);
  
        var Initial_BalAsBank_dr=round(<%=Initial_BalAsBank_dr%>);
        var Initial_BalAsBank_cr=round(<%=Initial_BalAsBank_cr%>);
        
      
        if(chk_SelectAll.checked)
        {
              var cr =  Initial_AmountNotBank_dr*1 + Initial_BalAsBank_dr*1 ;
              var dr =  Initial_AmountNotBank_cr*1 + Initial_BalAsBank_cr*1 ;   

              lbl_Amt_Not_cleared_dr.innerText=round(Initial_AmountNotBank_dr*1);
              lbl_Amt_Not_cleared_cr.innerText=round(Initial_AmountNotBank_cr*1);

              var result = cr - dr;
              if(result<0)
              {
                  lbl_Bal_As_Bank_dr.innerText=round(Math.abs(result));
                  lbl_Bal_As_Bank_cr.innerText='0.000';
              }
              else
              {
                  lbl_Bal_As_Bank_dr.innerText=round(Math.abs(result));
                  lbl_Bal_As_Bank_cr.innerText='0.000';
              }       
        }
        else
        {
               lbl_Amt_Not_cleared_dr.innerText=round(Initial_AmountNotBank_dr*1);
               lbl_Amt_Not_cleared_cr.innerText=round(Initial_AmountNotBank_cr*1);   

               lbl_Bal_As_Bank_dr.innerText=round(Initial_BalAsBank_dr*1);
               lbl_Bal_As_Bank_cr.innerText=round(Initial_BalAsBank_cr*1);     
        }
  }
  
function round(value)
{   
    var result;
    var mantissa;
    var exponent;
    
    result = Math.round(value * 1000)/1000;
    
    result = new String(result);
    var index;
    index = result.indexOf('.',1);
    
    if (index == -1)
        index = result.length ;
    
    mantissa = result.substring(0,index);
    exponent = result.substring(index+1,result.length+1);
    
    exponent = new String(exponent);
    if (exponent.length < 3)
        {
            switch(exponent.length)
            {
                case 0:
                    exponent = exponent + '000';
                    break;
                    
                case 1:
                    exponent = exponent + '00';
                    break;
                case 2:
                    exponent = exponent + '0';
                    break;
            }
         }
    else if(exponent.length > decLength)
        exponent = exponent.substring(1,decLength);
        
    return mantissa + '.' + exponent;
}
 

</script>


<asp:Panel ID="Panel1" runat="server" Width="100%">
    <table style="width: 100%" class = "TABLE">
        <tr>
            <td colspan="3" style="width: 100px; background-color: #ffffcc">
                <asp:Label ID="lbl_BankRecoHeading" runat="server" Font-Bold = "true"  CssClass="HEADINGLABEL"
                Text="BANK RECONCILLATION"></asp:Label></td>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
        </tr>
        <tr>
             <td style="width: 100px; background-color: #ffffcc;">&nbsp;
            </td>
             <td style="width: 100px; background-color: #ffffcc;">
            </td>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
             <td style="width: 100px; background-color: #ffffcc;">
            </td>
             <td style="width: 100px; background-color: #ffffcc;">
            </td>
             <td style="width: 100px; background-color: #ffffcc;">
            </td>
             <td style="width: 100px; background-color: #ffffcc;">
            </td>
             <td style="width: 100px; background-color: #ffffcc;">
            </td>
        </tr>
        <tr>
            <td colspan="3" style="background-color: #ffffcc">
            <asp:Label ID="lbl_LedgerName" runat="server" Font-Bold="true"></asp:Label></td>
            <td style="background-color: #ffffcc;">
            </td>
            <td style="background-color: #ffffcc;">
            </td>
            <td colspan="3" style="background-color: #ffffcc">
            From <asp:Label ID="lbl_Start_Date" runat="server" Font-Bold="true" Text="01-Apr-2008"></asp:Label>&nbsp;
            To &nbsp;<asp:Label ID="lbl_EndDate" runat="server" Font-Bold="true"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td colspan="8">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="8">
           <asp:DataGrid ID="dg_BankReco" runat="server" AutoGenerateColumns="False" PageSize="10"
                Width="100%" CssClass="GRID" PagerStyle-Mode="NumericPages" 
                OnItemDataBound="dg_BankReco_ItemDataBound">
            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
            <FooterStyle CssClass="GRIDFOOTERCSS" />
            
                 
                <Columns>
                <asp:TemplateColumn   Visible="false">
                   <ItemTemplate>
                    <asp:Label  ID="lbl_Voucher_Id" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Voucher_Id")%>'></asp:Label>
                 </ItemTemplate> 
                </asp:TemplateColumn>

                 <asp:TemplateColumn   ItemStyle-Width="8%" HeaderText="Sr No">
                   <ItemTemplate>
                       <asp:Label ID = "lbl_SrNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Sr_No")%>'></asp:Label>
                       <%-- <asp:CheckBox ID="chk_Select" runat="server"  Font-Bold="true"  TextAlign="Right" Checked='<%#(System.Boolean)DataBinder.Eval(Container.DataItem, "Is_Select")%>'  Text='<%#DataBinder.Eval(Container.DataItem, "Sr_No")%>'/>--%>
                 </ItemTemplate> 
                </asp:TemplateColumn>
                
                <asp:BoundColumn ItemStyle-Width="10%"  HeaderText="Voucher No" ReadOnly="true" DataField="Voucher_No" ></asp:BoundColumn>
                
                <asp:BoundColumn ItemStyle-Width="10%"  HeaderText="Date" ReadOnly="true" DataField="Cheque_Date"  ></asp:BoundColumn>
                
<%--                <asp:BoundColumn ItemStyle-Width="10%" HeaderText="Particulars" ReadOnly="true" DataField="Particulars" ></asp:BoundColumn>
 
                <asp:BoundColumn  HeaderText="Narration" ReadOnly="true" DataField="Narration" ></asp:BoundColumn>
--%>                
                <asp:TemplateColumn HeaderText="Particulars">
                  <HeaderStyle Width = "30%"/>
                     <ItemTemplate>
                      <asp:Label ID ="lbl_Particulars" runat="server"> <%#DataBinder.Eval(Container.DataItem, "Particulars")%> </asp:Label>
                      <br />
                      <asp:Label ID ="lbl_Narration" runat="server" ForeColor ="Red"> <%#DataBinder.Eval(Container.DataItem, "Narration")%> </asp:Label>
                     </ItemTemplate>
                 </asp:TemplateColumn>
                <asp:BoundColumn ItemStyle-Width="15%"  HeaderText="Voucher Type" ReadOnly="true" DataField="Voucher_Type"  ></asp:BoundColumn>

                <asp:BoundColumn ItemStyle-Width="10%"  HeaderText="Cheque No" ReadOnly="true" DataField="Cheque_No" ></asp:BoundColumn>
              
            <%-- <asp:TemplateColumn HeaderText="Bank Date" ItemStyle-Width="20%">
                      
                         <ItemTemplate>
                        
                            <uc1:wuc_Date_Picker ID="dtp_BankDate" runat="server" Selected_Date='<%#(System.DateTime)DataBinder.Eval(Container.DataItem, "Bank_Date")%>' />
                            
                         </ItemTemplate> 

                 </asp:TemplateColumn> --%>
                          
                  <asp:TemplateColumn HeaderText="Bank Date">
                      
                         <ItemTemplate>
                            <asp:Label ID ="lbl_Bank_Date" runat="server">  </asp:Label>                            
                         </ItemTemplate> 

                 </asp:TemplateColumn>  
                
                  <asp:BoundColumn  ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%"  HeaderText="Debit" ReadOnly="true" DataField="Debit"  ></asp:BoundColumn>
                
                  <asp:BoundColumn ItemStyle-HorizontalAlign="Right" ItemStyle-Width="10%"  HeaderText="Credit" ReadOnly="true" DataField="Credit"  ></asp:BoundColumn>
                    
                 </Columns>
                 
            </asp:DataGrid></td>
        </tr>
        <tr>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
            <td colspan="3" style="width: 100px; background-color: #ffffcc">
             <asp:Label ID="lbl_1" runat="server" Text="Balance as per Company Books:" Font-Bold="true" ></asp:Label></td>
            <td style="width: 100px; background-color: #ffffcc;">
             <asp:Label ID="lbl_Bal_Comp_dr" runat="server" Font-Bold="true" ></asp:Label></td>
            <td style="width: 100px; background-color: #ffffcc;">
             <asp:Label ID="lbl_Bal_Comp_cr" runat="server"  Font-Bold="true"  ></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
            <td colspan="3" style="width: 100px; background-color: #ffffcc">
             <asp:Label ID="lbl_2" runat="server" Text="Ammount Not Reflected In Bank:" Font-Bold="true" ></asp:Label></td>
            <td style="width: 100px; background-color: #ffffcc;">
             <asp:Label ID="lbl_Amt_Not_cleared_dr" Font-Bold="true" runat="server" ></asp:Label></td>
            <td style="width: 100px; background-color: #ffffcc;">
             <asp:Label ID="lbl_Amt_Not_cleared_cr"  Font-Bold="true" runat="server" ></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
            <td style="width: 100px; background-color: #ffffcc;">
            </td>
            <td colspan="3" style="width: 100px; background-color: #ffffcc">
             <asp:Label ID="lbl_3" runat="server" Text="Balance as Per Bank:" Font-Bold="true" ></asp:Label></td>
            <td style="width: 100px; background-color: #ffffcc;">
             <asp:Label ID="lbl_Bal_As_Bank_dr" runat="server" Font-Bold="true" EnableViewState="true" ></asp:Label></td>
            <td style="width: 100px; background-color: #ffffcc;">
             <asp:Label ID="lbl_Bal_As_Bank_cr" runat="server" Font-Bold="true" EnableViewState="true"  ></asp:Label></td>
        </tr>
    </table>
</asp:Panel>

<table style="width: 100%" class = "TABLE">
    <tr>
        <td colspan="5">
 <asp:UpdatePanel ID="upnl_Error" runat="server" UpdateMode="Conditional">
        <ContentTemplate> 
    &nbsp; <asp:Label ID="lbl_Errors" runat="server"  CssClass="LABELERROR" EnableViewState="false" ></asp:Label>
       </ContentTemplate>
        <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="btn_save" />
                 
         </Triggers>
        </asp:UpdatePanel> 
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <table  style=" width:100%" class="TABLE"  >
                <tr>
           <td style="width: 539px"> 
             <input id="hdf_Initial_BalAsBank_dr" type="hidden" runat="server"/> 
             <input id="hdf_Initial_BalAsBank_cr" type="hidden" runat="server" />  
             <input id="hdf_Updated_BalAsBank_dr" type="hidden" runat="server" />  
             <input id="hdf_Updated_BalAsBank_cr" type="hidden" runat="server" />  
             <input id="hdf_Updated_Amt_Not_cleared_dr" type="hidden" runat="server"/>  
             <input id="hdf_Updated_Amt_Not_cleared_cr" type="hidden" runat="server"/>  
          </td>
         <td style="width: 3px">
         </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
