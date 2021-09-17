using System;
using System.Data;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using Raj.EC .FinancePresenter;
using Raj.EC.FinanceView;

using System.Data.SqlClient;

/// <summary>
/// Created By    : Ankit Champaneriya 
/// Created On    : 16/10/2008
/// desctiption   : this page is for tds NATURE OF Payment details
/// Updated By    : Lad Aashish
/// Updated On    : 7th Jan 2009 
/// </summary>
/// 

public partial class FA_Common_Accounting_Masters_WucTdsNatureOfPayment : System.Web.UI.UserControl, ITdsNatureOfPaymentView
{
    #region ClassVariable
    TdsNatureOfPaymentPresenter objTdsNatureOfPaymentPresenter;
    TextBox txt_Exemption_Limit,txt_Rate;
    DropDownList ddl_Deductee_Type;
    ComponentArt.Web.UI.Calendar Picker_Applicable_From;
    bool isValid = false;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    DataSet objDS;
   
    LinkButton lbtn_Delete;
   

    #endregion

    #region ControlValue
   
    public DataSet Session_Payment_Details
    {
        get { return StateManager.GetState<DataSet>("Session_Payment_Details"); }
        set { StateManager.SaveState("Session_Payment_Details", value); 
        }
    }

    public DataSet Session_Nature_Payment
    {
        get { return StateManager.GetState<DataSet>("Session_Nature_Payment"); }
        set { StateManager.SaveState("Session_Nature_Payment", value); }
    }
    public string Section
    {
        get
        {
            return lbl_Section.Text;
        }
        set
        {
            lbl_Section.Text = value;
        }
    }

    public string Payment_Code
    {
        get
        {
            return lbl_Payment_Code.Text;
        }

        set
        {
            lbl_Payment_Code.Text = value;
        }
    }


    public int Nature_Payment_ID
    {
        get { return Util.String2Int(ddl_Nature.SelectedValue); }
        set { ddl_Nature.SelectedValue = Util.Int2String(value); }
    }

    public DataTable Session_Deductee_Type
    {
        get { return StateManager.GetState<DataTable>("Session_Deductee_Type"); }
        set { StateManager.SaveState("Session_Deductee_Type", value); }
    }
  
    #endregion

    #region ControlsBind
    public DataSet Bind_Payment_Grid
    {
        set
        {
            Session_Payment_Details = value;
            dg_Nature_Payment.DataSource = value;
            dg_Nature_Payment.DataBind();
        }
    }

    public DataSet Nature_Payment
    {
        set
        {
            ddl_Nature.DataSource = value;
            ddl_Nature.DataValueField = "TDS_Nature_of_Payment_Id";
            ddl_Nature.DataTextField = "TDS_Nature_of_Payment_Name";
            ddl_Nature.DataBind();
        }
    }
    public DataTable Bind_Deductee_Type
    {
        set
        {            
            ddl_Deductee_Type.DataSource = value;
            ddl_Deductee_Type.DataValueField = "TDS_Deductee_Type_ID";
            ddl_Deductee_Type.DataTextField = "TDS_Deductee_Type_Name";         
            ddl_Deductee_Type.DataBind();
            ddl_Deductee_Type.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    #endregion

    #region IView

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }

    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }

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
        objDS = Session_Payment_Details;
        DataRow DR = null;
        ddl_Deductee_Type = (DropDownList)e.Item.FindControl("ddl_Deductee_Type");
        Picker_Applicable_From = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("Picker_Applicable_From");

        txt_Exemption_Limit = (TextBox)e.Item.FindControl("txt_Exemption_Limit");
        txt_Rate = (TextBox)e.Item.FindControl("txt_Rate");               

        DataRow[] dr;
        isApplicable = CheckDeducteeTypeApplicableFrom(e);
        if (isApplicable == true)
        {
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
                DR["Nature_Payment_Id"] = Util.String2Int(ddl_Nature.SelectedValue);
                DR["TDS_Deductee_Type_Name"] = ddl_Deductee_Type.SelectedItem.Text;
                DR["TDS_Deductee_Type_ID"] = Util.String2Int(ddl_Deductee_Type.SelectedValue);
                DR["Applicable_From"] = String.Format("{0:dd MMM yyyy}", Picker_Applicable_From.SelectedDate);
                DR["Exemption_Limit"] = Util.String2Decimal(txt_Exemption_Limit.Text).ToString("0.000");
                DR["Rate"] = Util.String2Decimal(txt_Rate.Text).ToString("0.000");

                if (e.CommandName == "Add")
                {
                    objDS.Tables[0].Rows.Add(DR);
                }
                Session_Payment_Details = objDS;
            }
        }
    }
    private bool Allow_To_Add_Update()
    {
        if (Util.String2Int(ddl_Deductee_Type.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Deductee Type";
            ddl_Deductee_Type.Focus();
        }      
        else
            isValid = true;

        return isValid;
    }
    public void BindGrid()
    {
    
        dg_Nature_Payment.DataSource = Session_Payment_Details;
        dg_Nature_Payment.DataBind();

    }
    private bool CheckDeducteeTypeApplicableFrom(System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        objDS = Session_Payment_Details;
        DataRow [] dr_Array;
        int i;
        int _RowNo;
        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataSet ds_Temp = new DataSet();
            DataTable dt = new DataTable();
            DataView dv = new DataView();
           
            dv = ObjCommon.Get_View_Table(objDS.Tables[0], "TDS_Deductee_Type_ID= '" + ddl_Deductee_Type.SelectedValue + "'");
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
                            dr_Array = objDS.Tables[0].Select("Applicable_From='" + String.Format("{0:dd MMM yyyy}", Picker_Applicable_From.SelectedDate) + "'");
                            if (dr_Array.Length > 0)
                            {
                                _RowNo = objDS.Tables[0].Rows.IndexOf(dr_Array[0]);
                                if (_RowNo != e.Item.ItemIndex)
                                {
                                    errorMessage = "Duplicate Deductee Type and Applicable From";
                                    return false;
                                }
                            }
                        }           
                    }
                    else if (Picker_Applicable_From.SelectedDate == Convert.ToDateTime(objDS.Tables[0].Rows[i]["Applicable_From"]))
                    {
                         errorMessage = "Duplicate Deductee Type and Applicable From";
                         return false;
                    }
                 }
             }
                
           
            
        }
        return true;
    }
    private void SetUpdatePanel()
    {
        Upd_pnl_Errors.Update();
        Update_Nature.Update();
        Update_Payment.Update();
        Update_Section.Update();
    }
    public void ClearVariables()
    {
        Session_Nature_Payment = null;
        Session_Deductee_Type = null;
        Session_Payment_Details = null;
    }
    #endregion

    #region PageLoadEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Save.Attributes.Add("onclick", ObjCommon.ClickedOnceScript_For_JS_Validation(Page, btn_Save));
        objTdsNatureOfPaymentPresenter = new TdsNatureOfPaymentPresenter(this, IsPostBack);
        SetUpdatePanel();     
    }

    #endregion

    #region GridEvents
    protected void dg_Nature_Payment_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_Deductee_Type = (DropDownList)e.Item.FindControl("ddl_Deductee_Type");
                Picker_Applicable_From = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("Picker_Applicable_From");
                txt_Exemption_Limit = (TextBox)e.Item.FindControl("txt_Exemption_Limit");
                txt_Rate = (TextBox)e.Item.FindControl("txt_Rate");

            }
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                Bind_Deductee_Type = Session_Deductee_Type;
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDS = Session_Payment_Details;

                DataRow DR = objDS.Tables[0].Rows[e.Item.ItemIndex];                
                ddl_Deductee_Type.SelectedValue = DR["TDS_Deductee_Type_ID"].ToString();
                Picker_Applicable_From.SelectedDate =Convert.ToDateTime(DR["Applicable_From"]);
            }
        }
    }   

    protected void ddl_Nature_SelectedIndexChanged(object sender, EventArgs e)
    {
        objTdsNatureOfPaymentPresenter.fill_Nature_Payment_Grid();       
    }
    protected void dg_Nature_Payment_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            errorMessage = "";
            objDS = Session_Payment_Details;
                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindGrid();
                    dg_Nature_Payment.EditItemIndex = -1;
                    dg_Nature_Payment.ShowFooter = true;
                }
         }      
    }

    protected void dg_Nature_Payment_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        
            objDS = Session_Payment_Details;
            Insert_Update_Dataset(source, e);
            if (isValid == true)
            {
                dg_Nature_Payment.EditItemIndex = -1;
                dg_Nature_Payment.ShowFooter = true;
                BindGrid();
            }
    }
    protected void dg_Nature_Payment_EditCommand(object source, DataGridCommandEventArgs e)
    { 

        errorMessage = "";
        lbtn_Delete = (LinkButton)e.Item.FindControl("lbtn_Delete");
        lbtn_Delete.Enabled = false;
        dg_Nature_Payment.EditItemIndex = e.Item.ItemIndex;
        dg_Nature_Payment.ShowFooter = false;
        BindGrid();
    }
    protected void dg_Nature_Payment_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        errorMessage = "";
        dg_Nature_Payment.EditItemIndex = -1;
        dg_Nature_Payment.ShowFooter = true;
        BindGrid();


    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objTdsNatureOfPaymentPresenter.Save();
    }
 
    protected void dg_Nature_Payment_DeleteCommand(object source, DataGridCommandEventArgs e)
    {     
        errorMessage = "";
        if (e.Item.ItemIndex != -1)
        {
            objDS = Session_Payment_Details;
            objDS.Tables[0].Rows.RemoveAt(e.Item.ItemIndex);
            objDS.Tables[0].AcceptChanges();
            Session_Payment_Details = objDS;
            dg_Nature_Payment.EditItemIndex = -1;
            dg_Nature_Payment.ShowFooter = true;
            BindGrid();
        }
    }

    #endregion
}

   


