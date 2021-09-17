using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class RedirectPage : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        string Msg = Request.QueryString["Msg"];
        string Url = Request.QueryString["Url"];
        string RefreshParentPage = Request.QueryString["RefreshParentPage"];
        string DecryptUrl = Request.QueryString["DecryptUrl"];

        if (Msg != null)
        {
            Msg = ClassLibraryMVP.Util.DecryptToString(Request.QueryString["Msg"]);
            Response.Write("<script type='text/javascript'> {alert('" + Msg + "');}</script>");
        }

        if (Url == null)
        {
            if (RefreshParentPage == null)
            {
                Response.Write("<script language='javascript'> {self.close() }</script>");
            }
            else
            {
                Response.Write("<script language='javascript'> {window.opener.location.reload();window.close();}</script>");
            }
        }
        else
        {
            if (DecryptUrl == null)
                Url = ClassLibraryMVP.Util.DecryptToString(Url);
            else
            {
                string baseurl = ClassLibraryMVP.Util.GetBaseURL();
                Url = baseurl + "/" + Url + "&Mode=" + Request.QueryString["Mode"];
            }

            Response.Write("<script language='javascript'> {location.href('" + Url + "') }</script>");
        }
        
        
        
        
    }
}
