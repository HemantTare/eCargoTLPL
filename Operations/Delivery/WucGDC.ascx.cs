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
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC.OperationPresenter;
using Raj.EC.OperationView;
using Raj.EC;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 07/11/2008
/// Description   : This Page is For GDC Operation
/// </summary>
/// 

public partial class Operations_Delivery_WucGDC : System.Web.UI.UserControl,IGDCView
{
    #region ClassVariables
    GDCPresenter objGDCPresenter;
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable();
    decimal Total_Delivery_Wt, Total_Delivery_Art;
    TextBox txt_Del_Art, txt_Del_Wt, txt_Total_Delivery_Wt, txt_Total_Delivery_Art, txtMobileNo;
    DropDownList ddl_DeliveryStatus, ddlDeliveryStatus;
    HiddenField hdn_DeliveryStatus, hdn_GC_Id, hdn_Article_Id, hdn_StatusID; 
    CheckBox chk;
    DropDownList ddl;
    TimePicker Timepicker1;
    string _flag = "";
    string Mode = "0";
    string _GC_No_XML;
    int ds_index, i;
    #endregion

    #region ControlsValues

    public string GDCNo
    {
        set { lbl_GDC_No.Text = value; }
    }
    public DateTime GDCDate
    {
        set
        {
            dtp_GDC_Date.SelectedDate = value;
            hdn_GDC_Date.Value = dtp_GDC_Date.SelectedDate.ToString();
        }
        get { return dtp_GDC_Date.SelectedDate; }
    }     
    public int SupervisorID
    {
        get { return Util.String2Int(ddl_GodownSupervisor.SelectedValue); }
    }

    public string DeliveredTo
    {
        set { txt_DeliveredTo.Text = value; }
        get { return txt_DeliveredTo.Text; }
    }

    public string DeliveredToMobile
    {
        set { txt_DeliveredToMobile.Text = value; }
        get { return txt_DeliveredToMobile.Text; }
    }

    public int PhotoIDType
    {
        get { return Util.String2Int(ddl_PhotoIDType.SelectedValue); }
        set { ddl_PhotoIDType.SelectedValue = Util.Int2String(value); }
    }

    public string PhotoIDNo
    {
        set { txt_PhotoIDNo.Text = value; }
        get { return txt_PhotoIDNo.Text; }
    }

    public int VehicleType
    {
        get { return Util.String2Int(ddl_VehicleType.SelectedValue); }
        set { ddl_VehicleType.SelectedValue = Util.Int2String(value); }
    }

    public string VehicleNoPart1
    {
        set { txt_Number_Part1.Text = value; }
        get { return txt_Number_Part1.Text; }
    }

    public string VehicleNoPart2
    {
        set { txt_Number_Part2.Text = value; }
        get { return txt_Number_Part2.Text; }
    }

    public string VehicleNoPart3
    {
        set { txt_Number_Part3.Text = value; }
        get { return txt_Number_Part3.Text; }
    }

    public string VehicleNoPart4
    {
        set { txt_Number_Part4.Text = value; }
        get { return txt_Number_Part4.Text; }
    }

    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }
    public int Total_Delivered_Articles
    {
        set
        {
            hdn_totalDelArt.Value = Util.Int2String(value);
            lbl_totalDelArt.Text = Util.Int2String(value);
        }
    }
    public decimal Total_Delivered_Weight
    {
        set
        {
            hdn_totalDelWt.Value = Util.Decimal2String(value);
            lbl_totalDelWt.Text = Util.Decimal2String(value);
        }
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

    public void SetSuperviserId(string text, string value)
    {
        ddl_GodownSupervisor.DataTextField = "Emp_Name";
        ddl_GodownSupervisor.DataValueField = "Emp_Id";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_GodownSupervisor);
    }

    #endregion

    #region ControlsBind

    public DataTable BindPhotoType
    {
        set
        {
            ddl_PhotoIDType.DataSource = "";
            ddl_PhotoIDType.DataBind();

            ddl_PhotoIDType.DataTextField = "Photo_Type";
            ddl_PhotoIDType.DataValueField = "Photo_Type_ID";
            ddl_PhotoIDType.DataSource = value;
            ddl_PhotoIDType.DataBind();
        }
    }

    public DataTable BindVehicleType
    {
        set
        {
            ddl_VehicleType.DataSource = "";
            ddl_VehicleType.DataBind();

            ddl_VehicleType.DataTextField = "Vehicle_Type";
            ddl_VehicleType.DataValueField = "Vehicle_Type_ID";
            ddl_VehicleType.DataSource = value;
            ddl_VehicleType.DataBind();

            ddl_VehicleType.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
   
    public void BindGDCGrid()
    {
        dg_GDC.DataSource = SessionBindGDCGrid;
        dg_GDC.DataBind();
    }

    public DataTable SessionBindDDLUndelReason
    {
        get { return StateManager.GetState<DataTable>("BindDDLUndelReason"); }
        set { StateManager.SaveState("BindDDLUndelReason", value); }
    }

    public DataTable SessionBindGDCGrid
    {
        get { return StateManager.GetState<DataTable>("BindGDCGrid"); }
        set { StateManager.SaveState("BindGDCGrid", value); }
    }

    public DataTable SessionBindDDLDeliveryMode
    {
        get { return StateManager.GetState<DataTable>("DDLDeliveryMode"); }
        set { StateManager.SaveState("DDLDeliveryMode", value); }
    }
    public DataTable SessionBindDlyStatus
    {
        get { return StateManager.GetState<DataTable>("BindDlyStatus"); }
        set { StateManager.SaveState("BindDlyStatus", value); }
    }

    public String GDCDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();

            DataView view = objComm.Get_View_Table(SessionBindGDCGrid, "Att = true");
            _objDs.Tables.Add(view.ToTable().Copy());

            _objDs.Tables[0].TableName = "GDCGrid_Details";
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
        TextBox Txt_GodownSupervisor;

        Txt_GodownSupervisor = (TextBox)ddl_GodownSupervisor.FindControl("txtBoxddl_GodownSupervisor");

        if (Datemanager.IsValidProcessDate("OPR_GDC",GDCDate ) == false)
        {
            errorMessage = "Please Enter Valid Date";
        }
        else if (Total_No_Of_GC <= 0)
        {
            errorMessage = "Please Select atleast one " + CompanyManager.getCompanyParam().GcCaption; 
        }
        //else if (Total_No_Of_GC == 1 && Util.String2Int(SessionBindGDCGrid.Rows[0]["Octroi_Updated"].ToString()) == 0)
        //{
        //    errorMessage = "Please Update Octroi ";
        //}
        else if (SupervisorID <= 0)
        {
            errorMessage = "Please Select Supervisor";// GetLocalResourceObject("Msg_txt_Supervisor").ToString();
            Txt_GodownSupervisor.Focus();
        }
        else if (txt_DeliveredTo.Text.Trim().Length <= 0)
        {
            errorMessage = "Please Enter Delivered To.";// GetLocalResourceObject("Msg_txt_Supervisor").ToString();
            txt_DeliveredTo.Focus();
        }
        else if (txt_DeliveredToMobile.Text.Trim().Length < 10)
        {
            errorMessage = "Please Enter Delivered To Mobile No.";// GetLocalResourceObject("Msg_txt_Supervisor").ToString();
            txt_DeliveredToMobile.Focus();
        }
        else if (PhotoIDType <= 0)
        {
            errorMessage = "Please Select PhotoID Type";// GetLocalResourceObject("Msg_txt_Supervisor").ToString();
            ddl_PhotoIDType.Focus();
        }
        else if (txt_PhotoIDNo.Text.Trim().Length <= 0)
        {
            errorMessage = "Please Enter PhotoID No.";// GetLocalResourceObject("Msg_txt_Supervisor").ToString();
            txt_PhotoIDNo.Focus();
        }
        else if (VehicleType > 0 && txt_Number_Part1.Text.Trim().Length < 2)
        {
            errorMessage = "Truck Number Should Not Be less than 2 characters";
            txt_Number_Part1.Focus();
        }
        else if (VehicleType > 0 && txt_Number_Part4.Text.Trim().Length < 1)
        {
            errorMessage = "Truck Number Should Should have atleast 1 Digit";
            txt_Number_Part4.Focus();
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

    private bool grid_validation()
    {
        bool ATS = true;
        string GC_No;

        if (Total_No_Of_GC > 0)
        {
            objDT = SessionBindGDCGrid;

            for (i = 0; i <= dg_GDC.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_GDC.Items[i].FindControl("Chk_Attach");
                txt_Del_Art = (TextBox)dg_GDC.Items[i].FindControl("txt_Delivery_Art");
                txt_Del_Wt = (TextBox)dg_GDC.Items[i].FindControl("txt_Delivery_Wt");
                Timepicker1 = (TimePicker)dg_GDC.Items[i].FindControl("TimePicker1");
                ddl_DeliveryStatus = (DropDownList)dg_GDC.Items[i].FindControl("ddl_DeliveryStatus");
                txtMobileNo = (TextBox)dg_GDC.Items[i].FindControl("txtMobileNo");
                hdn_StatusID = (HiddenField)dg_GDC.Items[i].FindControl("hdn_Status_Id");
                GC_No = dg_GDC.Items[i].Cells[1].Text;
                       
                string Del_datetime, unlod_datetime;
                DateTime dt_Deldatetime, dt_unloddatetime;

                Del_datetime = GDCDate.ToShortDateString() + " " + Timepicker1.getTime() + ":00";
                unlod_datetime = objDT.Rows[i]["AUS_Date"].ToString() + " " + objDT.Rows[i]["AUS_Time"].ToString() + ":00";
                
                dt_Deldatetime = Convert.ToDateTime(Del_datetime);
                dt_unloddatetime = Convert.ToDateTime(unlod_datetime);

                if (chk.Checked == true && Util.String2Int(txt_Del_Art.Text) <= 0)
                {
                    errorMessage = "Delivered Articles Can't be Zero";
                    txt_Del_Art.Focus();
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Int(txt_Del_Art.Text) > Util.String2Int(objDT.Rows[i]["Balance_Articles"].ToString()))
                {
                    errorMessage = "Delivered Articles Can't be greater than Balance Articles";
                    txt_Del_Art.Focus();
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Decimal(txt_Del_Wt.Text) <= 0)
                {
                    errorMessage = "Delivered Actual Wt Can't be Zero";
                    txt_Del_Wt.Focus();
                    ATS = false;
                    break;
                }
                else if (chk.Checked == true && Util.String2Decimal(txt_Del_Wt.Text) > Util.String2Decimal(objDT.Rows[i]["Balance_Actual_Wt"].ToString()))
                {
                    errorMessage = "Delivered Actual Wt Can't be greater than Balance Actual Wt";
                    txt_Del_Wt.Focus();
                    ATS = false;
                    break;
                }
                //else if (chk.Checked == true && dt_Deldatetime < dt_unloddatetime)
                //{
                //    errorMessage = "Delivery Date & Time Can't be less than Actual Unloading Date & Time";
                //    ATS = false;
                //    break;
                //}
                else if (chk.Checked == true && Util.String2Int(objDT.Rows[i]["Octroi_Updated"].ToString()) == 0)
                {
                    errorMessage = "Octroi Not Updated. , Please Update Octroi For " + CompanyManager.getCompanyParam().GcCaption + " :" + GC_No;
                    ATS = false;
                    break;
                }
                else if (objGDCPresenter.ValidateConsigneeClient() == false)
                {
                    errorMessage = "Select LR of Same Consignee";
                    ATS = false;
                    break;
                }
                //else if (chk.Checked == true && Util.String2Int(objDT.Rows[i]["is_updated"].ToString()) == 0 && Util.String2Bool(objDT.Rows[i]["IsDelDetailsReq"].ToString()) == true)
                //{
                //    errorMessage = "Please Mention Delivery Details, For " + CompanyManager.getCompanyParam().GcCaption + " :" + GC_No;
                //    ATS = false;
                //    break;
                //}
                else if (chk.Checked == true && (Util.String2Int(txt_Del_Art.Text) != Util.String2Int(objDT.Rows[i]["Balance_Articles"].ToString()) && Util.String2Decimal(txt_Del_Wt.Text) == Util.String2Decimal(objDT.Rows[i]["Balance_Actual_Wt"].ToString())))
                {
                    errorMessage = "Please Enter Proper Loaded Actual Wt.";
                    txt_Del_Wt.Focus();
                    ATS = false;
                    break;
                }
                else if (txtMobileNo.Text.Trim() == string.Empty)
                {
                    errorMessage = "Please Enter Mobile No For " + objDT.Rows[i]["GC_No_For_Print"].ToString();
                    SCM_GDS.SetFocus(txtMobileNo);
                    ATS = false;
                    break;
                }
                else if (Util.String2Int(ddl_DeliveryStatus.SelectedValue) == 0)
                {
                    errorMessage = "Please Select Delivery Status For " + objDT.Rows[i]["GC_No_For_Print"].ToString();
                    SCM_GDS.SetFocus(ddl_DeliveryStatus);
                    ATS = false;
                    break;
                }
                else if (Util.String2Int(ddl_DeliveryStatus.SelectedValue) > 0)
                {
                    //1	C Cash //2 Q Cheque //3 R Credit //4 T Return
                    if (Util.String2Int(ddl_DeliveryStatus.SelectedValue) == 1)
                    {

                    }
                    else if (Util.String2Int(ddl_DeliveryStatus.SelectedValue) == 2)
                    {
                        string ChequeNo = objDT.Rows[i]["ChequeNo"].ToString();
                        string ChequeDate = objDT.Rows[i]["ChequeDate"].ToString();
                        string BankName = objDT.Rows[i]["BankName"].ToString();
                        if (ChequeNo == "" || ChequeDate == "" || BankName == "")
                        {
                            errorMessage = "Please Enter Bank Details For " + objDT.Rows[i]["GC_No_For_Print"].ToString();
                            ATS = false;
                            break;
                        }
                    }
                    else if (Util.String2Int(ddl_DeliveryStatus.SelectedValue) == 3)
                    {
                        string PartyName = objDT.Rows[i]["PartyName"].ToString();
                        string PartyfromBranchLocation = objDT.Rows[i]["PartyfromBranchLocation"].ToString();

                        if (PartyName == "" || PartyfromBranchLocation == "")
                        {
                            errorMessage = "Please Enter Client's Billing Details For " + objDT.Rows[i]["GC_No_For_Print"].ToString();
                            ATS = false;
                            break;
                        }
                    }
                    else if (Util.String2Int(ddl_DeliveryStatus.SelectedValue) == 4)
                    {
                        string ReasonforNonDly = objDT.Rows[i]["ReasonforNonDly"].ToString();

                        if (ReasonforNonDly == "0" || ReasonforNonDly == "")
                        {
                            errorMessage = "Please Enter Reason For Non Delivery For " + objDT.Rows[i]["GC_No_For_Print"].ToString();
                            ATS = false;
                            break;
                        }
                    }
                    else if (Util.String2Int(ddl_DeliveryStatus.SelectedValue) == 8)
                    {
                        string CouponNo = objDT.Rows[i]["CouponNo"].ToString();
                        int CouponAmount = Util.String2Int(objDT.Rows[i]["CouponAmount"].ToString());

                        if (CouponNo == "0" || CouponNo == "" || CouponAmount == 0)
                        {
                            errorMessage = "Please Enter Valid Coupon No. For " + objDT.Rows[i]["GC_No_For_Print"].ToString();
                            ATS = false;
                            break;
                        }
                    }
                } 
                else
                {
                    ATS = true;
                }
            }
        }
        return ATS;
    }

    protected void ddl_DeliveryStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox txt_del_art, txt_del_wt, txt_lol_Tmp_frgt, txt_Total_GC_Amount, lbl_txt_ChequeAmt, lbl_txt_CreditAmt;
        CheckBox chk;
        LinkButton lbtn_details;
        decimal sum_txt_Total_GC_AmountC = 0;
        decimal sum_txt_Total_GC_AmountQ = 0;
        decimal sum_txt_Total_GC_AmountR = 0;
        //1	C Cash //2 Q Cheque //3 R Credit //4 T Return 
        ddl = (DropDownList)sender; 
        DataGridItem _item = (DataGridItem)ddl.Parent.Parent;

        chk = (CheckBox)_item.FindControl("Chk_Attach");

        //txt_Actual_Status = (TextBox)_item.FindControl("txt_Actual_Status");
        //hdn_Status = (HiddenField)_item.FindControl("hdn_Status");
        //hdn_Actual_Status_Id = (HiddenField)_item.FindControl("hdn_Actual_Status_Id");
        //txt_Del_taken_by = (TextBox)_item.FindControl("txt_Delivery_TakenBy");
        //ddl_DeliveryLocation = (DropDownList)_item.FindControl("ddl_DeliveryLocation");
        //txt_lol_Tmp_frgt = (TextBox)_item.FindControl("txt_Local_Tempo_Freight");
        //lbl_txt_ChequeAmt = (TextBox)this.FindControl("lbl_txt_ChequeAmt");
        //lbl_txt_CreditAmt = (TextBox)this.FindControl("lbl_txt_CreditAmt");
        //hdn_ChequeAmt = (TextBox)this.FindControl("hdn_ChequeAmt");
        //hdn_CreditAmt = (TextBox)this.FindControl("hdn_CreditAmt");
        lbtn_details = (LinkButton)_item.FindControl("lbtn_details");
        hdn_StatusID = (HiddenField)_item.FindControl("hdn_Status_Id");
        ddl_DeliveryStatus = (DropDownList)_item.FindControl("ddl_DeliveryStatus");
        txt_del_art = (TextBox)_item.FindControl("txt_Delivery_Art");
        txt_del_wt = (TextBox)_item.FindControl("txt_Delivery_Wt");
        txt_Total_GC_Amount = (TextBox)_item.FindControl("txt_Total_GC_Amount");

        CalculateTotal();

        if (ddl.SelectedItem.Value != "4")
        {
            //txt_Actual_Status.Text = hdn_Status.Value;
            //hdn_Actual_Status_Id.Value = hdn_StatusID.Value;
            //ddl_Undelivered_Reason.SelectedValue = "0";
            //ddl_Undelivered_Reason.Enabled = false;
            //txt_Del_taken_by.Enabled = true;
            //lbtn_DelDetails.Enabled = true;
            //hdn_ChequeAmt.Text = lbl_txt_ChequeAmt.Text;
            //hdn_CreditAmt.Text = lbl_txt_CreditAmt.Text;
            //hdn_TotalCash.Text = lbl_txt_TotalCash.Text;
            //hdn_Local_TotalTempo_Freight.Value = Util.Decimal2String(Util.String2Decimal(hdn_Local_TotalTempo_Freight.Value) + Util.String2Decimal(txt_lol_Tmp_frgt.Text));
            //hdn_CashReceived.Value = txt_CashReceived.Text;
            _item.Cells[12].Enabled = true;
            hdn_totalDelArt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelArt.Value) + Util.String2Decimal(txt_del_art.Text));
            hdn_totalDelWt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelWt.Value) + Util.String2Decimal(txt_del_wt.Text));
            hdn_Total_GC.Value = lbl_Total_GC.Text;
            chk.Checked = true;
            if (ddl.SelectedItem.Value == "2" || ddl.SelectedItem.Value == "3")
            {
                //lbtn_details.Focus();
                SCM_GDS.SetFocus(lbtn_details);   
            }
        }
        else
        {
            
            //ddl_Undelivered_Reason.Enabled = true;
            //txt_Del_taken_by.Enabled = false;
            //txt_Del_taken_by.Text = "";
            //lbtn_DelDetails.Enabled = false;
            //hdn_CashReceived.Value = txt_CashReceived.Text;
            //hdn_ChequeAmt.Text = lbl_txt_ChequeAmt.Text;
            //hdn_CreditAmt.Text = lbl_txt_CreditAmt.Text;
            //hdn_TotalCash.Text = lbl_txt_TotalCash.Text;
            //hdn_Local_TotalTempo_Freight.Value = Util.Decimal2String(Util.String2Decimal(hdn_Local_TotalTempo_Freight.Value) - Util.String2Decimal(txt_lol_Tmp_frgt.Text));
            _item.Cells[12].Enabled = false;
            hdn_totalDelArt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelArt.Value) - Util.String2Decimal(txt_del_art.Text));
            hdn_totalDelWt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelWt.Value) - Util.String2Decimal(txt_del_wt.Text));
            hdn_Total_GC.Value = lbl_Total_GC.Text;
            chk.Checked = false;
        }

    }

    private void CalculateTotal()
    {
        //Total_GC_AmountC = 0;
        //Total_GC_AmountQ = 0;
        //Total_GC_AmountR = 0;
        //Total_Delivery_Wt = 0;
        //Total_Local_Tempo_Frt = 0;
        Total_No_Of_GC = 0;
        int i;

        //txt_CashReceived = (TextBox)this.FindControl("txt_CashReceived");
        objDT = SessionBindGDCGrid;

        if (dg_GDC.Items.Count > 0)
        {
            for (i = 0; i <= dg_GDC.Items.Count - 1; i++)
            {
                //txt_Payment_Type = (TextBox)dg_GDC.Items[i].FindControl("txt_Payment_Type");
                ddlDeliveryStatus = (DropDownList)dg_GDC.Items[i].FindControl("ddl_DeliveryStatus");
                //txt_Total_GC_AmountC = (TextBox)dg_GDC.Items[i].FindControl("txt_Total_GC_Amount");
                txt_Total_Delivery_Wt = (TextBox)dg_GDC.Items[i].FindControl("txt_Delivery_Wt");
                txt_Total_Delivery_Art = (TextBox)dg_GDC.Items[i].FindControl("txt_Delivery_Art");
                //txt_Total_Local_Tempo_Freight = (TextBox)dg_GDC.Items[i].FindControl("txt_Local_Tempo_Freight");
                //if (txt_Payment_Type.Text == "To Pay")
                //{
                if (ddlDeliveryStatus.SelectedValue == "1")
                {
                    //Total_GC_AmountC = Total_GC_AmountC + Convert.ToDecimal(txt_Total_GC_AmountC.Text);
                    objDT.Rows[i]["Client_ID"] = 0; 
                    objDT.Rows[i]["ChequeNo"] = "";
                    objDT.Rows[i]["BankName"] = "";
                    objDT.Rows[i]["PartyName"] = "";
                    objDT.Rows[i]["BillingLocationId"] = 0;
                    objDT.Rows[i]["PartyfromBranchLocation"] = "";
                    objDT.Rows[i]["ReasonForNonDly"] = 0;
                    objDT.Rows[i]["ChequeDate"] = GDCDate.ToString("dd MMM yyyy"); 

                }
                else if (ddlDeliveryStatus.SelectedValue == "2")
                {
                    //Total_GC_AmountQ = Total_GC_AmountQ + Convert.ToDecimal(txt_Total_GC_AmountC.Text);

                    objDT.Rows[i]["Client_ID"] = 0;
                    objDT.Rows[i]["PartyName"] = "";
                    objDT.Rows[i]["BillingLocationId"] = 0;
                    objDT.Rows[i]["PartyfromBranchLocation"] = "";
                    objDT.Rows[i]["ReasonForNonDly"] = 0; 

                }
                else if (ddlDeliveryStatus.SelectedValue == "3")
                {
                    //Total_GC_AmountR = Total_GC_AmountR + Convert.ToDecimal(txt_Total_GC_AmountC.Text);
                    objDT.Rows[i]["ChequeNo"] = "";
                    objDT.Rows[i]["BankName"] = "";
                    objDT.Rows[i]["ReasonForNonDly"] = 0; 
                }
                else if (ddlDeliveryStatus.SelectedValue == "4" || ddlDeliveryStatus.SelectedValue == "7")
                {
                    //txt_Actual_Status.Text = "UnDelivered";
                    //hdn_Actual_Status_Id.Value = "300";
                    //ddl_Undelivered_Reason.Enabled = true;
                    //txt_Del_taken_by.Enabled = false;
                    //txt_Del_taken_by.Text = "";

                    objDT.Rows[i]["ChequeNo"] = "";
                    objDT.Rows[i]["BankName"] = "";
                    objDT.Rows[i]["Client_ID"] = 0;
                    objDT.Rows[i]["PartyName"] = "";
                    objDT.Rows[i]["BillingLocationId"] = 0;
                    objDT.Rows[i]["PartyfromBranchLocation"] = ""; 
                }
                //}
                if (ddlDeliveryStatus.SelectedValue != "0")
                {
                    Total_Delivery_Wt = Total_Delivery_Wt + Convert.ToDecimal(txt_Total_Delivery_Wt.Text);
                    //Total_Local_Tempo_Frt = Total_Local_Tempo_Frt + Convert.ToDecimal(txt_Total_Local_Tempo_Freight.Text);
                    Total_Delivery_Art = Total_Delivery_Art + Convert.ToDecimal(txt_Total_Delivery_Art.Text);
                }

            }
            Total_No_Of_GC = dg_GDC.Items.Count;

            lbl_Total_GC.Text = Convert.ToString(Total_No_Of_GC);

            //lbl_txt_TotalCash.Text = Convert.ToString(Total_GC_AmountC);
            //lbl_txt_ChequeAmt.Text = Convert.ToString(Total_GC_AmountQ);
            //lbl_txt_CreditAmt.Text = Convert.ToString(Total_GC_AmountR);

            //lbl_txt_BalancedCash.Text = Convert.ToString(Convert.ToDecimal(lbl_txt_TotalCash.Text) - Convert.ToDecimal(txt_CashReceived.Text));

            //lbl_Local_TotalTempo_Freight.Text = Convert.ToString(Total_Local_Tempo_Frt);

            lbl_totalDelWt.Text = Convert.ToString(Total_Delivery_Wt);
            lbl_totalDelArt.Text = Convert.ToString(Total_Delivery_Art);
        }
    }

    protected void Chk_Attach_CheckedChanged(object sender, EventArgs e)
    {
        TextBox txt_del_art, txt_del_wt;

        chk = (CheckBox)sender;
        DataGridItem _item = (DataGridItem)chk.Parent.Parent;
        
        ddl_DeliveryStatus = (DropDownList)_item.FindControl("ddl_DeliveryStatus");
        txt_del_art = (TextBox)_item.FindControl("txt_Delivery_Art");
        txt_del_wt = (TextBox)_item.FindControl("txt_Delivery_Wt");

        Assign_Hidden_Values_To_TextBox();
    }

    private void calculate_griddetails()
    {
        Total_No_Of_GC = 0;

        if (dg_GDC.Items.Count > 0)
        {
            objDT = SessionBindGDCGrid;

            for (i = 0; i <= dg_GDC.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_GDC.Items[i].FindControl("Chk_Attach");
                txt_Del_Art = (TextBox)dg_GDC.Items[i].FindControl("txt_Delivery_Art");
                txt_Del_Wt = (TextBox)dg_GDC.Items[i].FindControl("txt_Delivery_Wt");
                Timepicker1 = (TimePicker)dg_GDC.Items[i].FindControl("TimePicker1");
                txtMobileNo = (TextBox)dg_GDC.Items[i].FindControl("txtMobileNo");
                hdn_StatusID = (HiddenField)dg_GDC.Items[i].FindControl("hdn_Status_Id");
                ddl_DeliveryStatus = (DropDownList)dg_GDC.Items[i].FindControl("ddl_DeliveryStatus");

                if (chk.Checked == true)
                {
                    Total_No_Of_GC = Total_No_Of_GC + 1;
                    objDT.Rows[i]["Previous_Status_ID"] = Util.String2Int(hdn_StatusID.Value);
                }
                objDT.Rows[i]["Att"] = chk.Checked;
                objDT.Rows[i]["Delivery_Articles"] = Util.String2Int(txt_Del_Art.Text);
                objDT.Rows[i]["Delivery_Actual_Wt"] = Util.String2Decimal(txt_Del_Wt.Text); 
                objDT.Rows[i]["Dly_Pay_Mode_Id"] = Util.String2Int(ddl_DeliveryStatus.SelectedValue);
                objDT.Rows[i]["MobileNo"] = txtMobileNo.Text;
                objDT.Rows[i]["Delivery_Time"] = Timepicker1.getTime();
            }
        }

    }

    private void Next_GDC_Number()
    {
        GDCNo = objComm.Get_Next_Number();
    }

    private void OnGetGCXML(object sender, EventArgs e)
    {
        if (WucSelectedItems1.EnterItem != string.Empty)
        {
            _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
            objGDCPresenter.fillgrid();
            WucSelectedItems1.dtdetails = SessionBindGDCGrid;

            BindGDCGrid();
            Assign_Hidden_Values_For_Reset();
            WucSelectedItems1.Get_Not_Selected_Items();
            //setfousonGrid();
        }

    }
    private void setfousonGrid()
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyFun1", "setfousonGrid();", true);
    }

    private void Assign_Hidden_Values_To_TextBox()
    {
        lbl_Total_GC.Text = hdn_Total_GC.Value;
        lbl_totalDelArt.Text = hdn_totalDelArt.Value;
        lbl_totalDelWt.Text = hdn_totalDelWt.Value;
    }

    private void Assign_Hidden_Values_For_Reset()
    {
        hdn_Total_GC.Value = "0";
        hdn_totalDelArt.Value = "0";
        hdn_totalDelWt.Value = "0";

        lbl_Total_GC.Text = "0";
        lbl_totalDelArt.Text = "0";
        lbl_totalDelWt.Text = "0";
    }
    private void SetStandardCaption()
    {
        const int GCNoCaption = 1;  ///change usermanager to companymanager by Ankit
        hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
        WucSelectedItems1.SetFoundCaption = "Enter  " + hdn_GCCaption.Value + "   Nos.:";
        WucSelectedItems1.SetNotFoundCaption = hdn_GCCaption.Value + "  Nos.Not Found :";
        WucSelectedItems1.Set_GCCaption = hdn_GCCaption.Value;

        Label1.Text = "Total  " + hdn_GCCaption.Value + ":";
        dg_GDC.Columns[GCNoCaption].HeaderText = hdn_GCCaption.Value + "  No";
    }
    #endregion
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            TD_Calender.Visible = false;
            dtp_GDC_Date.Enabled = false;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        SetStandardCaption();
        ddl_GodownSupervisor.DataTextField = "Emp_Name";
        ddl_GodownSupervisor.DataValueField = "Emp_ID";

        WucSelectedItems1.GetSelectedItemsXMLButtonClick += new EventHandler(OnGetGCXML);

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Close, btn_Save_Print));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Close, btn_Save_Print));
        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page,btn_Save_Print, btn_Save_Exit, btn_Save, btn_Close));

        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        if (!IsPostBack)
        {
            Assign_Hidden_Values_For_Reset();
        }
        objGDCPresenter = new GDCPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            //hdf_ResourecString.Value = objComm.GetResourceString("Operations/Delivery/App_LocalResources/WucGDC.ascx.resx");

            BindGDCGrid();
            if (keyID <= 0)
            {
                Next_GDC_Number();
            }
            else
            {
                td_gccontrol.Style.Add("visibility", "hidden");

                if (VehicleType > 0)
                {
                    txt_Number_Part1.Enabled = true;
                    txt_Number_Part2.Enabled = true;
                    txt_Number_Part3.Enabled = true;
                    txt_Number_Part4.Enabled = true;
                }

            }
        }
        Assign_Hidden_Values_To_TextBox();
        
    }
    protected void dg_GDC_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        string calculate_grid = ""; string calculate_grid1 = "";
        string calculate_grid2 = ""; string calculate_grid3 = "";
        LinkButton lbtn_Details; //CheckBox chk_Attach;
        TextBox Txt_Delivered_Art, Txt_Delivered_Wt, txt_GCAmount; ;
        TimePicker Timepicker1;
        LinkButton lbtn_GCNoForPrint;
        HiddenField hdn_GCNoForPrint;

        string current_time = DateTime.Now.ToShortTimeString();

        if (e.Item.ItemIndex != -1)
        {
            chk = (CheckBox)e.Item.FindControl("Chk_Attach");
            Txt_Delivered_Art = (TextBox)e.Item.FindControl("txt_Delivery_Art");
            Txt_Delivered_Wt = (TextBox)e.Item.FindControl("txt_Delivery_Wt");
            lbtn_Details = (LinkButton)e.Item.FindControl("lbtn_details");
            Timepicker1 = (TimePicker)e.Item.FindControl("TimePicker1");
            txtMobileNo = (TextBox)e.Item.FindControl("txtMobileNo");
            hdn_GC_Id = (HiddenField)e.Item.FindControl("hdn_GC_Id");
            hdn_Article_Id = (HiddenField)e.Item.FindControl("hdn_Article_Id");

            txt_GCAmount = (TextBox)e.Item.FindControl("txt_Total_GC_Amount");

            txtMobileNo.Text = SessionBindGDCGrid.Rows[e.Item.ItemIndex]["MobileNo"].ToString();
            ddl_DeliveryStatus = (DropDownList)e.Item.FindControl("ddl_DeliveryStatus");  
            ddl_DeliveryStatus.DataTextField = "Dly_Pay_Mode_Name";
            ddl_DeliveryStatus.DataValueField = "Dly_Pay_Mode_Id";
            ddl_DeliveryStatus.DataSource = SessionBindDlyStatus;
            ddl_DeliveryStatus.DataBind();
            //ddl_DeliveryStatus.Items.Insert(0, new ListItem("Select One", "0"));
            ddl_DeliveryStatus.SelectedValue = SessionBindGDCGrid.Rows[e.Item.ItemIndex]["Dly_Pay_Mode_Id"].ToString();

            if (CompanyManager.getCompanyParam().IsPartLoadingRequired == false)
            {
                disable_Textbox(Txt_Delivered_Art, Txt_Delivered_Wt);
            }
            else
            {
                calculate_grid = "Check_Single(" + chk.ClientID + ",'j','2')";
                calculate_grid1 = "Check_Single(" + chk.ClientID + ",'j','3')";
                calculate_grid2 = "Check_Single(" + chk.ClientID + ",'j','4')";
                calculate_grid3 = "Check_Single(" + chk.ClientID + ",'j','5')";

                Txt_Delivered_Art.Attributes.Add("onblur", calculate_grid);
                Txt_Delivered_Wt.Attributes.Add("onblur", calculate_grid1);
                Txt_Delivered_Art.Attributes.Add("onfocus", calculate_grid2);
                Txt_Delivered_Wt.Attributes.Add("onfocus", calculate_grid3);
            }
            Timepicker1.setFormat("24");

            if (Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Octroi_Updated").ToString()) == 0) 
            {
                e.Item.CssClass = "NOTUPDATEDLBL";
            }

            if (keyID > 0)
            {
                Timepicker1.setTime(SessionBindGDCGrid.Rows[e.Item.ItemIndex]["Delivery_Time"].ToString());
            }
            else
            {
                Timepicker1.setTime(current_time);
            }
            ds_index = e.Item.ItemIndex;

            StringBuilder Path = new StringBuilder(Util.GetBaseURL());
            Path.Append("/");
            Path.Append("Operations/Delivery/FrmDDCPaymentDetails.aspx?ds_index=" + Util.EncryptInteger(ds_index) + "&_menuItemid=" + Util.EncryptInteger(Raj.EC.Common.GetMenuItemId()) + "&Mode=" + Mode + "&DDCId=" + keyID + "&hdn_GDC_Date=" + hdn_GDC_Date.Value);
            lbtn_Details.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "'," + ddl_DeliveryStatus.ClientID + "," + hdn_GC_Id.ClientID + "," + hdn_Article_Id.ClientID + "," + txt_GCAmount.Text + ")");
            //Path.Append("Operations/Delivery/FrmGDCGridDetails.aspx?ds_index=" + Util.EncryptInteger(ds_index) + "&_menuItemid=" + Util.EncryptInteger(Raj.EC.Common.GetMenuItemId()) + "&Mode=" + Mode);
            //lbtn_Details.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "')");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                lbtn_GCNoForPrint = (LinkButton)e.Item.FindControl("lbtn_GCNoForPrint");
                hdn_GCNoForPrint = (HiddenField)e.Item.FindControl("hdn_GCNoForPrint");

                if (Mode == "2")
                {
                    int MenuItemId = Common.GetMenuItemId();

                    int Document_ID = Convert.ToInt32(hdn_GCNoForPrint.Value);
                    StringBuilder Path2 = new StringBuilder(Util.GetBaseURL());
                    Path2.Append("/");
                    Path2.Append("Reports/Direct_Printing/frmGCDlyReceiptViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID));
                    lbtn_GCNoForPrint.Attributes.Add("onclick", "return Open_Dly_Receipt('" + Path2 + "'," + ddl_DeliveryStatus.ClientID + ")");
                }
            }
            if (chk.Checked == true)
            {
                Chk_Attach_CheckedChanged(chk, e);
            }
            else
            {
                
                e.Item.Cells[12].Enabled = false;
            }
        }
    }

    protected void dg_GDC_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Header)
        //{
        //    dg_GDC.Items[0].Cells[0].Focus();
        //}
        //CheckBox allChk = (CheckBox)e.Item.Cells[1].FindControl("chkAllItems"); 
        //SCM_GDS.SetFocus(allChk);
    }

    private void disable_Textbox(TextBox txtbox1, TextBox txtbox2)
    {
        txtbox1.BackColor = Color.Transparent;
        txtbox1.BorderColor = Color.Transparent;
        txtbox1.ReadOnly = true;

        txtbox2.BackColor = Color.Transparent;
        txtbox2.BorderColor = Color.Transparent;
        txtbox2.ReadOnly = true;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndNew";
        calculate_griddetails();
        objGDCPresenter.save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _flag = "SaveAndExit";
        calculate_griddetails();
        objGDCPresenter.save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
      
        _flag = "SaveAndPrint";
        calculate_griddetails();
        objGDCPresenter.save();
    }
    protected void dtp_GDC_Date_SelectionChanged(object sender, EventArgs e)
    {
        Assign_Hidden_Values_For_Reset();
        Assign_Hidden_Values_To_TextBox();
        _GC_No_XML = WucSelectedItems1.GetSelectedItemsXML;
        objGDCPresenter.fillgrid();
        WucSelectedItems1.dtdetails = SessionBindGDCGrid;
        BindGDCGrid();
        WucSelectedItems1.Get_Not_Selected_Items();
        hdn_GDC_Date.Value = dtp_GDC_Date.SelectedDate.ToString(); 
    }

    public void ClearVariables()
    {
        SessionBindGDCGrid = null;
        SessionBindDDLDeliveryMode = null;
        SessionBindDlyStatus = null;
        SessionBindDDLUndelReason = null;
    }

    protected void btn_null_session_Click(object sender, EventArgs e) //added Ankit 21-02-09
    {
        ClearVariables();
    }

    protected void ddl_VehicleType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_Number_Part1.Text = "";
        txt_Number_Part2.Text = "";
        txt_Number_Part3.Text = "";
        txt_Number_Part4.Text = "";

        if (VehicleType == 0)
        {
            txt_Number_Part1.Enabled = false;
            txt_Number_Part2.Enabled = false;
            txt_Number_Part3.Enabled = false;
            txt_Number_Part4.Enabled = false;
        }
        else
        {
            txt_Number_Part1.Enabled = true;
            txt_Number_Part2.Enabled = true;
            txt_Number_Part3.Enabled = true;
            txt_Number_Part4.Enabled = true;

        }
    }

    protected void btn_DoorToGodown_Click(object sender, EventArgs e)
    {

        TextBox txtBox = (TextBox)WucSelectedItems1.FindControl("txt_get_not_item");

        StringBuilder Path = new StringBuilder(Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(285).AddUrl + "&Call_From=GDC"
            + "&GC_Nos=" + Util.EncryptString(txtBox.Text));

        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "DoorToGodown('" + Path + "');", true);

    }
}
