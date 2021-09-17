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
using System.IO;

public partial class FA_Common_Reports_ExportToExcelGrid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string filename = "BANK RECONCILATION";
        StringWriter strWtr = new System.IO.StringWriter();
        Html32TextWriter htmlWtr = new Html32TextWriter(strWtr);

       // string SessKey = Request.QueryString["SessionKey"];

        if (Session["ExportToExcel"] != null)
        {
            dg_Excel.DataSource = (DataTable)Session["ExportToExcel"];
            dg_Excel.DataBind();
            Session["ExportToExcel"] = null;
        }

        this.Panel1.RenderControl(htmlWtr);
        Response.Clear();

        //Use following line if you want to force user to download file instead of displaying it.   
        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
        //Response.AddHeader("Content-Disposition", "attachment; filename=""" + filename + """");
        Response.ContentEncoding = System.Text.Encoding.UTF7;
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel"; //Use application/msword for Word
        Response.Write(strWtr.ToString());
        Response.End();
    }
}
