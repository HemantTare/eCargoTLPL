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

public partial class Master_Sales_FrmCouponDiscount : ClassLibraryMVP.UI.Page
{
    TextBox txtCouponNo, txtAmount;

    ComponentArt.Web.UI.Calendar ValidFrom, ValidUpTo;
    bool ATS = false;

    private string ErrorMsg
    {
        set { lblErrors.Text = value; }
    }

    #region members
    public bool validateUI()
    {
        bool ATS;
        ATS = false;

        DataTable DT = (DataTable)(Session["DiscountDetails"]);

        if (Util.String2Int(ddl_DiscountTitle.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select Discount Tile";
            ddl_DiscountTitle.Focus();
        }
        else if (ddlParty.SelectedValue.Trim()  == "")
        {
            lblErrors.Text = "Please Client Name ";
            ddlParty.Focus();
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
            ddlParty.OtherColumns = "2";

            ReadValues();
            BindGrid();
        }

    }
    private void ReadValues()
    {

        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@CouponDiscountID", SqlDbType.Int, 0, Util.String2Int(hdnKeyID.Value)) };
        objDAL.RunProc("EC_Mst_CouponRateDiscount_ReadValues", objSqlParam, ref ds);

      
        if (ds.Tables[2].Rows.Count > 0)
        {
            BindDiscount_Title(ds.Tables[2]);
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            string text, value;
            DataRow objDR = ds.Tables[0].Rows[0];

                text = objDR["ClientName"].ToString();
                value = objDR["ClientID"].ToString();
                Raj.EC.Common.SetValueToDDLSearch(text, value, ddlParty);


            ddl_DiscountTitle.SelectedValue = objDR["DiscountTitleID"].ToString();

            hdn_Is_Regular_Client.Value = objDR["Is_Regular_Client"].ToString();
            
            txt_Remarks.Text = objDR["Remark"].ToString(); 
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
        string  ClientID = ddlParty.SelectedValue;
        
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@CouponDiscountID",SqlDbType.Int,0,Util.String2Int(hdnKeyID.Value)),
            objDAL.MakeInParams("@DiscountTitleID",SqlDbType.Int,0,Util.String2Int(ddl_DiscountTitle.SelectedValue)),
            objDAL.MakeInParams("@ClientID",SqlDbType.VarChar,15,ClientID),
            objDAL.MakeInParams("@Is_Regular_Client",SqlDbType.Bit ,0,hdn_Is_Regular_Client.Value),
            objDAL.MakeInParams("@DiscountDetailsXML",SqlDbType.Xml,0,discountDetailsXML),
            objDAL.MakeInParams("@Remark",SqlDbType.VarChar ,500,txt_Remarks.Text),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EC_Mst_CouponDiscount_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            String popupScript = "";
            string _Msg = "Saved SuccessFully";
            
            string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");

            System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(String), "PopupScript1", popupScript.ToString(), false);

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

        DataTable DT = (DataTable)(Session["DiscountDetails"]);
        DataRow DR = null;

        DataColumn[] _dtColumnPrimaryKey;
        _dtColumnPrimaryKey = new DataColumn[1];
        _dtColumnPrimaryKey[0] = DT.Columns["CouponNo"];
        DT.PrimaryKey = _dtColumnPrimaryKey;

        if (e.CommandName == "Add" || e.CommandName == "Update")
        {
            try
            {
                Insert_Update_Dataset(source, e);
                if (ATS)
                {
                    BindGrid();
                    dgGrid.EditItemIndex = -1;
                    dgGrid.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                ErrorMsg = "Duplicate Coupon No.";
                txtCouponNo.Focus();
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
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                txtCouponNo = (TextBox)(e.Item.FindControl("txtCouponNo"));
                txtAmount = (TextBox)(e.Item.FindControl("txtAmount"));

                ValidFrom = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("ValidFrom");
                ValidUpTo = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("ValidUpTo");
            }


            if (e.Item.ItemType == ListItemType.EditItem)
            {
                txtCouponNo = (TextBox)(e.Item.FindControl("txtCouponNo"));
                txtAmount = (TextBox)(e.Item.FindControl("txtAmount"));

                ValidFrom = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("ValidFrom");
                ValidUpTo = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("ValidUpTo");

                DataTable DT = (DataTable)(Session["DiscountDetails"]);
                DataRow DR = DT.Rows[e.Item.ItemIndex];

                txtCouponNo.Text = DR["CouponNo"].ToString();
                txtAmount.Text = DR["Amount"].ToString();

                ValidFrom.SelectedDate = Convert.ToDateTime(DR["ValidFrom"]);
                ValidUpTo.SelectedDate = Convert.ToDateTime(DR["ValidUpTo"]);

            }
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        DataTable DT = (DataTable)(Session["DiscountDetails"]);
        DataRow DR = null;


        txtCouponNo = (TextBox)(e.Item.FindControl("txtCouponNo"));
        txtAmount = (TextBox)(e.Item.FindControl("txtAmount"));
        ValidFrom = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("ValidFrom");
        ValidUpTo = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("ValidUpTo");

        if (e.CommandName == "Add")
        {
            
            DR = DT.NewRow();
        }
        else if (e.CommandName == "Update")
        {

            DR = DT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["CouponNo"] = txtCouponNo.Text;
            DR["Amount"] = txtAmount.Text;
            DR["ValidFrom"] = String.Format("{0:dd MMM yyyy}", ValidFrom.SelectedDate);
            DR["ValidUpTo"] = String.Format("{0:dd MMM yyyy}", ValidUpTo.SelectedDate); ;

            if (e.CommandName == "Add") { DT.Rows.Add(DR); }
            Session["DiscountDetails"] = DT;
        }
    }

    private bool Allow_To_Add_Update()
    {
        if (Util.String2Decimal(txtCouponNo.Text) <= 0)
        {
            lblErrors.Text = "Please Enter CouponNo";
            txtCouponNo.Focus();
        }
        else if (Util.String2Decimal(txtAmount.Text) <= 0)
        {
            lblErrors.Text = "Please Enter Minimum Discount Amount";
            txtAmount.Focus();
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

    protected void ddlParty_TxtChange(object sender, EventArgs e)
    {
        string ddlParty_Id;
        ddlParty_Id = Convert.ToString(ddlParty.SelectedValue);
        DataSet ds = new DataSet();
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Client_ID",SqlDbType.VarChar ,15,ddlParty_Id)
        };

        objDAL.RunProc("dbo.EC_Mst_CouponDiscount_GetClientDetails", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];

            hdn_Is_Regular_Client.Value = objDR["Is_Regular_Client"].ToString(); 
        }

        ddlParty.Focus();
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
