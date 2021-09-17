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
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
using ClassLibraryMVP.General;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 16/10/2008
/// Description   : This Page is For Account Transfer Operation
/// </summary>
/// 

public partial class Operations_Booking_WucAccountTransfer : System.Web.UI.UserControl,IAccountTransferView
{
   #region ClassVariables
    AccountTransferPresenter objAccountTransferPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable();
    string _flag = "";
    string Mode = "0";
    #endregion

   #region ControlsValues
   
    public int VAId
    {
        get { return Util.String2Int(ddl_Associate_Name.SelectedValue); }
        set { ddl_Associate_Name.SelectedValue = Util.Int2String(value); }
    }  
    public DateTime AccountTransferDate
    {
        set { AT_Date.SelectedDate = value; }
        get { return Convert.ToDateTime(AT_Date.SelectedDate); }
    }
    public int Total_GC
    {
        set { hdn_Total_GC.Value = Util.Int2String(value); }
    }
    public int Total_Article
    {
        set { hdn_Total_Articles.Value = Util.Int2String(value); }
    }
    public Decimal Total_Weight
    {
        set { hdn_Total_Wt.Value = Util.Decimal2String(value); }
    }
    public Decimal Total_Freight
    {
        set { hdn_Tot_Basic_Fret.Value = Util.Decimal2String(value); }
    }
    public Decimal Total_ServiceTax
    {
        set { hdn_Total_Ser_Tax.Value = Util.Decimal2String(value); }
    }
    public Decimal Total_GCAmount
    {
        set { hdn_Total_GC_Amt.Value = Util.Decimal2String(value); }
    }
    public string AccountTransferNo
    {
        set { lbl_Account_Transfer_No.Text = value; }
    }
    public string Flag
    {
        get { return _flag; }
    }
    private string BranchName
    {
        set { lbl_Branch.Text = value; }
    }
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }

 #endregion

    #region ControlsBind

    public DataTable BindVA
    {
        set
        {
            ddl_Associate_Name.DataTextField = "VA_Name";
            ddl_Associate_Name.DataValueField = "VA_ID";
            ddl_Associate_Name.DataSource = value;
            ddl_Associate_Name.DataBind();
            //if (keyID <= 0)
            //{
            //    ddl_Associate_Name.Items.Insert(0, new ListItem("Select One", "0"));
            //}
        }
    }

    public void BindAccountTransferGrid()
    {
        dg_AccountTransfer.DataSource = SessionBindAccountTransferGrid;
        dg_AccountTransfer.DataBind(); 
    }

    public DataTable SessionBindAccountTransferGrid
    {
        get { return StateManager.GetState<DataTable>("BindAccountTransferGrid"); }
        set 
        { 
            StateManager.SaveState("BindAccountTransferGrid", value);

            if (StateManager.Exist("BindAccountTransferGrid"))
                BindAccountTransferGrid();
        }
    }

    public String AccountTransferDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBindAccountTransferGrid.Copy());
            _objDs.Tables[0].TableName = "AccountTransferGrid_Details";
            return _objDs.GetXml().ToLower();
        }
    }

    #endregion

    #region OtherMethod
    private void calculate_griddetails()
    {
        DataSet ds_filtered = new DataSet();
        hdn_Total_GC.Value = "0";

        CheckBox chk;
        int row,i;

        if (dg_AccountTransfer.Items.Count > 0)
        {
            objDT = SessionBindAccountTransferGrid;

            for (i = 0; i <= dg_AccountTransfer.Items.Count - 1; i++)
            {
                row = (dg_AccountTransfer.PageSize * dg_AccountTransfer.CurrentPageIndex) + i;
                chk = (CheckBox)dg_AccountTransfer.Items[i].FindControl("Chk_Attach");
                objDT.Rows[row]["Att"] = chk.Checked;
                if(chk.Checked == true)
                {
                    hdn_Total_GC.Value = Util.Int2String(Util.String2Int(hdn_Total_GC.Value) + 1);
                }
            }
            DataView view = objComm.Get_View_Table(objDT, "Att = true");
             ds_filtered.Tables.Add(view.ToTable());

             SessionBindAccountTransferGrid = ds_filtered.Tables[0];
        }

    }

    private void Assign_Hidden_Values_To_TextBox()
    {
        txt_Total_GC.Text = hdn_Total_GC.Value;
        txt_Total_Articles.Text = hdn_Total_Articles.Value;
        txt_Total_Basic_Freight.Text = hdn_Tot_Basic_Fret.Value;
        txt_Total_Wt.Text = hdn_Total_Wt.Value;
        txt_Total_GC_Amt.Text = hdn_Total_GC_Amt.Value;
        txt_Total_Ser_Tax.Text = hdn_Total_Ser_Tax.Value;
    }

    private void Assign_Hidden_Values_For_Reset()
    {
        hdn_Total_GC.Value = "0";
        hdn_Total_Articles.Value = "0";
        hdn_Total_Wt.Value = "0";
        hdn_Tot_Basic_Fret.Value = "0";
        hdn_Total_GC_Amt.Value = "0";
        hdn_Total_Ser_Tax.Value = "0";

        txt_Total_GC.Text = "0";
        txt_Total_Articles.Text = "0";
        txt_Total_Basic_Freight.Text = "0";
        txt_Total_Wt.Text = "0";
        txt_Total_GC_Amt.Text = "0";
        txt_Total_Ser_Tax.Text = "0";
    }

    private void Next_AccountTansfer_Number()
    {
        AccountTransferNo = objComm.Get_Next_Number();
    }

     private void Reset_Values_To_Inital()
     {
        Assign_Hidden_Values_For_Reset();
        dg_AccountTransfer.DataSource = null;
        dg_AccountTransfer.DataBind();
        errorMessage = "";
        lbl_Error_Client.Text = "";
     }
   #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        //if (VAId == 0)
        //{
        //    errorMessage = GetLocalResourceObject("Msg_ddl_Associates").ToString();
        //    ddl_Associate_Name.Focus();
        //}
        //else 
        if (Datemanager.IsValidProcessDate("OPR_AT", AccountTransferDate) == false)
        {
            errorMessage = GetLocalResourceObject("Msg_dtp_ATDate").ToString();
        }
        else if (Util.String2Int(hdn_Total_GC.Value) == 0)
        {
            errorMessage = GetLocalResourceObject("Msg_grid_Validation").ToString();
        }
        //else if (Remarks  == string.Empty)
        //{
        //    errorMessage = GetLocalResourceObject("Msg_txt_Remarks").ToString();
        //    txt_Remarks.Focus();
        //}
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }
    //Added : Anita On: 19 Feb 09
    public void ClearVariables()
    {
        SessionBindAccountTransferGrid = null;
    }
    #endregion

    #region ControlsValues
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            TD_Calender.Visible = false;
            AT_Date.Enabled = false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SetStandardCaption();
        BranchName = UserManager.getUserParam().MainName;
       
        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save,btn_Save_Exit,btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit,btn_Save,btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());


        if (!IsPostBack)
        {
            Assign_Hidden_Values_For_Reset();

            hdf_ResourecString.Value = objComm.GetResourceString("Operations/Booking/App_LocalResources/WucAccountTransfer.ascx.resx");
            if (keyID <= 0)
            {
                Next_AccountTansfer_Number();
            }
        }
        objAccountTransferPresenter = new AccountTransferPresenter(this, IsPostBack);

        Assign_Hidden_Values_To_TextBox();
        
    }

    private void SetStandardCaption()
    {
        const int GcNoCaption = 2;
        const int GCAmountCaption = 12;
        //change userManager to CompanyManager by Ankit

        lbl_TotalGC.Text = "Total  " + CompanyManager.getCompanyParam().GcCaption + ":";
        lbl_TotalAmount.Text = "Total  " + CompanyManager.getCompanyParam().GcCaption + "  Amount:";

        dg_AccountTransfer.Columns[GcNoCaption].HeaderText = CompanyManager.getCompanyParam().GcCaption + "  No";
        dg_AccountTransfer.Columns[GCAmountCaption].HeaderText = "Total  " + CompanyManager.getCompanyParam().GcCaption + "  Amount";
    }

    protected void ddl_Associate_Name_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reset_Values_To_Inital();
        objAccountTransferPresenter.fillgrid();
        Assign_Hidden_Values_To_TextBox();
    }

    protected void AT_Date_SelectionChanged(object sender, EventArgs e)
    {
        Reset_Values_To_Inital();
        objAccountTransferPresenter.fillgrid();
        Assign_Hidden_Values_To_TextBox();
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        calculate_griddetails();
        objAccountTransferPresenter.Save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        calculate_griddetails();
        objAccountTransferPresenter.Save();
        
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    #endregion
}
