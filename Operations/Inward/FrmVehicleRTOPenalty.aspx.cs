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
using ClassLibraryMVP;
using System.Data.SqlClient;
using Raj.EC;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

public partial class Operations_Inward_FrmVehicleRTOPenalty : ClassLibraryMVP.UI.Page
{
    TextBox txtChallanNo, txtPlace, txtOffence, txtAmount, dtp_PenaltyDate;
    String scripts = "<script type='text/javascript' language='javascript'> initializeCalender();</script>";
    String editscripts = "<script type='text/javascript' language='javascript'> initializeCalenderonedit();</script>";
    TimePicker PenaltyTime;
    bool ATS = false;

    #region properties

    private int VehicleID
    {
        set
        {
            DDLVehicle.VehicleID = value;
        }

        get
        {
            return DDLVehicle.VehicleID;
        }
    }

    public DataTable Session_RTOPenaltyDetails
    {
        get { return StateManager.GetState<DataTable>("RTOPenaltyDetails"); }
        set { StateManager.SaveState("RTOPenaltyDetails", value); }
    } 

    private string ErrorMessage
    {
        set { lblErrors.Text = value.ToString(); }
    }
    #endregion

    #region grid operation

    protected void dgGrid_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgGrid.EditItemIndex = -1;
        dgGrid.ShowFooter = true;
        BindGrid();
        ScriptManager.RegisterStartupScript(Page, typeof(System.String), "ss", scripts, false);
    }
    protected void dgGrid_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        DataTable DT = (DataTable)(Session["RTOPenaltyDetails"]);
        DataRow DR = DT.Rows[e.Item.ItemIndex];
        DT.Rows.Remove(DR);
        Session["RTOPenaltyDetails"] = DT;
        BindGrid();
        ScriptManager.RegisterStartupScript(Page, typeof(System.String), "ss", scripts, false);
    }
    protected void dgGrid_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dgGrid.EditItemIndex = e.Item.ItemIndex;
        dgGrid.ShowFooter = false;
        BindGrid();        
    }
    protected void dgGrid_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            Insert_Update_Dataset(source, e);
            if (ATS)
            {
                BindGrid();
                dgGrid.EditItemIndex = -1;
                dgGrid.ShowFooter = true;
            }

            ScriptManager.RegisterStartupScript(Page, typeof(System.String), "ss", scripts, false);
        }
    }
    protected void dgGrid_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        Insert_Update_Dataset(source, e);

        if (ATS == true)
        {
            dgGrid.EditItemIndex = -1;
            dgGrid.ShowFooter = true;

            BindGrid();
        }
        ScriptManager.RegisterStartupScript(Page, typeof(System.String), "ss", scripts, false);
    }
    protected void dgGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {

            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                dtp_PenaltyDate = (TextBox)(e.Item.FindControl("dtp_PenaltyDate"));                
                PenaltyTime = (TimePicker)(e.Item.FindControl("PenaltyTime"));
                txtChallanNo = (TextBox)(e.Item.FindControl("txtChallanNo"));
                txtPlace = (TextBox)(e.Item.FindControl("txtPlace"));
                txtOffence = (TextBox)(e.Item.FindControl("txtOffence"));
                txtAmount = (TextBox)(e.Item.FindControl("txtAmount"));
                PenaltyTime.setFormat("24");
            }
            
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                dtp_PenaltyDate = (TextBox)(e.Item.FindControl("dtp_PenaltyDate"));  
                DataTable DT = (DataTable)(Session["RTOPenaltyDetails"]);
                DataRow DR = DT.Rows[e.Item.ItemIndex];

                dtp_PenaltyDate.Text = DR["Penalty_Date"].ToString();
                PenaltyTime.setTime(DR["Penalty_Time"].ToString());
                txtChallanNo.Text = DR["ChallanNo"].ToString();
                txtPlace.Text = DR["Place"].ToString();
                txtOffence.Text = DR["Offence"].ToString();
                txtAmount.Text = DR["Amount"].ToString();
                ScriptManager.RegisterStartupScript(Page, typeof(System.String), "ass", "<script type='text/javascript' language='javascript'> initializeCalenderonedit('" + DR["Penalty_Date"].ToString() + "');</script>", false);
            }
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        dtp_PenaltyDate = (TextBox)(e.Item.FindControl("dtp_PenaltyDate"));
        PenaltyTime = (TimePicker)(e.Item.FindControl("PenaltyTime"));
        txtChallanNo = (TextBox)(e.Item.FindControl("txtChallanNo"));
        txtPlace = (TextBox)(e.Item.FindControl("txtPlace"));
        txtOffence = (TextBox)(e.Item.FindControl("txtOffence"));
        txtAmount = (TextBox)(e.Item.FindControl("txtAmount"));

        DataTable DT = (DataTable)(Session["RTOPenaltyDetails"]);
        DataRow DR = null;

        if (e.CommandName == "Add")
        {
            DR = DT.NewRow();

        }
        else if (e.CommandName == "Update")
        {
            DR = DT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update(e.Item.ItemIndex) == true)
        {
            DR["Penalty_Date"] = String.Format("{0:dd MM yyyy}", dtp_PenaltyDate.Text);
            DR["Penalty_Time"] = PenaltyTime.getTime();
            DR["ChallanNo"] = txtChallanNo.Text;
            DR["Place"] = txtPlace.Text;
            DR["Offence"] = txtOffence.Text;
            DR["Amount"] = txtAmount.Text;

            if (e.CommandName == "Add")
            {
                DT.Rows.Add(DR);
            }
            Session["RTOPenaltyDetails"] = DT;
        }
    }

    private bool Allow_To_Add_Update(int index)
    {
        if (txtOffence.Text.Trim() == string.Empty)
        {
            lblErrors.Text = "Please Enter Offence";
            scmdelarea.SetFocus(txtOffence);
        }
        else if (txtChallanNo.Text.Trim() == string.Empty)
        {
            lblErrors.Text = "Please Enter Challan No.";
            scmdelarea.SetFocus(txtChallanNo);
        }
        else if (Util.String2Int(txtAmount.Text) == 0 || txtAmount.Text.Trim() == string.Empty)
        {
            lblErrors.Text = "Please Enter Penalty Amount";
            scmdelarea.SetFocus(txtAmount);
        }
        else
            ATS = true;
        return ATS;
    }
    #endregion

    private void BindGrid()
    {
        dgGrid.DataSource = (DataTable)(Session["RTOPenaltyDetails"]);
        dgGrid.DataBind();

        DDLVehicle.Focus();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            int id = Util.DecryptToInt(Request.QueryString["Id"]);
            Session["RTOPenaltyDetails"] = null;

            ReadValues();
            BindGrid();

        }
        ScriptManager.RegisterStartupScript(Page, typeof(System.String), "ss", editscripts, false);
    }

    #region subroutine
    private bool validateUI()
    {
        bool ATS = false;

        if (VehicleID <= 0)
        {
            lblErrors.Text = "Please Select Vehicle";
            scmdelarea.SetFocus(DDLVehicle);
        }
        else if (Session_RTOPenaltyDetails.Rows.Count<=0)
        {
            lblErrors.Text = "Please Enter Penalty Details";
            scmdelarea.SetFocus(dgGrid);
        }
        else
        {
            ATS = true;
        }
        return ATS;
    }

    private void ReadValues()
    {

        DataTable dtFirst = new DataTable();
        dtFirst.Columns.Add("Penalty_Date");
        dtFirst.Columns.Add("Penalty_Time");
        dtFirst.Columns.Add("ChallanNo");
        dtFirst.Columns.Add("Place");
        dtFirst.Columns.Add("Offence");
        dtFirst.Columns.Add("Amount");


        Session["RTOPenaltyDetails"] = dtFirst;
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();
        }
    }

    private Message Save()
    {
        DataTable DT1 = (DataTable)(Session["RTOPenaltyDetails"]);
        DT1.TableName = "RTOPenaltyDetails";
        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);

        string RTOPenaltyDetailsXML = ds.GetXml().ToLower();



        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),  
            objDAL.MakeInParams("@Details_ID",SqlDbType.Int,0,0), 
            objDAL.MakeInParams("@VehicleID",SqlDbType.Int,0,VehicleID), 
            objDAL.MakeInParams("@DetailsXML",SqlDbType.Xml,0,RTOPenaltyDetailsXML),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId),
            objDAL.MakeInParams("@Penalty_Date",SqlDbType.DateTime,0,System.DateTime.Now),
            objDAL.MakeInParams("@Penalty_Time",SqlDbType.VarChar,10,""),
            objDAL.MakeInParams("@ChallanNo",SqlDbType.VarChar,25,""),
            objDAL.MakeInParams("@Place",SqlDbType.VarChar,50,""),
            objDAL.MakeInParams("@Offence",SqlDbType.VarChar,250,""),
            objDAL.MakeInParams("@Amount",SqlDbType.Decimal,0,0)
        };

        objDAL.RunProc("dbo.EF_Opr_Vehicle_Penalty_Details_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {

            ClearVariables();

            string _Msg = "Saved SuccessFully";

            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else
        {
            lblErrors.Text = objMessage.message;
        }

        return objMessage;
    }

    public void ClearVariables()
    {

        Session_RTOPenaltyDetails = null;
        dgGrid.DataSource = null;
        dgGrid.DataBind();

    }
}
