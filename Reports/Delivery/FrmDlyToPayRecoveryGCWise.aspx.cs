using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Raj.EC;

//Author : Ankit champaneriya
//Desc   : Booking register Report
//Date   : 03-01-09

public partial class Reports_Delivery_FrmDlyToPayRecoveryGCWise : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    //string LRNo;
    decimal Total_Amount;
    string Crypt;

    public int DDC_ID
    {
        get { return Convert.ToInt32(ViewState["_DDC_ID"]); }
        set { ViewState["_DDC_ID"] = value; }
    }
    public string DlyNo
    {
        get { return Convert.ToString(ViewState["_DlyNo"]); }
        set 
        {
            ViewState["_DlyNo"] = value;
            lblClient_Name.Text = value;
        }
    }
    public int MONTHID
    {
        get { return Convert.ToInt32(ViewState["_MONTHID"]); }
        set { ViewState["_MONTHID"] = value; }
    }
    public string MONTHNAME
    {
        get { return Convert.ToString(ViewState["_MONTHNAME"]); }
        set
        {
            ViewState["_MONTHNAME"] = value;
            lblMONTHNAME.Text = value;
        }
    }
    public string TempoFrt
    {
        get { return Convert.ToString(ViewState["_TempoFrt"]); }
        set
        {
            ViewState["_TempoFrt"] = value;
            lblTempoFrt.Text = " TempoFrt : " + value; ;
        }
    }
    public string Bonus
    {
        get { return Convert.ToString(ViewState["_Bonus"]); }
        set
        {
            ViewState["_Bonus"] = value;
            lblBonus.Text = " Bonus : " + value;
        }
    }
    public string TotalTmpFrt
    {
        get { return Convert.ToString(ViewState["_TotalTmpFrt"]); }
        set
        {
            ViewState["_TotalTmpFrt"] = value;
            lblTotalTmpFrt.Text = " TotalTmpFrt : " + value;
        }
    } 
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "DlyToPayRecoveryDateWise";

        if (IsPostBack == false)
        {
            Crypt = Request.QueryString["DDC_ID"];
            DDC_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt); 

            Crypt = Request.QueryString["DlyNo"];
            DlyNo = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["MONTHID"];
            MONTHID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["MONTHNAME"];
            MONTHNAME = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["TempoFrt"];
            TempoFrt = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["Bonus"];
            Bonus = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["TotalTmpFrt"];
            TotalTmpFrt = ClassLibraryMVP.Util.DecryptToString(Crypt); 

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_GridGCWise);

            BindGrid("form", e);
             
        }
    } 
     

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom= (string)(sender);

        int grid_currentpageindex = dg_GridGCWise.CurrentPageIndex;
        int grid_PageSize = dg_GridGCWise.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }
        DateTime StartDate = UserManager.getUserParam().StartDate;
        DateTime EndDate = UserManager.getUserParam().EndDate;

        SqlParameter[] objSqlParam ={ 
             objDAL.MakeInParams("@Flag", SqlDbType.Int,0,3), 
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int,0,DDC_ID),
            objDAL.MakeInParams("@StartDate", SqlDbType.DateTime,0,StartDate),
            objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,EndDate), 
            objDAL.MakeInParams("@MonthID", SqlDbType.Int,0,MONTHID) 
        };


        objDAL.RunProc("EC_RPT_DlyToPayRecovery_GRD", objSqlParam, ref ds);
       

        dg_GridGCWise.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[2].Rows[0][0].ToString();
        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_GridGCWise, ds.Tables[0], CallFrom,lbl_Error,TotalRecords);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }          
    }

    private void PrepareDTForExportToExcel()
    { 
        DataRow dr = ds.Tables[0].NewRow();
        dr["BkgBranch"] = "Total";
        dr["Total_Amount"] = Total_Amount; 
        ds.Tables[0].Rows.Add(dr); 

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void dg_GridGCWise_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_GridGCWise.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Total_Amount = Util.String2Decimal(dr["Total_Amount"].ToString()); 
        
    }
    #endregion


    protected void dg_GridGCWise_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        //LinkButton lbtn_DlyNo;
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_LRNo, lbl_Total_Amount;

            lbl_LRNo = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_LRNo");
            lbl_Total_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Amount");

            //lbl_LRNo.Text = LRNo.ToString();
            lbl_Total_Amount.Text = Total_Amount.ToString(); 
        }
        //string DlyToPayRecDateWiseUrl;
        //if (e.Item.ItemIndex != -1)
        //{
        //    HiddenField hdfn_DDC_ID;
        //    hdfn_DDC_ID = (HiddenField)e.Item.FindControl("hdfn_DDC_ID");

        //    DlyToPayRecDateWiseUrl = ClassLibraryMVP.Util.GetBaseURL() +
        //                "/Reports/Delivery/FrmDlyToPayRecoveryDateWise.aspx?DDC_ID=" + Util.EncryptInteger(Convert.ToInt32(hdfn_DDC_ID.Value));

        //    lbtn_DlyNo = (LinkButton)e.Item.FindControl("lbtn_DlyNo");
        //    lbtn_DlyNo.Attributes.Add("onclick", "return DlyToPayRecDate('" + DlyToPayRecDateWiseUrl + "');");

        //}
       
    }

    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    
}
