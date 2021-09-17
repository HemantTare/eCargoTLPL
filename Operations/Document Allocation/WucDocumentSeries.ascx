<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDocumentSeries.ascx.cs" Inherits="Operations_Document_Allocation_WucDocumentSeries" %>
<%@ Register Src="../../CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>   
<script type="text/javascript" language="javascript" src="../../Javascript/ddlsearch.js"></script> 
<script type="text/javascript" language="javascript" src="../../Javascript/Operations/Document Allocation/DocumentSeries.js"></script>    
<asp:ScriptManager ID="scm_DocumentSeries" runat="server"></asp:ScriptManager>
    
<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">&nbsp;
            <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="DOCUMENT SERIES" meta:resourcekey="lbl_HeadingResource1"  ></asp:Label>
        </td>        
    </tr>
     <tr>
        <td colspan="6">&nbsp;</td>
    </tr>    
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_DocumentType" runat="server" CssClass="LABEL" Text="Document Type :" meta:resourcekey="lbl_DocumentTypeResource1" ></asp:Label></td>
        <td style="width: 28%">
           <asp:DropDownList ID="ddl_DocumentType" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_DocumentType_SelectedIndexChanged">
            </asp:DropDownList></td>
        <td style="width: 2%" class="TDMANDATORY">*</td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_DateofPrinting" runat="server" CssClass="LABEL" Text="Date Of Allocation :" meta:resourcekey="lbl_DateofPrintingResource1" ></asp:Label></td>
        <td style="width: 28%">
            <uc1:WucDatePicker ID="WucDateofAllocation" runat="server" />
        </td>        
        <td style="width: 2%"></td>
    </tr>
    <tr>
        <td style="width: 50%"  colspan="3" >
          <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
            <table width="100%" id="tbl_Branch" runat="server">
            <tr>
                <td class="TD1" style="width: 40%">
                    <asp:Label ID="lbl_Branch" runat="server" CssClass="LABEL" Text="Branch :" meta:resourcekey="lbl_BranchResource1"></asp:Label>
                </td>
                <td style="width: 58%"> 
                    <cc1:ddlsearch id="ddl_Branch" runat="server" callbackfunction="Raj.EF.CallBackFunction.CallBack.GetBranch"
                    iscallback="True" PostBack="True" OnTxtChange="ddl_Branch_TxtChange" ></cc1:ddlsearch>         
                </td>
                <td style="width: 2%" class="TDMANDATORY">*</td>
            </tr>
            </table>
            </ContentTemplate>
             <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />                    
             </Triggers>
         </asp:UpdatePanel>  
        </td> 
      
        <td style="width: 50%" colspan="3"></td>      
    </tr>
    <tr>
        <td class="TD1" style="width: 100%" colspan="6">
          <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <uc2:WucHierarchyWithID ID="WucHierarchyWithID1" runat="server" />   
             </ContentTemplate>
             <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />                    
             </Triggers>
         </asp:UpdatePanel>         
        </td> 
    </tr>        
    <tr id="tr_VA" runat="server">
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_VA" runat="server" CssClass="LABEL" Text="VA :"></asp:Label></td>
        <td style="width: 28%">
          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="ddl_VA" runat="server" CssClass="DROPDOWN"></asp:DropDownList>
            </ContentTemplate>
             <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Branch" />                    
             </Triggers>
         </asp:UpdatePanel>
        </td>
        <td style="width: 2%"></td>
        <td style="width: 50%" colspan="3"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; " valign="top">
            <asp:Label ID="lbl_PrintedSeries" runat="server" CssClass="LABEL" Text="Printed Series :" meta:resourcekey="lbl_PrintedSeriesResource1" ></asp:Label></td>
        <td colspan="4" >
           <asp:UpdatePanel ID="Upd_Pnl_dg_PrintedSeries" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dg_PrintedSeries" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" CellPadding="2" CssClass="GRID" 
                        PageSize="15" Width="100%" meta:resourcekey="dg_PrintedSeriesResource1" >
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <Columns>
                          
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_PrintedSeriesID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Series_Printing_ID") %>' meta:resourcekey="lbl_PrintedSeriesIDResource1" ></asp:Label>
                                </ItemTemplate>                               
                                <ItemStyle CssClass="HIDEGRIDCOL" />
                                <HeaderStyle CssClass="HIDEGRIDCOL" />
                            </asp:TemplateColumn>     
                               <asp:TemplateColumn>
                                <ItemTemplate>
                                  <asp:RadioButton ID="rdo_Series" AutoPostBack="True"   runat="server" OnCheckedChanged="rdo_Series_CheckedChanged" meta:resourcekey="rdo_SeriesResource1" />  
                                </ItemTemplate>                                                               
                            </asp:TemplateColumn>                       
                            <asp:TemplateColumn HeaderText="Start No" >
                                <ItemTemplate>
                                    <asp:Label ID="lbl_StartNo" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Start_No") %>' meta:resourcekey="lbl_StartNoResource1" ></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="End No">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_EndNo" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "End_No") %>' meta:resourcekey="lbl_EndNoResource1" ></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Balance" >
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Balance" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Balance") %>' meta:resourcekey="lbl_BalanceResource1" ></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateColumn>                            
                        </Columns>
                        <PagerStyle Mode="NumericPages" />
                    </asp:DataGrid>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />                    
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%; ">
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_StartNo" runat="server" CssClass="LABEL" Text="Start No :" meta:resourcekey="lbl_StartNoResource2" ></asp:Label></td>
        <td style="width: 28%">
        <asp:UpdatePanel ID="Upd_Pnl_txt_StartNo" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_PrintedSeries" />
                <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />
            </Triggers>
            <ContentTemplate>
                <asp:TextBox ID="txt_StartNo" runat="server" CssClass="TEXTBOXNOS" MaxLength="9" onkeypress="return Only_Integers(this,event)"></asp:TextBox>
            </ContentTemplate>            
            </asp:UpdatePanel>
            </td>
        <td style="width: 2%">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_EndNo" runat="server" CssClass="LABEL" Text="End No :" meta:resourcekey="lbl_EndNoResource2" ></asp:Label></td>
        <td style="width: 28%">
        <asp:UpdatePanel ID="Upd_Pnl_txt_EndNo" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_PrintedSeries" />
                <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />
            </Triggers>
            <ContentTemplate>
                <asp:TextBox ID="txt_EndNo" runat="server" CssClass="TEXTBOXNOS" MaxLength="9" onkeypress="return Only_Integers(this,event)"></asp:TextBox>
           </ContentTemplate>                
        </asp:UpdatePanel>
        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" align="left">  
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1" ></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save"  OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    <tr>
        <td align="left"  colspan="6">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dg_PrintedSeries" />
            </Triggers>
            <ContentTemplate>
                <asp:HiddenField ID="hdn_StartNo" runat="server" />
                <asp:HiddenField ID="hdn_EndNo" runat="server" />
                <asp:HiddenField ID="hdn_PrintedSeriesID" runat="server" />
                <asp:HiddenField ID="hdn_MinStartNo" runat="server" />
                <asp:HiddenField ID="hdn_MaxEndNo" runat="server" />
                <asp:HiddenField ID="hdn_ParentStartNo" runat="server" />
                <asp:HiddenField ID="hdn_ParentEndNo" runat="server" />
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
