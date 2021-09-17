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

public partial class Reports_Delivery_FrmDlyToPayRecoveryDateWise : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal Cash, Cheque, Credit, Discount, Total, TempoFrt, Bonus, TotalTmpFrt;
    string Crypt;   

    public int RegionID
    {
        get { return Convert.ToInt32(ViewState["_RegionID"]); }
        set { ViewState["_RegionID"] = value; }
    }
    public int AreaID
    {
        get { return Convert.ToInt32(ViewState["_AreaID"]); }
        set { ViewState["_AreaID"] = value; }
    }
    public int BranchID
    {
        get { return Convert.ToInt32(ViewState["_BranchID"]); }
        set { ViewState["_BranchID"] = value; }
    }
    public string DlyDate
    {
        get { return Convert.ToString(ViewState["_DlyDate"]); }
        set { ViewState["_DlyDate"] = value; }
    }
    public string BranchText
    {
        get { return Convert.ToString(ViewState["_BranchText"]); }
        set 
        {
            ViewState["_BranchText"] = value;
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
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "DlyToPayRecoveryDateWise";

        if (IsPostBack == false)
        {
            Crypt = Request.QueryString["Region_Id"];
            RegionID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["Area_Id"];
            AreaID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["Branch_Id"];
            BranchID = ClassLibraryMVP.Util.DecryptToInt(Crypt); 

            DlyDate = Request.QueryString["DlyDate"];

            Crypt = Request.QueryString["MONTHID"];
            MONTHID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["MONTHNAME"];
            MONTHNAME = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["BranchText"];
            BranchText = ClassLibraryMVP.Util.DecryptToString(Crypt); 



            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);

            BindGrid("form", e);
             
        }
    } 
     

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom= (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        SqlParameter[] objSqlParam ={ 
             objDAL.MakeInParams("@Flag", SqlDbType.Int,0,2), 
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int,0,BranchID),
            objDAL.MakeInParams("@StartDate", SqlDbType.DateTime,0,DlyDate),
            objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,DlyDate), 
            objDAL.MakeInParams("@MonthID", SqlDbType.Int,0,MONTHID) 
        };


        objDAL.RunProc("EC_RPT_DlyToPayRecovery_GRD", objSqlParam, ref ds);
       

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[2].Rows[0][0].ToString();
        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom,lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }          
    }

    private void PrepareDTForExportToExcel()
    { 
        DataRow dr = ds.Tables[0].NewRow();
        dr["DlyDate"] = "Total";
        dr["Cash"] = Cash;
        dr["Cheque"] = Cheque;
        dr["Credit"] = Credit;
        dr["Discount"] = Discount;
        dr["Total"] = Total;
        dr["TempoFrt"] = TempoFrt;
        dr["Bonus"] = Bonus;
        dr["TotalTmpFrt"] = TotalTmpFrt;
        ds.Tables[0].Rows.Add(dr);

        ds.Tables[0].Columns.Remove("DDC_ID"); 

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Cash = Util.String2Decimal(dr["Cash"].ToString());
        Cheque = Util.String2Decimal(dr["Cheque"].ToString());
        Credit = Util.String2Decimal(dr["Credit"].ToString());
        Discount = Util.String2Decimal(dr["Discount"].ToString());
        Total = Util.String2Decimal(dr["Total"].ToString());
        TempoFrt = Util.String2Decimal(dr["TempoFrt"].ToString());
        Bonus = Util.String2Decimal(dr["Bonus"].ToString());
        TotalTmpFrt = Util.String2Decimal(dr["TotalTmpFrt"].ToString());    
        
    }
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        LinkButton lbtn_DlyNo;
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_DlyDate, lbl_Cash, lbl_Cheque, lbl_Credit, lbl_Discount, lbl_Total;
            System.Web.UI.WebControls.Label lbl_TempoFrt, lbl_Bonus, lbl_TotalTmpFrt;

            lbl_DlyDate = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_DlyDate");
            lbl_Cash = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Cash");
            lbl_Cheque = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Cheque");
            lbl_Credit = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Credit");
            lbl_Discount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Discount");
            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_TempoFrt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TempoFrt");
            lbl_Bonus = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Bonus");
            lbl_TotalTmpFrt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalTmpFrt"); 

            lbl_Cash.Text = Cash.ToString();
            lbl_Cheque.Text = Cheque.ToString();
            lbl_Credit.Text = Credit.ToString();
            lbl_Discount.Text = Discount.ToString();
            lbl_Total.Text = Total.ToString();
            lbl_TempoFrt.Text = TempoFrt.ToString();
            lbl_Bonus.Text = Bonus.ToString();
            lbl_TotalTmpFrt.Text = TotalTmpFrt.ToString(); 
        }
        string DlyToPayRecDateWiseUrl;
        if (e.Item.ItemIndex != -1)
        {
            HiddenField hdfn_DDC_ID, hdfn_TempoFrt, hdfn_Bonus, hdfn_TotalTmpFrt;
            lbtn_DlyNo = (LinkButton)e.Item.FindControl("lbtn_DlyNo");
            hdfn_DDC_ID = (HiddenField)e.Item.FindControl("hdfn_DDC_ID");
            hdfn_TempoFrt = (HiddenField)e.Item.FindControl("hdfn_TempoFrt");
            hdfn_Bonus = (HiddenField)e.Item.FindControl("hdfn_Bonus");
            hdfn_TotalTmpFrt = (HiddenField)e.Item.FindControl("hdfn_TotalTmpFrt");
            int DDC_ID  = Convert.ToInt32(hdfn_DDC_ID.Value);

            DlyToPayRecDateWiseUrl = ClassLibraryMVP.Util.GetBaseURL() +
                        "/Reports/Delivery/FrmDlyToPayRecoveryGCWise.aspx?DDC_ID=" + Util.EncryptInteger(DDC_ID) + "&DlyNo=" 
                        + Util.EncryptString(lbtn_DlyNo.Text) + "&MONTHID=" + Util.EncryptInteger(MONTHID)
                        + "&MONTHNAME=" + Util.EncryptString(MONTHNAME)
                        + "&TempoFrt=" + Util.EncryptString(hdfn_TempoFrt.Value)
                        + "&Bonus=" + Util.EncryptString(hdfn_Bonus.Value)
                        + "&TotalTmpFrt=" + Util.EncryptString(hdfn_TotalTmpFrt.Value);

            lbtn_DlyNo.Attributes.Add("onclick", "return DlyToPayRecGC('" + DlyToPayRecDateWiseUrl + "');");

        }
       
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
