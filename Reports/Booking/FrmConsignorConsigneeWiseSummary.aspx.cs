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

public partial class Reports_Booking_FrmConsignorConsigneeWiseSummary : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds; 

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
    public int Division_ID
    {
        get { return Convert.ToInt32(ViewState["_Division_ID"]); }
        set { ViewState["_Division_ID"] = value; }
    }
    public int Client_ID
    {
        get { return Convert.ToInt32(ViewState["_Client_ID"]); }
        set { ViewState["_Client_ID"] = value; }
    }
    public string Client_Name
    {
        get { return Convert.ToString(ViewState["_Client_Name"]); }
        set 
        { 
            ViewState["_Client_Name"] = value;
            lblClient_Name.Text = value;
        }
    }
    public string From_Date
    {
        get { return Convert.ToString(ViewState["_From_Date"]); }
        set { ViewState["_From_Date"] = value; }
    }
    public string To_Date
    {
        get { return Convert.ToString(ViewState["_To_Date"]); }
        set { ViewState["_To_Date"] = value; }
    }
    public string StartDate
    {
        get { return Convert.ToString(ViewState["_StartDate"]); }
        set { ViewState["_StartDate"] = value; }
    }
    public string EndDate
    {
        get { return Convert.ToString(ViewState["_EndDate"]); }
        set { ViewState["_EndDate"] = value; }
    }
    public Boolean IsConsignorWise
    {
        get { return Convert.ToBoolean(ViewState["_IsConsignorWise"]); }
        set { ViewState["_IsConsignorWise"] = value; }
    }
    public string Name
    {
        get { return Convert.ToString(ViewState["_Name"]); }
        set { ViewState["_Name"] = value; }
    }

    string Crypt;
    int TotLR, Tot_Art, SubTotal, TotST, TotRoundOff, TotGCAmt, TotDiscount;
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        
            Wuc_Export_To_Excel1.FileName = "ClientWiseSummary";

        if (IsPostBack == false)
        {
            Crypt = Request.QueryString["RegionID"];
            RegionID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["AreaID"];
            AreaID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["BranchID"];
            BranchID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            
            IsConsignorWise = Convert.ToBoolean(Request.QueryString["IsConsignorWise"]);

            From_Date = Request.QueryString["From_Date"];
            To_Date = Request.QueryString["To_Date"];

            StartDate = Request.QueryString["StartDate"];
            EndDate = Request.QueryString["EndDate"];

            Crypt = Request.QueryString["Name"];
            Name = ClassLibraryMVP.Util.DecryptToString(Crypt);

            Crypt = Request.QueryString["Division_ID"];
            Division_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["Client_ID"];
            Client_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["Client_Name"];
            Client_Name = ClassLibraryMVP.Util.DecryptToString(Crypt);


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
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,RegionID),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,AreaID),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,BranchID),
            objDAL.MakeInParams("@IsConsignorWise", SqlDbType.Int,0,IsConsignorWise),
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,Convert.ToDateTime(From_Date)),
            objDAL.MakeInParams("@ToDate", SqlDbType.DateTime,0,Convert.ToDateTime(To_Date)),
            objDAL.MakeInParams("@StartDate", SqlDbType.DateTime,0,Convert.ToDateTime(StartDate)),
            objDAL.MakeInParams("@EndDate", SqlDbType.DateTime,0,Convert.ToDateTime(EndDate)),   
            objDAL.MakeInParams("@Name", SqlDbType.VarChar,100,Name), 
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID), 
            objDAL.MakeInParams("@CallFrom",SqlDbType.Int,0,2), 
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize), 
            objDAL.MakeInParams("@Client_Group_Id",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@MobileNo",SqlDbType.VarChar,20,"")  
        };
         

        objDAL.RunProc("EC_RPT_Clientwise_Monthly_Booking_Summary_GRD", objSqlParam, ref ds);
       

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[2].Rows[0][0].ToString();
        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom,lbl_Error,TotalRecords);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }          
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["ClientName"] = "Total";
        
        dr["TotLR"] = TotLR;
        dr["Tot_Art"] = Tot_Art;
        dr["SubTotal"] = SubTotal;
        dr["TotST"] = TotST;
        dr["TotRoundOff"] = TotRoundOff;
        dr["TotGCAmt"] = TotGCAmt;
        dr["TotDiscount"] = TotDiscount; 
        ds.Tables[0].Rows.Add(dr);

        if (ds.Tables[0].Columns.Contains("ClientName"))
        {
            ds.Tables[0].Columns["ClientName"].ColumnName = "Months";
        }

        ds.Tables[0].Columns.Remove("SRNO");
        ds.Tables[0].Columns.Remove("MONTHID");
        ds.Tables[0].Columns.Remove("MONTHNAME");
        ds.Tables[0].Columns.Remove("YearID");
        ds.Tables[0].Columns.Remove("IsConsignorWise");
        ds.Tables[0].Columns.Remove("StartDate");
        ds.Tables[0].Columns.Remove("EndDate");  

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

        TotLR = Util.String2Int(dr["TotalTotLR"].ToString());
        Tot_Art = Util.String2Int(dr["TotalTot_Art"].ToString());
        SubTotal = Util.String2Int(dr["TotalSubTotal"].ToString());
        TotST = Util.String2Int(dr["TotalTotST"].ToString());
        TotRoundOff = Util.String2Int(dr["TotalTotRoundOff"].ToString());
        TotGCAmt = Util.String2Int(dr["TotalTotGCAmt"].ToString());
        TotDiscount = Util.String2Int(dr["TotalTotDiscount"].ToString()); 
        
    }
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_TotLR, lbl_Tot_Art, lbl_SubTotal;
            System.Web.UI.WebControls.Label lbl_TotST, lbl_TotRoundOff, lbl_TotGCAmt, lbl_TotDiscount; 
 
            lbl_TotLR = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotLR");
            lbl_Tot_Art = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Tot_Art");
            lbl_SubTotal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SubTotal");
            lbl_TotST = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotST");
            lbl_TotRoundOff = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotRoundOff");
            lbl_TotGCAmt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotGCAmt");
            lbl_TotDiscount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotDiscount"); 
            
            lbl_TotLR.Text = TotLR.ToString();
            lbl_Tot_Art.Text = Tot_Art.ToString();
            lbl_SubTotal.Text = SubTotal.ToString();
            lbl_TotST.Text = TotST.ToString();
            lbl_TotRoundOff.Text = TotRoundOff.ToString();
            lbl_TotGCAmt.Text = TotGCAmt.ToString();
            lbl_TotDiscount.Text = TotDiscount.ToString(); 
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int  MONTHID, YearID;//Client_ID,
            LinkButton lnk_ClientName;
            string MONTHNAME;

            string StartDate = DataBinder.Eval(e.Item.DataItem, "StartDate").ToString();
            string EndDate = DataBinder.Eval(e.Item.DataItem, "EndDate").ToString();
            //Client_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Client_ID").ToString());

            MONTHID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "MONTHID").ToString());
             
            YearID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "YearID").ToString());

            lnk_ClientName = (LinkButton)e.Item.FindControl("lnk_ClientName");
            MONTHNAME = lnk_ClientName.Text;
            Name = DataBinder.Eval(e.Item.DataItem, "MONTHNAME").ToString();
            lnk_ClientName.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(RegionID) + "','" + ClassLibraryMVP.Util.EncryptInteger(AreaID) + "','" + ClassLibraryMVP.Util.EncryptInteger(BranchID) + "','" + IsConsignorWise + "','" + From_Date + "','" + To_Date + "','" + StartDate + "','" + EndDate + "','" + ClassLibraryMVP.Util.EncryptString(Name) + "','" + ClassLibraryMVP.Util.EncryptInteger(Division_ID) + "','" + ClassLibraryMVP.Util.EncryptString(Client_Name) + "','" + ClassLibraryMVP.Util.EncryptInteger(MONTHID) + "','" + ClassLibraryMVP.Util.EncryptString(MONTHNAME) + "','" + ClassLibraryMVP.Util.EncryptInteger(YearID) + "')");

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
