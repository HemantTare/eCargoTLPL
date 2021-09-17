using System;
using System.Data;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;
using Raj.EC;

/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 17/10/2008
/// description   : page is for FBT Categories details.
/// </summary>
/// 

public partial class FA_Common_Accounting_Masters_WucFBTCategories : System.Web.UI.UserControl, IFBTCategoriesView
{
    #region ClassVariable
    FBTCategoriesPresenter objFBTCategoriesPresenter;
    TextBox txt_Eligible;
    DropDownList ddl_Assessee_Category;
    ComponentArt.Web.UI.Calendar dtp_Applicable;
    bool isValid = false;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    DataSet objDS;
    LinkButton lbtn_Delete;

    #endregion

    #region ControlsValue
    public int FBTCategoryId
    {
        get { return Convert.ToInt32(ddl_FBT_Categories.SelectedValue); }

        set { ddl_FBT_Categories.SelectedValue = value.ToString(); }
    }
    public string FBTSection
    {
        get { return lbl_Section.Text; }
        set { lbl_Section.Text = value; }
    }

    public DataSet SessionCategoryDetailsGrid
    {
        get { return StateManager.GetState<DataSet>("FBTCategoriesDetails"); }
        set { StateManager.SaveState("FBTCategoriesDetails", value); }
    }
    public DataTable SessionFBTAssesseeCategory
    {
        get { return StateManager.GetState<DataTable>("FBT_Assessee_Categories"); }
        set { StateManager.SaveState("FBT_Assessee_Categories", value); }
    }
    #endregion

    #region IView

    public string errorMessage
    {
        set { lbl_Error.Text = value; }
    }

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public bool validateUI()
    {
        if (Convert.ToInt32(ddl_FBT_Categories.SelectedValue) == 0)
        {
            errorMessage = GetLocalResourceObject("Msg_FBT_Category").ToString();// "Select FBT Category";
            return false;
        }
        return true;
    }
    #endregion

    #region ControlsBind
    public DataSet BindCategoryDetailsGrid
    {
        set
        {
            dg_Category_Details.DataSource = value;
            dg_Category_Details.DataBind();
        }
    }
    public DataTable BindAssesseCategory
    {
        set
        {
            ddl_Assessee_Category.DataSource = value;
            ddl_Assessee_Category.DataTextField = "Assessee_Category_Name";
            ddl_Assessee_Category.DataValueField = "Assessee_Category_Id";
            ddl_Assessee_Category.DataBind();
            ddl_Assessee_Category.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindFBTCateroies
    {
        set
        {
            ddl_FBT_Categories.DataSource = value;
            ddl_FBT_Categories.DataTextField = "FBT_Category_Name";
            ddl_FBT_Categories.DataValueField = "FBT_Category_Id";
            ddl_FBT_Categories.DataBind();
            //ddl_FBT_Categories.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    #endregion

    #region OtherMethods
    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        bool isApplicable = false;
        objDS = SessionCategoryDetailsGrid;
        DataRow DR = null;

        ddl_Assessee_Category = (DropDownList)e.Item.FindControl("ddl_Assessee_Category");
        dtp_Applicable = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("dtp_Applicable");
        txt_Eligible = (TextBox)e.Item.FindControl("txt_Eligible");
        isApplicable = CheckAssesseTypeApplicableFrom(e);

        if (isApplicable == true)
        {
            if (e.CommandName == "ADD")
            {
                DR = objDS.Tables[0].NewRow();
            }

            if (e.CommandName == "Update")
            {
                DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
            }

            if (Allow_To_Add_Update() == true)
            {
                DR["FBT_Category_Id"] = ddl_FBT_Categories.SelectedValue;
                DR["Assesse_Category_Id"] = ddl_Assessee_Category.SelectedValue;
                DR["Assesse_Category_Name"] = ddl_Assessee_Category.SelectedItem.Text;
                DR["Applicable_From"] = String.Format("{0:dd MMM yyyy}", dtp_Applicable.SelectedDate);
                DR["Eligible"] = Util.String2Decimal(txt_Eligible.Text);

                if (e.CommandName == "ADD")
                {
                    objDS.Tables[0].Rows.Add(DR);
                }
                SessionCategoryDetailsGrid = objDS;
            }
        }
    }
    private bool Allow_To_Add_Update()
    {
        if (Util.String2Int(ddl_Assessee_Category.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Assessee Category";
            ddl_Assessee_Category.Focus();
        }
        else
            isValid = true;

        return isValid;
    }
    private bool CheckAssesseTypeApplicableFrom(System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        objDS = SessionCategoryDetailsGrid;
        DataRow[] dr_Array;
        int i;
        int _RowNo;
        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataSet ds_Temp = new DataSet();
            DataTable dt = new DataTable();
            DataView dv = new DataView();

            dv = ObjCommon.Get_View_Table(objDS.Tables[0], "Assesse_Category_Id= '" + ddl_Assessee_Category.SelectedValue + "'");
            dt = dv.ToTable();
            ds_Temp.Tables.Add(dt);

            if (ds_Temp.Tables[0].Rows.Count >= 1)
            {
                for (i = 0; i < objDS.Tables[0].Rows.Count; i++)
                {
                    if (e.CommandName == "Update")
                    {
                        if (i == e.Item.ItemIndex)
                        {
                            dr_Array = objDS.Tables[0].Select("Applicable_From='" + String.Format("{0:dd MMM yyyy}", dtp_Applicable.SelectedDate) + "'");
                            if (dr_Array.Length > 0)
                            {
                                _RowNo = objDS.Tables[0].Rows.IndexOf(dr_Array[0]);
                                if (_RowNo != e.Item.ItemIndex)
                                {
                                    errorMessage = "Duplicate Assesse Category and Applicable From";
                                    return false;
                                }
                            }
                        }
                    }
                    else if (dtp_Applicable.SelectedDate == Convert.ToDateTime(objDS.Tables[0].Rows[i]["Applicable_From"]))
                    {
                        errorMessage = "Duplicate Assesse Category and Applicable From";
                        return false;
                    }
                }
            }
        }
        return true;
    }
    public void BindGrid()
    {
        dg_Category_Details.DataSource = SessionCategoryDetailsGrid;
        dg_Category_Details.DataBind();
    }

    private void SetUpdatePanel()
    {
        up_Section.Update();
        up_Grid.Update();
        up_Errors.Update();
    }
    public void ClearVariables()
    {
        SessionCategoryDetailsGrid = null;
        SessionFBTAssesseeCategory = null;
    }
    #endregion

    #region GridEvents
    protected void dg_Category_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_Assessee_Category = (DropDownList)e.Item.FindControl("ddl_Assessee_Category");
                dtp_Applicable = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("dtp_Applicable");
                txt_Eligible = (TextBox)e.Item.FindControl("txt_Eligible");
            }
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                BindAssesseCategory = SessionFBTAssesseeCategory;
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDS = SessionCategoryDetailsGrid;

                DataRow DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
                ddl_Assessee_Category.SelectedValue = DR["Assesse_Category_Id"].ToString();
                dtp_Applicable.SelectedDate = Convert.ToDateTime(DR["Applicable_From"]);
            }
        }
    }
    protected void dg_Category_Details_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "ADD")
        {
            errorMessage = "";
            objDS = SessionCategoryDetailsGrid;
            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                BindGrid();
                dg_Category_Details.EditItemIndex = -1;
                dg_Category_Details.ShowFooter = true;
            }
        }

    }
    protected void dg_Category_Details_EditCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_Category_Details.EditItemIndex = e.Item.ItemIndex;
        dg_Category_Details.ShowFooter = false;
        BindGrid();
    }
    protected void dg_Category_Details_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";

        objDS = SessionCategoryDetailsGrid;
        Insert_Update_Dataset(source, e);
        if (isValid == true)
        {
            dg_Category_Details.EditItemIndex = -1;
            dg_Category_Details.ShowFooter = true;
            BindGrid();
        }

    }
    protected void dg_Category_Details_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        dg_Category_Details.EditItemIndex = -1;
        dg_Category_Details.ShowFooter = true;
        BindGrid();
    }
    #endregion

    #region PageEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_Error.Text = "";
        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));
        objFBTCategoriesPresenter = new FBTCategoriesPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            hdf_ResourceString.Value = ObjCommon.GetResourceString("Finance/Masters/App_LocalResources/WucFBTCategories.ascx.resx");
        }
        SetUpdatePanel();
    }
    protected void ddl_FBT_Categories_SelectedIndexChanged(object sender, EventArgs e)
    {
        objFBTCategoriesPresenter.SetSection();
        objFBTCategoriesPresenter.FillGrid();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objFBTCategoriesPresenter.Save();
    }
    protected void dg_Category_Details_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        if (e.Item.ItemIndex != -1)
        {
            objDS = SessionCategoryDetailsGrid;
            objDS.Tables[0].Rows.RemoveAt(e.Item.ItemIndex);
            objDS.Tables[0].AcceptChanges();
            SessionCategoryDetailsGrid = objDS;
            dg_Category_Details.EditItemIndex = -1;
            dg_Category_Details.ShowFooter = true;
            BindGrid();
        }
    }
    #endregion
}
