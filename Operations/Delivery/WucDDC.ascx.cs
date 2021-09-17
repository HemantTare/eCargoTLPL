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
/// Description   : This Page is For DDS Operation
/// </summary>
/// 

public partial class Operations_Delivery_WucDDC : System.Web.UI.UserControl, IDDCView
{
    #region ClassVariables
    DDCPresenter objDDCPresenter;
    IDDCView objIDDCView;
    Raj.EC.Common objComm = new Raj.EC.Common();
    DataTable objDT = new DataTable(); 
    private DAL objDAL = new DAL();
    private DataSet objDS;  
    string _flag = "";
    string Mode = "0";
    CheckBox chk;
    DropDownList ddl;
    HiddenField hdn_StatusID, hdn_Status, hdn_Actual_Status_Id, hdn_DeliveryStatus, hdn_GC_Id, hdn_Article_Id, hdn_Local_Tempo_Freight, hdn_Bonus, hdn_AddTempoFrt;
    TextBox txt_Del_TakenBy, txt_Actual_Status, txt_Del_taken_by, txt_Payment_Type, txtMobileNo, txt_Local_Tempo_Freight, txt_Bonus, txt_AddTempoFrt;
    DropDownList ddl_Undelivered_Reason, ddl_DeliveryStatus, ddl_DeliveryLocation, ddlDeliveryStatus;
    TimePicker Timepicker1;
    ComponentArt.Web.UI.Calendar dtp_DD_Date;
    int ds_index, i;
    TextBox txt, txt_Total_GC_AmountC, txt_Total_GC_AmountQ, txt_Total_GC_AmountR, txt_Total_GC_AmountM, txt_Total_GC_AmountS,txt_Total_GC_AmountP, txt_Total_Delivery_Wt, txt_Total_Local_Tempo_Freight, txt_Total_Delivery_Art, txt_Total_Bonus, txt_Total_AddTempoFrt;
    decimal Total_GC_AmountC, Total_GC_AmountQ, Total_GC_AmountR, Total_GC_AmountM, Total_GC_AmountS, Total_GC_AmountP, Total_Delivery_Wt, Total_Local_Tempo_Frt, Total_Delivery_Art, Total_Bonus, Total_AddTempoFrt;
    #endregion

    #region ControlsValues

    public string DDSNo
    {
        set { lbl_DDC_No.Text = value; }
    }
    public DateTime DDSDate
    {
        set 
        {
            dtp_DDS_Date.SelectedDate = value;
            hdn_DDS_Date.Value = dtp_DDS_Date.SelectedDate.ToString();
        } 
        get { return dtp_DDS_Date.SelectedDate; }
    }
    public int DeliveryModeID
    {
        get { return Util.String2Int(ddl_DeliveryMode.SelectedValue); }
        set { ddl_DeliveryMode.SelectedValue = Util.Int2String(value); }
    }
    public string DeliveryModeDescription
    {
        get { return ddl_DeliveryMode.SelectedItem.Text; }
        set
        {
            ddl_DeliveryMode.SelectedItem.Text = value;
            if (value != "")
            {
                lbl_DriverName.Text = value;
            }
        }
    }
    public string PDSDate
    {
        set { lbl_PDSDate.Text = value; }
        get { return lbl_PDSDate.Text; }
    }
    public string DiverName
    {
        set { txt_DriverName.Text = value; }
        get { return txt_DriverName.Text; }
    }
    public int SupervisorID
    {
        get { return Util.String2Int(ddl_Supervisor.SelectedValue); }
    }
    
    public void SetSupervisor(string text, string value)
    {
        ddl_Supervisor.DataTextField = "Emp_Name";
        ddl_Supervisor.DataValueField = "Emp_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Supervisor);
    }

    //public DataSet BindSupervisor
    //{
    //    set
    //    {
    //        ddl_Supervisor.DataSource = value;
    //        ddl_Supervisor.DataTextField = "Supervisor_Name";
    //        ddl_Supervisor.DataValueField = "Supervisor_Id";
    //        ddl_Supervisor.DataBind();
    //    }
    //}

    public int PDSId
    {
        get { return Util.String2Int(ddl_PDSNo.SelectedValue); }
        set { ddl_PDSNo.SelectedValue = Util.Int2String(value); }
    }
    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }

    public decimal Total_GC_Amount
    {
        set
        {
            hdn_TotalCash.Text = Util.Decimal2String(value);
            lbl_txt_TotalCash.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TotalCash.Text); }
    }
    public decimal Total_Cash_Received
    {
        set
        {
            hdn_CashReceived.Value = Util.Decimal2String(value);
            txt_CashReceived.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_CashReceived.Value); }
    }
    public decimal Total_Cash_Balance
    {
        set
        {
            hdn_BalancedCash.Value = Util.Decimal2String(value);
            lbl_txt_BalancedCash.Text = Util.Decimal2String(value);
        }
        //get { return Util.String2Decimal(hdn_BalancedCash.Value); }
        get { return Util.String2Decimal(lbl_txt_BalancedCash.Text); }
    }

    public decimal Total_ChequeAmt
    {
        set
        {
            hdn_ChequeAmt.Text = Util.Decimal2String(value);
            lbl_txt_ChequeAmt.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_ChequeAmt.Text); }
    }

    public decimal Total_MobilePay
    {
        set
        {
            hdn_MobilePay.Text = Util.Decimal2String(value);
            lbl_txt_MobilePay.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_MobilePay.Text); }
    }

    public decimal Total_SwipeCard
    {
        set
        {
            hdn_SwipeCard.Text = Util.Decimal2String(value);
            lbl_txt_SwipeCard.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_SwipeCard.Text); }
    }

    public decimal Total_PendingFreight
    {
        set
        {
            hdn_PendingFreight.Value = Util.Decimal2String(value);
            txt_PendingFreight.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_PendingFreight.Value); }
    }

    public decimal Total_CreditAmt
    {
        set
        {
            hdn_CreditAmt.Text = Util.Decimal2String(value);
            lbl_txt_CreditAmt.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_CreditAmt.Text); }
    }
    public int Total_Delivered_Articles
    {
        set
        {
            hdn_totalDelArt.Value = Util.Int2String(value);
            lbl_totalDelArt.Text =  Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_totalDelArt.Value); }
    }
    public decimal Total_Local_Tempo_Freight
    {
        set
        {
            hdn_Local_TotalTempo_Freight.Value = Util.Decimal2String(value);
            lbl_Local_TotalTempo_Freight.Text = Util.Decimal2String(value); 
        }
        get { return Util.String2Decimal(hdn_Local_TotalTempo_Freight.Value); }
    }

    public decimal Total_TempoBonus
    {
        set
        {
            hdn_TotalBonus.Value = Util.Decimal2String(value);
            lbl_TotalBonus.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TotalBonus.Value); }
    }

    public decimal Total_TempoAddTempoFrt
    {
        set
        {
            hdn_TotalAddTempoFrt.Value = Util.Decimal2String(value);
            lbl_TotalAddTempoFrt.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_TotalAddTempoFrt.Value); }
    }

    public decimal Total_Delivered_Weight
    {
        set
        {
            hdn_totalDelWt.Value = Util.Decimal2String(value);
            lbl_totalDelWt.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(hdn_totalDelWt.Value); }
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

    public int VehicleID
    {
        get { return WucVehicleSearch1.VehicleID; }
        set
        {
            WucVehicleSearch1.VehicleID = value;
            hdn_VehicleID.Value = Util.Int2String(value);
        }
    }

    public int VendorID
    {
        get { return WucVehicleSearch1.VehicleVendorID; }
        set
        {
            WucVehicleSearch1.VehicleVendorID = value;
            hdn_Vendor_ID.Value = Util.Int2String(value);
        }
    }
    public string VendorName
    {
        get { return lbltxt_Vendor.Text; }
        set { lbltxt_Vendor.Text = value; }
    }


    public int DeliveryStatusId
    {
        get { return Util.String2Int(ddl_DeliveryStatus.SelectedValue); }
        set { ddl_DeliveryStatus.SelectedValue = Util.Int2String(value); }
    }
    public int MobileNo
    {
        get { return Util.String2Int(txtMobileNo.Text); }
        set { txtMobileNo.Text = Util.Int2String(value); }
    }
   
    #endregion

    #region ControlsBind

    public DataTable BindDeliveryMode
    {
        set
        {
            ddl_DeliveryMode.DataSource = "";
            ddl_DeliveryMode.DataBind();

            ddl_DeliveryMode.DataTextField = "Delivery_Mode";
            ddl_DeliveryMode.DataValueField = "Delivery_Mode_ID";
            ddl_DeliveryMode.DataSource = value;
            ddl_DeliveryMode.DataBind();
            ddl_DeliveryMode.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    public DataTable BindPreDeliverySheet
    {
        set
        {
            ddl_PDSNo.DataSource = "";
            ddl_PDSNo.DataBind();

            ddl_PDSNo.DataTextField = "PDS_No";
            ddl_PDSNo.DataValueField = "PDS_ID";
            ddl_PDSNo.DataSource = value;
            ddl_PDSNo.DataBind();
            if (keyID < 0)
            {
                ddl_PDSNo.Items.Insert(0, new ListItem("Select PDS No", "0"));
            }
        }
    }

    public void BindDDSGrid()
    {
        dg_DDS.DataSource = SessionBindDDSGrid;
        dg_DDS.DataBind();
    }

    public DataTable SessionBindDDLUndelReason
    {
        get { return StateManager.GetState<DataTable>("BindDDLUndelReason"); }
        set { StateManager.SaveState("BindDDLUndelReason", value); }
    }
    public DataTable SessionBindDlyAreaGrid
    {
        get { return StateManager.GetState<DataTable>("BindDlyAreaGrid"); }
        set { StateManager.SaveState("BindDlyAreaGrid", value); }
    }
    public DataTable SessionBindDlyStatus
    {
        get { return StateManager.GetState<DataTable>("BindDlyStatus"); }
        set { StateManager.SaveState("BindDlyStatus", value); }
    }
    public DataTable SessionBindDDSGrid
    {
        get { return StateManager.GetState<DataTable>("BindDDSGrid"); }
        set
        {
            StateManager.SaveState("BindDDSGrid", value);
            if (StateManager.Exist("BindDDSGrid"))
            {
                BindDDSGrid();
                hdnforselectall.Value = Util.Int2String(SessionBindDDSGrid.Rows.Count);
            }
        }
    }
    
    public DataTable SessionBindDDLDeliveryMode
    {
        get { return StateManager.GetState<DataTable>("DDLDeliveryMode"); }
        set { StateManager.SaveState("DDLDeliveryMode", value); }
    }

    public String DDSDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();
            _objDs.Tables.Add(SessionBindDDSGrid.Copy());

            _objDs.Tables[0].TableName = "DDCGrid_Details";
            return _objDs.GetXml().ToLower();
        }
    }

    #endregion

    #region IView
    public bool validateUI()
    {
        //CalculateTotal();

        CalculateBalanceAmt();
        bool _isValid = false;
        TextBox Txt_GodownSupervisor;

        Txt_GodownSupervisor = (TextBox)ddl_Supervisor.FindControl("txtBoxddl_Supervisor");

        if (Datemanager.IsValidProcessDate("OPR_DDC", DDSDate) == false)
        {
            errorMessage = "Please Select Valid DDC Date";// GetLocalResourceObject("Msg_dtp_DDSDate").ToString();
        }
        else if (PDSId <= 0)
        {
            errorMessage = "Please Select PDS No";// GetLocalResourceObject("Msg_PDS_validation").ToString();
            ddl_PDSNo.Focus();
        }
        else if (DeliveryModeID <= 0)
        {
            errorMessage = "Please Select Delivery Mode";// GetLocalResourceObject("Msg_ddl_DeliveryMode").ToString();
            ddl_DeliveryMode.Focus();
        }
        //else if ((DeliveryModeID == 3 || DeliveryModeID == 4) && Total_Cash_Balance != 0 )
        else if (Total_Cash_Balance != 0 )
        {
            errorMessage = "Cash Balance Should be Zero";// GetLocalResourceObject("Msg_ddl_DeliveryMode").ToString();
            lbl_txt_BalancedCash.Focus();
        }
        else if (SupervisorID <= 0)
        {
            errorMessage = "Please Select Godown Supervisor";// GetLocalResourceObject("Msg_ddl_DeliveryMode").ToString();
            Txt_GodownSupervisor.Focus();
        }
        else if (SessionBindDDSGrid.Rows.Count <= 0)
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
    private void Next_DDS_Number()
    {
        DDSNo = objComm.Get_Next_Number();
    }

    private void Assign_Hidden_Values_To_TextBox()
    {

        lbl_Total_GC.Text = hdn_Total_GC.Value;
        //lbl_totalDelArt.Text = hdn_totalDelArt.Value;
        lbl_totalDelWt.Text = hdn_totalDelWt.Value;
        lbl_Local_TotalTempo_Freight.Text = Convert.ToString(hdn_Local_TotalTempo_Freight.Value);
        lbl_txt_TotalCash.Text = hdn_TotalCash.Text;
        lbl_txt_ChequeAmt.Text = hdn_ChequeAmt.Text;
        lbl_txt_CreditAmt.Text = hdn_CreditAmt.Text;
        lbl_txt_MobilePay.Text = hdn_MobilePay.Text;
        lbl_txt_SwipeCard.Text = hdn_SwipeCard.Text;
        txt_PendingFreight.Text = hdn_PendingFreight.Value;

        lbl_TotalBonus.Text = Convert.ToString(hdn_TotalBonus.Value);
        lbl_TotalAddTempoFrt.Text = Convert.ToString(hdn_TotalAddTempoFrt.Value);
    }

    private void Assign_Hidden_Values_For_Reset()
    {
        hdn_Total_GC.Value = "0";
        hdn_totalDelArt.Value = "0";
        hdn_totalDelWt.Value = "0";
        hdn_Local_TotalTempo_Freight.Value = "0";
        hdn_TotalCash.Text = "0";
        hdn_BalancedCash.Value = "0";
        hdn_CashReceived.Value = "0";
        hdn_ChequeAmt.Text = "0";
        hdn_CreditAmt.Text = "0";
        hdn_Vendor_ID.Value = "0";
        hdn_MobilePay.Text = "0";
        hdn_SwipeCard.Text = "0";
        hdn_PendingFreight.Value = "0";

        hdn_TotalBonus.Value = "0";
        hdn_TotalAddTempoFrt.Value = "0";

        //lbl_Total_GC.Text = "0";
        lbl_totalDelArt.Text = "0";
        lbl_totalDelWt.Text = "0";
        lbl_Local_TotalTempo_Freight.Text = "0";
        lbl_txt_TotalCash.Text = "0";
        lbl_txt_BalancedCash.Text = "0";
        txt_CashReceived.Text = "0"; 
        lbl_txt_ChequeAmt.Text = "0";
        lbl_txt_CreditAmt.Text = "0";
        txt_DriverName.Text = "";
        lbltxt_Vendor.Text = "";
        lbl_PDSDate.Text = "";
        lbl_txt_MobilePay.Text = "0";
        lbl_txt_SwipeCard.Text = "0";
        txt_PendingFreight.Text = "0";

        lbl_TotalBonus.Text = "0";
        lbl_TotalAddTempoFrt.Text = "0";
    }
    private void SetStandardCaption()
    {
        const int GCNoCaption = 1;       //change usermanager to companymanager by Ankit 
        hdn_GCCaption.Value = CompanyManager.getCompanyParam().GcCaption;
        dg_DDS.Columns[GCNoCaption].HeaderText = CompanyManager.getCompanyParam().GcCaption + "  No";
    }
    #endregion

    #region OtherProperty

    private bool grid_validation()
    {
        bool ATS = true;
        string GC_No;

        objDT = SessionBindDDSGrid;

        if (objDT.Rows.Count > 0)
        {

            for (i = 0; i <= dg_DDS.Items.Count - 1; i++)
            {
                txt_Del_TakenBy = (TextBox)dg_DDS.Items[i].FindControl("txt_Delivery_TakenBy");
                dtp_DD_Date = (ComponentArt.Web.UI.Calendar)dg_DDS.Items[i].FindControl("dtp_DD_Date");
                Timepicker1 = (TimePicker)dg_DDS.Items[i].FindControl("TimePicker1");
                ddl_Undelivered_Reason = (DropDownList)dg_DDS.Items[i].FindControl("ddl_UnDel_Reason");
                ddl_DeliveryLocation = (DropDownList)dg_DDS.Items[i].FindControl("ddl_DeliveryLocation");
                ddl_DeliveryStatus = (DropDownList)dg_DDS.Items[i].FindControl("ddl_DeliveryStatus");
                txtMobileNo = (TextBox)dg_DDS.Items[i].FindControl("txtMobileNo");
                hdn_Actual_Status_Id = (HiddenField)dg_DDS.Items[i].FindControl("hdn_Actual_Status_Id");
                GC_No = dg_DDS.Items[i].Cells[1].Text;

                string Del_datetime, unlod_datetime;
                DateTime dt_Deldatetime, dt_unloddatetime;

                Del_datetime = dtp_DD_Date.SelectedDate.ToShortDateString() + " " + Timepicker1.getTime();
                unlod_datetime = objDT.Rows[i]["AUS_Date"].ToString() + " " + objDT.Rows[i]["AUS_Time"].ToString();

                dt_Deldatetime = Convert.ToDateTime(Del_datetime);
                //dt_unloddatetime = Convert.ToDateTime(unlod_datetime);


                //if (Convert.ToDateTime(dtp_DD_Date.SelectedDate) < Convert.ToDateTime(PDSDate))
                //{
                //    errorMessage = "Door Delivery Date should be greater than PDS Date";
                //    ATS = false;
                //    break;
                //}
                //else if (Convert.ToDateTime(dtp_DD_Date.SelectedDate) > Convert.ToDateTime(DDSDate))
                //{
                //    errorMessage = "Door Delivery Date should be less than DDC Date";
                //    ATS = false;
                //    break;
                //}
                //else 
                if (txtMobileNo.Text.Trim() == string.Empty)
                {
                    errorMessage = "Please Enter Mobile No For " + objDT.Rows[i]["GC_No_For_Print"].ToString();
                    scm_dds.SetFocus(txtMobileNo);
                    ATS = false;
                    break;
                }
                else if (txtMobileNo.Text.Length < 10)
                {
                    errorMessage = "Please Enter Mobile No For " + objDT.Rows[i]["GC_No_For_Print"].ToString();
                    scm_dds.SetFocus(txtMobileNo);
                    ATS = false;
                    break;
                }  
                //else if (Util.String2Int(hdn_Actual_Status_Id.Value) == 300 && Util.String2Int(ddl_Undelivered_Reason.SelectedValue) == 0)
                //{
                //    errorMessage = "Please Select Undelivered Reason";
                //    scm_dds.SetFocus(ddl_Undelivered_Reason);
                //    ATS = false;
                //    break;
                //}
                else if (Util.String2Int(ddl_DeliveryLocation.SelectedValue) == 0)
                {
                    errorMessage = "Please Select Delivery Area For " + objDT.Rows[i]["GC_No_For_Print"].ToString();
                    scm_dds.SetFocus(ddl_DeliveryLocation);
                    ATS = false;
                    break;
                }
                else if (Util.String2Int(ddl_DeliveryStatus.SelectedValue) == 0)
                {
                    errorMessage = "Please Select Delivery Status For " + objDT.Rows[i]["GC_No_For_Print"].ToString();
                    scm_dds.SetFocus(ddl_DeliveryStatus);
                    ATS = false;
                    break;
                }
                else if (Util.String2Int(ddl_DeliveryStatus.SelectedValue) > 0)
                {
                    //1	C Cash //2 Q Cheque //3 R Credit //4 T Return //7 P Pending
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
                    else if (Util.String2Int(ddl_DeliveryStatus.SelectedValue) == 7)
                    {
                        string ReasonforPendingFrt = objDT.Rows[i]["ReasonforNonDly"].ToString();

                        if (ReasonforPendingFrt == "0" || ReasonforPendingFrt == "")
                        {
                            errorMessage = "Please Enter Reason For Pending Freight " + objDT.Rows[i]["GC_No_For_Print"].ToString();
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
                //else if (dt_Deldatetime < dt_unloddatetime)
                //{
                //    errorMessage = "Delivery Date & Time Can't be less than Actual Unloading Date & Time";
                //    ATS = false;
                //    break;
                //}
                //else if (Util.String2Int(hdn_Actual_Status_Id.Value) != 300 && txt_Del_TakenBy.Text.Trim() == string.Empty)
                //{
                //    errorMessage = "Please Enter Delivery Taken By";
                //    scm_dds.SetFocus(txt_Del_TakenBy);
                //    ATS = false;
                //    break;
                //}
                else if (Util.String2Bool(objDT.Rows[i]["IsDelDetailsReq"].ToString()) == true && Util.String2Int(objDT.Rows[i]["is_updated"].ToString()) == 0)
                {
                    errorMessage = "Please Mention Delivery Details, For " + CompanyManager.getCompanyParam().GcCaption + " :" + GC_No;
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
        if (dg_DDS.Items.Count > 0)
        {
            objDT = SessionBindDDSGrid;

            for (i = 0; i <= dg_DDS.Items.Count - 1; i++)
            {
                chk = (CheckBox)dg_DDS.Items[i].FindControl("Chk_Attach");
                txt_Del_TakenBy = (TextBox)dg_DDS.Items[i].FindControl("txt_Delivery_TakenBy");
                ddl_Undelivered_Reason = (DropDownList)dg_DDS.Items[i].FindControl("ddl_UnDel_Reason");
                ddl_DeliveryLocation = (DropDownList)dg_DDS.Items[i].FindControl("ddl_DeliveryLocation");
                ddl_DeliveryStatus = (DropDownList)dg_DDS.Items[i].FindControl("ddl_DeliveryStatus");
                txtMobileNo = (TextBox)dg_DDS.Items[i].FindControl("txtMobileNo");
                Timepicker1 = (TimePicker)dg_DDS.Items[i].FindControl("TimePicker1");
                dtp_DD_Date = (ComponentArt.Web.UI.Calendar)dg_DDS.Items[i].FindControl("dtp_DD_Date");
                hdn_StatusID = (HiddenField)dg_DDS.Items[i].FindControl("hdn_Status_Id");
                hdn_Status = (HiddenField)dg_DDS.Items[i].FindControl("hdn_Status");
                txt_Local_Tempo_Freight = (TextBox)dg_DDS.Items[i].FindControl("txt_Local_Tempo_Freight");
                hdn_Local_Tempo_Freight = (HiddenField)dg_DDS.Items[i].FindControl("hdn_Local_Tempo_Freight");

                txt_Bonus = (TextBox)dg_DDS.Items[i].FindControl("txt_Bonus");
                hdn_Bonus = (HiddenField)dg_DDS.Items[i].FindControl("hdn_Bonus");

                txt_AddTempoFrt = (TextBox)dg_DDS.Items[i].FindControl("txt_AddTempoFrt");
                hdn_AddTempoFrt = (HiddenField)dg_DDS.Items[i].FindControl("hdn_AddTempoFrt");

                if (chk.Checked == true)
                { 
                    objDT.Rows[i]["Actual_Status_ID"] = Util.String2Int(hdn_StatusID.Value);
                    objDT.Rows[i]["Actual_Status"] = hdn_Status.Value;
                }
                else
                {
                    objDT.Rows[i]["Actual_Status_ID"] = 300;
                    objDT.Rows[i]["Actual_Status"] = "UnDelivered";
                    objDT.Rows[i]["Status_ID"] = 300;
                    objDT.Rows[i]["Status"] = "UnDelivered";

                }
                 
                objDT.Rows[i]["Att"] = chk.Checked; 
                objDT.Rows[i]["UnDelivered_Reason_Id"] = Util.String2Int(ddl_Undelivered_Reason.SelectedValue);
                objDT.Rows[i]["DeliveryAreaID"] = Util.String2Int(ddl_DeliveryLocation.SelectedValue);
                objDT.Rows[i]["Dly_Pay_Mode_Id"] = Util.String2Int(ddl_DeliveryStatus.SelectedValue);
                objDT.Rows[i]["MobileNo"] = txtMobileNo.Text;
                objDT.Rows[i]["Delivery_Time"] = Timepicker1.getTime();
                //objDT.Rows[i]["Delivery_Date"] = dtp_DD_Date.SelectedDate;
                objDT.Rows[i]["Local_Tempo_Freight"] = txt_Local_Tempo_Freight.Text;
                objDT.Rows[i]["Bonus"] = txt_Bonus.Text;
                objDT.Rows[i]["AddTempoFrt"] = txt_AddTempoFrt.Text;

            }
        }

    }



    #endregion
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
            TD_Calender.Visible = false;
            dtp_DDS_Date.Enabled = false;
        }
        if (Mode == "2")
        {
            ddl_DeliveryMode.Enabled = false;
            WucVehicleSearch1.Enable_Disable(false); 
            TD_Calender.Visible = false;
            dtp_DDS_Date.Enabled = false;
            txt_DriverName.Enabled = false;
            lbltxt_Vendor.Enabled = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ddl_Supervisor.DataTextField = "Emp_Name";
        ddl_Supervisor.DataValueField = "Emp_ID";

        SetPostBackValues(); 
        SetStandardCaption();
        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Close, btn_Save_Print));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Close, btn_Save_Print));
        btn_Save_Print.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page,btn_Save_Print,btn_Save_Exit, btn_Save, btn_Close));

        WucVehicleSearch1.Can_Add_Vehicle = false;
        WucVehicleSearch1.Can_View_Vehicle = true;
        WucVehicleSearch1.TransactionDate = dtp_DDS_Date.SelectedDate; 

        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());
        hdn_Mode.Value = Mode;

        if (!IsPostBack)
        {
            Assign_Hidden_Values_For_Reset();  

            ddl_DeliveryMode.Focus();
        }
        objDDCPresenter = new DDCPresenter(this, IsPostBack, VehicleID);
        if (!IsPostBack)
        {
            if (keyID <= 0)
            {
                Next_DDS_Number();

                //Added On 27 Aug 2018
                string Crypt = "";
                int _VehicleId, PendingPDS_Id, _Delivery_Mode_Id;

                Crypt = System.Web.HttpContext.Current.Request.QueryString["PendingPDSId"];

                if (Crypt != null)
                {
                    PendingPDS_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

                    Crypt = System.Web.HttpContext.Current.Request.QueryString["VehicleId"];
                    _VehicleId = ClassLibraryMVP.Util.DecryptToInt(Crypt);

                    Crypt = System.Web.HttpContext.Current.Request.QueryString["Delivery_Mode_Id"];
                    _Delivery_Mode_Id = ClassLibraryMVP.Util.DecryptToInt(Crypt);

                    if (_Delivery_Mode_Id > 0)
                    {
                        DeliveryModeID = _Delivery_Mode_Id;
                        VehicleID = _VehicleId;
                        OnDDLVehicleSelection(sender, e);
                        PDSId = PendingPDS_Id;
                        ddl_PDSNo.SelectedIndex = PDSId;
                        ddl_PDSNo_SelectedIndexChanged(sender, e);
                    }
                }
                // End 27 Aug 2018

            }
            else if (keyID > 0)
            {
                OnDDLVehicleSelection(sender, e);
                if (DeliveryModeID == 4)
                {
                    TextBox txt_Local_Tempo_Freight;
                    txt_Local_Tempo_Freight = (TextBox)dg_DDS.Items[0].FindControl("txt_Local_Tempo_Freight");

                    TextBox txt_Bonus;
                    txt_Bonus = (TextBox)dg_DDS.Items[0].FindControl("txt_Bonus");

                    TextBox txt_AddTempoFrt;
                    txt_AddTempoFrt = (TextBox)dg_DDS.Items[0].FindControl("txt_AddTempoFrt");

                    if (DeliveryModeID == 4)
                    {
                        txt_Local_Tempo_Freight.Enabled = false;
                        txt_Bonus.Enabled = false;
                        txt_AddTempoFrt.Enabled = false;
                    }
                    else
                    {
                        txt_Local_Tempo_Freight.Enabled = true;
                        txt_Bonus.Enabled = true;
                        txt_AddTempoFrt.Enabled = true;
                    }

                }
               
            } 
            //ddl_PDSNo.DataSource = "";
            //ddl_PDSNo.DataBind();
        }
        Assign_Hidden_Values_To_TextBox();        
    }

  
    private void DeliveryModeChange()
    {
        if (ddl_DeliveryMode.SelectedValue == "2")
        {
            lbl_DriverName.Text = "Driver Name";
            WucVehicleSearch1.Enable_Disable(true);
        }
        if (ddl_DeliveryMode.SelectedValue == "3")
        {
            lbl_DriverName.Text = "Hand Cart No";
            WucVehicleSearch1.Enable_Disable(false);
        }
        if (ddl_DeliveryMode.SelectedValue == "4")
        {
            lbl_DriverName.Text = "Person Name";
            WucVehicleSearch1.Enable_Disable(false); 
        }

        if (ddl_DeliveryMode.SelectedValue != "2")
        { objDDCPresenter.FillPDSValues(); }
        else
        {
            //ddl_PDSNo.DataSource = "";
            //ddl_PDSNo.DataBind(); 
        }
        scm_dds.SetFocus(txt_DriverName); 
    }

    private void SetPostBackValues()
    {
        WucVehicleSearch1.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

    }

    private void OnDDLVehicleSelection(object sender, EventArgs e)
    {
        if (Mode == "2" || Mode == "4")
        {
            if (ddl_DeliveryMode.SelectedValue == "2")
            {
                //VendorName = WucVehicleSearch1.GetVehicleParameter("Vendor_Name");
                VendorID = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("Vendor_ID"));
            }
        }
        else
        {
            if (ddl_DeliveryMode.SelectedValue == "2")
            {
                //VendorName = WucVehicleSearch1.GetVehicleParameter("Vendor_Name");
                VendorID = Util.String2Int(WucVehicleSearch1.GetVehicleParameter("Vendor_ID"));
            }
            if ((VehicleID > 0))
            {
                //objDDCPresenter = new DDCPresenter();
                objDDCPresenter.FillPDSValues();
            }
        }
    }


    #region otherControls
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        errorMessage = "";
        _flag = "SaveAndNew";
        objDDCPresenter.save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        errorMessage = "";
        _flag = "SaveAndExit";
        objDDCPresenter.save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void btn_Save_Print_Click(object sender, EventArgs e)
    {
        errorMessage = "";
        _flag = "SaveAndPrint";
        objDDCPresenter.save();
    }

    protected void ddl_PDSNo_SelectedIndexChanged(object sender, EventArgs e)
    {
    
        Assign_Hidden_Values_For_Reset();
        Assign_Hidden_Values_To_TextBox();
        objDDCPresenter.FillGrid();

        ddl_PDSNo.Items.Remove(new ListItem("Select PDS No", "0"));

        if (dg_DDS.Items.Count > 0)
        {
            //if (keyID <= 0 && ddl_PDSNo.SelectedIndex != -1)
            //{
            //    for (i = 0; i <= dg_DDS.Items.Count - 1; i++)
            //    {
            //        ddl_DeliveryStatus = (DropDownList)dg_DDS.Items[i].FindControl("ddl_DeliveryStatus");
            //        ddl_DeliveryStatus_SelectedIndexChanged(ddl_DeliveryStatus, e);
            //    }
            //}
            TextBox txtMobileNo; 
            txtMobileNo = (TextBox)dg_DDS.Items[0].FindControl("txtMobileNo"); 
            scm_dds.SetFocus(txtMobileNo);

            TextBox txt_Local_Tempo_Freight;
            txt_Local_Tempo_Freight = (TextBox)dg_DDS.Items[0].FindControl("txt_Local_Tempo_Freight");

            TextBox txt_Bonus;
            txt_Bonus = (TextBox)dg_DDS.Items[0].FindControl("txt_Bonus");

            TextBox txt_AddTempoFrt;
            txt_AddTempoFrt = (TextBox)dg_DDS.Items[0].FindControl("txt_AddTempoFrt");

            if (DeliveryModeID == 4)
            {
                txt_Local_Tempo_Freight.Enabled = false;
                txt_Bonus.Enabled = false;
                txt_AddTempoFrt.Enabled = false;
            }
            else
            {
                txt_Local_Tempo_Freight.Enabled = true;
                txt_Bonus.Enabled = true;
                txt_AddTempoFrt.Enabled = true;
            }
            CalculateTotal();
        }

    }

    protected void dtp_DDS_Date_SelectionChanged(object sender, EventArgs e)
    {
        Assign_Hidden_Values_For_Reset();
        Assign_Hidden_Values_To_TextBox();
        objDDCPresenter.FillValues();
        objDDCPresenter.FillGrid();
        hdn_DDS_Date.Value = dtp_DDS_Date.SelectedDate.ToString(); 
    }
    #endregion

 
    protected void dg_DDS_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        LinkButton lbtn_Details;
        LinkButton lbtn_GCNoForPrint;
        HiddenField hdn_GCNoForPrint;
        TextBox txt_GCAmount;
       
        if (e.Item.ItemIndex != -1)
        {
            chk = (CheckBox)e.Item.FindControl("Chk_Attach");
            Timepicker1 = (TimePicker)e.Item.FindControl("TimePicker1");
            dtp_DD_Date = (ComponentArt.Web.UI.Calendar)e.Item.FindControl("dtp_DD_Date");
            ddl_Undelivered_Reason = (DropDownList)e.Item.FindControl("ddl_UnDel_Reason");
            ddl_DeliveryLocation = (DropDownList)e.Item.FindControl("ddl_DeliveryLocation");
            ddl_DeliveryStatus = (DropDownList)e.Item.FindControl("ddl_DeliveryStatus");
            hdn_StatusID = (HiddenField)e.Item.FindControl("hdn_Status_Id");
            txtMobileNo = (TextBox)e.Item.FindControl("txtMobileNo");
            txt_Local_Tempo_Freight = (TextBox)e.Item.FindControl("txt_Local_Tempo_Freight");
            hdn_Local_Tempo_Freight = (HiddenField)e.Item.FindControl("hdn_Local_Tempo_Freight");


            txt_Bonus = (TextBox)e.Item.FindControl("txt_Bonus");
            hdn_Bonus = (HiddenField)e.Item.FindControl("hdn_Bonus");

            txt_AddTempoFrt = (TextBox)e.Item.FindControl("txt_AddTempoFrt");
            hdn_AddTempoFrt = (HiddenField)e.Item.FindControl("hdn_AddTempoFrt");

            lbtn_Details = (LinkButton)e.Item.FindControl("lbtn_details");
            txt_Del_taken_by = (TextBox)e.Item.FindControl("txt_Delivery_TakenBy");

            Timepicker1.setFormat("24");

            ddl_Undelivered_Reason.DataTextField = "Reason";
            ddl_Undelivered_Reason.DataValueField = "Reason_ID";
            ddl_Undelivered_Reason.DataSource = SessionBindDDLUndelReason;
            ddl_Undelivered_Reason.DataBind();
            ddl_Undelivered_Reason.Items.Insert(0, new ListItem("Select One", "0"));

            ddl_DeliveryLocation.DataTextField = "DeliveryAreaName";
            ddl_DeliveryLocation.DataValueField = "DeliveryAreaID";
            ddl_DeliveryLocation.DataSource = SessionBindDlyAreaGrid;
            ddl_DeliveryLocation.DataBind();
            ddl_DeliveryLocation.Items.Insert(0, new ListItem("Select One", "0"));

            ddl_DeliveryStatus.DataTextField = "Dly_Pay_Mode_Name";
            ddl_DeliveryStatus.DataValueField = "Dly_Pay_Mode_Id";
            ddl_DeliveryStatus.DataSource = SessionBindDlyStatus;
            ddl_DeliveryStatus.DataBind();
            ddl_DeliveryStatus.Items.Insert(0, new ListItem("Select", "0"));

            Timepicker1.setTime(SessionBindDDSGrid.Rows[e.Item.ItemIndex]["Delivery_Time"].ToString());
            string ddate = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["Delivery_Date"].ToString();
            dtp_DD_Date.SelectedDate = DateTime.Now;//Convert.ToDateTime(ddate);
            ddl_Undelivered_Reason.SelectedValue = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["UnDelivered_Reason_Id"].ToString();
            ddl_DeliveryLocation.SelectedValue = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["DeliveryAreaID"].ToString();
            txtMobileNo.Text = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["MobileNo"].ToString();
            txt_Local_Tempo_Freight.Text = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["Local_Tempo_Freight"].ToString();
            hdn_Local_Tempo_Freight.Value = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["Local_Tempo_Freight"].ToString();

            txt_Bonus.Text = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["Bonus"].ToString();
            hdn_Bonus.Value = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["Bonus"].ToString();

            txt_AddTempoFrt.Text = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["AddTempoFrt"].ToString();
            hdn_AddTempoFrt.Value = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["AddTempoFrt"].ToString();

            if (Mode == "1")
            { 
                lbl_PDSDate.Text = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["PDS_Date"].ToString();            
                txt_DriverName.Text = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["DiverName"].ToString();
                lbltxt_Vendor.Text = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["VendorName"].ToString();
            }
            //1	C Cash //2 Q Cheque //3 R Credit //4 T Return 

            ds_index = e.Item.ItemIndex;
           
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {


                ddl_DeliveryLocation = (DropDownList)e.Item.FindControl("ddl_DeliveryLocation");
                ddl_DeliveryStatus = (DropDownList)e.Item.FindControl("ddl_DeliveryStatus");
                ddl_DeliveryStatus.SelectedValue = SessionBindDDSGrid.Rows[e.Item.ItemIndex]["Dly_Pay_Mode_Id"].ToString();

                hdn_GC_Id = (HiddenField)e.Item.FindControl("hdn_GC_Id");
                hdn_Article_Id = (HiddenField)e.Item.FindControl("hdn_Article_Id");
                
                txt_GCAmount = (TextBox)e.Item.FindControl("txt_Total_GC_Amount");

                StringBuilder Path = new StringBuilder(Util.GetBaseURL());
                Path.Append("/");
                Path.Append("Operations/Delivery/FrmDDCPaymentDetails.aspx?ds_index=" + Util.EncryptInteger(ds_index) + "&_menuItemid=" + Util.EncryptInteger(Raj.EC.Common.GetMenuItemId()) + "&Mode=" + Mode +"&DDCId=" + keyID); //+ "&hdn_DDS_Date=" + hdn_DDS_Date.Value);
                lbtn_Details.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "'," + ddl_DeliveryStatus.ClientID + "," + hdn_GC_Id.ClientID + "," + hdn_Article_Id.ClientID + "," + hdn_DDS_Date.ClientID + "," + txt_GCAmount.Text + ")");

                ddl_DeliveryLocation.Attributes.Add("onblur", "setFocusonddlArea('" + ddl_DeliveryStatus.ClientID + "')");


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

                    ////TextBox txt_Local_Tempo_Freight;
                    ////txt_Local_Tempo_Freight = (TextBox)dg_DDS.Items[0].FindControl("txt_Local_Tempo_Freight");

                    if (DeliveryModeID == 4)
                    {
                        txt_Local_Tempo_Freight.Enabled = false;
                        txt_Bonus.Enabled = false;
                        txt_AddTempoFrt.Enabled = false;
                    }
                    else
                    {
                        txt_Local_Tempo_Freight.Enabled = true;
                        txt_Bonus.Enabled = true;
                        txt_AddTempoFrt.Enabled = true;
                    }
                
                
                }

                if (txtMobileNo.Text.Length >= 10)
                {
                    txtMobileNo.Enabled = false;
                }
                if (Convert.ToInt32(ddl_DeliveryLocation.SelectedValue) > 0)
                {
                    ddl_DeliveryLocation.Enabled = false;
                }
            }

            if (chk.Checked == true)
            {
                Chk_Attach_CheckedChanged(chk, e);
            }
            else
            {
                txt_Del_taken_by.Text = "";
                txt_Del_taken_by.Enabled = false;
                //lbtn_Details.Enabled = false;
                e.Item.Cells[12].Enabled = false;
            }

            if (ddl_DeliveryStatus.SelectedItem.Value == "4")
            {
                ddl_DeliveryStatus_SelectedIndexChanged(ddl_DeliveryStatus, e);
            }
            else
            {
                txt_Del_taken_by.Text = "";
                txt_Del_taken_by.Enabled = false;
                //lbtn_Details.Enabled = false;
                e.Item.Cells[12].Enabled = false;
            }
        }
        
    }

    protected void ddl_DeliveryLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        CalculateTotal();

        TextBox txt_lol_Tmp_frgt, txt_Bonus, txt_AddTempoFrt;
        HiddenField hdn_GCNoForPrint, hdn_lol_Tmp_frgt, hdn_Bonus, hdn_AddTempoFrt;
        DropDownList ddl_DeliveryStatus;
        ddl = (DropDownList)sender; 
        DataGridItem _item = (DataGridItem)ddl.Parent.Parent;

        txt_lol_Tmp_frgt = (TextBox)_item.FindControl("txt_Local_Tempo_Freight");
        hdn_lol_Tmp_frgt = (HiddenField)_item.FindControl("hdn_Local_Tempo_Freight");

        txt_Bonus = (TextBox)_item.FindControl("txt_Bonus");
        hdn_Bonus = (HiddenField)_item.FindControl("hdn_Bonus");

        txt_AddTempoFrt = (TextBox)_item.FindControl("txt_AddTempoFrt");
        hdn_AddTempoFrt = (HiddenField)_item.FindControl("hdn_AddTempoFrt");

        hdn_GCNoForPrint = (HiddenField)_item.FindControl("hdn_GCNoForPrint");
        ddl_DeliveryStatus = (DropDownList)_item.FindControl("ddl_DeliveryStatus");

        if (Convert.ToInt32(ddl.SelectedValue) > 0 && Convert.ToInt32(ddl_DeliveryStatus.SelectedValue) != 4 && Convert.ToInt32(ddl_DeliveryStatus.SelectedValue) != 8)
        { 
            SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@GC_ID", SqlDbType.Int, 0, Convert.ToInt32(hdn_GCNoForPrint.Value)),
            objDAL.MakeInParams("@DeliveryAreaID", SqlDbType.Int, 0, Convert.ToInt32(ddl.SelectedValue)),
            objDAL.MakeInParams("@ApplicableFrom", SqlDbType.DateTime, 0, DDSDate),
            objDAL.MakeInParams("@PDSID", SqlDbType.Int, 0, PDSId)};
            objDAL.RunProc("dbo.EC_Opr_Dly_Get_DeliveryChargeID_LocalTempoFreight", objSqlParam, ref objDS);

            if (objDS.Tables[0].Rows.Count > 0)
            {
                txt_lol_Tmp_frgt.Text = objDS.Tables[0].Rows[0]["Local_Tempo_Frt"].ToString();
                hdn_lol_Tmp_frgt.Value = objDS.Tables[0].Rows[0]["Local_Tempo_Frt"].ToString();

                txt_Bonus.Text = objDS.Tables[0].Rows[0]["Bonus"].ToString();
                hdn_Bonus.Value = objDS.Tables[0].Rows[0]["Bonus"].ToString();
            }
            else
            {
                txt_lol_Tmp_frgt.Text = "0";
                hdn_lol_Tmp_frgt.Value = "0";

                txt_Bonus.Text = "0";
                hdn_Bonus.Value = "0";
            }
        } 
        else
        {
            txt_lol_Tmp_frgt.Text = "0";
            hdn_lol_Tmp_frgt.Value = "0";

            txt_Bonus.Text = "0";
            hdn_Bonus.Value = "0";
        }

        scm_dds.SetFocus(ddl_DeliveryStatus);

    }
    
    protected void ddl_DeliveryStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox txt_lol_Tmp_frgt, txt_del_art, txt_del_wt, txt_Total_GC_Amount, lbl_txt_ChequeAmt, lbl_txt_CreditAmt, txt_Bonus, txt_AddTempoFrt;
        CheckBox chk;
        LinkButton lbtn_details;
        HiddenField hdn_GCNoForPrint, hdn_GCId, hdn_ActualBonus;
        DropDownList ddl_DlyStatus;
        decimal sum_txt_Total_GC_AmountC = 0;
        decimal sum_txt_Total_GC_AmountQ = 0;
        decimal sum_txt_Total_GC_AmountR = 0;
        decimal sum_txt_Total_GC_AmountM = 0;
        decimal sum_txt_Total_GC_AmountS = 0;
        decimal sum_txt_Total_GC_AmountP = 0;

        //1	C Cash //2 Q Cheque //3 R Credit //4 T Return 
        ddl = (DropDownList)sender;
        //txt_Total_GC_Amount = (DropDownList)dg_DDS.Items[i].FindControl("txt_Total_GC_Amount");
        DataGridItem _item = (DataGridItem)ddl.Parent.Parent;

        chk = (CheckBox)_item.FindControl("Chk_Attach");
        lbtn_details = (LinkButton)_item.FindControl("lbtn_details");
        txt_Actual_Status = (TextBox)_item.FindControl("txt_Actual_Status");
        hdn_StatusID = (HiddenField)_item.FindControl("hdn_Status_Id");
        hdn_Status = (HiddenField)_item.FindControl("hdn_Status");
        hdn_Actual_Status_Id = (HiddenField)_item.FindControl("hdn_Actual_Status_Id");
        txt_Del_taken_by = (TextBox)_item.FindControl("txt_Delivery_TakenBy");
        ddl_Undelivered_Reason = (DropDownList)_item.FindControl("ddl_UnDel_Reason");
        ddl_DeliveryLocation = (DropDownList)_item.FindControl("ddl_DeliveryLocation");
        ddl_DeliveryStatus = (DropDownList)_item.FindControl("ddl_DeliveryStatus");
        txt_del_art = (TextBox)_item.FindControl("txt_Delivery_Art");
        txt_del_wt = (TextBox)_item.FindControl("txt_Delivery_Wt");
        txt_lol_Tmp_frgt = (TextBox)_item.FindControl("txt_Local_Tempo_Freight");
        hdn_Local_Tempo_Freight = (HiddenField)_item.FindControl("hdn_Local_Tempo_Freight");

        txt_Bonus = (TextBox)_item.FindControl("txt_Bonus");
        hdn_Bonus = (HiddenField)_item.FindControl("hdn_Bonus");

        txt_AddTempoFrt = (TextBox)_item.FindControl("txt_AddTempoFrt");
        hdn_AddTempoFrt = (HiddenField)_item.FindControl("hdn_AddTempoFrt");

        txt_Total_GC_Amount = (TextBox)_item.FindControl("txt_Total_GC_Amount");
        lbl_txt_ChequeAmt = (TextBox)this.FindControl("lbl_txt_ChequeAmt");
        lbl_txt_CreditAmt = (TextBox)this.FindControl("lbl_txt_CreditAmt");
        hdn_ChequeAmt = (TextBox)this.FindControl("hdn_ChequeAmt");
        hdn_CreditAmt = (TextBox)this.FindControl("hdn_CreditAmt");
        hdn_GCNoForPrint = (HiddenField)_item.FindControl("hdn_GCNoForPrint");
        hdn_GCId = (HiddenField)_item.FindControl("hdn_GC_Id");

        lbl_txt_MobilePay = (TextBox)this.FindControl("lbl_txt_MobilePay");
        hdn_MobilePay = (TextBox)this.FindControl("hdn_MobilePay");
        lbl_txt_SwipeCard = (TextBox)this.FindControl("lbl_txt_SwipeCard");
        hdn_SwipeCard = (TextBox)this.FindControl("hdn_SwipeCard");

        txt_PendingFreight = (TextBox)this.FindControl("txt_PendingFreight");
        hdn_PendingFreight = (HiddenField)this.FindControl("hdn_PendingFreight");

        DataTable DT = SessionBindDDSGrid;
        DT.TableName = "pdsgcids";
        DataTable DT1 = DT.Copy();

        for (i = 0; i <= dg_DDS.Items.Count - 1; i++)
        {
            ddl_DlyStatus = (DropDownList)dg_DDS.Items[i].FindControl("ddl_DeliveryStatus");
            DT1.Rows[i]["Att"] = Convert.ToInt32(ddl_DlyStatus.SelectedValue) != 4 ? true : false;

            ddl_DlyStatus = (DropDownList)dg_DDS.Items[i].FindControl("ddl_DeliveryStatus");
            DT1.Rows[i]["Dly_Pay_Mode_Id"] = Convert.ToInt32(ddl_DlyStatus.SelectedValue) == 8 ? "8" : DT1.Rows[i]["Dly_Pay_Mode_ID"].ToString();

        }

        DataSet ds = new DataSet();
        ds.Tables.Add(DT1);

        string gcsXML = ds.GetXml().ToLower();

        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@gcsXML", SqlDbType.Xml, 0, gcsXML),
        objDAL.MakeInParams("@PDSId", SqlDbType.Int, 0, PDSId),
        objDAL.MakeInParams("@VehicleID", SqlDbType.Int, 0, null)};
        objDAL.RunProc("[dbo].[ec_opr_pds_tempo_freight_calculator]", objSqlParam, ref objDS);
        
        for (i = 0; i <= dg_DDS.Items.Count - 1; i++)
        {
            ddl_DlyStatus = (DropDownList)dg_DDS.Items[i].FindControl("ddl_DeliveryStatus");
            hdn_GCId = (HiddenField)dg_DDS.Items[i].FindControl("hdn_GC_Id");
            txt_lol_Tmp_frgt = (TextBox)dg_DDS.Items[i].FindControl("txt_Local_Tempo_Freight");
            hdn_Local_Tempo_Freight = (HiddenField)dg_DDS.Items[i].FindControl("hdn_Local_Tempo_Freight");
            txt_Bonus = (TextBox)dg_DDS.Items[i].FindControl("txt_Bonus");
            hdn_Bonus = (HiddenField)dg_DDS.Items[i].FindControl("hdn_Bonus");
            hdn_ActualBonus = (HiddenField)dg_DDS.Items[i].FindControl("hdn_ActualBonus");
            int gcid = Convert.ToInt32(hdn_GCId.Value);

            DataView dv = new DataView(objDS.Tables[0]);
            dv.RowFilter = "LRID=" + gcid;
            dv.RowStateFilter = DataViewRowState.CurrentRows;
            DataTable dd = dv.ToTable();
            if (dd.Rows.Count > 0)
            {
                txt_lol_Tmp_frgt.Text = dd.Rows[0]["TempoFrt"].ToString();
                hdn_Local_Tempo_Freight.Value = dd.Rows[0]["TempoFrt"].ToString();

                txt_Bonus.Text = hdn_ActualBonus.Value;
                hdn_Bonus.Value = hdn_ActualBonus.Value;
            }
            else
            {
                txt_lol_Tmp_frgt.Text = "0";
                hdn_Local_Tempo_Freight.Value = "0";

                txt_Bonus.Text = "0";
                hdn_Bonus.Value = "0";
            }
        }

        if (Convert.ToInt32(ddl.SelectedValue) != 4 && Convert.ToInt32(ddl.SelectedValue) != 8)
        {
            if (Convert.ToInt32(ddl_DeliveryMode.SelectedValue) == 4)
            {
                //txt_lol_Tmp_frgt.Text = "0";
                //hdn_Local_Tempo_Freight.Value = "0";

                //txt_Bonus.Text = "0";
                //hdn_Bonus.Value = "0";

                txt_AddTempoFrt.Text = "0";
                hdn_AddTempoFrt.Value = "0";

                txt_AddTempoFrt.Enabled = false;
            }
        } 
        else
        {
            //txt_lol_Tmp_frgt.Text = "0";
            //hdn_Local_Tempo_Freight.Value = "0";

            //txt_Bonus.Text = "0";
            //hdn_Bonus.Value = "0";

            txt_AddTempoFrt.Text = "0";
            hdn_AddTempoFrt.Value = "0";
            //return;
        }


         CalculateTotal();
        
        if (ddl.SelectedItem.Value != "4" )
        {
            txt_Actual_Status.Text = hdn_Status.Value;
            hdn_Actual_Status_Id.Value = hdn_StatusID.Value;
            ddl_Undelivered_Reason.SelectedValue = "0";
            ddl_Undelivered_Reason.Enabled = false;
            txt_Del_taken_by.Enabled = true;
            //lbtn_DelDetails.Enabled = true;
            _item.Cells[12].Enabled = true;
            hdn_totalDelArt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelArt.Value) + Util.String2Decimal(txt_del_art.Text));
            //hdn_Local_TotalTempo_Freight.Value = Util.Decimal2String(Util.String2Decimal(hdn_Local_TotalTempo_Freight.Value) + Util.String2Decimal(txt_lol_Tmp_frgt.Text));
            hdn_totalDelWt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelWt.Value) + Util.String2Decimal(txt_del_wt.Text));
            hdn_ChequeAmt.Text =  lbl_txt_ChequeAmt.Text;
            hdn_CreditAmt.Text =  lbl_txt_CreditAmt.Text;
            hdn_TotalCash.Text = lbl_txt_TotalCash.Text;
            hdn_Total_GC.Value = lbl_Total_GC.Text;
            hdn_CashReceived.Value = txt_CashReceived.Text;
            hdn_MobilePay.Text = lbl_txt_MobilePay.Text;
            hdn_SwipeCard.Text = lbl_txt_SwipeCard.Text;
            hdn_PendingFreight.Value = txt_PendingFreight.Text; 

            chk.Checked = true;
            if (chk.Checked = true)
            {
                txt_Actual_Status.Text = "Delivered";
                hdn_Actual_Status_Id.Value = "200";

                hdn_Status.Value = "Delivered";
                hdn_StatusID.Value = "200";
            }
            if (ddl.SelectedItem.Value == "2" || ddl.SelectedItem.Value == "3")
            {
                //lbtn_details.Focus();
                scm_dds.SetFocus(lbtn_details);
            }
            if (ddl.SelectedItem.Value == "0")
            {
                txt_lol_Tmp_frgt.Text = "0";
                hdn_Local_Tempo_Freight.Value = "0";

                txt_Bonus.Text = "0";
                hdn_Bonus.Value = "0";

                txt_AddTempoFrt.Text = "0";
                hdn_AddTempoFrt.Value = "0";
            }
            if (ddl.SelectedItem.Value == "1")
            {
                scm_dds.SetFocus(txt_AddTempoFrt);
            }
        } 
        else
        {
            txt_Actual_Status.Text = "UnDelivered";
            hdn_Actual_Status_Id.Value = "300";

            hdn_Status.Value = "UnDelivered";
            hdn_StatusID.Value = "300";

            ddl_Undelivered_Reason.Enabled = true;
            txt_Del_taken_by.Enabled = false;
            txt_Del_taken_by.Text = "";
            //lbtn_DelDetails.Enabled = false;
            _item.Cells[12].Enabled = false;
            hdn_totalDelArt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelArt.Value) - Util.String2Decimal(txt_del_art.Text));
            //hdn_Local_TotalTempo_Freight.Value = Util.Decimal2String(Util.String2Decimal(hdn_Local_TotalTempo_Freight.Value) - Util.String2Decimal(txt_lol_Tmp_frgt.Text));
            hdn_totalDelWt.Value = Util.Decimal2String(Util.String2Decimal(hdn_totalDelWt.Value) - Util.String2Decimal(txt_del_wt.Text));
            hdn_ChequeAmt.Text =  lbl_txt_ChequeAmt.Text;
            hdn_CreditAmt.Text = lbl_txt_CreditAmt.Text;
            hdn_TotalCash.Text = lbl_txt_TotalCash.Text;
            hdn_Total_GC.Value = lbl_Total_GC.Text;
            hdn_CashReceived.Value = txt_CashReceived.Text;
            hdn_MobilePay.Text = lbl_txt_MobilePay.Text;
            hdn_SwipeCard.Text = lbl_txt_SwipeCard.Text;
            hdn_PendingFreight.Value = txt_PendingFreight.Text;
            chk.Checked = false;
            if (ddl.SelectedItem.Value == "4" || ddl.SelectedItem.Value == "8")
            {
                //txt_lol_Tmp_frgt.Text = "0";
                //hdn_Local_Tempo_Freight.Value = "0";

                txt_Bonus.Text = "0";
                hdn_Bonus.Value = "0";

                txt_AddTempoFrt.Text = "0";
                hdn_AddTempoFrt.Value = "0";

                scm_dds.SetFocus(lbtn_details);
            }
        }

        //Assign_Hidden_Values_To_TextBox();

    }
    protected void txt_Local_Tempo_Freight_TextChanged(object sender, EventArgs e)
    {

        CalculateTotal();

        TextBox txt_lol_Tmp_frgt, txt_Bonus, txt_AddTempoFrt; 
        DropDownList ddl_DeliveryStatus;
        HiddenField hdn_IsConsignorNew, hdn_IsConsigneeNew, hdn_ActualBonus;
        int Old_txt_lol_Tmp_frgt;
        
        txt = (TextBox)sender;
        DataGridItem _item = (DataGridItem)txt.Parent.Parent;
        txt_lol_Tmp_frgt = (TextBox)_item.FindControl("txt_Local_Tempo_Freight");
        hdn_Local_Tempo_Freight = (HiddenField)_item.FindControl("hdn_Local_Tempo_Freight");
        hdn_IsConsignorNew = (HiddenField)_item.FindControl("hdnIsConsignorNew");
        hdn_IsConsigneeNew = (HiddenField)_item.FindControl("hdnIsConsigneeNew");
        hdn_ActualBonus = (HiddenField)_item.FindControl("hdn_ActualBonus");
        txt_Bonus = (TextBox)_item.FindControl("txt_Bonus");
        hdn_Bonus = (HiddenField)_item.FindControl("hdn_Bonus");

        txt_AddTempoFrt = (TextBox)_item.FindControl("txt_AddTempoFrt");
        hdn_AddTempoFrt = (HiddenField)_item.FindControl("hdn_AddTempoFrt");

        //ddl_DeliveryStatus = (DropDownList)_item.FindControl("ddl_DeliveryStatus");
        Total_Local_Tempo_Frt = 0;
        Total_Bonus = 0;
        Total_AddTempoFrt = 0;
        int CalculateBonus = 0;
        Old_txt_lol_Tmp_frgt = Convert.ToInt32(hdn_Local_Tempo_Freight.Value);
        
        if (Convert.ToInt32(txt_lol_Tmp_frgt.Text)  >= 0)
        {
            if (Old_txt_lol_Tmp_frgt >= 0)
            {
                if (Convert.ToInt32(txt_lol_Tmp_frgt.Text) >= Old_txt_lol_Tmp_frgt)
                {
                    txt_lol_Tmp_frgt.Text = Old_txt_lol_Tmp_frgt.ToString();
                    //return;
                }
            }

            if (Convert.ToInt32(hdn_ActualBonus.Value) > 0)
            {
                if (hdn_IsConsigneeNew.Value == "True")
                    CalculateBonus = Convert.ToInt32(txt_lol_Tmp_frgt.Text) * 50 / 100;
                else
                    CalculateBonus = Convert.ToInt32(txt_lol_Tmp_frgt.Text) * 25 / 100;
            }

            txt_Bonus.Text = CalculateBonus.ToString();
            hdn_Bonus.Value = txt_Bonus.Text;
        }

        if (Util.String2Int(txt_lol_Tmp_frgt.Text) > 0)
        {
            txt_AddTempoFrt.Text = "0";
            hdn_AddTempoFrt.Value = "0";
        }
        else if (Old_txt_lol_Tmp_frgt > 0 && Convert.ToInt32(txt_AddTempoFrt.Text) >= Old_txt_lol_Tmp_frgt)
        {
            txt_AddTempoFrt.Text = Old_txt_lol_Tmp_frgt.ToString();
            hdn_AddTempoFrt.Value = Old_txt_lol_Tmp_frgt.ToString();
        }

        objDT = SessionBindDDSGrid;
        //if (ddl_DeliveryStatus.SelectedItem.Value == "4" || ddl_DeliveryStatus.SelectedItem.Value == "0")
        //{
        //    txt_lol_Tmp_frgt.Text = "0";
        //    return;
        //}
        if (dg_DDS.Items.Count > 0)
        {
            for (i = 0; i <= dg_DDS.Items.Count - 1; i++)
            {
                txt_Total_Local_Tempo_Freight = (TextBox)dg_DDS.Items[i].FindControl("txt_Local_Tempo_Freight");

                txt_Total_Bonus = (TextBox)dg_DDS.Items[i].FindControl("txt_Bonus");
                txt_Total_AddTempoFrt = (TextBox)dg_DDS.Items[i].FindControl("txt_AddTempoFrt");

                dtp_DD_Date = (ComponentArt.Web.UI.Calendar)dg_DDS.Items[i].FindControl("dtp_DD_Date");

                ddl_DeliveryStatus = (DropDownList)dg_DDS.Items[i].FindControl("ddl_DeliveryStatus");
                if (ddl_DeliveryStatus.SelectedItem.Value != "0" && ddl_DeliveryStatus.SelectedItem.Value != "4" && ddl_DeliveryStatus.SelectedItem.Value != "8")
                {
                    Total_Local_Tempo_Frt = Total_Local_Tempo_Frt + Convert.ToDecimal(txt_Total_Local_Tempo_Freight.Text);
                    Total_Bonus = Total_Bonus + Convert.ToDecimal(txt_Total_Bonus.Text);
                    Total_AddTempoFrt = Total_AddTempoFrt + Convert.ToDecimal(txt_Total_AddTempoFrt.Text);
                }
                if (ddl_DeliveryStatus.SelectedItem.Value == "4" || ddl_DeliveryStatus.SelectedItem.Value == "0" || ddl_DeliveryStatus.SelectedItem.Value == "8")
                {
                    txt_Total_Local_Tempo_Freight.Text = "0";
                    txt_Total_Bonus.Text = "0";
                    txt_Total_AddTempoFrt.Text = "0";
                }
            }

            //dtp_DD_Date.SelectedDate = DateTime.Now;
            lbl_Local_TotalTempo_Freight.Text = Convert.ToString(Total_Local_Tempo_Frt);
            hdn_Local_TotalTempo_Freight.Value = Convert.ToString(Total_Local_Tempo_Frt);

            lbl_TotalBonus.Text = Convert.ToString(Total_Bonus);
            hdn_TotalBonus.Value = Convert.ToString(Total_Bonus);

            lbl_TotalAddTempoFrt.Text = Convert.ToString(Total_AddTempoFrt);
            hdn_TotalAddTempoFrt.Value = Convert.ToString(Total_AddTempoFrt);
        }

        if (ddl_DeliveryMode.SelectedItem.Value == "4" || ddl_DeliveryMode.SelectedItem.Value == "8")
        {
            txt_lol_Tmp_frgt.Text = "0";
            hdn_Local_Tempo_Freight.Value = "0";

            txt_Bonus.Text = "0";
            hdn_Bonus.Value = "0";

            txt_AddTempoFrt.Text = "0";
            hdn_AddTempoFrt.Value = "0";
        }
    }


    protected void txt_Local_Tempo_Bonus_TextChanged(object sender, EventArgs e)
    {

        CalculateTotal();

        TextBox txt_lol_Tmp_frgt, txt_Bonus, txt_AddTempoFrt;
        DropDownList ddl_DeliveryStatus;

        int Old_txt_lol_Tmp_frgt;
        int Old_txt_lol_Tmp_bonus;

        txt = (TextBox)sender;
        DataGridItem _item = (DataGridItem)txt.Parent.Parent;
        txt_lol_Tmp_frgt = (TextBox)_item.FindControl("txt_Local_Tempo_Freight");
        hdn_Local_Tempo_Freight = (HiddenField)_item.FindControl("hdn_Local_Tempo_Freight");

        txt_Bonus = (TextBox)_item.FindControl("txt_Bonus");
        hdn_Bonus = (HiddenField)_item.FindControl("hdn_Bonus");

        txt_AddTempoFrt = (TextBox)_item.FindControl("txt_AddTempoFrt");
        hdn_AddTempoFrt = (HiddenField)_item.FindControl("hdn_AddTempoFrt");

        //ddl_DeliveryStatus = (DropDownList)_item.FindControl("ddl_DeliveryStatus");
        Total_Local_Tempo_Frt = 0;
        Total_Bonus = 0;
        Total_AddTempoFrt = 0;
        Old_txt_lol_Tmp_frgt = Convert.ToInt32(hdn_Local_Tempo_Freight.Value);

        Old_txt_lol_Tmp_bonus = Convert.ToInt32(hdn_Bonus.Value);

        if (Convert.ToInt32(txt_Bonus.Text) > 0)
        {
            //if (Old_txt_lol_Tmp_bonus > 0)
            //{
                if (Convert.ToInt32(txt_Bonus.Text) > Old_txt_lol_Tmp_bonus)
                {
                    txt_Bonus.Text = Old_txt_lol_Tmp_bonus.ToString();
                    //return;
                }
            //}
        }

        objDT = SessionBindDDSGrid;
        //if (ddl_DeliveryStatus.SelectedItem.Value == "4" || ddl_DeliveryStatus.SelectedItem.Value == "0")
        //{
        //    txt_lol_Tmp_frgt.Text = "0";
        //    return;
        //}
        if (dg_DDS.Items.Count > 0)
        {
            for (i = 0; i <= dg_DDS.Items.Count - 1; i++)
            {
                txt_Total_Local_Tempo_Freight = (TextBox)dg_DDS.Items[i].FindControl("txt_Local_Tempo_Freight");

                txt_Total_Bonus = (TextBox)dg_DDS.Items[i].FindControl("txt_Bonus");
                txt_Total_AddTempoFrt = (TextBox)dg_DDS.Items[i].FindControl("txt_AddTempoFrt");

                ddl_DeliveryStatus = (DropDownList)dg_DDS.Items[i].FindControl("ddl_DeliveryStatus");
                if (ddl_DeliveryStatus.SelectedItem.Value != "0" && ddl_DeliveryStatus.SelectedItem.Value != "4" && ddl_DeliveryStatus.SelectedItem.Value != "8")
                {
                    Total_Local_Tempo_Frt = Total_Local_Tempo_Frt + Convert.ToDecimal(txt_Total_Local_Tempo_Freight.Text);
                    Total_Bonus = Total_Bonus + Convert.ToDecimal(txt_Total_Bonus.Text);
                    Total_AddTempoFrt = Total_AddTempoFrt + Convert.ToDecimal(txt_Total_AddTempoFrt.Text);
                }
                if (ddl_DeliveryStatus.SelectedItem.Value == "4" || ddl_DeliveryStatus.SelectedItem.Value == "8"  || ddl_DeliveryStatus.SelectedItem.Value == "0")
                {
                    txt_Total_Local_Tempo_Freight.Text = "0";
                    txt_Total_Bonus.Text = "0";
                    txt_Total_AddTempoFrt.Text = "0";
                }
            }
            lbl_Local_TotalTempo_Freight.Text = Convert.ToString(Total_Local_Tempo_Frt);
            hdn_Local_TotalTempo_Freight.Value = Convert.ToString(Total_Local_Tempo_Frt);

            lbl_TotalBonus.Text = Convert.ToString(Total_Bonus);
            hdn_TotalBonus.Value = Convert.ToString(Total_Bonus);

            lbl_TotalAddTempoFrt.Text = Convert.ToString(Total_AddTempoFrt);
            hdn_TotalAddTempoFrt.Value = Convert.ToString(Total_AddTempoFrt);
        }

        if (ddl_DeliveryMode.SelectedItem.Value == "4" || ddl_DeliveryMode.SelectedItem.Value == "8")
        {
            txt_lol_Tmp_frgt.Text = "0";
            hdn_Local_Tempo_Freight.Value = "0";

            txt_Bonus.Text = "0";
            hdn_Bonus.Value = "0";

            txt_AddTempoFrt.Text = "0";
            hdn_AddTempoFrt.Value = "0";
        }
    }

    private void CalculateTotal()
    {

        Total_GC_AmountC = 0;
        Total_GC_AmountQ = 0;
        Total_GC_AmountR = 0;
        Total_GC_AmountM = 0;
        Total_GC_AmountS = 0;
        Total_GC_AmountP = 0;
        Total_Delivery_Wt = 0;
        Total_Delivery_Art = 0; // Assign Zero to Total variable to use to sum in grid
        Total_Local_Tempo_Frt = 0;
        Total_No_Of_GC = 0;
        hdn_Total_CouponBalance.Value = "0";
        int i;

        int Total_CouponBalance;
        Total_CouponBalance = 0;

        TextBox txt_Delivery_Art;// variable for Textbox in grid
        //txt_CashReceived = (TextBox)this.FindControl("txt_CashReceived");
        objDT = SessionBindDDSGrid;

        if (dg_DDS.Items.Count > 0)
        {
            for (i = 0; i <= dg_DDS.Items.Count - 1; i++)
            {
                txt_Payment_Type = (TextBox)dg_DDS.Items[i].FindControl("txt_Payment_Type");
                ddlDeliveryStatus = (DropDownList)dg_DDS.Items[i].FindControl("ddl_DeliveryStatus");
                txt_Total_GC_AmountC = (TextBox)dg_DDS.Items[i].FindControl("txt_Total_GC_Amount");
                txt_Total_Delivery_Wt = (TextBox)dg_DDS.Items[i].FindControl("txt_Delivery_Wt");
                txt_Total_Local_Tempo_Freight = (TextBox)dg_DDS.Items[i].FindControl("txt_Local_Tempo_Freight");

                txt_Total_Bonus = (TextBox)dg_DDS.Items[i].FindControl("txt_Bonus");
                txt_Total_AddTempoFrt = (TextBox)dg_DDS.Items[i].FindControl("txt_AddTempoFrt");

                //txt_Total_Delivery_Art = (TextBox)dg_DDS.Items[i].FindControl("txt_Delivery_Art");
                txt_Delivery_Art = (TextBox)dg_DDS.Items[i].FindControl("txt_Delivery_Art");

                txt_Actual_Status = (TextBox)dg_DDS.Items[i].FindControl("txt_Actual_Status");
                hdn_Actual_Status_Id = (HiddenField)dg_DDS.Items[i].FindControl("hdn_Actual_Status_Id");
                ddl_Undelivered_Reason = (DropDownList)dg_DDS.Items[i].FindControl("ddl_UnDel_Reason");
                txt_Del_taken_by = (TextBox)dg_DDS.Items[i].FindControl("txt_Delivery_TakenBy");

                    if (ddlDeliveryStatus.SelectedValue == "1")
                    {
                        Total_GC_AmountC = Total_GC_AmountC + Convert.ToDecimal(txt_Total_GC_AmountC.Text);
                        objDT.Rows[i]["Client_ID"] = 0;
                        objDT.Rows[i]["Remark"] = "";
                        objDT.Rows[i]["ChequeNo"] = "";
                        objDT.Rows[i]["BankName"] = ""; 
                        objDT.Rows[i]["PartyName"] = "";
                        objDT.Rows[i]["BillingLocationId"] = 0;
                        objDT.Rows[i]["PartyfromBranchLocation"] = "";
                        objDT.Rows[i]["ReasonForNonDly"] = 0;
                    }
                    else if (ddlDeliveryStatus.SelectedValue == "2")
                    {
                        Total_GC_AmountQ = Total_GC_AmountQ + Convert.ToDecimal(txt_Total_GC_AmountC.Text);

                        objDT.Rows[i]["Client_ID"] = 0;  
                        objDT.Rows[i]["PartyName"] = "";
                        objDT.Rows[i]["BillingLocationId"] = 0;
                        objDT.Rows[i]["PartyfromBranchLocation"] = "";
                        objDT.Rows[i]["ReasonForNonDly"] = 0;
                        objDT.Rows[i]["Remark"] = "";  

                    }
                    else if (ddlDeliveryStatus.SelectedValue == "3")
                    {
                        Total_GC_AmountR = Total_GC_AmountR + Convert.ToDecimal(txt_Total_GC_AmountC.Text);
                        objDT.Rows[i]["ChequeNo"] = "";
                        objDT.Rows[i]["BankName"] = "";  
                        objDT.Rows[i]["ReasonForNonDly"] = 0;
                        objDT.Rows[i]["Remark"] = "";   
                    }
                    else if (ddlDeliveryStatus.SelectedValue == "4")
                    {
                        txt_Actual_Status.Text = "UnDelivered";
                        hdn_Actual_Status_Id.Value = "300";
                        ddl_Undelivered_Reason.Enabled = true;
                        txt_Del_taken_by.Enabled = false;
                        txt_Del_taken_by.Text = "";

                        objDT.Rows[i]["ChequeNo"] = "";
                        objDT.Rows[i]["BankName"] = "";  
                        objDT.Rows[i]["Client_ID"] = 0;
                        objDT.Rows[i]["PartyName"] = "";
                        objDT.Rows[i]["BillingLocationId"] = 0;
                        objDT.Rows[i]["PartyfromBranchLocation"] = ""; 
                        objDT.Rows[i]["Remark"] = "";  
                    }

                    else if (ddlDeliveryStatus.SelectedValue == "5")
                    {
                        Total_GC_AmountM = Total_GC_AmountM + Convert.ToDecimal(txt_Total_GC_AmountC.Text);

                        objDT.Rows[i]["Client_ID"] = 0;
                        objDT.Rows[i]["PartyName"] = "";
                        objDT.Rows[i]["BillingLocationId"] = 0;
                        objDT.Rows[i]["PartyfromBranchLocation"] = "";
                        objDT.Rows[i]["ReasonForNonDly"] = 0;
                        objDT.Rows[i]["Remark"] = "";

                    }

                    else if (ddlDeliveryStatus.SelectedValue == "6")
                    {
                        Total_GC_AmountS = Total_GC_AmountS + Convert.ToDecimal(txt_Total_GC_AmountC.Text);

                        objDT.Rows[i]["Client_ID"] = 0;
                        objDT.Rows[i]["PartyName"] = "";
                        objDT.Rows[i]["BillingLocationId"] = 0;
                        objDT.Rows[i]["PartyfromBranchLocation"] = "";
                        objDT.Rows[i]["ReasonForNonDly"] = 0;
                        objDT.Rows[i]["Remark"] = "";

                    }
                    else if (ddlDeliveryStatus.SelectedValue == "7")
                    {
                        Total_GC_AmountP = Total_GC_AmountP + Convert.ToDecimal(txt_Total_GC_AmountC.Text);

                        ddl_Undelivered_Reason.Enabled = true;

                        objDT.Rows[i]["ChequeNo"] = "";
                        objDT.Rows[i]["BankName"] = "";
                        objDT.Rows[i]["Client_ID"] = 0;
                        objDT.Rows[i]["PartyName"] = "";
                        objDT.Rows[i]["BillingLocationId"] = 0;
                        objDT.Rows[i]["PartyfromBranchLocation"] = "";
                        objDT.Rows[i]["Remark"] = "";

                    }

                    else if (ddlDeliveryStatus.SelectedValue == "8")
                    {
                        Total_CouponBalance = Total_CouponBalance + Convert.ToInt32(objDT.Rows[i]["BalanceToBePaid"].ToString());
                    }

                    if (ddlDeliveryStatus.SelectedValue != "0" && ddlDeliveryStatus.SelectedValue != "4" && ddlDeliveryStatus.SelectedValue != "8")
                    {
                        Total_Delivery_Wt = Total_Delivery_Wt + Convert.ToDecimal(txt_Total_Delivery_Wt.Text);
                        Total_Local_Tempo_Frt = Total_Local_Tempo_Frt + Convert.ToDecimal(txt_Total_Local_Tempo_Freight.Text);

                        Total_Bonus = Total_Bonus + Convert.ToDecimal(txt_Total_Bonus.Text);
                        Total_AddTempoFrt = Total_AddTempoFrt + Convert.ToDecimal(txt_Total_AddTempoFrt.Text);

                        Total_Delivery_Art = Total_Delivery_Art + Convert.ToDecimal(txt_Delivery_Art.Text);
                    }
                
            }
            Total_No_Of_GC = dg_DDS.Items.Count;
 
            lbl_Total_GC.Text = Convert.ToString(Total_No_Of_GC);

            hdn_Total_CouponBalance.Value = Convert.ToString(Total_CouponBalance);

            lbl_txt_TotalCash.Text = Convert.ToString(Total_GC_AmountC + Total_CouponBalance);
            hdn_TotalCash.Text = Convert.ToString(Total_GC_AmountC + Total_CouponBalance);
            lbl_txt_ChequeAmt.Text = Convert.ToString(Total_GC_AmountQ);
            lbl_txt_CreditAmt.Text = Convert.ToString(Total_GC_AmountR);
            lbl_txt_MobilePay.Text = Convert.ToString(Total_GC_AmountM);
            lbl_txt_SwipeCard.Text = Convert.ToString(Total_GC_AmountS);
            txt_PendingFreight.Text = Convert.ToString(Total_GC_AmountP);

            lbl_txt_BalancedCash.Text = Convert.ToString(Convert.ToDecimal(lbl_txt_TotalCash.Text) - Convert.ToDecimal(txt_CashReceived.Text));


            lbl_totalDelWt.Text = Convert.ToString(Total_Delivery_Wt);
            lbl_Local_TotalTempo_Freight.Text = Convert.ToString(Total_Local_Tempo_Frt);

            lbl_TotalBonus.Text = Convert.ToString(Total_Bonus);
            lbl_TotalAddTempoFrt.Text = Convert.ToString(Total_AddTempoFrt);

            lbl_totalDelArt.Text = Convert.ToString(Total_Delivery_Art);
            hdn_totalDelArt.Value = Convert.ToString(Total_Delivery_Art);


        }
    }

    protected void Chk_Attach_CheckedChanged(object sender, EventArgs e)
    {
        TextBox txt_del_art, txt_del_wt, txt_lol_Tmp_frgt, txt_Bonus, txt_AddTempoFrt;
        //LinkButton lbtn_DelDetails;

        chk = (CheckBox)sender;
        DataGridItem _item = (DataGridItem)chk.Parent.Parent;

        txt_Actual_Status = (TextBox)_item.FindControl("txt_Actual_Status");
        hdn_StatusID = (HiddenField)_item.FindControl("hdn_Status_Id");
        hdn_Status = (HiddenField)_item.FindControl("hdn_Status");
        hdn_Actual_Status_Id = (HiddenField)_item.FindControl("hdn_Actual_Status_Id");
        txt_Del_taken_by = (TextBox)_item.FindControl("txt_Delivery_TakenBy");
        ddl_Undelivered_Reason = (DropDownList)_item.FindControl("ddl_UnDel_Reason");
        ddl_DeliveryLocation = (DropDownList)_item.FindControl("ddl_DeliveryLocation");
        ddl_DeliveryStatus = (DropDownList)_item.FindControl("ddl_DeliveryStatus");
        txt_del_art = (TextBox)_item.FindControl("txt_Delivery_Art");
        txt_del_wt = (TextBox)_item.FindControl("txt_Delivery_Wt");
        txt_lol_Tmp_frgt = (TextBox)_item.FindControl("txt_Local_Tempo_Freight");

        txt_Bonus = (TextBox)_item.FindControl("txt_Bonus");
        txt_AddTempoFrt = (TextBox)_item.FindControl("txt_AdddTempoFrt");

        Assign_Hidden_Values_To_TextBox();
    }

    public void ClearVariables()
    {
        SessionBindDDSGrid = null;
        SessionBindDDLUndelReason = null;
        SessionBindDlyAreaGrid = null;
        SessionBindDlyStatus = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e) //added Ankit : 21-02-09
    {
        ClearVariables();
    }

    protected void ddl_DeliveryMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        WucVehicleSearch1.VehicleID = 0;
        VendorName = "";
        VendorID = 0;
        ddl_PDSNo.DataSource = "";
        ddl_PDSNo.DataBind();
        dg_DDS.DataSource = "";
        dg_DDS.DataBind();
        Assign_Hidden_Values_For_Reset();
        DeliveryModeChange();
    }

    protected void txt_CashReceived_TextChanged(object sender, EventArgs e)
    {

        //CalculateBalanceAmt(); 
    }
    private void CalculateBalanceAmt()
    {
        
        lbl_txt_BalancedCash.Text = Convert.ToString(Convert.ToDecimal(lbl_txt_TotalCash.Text) - Convert.ToDecimal(txt_CashReceived.Text));
    
    }


    protected void btn_CouponUpdate_Click(object sender, EventArgs e)
    {
        
        CalculateTotal();

    }
}
