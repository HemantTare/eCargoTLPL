<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPrintingStationary.ascx.cs" Inherits="Operations_Document_Allocation_WucPrintingStationary" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"  TagPrefix="uc1" %>
    <script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>
    <script type="text/javascript" language="javascript" src="../../Javascript/Operations/Document Allocation/PrintingStationary.js"></script>
    <asp:ScriptManager ID="scm_PrintingStationary" runat="server"></asp:ScriptManager>
<table style="width: 100%" class="TABLE">
<tr>
<td class="TDGRADIENT" colspan="6">
&nbsp;<asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="PRINTING STATIONARY" meta:resourcekey="lbl_HeadingResource1"  ></asp:Label>

        </td>
        
    </tr>
     <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_DateofPrinting" runat="server" CssClass="LABEL" Text="Date Of Printing:" meta:resourcekey="lbl_DateofPrintingResource1"></asp:Label></td>
        <td style="width: 28%">
            <uc1:WucDatePicker ID="WucDateofPrinting" runat="server" />
        </td>
        <td style="width: 2%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 28%">
        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_DocumentType" runat="server" CssClass="LABEL" Text="Document Type:" meta:resourcekey="lbl_DocumentTypeResource1"></asp:Label></td>
        <td style="width: 28%">
           <asp:DropDownList ID="ddl_DocumentType" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_DocumentType_SelectedIndexChanged" meta:resourcekey="ddl_DocumentTypeResource1" >
            </asp:DropDownList></td>
        <td style="width: 2%">
        </td>
        <td style="width: 20%">
        </td>
        <td style="width: 28%">
        </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; " valign="top">
            <asp:Label ID="lbl_GeneratedSeries" runat="server" CssClass="LABEL" Text="Generated Series:" meta:resourcekey="lbl_GeneratedSeriesResource1"></asp:Label></td>
        <td colspan="4" >
            <asp:UpdatePanel ID="Upd_Pnl_dg_GeneratedSeries" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dg_GeneratedSeries" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" CellPadding="2" CssClass="GRID" 
                        PageSize="15" Width="100%" meta:resourcekey="dg_GeneratedSeriesResource1" >
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <Columns>
                          
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_SeriesGenerationID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Series_Generation_ID") %>' meta:resourcekey="lbl_SeriesGenerationIDResource1"></asp:Label>
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
                                    <asp:Label ID="lbl_StartNo" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Start_No") %>' meta:resourcekey="lbl_StartNoResource1"></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="End No">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_EndNo" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "End_No") %>' meta:resourcekey="lbl_EndNoResource1"></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Balance" >
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Balance" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Balance") %>' meta:resourcekey="lbl_BalanceResource1"></asp:Label>
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
        <td class="TD1" colspan="6">
            &nbsp; &nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_StartNo" runat="server" CssClass="LABEL" Text="Start No:" meta:resourcekey="lbl_StartNoResource2"></asp:Label></td>
        <td style="width: 28%">
        <asp:UpdatePanel ID="Upd_Pnl_txt_StartNo" runat="server" UpdateMode="conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dg_GeneratedSeries" />
            <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />
        </Triggers>
                <ContentTemplate>
            <asp:TextBox ID="txt_StartNo" runat="server" CssClass="TEXTBOXNOS" MaxLength="9" onkeypress="return Only_Integers(this,event)" meta:resourcekey="txt_StartNoResource1"></asp:TextBox>
            </ContentTemplate>            
            </asp:UpdatePanel>
            </td>
        <td style="width: 2%">
        </td>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_EndNo" runat="server" CssClass="LABEL" Text="End No:" meta:resourcekey="lbl_EndNoResource2"></asp:Label></td>
        <td style="width: 28%">
        <asp:UpdatePanel ID="Upd_Pnl_txt_EndNo" runat="server" UpdateMode="conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dg_GeneratedSeries" />
            <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />
        </Triggers>
                <ContentTemplate>
            <asp:TextBox ID="txt_EndNo" runat="server" CssClass="TEXTBOXNOS" MaxLength="9" 
                onkeypress="return Only_Integers(this,event)" meta:resourcekey="txt_EndNoResource1"></asp:TextBox>
                </ContentTemplate>
                
                </asp:UpdatePanel>
                </td>
        <td style="width: 2%">
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6" align="left">
       <%-- <asp:UpdatePanel ID="Upd_Pnl_lbl_Errors" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dg_GeneratedSeries" />
            <asp:AsyncPostBackTrigger ControlID="btn_Save" />
        </Triggers>
                <ContentTemplate>--%>
            &nbsp;<asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
          <%--  </ContentTemplate>
            
            </asp:UpdatePanel>--%>
            </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" /></td>
    </tr>
    <tr>
        <td align="left"  colspan="6">
       <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dg_GeneratedSeries" />
        </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="hdn_StartNo" runat="server" />
                    <asp:HiddenField ID="hdn_EndNo" runat="server" />
                    <asp:HiddenField ID="hdn_SeriesGenerationID" runat="server" />
                    <asp:HiddenField ID="hdn_MinStartNo" runat="server" />
                    <asp:HiddenField ID="hdn_MaxEndNo" runat="server" />
                    <asp:HiddenField ID="hdn_ParentStartNo" runat="server" />
                    <asp:HiddenField ID="hdn_ParentEndNo" runat="server" />
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
