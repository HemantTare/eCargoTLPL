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
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Operations_Inward_FrmVehicleFailure : System.Web.UI.Page
{
    #region Declaration
    private DataSet objDS;
    Raj.EC.Common objComm = new Raj.EC.Common();

    public int VehicleID
    {
        get { return WucVehicleSearch1.VehicleID; }
        set
        {
            WucVehicleSearch1.VehicleID = value;
        }
    }

    public string errorMessage
    {
        set { lblErrors.Text = value; }
    }


    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        errorMessage = "";

        if (IsPostBack == false)
        {

            Fill_CrossingReason();
            Fill_CrossingBranch();

        }

        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(GetDetails);

    }



    #endregion

    #region Other Function


    public void GetDetails(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam ={
                                    objDAL.MakeInParams("@VehicleID", SqlDbType.Int, 0, WucVehicleSearch1.VehicleID)};

        objDAL.RunProc("EC_Opr_Vehicle_Failure_Get_Details", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            lbl_LHPONo.Text = objDS.Tables[0].Rows[0][0].ToString();
            lbl_LHPODate.Text = objDS.Tables[0].Rows[0][1].ToString();
            hdn_Main_LHPO_Id.Value = objDS.Tables[0].Rows[0][2].ToString();

            if (objDS.Tables[1].Rows.Count > 0)
            {
                dg_Grid.DataSource = objDS.Tables[1];
                dg_Grid.DataBind();

                Session["MenifestDetails"] = objDS.Tables[1];
            }
        }
    }






    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int StatusId;

            StatusId = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "StatusId").ToString());

            if (StatusId == 1)
            {
                e.Item.BackColor = System.Drawing.Color.Purple;
                e.Item.ForeColor = System.Drawing.Color.White;
                e.Item.Font.Bold = true;
            }
            else
            {
                e.Item.BackColor = System.Drawing.Color.Yellow;
                e.Item.ForeColor = System.Drawing.Color.Black;
                e.Item.Font.Bold = true;
            }

        }
    }


    private void Fill_CrossingReason()
    {
        DAL objDAL = new DAL();

        objDAL.RunProc("EC_Opr_Vehicle_Failure_Reasons", ref objDS);

        ddl_CrossingReason.DataSource = objDS;
        ddl_CrossingReason.DataTextField = "Reason";
        ddl_CrossingReason.DataValueField = "Reason_Id";
        ddl_CrossingReason.DataBind();

        ddl_CrossingReason.Items.Insert(0, new ListItem("Select Reason", "0"));
    }

    private void Fill_CrossingBranch()
    {
        DAL objDAL = new DAL();

        objDAL.RunProc("EC_Opr_Vehicle_Failure_CrossingBranch", ref objDS);

        ddl_CrossingBranch.DataSource = objDS;
        ddl_CrossingBranch.DataTextField = "Branch_Name";
        ddl_CrossingBranch.DataValueField = "Branch_Id";
        ddl_CrossingBranch.DataBind();

        ddl_CrossingBranch.Items.Insert(0, new ListItem("Select Branch", "0"));
    }


    public void ClearVariables()
    {
        objDS = null;

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();
        }
    }

    private Message Save()
    {

        int Reason_Id = 0, Crossing_Branch_Id = 0;
        string Crossing_Branch_Name;

        Reason_Id = Util.String2Int(ddl_CrossingReason.SelectedValue);
        Crossing_Branch_Id = Util.String2Int(ddl_CrossingBranch.SelectedValue);
        Crossing_Branch_Name = ddl_CrossingBranch.SelectedItem.Text;

        DataTable DT = (DataTable)(Session["MenifestDetails"]);
        DT.TableName = "MenifestDetails";
        DataTable DT1 = DT.Copy();

        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);

        string MenifestDetailsXML = ds.GetXml().ToLower();

        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int,0,UserManager.getUserParam().YearCode), 
            objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int,0,VehicleID), 
            objDAL.MakeInParams("@Main_LHPO_Id", SqlDbType.Int,0,Util.String2Int(hdn_Main_LHPO_Id.Value) ), 
            objDAL.MakeInParams("@Actual_Location",SqlDbType.VarChar,50,txt_ActualLocation.Text), 
            objDAL.MakeInParams("@Crossing_Branch_Id", SqlDbType.Int,0,Crossing_Branch_Id), 
            objDAL.MakeInParams("@Crossing_Branch_Name", SqlDbType.VarChar ,50,Crossing_Branch_Name), 
            objDAL.MakeInParams("@Crossing_Date", SqlDbType.Date,0,Dtp_CrossingDate.SelectedDate), 
            objDAL.MakeInParams("@Crossing_Reason_Id", SqlDbType.Int,0,Reason_Id), 
            objDAL.MakeInParams("@Remark",SqlDbType.VarChar,1000,txt_Remarks.Text),
            objDAL.MakeInParams("@MemoDetailsXML",SqlDbType.Xml,0,MenifestDetailsXML),
            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };

        objDAL.RunProc("dbo.EC_Opr_Vehicle_Failure_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            lblErrors.Text = "Saved SuccessFully";
            ClearVariables();
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString("Saved Succesfully"));
        }
        else
        {
            lblErrors.Text = objMessage.message;
        }
        return objMessage;
    }


    public bool validateUI()
    {
        bool ATS;
        ATS = false;

        if (VehicleID <= 0)
        {
            lblErrors.Text = "Please Select Vehicle";
            WucVehicleSearch1.Focus();
        }
        else if (txt_ActualLocation.Text.Trim().Length <= 0)
        {
            lblErrors.Text = "Please Enter Actual Location";
            txt_ActualLocation.Focus();
        }
        else if (Util.String2Int(ddl_CrossingBranch.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select Crossing Branch";
            ddl_CrossingBranch.Focus();
        }

        else if (Util.String2Int(ddl_CrossingReason.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select Valid Reason";
            ddl_CrossingReason.Focus();
        }

        else if (Dtp_CrossingDate.SelectedDate < UserManager.getUserParam().StartDate || Dtp_CrossingDate.SelectedDate > UserManager.getUserParam().EndDate)
        {
            errorMessage = "Crossing Date Must Be of Current Financial Year.";
        }
        else if (Dtp_CrossingDate.SelectedDate < Convert.ToDateTime(lbl_LHPODate.Text))
        {
            errorMessage = "Crossing Date Must Be of Greater Than Trip Memo Date";
        }
        else if (grid_validation() == false)
        {
        }
        else if (txt_Remarks.Text.Trim().Length <= 10)
        {
            lblErrors.Text = "Please Enter Remarks";
            txt_Remarks.Focus();
        }

        else
        {
            ATS = true;
        }

        return ATS;
    }


    public bool grid_validation()
    {
        int i;
        bool ATS = true;

        DataTable DT = (DataTable)(Session["MenifestDetails"]);
        Label Trip_Memo_Date;

        if (DT.Rows.Count > 0)
        {

            for (i = 0; i <= dg_Grid.Items.Count - 1; i++)
            {
                Trip_Memo_Date = (Label)dg_Grid.Items[i].FindControl("lbl_Trip_Memo_Date");


                if (Convert.ToDateTime(Trip_Memo_Date.Text) > Dtp_CrossingDate.SelectedDate)
                {
                    errorMessage = "Invalid Crossing Date. Crossing Date Cannot Be Less Than Trip Memo Date";
                    ATS = false;
                    break;
                }
                else
                {
                    ATS = true;
                }
            }
        }
        return ATS;
    }


    #endregion
}
