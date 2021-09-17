using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
using Raj.EC;
using Raj.EC.ControlsView;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 10/11/2008
/// Description   : This Page is For FDV Operation
/// </summary>
/// 

public partial class Operations_Delivery_WucFrghtDisVoucher : System.Web.UI.UserControl, IFrghtDisVoucherView
{
    #region ClassVariables
    FrghtDisVoucherPresenter objFrghtDisVoucherPresenter;
    IFrghtDisVoucherView objIFrghtDisVoucherView;
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable(); 
    private DAL objDAL = new DAL();
    private DataSet objDS;  
    string _flag = "";
    string Mode = "0";
    string _GC_No_XML;
    PageControls pc = new PageControls();
 
    DropDownList ddl;
    HiddenField hdn_DiscountAmt;
    TextBox txt_DiscountAmt;
    DropDownList ddl_Undelivered_Reason;
    
    //ComponentArt.Web.UI.Calendar dtp_Committed_Del_Date;
    int ds_index, i;
    TextBox txt, txt_Total_GC_Amount, txt_Total_DiscountAmt;
    decimal Total_GC_Amount, TotalDiscountAmt;//, totaltotalFreight;
    Label lbl_totalFreight;
    #endregion

    #region ControlsValues

    public string VoucherNo
    {
        set { lbl_FrghtDisVoucher_No.Text = value; }
    }
    public DateTime VoucherDate
    {
        set 
        {
            dtp_Voucher_Date.SelectedDate = value;
            hdn_Voucher_Date.Value = dtp_Voucher_Date.SelectedDate.ToString();
        } 
        get { return dtp_Voucher_Date.SelectedDate; }
    }  
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }

    public decimal Total_Total_GC_Amount
    {
        set
        {
            hdn_totaltotalFreight.Value = Util.Decimal2String(value);
            lbl_totaltotalFreight.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_totaltotalFreight.Value); }
    }
     
    public decimal Total_DiscountAmt
    {
        set
        {
            hdn_TotalDiscountAmt.Value = Util.Decimal2String(value);
            lbl_TotalDiscountAmt.Text = Util.Decimal2String(value); 
        }
        get { return Util.String2Decimal(hdn_TotalDiscountAmt.Value); }
    }
    
    public int Total_No_Of_GC
    {
        set
        {
            hdn_Total_GC.Value = Util.Int2String(value);
            lbl_Total_GC.Text = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_Total_GC.Value); }
    }
    public string Flag
    {
        get { return _flag; }
    } 
   
   
    #endregion

    #region ControlsBind
  

    public void BindFDVGrid()
    {
        dg_FDV.DataSource = SessionBindFDVGrid;
        dg_FDV.DataBind();
        CalculateTotal();
    }

    public DataTable SessionBindDDLUndelReason
    {
        get { return StateManager.GetState<DataTable>("BindDDLUndelReason"); }
        set { StateManager.SaveState("BindDDLUndelReason", value); }
    }  
    public DataTable SessionBindFDVGrid
    {
        get { return StateManager.GetState<DataTable>("BindFDVGrid"); }
        set
        {
            StateManager.SaveState("BindFDVGrid", value);
            if (StateManager.Exist("BindFDVGrid"))
            {
                BindFDVGrid();
                hdnforselectall.Value = Util.Int2String(SessionBindFDVGrid.Rows.Count);
            }
        }
    } 

    public String FDVDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBindFDVGrid.Copy());

            _objDs.Tables[0].TableName = "FrghtDisVoucherGrid_Details";
            return _objDs.GetXml().ToLower();
        }
    }
    public String GetGCNoXML
    {
        get
        {
            if (_GC_No_XML != null)
            {
                return _GC_No_XML.ToString().ToLower();
            }
            else
            {
                return "<NewDataSet/>";
            }
        }
        set { _GC_No_XML = value; }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        
        bool _isValid = false; 
        
        if (VoucherDate < UserManager.getUserParam().StartDate || VoucherDate > UserManager.getUserParam().EndDate)
        {
            errorMessage = "Voucher Date should be in current Financial Date";
        }  
        else if (SessionBindFDVGrid.Rows.Count <= 0)
        {
            errorMessage = "Please Select Atleast One "+ CompanyManager.getCompanyParam().GcCaption;
        }
        else if (grid_validation() == false)
        {
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
    #endregion

    #region OtherMethod
    private void Next_FDV_Number()
    {
        VoucherNo = objComm.Get_Next_Number();
    }

    private void OnGetGCXML(object sender, EventArgs e)
    {
        if (WucSelectedItems1.EnterItem != string.Empty)
        {
            _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
            Assign_Hidden_Values_For_Reset();
            objFrghtDisVoucherPresenter.FillGrid();
            WucSelectedItems1.dtdetails = SessionBindFDVGrid;
             
            WucSelectedItems1.Get_Not_Selected_Items();
        }

    }

    private void Assign_Hidden_Values_To_TextBox()
    {

        lbl_Total_GC.Text = hdn_Total_GC.Value; 
        lbl_totaltotalFreight.Text = Convert.ToString(hdn_totaltotalFreight.Value);
        lbl_TotalDiscountAmt.Text = Convert.ToString(hdn_TotalDiscountAmt.Value); 
    }

    private void Assign_Hidden_Values_For_Reset()
    {
        hdn_Total_GC.Value = "0";
        hdn_totaltotalFreight.Value = "0"; 
        hdn_TotalDiscountAmt.Value = "0"; 
         
        lbl_Total_GC.Text = "0";
        lbl_totaltotalFreight.Text = "0";
        lbl_TotalDiscountAmt.Text = "0"; 
    }
    private void SetStandardCaption()
    {
        const int GCNoCaption = 0;
        hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
        WucSelectedItems1.SetFoundCaption = "Enter  " + hdn_GCCaption.Value + "   Nos.:";
        WucSelectedItems1.SetNotFoundCaption = hdn_GCCaption.Value + "  Nos.Not Found :";
        WucSelectedItems1.Set_GCCaption = hdn_GCCaption.Value;

        dg_FDV.Columns[GCNoCaption].HeaderText = CompanyManager.getCompanyParam().GcCaption + "  No";
    }
    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            TD_Calender.Visible = false;
            dtp_Voucher_Date.Enabled = false;
            WucSelectedItems1.Visible = false;
            td_gccontrol.Visible = false;
        }
        if (Mode == "2")
        {
            TD_Calender.Visible = false;
            dtp_Voucher_Date.Enabled = false;
            WucSelectedItems1.Visible = false;
            td_gccontrol.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    { 
        SetStandardCaption();

        WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Close, btn_Save_Print));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Close, btn_Save_Print));
        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Print, btn_Save_Exit, btn_Save, btn_Close));
        
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
        hdn_Mode.Value = Mode;

        if (!IsPostBack)
        {
            Assign_Hidden_Values_For_Reset();
        }
        objFrghtDisVoucherPresenter = new FrghtDisVoucherPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                Next_FDV_Number();
            }
        }
        Assign_Hidden_Values_To_TextBox();
    } 

    #region OtherProperty

    private bool grid_validation()
    {
        bool ATS = true;
        string GC_No;

        objDT = SessionBindFDVGrid;

        if (objDT.Rows.Count > 0)
        { 
            for (i = 0; i <= dg_FDV.Items.Count - 1; i++)
            {
                ddl_Undelivered_Reason = (DropDownList)dg_FDV.Items[i].FindControl("ddl_UnDel_Reason");
                txt_DiscountAmt = (TextBox)dg_FDV.Items[i].FindControl("txt_DiscountAmt");  
                
                GC_No = dg_FDV.Items[i].Cells[0].Text;

                if (Convert.ToDecimal(txt_DiscountAmt.Text) <= 0)
                {
                    errorMessage = "Please Select Valid Discount Amount for " + GC_No;
                    scm_FDV.SetFocus(txt_DiscountAmt);
                    ATS = false;
                    break;
                } 
                else if (Convert.ToInt32(ddl_Undelivered_Reason.SelectedValue) <= 0)
                {
                    errorMessage = "Please Select Valid Reason for " + GC_No ;
                    scm_FDV.SetFocus(ddl_Undelivered_Reason);
                    ATS = false;
                    break;
                }
                else
                {
                    ATS = true;
                }
            }
            if (ATS == true)
            {
                calculate_griddetails();
            }
        }
        return ATS;
    }

    private void calculate_griddetails()
    {
        if (dg_FDV.Items.Count > 0)
        {
            objDT = SessionBindFDVGrid;

            for (i = 0; i <= dg_FDV.Items.Count - 1; i++)
            {
                //chk = (CheckBox)dg_FDV.Items[i].FindControl("Chk_Attach");
                 
                ddl_Undelivered_Reason = (DropDownList)dg_FDV.Items[i].FindControl("ddl_UnDel_Reason"); 
                
                txt_DiscountAmt = (TextBox)dg_FDV.Items[i].FindControl("txt_DiscountAmt");
                hdn_DiscountAmt = (HiddenField)dg_FDV.Items[i].FindControl("hdn_DiscountAmt");

                //if (chk.Checked == true)
                //{ 
                //    objDT.Rows[i]["Actual_Status_ID"] = Util.String2Int(hdn_StatusID.Value);
                //    objDT.Rows[i]["Actual_Status"] = hdn_Status.Value;
                //}
                //else
                //{
                //    objDT.Rows[i]["Actual_Status_ID"] = 300;
                //    objDT.Rows[i]["Actual_Status"] = "UnDelivered";
                //    objDT.Rows[i]["Status_ID"] = 300;
                //    objDT.Rows[i]["Status"] = "UnDelivered"; 
                //}
                 
                
                objDT.Rows[i]["Reason_Id"] = Util.String2Int(ddl_Undelivered_Reason.SelectedValue);
                objDT.Rows[i]["DiscountAmt"] = txt_DiscountAmt.Text;

            }
        }

    }



    #endregion


    #region otherControls
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        errorMessage = "";
        _flag = "SaveAndNew";
        objFrghtDisVoucherPresenter.save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        errorMessage = "";
        _flag = "SaveAndExit";
        objFrghtDisVoucherPresenter.save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        errorMessage = "";
        _flag = "SaveAndPrint";
        objFrghtDisVoucherPresenter.save();
    } 
    
    protected void dtp_Voucher_Date_SelectionChanged(object sender, EventArgs e)
    {
        Assign_Hidden_Values_For_Reset();
        Assign_Hidden_Values_To_TextBox();
        objFrghtDisVoucherPresenter.FillValues();
        objFrghtDisVoucherPresenter.FillGrid();
        hdn_Voucher_Date.Value = dtp_Voucher_Date.SelectedDate.ToString(); 
    }
    #endregion

 
    protected void dg_FDV_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton lbtn_Details;
        LinkButton lbtn_GCNoForPrint;
        HiddenField hdn_GCNoForPrint;

       
        if (e.Item.ItemIndex != -1)
        {
            ddl_Undelivered_Reason = (DropDownList)e.Item.FindControl("ddl_UnDel_Reason");
            txt_DiscountAmt = (TextBox)e.Item.FindControl("txt_DiscountAmt");
            hdn_DiscountAmt = (HiddenField)e.Item.FindControl("hdn_DiscountAmt");
                      
            ddl_Undelivered_Reason.DataTextField = "Reason";
            ddl_Undelivered_Reason.DataValueField = "Reason_ID";
            ddl_Undelivered_Reason.DataSource = SessionBindDDLUndelReason;
            ddl_Undelivered_Reason.DataBind();
            ddl_Undelivered_Reason.Items.Insert(0, new ListItem("Select One", "0"));
            
            ddl_Undelivered_Reason.SelectedValue = SessionBindFDVGrid.Rows[e.Item.ItemIndex]["Reason_Id"].ToString();
            
            txt_DiscountAmt.Text = SessionBindFDVGrid.Rows[e.Item.ItemIndex]["DiscountAmt"].ToString();
            hdn_DiscountAmt.Value = SessionBindFDVGrid.Rows[e.Item.ItemIndex]["DiscountAmt"].ToString();
             
            ds_index = e.Item.ItemIndex;
           
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{ 
            //    hdn_GC_Id = (HiddenField)e.Item.FindControl("hdn_GC_Id");
           
            //    lbtn_GCNoForPrint = (LinkButton)e.Item.FindControl("lbtn_GCNoForPrint");
            //    hdn_GCNoForPrint = (HiddenField)e.Item.FindControl("hdn_GCNoForPrint"); 
                 
            //} 
        }
        
    }


    protected void ddl_UnDel_Reason_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TextBox txt_lol_Tmp_frgt, txt_del_art, txt_del_wt, txt_Total_GC_Amount, lbl_txt_ChequeAmt, lbl_txt_CreditAmt;
        //CheckBox chk;
        //LinkButton lbtn_details;
        //HiddenField hdn_GCNoForPrint; 
       
        //ddl = (DropDownList)sender;
         
        //DataGridItem _item = (DataGridItem)ddl.Parent.Parent;

        //chk = (CheckBox)_item.FindControl("Chk_Attach");
        //lbtn_details = (LinkButton)_item.FindControl("lbtn_details");
        //txt_Actual_Status = (TextBox)_item.FindControl("txt_Actual_Status");
        //hdn_StatusID = (HiddenField)_item.FindControl("hdn_Status_Id");
        //hdn_Status = (HiddenField)_item.FindControl("hdn_Status");
        //hdn_Actual_Status_Id = (HiddenField)_item.FindControl("hdn_Actual_Status_Id");
        //txt_Del_taken_by = (TextBox)_item.FindControl("txt_Delivery_TakenBy");
        //ddl_Undelivered_Reason = (DropDownList)_item.FindControl("ddl_UnDel_Reason");
        //ddl_DeliveryLocation = (DropDownList)_item.FindControl("ddl_DeliveryLocation");
        //ddl_DeliveryStatus = (DropDownList)_item.FindControl("ddl_DeliveryStatus");
        //txt_del_art = (TextBox)_item.FindControl("txt_Delivery_Art");
        //txt_del_wt = (TextBox)_item.FindControl("txt_Delivery_Wt");
        //txt_lol_Tmp_frgt = (TextBox)_item.FindControl("txt_DiscountAmt");
        //hdn_DiscountAmt = (HiddenField)_item.FindControl("hdn_DiscountAmt");
        //txt_Total_GC_Amount = (TextBox)_item.FindControl("txt_Total_GC_Amount");
     
        //hdn_GCNoForPrint = (HiddenField)_item.FindControl("hdn_GCNoForPrint");

     

        // CalculateTotal();
        
        //if (ddl.SelectedItem.Value != "4")
        //{
        //    txt_Actual_Status.Text = hdn_Status.Value;
        //    hdn_Actual_Status_Id.Value = hdn_StatusID.Value;
        //    ddl_Undelivered_Reason.SelectedValue = "0";
        //    ddl_Undelivered_Reason.Enabled = false;
        //    txt_Del_taken_by.Enabled = true;
        //    _item.Cells[12].Enabled = true;
        //    hdn_totalDelArt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelArt.Value) + Util.String2Decimal(txt_del_art.Text));
        //    hdn_totalDelWt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelWt.Value) + Util.String2Decimal(txt_del_wt.Text));
        //    hdn_TotalCash.Text = lbl_txt_TotalCash.Text;
        //    hdn_Total_GC.Value = lbl_Total_GC.Text;
            
        //    if (ddl.SelectedItem.Value == "2" || ddl.SelectedItem.Value == "3")
        //    {
        //        //lbtn_details.Focus();
        //        scm_FDV.SetFocus(lbtn_details);
        //    }
        //    if (ddl.SelectedItem.Value == "0")
        //    {
        //        txt_lol_Tmp_frgt.Text = "0";
        //        hdn_DiscountAmt.Value = "0";
        //    }
        //    if (ddl.SelectedItem.Value == "1")
        //    {
        //        scm_FDV.SetFocus(txt_lol_Tmp_frgt);
        //    }
        //} 
        //else
        //{
        //    //txt_Actual_Status.Text = "UnDelivered";
        //    //hdn_Actual_Status_Id.Value = "300";

        //    //hdn_Status.Value = "UnDelivered";
        //    //hdn_StatusID.Value = "300";

        //    ddl_Undelivered_Reason.Enabled = true;
           
        //    //_item.Cells[12].Enabled = false;
        //    hdn_totalDelArt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelArt.Value) - Util.String2Decimal(txt_del_art.Text));
        //    hdn_totalDelWt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelWt.Value) - Util.String2Decimal(txt_del_wt.Text));
             
        //    hdn_TotalCash.Text = lbl_txt_TotalCash.Text;
        //    hdn_Total_GC.Value = lbl_Total_GC.Text; 
            
             
        //}

      

    }
    protected void txt_DiscountAmt_TextChanged(object sender, EventArgs e)
    {
        TextBox txt_DiscountAmt;
        DropDownList ddl_UnDel_Reason;

        txt = (TextBox)sender;
        DataGridItem _item = (DataGridItem)txt.Parent.Parent;
        txt_DiscountAmt = (TextBox)_item.FindControl("txt_DiscountAmt");
        ddl_UnDel_Reason = (DropDownList)_item.FindControl("ddl_UnDel_Reason");
        hdn_DiscountAmt = (HiddenField)_item.FindControl("hdn_DiscountAmt"); 
        TotalDiscountAmt = 0; 
      

        objDT = SessionBindFDVGrid;
       
        if (dg_FDV.Items.Count > 0)
        {
            for (i = 0; i <= dg_FDV.Items.Count - 1; i++)
            {
                txt_Total_DiscountAmt = (TextBox)dg_FDV.Items[i].FindControl("txt_DiscountAmt");  
                TotalDiscountAmt = TotalDiscountAmt + Convert.ToDecimal(txt_Total_DiscountAmt.Text); 
            }
            lbl_TotalDiscountAmt.Text = Convert.ToString(TotalDiscountAmt);
            hdn_TotalDiscountAmt.Value = Convert.ToString(TotalDiscountAmt);
        }
        scm_FDV.SetFocus(ddl_UnDel_Reason); 
 
    } 
    private void CalculateTotal()
    {
          
        //totaltotalFreight = 0;
        TotalDiscountAmt = 0;
        Total_No_Of_GC = 0;
        int i;
        
        objDT = SessionBindFDVGrid;

        if (dg_FDV.Items.Count > 0)
        {
            for (i = 0; i <= dg_FDV.Items.Count - 1; i++)
            {   
                txt_Total_GC_Amount = (TextBox)dg_FDV.Items[i].FindControl("txt_Total_GC_Amount"); 
                txt_Total_DiscountAmt = (TextBox)dg_FDV.Items[i].FindControl("txt_DiscountAmt");

                if (lbl_totaltotalFreight.Text == "")
                    lbl_totaltotalFreight.Text = "0";

                Total_Total_GC_Amount = Total_Total_GC_Amount + Convert.ToDecimal(txt_Total_GC_Amount.Text);    
            }
            
            Total_No_Of_GC = dg_FDV.Items.Count; 
            lbl_Total_GC.Text = Convert.ToString(Total_No_Of_GC); 
            lbl_TotalDiscountAmt.Text = Convert.ToString(TotalDiscountAmt);
            lbl_totaltotalFreight.Text = Convert.ToString(Total_Total_GC_Amount);
            
        }
    }

    //protected void Chk_Attach_CheckedChanged(object sender, EventArgs e)
    //{
    //    TextBox txt_del_art, txt_del_wt, txt_lol_Tmp_frgt; 

    //    chk = (CheckBox)sender;
    //    DataGridItem _item = (DataGridItem)chk.Parent.Parent;

    //    txt_Actual_Status = (TextBox)_item.FindControl("txt_Actual_Status");
    //    hdn_StatusID = (HiddenField)_item.FindControl("hdn_Status_Id");
    //    hdn_Status = (HiddenField)_item.FindControl("hdn_Status");
    //    hdn_Actual_Status_Id = (HiddenField)_item.FindControl("hdn_Actual_Status_Id");
    //    txt_Del_taken_by = (TextBox)_item.FindControl("txt_Delivery_TakenBy");
    //    ddl_Undelivered_Reason = (DropDownList)_item.FindControl("ddl_UnDel_Reason");
    //    ddl_DeliveryLocation = (DropDownList)_item.FindControl("ddl_DeliveryLocation");
    //    ddl_DeliveryStatus = (DropDownList)_item.FindControl("ddl_DeliveryStatus");
    //    txt_del_art = (TextBox)_item.FindControl("txt_Delivery_Art");
    //    txt_del_wt = (TextBox)_item.FindControl("txt_Delivery_Wt");
    //    txt_lol_Tmp_frgt = (TextBox)_item.FindControl("txt_DiscountAmt");
  

    //     Assign_Hidden_Values_To_TextBox();
    //}

    public void ClearVariables()
    {
        SessionBindFDVGrid = null;
        SessionBindDDLUndelReason = null; 
    }
    protected void btn_null_session_Click(object sender, EventArgs e) //added Ankit : 21-02-09
    {
        ClearVariables();
    } 
 
    
}
