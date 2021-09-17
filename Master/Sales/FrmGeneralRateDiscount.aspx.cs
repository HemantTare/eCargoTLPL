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
using ClassLibrary;
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Master_Sales_FrmGeneralRateDiscount : ClassLibraryMVP.UI.Page
{
    TextBox txtMinimumQty, txtMinimumFreight, txtDiscountPercent;
    bool ATS = false;

    #region members
    public bool validateUI()
    {
        bool ATS;
        ATS = false;

        DataTable DT = (DataTable)(Session["DiscountDetails"]);

        if (ddlRateDiscountFor.SelectedValue == "2" && Util.String2Int(ddlParty.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please select party";
            ddlParty.Focus();
        }
        else if (ddlRateDiscountFor.SelectedValue == "2" && Util.String2Int(hdn_PartyCategory_ID.Value) > 0)
        {
            lblErrors.Text = "Please Remove Category From Regular Party Master";
            ddlParty.Focus();
        }
        else if (ddlRateDiscountFor.SelectedValue == "1" && Util.String2Int(ddlDiscountBranch.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please select discount branch";
            ddlDiscountBranch.Focus();
        }
        else if (dtpValidFrom.SelectedDate > dtpValidUpto.SelectedDate)
        {
            lblErrors.Text = "Valid Upto must be greater than Valid from date";
        }
        else if (DT.Rows.Count <= 0)
        {
            lblErrors.Text = "Please enter discount percent";
        }
        else
        {
            ATS = true;
        }

        return ATS;
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));

            ddlParty.DataTextField = "Client_Name";
            ddlParty.DataValueField = "Client_Id";
            ddlParty.OtherColumns = "3";

            ddlDiscountBranch.DataTextField = "Branch_Name";
            ddlDiscountBranch.DataValueField = "Branch_Id";
 

            ReadValues();
            BindGrid();
        }

    }
    private void ReadValues()
    {

        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@GeneralRateDiscountID", SqlDbType.Int, 0, Util.String2Int(hdnKeyID.Value)) };
        objDAL.RunProc("EC_Mst_GeneralRateDiscount_ReadValues", objSqlParam, ref ds);

        if (ds.Tables[2].Rows.Count > 0)
        {
            BindCustomer_Category(ds.Tables[2]);
        }

        if (ds.Tables[3].Rows.Count > 0)
        {
            BindDiscount_Title(ds.Tables[3]);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            string text, value;
            DataRow objDR = ds.Tables[0].Rows[0];

            ddlRateDiscountFor.SelectedValue = objDR["RateDiscountForID"].ToString();

            if (ddlRateDiscountFor.SelectedValue == "2")
            {
                text = objDR["Client_Name"].ToString();
                value = objDR["Client_ID"].ToString();
                Raj.EC.Common.SetValueToDDLSearch(text, value, ddlParty);
            }

            ddlPartyAs.SelectedValue = objDR["ClientAsId"].ToString();
            txtPromisedQtyPerMonth.Text = objDR["PromisedQtyPerMonth"].ToString();

            text = objDR["Branch_Name"].ToString();
            value = objDR["DiscountBranchID"].ToString();
            Raj.EC.Common.SetValueToDDLSearch(text, value, ddlDiscountBranch);

            ddlBranchAs.SelectedValue = objDR["BranchAsID"].ToString();
            BindDlyArea();
            ddl_dlyArea.SelectedValue = objDR["DeliveryAreaID"].ToString();  
            dtpValidFrom.SelectedDate = Convert.ToDateTime(objDR["ValidFrom"].ToString());
            dtpValidUpto.SelectedDate = Convert.ToDateTime(objDR["ValidUpTo"].ToString());
            ddl_ClientCategory.SelectedValue = objDR["Category_ID"].ToString();
            ddl_DiscountTitle.SelectedValue = objDR["DiscountTitleID"].ToString();

            hdn_PartyCategory_ID.Value = objDR["PartyCategory_ID"].ToString(); 

        }
        Session["DiscountDetails"] = ds.Tables[1];

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            Save();
        }
    }

    private Message Save()
    {
        DataTable DT = (DataTable)(Session["DiscountDetails"]);
        DT.TableName = "discountdetails";
        DataTable DT1 = DT.Copy();

        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);

        string discountDetailsXML = ds.GetXml().ToLower();

        int dlyAreaId = 0, ClientID = 0, ClientAsId = 0;
        if (ddlRateDiscountFor.SelectedValue == "1" && ddlBranchAs.SelectedValue == "2")
        {
            dlyAreaId = Util.String2Int(ddl_dlyArea.SelectedValue);
        }
        else
        {
            dlyAreaId = 0;
        }
        if (ddlRateDiscountFor.SelectedValue == "2")
        {
            ClientID = Util.String2Int(ddlParty.SelectedValue);
            ClientAsId = Util.String2Int(ddlPartyAs.SelectedValue);
        }
        else
        {
            ClientID = 0;
            ClientAsId = 0;
        }
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeOutParams("@GRDIDOutput", SqlDbType.Int, 0), 
            objDAL.MakeInParams("@GeneralRateDiscountID",SqlDbType.Int,0,Util.String2Int(hdnKeyID.Value)),
            objDAL.MakeInParams("@RateDiscountForID",SqlDbType.Int,0,Util.String2Int(ddlRateDiscountFor.SelectedValue)),
            objDAL.MakeInParams("@ClientID",SqlDbType.Int,0,ClientID),
            objDAL.MakeInParams("@ClientAsId",SqlDbType.Int,0,ClientAsId),
            objDAL.MakeInParams("@PromisedQtyPerMonth",SqlDbType.Decimal,0,Util.String2Int(txtPromisedQtyPerMonth.Text)),
            objDAL.MakeInParams("@DiscountBranchID",SqlDbType.Int,0,Util.String2Int(ddlDiscountBranch.SelectedValue)),
            objDAL.MakeInParams("@BranchAsID",SqlDbType.Int,0,Util.String2Int(ddlBranchAs.SelectedValue)),
            objDAL.MakeInParams("@DeliveryAreaID",SqlDbType.Int,0,dlyAreaId),
            objDAL.MakeInParams("@ValidFrom",SqlDbType.DateTime,0,dtpValidFrom.SelectedDate),
            objDAL.MakeInParams("@ValidUpTo",SqlDbType.DateTime,0,dtpValidUpto.SelectedDate),
            objDAL.MakeInParams("@DiscountDetailsXML",SqlDbType.Xml,0,discountDetailsXML),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId),
            objDAL.MakeInParams("@Category_ID",SqlDbType.Int,0,Util.String2Int(ddl_ClientCategory.SelectedValue)),
            objDAL.MakeInParams("@DiscountTitleID",SqlDbType.Int,0,Util.String2Int(ddl_DiscountTitle.SelectedValue))
        };

        objDAL.RunProc("dbo.EC_Mst_GeneralRateDiscount_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            lblErrors.Text = "Saved SuccessFully";
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);
        }
        else
        {
            lblErrors.Text = objMessage.message;
        }
        return objMessage;
    }

    #region grid operation

    protected void dgGrid_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dgGrid.EditItemIndex = -1;
        dgGrid.ShowFooter = true;
        BindGrid();
    }
    protected void dgGrid_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        DataTable DT = (DataTable)(Session["DiscountDetails"]);
        DataRow DR = DT.Rows[e.Item.ItemIndex];
        DT.Rows.Remove(DR);
        Session["DiscountDetails"] = DT;
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
                txtMinimumQty = (TextBox)(e.Item.FindControl("txtMinimumQtyAdd"));
                txtMinimumFreight = (TextBox)(e.Item.FindControl("txtMinimumFreightAdd"));
                txtDiscountPercent = (TextBox)(e.Item.FindControl("txtDiscountPercentAdd"));
            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {
                txtMinimumQty = (TextBox)(e.Item.FindControl("txtMinimumQtyEdit"));
                txtMinimumFreight = (TextBox)(e.Item.FindControl("txtMinimumFreightEdit"));
                txtDiscountPercent = (TextBox)(e.Item.FindControl("txtDiscountPercentEdit"));
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                DataTable DT = (DataTable)(Session["DiscountDetails"]);
                DataRow DR = DT.Rows[e.Item.ItemIndex];

                txtMinimumQty.Text = DR["MinimumQty"].ToString();
                txtMinimumFreight.Text = DR["MinimumFreight"].ToString();
                txtDiscountPercent.Text = DR["DiscountPercent"].ToString();
            }
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        DataTable DT = (DataTable)(Session["DiscountDetails"]);
        DataRow DR = null;
        if (e.CommandName == "Add")
        {
            txtMinimumQty = (TextBox)(e.Item.FindControl("txtMinimumQtyAdd"));
            txtMinimumFreight = (TextBox)(e.Item.FindControl("txtMinimumFreightAdd"));
            txtDiscountPercent = (TextBox)(e.Item.FindControl("txtDiscountPercentAdd"));

            DR = DT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            txtMinimumQty = (TextBox)(e.Item.FindControl("txtMinimumQtyEdit"));
            txtMinimumFreight = (TextBox)(e.Item.FindControl("txtMinimumFreightEdit"));
            txtDiscountPercent = (TextBox)(e.Item.FindControl("txtDiscountPercentEdit"));

            DR = DT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["MinimumQty"] = txtMinimumQty.Text;
            DR["MinimumFreight"] = txtMinimumFreight.Text;
            DR["DiscountPercent"] = txtDiscountPercent.Text;

            if (e.CommandName == "Add") { DT.Rows.Add(DR); }
            Session["DiscountDetails"] = DT;
        }
    }

    private bool Allow_To_Add_Update()
    {
        if (Util.String2Decimal(txtMinimumQty.Text) <= 0)
        {
            lblErrors.Text = "Please enter minimum qty greater > 0";
            txtMinimumQty.Focus();
        }
        else if (Util.String2Decimal(txtMinimumFreight.Text) <= 0)
        {
            lblErrors.Text = "Please enter minimum freight greater > 0";
            txtMinimumFreight.Focus();
        }
        else if (Util.String2Decimal(txtDiscountPercent.Text) <= 0)
        {
            lblErrors.Text = "Please enter discount percent greater > 0";
            txtDiscountPercent.Focus();
        }
        else
            ATS = true;

        return ATS;
    }
    #endregion

    private void BindGrid()
    {
        dgGrid.DataSource = (DataTable)(Session["DiscountDetails"]);
        dgGrid.DataBind();
    }
    protected void ddlDiscountBranch_TxtChange(object sender, EventArgs e)
    {
        BindDlyArea();
    }
    protected void ddlParty_TxtChange(object sender, EventArgs e)
    {
        int ddlParty_Id;
        ddlParty_Id = Convert.ToInt32(ddlParty.SelectedValue);
        DataSet ds = new DataSet();
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Client_ID",SqlDbType.Int,0,ddlParty_Id)
        };

        objDAL.RunProc("dbo.EC_Mst_GeneralRateDiscount_GetClientDetails", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];

            hdn_PartyCategory_ID.Value = objDR["Category_ID"].ToString(); 
        }


    }

    public void BindDlyArea()
    {
        DataTable dt = new DataTable();
        int DiscountBranch_Id;
        DiscountBranch_Id = Convert.ToInt32(ddlDiscountBranch.SelectedValue);
        Common objCommon = new Common();


        dt = objCommon.GetDeliveryArea(DiscountBranch_Id, false, true, false, 0);

        ddl_dlyArea.DataTextField = "DeliveryAreaName";
        ddl_dlyArea.DataValueField = "DeliveryAreaID";
        ddl_dlyArea.DataSource = dt;
        ddl_dlyArea.DataBind();

        ddl_dlyArea.Items.Insert(0, new ListItem("Select One", "0"));
        ScriptManager.SetFocus(ddl_dlyArea);

    }
    public void BindCustomer_Category(DataTable dt)
    {
        ddl_ClientCategory.DataTextField = "Category_Name";
        ddl_ClientCategory.DataValueField = "Category_ID";
        ddl_ClientCategory.DataSource = dt;
        ddl_ClientCategory.DataBind();

        ddl_ClientCategory.Items.Insert(0, new ListItem("Select One", "0"));
        ScriptManager.SetFocus(ddl_ClientCategory);

    }

    public void BindDiscount_Title(DataTable dt)
    {
        ddl_DiscountTitle.DataTextField = "DiscountTitle";
        ddl_DiscountTitle.DataValueField = "DiscountTitleID";
        ddl_DiscountTitle.DataSource = dt;
        ddl_DiscountTitle.DataBind();

        ddl_DiscountTitle.Items.Insert(0, new ListItem("Select One", "0"));
        ScriptManager.SetFocus(ddl_DiscountTitle);

    }
}
