using System;
using System.Data;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;

using System.Data.SqlClient;

/// <summary>
/// Created By    : Ankit Champaneriya 
/// Created On    : 17/10/2008
/// description   : TDS Deductee type details
/// Updated By    : Aashish Lad
/// Updated On    : 7th Jan 2009 
/// </summary>
/// 
public partial class FA_Common_Accounting_Masters_WucTDSDeducteeType : System.Web.UI.UserControl,ITDSDeducteeTypeView
{
    #region ClassVariable
    TDSDeducteeTypePresenter objTDSDeducteeTypePresenter;
    TextBox txt_Exemption_Limit, txt_Addl_Surcharge, txt_Surcharge, txt_Edu_Add_Education;  
    ComponentArt.Web.UI.Calendar Picker_Applicable_From;
    bool isValid = false;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    DataSet objDS;    
    LinkButton lbtn_Delete;
   
    #endregion

    #region ControlsValue
    public DataSet Session_Deductee_Details
    {
        get { return StateManager.GetState<DataSet>("Deductee_Details"); }
        set { StateManager.SaveState("Deductee_Details", value); }
    }

    public DataTable Session_Deductee_Type
    {
        get { return StateManager.GetState<DataTable>("Deductee_Type"); }
        set { StateManager.SaveState("Deductee_Type", value); }
    }
    public int Deductee_Type_ID
    {
        get { return Util.String2Int(ddl_Deductee_Type.SelectedValue); }
        set { ddl_Deductee_Type.SelectedValue = Util.Int2String(value); }
    }

    public int Resedential_Status
    {
        get { return Util.String2Int(ddl_Residential_Status.SelectedValue); }
        set { ddl_Residential_Status.SelectedValue = Util.Int2String(value); }
    }

    public int Deductee_Status
    {
        get { return Util.String2Int(ddl_Deductee_Status.SelectedValue); }
        set { ddl_Deductee_Status.SelectedValue = Util.Int2String(value); }
    }
  
    #endregion

    #region ControlsBind
    public DataTable Deductee_Type
    {
        set
        {
            ddl_Deductee_Type.DataSource = value;
            ddl_Deductee_Type.DataValueField = "TDS_Deductee_Type_ID";
            ddl_Deductee_Type.DataTextField = "TDS_Deductee_Type_Name";
            ddl_Deductee_Type.DataBind();
        }
    }

    public DataSet Bind_Deductee_Grid
    {
        set
        {
            dg_Deuctee_TDS.DataSource = value;
            dg_Deuctee_TDS.DataBind();
        }
    }

    #endregion
    
    #region IView

    public int keyID
    {
        get { return Util.String2Int(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }

    public bool validateUI()
    {
        return true;
    }

    #endregion
    #region Other Methods
    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        bool isApplicable = false;
        objDS = Session_Deductee_Details;
        DataRow DR = null;
        Picker_Applicable_From = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("Picker_Applicable_From");
        txt_Exemption_Limit = (TextBox)e.Item.FindControl("txt_Exemption_Limit");
        txt_Surcharge = (TextBox)e.Item.FindControl("txt_Surcharge");
        txt_Addl_Surcharge = (TextBox)e.Item.FindControl("txt_Addl_Surcharge");
        txt_Edu_Add_Education = (TextBox)e.Item.FindControl("txt_Edu_Add_Education");
       
            if (e.CommandName == "Add")
            {
                DR = objDS.Tables[0].NewRow();
            }

            if (e.CommandName == "Update")
            {
                DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
            }

            if (Allow_To_Add_Update() == true)
            {
              
                DR["TDS_Deductee_Type_Id"] = ddl_Deductee_Type.SelectedValue;
                DR["Exemption_Limit"] = Util.String2Decimal(txt_Exemption_Limit.Text).ToString("0.000");
                DR["Applicable_From"] = String.Format("{0:dd MMM yyyy}", Picker_Applicable_From.SelectedDate);
                DR["Surcharge"] = Util.String2Decimal(txt_Surcharge.Text).ToString("0.000");
                DR["Addl_Surcharge_Cess"] = Util.String2Decimal(txt_Addl_Surcharge.Text).ToString("0.000");
                DR["Addl_Education_Cess"] = Util.String2Decimal(txt_Edu_Add_Education.Text).ToString("0.000");

                if (e.CommandName == "Add")
                {
                    objDS.Tables[0].Rows.Add(DR);
                }
                Session_Deductee_Details = objDS;
            }

    }
    private bool Allow_To_Add_Update()
    {
            isValid = true;

        return isValid;
    }
    public void BindGrid()
    {

        dg_Deuctee_TDS.DataSource = Session_Deductee_Details;
        dg_Deuctee_TDS.DataBind();

    }
    private void SetUpdatePanel()
    {
        Update_Deductee.Update();
        Update_Deductee_Status.Update();
        Update_Residence.Update();
        UpdatePanel1.Update();
    }
    public void ClearVariables()
    {
        Session_Deductee_Details = null;
        Session_Deductee_Type = null;
    }
     #endregion


    #region PageEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));
       objTDSDeducteeTypePresenter = new TDSDeducteeTypePresenter(this, IsPostBack);     
    }

    #endregion

    #region GridEvents
    protected void dg_Deuctee_TDS_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                Picker_Applicable_From = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("Picker_Applicable_From");
                txt_Exemption_Limit = (TextBox)e.Item.FindControl("txt_Exemption_Limit");
                txt_Surcharge = (TextBox)e.Item.FindControl("txt_Surcharge");
                txt_Addl_Surcharge = (TextBox)e.Item.FindControl("txt_Addl_Surcharge");
                txt_Edu_Add_Education = (TextBox)e.Item.FindControl("txt_Edu_Add_Education");
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDS = Session_Deductee_Details;
                DataRow DR = objDS.Tables[0].Rows[e.Item.ItemIndex];
           
                Picker_Applicable_From.SelectedDate = Convert.ToDateTime(DR["Applicable_From"]);
            }
           
        }
    }
    protected void dg_Deuctee_TDS_EditCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_Deuctee_TDS.EditItemIndex = e.Item.ItemIndex;
        dg_Deuctee_TDS.ShowFooter = false;
        BindGrid();
    }
    protected void dg_Deuctee_TDS_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        try
        {
            objDS = Session_Deductee_Details;
            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                dg_Deuctee_TDS.EditItemIndex = -1;
                dg_Deuctee_TDS.ShowFooter = true;
                BindGrid();
            }
        }
        catch (ConstraintException)
        {
            errorMessage = "Duplicate Applicable From";
            return;
        }
    }


    protected void dg_Deuctee_TDS_ItemCommand(object source, DataGridCommandEventArgs e)
    {
           errorMessage = "";
           try
           {
               if (e.CommandName == "Add")
               {

                   objDS = Session_Deductee_Details;
                   Insert_Update_Dataset(source, e);
                   if (isValid == true)
                   {
                       BindGrid();
                       dg_Deuctee_TDS.EditItemIndex = -1;
                       dg_Deuctee_TDS.ShowFooter = true;
                   }
               }
           }
           catch (ConstraintException)
           {
               errorMessage = "Duplicate Applicable From";               
               return;
           }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objTDSDeducteeTypePresenter.Save();
    }
    protected void ddl_Deductee_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        objTDSDeducteeTypePresenter.FillGrid();
    }
    protected void dg_Deuctee_TDS_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        dg_Deuctee_TDS.EditItemIndex = -1;
        dg_Deuctee_TDS.ShowFooter = true;
        BindGrid();
    }
    protected void dg_Deuctee_TDS_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        if (e.Item.ItemIndex != -1)
        {
            objDS = Session_Deductee_Details;
            objDS.Tables[0].Rows.RemoveAt(e.Item.ItemIndex);
            objDS.Tables[0].AcceptChanges();
            Session_Deductee_Details = objDS;
            dg_Deuctee_TDS.EditItemIndex = -1;
            dg_Deuctee_TDS.ShowFooter = true;
            BindGrid();
        }
    }
    #endregion
}
