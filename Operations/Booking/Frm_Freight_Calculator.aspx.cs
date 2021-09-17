using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;


public partial class Operations_Booking_Frm_Freight_Calculator : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    Common objCommon = new Common();

    private string FromBranchID
    {
        set
        {

            ddlFromBranch.SelectedValue = value;
        }
        get
        {
            return ddlFromBranch.SelectedValue;
        }
    }

    private string ToBranchID
    {
        set
        {

            ddlToBranch.SelectedValue = value;
        }
        get
        {
            return ddlToBranch.SelectedValue;
        }
    }


    private string DeliveryAreaID
    {
        set
        {

            ddlDeliveryArea.SelectedValue = value;
        }
        get
        {
            //return ddlDeliveryArea.SelectedValue;
            return ddlDeliveryArea.SelectedValue == string.Empty ? "0" : ddlDeliveryArea.SelectedValue;
        }

    }

    public decimal FreightRate
    {
        set
        {
            txtFreightRate.Text = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return txtFreightRate.Text == string.Empty ? 0 : Util.String2Decimal(txtFreightRate.Text); }
    }

    public decimal HamaliPerKg
    {
        set
        {
            txtHamaliPerKg.Text = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return txtHamaliPerKg.Text == string.Empty ? 0 : Util.String2Decimal(txtHamaliPerKg.Text); }
    }

    public decimal StatisticCharge
    {
        set
        {
            txtStatisticCharge.Text = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return txtStatisticCharge.Text == string.Empty ? 0 : Util.String2Decimal(txtStatisticCharge.Text); }
    }

    public decimal FovPercent
    {
        set
        {
            txtFovPercent.Text = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return txtFovPercent.Text == string.Empty ? 0 : Util.String2Decimal(txtFovPercent.Text); }
    }

    public decimal AOCPercent
    {
        set
        {
            txtAOCPercent.Text = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return txtAOCPercent.Text == string.Empty ? 0 : Util.String2Decimal(txtAOCPercent.Text); }
    }

    public decimal NoOfParcls
    {
        set
        {
            txtNoOfParcls.Text = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return txtNoOfParcls.Text == string.Empty ? 0 : Util.String2Decimal(txtNoOfParcls.Text); }
    }

    private string SizeID
    {
        set
        {

            ddlSize.SelectedValue = value;
        }
        get
        {
            return ddlSize.SelectedValue == string.Empty ? "0" : ddlSize.SelectedValue;
        }

    }

    public decimal InvoiceValue
    {
        set
        {
            txtInvoiceValue.Text = Util.Decimal2String(Math.Round(value, 2));
        }
        get { return txtInvoiceValue.Text == string.Empty ? 0 : Util.String2Decimal(txtInvoiceValue.Text); }
    }

    #endregion



    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "FreightCalculator";

        if (!IsPostBack)
        {

            FillFromAndToBranch();
            FillDeliveryArea();
            FillSize();

            BranchRateParameters("BkgBr");
            BranchRateParameters("DlyBr");

            ScriptManager.SetFocus(ddlFromBranch);
        }

    }

    private void FillSize()
    {
        string query = " Select SizeID,SizeName from EC_Master_Size Where IsActive = 1 order by SizeID ";
        DataSet ds = new DataSet();
        ds = objCommon.EC_Common_Pass_Query(query);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlSize.DataSource = ds;
            ddlSize.DataValueField = "SizeID";
            ddlSize.DataTextField = "SizeName";
            ddlSize.DataBind();


        }
        else
        {
            ddlSize.Items.Clear();
        }
    }

    private void FillFromAndToBranch()
    {
        string query = "Select Branch_Id,Branch_Name from ec_master_branch where Is_Active=1 order by Branch_Name";
        DataSet ds = new DataSet();
        ds = objCommon.EC_Common_Pass_Query(query);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlFromBranch.DataSource = ds;
            ddlFromBranch.DataValueField = "Branch_Id";
            ddlFromBranch.DataTextField = "Branch_Name";
            ddlFromBranch.DataBind();

            ddlToBranch.DataSource = ds;
            ddlToBranch.DataValueField = "Branch_Id";
            ddlToBranch.DataTextField = "Branch_Name";
            ddlToBranch.DataBind();

        }
        else
        {
            ddlFromBranch.Items.Clear();
            ddlToBranch.Items.Clear();
        }
    }

    protected void ddlFromBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        BranchRateParameters("BkgBr");
    }

    protected void ddlToBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        BranchRateParameters("DlyBr");
        FillDeliveryArea();
        ScriptManager.SetFocus(ddlDeliveryArea);
    }

    private void FillDeliveryArea()
    {
        string query = "Select 0 as DeliveryAreaID,' All' as DeliveryAreaName UNION Select DeliveryAreaID,DeliveryAreaName from ec_master_deliveryarea where branchid = " + ddlToBranch.SelectedValue + " order by DeliveryAreaName";
        DataSet ds = new DataSet();
        ds = objCommon.EC_Common_Pass_Query(query);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlDeliveryArea.DataSource = ds;
            ddlDeliveryArea.DataValueField = "DeliveryAreaID";
            ddlDeliveryArea.DataTextField = "DeliveryAreaName";
            ddlDeliveryArea.DataBind();
            ScriptManager.SetFocus(ddlDeliveryArea);
        }
        else
        {
            ddlDeliveryArea.Items.Clear();
            ddlToBranch.Items.Clear();
        }
    }


    private void BranchRateParameters(String FromBranchORToBranch)
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@FromBranchId", SqlDbType.Int, 0, FromBranchID),
        objDAL.MakeInParams("@ToBranchId", SqlDbType.Int, 0, ToBranchID)};
        objDAL.RunProc("dbo.EC_Opr_Freight_Calculator_Branch_Parameters", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];

            FreightRate = Convert.ToDecimal(objDR["FreightRate"].ToString());

            if (FromBranchORToBranch == "BkgBr")
            {
                HamaliPerKg = Convert.ToDecimal(objDR["HamaliPerKg"].ToString());
                StatisticCharge = Convert.ToDecimal(objDR["BiltyCharges"].ToString());
                FovPercent = Convert.ToDecimal(objDR["FOVPercent"].ToString());
                AOCPercent = Convert.ToDecimal(objDR["AOCPercent"].ToString());
            }
        }
    }

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {


        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

        }
    }



    #endregion

    #region Other Function

    protected void btn_view_Click(object sender, EventArgs e)
    {

        dg_Grid.CurrentPageIndex = 0;
        lbl_Error.Text = "";
        BindGrid("form", e);

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



        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@FromBranchId", SqlDbType.Int, 0,FromBranchID),
            objDAL.MakeInParams("@ToBranchId", SqlDbType.Int, 0,ToBranchID),
            objDAL.MakeInParams("@DlyAreaId", SqlDbType.Int, 0,DeliveryAreaID),
            objDAL.MakeInParams("@NoOfParcls",SqlDbType.Int,0, NoOfParcls),
            objDAL.MakeInParams("@FreightRate",SqlDbType.Decimal ,0,FreightRate ),
            objDAL.MakeInParams("@HamaliPerKg",SqlDbType.Decimal ,0,HamaliPerKg),
            objDAL.MakeInParams("@BiltyCharges",SqlDbType.Decimal ,0,StatisticCharge),
            objDAL.MakeInParams("@FOVPercent",SqlDbType.Decimal ,0,FovPercent),
            objDAL.MakeInParams("@AOCPercent",SqlDbType.Decimal ,0,AOCPercent),
            objDAL.MakeInParams("@InvoiceValue",SqlDbType.Decimal ,0,InvoiceValue),
            objDAL.MakeInParams("@SizeID", SqlDbType.Int, 0,SizeID),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("EC_Opr_Freight_Calculator", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());

        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);


        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();

        ds.Tables[0].Columns.Remove("DlyAreaId");

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];

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
    #endregion


}
