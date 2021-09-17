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
using ClassLibraryMVP;

public partial class CommonControls_Wuc_Export_To_Excel : System.Web.UI.UserControl
{
    public event EventHandler btn_export_to_excel_click;

    private string _FileName;
    private DataTable _dt_to_export;
    private bool _has_last_row_as_total = true;

    public string FileName
    {
        get { return _FileName; }
        set { _FileName = value; }
    }

    public DataTable SessionExporttoExcel
    {
        get { return _dt_to_export; }
        set { _dt_to_export = value; }
    }

    public bool btnExportToExcelEnabled
    {
        set { btn_export_to_excel.Enabled = value; }
    }

    public bool has_last_row_as_total
    {
        set { _has_last_row_as_total = value; }
        get { return _has_last_row_as_total; }
    }

    //public DataTable SessionExporttoExcel
    //{
    //    get { return StateManager.GetState<DataTable>("DatasetForExporttoExcel"); }
    //    set { StateManager.SaveState("DatasetForExporttoExcel", value); }
    //}

    protected void btn_export_to_excel_Click(object sender, EventArgs e)
    {
        if (btn_export_to_excel_click != null)
            btn_export_to_excel_click("exporttoexcelusercontrol", e);

        DataRow dr = SessionExporttoExcel.Rows[SessionExporttoExcel.Rows.Count - 1];

        int counttocheck = 0;

        if (has_last_row_as_total == true)
        {
            counttocheck = 1;
        }

        if (SessionExporttoExcel.Rows.Count > counttocheck)
        {
            GridView dg1 = new GridView();
            SetStandardCaption();
            dg1.DataSource = SessionExporttoExcel;
            dg1.DataBind();
            StringWriter SW = new StringWriter();
            HtmlTextWriter htmlWrite = new HtmlTextWriter(SW);
            dg1.RenderControl(htmlWrite);
            Response.Clear();
            Response.Charset = "";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".xls");
            Response.ContentEncoding = System.Text.Encoding.Default;
            Response.ContentType = "application/vnd.ms-excel";
            Response.Write(SW.ToString());
            Response.End();
        }
        else
        {
            //Response.Write("<script type='text/javascript'> {alert('No Record(s) Found');}</script>");
            String popupScript = "<script language='javascript'>alert('No Record(s) Found');</script>";

            Page.ClientScript.RegisterStartupScript(typeof(String), "PopupScript1", popupScript.ToString(), false);

        }
        SessionExporttoExcel = null;
    }

    private void SetStandardCaption()
    {
        //dg_Grid.Columns[0].HeaderText = CompanyManager.getCompanyParam().GcCaption + " No";
        //dg_Grid.Columns[1].HeaderText = CompanyManager.getCompanyParam().GcCaption + " Date";

        string col_header = "";

        for (int i = 0; i <= SessionExporttoExcel.Columns.Count - 1; i++)
        {
            col_header = SessionExporttoExcel.Columns[i].ColumnName;

            if (col_header.ToLower().Contains("gc_caption"))
            {
                SessionExporttoExcel.Columns[i].ColumnName = col_header.Replace("gc_caption", CompanyManager.getCompanyParam().GcCaption);
            }
            else if (col_header.ToLower().Contains("lhpo_caption"))
            {
                SessionExporttoExcel.Columns[i].ColumnName = col_header.Replace("lhpo_caption", CompanyManager.getCompanyParam().LHPOCaption);
            }
        }
    }

}
