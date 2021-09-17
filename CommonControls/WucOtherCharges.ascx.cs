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
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class CommonControls_WucOtherCharges : System.Web.UI.UserControl
{
    #region ClassVariables
    Common objCommon = new Common();
    int ItemIndex = 0;    
    int RowNo;
    TextBox txt_Description, txt_Amount;
    DropDownList ddl_OtherChargesHead;
    HiddenField hdn_SrNo;
    bool isValid = false;   
    DataTable objDT;
    DataRow DR = null;
    string Query,Query1;
    DataSet objDS,DS;
    DAL obj;
    Label lbl_OtherCharges;
    #endregion

    #region ControlsBind

    public DataSet SessionDDLTransportChargesHead
    {
        set { StateManager.SaveState("DDLTransportChargesHead", value); }
        get { return StateManager.GetState<DataSet>("DDLTransportChargesHead"); }
    }

    public DataSet SessionOtherChargesGrid
    {
        set { StateManager.SaveState("SessionOtherChargeGrid", value); }
        get { return StateManager.GetState<DataSet>("SessionOtherChargeGrid"); }
    }

    private void Bind_Grid()
    {
       
        Calculate_Srno();
        dg_TransportOtherCharges.DataSource = SessionOtherChargesGrid;
        dg_TransportOtherCharges.DataBind();

        if (IsPostBack)
        {
            //updatesession();
            updateparentcontrol();
            //updateparentcontrol(Other_Charges);

        }
    }

    private void Calculate_Srno()
    {
        for (int i = 0; i <= SessionOtherChargesGrid.Tables[0].Rows.Count - 1; i++)
        {
            SessionOtherChargesGrid.Tables[0].Rows[i]["Sr_No"] = i;
        }
    }

    #endregion

    #region controlsValue

    public int MenuItemId
    {
        get { return Util.DecryptToInt(Request.QueryString["Menu_Item_ID"]); }
    }
    public int btnCanEdit
    {
        get { return Util.String2Int(Request.QueryString["btnCanEdit"]); }
    }

    #endregion

    #region OtherMethods

    public string OtherChargesDetails
    {

        get
        {
            //DataSet _objDs = new DataSet();
            //_objDs.Tables.Add(SessionOtherChargesGrid.Copy());
            //_objDs.Tables[0].TableName = "SessionOtherChargeGrid";
            //return _objDs.GetXml();
            return SessionOtherChargesGrid.GetXml();
           
        }
    }


    //private DataSet makeDS()
    //{

    //    DataSet objDS;

    //    objDS = SessionOtherChargesGrid;



    //    DataRow objDR = objDS.Tables[0].NewRow();

    //    objDR["GC_Other_Charge_Head_ID"] = ddl_OtherChargesHead.SelectedValue;
    //    objDR["GC_Other_Charge_Head"] = ddl_OtherChargesHead.SelectedItem.Text;
    //    objDR["Description"] = txt_Description.Text;
    //    objDR["Amount"] = txt_Amount.Text;
        

    //    objDS.Tables[0].Rows.Add(objDR);
    //    SessionOtherChargesGrid = objDS;
    //    return objDS;



    //}
    #endregion

    #region FillChargeHead

    private void Fill_ChargeHead()
    {
        SessionDDLTransportChargesHead = objCommon.Get_Values_Where("EC_Master_GC_Other_Charge_Head", "GC_Other_Charge_Head_ID,GC_Other_Charge_Head", "Is_Active =1", "GC_Other_Charge_Head", true);
        //Query1 = "Select GC_Other_Charge_Head_ID,GC_Other_Charge_Head from EC_Master_GC_Other_Charge_Head where Is_active=1";
        //DS = obj.RunQuery(Query1);
        //SessionDDLTransportChargesHead = DS;
    }
    #endregion

    #region IView

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public bool validateUI()
    {
        bool _Is_Valid = true;
        return _Is_Valid;
    }
    private string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }


    #endregion

   #region ControlEvents

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Fill_ChargeHead();

            if (StateManager.IsValidSession("SessionOtherChargeGrid") == false)
            {
                Query = "select Other_Charges_ID as 'Sr_No',GC_Other_Charge_Head as Other_Charge_Head,* from EC_Opr_VTrans_Other_Charge_Details left outer join EC_Master_GC_Other_Charge_Head on EC_Master_GC_Other_Charge_Head.GC_Other_Charge_Head_ID=EC_Opr_VTrans_Other_Charge_Details.Other_Charge_Head_ID where Document_ID= " + keyID + "and  MenuItem_ID= " + MenuItemId;
                objDS = objCommon.EC_Common_Pass_Query(Query);
                SessionOtherChargesGrid = objDS;
            }

            Bind_Grid();

           
        }
        if (btnCanEdit == 0)
        {
            dg_TransportOtherCharges.Enabled = false;
        }
    }

    protected void btn_Exit_Click(object sender, EventArgs e)
    {

        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

#endregion

    #region OtherMethod   

    private void updateparentcontrol()
    {
        decimal Other_Charges;
        Other_Charges = 0;

        if (SessionOtherChargesGrid.Tables[0].Rows.Count > 0)
        {
            Other_Charges = Util.String2Decimal(SessionOtherChargesGrid.Tables[0].Compute("Sum(Amount)", "").ToString());
        }
        string popupScript = "<script language='javascript'>updateparentcontrol('" + Other_Charges.ToString() + "');</script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(up_lbl_Errors, typeof(String), "PopupScript", popupScript.ToString(), false);
       

    }

    #endregion

    #region GridEvents

    protected void dg_TransportOtherCharges_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_TransportOtherCharges.EditItemIndex = -1;
        dg_TransportOtherCharges.ShowFooter = true;
        Bind_Grid();
    }

    protected void dg_TransportOtherCharges_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_TransportOtherCharges.EditItemIndex = e.Item.ItemIndex;
        dg_TransportOtherCharges.ShowFooter = false;
        Bind_Grid();
    }

    protected void dg_TransportOtherCharges_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
            try
            {
                objDS = SessionOtherChargesGrid;
                DataColumn[] _dtColumnPrimaryKey;
                _dtColumnPrimaryKey = new DataColumn[1];
                _dtColumnPrimaryKey[0] = objDS.Tables[0].Columns["Other_Charge_Head_ID"];
                objDS.Tables[0].PrimaryKey = _dtColumnPrimaryKey;

                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    Bind_Grid();
                    dg_TransportOtherCharges.EditItemIndex = -1;
                    dg_TransportOtherCharges.ShowFooter = true;
                }
            }
            catch (ConstraintException)
            {
                errorMessage = "Duplicate Charges Head";
                scm_OtherCharges.SetFocus(ddl_OtherChargesHead);
                //ddl_OtherChargesHead.Focus();
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
                lbl_OtherCharges = (Label)(e.Item.FindControl("lbl_OtherChargeHead"));

                ddl_OtherChargesHead.DataSource = SessionDDLTransportChargesHead;
                ddl_OtherChargesHead.DataTextField = "GC_Other_Charge_Head";
                ddl_OtherChargesHead.DataValueField = "GC_Other_Charge_Head_ID";
                ddl_OtherChargesHead.DataBind();
            }

            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDS = SessionOtherChargesGrid;

                RowNo = Util.String2Int(hdn_SrNo.Value);
                DR = objDS.Tables[0].Rows[RowNo];

                hdn_SrNo.Value = DR["Sr_No"].ToString();
                ddl_OtherChargesHead.SelectedValue = DR["Other_Charge_Head_ID"].ToString();
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

        objDS = SessionOtherChargesGrid;

        if (e.CommandName == "Add")
        {
            DR = objDS.Tables[0].NewRow();
        }
        else if (e.CommandName == "Update")
        {
            RowNo = Util.String2Int(hdn_SrNo.Value);
            DR = objDS.Tables[0].Rows[RowNo];
        }

        if (Allow_To_Add_Update() == true)
        {
            
            DR["Other_Charge_Head_ID"] = ddl_OtherChargesHead.SelectedValue;
            DR["Other_Charge_Head"] = ddl_OtherChargesHead.SelectedItem;
            DR["Description"] = txt_Description.Text;
            DR["Amount"] = txt_Amount.Text;
            DR["MenuItem_ID"] = MenuItemId;
            DR["Document_ID"] = keyID;

            if (e.CommandName == "Add")
            { objDS.Tables[0].Rows.Add(DR); }
            SessionOtherChargesGrid = objDS;
            //DataTable objDT1 = (DataTable)Session("objDT");
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
            objDS = SessionOtherChargesGrid;

            DataColumn[] _dtColumnPrimaryKey;
            _dtColumnPrimaryKey = new DataColumn[1];
            _dtColumnPrimaryKey[0] = objDS.Tables[0].Columns["Other_Charge_Head_ID"];           
            objDS.Tables[0].PrimaryKey = _dtColumnPrimaryKey;

            Insert_Update_Dataset(source, e);

            if (isValid == true)
            {
                dg_TransportOtherCharges.EditItemIndex = -1;
                dg_TransportOtherCharges.ShowFooter = true;
                Bind_Grid();
            }
        }
        catch (ConstraintException)
        {
            errorMessage = "Duplicate Charges Head";
            scm_OtherCharges.SetFocus(ddl_OtherChargesHead);
        }
    }

    protected void dg_TransportOtherCharges_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
           objDS = SessionOtherChargesGrid;

            hdn_SrNo = (HiddenField)(e.Item.FindControl("hdn_SrNo"));
            RowNo = Util.String2Int(hdn_SrNo.Value);

            objDS.Tables[0].Rows.RemoveAt(RowNo);
            objDS.AcceptChanges();
            SessionOtherChargesGrid =objDS;
            dg_TransportOtherCharges.EditItemIndex = -1;
            dg_TransportOtherCharges.ShowFooter = true;
            Bind_Grid();
        }
    }

    #endregion   
}
