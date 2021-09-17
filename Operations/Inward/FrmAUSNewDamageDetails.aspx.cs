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
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;

public partial class Operations_Inward_FrmAUSNewDamageDetails : System.Web.UI.Page
{
    #region ClassVariables
    Common objCommon = new Common();

    private DataSet objDS;

    DataTable objDT, objMainDT;
    DataRow DR = null;
    bool isValid = false;
    String AUSID = "0";
    DataTable SessionDamageDetailsDT = null;
    DataTable SessionUnloadingDetails = null;
    DropDownList ddl_Received_Condintion;
    TextBox txt_ReceivedArticles;
    Label lbl_ItemName, lbl_Packing, lbl_Size, lbl_LoadedArticles, lbl_ShortArticles;
    TextBox txt_Damaged_Leakage_Articles, txt_Damaged_Leakage_Value;
    ClassLibrary.UIControl.DDLSearch ddl_GC_No;
    #endregion

    public int TotalDamageArticles
    {
        set
        {
            lbl_TotalDamageArticlesValue.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(lbl_TotalDamageArticlesValue.Text);
        }
    }
    private string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }

    public DataTable SessionReceivedCondition
    {
        get { return StateManager.GetState<DataTable>("ReceivedCondition"); }
        set { StateManager.SaveState("ReceivedCondition", value); }
    }

    public void BindReceivedCondition()
    {
        ddl_Received_Condintion.DataSource = SessionReceivedCondition;
        ddl_Received_Condintion.DataTextField = "Received_Condition";
        ddl_Received_Condintion.DataValueField = "Received_Condition_ID";
        ddl_Received_Condintion.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        SessionDamageDetailsDT = StateManager.GetState<DataTable>("SessionDamageDetailsGrid");
        SessionUnloadingDetails = StateManager.GetState<DataTable>("UnloadingDetailsGrid");

        AUSID = Request.QueryString["Id"];
        if (!IsPostBack)
        {
            FillValue();
            Bind_Grid();
        }
    }

    public void CalculateTotal(DataTable dt)
    {
        int i = dt.Rows.Count;
        int Total = 0;
        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {
            Total = Total + Util.String2Int(dt.Rows[i]["Damaged_Articles"].ToString());
        }
        TotalDamageArticles = Total;
    }
    private void Bind_Grid()
    {
        string Condition = "";

        dg_DamageDetails.DataSource = objCommon.Get_View_Table(SessionDamageDetailsDT, Condition);
        dg_DamageDetails.DataBind();

        if (IsPostBack)
        {
            //update_Session();
            //updateparentdataset();
        }

        CalculateTotal(SessionDamageDetailsDT);
    }

    private void FillValue()
    {
        DAL objDAL = new DAL();
        objDAL.RunProc("dbo.EC_Opr_AUS_FillValues", ref objDS);
        SessionReceivedCondition = objDS.Tables[1];
    }
    protected void dg_DamageDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_DamageDetails.EditItemIndex = -1;
        dg_DamageDetails.ShowFooter = true;
        Bind_Grid();
    }

    protected void dg_DamageDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_DamageDetails.EditItemIndex = e.Item.ItemIndex;
        dg_DamageDetails.ShowFooter = false;
        Bind_Grid();
    }
    protected void dg_DamageDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                objDT = SessionDamageDetailsDT;

                Insert_Update_Dataset(source, e);

                if (isValid == true)
                {
                    Bind_Grid();
                    dg_DamageDetails.EditItemIndex = -1;
                    dg_DamageDetails.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Error";
                ScriptManager.SetFocus(ddl_GC_No);
            }
        }
    }
    protected void dg_DamageDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            lbl_ItemName = (Label)(e.Item.FindControl("lbl_ItemName"));
            lbl_Packing = (Label)(e.Item.FindControl("lbl_Packing"));
            lbl_Size = (Label)(e.Item.FindControl("lbl_Size"));
            lbl_LoadedArticles = (Label)(e.Item.FindControl("lbl_LoadedArticles"));
            txt_Damaged_Leakage_Articles = (TextBox)(e.Item.FindControl("txt_Damaged_Leakage_Articles"));
            txt_Damaged_Leakage_Value = (TextBox)(e.Item.FindControl("txt_Damaged_Leakage_Value"));
            ddl_Received_Condintion = (DropDownList)(e.Item.FindControl("ddl_Received_Condintion"));

            BindReceivedCondition();
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ddl_GC_No = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_GC_No"));
                if (IsPostBack)
                    ddl_GC_No.Focus();
                ddl_GC_No.OtherColumns = AUSID;
            }
            else if (e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_GC_No = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_GC_No"));
                ddl_GC_No.OtherColumns = AUSID;
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionDamageDetailsDT;

                DR = objDT.Rows[e.Item.ItemIndex];

                lbl_ItemName.Text = DR["Item_Name"].ToString();
                lbl_Packing.Text = DR["Packing_Type"].ToString();
                lbl_Size.Text = DR["SizeName"].ToString();
                lbl_LoadedArticles.Text = DR["Loaded_Articles"].ToString();
                txt_Damaged_Leakage_Articles.Text = DR["Damaged_Articles"].ToString();
                txt_Damaged_Leakage_Value.Text = DR["Damaged_Value"].ToString();
                ddl_Received_Condintion.SelectedValue = DR["Received_Condition_ID"].ToString();

                SetDDLGCNo(DR["GC_No"].ToString(), DR["GC_Id"].ToString(), ddl_GC_No);
            }
            if (e.Item.ItemIndex != -1)
            {
                ddl_Received_Condintion.SelectedValue = SessionDamageDetailsDT.Rows[e.Item.ItemIndex]["Received_Condition_ID"].ToString();
            }
        }
    }
    private void SetDDLGCNo(string text, string value, ClassLibrary.UIControl.DDLSearch ddlSearch)
    {
        ddlSearch.DataTextField = "GC_No";
        ddlSearch.DataValueField = "GC_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddlSearch);
    }

    protected void dg_DamageDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            int l;
            objDT = SessionDamageDetailsDT;
            objMainDT = SessionUnloadingDetails;

            DataRow DR = objDT.Rows[e.Item.ItemIndex];
            int damageGCID = Util.String2Int(DR["GC_ID"].ToString());
            for (l = 0; l < objMainDT.Rows.Count; l++)
            {
                int GCID = Util.String2Int(objMainDT.Rows[l]["GC_ID"].ToString());

                if (GCID == damageGCID)
                {
                    objMainDT.Rows[l]["damaged_articles"] = 0;
                    objMainDT.Rows[l]["Damaged_Value"] = "0.00";
                    objMainDT.Rows[l]["Received_Condition_ID"] = 0;

                    SessionUnloadingDetails = objMainDT;
                    break;
                }
            }

            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionDamageDetailsDT = objDT;

            dg_DamageDetails.EditItemIndex = -1;
            dg_DamageDetails.ShowFooter = true;
            Bind_Grid();

            string popupScript = "<script language='javascript'>updateparent();</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(lbl_Errors, typeof(String), "PopupScript", popupScript.ToString(), false);
        }
    }
    protected void dg_DamageDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDT = SessionDamageDetailsDT;

            Insert_Update_Dataset(source, e);

            if (isValid == true)
            {
                dg_DamageDetails.EditItemIndex = -1;
                dg_DamageDetails.ShowFooter = true;
                Bind_Grid();
            }
        }
        catch (ConstraintException)
        {
            errorMessage = "Error";
            ScriptManager.SetFocus(ddl_GC_No);
        }
    }
    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        lbl_ItemName = (Label)(e.Item.FindControl("lbl_ItemName"));
        lbl_Packing = (Label)(e.Item.FindControl("lbl_Packing"));
        lbl_Size = (Label)(e.Item.FindControl("lbl_Size"));
        lbl_LoadedArticles = (Label)(e.Item.FindControl("lbl_LoadedArticles"));
        txt_Damaged_Leakage_Articles = (TextBox)(e.Item.FindControl("txt_Damaged_Leakage_Articles"));
        txt_Damaged_Leakage_Value = (TextBox)(e.Item.FindControl("txt_Damaged_Leakage_Value"));
        ddl_Received_Condintion = (DropDownList)(e.Item.FindControl("ddl_Received_Condintion"));

        objDT = SessionDamageDetailsDT;

        if (e.CommandName == "Add")
        {
            ddl_GC_No = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_GC_No"));
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            ddl_GC_No = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_GC_No"));
            DR = objDT.Rows[e.Item.ItemIndex];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["GC_Id"] = ddl_GC_No.SelectedValue;
            DR["GC_No"] = ddl_GC_No.SelectedText;

            DR["Item_Name"] = lbl_ItemName.Text;
            DR["Packing_Type"] = lbl_Packing.Text;
            DR["SizeName"] = lbl_Size.Text;
            DR["Loaded_Articles"] = Util.String2Int(lbl_LoadedArticles.Text);
            DR["Received_Condition_ID"] = Util.String2Int(ddl_Received_Condintion.SelectedValue);
            DR["Damaged_Articles"] = Util.String2Int(txt_Damaged_Leakage_Articles.Text);
            DR["Damaged_Value"] = Util.String2Decimal(txt_Damaged_Leakage_Value.Text);

            if (e.CommandName == "Add")
            {
                objDT.Rows.Add(DR);
            }
            SessionDamageDetailsDT = objDT;

            CalculateTotal(SessionDamageDetailsDT);

            string popupScript = "<script language='javascript'>updateparent();</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(lbl_Errors, typeof(String), "PopupScript", popupScript.ToString(), false);
        }
    }

    private bool Allow_To_Add_Update()
    {
        lbl_Errors.Text = "";
        if (ddl_Received_Condintion.SelectedValue == "1")
        {
            errorMessage = "Please select damaged condition";
            ScriptManager.SetFocus(ddl_Received_Condintion);
        }
        else if (Util.String2Int(txt_Damaged_Leakage_Articles.Text) > Util.String2Int(lbl_LoadedArticles.Text))
        {
            errorMessage = "Damaged Articles can not be greater than Total Articles";
            ScriptManager.SetFocus(txt_Damaged_Leakage_Articles);
        }
        else
        {
            isValid = true;
        }

        return isValid;
    }
    protected void btn_Exit_Click(object sender, EventArgs e)
    {

        Response.Write("<script language='javascript'>{self.close()}</script>");

    }
    protected void ddl_GC_No_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        DataGridItem item = (DataGridItem)txt.Parent.Parent.Parent;
        DataSet ds = new DataSet();

        ddl_GC_No = (ClassLibrary.UIControl.DDLSearch)item.FindControl("ddl_GC_No");
        lbl_ItemName = (Label)item.FindControl("lbl_ItemName");
        lbl_Packing = (Label)item.FindControl("lbl_Packing");
        lbl_Size = (Label)item.FindControl("lbl_Size");
        lbl_LoadedArticles = (Label)item.FindControl("lbl_LoadedArticles");
        ddl_Received_Condintion = (DropDownList)item.FindControl("ddl_Received_Condintion");
        txt_Damaged_Leakage_Articles = (TextBox)item.FindControl("txt_Damaged_Leakage_Articles");
        txt_Damaged_Leakage_Value = (TextBox)item.FindControl("txt_Damaged_Leakage_Value");
        BindReceivedCondition();
        int LHPO_Id = 0;
        int GC_Id = 0;
        if (Util.String2Int(ddl_GC_No.SelectedValue) > 0)
            GC_Id = Util.String2Int(ddl_GC_No.SelectedValue);

        if (System.Web.HttpContext.Current.Session["AUSNew_LHPO_Id"] != null)
        {
            LHPO_Id = Util.String2Int((string)System.Web.HttpContext.Current.Session["AUSNew_LHPO_Id"]);
        }

        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam ={  
                                    objDAL.MakeInParams("@GC_Id", SqlDbType.Int,0,GC_Id),
                                    objDAL.MakeInParams("@LHPO_Id", SqlDbType.Int,0,LHPO_Id),
                                    objDAL.MakeInParams("@AUS_Id", SqlDbType.Int,0,AUSID)};
        objDAL.RunProc("EC_Opr_Get_GCDetailsForAUS_ShortDamage", objSqlParam, ref objDS);


        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            lbl_ItemName.Text = objDR["Item_Name"].ToString();
            lbl_Packing.Text = objDR["Packing"].ToString();
            lbl_Size.Text = objDR["Size"].ToString();
            lbl_LoadedArticles.Text = objDR["Loaded_Articles"].ToString();

            ddl_Received_Condintion.SelectedValue = "1";
            txt_Damaged_Leakage_Articles.Text = "0";
            txt_Damaged_Leakage_Value.Text = "0";
        }

        ddl_GC_No.DataTextField = "GC_No";
        ddl_GC_No.DataValueField = "GC_Id";
        ddl_GC_No.OtherColumns = AUSID;
        ddl_Received_Condintion.Focus();

    }
}
