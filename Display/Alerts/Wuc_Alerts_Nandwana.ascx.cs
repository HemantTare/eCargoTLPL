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
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Text;

public partial class Display_Alerts_Wuc_Alerts_Nandwana : System.Web.UI.UserControl
{
    DAL objDAL = new DAL();
    private DataSet objDS;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            if (CompanyManager.getCompanyParam().ClientCode.ToLower() == "nandwana")
                GetVehicleAlerts();
            else
                this.Visible = false;
        }

        Img1.Visible = false;
        lnk_btnIncomingTruck.Visible = false;

        imgIncPro.Visible = false;
        lnk_btnIncPro.Visible = false;

        if ((Rights.GetObject().getForm_Rights(36).canAdd()) == false)
        {
            imgClientSearch.Visible = false;
            lnk_btnClientSearch.Visible = false;
        }

        if ((Rights.GetObject().getForm_Rights(79).canRead()) == false)
        {
            imgDebtorsBalance.Visible = false;
            lnk_DebtorsBalance.Visible = false;
        }
    }

    private void GetVehicleAlerts()
    {
        StringBuilder PathF4 = new StringBuilder(Util.GetBaseURL());
        lnk_btnIncomingTruck.Text = "Incoming Vehicles";
        string IsFromDesktop;
        IsFromDesktop = "y";
        lnk_btnIncomingTruck.Attributes.Add("onclick", "return OpenIncomingTrucksAlert('" + IsFromDesktop + "')");

        lnk_btnIncPro.Text = "Incomplete Process";
        lnk_btnIncPro.Attributes.Add("onclick", "return OpenIncompleteProcess()");

        lnk_btnCurrStat.Text = "Current Statistics";
        lnk_btnCurrStat.Attributes.Add("onclick", "return OpenCurrentStatistics()");

        lnk_btnDlyBranchwiseStock.Text = "";
        lnk_btnDlyBranchwiseStock.Attributes.Add("onclick", "return DlyBranchwiseStock()");

        lnk_btnBranchwiseCashBalance.Text = "Branch Cash Balance";
        lnk_btnBranchwiseCashBalance.Attributes.Add("onclick", "return BranchwiseCashBalance()");

        lnk_btneWayBillVerificationH.Text = "eWay Bill Verification";
        lnk_btneWayBillVerificationH.Attributes.Add("onclick", "return eWayBillVerification()");

        lnk_btneWayBillVerification.Text = "eWay Bill Verification";
        lnk_btneWayBillVerification.Attributes.Add("onclick", "return eWayBillVerification()");

        lnk_btnClientBusiness.Text = "Client Last Business";
        lnk_btnClientBusiness.Attributes.Add("onclick", "return ClientBusiness()");

        lnk_btnClientSearch.Text = "Client Search";
        lnk_btnClientSearch.Attributes.Add("onclick", "return Clientsearch()");

        lnk_btnPreveWayBillVerificationH.Text = "Previous Day Unverified eWay Bills";

        int PDSCount, PendingTripVehicleCount, DirectDlyLRCount, DirectDlyLRCountDelivered, InwardPendingDaysCount, InwardBranchCount, eWayBillVerificationPending, PreveWayBillVerificationPending, MyStockLRCount, MyStockQty, DlyStockLRCount, DlyStockQty, PendingDlyAddressCount, UpdatedDlyAddressCount, PendingDlyAddressDlyBrWiseCount, UpdatedDlyAddressDlyBrWiseCount, PendingFrtDlyLRCount, PendingFrtBkgLRCount, BillingDueClientCount, TripExpenseApprovalPending, TripAdvancePending, eWayBillVehicleUpdatePending, PartBUpdatedVehicles, PendingTripSheet, PendingChequeForDeposite, eWayBillVehicleUpdatePendingPDS, PartBUpdatedVehiclesPDS,  eWayBillExpiry;
        string PDSAmount, PendingTripMemoInvoiceCount, DirectDlyLRAmount, DirectDlyLRAmountDelivered, InwardPendingVehicleCount, DlyStockAmount, PendingFrtDlyAmount, PendingFrtBkgAmount;


        string HierarchyCode = UserManager.getUserParam().HierarchyCode;
        int MainId = UserManager.getUserParam().MainId;
        int Branch_ID, Area_ID, Region_ID;
        string Branch_Name;

        Branch_Name = UserManager.getUserParam().MainName;

        Branch_ID = 0;
        Area_ID = 0;
        Region_ID = 0;

        if (HierarchyCode == "BO")
        {
            lbl_DlyBranchwiseStock.Visible = true;
            lnk_btnDlyBranchwiseStock.Visible = true;
            imgDlyBranchwiseStock.Visible = true;

            lnk_btnBranchwiseCashBalance.Visible = false;
            imgBranchwiseCashBalance.Visible = false;
            lnk_btnClientBusiness.Visible = false;
            imgClientBusiness.Visible = false;

            if (MainId == 13 || MainId == 12 || MainId == 72 || MainId == 23)
            {
                lnk_DlyBranchWiseToPayRecovery.Visible = true;
                imgDlyBranchWiseToPayRecovery.Visible = true;
            }
            else
            {
                lnk_DlyBranchWiseToPayRecovery.Visible = false;
                imgDlyBranchWiseToPayRecovery.Visible = false;
            }
        }
        else
        {
            lbl_DlyBranchwiseStock.Visible = false;
            lnk_btnDlyBranchwiseStock.Visible = false;
            imgDlyBranchwiseStock.Visible = false;
        }

        string UserName;

        UserName = UserManager.getUserParam().UserName;

        if (UserName == "BO0022" || UserName == "BO0068" || UserName == "BO0040")
        {
            lnk_btnClientBusiness.Visible = true;
            imgClientBusiness.Visible = true;
        }

        if (HierarchyCode == "AD" || HierarchyCode == "HO")
        {
            Branch_ID = 0;
            Area_ID = 0;
            Region_ID = 0;
        }
        else if (HierarchyCode == "RO")
        {
            Branch_ID = 0;
            Area_ID = 0;
            Region_ID = MainId;
        }
        else if (HierarchyCode == "AO")
        {
            Branch_ID = 0;
            Area_ID = MainId;
            Region_ID = 0;
        }
        else if (HierarchyCode == "BO")
        {
            Branch_ID = MainId;
            Area_ID = 0;
            Region_ID = 0;
        }

        PathF4 = new StringBuilder(Util.GetBaseURL());
        PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmUserDeskDlyBranchToPayRecovery.aspx?BranchID=" + Branch_ID);
        lnk_DlyBranchWiseToPayRecovery.Attributes.Add("onclick", "return DlyBranchToPayRecovery('" + PathF4 + "')");

        PathF4 = new StringBuilder(Util.GetBaseURL());
        PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmUserDeskCreditDebitCustomerBalance.aspx");
        lnk_DebtorsBalance.Attributes.Add("onclick", "return CreditDebitCustomerBalance('" + PathF4 + "')");


        SqlParameter[] objSqlParam = {objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,Branch_ID),
            objDAL.MakeInParams("@Area_ID",SqlDbType.Int,0,Area_ID),
            objDAL.MakeInParams("@Region_ID",SqlDbType.Int,0,Region_ID)};

        objDAL.RunProc("dbo.COM_UserDesk_Get_Pending_PDS", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            PDSCount = Convert.ToInt32(objDR["PendingPDS"].ToString());
            PDSAmount = objDR["Amount"].ToString();

            lnk_btnPDS.Text = " (" + Convert.ToString(PDSCount) + "), (" + PDSAmount + ")";

            if (PDSCount <= 0)
            {
                lnk_btnPDS.Visible = false;
            }

            if (Branch_ID > 0)
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingPDSList.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID);
                lnk_btnPDS.Attributes.Add("onclick", "return OpenF4MenuPDS('" + PathF4 + "')");

            }
            else
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/Frm_Pending_PDS_Summary.aspx?Branch_ID=0&Area_ID=0&Region_ID=0");
                lnk_btnPDS.Attributes.Add("onclick", "return OpenF4MenuPDSSummary('" + PathF4 + "')");
            }

        }

        if (objDS.Tables[2].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[2].Rows[0];

            PendingTripVehicleCount = Convert.ToInt32(objDR["VehicleCount"].ToString());
            PendingTripMemoInvoiceCount = objDR["MemoCount"].ToString();

            lnk_TripMemoPendingCount.Text = " (" + PendingTripVehicleCount + "), (" + PendingTripMemoInvoiceCount + ")";

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingTripMemoList.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID);
            lnk_TripMemoPendingCount.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");

            if (PendingTripVehicleCount <= 0)
            {
                lnk_TripMemoPendingCount.Visible = false;
            }
        }

        if (objDS.Tables[3].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[3].Rows[0];

            DirectDlyLRCount = Convert.ToInt32(objDR["DirectDlyLRCount"].ToString());
            DirectDlyLRAmount = objDR["DirectDlyLRAmount"].ToString();

            lnk_DirectDlyPending.Text = " (" + DirectDlyLRCount + "), (" + DirectDlyLRAmount + ")";

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingDirectDeliveryList.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID);
            lnk_DirectDlyPending.Attributes.Add("onclick", "return PendingDirectDelivery('" + PathF4 + "')");

            if (DirectDlyLRCount <= 0 || (UserManager.getUserParam().HierarchyCode == "HO" && UserManager.getUserParam().ProfileId != 20) )
            {
                lnk_DirectDlyPending.Visible = false;
            }
        }





        if (objDS.Tables[4].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[4].Rows[0];

            InwardPendingDaysCount = Convert.ToInt32(objDR["InwardPendingDaysCount"].ToString());

            InwardBranchCount = Convert.ToInt32(objDR["InwardBranchCount"].ToString());

            InwardPendingVehicleCount = objDR["InwardPendingVehicleCount"].ToString();

            lnk_InwardPending.Text = " (" + InwardBranchCount + "), (" + InwardPendingVehicleCount + ")";


            if (HierarchyCode == "BO")
            {
                string InwardBranch_Name;

                InwardBranch_Name = UserManager.getUserParam().MainName;

                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingInwardList.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID + "&InwardBranch_Name=" + InwardBranch_Name);
                lnk_InwardPending.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");
            }
            else
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingInwardSummary.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID);
                lnk_InwardPending.Attributes.Add("onclick", "return OpenF4MenuSummaryAUS('" + PathF4 + "')");

            }


            if (InwardPendingDaysCount <= 0)
            {
                lnk_InwardPending.Visible = false;
            }
        }

        if (objDS.Tables[5].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[5].Rows[0];

            eWayBillVerificationPending = Convert.ToInt32(objDR["PendingeWayBills"].ToString());

            lnk_btneWayBillVerification.Text = " (" + eWayBillVerificationPending + ")";


            if (eWayBillVerificationPending <= 0)
            {
                lnk_btneWayBillVerification.Visible = false;
            }
        }

        if (objDS.Tables[6].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[6].Rows[0];

            MyStockLRCount = Convert.ToInt32(objDR["MyStockLRCount"].ToString());
            MyStockQty = Convert.ToInt32(objDR["MyStockQty"].ToString());


            lnk_btnDlyBranchwiseStock.Text = " (" + MyStockLRCount + "), (" + MyStockQty + ")";

            if (MyStockLRCount <= 0)
            {
                lnk_btnDlyBranchwiseStock.Visible = false;
            }
        }

        if (objDS.Tables[7].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[7].Rows[0];

            DlyStockLRCount = Convert.ToInt32(objDR["DlyStockLRCount"].ToString());
            DlyStockQty = Convert.ToInt32(objDR["DlyStockQty"].ToString());
            DlyStockAmount = objDR["DlyStockAmount"].ToString();

            lnk_btnPendingDlyStock.Text = " (" + DlyStockLRCount + "), (" + DlyStockQty + "), (" + DlyStockAmount + ")";

            //if (Branch_ID == 0)
            //{
            lnk_btnPendingDlyStock.Attributes.Add("onclick", "return PendingDlyStock(" + Convert.ToString(Branch_ID) + ")");
            //}
            //else
            //{
            //    DateTime As_On_Date = DateTime.Now;
            //    int Division_Id = 1;

            //    PathF4 = new StringBuilder(Util.GetBaseURL());
            //    PathF4.Append("/Reports/CL_Nandwana/User Desk/Frm_Dly_BranchWise_Pending_Stock_Details.aspx?Branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Branch_ID)
            //        + "&As_On_Date=" + As_On_Date.ToString()
            //        + "&Division_Id=" + ClassLibraryMVP.Util.EncryptInteger(Division_Id)
            //        + "&Delivery_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Branch_ID)
            //        + "&DlyBranch=" + ClassLibraryMVP.Util.EncryptString(UserManager.getUserParam().MainName) + "&IsDetailed=True");

            //    lnk_btnPendingDlyStock.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");
            //}


            if (DlyStockLRCount <= 0)
            {
                lnk_btnPendingDlyStock.Visible = false;
            }
        }

        if (objDS.Tables[8].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[8].Rows[0];

            PendingDlyAddressCount = Convert.ToInt32(objDR["NewClientGC"].ToString());
            UpdatedDlyAddressCount = Convert.ToInt32(objDR["UpdatedGC"].ToString());

            lnk_btnPendingDlyAddress.Text = " (" + PendingDlyAddressCount + ")";
            lnk_btnUpdatedDlyAddress.Text = " (" + UpdatedDlyAddressCount + ")";


            if (Branch_ID > 0)
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingDeliveryAddress.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID + "&IsUpdated=0&IsBkg=1&DlyLocation=0");
                lnk_btnPendingDlyAddress.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");

                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingDeliveryAddress.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID + "&IsUpdated=1&IsBkg=1&DlyLocation=0");
                lnk_btnUpdatedDlyAddress.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");
            }
            else
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingDeliveryAddressSummary.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID + "&IsUpdated=0&IsBkg=1&DlyLocation=0");
                lnk_btnPendingDlyAddress.Attributes.Add("onclick", "return OpenF4MenuSummary('" + PathF4 + "')");

                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingDeliveryAddressSummary.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID + "&IsUpdated=1&&IsBkg=1&DlyLocation=0");
                lnk_btnUpdatedDlyAddress.Attributes.Add("onclick", "return OpenF4MenuSummary('" + PathF4 + "')");

            }

            if (PendingDlyAddressCount <= 0)
            {
                lnk_btnPendingDlyAddress.Visible = false;
            }


            if (UpdatedDlyAddressCount <= 0)
            {
                lnk_btnUpdatedDlyAddress.Visible = false;
            }

        }

        if (objDS.Tables[9].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[9].Rows[0];

            PendingDlyAddressDlyBrWiseCount = Convert.ToInt32(objDR["NewClientGC"].ToString());
            UpdatedDlyAddressDlyBrWiseCount = Convert.ToInt32(objDR["UpdatedGC"].ToString());

            lnk_btnPendingDlyAddressDlyBrWise.Text = " (" + PendingDlyAddressDlyBrWiseCount + ")";
            lnk_btnUpdatedDlyAddressDlyBrWise.Text = " (" + UpdatedDlyAddressDlyBrWiseCount + ")";


            if (Branch_ID > 0)
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingDeliveryAddress.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID + "&IsUpdated=0&IsBkg=0&DlyLocation=0");
                lnk_btnPendingDlyAddressDlyBrWise.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");

                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingDeliveryAddress.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID + "&IsUpdated=1&IsBkg=0&DlyLocation=0");
                lnk_btnUpdatedDlyAddressDlyBrWise.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");
            }
            else
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingDeliveryAddressSummary.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID + "&IsUpdated=0&IsBkg=0&DlyLocation=0");
                lnk_btnPendingDlyAddressDlyBrWise.Attributes.Add("onclick", "return OpenF4MenuSummary('" + PathF4 + "')");

                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingDeliveryAddressSummary.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID + "&IsUpdated=1&IsBkg=0&DlyLocation=0");
                lnk_btnUpdatedDlyAddressDlyBrWise.Attributes.Add("onclick", "return OpenF4MenuSummary('" + PathF4 + "')");

            }

            if (PendingDlyAddressDlyBrWiseCount <= 0)
            {
                lnk_btnPendingDlyAddressDlyBrWise.Visible = false;
            }


            if (UpdatedDlyAddressDlyBrWiseCount <= 0)
            {
                lnk_btnUpdatedDlyAddressDlyBrWise.Visible = false;
            }

        }

        if (objDS.Tables[10].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[10].Rows[0];

            PendingFrtDlyLRCount = Convert.ToInt32(objDR["TotalLR"].ToString());
            PendingFrtDlyAmount = objDR["TotalFreight"].ToString();


            lnk_PendingDeliveryFreight.Text = " (" + PendingFrtDlyLRCount + "), (" + PendingFrtDlyAmount + ")";

            if (PendingFrtDlyLRCount <= 0)
            {
                lnk_PendingDeliveryFreight.Visible = false;
            }

            if (Branch_ID > 0)
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingFreightDeliveryConsigneeSummary.aspx?Delivery_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Branch_ID) + "&DlyBranch=" + ClassLibraryMVP.Util.EncryptString(Branch_Name) + "&IsDetailed=True");
                lnk_PendingDeliveryFreight.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");
            }
            else
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingFreightDeliverySummary.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID);
                lnk_PendingDeliveryFreight.Attributes.Add("onclick", "return OpenF4MenuSummary('" + PathF4 + "')");
            }

        }

        if (objDS.Tables[11].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[11].Rows[0];

            BillingDueClientCount = Convert.ToInt32(objDR["UnBilledTotalClient"].ToString());

            lnk_BillingDue.Text = " ("  + BillingDueClientCount  + ")";

            if (BillingDueClientCount <= 0)
            {
                lnk_BillingDue.Visible = false;
            }

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmDueForBilling.aspx");
            lnk_BillingDue.Attributes.Add("onclick", "return DueForBilling('" + PathF4 + "')");

            if (UserManager.getUserParam().ProfileId != 32 && UserManager.getUserParam().HierarchyCode == "BO" )
            {
                imgBillingDue.Visible = false;
                lbl_BillingDue.Visible = false;
                lnk_BillingDue.Visible = false;
            }

        }

        if (objDS.Tables[12].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[12].Rows[0];

            TripExpenseApprovalPending = Convert.ToInt32(objDR["Trips"].ToString());

            lnk_btnTripExpenseApproval.Text = " (" + TripExpenseApprovalPending + ")";

            if (TripExpenseApprovalPending <= 0)
            {
                imgTripExpenseApproval.Visible = false;
                lbl_TripExpenseApproval.Visible = false;
                lnk_btnTripExpenseApproval.Visible = false;
            }

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingTripExpenseApproval.aspx");
            lnk_btnTripExpenseApproval.Attributes.Add("onclick", "return TripExpenseApproval('" + PathF4 + "')");

            if (UserManager.getUserParam().ProfileId != 32 && UserManager.getUserParam().HierarchyCode == "BO")
            {
                imgTripExpenseApproval.Visible = false;
                lbl_TripExpenseApproval.Visible = false;
                lnk_btnTripExpenseApproval.Visible = false;
            }
        }

        if (objDS.Tables[13].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[13].Rows[0];

            TripAdvancePending = Convert.ToInt32(objDR["PendingAdvance"].ToString());

            lnk_btnTripAdvancePending.Text = " (" + TripAdvancePending + ")";

            if (TripAdvancePending <= 0)
            {
                lnk_btnTripAdvancePending.Visible = false;
                imgTripAdvancePending.Visible = false;
                lbl_TripAdvancePending.Visible = false;
            }

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingTripDriverAdvance.aspx");
            lnk_btnTripAdvancePending.Attributes.Add("onclick", "return TripAdvancePending('" + PathF4 + "')");

        }

        if (objDS.Tables[14].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[14].Rows[0];

            eWayBillVehicleUpdatePending = Convert.ToInt32(objDR["eWayBillVehicleUpdatePending"].ToString());
            PartBUpdatedVehicles = Convert.ToInt32(objDR["PartBUpdatedVehicles"].ToString());

            lnk_btneWayBillVehicleUpdate.Text = " (" + eWayBillVehicleUpdatePending + ")";
            lnk_btnPartBUpdatedVehicles.Text = " (" + PartBUpdatedVehicles  + ")"; 


            //if (eWayBillVehicleUpdatePending <= 0 && PartBUpdatedVehicles <=0 )
            //{
            //    lnk_btneWayBillVehicleUpdate.Visible = false;
            //    lnk_btnPartBUpdatedVehicles.Visible = false; 
            //    imgeWayBillVehicleUpdate.Visible = false;
            //    lbl_eWayBillVehicleUpdate.Visible = false;
            //}
            //else if (eWayBillVehicleUpdatePending <= 0)
            //{
            //    lnk_btneWayBillVehicleUpdate.Visible = false;
            //}
            //else if (PartBUpdatedVehicles <= 0)
            //{
            //    lnk_btnPartBUpdatedVehicles.Visible = false; 
            //}

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmeWayBill_Pending_Vehicle_Update.aspx");
            lnk_btneWayBillVehicleUpdate.Attributes.Add("onclick", "return eWayBillVehicleUpdatePending('" + PathF4 + "')");

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmeWayBill_PartB_Updated_Vehicles.aspx");
            lnk_btnPartBUpdatedVehicles.Attributes.Add("onclick", "return eWayBill_PartB_Updated_Vehicles('" + PathF4 + "')");

        }

        if (objDS.Tables[15].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[15].Rows[0];

            PendingTripSheet = Convert.ToInt32(objDR["PendingTripSheet"].ToString());

            lnk_btnPendingTripSheet.Text = " (" + PendingTripSheet + ")";

            if (PendingTripSheet <= 0)
            {
                lnk_btnPendingTripSheet.Visible = false;
                imgPendingTripSheet.Visible = false;
                lbl_PendingTripSheet.Visible = false;
            }

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingTripSheet.aspx");
            lnk_btnPendingTripSheet.Attributes.Add("onclick", "return PendingTripSheet('" + PathF4 + "')");

            if (UserManager.getUserParam().ProfileId != 32 && UserManager.getUserParam().HierarchyCode == "BO")
            {
                imgPendingTripSheet.Visible = false;
                lbl_PendingTripSheet.Visible = false;
                lnk_btnPendingTripSheet.Visible = false;
            }
        }


        if (objDS.Tables[16].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[16].Rows[0];

            PendingChequeForDeposite = Convert.ToInt32(objDR["ChqCount"].ToString());

            lnk_btnPendingChequeForDeposite.Text = " (" + PendingChequeForDeposite + ")";

            if (PendingChequeForDeposite <= 0)
            {
                lnk_btnPendingChequeForDeposite.Visible = false;
                imgPendingChequeForDeposite.Visible = false;
                lbl_PendingChequeForDeposite.Visible = false;
            }

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingChequeForDeposite.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID);
            lnk_btnPendingChequeForDeposite.Attributes.Add("onclick", "return PendingChequeForDeposite('" + PathF4 + "')");

        }


        if (objDS.Tables[17].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[17].Rows[0];

            eWayBillVehicleUpdatePendingPDS = Convert.ToInt32(objDR["eWayBillVehicleUpdatePendingPDS"].ToString());
            PartBUpdatedVehiclesPDS = Convert.ToInt32(objDR["PartBUpdatedVehiclesPDS"].ToString());

            lnk_btneWayBillVehicleUpdatePDS.Text = " (" + eWayBillVehicleUpdatePendingPDS + ")";
            lnk_btnPartBUpdatedVehiclesPDS.Text = " (" + PartBUpdatedVehiclesPDS + ")";


            //if (eWayBillVehicleUpdatePendingPDS <= 0 && PartBUpdatedVehiclesPDS <= 0)
            //{
            //    lnk_btneWayBillVehicleUpdatePDS.Visible = false;
            //    lnk_btnPartBUpdatedVehiclesPDS.Visible = false;
            //    imgeWayBillVehicleUpdatePDS.Visible = false;
            //    lbl_eWayBillVehicleUpdatePDS.Visible = false;
            //}
            //else if (eWayBillVehicleUpdatePendingPDS <= 0)
            //{
            //    lnk_btneWayBillVehicleUpdatePDS.Visible = false;
            //}
            //else if (PartBUpdatedVehiclesPDS <= 0)
            //{
            //    lnk_btnPartBUpdatedVehiclesPDS.Visible = false;
            //}

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmeWayBill_Pending_Vehicle_Update_PDS.aspx");
            lnk_btneWayBillVehicleUpdatePDS.Attributes.Add("onclick", "return eWayBillVehicleUpdatePendingPDS('" + PathF4 + "')");

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmeWayBill_PartB_Updated_Vehicles_PDS.aspx");
            lnk_btnPartBUpdatedVehiclesPDS.Attributes.Add("onclick", "return eWayBill_PartB_Updated_Vehicles_PDS('" + PathF4 + "')");

        }

        if (objDS.Tables[18].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[18].Rows[0];

            DirectDlyLRCountDelivered = Convert.ToInt32(objDR["DirectDlyLRCountDelivered"].ToString());
            DirectDlyLRAmountDelivered = objDR["DirectDlyLRAmountDelivered"].ToString();

            lnk_DirectDlyPendingDelivered.Text = " (" + DirectDlyLRCountDelivered + "), (" + DirectDlyLRAmountDelivered + ")";

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/Operation/FrmDirectDlyWithoutMR.aspx");
            lnk_DirectDlyPendingDelivered.Attributes.Add("onclick", "return PendingDirectDeliveryDelivered('" + PathF4 + "')");

            if (DirectDlyLRCountDelivered <= 0 || (UserManager.getUserParam().HierarchyCode == "HO" && UserManager.getUserParam().ProfileId != 20))
            {
                lnk_DirectDlyPendingDelivered.Visible = false;
            }
        }


        if (objDS.Tables[19].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[19].Rows[0];

            PreveWayBillVerificationPending = Convert.ToInt32(objDR["PendingPreveWayBills"].ToString());

            lnk_btnPreveWayBillVerification.Text = " (" + PreveWayBillVerificationPending + ")";


            string PendingBranch_Name;

            PendingBranch_Name = UserManager.getUserParam().MainName;

            PathF4 = new StringBuilder(Util.GetBaseURL());
            PathF4.Append("/Reports/CL_Nandwana/User Desk/frm_PrevDayPendingeWayBillVerification.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID + "&PendingBranch_Name=" + PendingBranch_Name);

            lnk_btnPreveWayBillVerificationH.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");
            lnk_btnPreveWayBillVerification.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");


            if (PreveWayBillVerificationPending <= 0 || (UserManager.getUserParam().HierarchyCode == "BO"))
            {
                imgPreveWayBillVerification.Visible = false;
                lnk_btnPreveWayBillVerification.Visible = false;
                lnk_btnPreveWayBillVerificationH.Visible = false;
            }
        }

        if (objDS.Tables[20].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[20].Rows[0];

            PendingFrtBkgLRCount = Convert.ToInt32(objDR["TotalLR"].ToString());
            PendingFrtBkgAmount = objDR["TotalFreight"].ToString();


            lnk_PendingBookingFreight.Text = " (" + PendingFrtBkgLRCount + "), (" + PendingFrtBkgAmount + ")";

            if (PendingFrtBkgLRCount <= 0)
            {
                lnk_PendingBookingFreight.Visible = false;
            }

            if (Branch_ID > 0)
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingFreightBookingConsignorSummary.aspx?Booking_branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Branch_ID) + "&BkgBranch=" + ClassLibraryMVP.Util.EncryptString(Branch_Name) + "&IsDetailed=True");
                lnk_PendingBookingFreight.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");
            }
            else
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmPendingFreightBookingSummary.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID);
                lnk_PendingBookingFreight.Attributes.Add("onclick", "return OpenF4MenuSummary('" + PathF4 + "')");
            }
        }

        if (objDS.Tables[21].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[21].Rows[0];

            eWayBillExpiry = Convert.ToInt32(objDR["ExpiringToday"].ToString());


            lnk_btneWayBillExpiry.Text = " (" + eWayBillExpiry + ")";

            if (eWayBillExpiry <= 0)
            {
                lnk_btneWayBillExpiry.Visible = false;
            }

            if (Branch_ID > 0)
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmeWayBillsExpiringTodayList.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID + "&Branch_Name=" + Branch_Name);
                lnk_btneWayBillExpiry.Attributes.Add("onclick", "return OpenF4Menu('" + PathF4 + "')");
            }
            else
            {
                PathF4 = new StringBuilder(Util.GetBaseURL());
                PathF4.Append("/Reports/CL_Nandwana/User Desk/FrmeWayBillsExpiringTodaySummary.aspx?Branch_ID=" + Branch_ID + "&Area_ID=" + Area_ID + "&Region_ID=" + Region_ID);
                lnk_btneWayBillExpiry.Attributes.Add("onclick", "return OpenF4MenuSummary('" + PathF4 + "')");
                lnk_btneWayBillExpiryH.Attributes.Add("onclick", "return OpenF4MenuSummary('" + PathF4 + "')");

            }
        }

    }
}
