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

public partial class Reports_Sales_Billing_FrmTopWalkinClientDetailsMonthly : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;



    private int Branch_Id
    {
        get
        {
            return Convert.ToInt32(ViewState["_Branch_Id"]);
        }
        set
        {
            ViewState["_Branch_Id"] = value;
        }
    }
    
    private int DeliveryAreaId
    {
        get
        {
            return Convert.ToInt32(ViewState["_DeliveryAreaId"]);
        }
        set
        {
            ViewState["_DeliveryAreaId"] = value;
        }
    }

    private int Client_ID
    {
        get
        {
            return Convert.ToInt32(ViewState["_Client_ID"]);
        }
        set
        {
            ViewState["_Client_ID"] = value;
        }
    }


    private string Mobile_No
    {
        get
        {
            return Convert.ToString(ViewState["_Mobile_No"]);
        }
        set
        {
            ViewState["_Mobile_No"] = value;
        }
    }

    private string Phone1
    {
        get
        {
            return Convert.ToString(ViewState["_Phone1"]);
        }
        set
        {
            ViewState["_Phone1"] = value;
        }
    }


    private string Phone2
    {
        get
        {
            return Convert.ToString(ViewState["_Phone2"]);
        }
        set
        {
            ViewState["_Phone2"] = value;
        }
    }

    private DateTime From_Date
    {
        get
        {
            return Convert.ToDateTime(ViewState["_From_Date"]);
        }
        set
        {
            ViewState["_From_Date"] = value;
        }
    }

    private DateTime To_Date
    {
        get
        {
            return Convert.ToDateTime(ViewState["_To_Date"]);
        }
        set
        {
            ViewState["_To_Date"] = value;
        }
    }



    #endregion

    #region EventClick

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "TopClientMonthlySummary";

        if (IsPostBack == false)
        {

            Branch_Id = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["Branch_Id"]);
            DeliveryAreaId = Convert.ToInt32(Request.QueryString["DeliveryAreaId"]);

            Client_ID = Convert.ToInt32(Request.QueryString["Client_ID"]);
            Mobile_No = Request.QueryString["Mobile_No"];
            Phone1 = Request.QueryString["Phone1"];
            Phone2 = Request.QueryString["Phone2"];

            From_Date = Convert.ToDateTime(Request.QueryString["From_Date"]);
            To_Date = Convert.ToDateTime(Request.QueryString["To_Date"]);


            if (DeliveryAreaId > 0)
            {
                lbl_Branch.Text = Request.QueryString["Branch_Name"] + "   (" + Request.QueryString["DeliveryAreaName"] + ")";
            }
            else 
            {
                lbl_Branch.Text = Request.QueryString["Branch_Name"];
            }

            lbl_Client.Text = Request.QueryString["Client_Name"];
            lbl_TotalLR.Text = Request.QueryString["TotalLR"];
              

            Common objcommon = new Common();
            BindGrid("form_and_pageload", e);
        }

    }

    private void ClearGrid(object sender, EventArgs e)
    {
        dg_Grid.DataSource = null;
        dg_Grid.DataBind();
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }


        if (DeliveryAreaId == 0)
        {
            SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
                objDAL.MakeInParams("@To_Date", SqlDbType.DateTime,0,To_Date),
                objDAL.MakeInParams("@BranchID", SqlDbType.Int,0,Branch_Id),
                objDAL.MakeInParams("@MobileNo", SqlDbType.VarChar,20,Mobile_No),
                objDAL.MakeInParams("@Phone1", SqlDbType.VarChar,20,Phone1),
                objDAL.MakeInParams("@Phone2", SqlDbType.VarChar,20,Phone2),
                objDAL.MakeInParams("@ClientId", SqlDbType.Int,0,Client_ID) 
            };
            objDAL.RunProc("EC_RPT_Top_Client_Details_BookingBranchWise_MonthlySummary", objSqlParam, ref ds);
        }
        else
        {
            SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
                objDAL.MakeInParams("@To_Date", SqlDbType.DateTime,0,To_Date),
                objDAL.MakeInParams("@DeliveryAreaId", SqlDbType.Int,0,DeliveryAreaId),
                objDAL.MakeInParams("@MobileNo", SqlDbType.VarChar,20,Mobile_No),
                objDAL.MakeInParams("@Phone1", SqlDbType.VarChar,20,Phone1),
                objDAL.MakeInParams("@Phone2", SqlDbType.VarChar,20,Phone2),
                objDAL.MakeInParams("@ClientId", SqlDbType.Int,0,Client_ID) 
            };
            objDAL.RunProc("EC_RPT_Top_Client_Details_DeliveryAreaWise_MonthlySummary", objSqlParam, ref ds);        
        
        }
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //if (CallFrom == "form_and_pageload") return;


                Common objcommon = new Common();
                objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Errors);


                if (CallFrom == "exporttoexcelusercontrol")
                {
                    PrepareDTForExportToExcel();
                }
            }
        }
        else
        {
            errorMessage = "No Record Found";
            lbl_Errors.Visible = true;
            return;
        }
    }

    private void PrepareDTForExportToExcel()
    {

        //ds.Tables[0].Columns.Remove("Sr No.");
        //ds.Tables[0].Columns.Remove("GC_ID"); 

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) //  == DataControlRowType.DataRow)
        {
            //HyperLink link = new HyperLink();
            //LinkButton lnk_DlyBranch = new LinkButton();
            //lnk_DlyBranch = (LinkButton)e.Item.FindControl("lnk_DlyBranch");
            //link.Text = lnk_DlyBranch.Text;
 
        }

    }


    #endregion



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
