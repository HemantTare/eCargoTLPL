<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmInsuranceCompanyBranch.aspx.cs" Inherits="Master_Vehicle_frm_Mst_Vehicle_InsuranceCompanyBranch" %>

<%@ Register Src="~/CommonControls/WucViewUserInfo.ascx" TagName="WucViewUserInfo"
    TagPrefix="uc2" %>

<%@ Register Src="WucInsuranceCompanyBranch.ascx" TagName="WucInsuranceCompanyBranch"
    TagPrefix="uc1" %>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title> 
     <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
<uc1:WucInsuranceCompanyBranch id="WucInsuranceCompanyBranch1" runat="server">
</uc1:WucInsuranceCompanyBranch><uc2:WucViewUserInfo ID="WucViewUserInfo1" runat="server" />
    </div>
    </form>
</body>
</html>


