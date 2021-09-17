using System;
using System.Data;
using System.Web.UI.WebControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Finance_Accounting_Vouchers_FrmTransportBillOtherCharge : System.Web.UI.Page //ClassLibraryMVP.UI.Page
{
    #region ClassVariables
    Common objCommon = new Common();
    int ItemIndex = 0;
    int GCID = 0;
    int RowNo;
    TextBox txt_Description,txt_Amount;
    DropDownList ddl_OtherChargesHead;
    HiddenField hdn_SrNo;
    bool isValid = false;
    string Mode;
    int QueryString;
    DataTable objDT;
    DataRow DR = null;
    DataTable SessionDT = null;
    DataTable SessionOtherChargeDT = null;
    PageControls pc = new PageControls();
    string attached;
    #endregion

    #region ControlsBind
    
    public DataSet SessionDDLTransportChargesHead
    {
        set { StateManager.SaveState("DDLTransportChargesHead", value); }
        get { return StateManager.GetState<DataSet>("DDLTransportChargesHead"); }
    }

    private void Bind_Grid(int GCID)
    {
        string Condition = "";
        if(Util.String2Bool(hdn_IsFrom_GC.Value) == true)
        {
            Condition = "gc_id=" + GCID.ToString() + " and Is_GC_Other_Charge=1";
        }
        else
        {
            Condition = "gc_id=" + GCID.ToString() + " and Is_GC_Other_Charge=0";
        }

        Calculate_Srno();

        dg_TransportOtherCharges.DataSource = objCommon.Get_View_Table(SessionOtherChargeDT, Condition);
        dg_TransportOtherCharges.DataBind();

        if (IsPostBack)
        {
            update_Session();
            updateparentdataset();
        }       
    }

    private void Calculate_Srno()
    {
        for (int i = 0; i <= SessionOtherChargeDT.Rows.Count - 1;i++ )
        {
            SessionOtherChargeDT.Rows[i]["Sr_No"] = i;
        }
    }
  
    #endregion
    #region FillChargeHead

    private void Fill_ChargeHead()
    {
        SessionDDLTransportChargesHead = objCommon.Get_Values_Where("EC_Master_GC_Other_Charge_Head", "GC_Other_Charge_Head_ID,GC_Other_Charge_Head", "Is_Active =1", "GC_Other_Charge_Head", true);
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
        ItemIndex = Util.DecryptToInt(Request.QueryString["ItemIndex"].ToString());
        GCID = Util.DecryptToInt(Request.QueryString["GCID"].ToString());
        //Mode = Util.DecryptToInt(Request.QueryString["Mode"].ToString());
        Mode = Request.QueryString["Mode"].ToString();
        QueryString = Util.DecryptToInt(Mode);

        attached = Request.QueryString["Attched"];

        hdn_IsFrom_GC.Value = Request.QueryString["IsFromGC"].ToString();

        SessionDT = StateManager.GetState<DataTable>("BindBillGrid");
        SessionOtherChargeDT = StateManager.GetState<DataTable>("SessionBillOtherChargeGrid");

        if (QueryString == 4) //For View Mode
        {
            dg_TransportOtherCharges.Enabled = false;
        }
        if (!IsPostBack)
        {
            Fill_ChargeHead();
            Bind_Grid(GCID);
        }
    }    

    #region GridEvents

    protected void dg_TransportOtherCharges_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_TransportOtherCharges.EditItemIndex = -1;
        dg_TransportOtherCharges.ShowFooter = true;
        Bind_Grid(GCID);
    }
      
    protected void dg_TransportOtherCharges_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_TransportOtherCharges.EditItemIndex = e.Item.ItemIndex;
        dg_TransportOtherCharges.ShowFooter = false;
        Bind_Grid(GCID);
    }

    protected void dg_TransportOtherCharges_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                objDT = SessionOtherChargeDT;
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[3];
                _dtColumnPrimaryKey[0] = objDT.Columns["GC_Other_Charge_Head_ID"];
                _dtColumnPrimaryKey[1] = objDT.Columns["GC_ID"];
                _dtColumnPrimaryKey[2] = objDT.Columns["Is_GC_Other_Charge"];
                objDT.PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    Bind_Grid(GCID);
                    dg_TransportOtherCharges.EditItemIndex = -1;
                    dg_TransportOtherCharges.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Dupliacte Charges Head";
                scm_OtherCharges.SetFocus(ddl_OtherChargesHead);
            }
        }
    }

    protected void dg_TransportOtherCharges_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_OtherChargesHead = (DropDownList)(e.Item.FindControl("ddl_OtherChargesHead"));
                txt_Description = (TextBox)(e.Item.FindControl("txt_Description"));
                txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
                hdn_SrNo = (HiddenField)(e.Item.FindControl("hdn_SrNo"));

                ddl_OtherChargesHead.DataTextField = "GC_Other_Charge_Head";
                ddl_OtherChargesHead.DataValueField = "GC_Other_Charge_Head_ID";
                ddl_OtherChargesHead.DataSource = SessionDDLTransportChargesHead;
                ddl_OtherChargesHead.DataBind();
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionOtherChargeDT;

                RowNo = Util.String2Int(hdn_SrNo.Value);
                DR = objDT.Rows[RowNo];

                hdn_SrNo.Value = DR["Sr_No"].ToString();
                ddl_OtherChargesHead.SelectedValue = DR["GC_Other_Charge_Head_ID"].ToString();
                txt_Description.Text = DR["Description"].ToString();
                txt_Amount.Text = DR["Amount"].ToString();
            }
        }
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        ddl_OtherChargesHead = (DropDownList)(e.Item.FindControl("ddl_OtherChargesHead"));
        txt_Description = (TextBox)(e.Item.FindControl("txt_Description"));
        txt_Amount = (TextBox)(e.Item.FindControl("txt_Amount"));
        hdn_SrNo = (HiddenField)(e.Item.FindControl("hdn_SrNo"));

        objDT = SessionOtherChargeDT;

        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            RowNo = Util.String2Int(hdn_SrNo.Value);
            DR = objDT.Rows[RowNo];
        }

        if (Allow_To_Add_Update() == true)
        {
            DR["Is_GC_Other_Charge"] = hdn_IsFrom_GC.Value == "true" ? 1 : 0;
            DR["GC_ID"] = GCID.ToString();
            DR["GC_Other_Charge_Head_ID"] = ddl_OtherChargesHead.SelectedValue;
            DR["GC_Other_Charge_Head"] = ddl_OtherChargesHead.SelectedItem;
            DR["Description"] = txt_Description.Text;
            DR["Amount"] = txt_Amount.Text;

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            SessionOtherChargeDT = objDT;
        }
    }   

    private bool Allow_To_Add_Update()
    {
        lbl_Errors.Text = "";

        if (Util.String2Int(ddl_OtherChargesHead.SelectedValue) <= 0)
        {
            errorMessage = "Please Select Charges Head";
            scm_OtherCharges.SetFocus(ddl_OtherChargesHead);
        }
        else if (txt_Description.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Description";
            scm_OtherCharges.SetFocus(txt_Description);
        }
        else if (txt_Amount.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Amount";
            scm_OtherCharges.SetFocus(txt_Amount);
        }        
        else
        {
            isValid = true;
        }

        return isValid;
    }

    protected void dg_TransportOtherCharges_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            objDT = SessionOtherChargeDT;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[3];
            _dtColumnPrimaryKey[0] = objDT.Columns["GC_Other_Charge_Head_ID"];
            _dtColumnPrimaryKey[1] = objDT.Columns["GC_ID"];
            _dtColumnPrimaryKey[2] = objDT.Columns["Is_GC_Other_Charge"];
            objDT.PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset(source, e);

            if (isValid == true)
            {
                dg_TransportOtherCharges.EditItemIndex = -1;
                dg_TransportOtherCharges.ShowFooter = true;
                Bind_Grid(GCID);
            }
        }
        catch (ConstraintException)
        {
            errorMessage = "Dupliacte Charges Head";
            scm_OtherCharges.SetFocus(ddl_OtherChargesHead);
        }
    }

    protected void dg_TransportOtherCharges_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionOtherChargeDT;

            hdn_SrNo = (HiddenField)(e.Item.FindControl("hdn_SrNo"));
            RowNo = Util.String2Int(hdn_SrNo.Value);

            objDT.Rows.RemoveAt(RowNo);
            objDT.AcceptChanges();
            SessionOtherChargeDT = objDT;
            dg_TransportOtherCharges.EditItemIndex = -1;
            dg_TransportOtherCharges.ShowFooter = true;
            Bind_Grid(GCID);
        }
    }

    #endregion   
   
    private void updateparentdataset()
    {
        string popupScript = "<script language='javascript'>updateparentdataset();</script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(up_lbl_Errors, typeof(String), "PopupScript", popupScript.ToString(), false);
    }

    private void update_Session()
    {
        string Condition = "";
        
        if (Util.String2Bool(hdn_IsFrom_GC.Value) == true)
        {
            Condition = "gc_id=" + GCID.ToString() + " and Is_GC_Other_Charge=1";
        }
        else
        {
            Condition = "gc_id=" + GCID.ToString() + " and Is_GC_Other_Charge=0";
        }

        decimal Other_Charges = Util.String2Decimal(SessionOtherChargeDT.Compute("Sum(Amount)", Condition).ToString());

        if (Other_Charges <= 0) Other_Charges = 0;

        if (Util.String2Bool(hdn_IsFrom_GC.Value) == true)
        {
            SessionDT.Rows[ItemIndex]["GC_Other_Charges"] = Other_Charges;
        }
        else
        {
            SessionDT.Rows[ItemIndex]["FA_Other_Charges"] = Other_Charges;

            if (attached != null)
                SessionDT.Rows[ItemIndex]["Att"] = Util.String2Bool(attached);

        }
    }

    protected void btn_Exit_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
}
