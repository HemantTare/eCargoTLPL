<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrackNTraceUserInformation.ascx.cs" Inherits="TrackNTrace_WucTrackNTraceUserInformation" %>

<asp:Label ID="lbl_Heading" runat="server" CssClass="LABEL" Font-Bold="True" Text="User Details"></asp:Label>
<asp:DetailsView ID="DetailsView1" runat="server" style="position: static;" GridLines="Horizontal" CssClass="DETAILSVIEW" OnDataBound="DetailsView1_DataBound">
</asp:DetailsView>
