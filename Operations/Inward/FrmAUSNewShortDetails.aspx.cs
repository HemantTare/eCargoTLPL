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

public partial class Operations_Inward_FrmAUSNewShortDetails : System.Web.UI.Page
{

    #region ClassVariables
    Common objCommon = new Common();

    private DataSet objDS;

    DataTable objDT, objMainDT;
    DataRow DR = null;
    bool isValid = false;
    String AUSID = "0";
    DataTable SessionShortDetailsDT = null;
    DataTable SessionUnloadingDetails = null;
    TextBox txt_ReceivedArticles;
    Label lbl_ItemName, lbl_Packing,lbl_Size, lbl_LoadedArticles, lbl_ShortArticles;

    ClassLibrary.UIControl.DDLSearch ddl_GC_No;

    #endregion


    #region ControlsBind


    public int TotalShortArticles
    {
        set
        {
            lbl_TotalShortArticlesValue.Text = Util.Int2String(value);
        }
        get
        {
            return Util.String2Int(lbl_TotalShortArticlesValue.Text);
        }
    }

    #endregion

    #region IView

    private string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        SessionShortDetailsDT = StateManager.GetState<DataTable>("SessionShortDetailsGrid");
        SessionUnloadingDetails = StateManager.GetState<DataTable>("UnloadingDetailsGrid");
        AUSID = Request.QueryString["Id"];
        if (!IsPostBack)
        {
            Bind_Grid();
        }
    }

    public void CalculateTotal(DataTable dt)
    {
        int i = dt.Rows.Count;
        int Total = 0;
        for (i = 0; i <= dt.Rows.Count - 1; i++)
        {
            Total = Total + Util.String2Int(dt.Rows[i]["Short_Articles"].ToString());
        }
        TotalShortArticles = Total;
    }

    private void Bind_Grid()
    {
        string Condition = "";

        dg_ShortDetails.DataSource = objCommon.Get_View_Table(SessionShortDetailsDT, Condition);
        dg_ShortDetails.DataBind();

        if (IsPostBack)
        {
            //update_Session();
            //updateparentdataset();
        }

        CalculateTotal(SessionShortDetailsDT);
    }

    protected void dg_ShortDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_ShortDetails.EditItemIndex = -1;
        dg_ShortDetails.ShowFooter = true;
        Bind_Grid();
    }

    protected void dg_ShortDetails_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_ShortDetails.EditItemIndex = e.Item.ItemIndex;
        dg_ShortDetails.ShowFooter = false;
        Bind_Grid();
    }

    protected void dg_ShortDetails_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                objDT = SessionShortDetailsDT;

                Insert_Update_Dataset(source, e);

                if (isValid == true)
                {
                    Bind_Grid();
                    dg_ShortDetails.EditItemIndex = -1;
                    dg_ShortDetails.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate LR no.";
                ScriptManager.SetFocus(ddl_GC_No);
            }
        }
    }


    protected void dg_ShortDetails_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            lbl_ItemName = (Label)(e.Item.FindControl("lbl_ItemName"));
            lbl_Packing = (Label)(e.Item.FindControl("lbl_Packing"));
            lbl_Size = (Label)(e.Item.FindControl("lbl_Size"));
            lbl_LoadedArticles = (Label)(e.Item.FindControl("lbl_LoadedArticles"));
            txt_ReceivedArticles = (TextBox)(e.Item.FindControl("txt_ReceivedArticles"));
            lbl_ShortArticles = (Label)(e.Item.FindControl("lbl_ShortArticles"));

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
                objDT = SessionShortDetailsDT;

                DR = objDT.Rows[e.Item.ItemIndex];

                lbl_ItemName.Text = DR["Item_Name"].ToString();
                lbl_Packing.Text = DR["Packing_Type"].ToString();
                lbl_Size.Text = DR["SizeName"].ToString();
                lbl_LoadedArticles.Text = DR["Loaded_Articles"].ToString();
                txt_ReceivedArticles.Text = DR["Received_Articles"].ToString();
                lbl_ShortArticles.Text = DR["Short_Articles"].ToString();

                SetDDLGCNo(DR["GC_No"].ToString(), DR["GC_Id"].ToString(), ddl_GC_No);
            }
        }
    }

    private void SetDDLGCNo(string text, string value, ClassLibrary.UIControl.DDLSearch ddlSearch)
    {
        ddlSearch.DataTextField = "GC_No";
        ddlSearch.DataValueField = "GC_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddlSearch);
    }

    protected void dg_ShortDetails_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            int l;
            objDT = SessionShortDetailsDT;
            objMainDT = SessionUnloadingDetails;

            DataRow DR = objDT.Rows[e.Item.ItemIndex];
            int shortGCID = Util.String2Int(DR["GC_ID"].ToString());
            int shortArticles = Util.String2Int(DR["Short_Articles"].ToString());
            int receivedArticles = Util.String2Int(DR["Received_Articles"].ToString());

            for (l = 0; l < objMainDT.Rows.Count; l++)
            {
                int GCID = Util.String2Int(objMainDT.Rows[l]["GC_ID"].ToString());

                if (GCID == shortGCID)
                {
                    int TotalArticles = shortArticles + receivedArticles;
                    int LoadedArticles = Util.String2Int(objMainDT.Rows[l]["Loaded_Articles"].ToString());
                    decimal LoadedWeight = Util.String2Decimal(objMainDT.Rows[l]["Loaded_Actual_Wt"].ToString());
                    decimal AvgWeight = (LoadedWeight / LoadedArticles) * TotalArticles;

                    objMainDT.Rows[l]["Received_Articles"] = TotalArticles;
                    objMainDT.Rows[l]["Received_Wt"] = AvgWeight;

                    SessionUnloadingDetails = objMainDT;

                    break;
                }
            }

            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionShortDetailsDT = objDT;
            dg_ShortDetails.EditItemIndex = -1;
            dg_ShortDetails.ShowFooter = true;
            Bind_Grid();

            string popupScript = "<script language='javascript'>updateparent();</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(lbl_Errors, typeof(String), "PopupScript", popupScript.ToString(), false);
        }
    }

    protected void dg_ShortDetails_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDT = SessionShortDetailsDT;

            Insert_Update_Dataset(source, e);

            if (isValid == true)
            {
                dg_ShortDetails.EditItemIndex = -1;
                dg_ShortDetails.ShowFooter = true;
                Bind_Grid();
            }
        }
        catch (ConstraintException)
        {
            errorMessage = "Duplicate LR no.";
            ScriptManager.SetFocus(ddl_GC_No);
        }
    }
    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        lbl_ItemName = (Label)(e.Item.FindControl("lbl_ItemName"));
        lbl_Packing = (Label)(e.Item.FindControl("lbl_Packing"));
        lbl_Size = (Label)(e.Item.FindControl("lbl_Size"));
        lbl_LoadedArticles = (Label)(e.Item.FindControl("lbl_LoadedArticles"));
        txt_ReceivedArticles = (TextBox)(e.Item.FindControl("txt_ReceivedArticles"));
        lbl_ShortArticles = (Label)(e.Item.FindControl("lbl_ShortArticles"));

        objDT = SessionShortDetailsDT;

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
            DR["Loaded_Articles"] = Convert.ToInt32(lbl_LoadedArticles.Text);
            DR["Received_Articles"] = Convert.ToInt32(txt_ReceivedArticles.Text);
            DR["Short_Articles"] = Convert.ToInt32(lbl_LoadedArticles.Text) - Convert.ToInt32(txt_ReceivedArticles.Text);


            if (e.CommandName == "Add")
            {
                objDT.Rows.Add(DR);
            }
            SessionShortDetailsDT = objDT;

            CalculateTotal(SessionShortDetailsDT);

            string popupScript = "<script language='javascript'>updateparent();</script>";
            System.Web.UI.ScriptManager.RegisterStartupScript(lbl_Errors, typeof(String), "PopupScript", popupScript.ToString(), false);
        }
    }

    private bool Allow_To_Add_Update()
    {
        lbl_Errors.Text = "";
        if ((Util.String2Int(lbl_LoadedArticles.Text) - Util.String2Int(txt_ReceivedArticles.Text)) <= 0)
        {
            errorMessage = "Please Enter Valid Received Articles";
            ScriptManager.SetFocus(txt_ReceivedArticles);
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
        txt_ReceivedArticles = (TextBox)item.FindControl("txt_ReceivedArticles");
        lbl_ShortArticles = (Label)item.FindControl("lbl_ShortArticles");

        int LHPO_Id=0;
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
            txt_ReceivedArticles.Text = objDR["Loaded_Articles"].ToString();
            lbl_ShortArticles.Text = "0";
        }

        ddl_GC_No.DataTextField = "GC_No";
        ddl_GC_No.DataValueField = "GC_Id";
        ddl_GC_No.OtherColumns = AUSID;
        txt_ReceivedArticles.Focus();
    
    }
}
