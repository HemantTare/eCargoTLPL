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
using System.Data.SqlClient;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using ClassLibraryMVP;

public partial class Reports_CL_Nandwana_DOC_Monitoring_Frm_Rpt_Missing_Document_Details : System.Web.UI.Page
{
    int Doc_Id, Alloc_Id;
    string Branch_Name, Start_No, End_No, Document;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        Doc_Id = Convert.ToInt32(Request.QueryString["Document_Id"]);
        Alloc_Id = Convert.ToInt32(Request.QueryString["Allocation_Id"]);
        Branch_Name = (Request.QueryString["Branch_Name"]);
        BindGrid("form", e);

    }
    private void BindGrid(object sender, EventArgs e)
    {

        DAL objDAL = new DAL();
        SqlParameter[] sqlParam = {
            objDAL.MakeInParams("@Allocation_Id", SqlDbType.Int,0,Alloc_Id),
            objDAL.MakeInParams("@Document_Id", SqlDbType.Int,0,Doc_Id)
            };
        objDAL.RunProc("[EC_RPT_Missing_Document_Detail]", sqlParam, ref ds);

        Rep_Missing.DataSource = ds;
        Rep_Missing.DataBind();
        Assign_Label();
        lbl_Branch.Text = "Branch:" + Branch_Name;
        lbl_Start_No.Text = "Start No:" + Start_No;
        lbl_End_No.Text = "End No:" + End_No;
        lbl_Document.Text = "Document Type:" + Document;

    }

    private void Assign_Label()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Start_No = dr["Start_No"].ToString();
        End_No = dr["End_No"].ToString();
        Document = dr["Document"].ToString();
    }
}
