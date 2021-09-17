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
using ClassLibraryMVP.Security;
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;
/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 16/12/2008
/// Description   : This Page is Standard Booking Rate
/// </summary> 
/// 
public partial class Master_Branch_WucStdBookingRates : System.Web.UI.UserControl,IStandardBookingRateView
{
    #region ClassVariables
    StandardBookingRatePresenter objStandardBookingRatePresenter;
    DataTable objDT = new DataTable();
    DataSet objDS = new DataSet();
    ClassLibrary.UIControl.DDLSearch ddl_ToBranch;
    Label lbl_FromBranch, lbl_CrossingRate;
    HiddenField hdn_FromBranch_Id;
    TextBox txt_FromBranch,Txt_BookingBranch, Txt_DeliveryBranch;
    DataRow DR = null;
    bool isValid = false;
    int RowIndex;
    #endregion

    #region ControlsValues

    public int BookingBranchId
    {
        get { return Util.String2Int(DDL_BookingBranch.SelectedValue); }
    }
    public int DeliveryBranchId
    {
        get { return Util.String2Int(DDL_DeliveryBranch.SelectedValue); }
    }
    public DateTime ApplicableFromDate
    {
        set { dtp_ApplicableFrom.SelectedDate = value; }
        get { return dtp_ApplicableFrom.SelectedDate; }
    }
    public decimal ProfitRatio
    {
        set { txt_ProfitRatio.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_ProfitRatio.Text); }
    }
    public decimal BookingRate
    {
        set { lbl_BookingRate.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(lbl_BookingRate.Text); }
    }

    public void SetBookingBranchId(string Text, string Value)
    {
        DDL_BookingBranch.DataTextField = "Branch_Name";
        DDL_BookingBranch.DataValueField = "Branch_ID";
        Raj.EC.Common.SetValueToDDLSearch(Text, Value, DDL_BookingBranch);
    }
    public void SetDeliveryBranchId(string Text, string Value)
    {
        DDL_DeliveryBranch.DataTextField = "Branch_Name";
        DDL_DeliveryBranch.DataValueField = "Branch_ID";
        Raj.EC.Common.SetValueToDDLSearch(Text, Value, DDL_DeliveryBranch);
    }
    public void SetToBranchId(string Text, string Value)
    {
        ddl_ToBranch.DataTextField = "Branch_Name";
        ddl_ToBranch.DataValueField = "Branch_ID";
        Raj.EC.Common.SetValueToDDLSearch(Text, Value, ddl_ToBranch);
    }
     #endregion

    #region ControlsBind

    public void BindStandardRateGrid()
    {
        dg_StandardRate.DataSource = SessionStandardRateGrid;
        dg_StandardRate.DataBind();
        update_Panel();
    }

    public DataTable SessionStandardRateGrid
    {
        get { return StateManager.GetState<DataTable>("StandardRateGrid"); }
        set
        {
            StateManager.SaveState("StandardRateGrid", value);

            if(StateManager.Exist("StandardRateGrid"))
                BindStandardRateGrid();
        }
    }

    public String CrossingPointXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionStandardRateGrid.Copy());

            _objDs.Tables[0].TableName = "StandardRatedetails";
            return _objDs.GetXml().ToLower();
        }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        Txt_BookingBranch = (TextBox)DDL_BookingBranch.FindControl("txtBoxDDL_BookingBranch");
        Txt_DeliveryBranch = (TextBox)DDL_DeliveryBranch.FindControl("txtBoxDDL_DeliveryBranch");

        if (BookingBranchId <= 0)
        {
            errorMessage = "Please Select Booking Branch";// GetLocalResourceObject("Msg_ddl_Book_Branch").ToString();
            scm_StandardRate.SetFocus(Txt_BookingBranch);
        }
        else if (Datemanager.IsValidProcessDate("STD_BOOK_RATE", ApplicableFromDate) == false)
        {
            errorMessage = "Invalid Applicable From Date";// GetLocalResourceObject("Msg_dtp_Applicable_Date").ToString();
        }
        else if (DeliveryBranchId <= 0)
        {
            errorMessage = "Please Select Delivery Branch";// GetLocalResourceObject("Msg_ddl_Deli_Branch").ToString();
            scm_StandardRate.SetFocus(Txt_DeliveryBranch);
        }
        else if (BookingBranchId == DeliveryBranchId)
        {
            errorMessage = "Booking Branch and Delivery Branch should not be same";// GetLocalResourceObject("Msg_ddl_Book_Del_Branch").ToString();
        }
        else if (ProfitRatio < 0 || ProfitRatio > 100)
        {
            errorMessage = "Profit Ratio Can't be greater than 100";// GetLocalResourceObject("Msg_txt_profit_ratio").ToString();
            scm_StandardRate.SetFocus(txt_ProfitRatio);
        }
        else if (SessionStandardRateGrid.Rows.Count <= 0)
        {
            errorMessage = "Please add atleast One Record";// GetLocalResourceObject("Msg_grid_validation").ToString();
        }
        else if (validate_grid() == false)
        {
            _isValid = false;
        }
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
    //Added : Anita On : 18 Feb 09
    public void ClearVariables()
    {
        SessionStandardRateGrid = null;       
    }

    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
    }

    #endregion      

    #region page load
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        dg_StandardRate.Columns[2].HeaderText = "Crossing Rate/" + CompanyManager.getCompanyParam().StandardFreightRatePer.ToString() + "Kg"; //ankit

        objStandardBookingRatePresenter = new StandardBookingRatePresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            hdn_FromBranch_Id.Value = "0";
        }
        lbl_StdFreightRate.Text = "/" + CompanyManager.getCompanyParam().StandardFreightRatePer.ToString() + "Kg"; //ankit


    }
    #endregion

    # region "Grid event"
    protected void dg_StandardRate_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header)
        {
            if (e.Item.ItemType == ListItemType.Footer || e.Item.ItemType == ListItemType.EditItem)
            {
                ddl_ToBranch = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_ToBranch"));
                lbl_FromBranch = (Label)(e.Item.FindControl("lbl_FromBranch"));
                hdn_FromBranch_Id = (HiddenField)(e.Item.FindControl("hdn_FromBranch_Id"));
                lbl_CrossingRate = (Label)(e.Item.FindControl("lbl_CrossingRate"));

                ddl_ToBranch.DataTextField = "Branch_Name";
                ddl_ToBranch.DataValueField = "Branch_ID";
            }

            RowIndex = SessionStandardRateGrid.Rows.Count - 1;

            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (BookingBranchId > 0 && SessionStandardRateGrid.Rows.Count <= 0 && e.Item.ItemIndex < 0)
                {
                    lbl_FromBranch.Text = DDL_BookingBranch.SelectedItem;
                    hdn_FromBranch_Id.Value = DDL_BookingBranch.SelectedValue;
                }
                else if (BookingBranchId > 0 && e.Item.ItemIndex < 0)
                {
                    lbl_FromBranch.Text = SessionStandardRateGrid.Rows[RowIndex]["To_Branch_Name"].ToString();
                    hdn_FromBranch_Id.Value = SessionStandardRateGrid.Rows[RowIndex]["To_Branch_Id"].ToString();
                }
            }
            
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                if (BookingBranchId > 0 && SessionStandardRateGrid.Rows.Count > 0 && e.Item.ItemIndex > 0)
                {
                    lbl_FromBranch.Text = DDL_BookingBranch.SelectedItem;
                    hdn_FromBranch_Id.Value = DDL_BookingBranch.SelectedValue;
                }
                else if (BookingBranchId > 0 && e.Item.ItemIndex < 0)
                {
                    lbl_FromBranch.Text = SessionStandardRateGrid.Rows[RowIndex]["To_Branch_Name"].ToString();
                    hdn_FromBranch_Id.Value = SessionStandardRateGrid.Rows[RowIndex]["To_Branch_Id"].ToString();
                }
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {
                objDT = SessionStandardRateGrid;

                DR = objDT.Rows[e.Item.ItemIndex];

                SetToBranchId(DR["To_Branch_Name"].ToString(), DR["To_Branch_ID"].ToString());
                hdn_FromBranch_Id.Value = DR["From_Branch_ID"].ToString();
                lbl_FromBranch.Text = DR["From_Branch_Name"].ToString();
                lbl_CrossingRate.Text = DR["Crossing_Rate"].ToString();
            }
        }
        
        if (SessionStandardRateGrid.Rows.Count > 0)
        {
            txt_ProfitRatio_TextChanged(sender, e);
        }

        update_Panel();
    }

    private void Insert_Update_Dataset(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        int addrow;
        ddl_ToBranch = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_ToBranch"));
        lbl_FromBranch = (Label)(e.Item.FindControl("lbl_FromBranch"));
        lbl_CrossingRate = (Label)(e.Item.FindControl("lbl_CrossingRate"));
        hdn_FromBranch_Id = (HiddenField)(e.Item.FindControl("hdn_FromBranch_Id"));

        objDT = SessionStandardRateGrid;

        if (e.CommandName == "Add")
        {
            DR = objDT.NewRow();
        }
        else if (e.CommandName == "Update")
        {
            DR = objDT.Rows[e.Item.ItemIndex];
        }

        if (e.Item.ItemIndex < 0)
        {
            addrow = SessionStandardRateGrid.Rows.Count - 1;
        }
        else
        {
            addrow = e.Item.ItemIndex - 1;
        }

        if (Allow_To_Add_Update(source,e) == true)
        {
            DR["To_Branch_ID"] = ddl_ToBranch.SelectedValue;
            DR["To_Branch_Name"] = ddl_ToBranch.SelectedItem;
            if (SessionStandardRateGrid.Rows.Count <= 0 && e.Item.ItemIndex < 0)
            {
                DR["From_Branch_Id"] = BookingBranchId;
                DR["From_Branch_Name"] = DDL_BookingBranch.SelectedItem;
            }
            else if (SessionStandardRateGrid.Rows.Count > 0 && e.Item.ItemIndex < 0)
            {
                DR["From_Branch_Id"] = Util.String2Int(SessionStandardRateGrid.Rows[addrow]["To_Branch_ID"].ToString());
                DR["From_Branch_Name"] = SessionStandardRateGrid.Rows[addrow]["To_Branch_Name"].ToString();
            }
            if (SessionStandardRateGrid.Rows.Count > 0 && e.Item.ItemIndex >= 0 && (SessionStandardRateGrid.Rows.Count != e.Item.ItemIndex + 1))
            {
                objDT.Rows[(e.Item.ItemIndex) + 1]["From_Branch_Id"] = ddl_ToBranch.SelectedValue;
                objDT.Rows[(e.Item.ItemIndex) + 1]["From_Branch_Name"] = ddl_ToBranch.SelectedItem;
            }
            else if ((SessionStandardRateGrid.Rows.Count == e.Item.ItemIndex + 2 ) && e.Item.ItemIndex > 0)
            {
                DR["To_Branch_ID"] = ddl_ToBranch.SelectedValue;
                DR["To_Branch_Name"] = ddl_ToBranch.SelectedItem;
            }

            DR["Crossing_Rate"] = lbl_CrossingRate.Text;

            if (e.CommandName == "Add") { objDT.Rows.Add(DR); }
            SessionStandardRateGrid = objDT;
        }
    }

    private bool Allow_To_Add_Update(Object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {

        lbl_Errors.Text = "";

        Txt_BookingBranch = (TextBox)DDL_BookingBranch.FindControl("txtBoxDDL_BookingBranch");
        Txt_DeliveryBranch = (TextBox)DDL_DeliveryBranch.FindControl("txtBoxDDL_DeliveryBranch");

        ddl_ToBranch = (ClassLibrary.UIControl.DDLSearch)(e.Item.FindControl("ddl_ToBranch"));
        hdn_FromBranch_Id = (HiddenField)(e.Item.FindControl("hdn_FromBranch_Id"));

        if (BookingBranchId <= 0)
        {
            errorMessage = "Please Select Booking Branch";// GetLocalResourceObject("Msg_ddl_Book_Branch").ToString();
            scm_StandardRate.SetFocus(Txt_BookingBranch);
        }
        else if (DeliveryBranchId <= 0)
        {
            errorMessage = "Please Select Delivery Branch";// GetLocalResourceObject("Msg_ddl_Deli_Branch").ToString();
            scm_StandardRate.SetFocus(Txt_DeliveryBranch);
        }
        else if (Util.String2Int(ddl_ToBranch.SelectedValue) <= 0)
        {
            errorMessage = "Please Select To Branch";// GetLocalResourceObject("Msg_ddl_To_Branch").ToString();
            scm_StandardRate.SetFocus(ddl_ToBranch);
        }
        else if (BookingBranchId == Util.String2Int(ddl_ToBranch.SelectedValue))
        {
            errorMessage = "Booking Branch and To Branch should not be same";// GetLocalResourceObject("Msg_ddl_Book_To_Branch").ToString();
            scm_StandardRate.SetFocus(Txt_BookingBranch);
        }
        else if (Util.String2Int(hdn_FromBranch_Id.Value) == Util.String2Int(ddl_ToBranch.SelectedValue))
        {
            errorMessage = "From Branch and To Branch Can't be same";// GetLocalResourceObject("Msg_ddl_From_To_Branch").ToString();
            scm_StandardRate.SetFocus(ddl_ToBranch);
        }
        else
        {
            isValid = true;
        }

        return isValid;
    }
    protected void dg_StandardRate_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Add")
        {
              try
              {
                objDT = SessionStandardRateGrid;
                Insert_Update_Dataset(source, e);
                if (isValid == true)
                {
                    BindStandardRateGrid();
                }
              }
             catch (ConstraintException)
             {
                 errorMessage = "Duplicate Branch Found";// GetLocalResourceObject("Msg_Duplicatel_To_Branch").ToString();
                scm_StandardRate.SetFocus(ddl_ToBranch);
             }
        }
    }

    protected void dg_StandardRate_EditCommand(object source, DataGridCommandEventArgs e)
    {
        dg_StandardRate.EditItemIndex = e.Item.ItemIndex;
        dg_StandardRate.ShowFooter = false;
        BindStandardRateGrid();
    }

    protected void dg_StandardRate_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        dg_StandardRate.EditItemIndex = -1;
        dg_StandardRate.ShowFooter = true;
        BindStandardRateGrid();
    }

    protected void dg_StandardRate_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
          {
            objDT = SessionStandardRateGrid;
            Insert_Update_Dataset(source, e);

            if (isValid == true)
            {
                dg_StandardRate.EditItemIndex = -1;
                dg_StandardRate.ShowFooter = true;
                BindStandardRateGrid();
            }
        }
        catch (ConstraintException)
        {
            errorMessage = GetLocalResourceObject("Msg_Duplicatel_To_Branch").ToString();
            scm_StandardRate.SetFocus(ddl_ToBranch);
        }
    }

    protected void dg_StandardRate_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
            objDT = SessionStandardRateGrid;
            
            if (e.Item.ItemIndex == 0 && SessionStandardRateGrid.Rows.Count > 1)
            {
                objDT.Rows[(e.Item.ItemIndex) + 1]["From_Branch_Id"] = DDL_BookingBranch.SelectedValue;
                objDT.Rows[(e.Item.ItemIndex) + 1]["From_Branch_Name"] = DDL_BookingBranch.SelectedItem;
            }
            else if (e.Item.ItemIndex != SessionStandardRateGrid.Rows.Count - 1)
            {
                objDT.Rows[(e.Item.ItemIndex) + 1]["From_Branch_Id"] = objDT.Rows[(e.Item.ItemIndex) - 1]["To_Branch_Id"];
                objDT.Rows[(e.Item.ItemIndex) + 1]["From_Branch_Name"] = objDT.Rows[(e.Item.ItemIndex) - 1]["To_Branch_Name"];
            }
 
            objDT.Rows.RemoveAt(e.Item.ItemIndex);
            objDT.AcceptChanges();
            SessionStandardRateGrid = objDT;
            dg_StandardRate.EditItemIndex = -1;
            dg_StandardRate.ShowFooter = true;
           
            BindStandardRateGrid();
        }
    }

    # endregion

    # region "Other method"

    private bool validate_grid()
    {
        bool ats = false;
        int BookingID, DeliveryId;

        Txt_BookingBranch = (TextBox)DDL_BookingBranch.FindControl("txtBoxDDL_BookingBranch");
        Txt_DeliveryBranch = (TextBox)DDL_DeliveryBranch.FindControl("txtBoxDDL_DeliveryBranch");

        objDT = SessionStandardRateGrid;

        BookingID = Util.String2Int(objDT.Rows[0]["From_Branch_Id"].ToString());
        DeliveryId = Util.String2Int(objDT.Rows[objDT.Rows.Count - 1]["To_Branch_Id"].ToString());

        if (BookingID != BookingBranchId)
        {
            errorMessage = "Please Select Proper Booking Branch";// GetLocalResourceObject("Msg_ddl_Grid_Book_Branch").ToString();
            scm_StandardRate.SetFocus(Txt_BookingBranch);
            ats = false;
        }
        else if (DeliveryId != DeliveryBranchId)
        {
            errorMessage = "Please select Delivery Branch and Crossing Point last branch same";// GetLocalResourceObject("Msg_ddl_Grid_Deli_Branch").ToString();
            scm_StandardRate.SetFocus(Txt_DeliveryBranch);
            ats = false;
        }
        else
        {
            ats = true;
        }
        return ats;
    }

    private void update_Panel()
    {
        UpdatePanel1.Update();
        UpdatePanel3.Update();
    }

    protected void ddl_ToBranch_TxtChange(object sender, EventArgs e)
    {
        int ToBranchid,FromBrcID;

        txt_FromBranch = (TextBox)sender;
        ddl_ToBranch = (ClassLibrary.UIControl.DDLSearch)txt_FromBranch.Parent;
        DataGridItem _item = (DataGridItem)ddl_ToBranch.Parent.Parent;

        lbl_CrossingRate = (Label)_item.FindControl("lbl_CrossingRate");
        hdn_FromBranch_Id = (HiddenField)_item.FindControl("hdn_FromBranch_Id");
        
        FromBrcID = Util.String2Int(hdn_FromBranch_Id.Value);
        ToBranchid = Util.String2Int(ddl_ToBranch.SelectedValue);
  
        if (BookingBranchId > 0 && ToBranchid > 0)
        {
            objDS = Get_CrossingRate(FromBrcID, ToBranchid);

            if (objDS.Tables[0].Rows.Count > 0)
                lbl_CrossingRate.Text = objDS.Tables[0].Rows[0]["Crossing_Rate"].ToString();
            else
                lbl_CrossingRate.Text = "0";
        }
    }
   
    private DataSet Get_CrossingRate(int FromId,int ToId)
    {
        objDS = objStandardBookingRatePresenter.Fill_Crossing_Rate(FromId, ToId);
        return objDS;
    }

    protected void DDL_BookingBranch_TxtChange(object sender, EventArgs e)
    {
        SessionStandardRateGrid.Rows.Clear();
        SessionStandardRateGrid.AcceptChanges();

        //dg_StandardRate.EditItemIndex = -1;
        //dg_StandardRate.ShowFooter = true;
        BindStandardRateGrid();
        update_Panel();
    }
    protected void DDL_DeliveryBranch_TxtChange(object sender, EventArgs e)
    {
        BindStandardRateGrid();
    }
    protected void txt_ProfitRatio_TextChanged(object sender, EventArgs e)
    {
        decimal Total_CrossingRate, Total_BookingRate;
        Total_CrossingRate = Util.String2Decimal(SessionStandardRateGrid.Compute("SUM(Crossing_Rate)", "").ToString());

        Total_BookingRate = (Total_CrossingRate * Util.String2Decimal(txt_ProfitRatio.Text)) / 100;
        lbl_BookingRate.Text = Util.Decimal2String(Math.Round((Total_CrossingRate + Total_BookingRate),2));
        update_Panel();
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objStandardBookingRatePresenter.Save();
    }

# endregion
}
