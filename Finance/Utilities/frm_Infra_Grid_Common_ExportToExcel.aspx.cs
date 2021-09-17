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

public partial class FA_Common_Reports_Infra_Grid_Common_ExportToExcel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ExportToExcel"] != null)
        {
            grid.DataSource = (DataTable)Session["ExportToExcel"];
            grid.DataBind();
            ExcelExporter.Export(grid); 
            Session["ExportToExcel"] = null;
        }
    }
}
