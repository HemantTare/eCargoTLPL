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

public partial class Master_Location_FrmDeliveryArea : ClassLibraryMVP.UI.Page
{
    TextBox txtDescription, txtUpToKg, txtAmountPerLR, txtAmountPerLRTBP, txtTempoFreight;
    bool ATS = false;
    #region properties

    private string DeliveryAreaID
    {
        set
        {

            if (value == null)
                hdnKeyID.Value = "0";
            else
                hdnKeyID.Value = value.ToString();
        }
        get
        {
            return hdnKeyID.Value;
        }
    }
    private string DeliveryAreaCode
    {
        set
        {
            txtDeliveryAreaCode.Text = value.ToString();
        }
        get
        {
            return txtDeliveryAreaCode.Text.Trim();
        }
    }
    private string DeliveryAreaName
    {
        set
        {
            txtDeliveryAreaName.Text = value.ToString();
        }
        get
        {
            return txtDeliveryAreaName.Text.Trim();
        }
    }
    private string BranchID
    {
        get
        {
            return DDLBranch.SelectedValue;
        }
    }
    private string DistanceFromBranch
    {
        set { txtDistanceFromBranch.Text = value.ToString(); }
        get
        {
            return txtDistanceFromBranch.Text;
        }
    }

    private bool IsODALocation
    {
        set { Chk_IsODALocation.Checked = value; }
        get { return Chk_IsODALocation.Checked; }
    }

    private int DeliveryChargeType
    {
        set { ddl_DeliveryCharge.SelectedValue = value.ToString(); }
        get
        {
            return Util.String2Int(ddl_DeliveryCharge.SelectedValue);
        }
    }
    private decimal RsPerKG
    {
        set { txtPerParcel.Text = value.ToString(); }
        get
        {
            return txtPerParcel.Text == string.Empty ? 0 : Util.String2Decimal(txtPerParcel.Text);
        }
    }
    private decimal RsPerKGToBePaid
    {
        set { txtPerParcelToBePaid.Text = value.ToString(); }
        get
        {
            return txtPerParcelToBePaid.Text == string.Empty ? 0 : Util.String2Decimal(txtPerParcelToBePaid.Text);
        }
    }

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
    }
    protected void dgGrid_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        DataTable DT = (DataTable)(Session["DlyChargeDetails"]);
        DataRow DR = DT.Rows[e.Item.ItemIndex];
        DT.Rows.Remove(DR);
        Session["DlyChargeDetails"] = DT;
        BindGrid();
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
    }
    protected void dgGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                txtDescription = (TextBox)(e.Item.FindControl("txtDescriptionAdd"));
                txtUpToKg = (TextBox)(e.Item.FindControl("txtUpToKgAdd"));
                txtAmountPerLR = (TextBox)(e.Item.FindControl("txtAmountPerLRAdd"));
                txtAmountPerLRTBP = (TextBox)(e.Item.FindControl("txtAmountPerLRTBPAdd"));
                txtTempoFreight = (TextBox)(e.Item.FindControl("txtAddOnTempoFreightAdd"));
            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {
                txtDescription = (TextBox)(e.Item.FindControl("txtDescriptionEdit"));
                txtUpToKg = (TextBox)(e.Item.FindControl("txtUpToKgEdit"));
                txtAmountPerLR = (TextBox)(e.Item.FindControl("txtAmountPerLREdit"));
                txtAmountPerLRTBP = (TextBox)(e.Item.FindControl("txtAmountPerLRTBPEdit"));
                txtTempoFreight = (TextBox)(e.Item.FindControl("txtAddOnTempoFreightEdit"));
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                DataTable DT = (DataTable)(Session["DlyChargeDetails"]);
                DataRow DR = DT.Rows[e.Item.ItemIndex];

                txtDescription.Text = DR["Description"].ToString();
                txtUpToKg.Text = DR["UpToKg"].ToString();
                txtAmountPerLR.Text = DR["AmountPerLR"].ToString();
                txtAmountPerLRTBP.Text = DR["AmountPerLRToBePaid"].ToString();
                txtTempoFreight.Text = DR["AddOnTempoFreight"].ToString();
            }
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        DataTable DT = (DataTable)(Session["DlyChargeDetails"]);
        DataRow DR = null;
        if (e.CommandName == "Add")
        {
            txtDescription = (TextBox)(e.Item.FindControl("txtDescriptionAdd"));
            txtUpToKg = (TextBox)(e.Item.FindControl("txtUpToKgAdd"));
            txtAmountPerLR = (TextBox)(e.Item.FindControl("txtAmountPerLRAdd"));
            txtAmountPerLRTBP = (TextBox)(e.Item.FindControl("txtAmountPerLRTBPAdd"));
            txtTempoFreight = (TextBox)(e.Item.FindControl("txtAddOnTempoFreightAdd"));
            
            DR = DT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            txtDescription = (TextBox)(e.Item.FindControl("txtDescriptionEdit"));
            txtUpToKg = (TextBox)(e.Item.FindControl("txtUpToKgEdit"));
            txtAmountPerLR = (TextBox)(e.Item.FindControl("txtAmountPerLREdit"));
            txtAmountPerLRTBP = (TextBox)(e.Item.FindControl("txtAmountPerLRTBPEdit"));
            txtTempoFreight = (TextBox)(e.Item.FindControl("txtAddOnTempoFreightEdit"));

            DR = DT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update(e.Item.ItemIndex) == true)
        {
            DR["Description"] = txtDescription.Text;
            DR["UpToKg"] = txtUpToKg.Text == string.Empty ? "0" : txtUpToKg.Text;
            DR["AmountPerLR"] = txtAmountPerLR.Text == string.Empty ? "0" : txtAmountPerLR.Text;
            DR["AmountPerLRToBePaid"] = txtAmountPerLRTBP.Text == string.Empty ? "0" : txtAmountPerLRTBP.Text;
            DR["AddOnTempoFreight"] = txtTempoFreight.Text == string.Empty ? "0" : txtTempoFreight.Text;

            if (e.CommandName == "Add") { DT.Rows.Add(DR); }
            Session["DlyChargeDetails"] = DT;
        }
    }

    private bool Allow_To_Add_Update(int index)
    {
        int UpToKg = Util.String2Int(txtUpToKg.Text);
        if (txtDescription.Text.Trim() == string.Empty)
        {
            lblErrors.Text = "Please enter description";
            scmdelarea.SetFocus(txtDescription);
        }
        else if (UpToKg <= 0)
        {
            lblErrors.Text = "Please enter above KG greater than 0";
            scmdelarea.SetFocus(txtUpToKg);
        }
        else if (Util.String2Decimal(txtAmountPerLR.Text) <= 0)
        {
            lblErrors.Text = "Please enter Amount Per LR greater than 0";
            scmdelarea.SetFocus(txtAmountPerLR);
        }
        else
            ATS = true;

        if (ATS)
        {
            DataTable DT = (DataTable)(Session["DlyChargeDetails"]);
            int maxvalue = DT.Rows.Count > 0 ? Convert.ToInt32(DT.Compute("max([uptokg])", string.Empty)) : 0;
            int editminvalue = 0;
            int editmaxvalue = 0;
            int maxindex = DT.Rows.Count - 1;
            if (index == 0)
            {
                editminvalue = 0;
                editmaxvalue = Convert.ToInt32(DT.Rows[index + 1][1]);
            }
            else if (index >= 1 && index < maxindex)
            {
                //int DR1 = Convert.ToInt32(DT.Rows[index][1]);
                editminvalue = Convert.ToInt32(DT.Rows[index - 1][1]);
                editmaxvalue = Convert.ToInt32(DT.Rows[index + 1][1]);
            }
            else if (index > 0 && index == maxindex)
            {
                editminvalue = Convert.ToInt32(DT.Rows[index - 1][1]);
            }

            if (index < 0 && UpToKg <= maxvalue)
            {
                lblErrors.Text = "Please enter above KG greater than " + maxvalue;
                scmdelarea.SetFocus(txtUpToKg);
                ATS = false;
            }
            else if ((index >= 0 && index < maxindex) && (UpToKg <= editminvalue || UpToKg >= editmaxvalue))
            {
                lblErrors.Text = "Please enter above KG value greater than " + editminvalue + " and less than " + editmaxvalue;
                scmdelarea.SetFocus(txtUpToKg);
                ATS = false;
            }
            else if (index == maxindex && UpToKg <= editminvalue)
            {
                lblErrors.Text = "Please enter above KG value greater than " + editminvalue;
                scmdelarea.SetFocus(txtUpToKg);
                ATS = false;
            }
        }
        return ATS;
    }
    #endregion

    private void BindGrid()
    {
        dgGrid.DataSource = (DataTable)(Session["DlyChargeDetails"]);
        dgGrid.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
       
        if (!IsPostBack)
        {
            txtDeliveryAreaCode.Focus();
            DeliveryAreaID = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));
            int id = Util.DecryptToInt(Request.QueryString["Id"]);
            Session["DlyChargeDetails"] = null;
            //if (id > 0)
            //{
            //    txtDeliveryAreaCode.Enabled = false;
            //}
            
            ReadValues();
            BindGrid();
        }
    }

    #region subroutine
    private bool validateUI()
    {
        bool ATS = false;

        if (txtDeliveryAreaCode.Text.Trim() == "")
        {
            ErrorMessage = "Please Enter Delivery Area Code";
            scmdelarea.SetFocus(txtDeliveryAreaCode);
        }
        else if (txtDeliveryAreaName.Text.Trim() == "")
        {
            ErrorMessage = "Please Enter Delivery Area Name";
            scmdelarea.SetFocus(txtDeliveryAreaName);
        }
        else if (Util.String2Int(BranchID) <= 0)
        {
            ErrorMessage = "Please Select Branch";
            TextBox txtBranch = (TextBox)DDLBranch.FindControl("txtBoxDDLBranch");
            scmdelarea.SetFocus(txtBranch);
        }
        else if (Util.String2Int(DistanceFromBranch) <= 0)
        {
            ErrorMessage = "Distance From Branch Field Must Be Greater Than Zero";
            scmdelarea.SetFocus(txtDistanceFromBranch);
        }
        else
            ATS = true;

        return ATS;
    }
    private Message Save()
    {
        DataTable DT = (DataTable)(Session["DlyChargeDetails"]);
        DT.TableName = "dlychargedetails";
        DataTable DT1 = DT.Copy();

        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);

        string dlyChargeDetailsXML = ds.GetXml().ToLower();

        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { 
        objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
        objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
        objDAL.MakeOutParams("@KeyID", SqlDbType.Int, 0), 
        objDAL.MakeInParams("@DeliveryAreaID", SqlDbType.Int,0, Util.String2Int(DeliveryAreaID)), 
        objDAL.MakeInParams("@DeliveryAreaCode", SqlDbType.VarChar, 4,DeliveryAreaCode), 
        objDAL.MakeInParams("@DeliveryAreaName", SqlDbType.VarChar, 50,DeliveryAreaName), 
        objDAL.MakeInParams("@BranchID",SqlDbType.Int,0, BranchID),
        objDAL.MakeInParams("@DistanceFromBranch",SqlDbType.Int,0, DistanceFromBranch),
        objDAL.MakeInParams("@IsODA",SqlDbType.Bit,0, IsODALocation),
        objDAL.MakeInParams("@DlyChargeType",SqlDbType.Int,0, DeliveryChargeType),
        objDAL.MakeInParams("@RsPerKG",SqlDbType.Decimal,0, RsPerKG),
        objDAL.MakeInParams("@RsPerKGToBePaid",SqlDbType.Decimal,0, RsPerKGToBePaid),
        objDAL.MakeInParams("@DlyChargeDetailsXML",SqlDbType.Xml,0,dlyChargeDetailsXML),
        objDAL.MakeInParams("@Vehicle_Id",SqlDbType.Int,0, VehicleID),
        objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("[dbo].[ec_master_deliveryAreaSave]", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            ErrorMessage = "Saved SuccessFully";
            DeliveryAreaID = Convert.ToString(objSqlParam[2].Value);
            string _Msg;
            _Msg = "Saved SuccessFully";
            int MenuItemId = Common.GetMenuItemId();
            string Mode = HttpContext.Current.Request.QueryString["Mode"];
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Master/Location/FrmdeliveryArea.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
        }
        else if (objMessage.messageID == 2627)
        {
            ErrorMessage = "Duplicate Delivery Area Name";
        }
        else
        {
            ErrorMessage = objMessage.message;
        }
        return objMessage;
    }
    private void ReadValues()
    {
        //if (Util.String2Int(DeliveryAreaID) > 0)
        //{
            DAL objDAL = new DAL();
            DataSet ds = new DataSet();

            SqlParameter[] objSqlParam = {
       objDAL.MakeInParams("@DeliveryAreaID", SqlDbType.Int,0, Util.String2Int(DeliveryAreaID))};

            objDAL.RunProc("[dbo].[EC_master_DeliveryAreaReadValues]", objSqlParam, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = ds.Tables[0].Rows[0];

                DeliveryAreaCode = objDR["DeliveryAreaCode"].ToString();
                DeliveryAreaName = objDR["DeliveryAreaName"].ToString();
                DeliveryChargeType = Util.String2Int(objDR["DeliveryChargeType"].ToString());
                RsPerKG = Util.String2Decimal(objDR["RsPerKG"].ToString());
                RsPerKGToBePaid = Util.String2Decimal(objDR["RsPerKGToBePaid"].ToString());

                DDLBranch.DataTextField = "Branch_Name";
                DDLBranch.DataValueField = "Branch_ID";
                Raj.EC.Common.SetValueToDDLSearch(objDR["Branch_Name"].ToString(), objDR["Branch_ID"].ToString(), DDLBranch);

                DistanceFromBranch = objDR["DistanceFromBranch"].ToString();
                IsODALocation = Util.String2Bool(objDR["Is_ODA"].ToString());

                VehicleID = Util.String2Int(objDR["Vehicle_Id"].ToString());

            }
            Session["DlyChargeDetails"] = ds.Tables[1];

            if (Util.String2Int(ddl_DeliveryCharge.SelectedValue) == 1)
            {
                trPerConsignment.Visible = false;
                trPerParcel.Visible = true;
            }
            else
            {
                trPerParcel.Visible = false;
                trPerConsignment.Visible = true;
            }
        //}
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();
        }
    }
    protected void ddl_DeliveryCharge_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(ddl_DeliveryCharge.SelectedValue) == 1)
        {
            trPerConsignment.Visible = false;
            trPerParcel.Visible = true;
        }
        else
        {
            trPerParcel.Visible = false;
            trPerConsignment.Visible = true;
        }
    }
}
