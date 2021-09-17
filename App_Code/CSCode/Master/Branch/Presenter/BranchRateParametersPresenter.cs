using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for BranchRateParametersPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class BranchRateParametersPresenter : Presenter
    {
        private IBranchRateParametersView objIBranchRateParametersView;
        private BranchRateParametersModel objBranchRateParametersModel;
        private DataSet objDS;

        public BranchRateParametersPresenter(IBranchRateParametersView BranchRateParametersView, bool isPostback)
        {
            objIBranchRateParametersView = BranchRateParametersView;
            objBranchRateParametersModel = new BranchRateParametersModel(objIBranchRateParametersView);
            base.Init(objIBranchRateParametersView, objBranchRateParametersModel);

            if (!isPostback)
            {
                objDS = objBranchRateParametersModel.FillValues();
                objIBranchRateParametersView.IsFOVCalculatedAsPerStandard = Util.String2Bool(objDS.Tables[0].Rows[0]["Is_FOV_Calculated_As_Per_Standard"].ToString());
                if (objIBranchRateParametersView.keyID > 0)
                {   
                    initValues();
                }
            }
        }
      
        public void fillValuesAfterBranchSelection()
        {
            objDS = objBranchRateParametersModel.FillValuesAfterBranchselection();
            SetAllValues(objDS,false);
        }

        private void initValues()
        {
            objDS = objBranchRateParametersModel.ReadValues();
            SetAllValues(objDS,true);
        }
        private void SetAllValues(DataSet objDS,bool Is_ChangeBranch)
        {
               if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    if (Is_ChangeBranch == true)
                    {
                        objIBranchRateParametersView.ToBranchName = objDR["Branch_Name"].ToString();
                    }
                    objIBranchRateParametersView.MinChargeWt = Util.String2Decimal(objDR["Min_Charge_Wt"].ToString());
                    objIBranchRateParametersView.BiltyCharges = Util.String2Decimal(objDR["Bilty_Charges"].ToString());
                    objIBranchRateParametersView.FOVPercent = Util.String2Decimal(objDR["FOV_Percent"].ToString());
                    objIBranchRateParametersView.MinFOV = Util.String2Decimal(objDR["Min_FOV"].ToString());
                    objIBranchRateParametersView.Hamali = Util.String2Decimal(objDR["Hamali_Per_Kg"].ToString());
                    objIBranchRateParametersView.MinHamali = Util.String2Decimal(objDR["Min_Hamali"].ToString());
                    objIBranchRateParametersView.DoorDeliveryCharges = Util.String2Decimal(objDR["Door_Delivery_Charges"].ToString());
                    objIBranchRateParametersView.ToPayCharges = Util.String2Decimal(objDR["To_Pay_Charges"].ToString());
                    objIBranchRateParametersView.DACCCharges = Util.String2Decimal(objDR["DACC_Charges"].ToString());
                    objIBranchRateParametersView.CFTFactor = Util.String2Int(objDR["CFT_Factor"].ToString());
                    objIBranchRateParametersView.ServiceTax = Util.String2Decimal(objDR["Service_Tax_Percent"].ToString());
                    objIBranchRateParametersView.DemurrageDays = Util.String2Int(objDR["Demurrage_Days"].ToString());
                    objIBranchRateParametersView.DemurrageRate = Util.String2Decimal(objDR["Demurrage_Rate_Kg_Per_Day"].ToString());
                    objIBranchRateParametersView.OctroiFormCharges = Util.String2Decimal(objDR["Octroi_Form_Charges"].ToString());
                    objIBranchRateParametersView.OctroiServiceCharge = Util.String2Decimal(objDR["Octroi_Service_Charges"].ToString());
                    objIBranchRateParametersView.GICharges = Util.String2Decimal(objDR["GI_Charges"].ToString());
                    objIBranchRateParametersView.DeliveryCommission = Util.String2Decimal(objDR["Delivery_Commission"].ToString());
                    objIBranchRateParametersView.FirstNoticeDays = Util.String2Int(objDR["First_Notice_Days"].ToString());
                    objIBranchRateParametersView.SecontNoticeDays = Util.String2Int(objDR["Second_Notice_Days"].ToString());
                    objIBranchRateParametersView.ThirdNoticeDays = Util.String2Int(objDR["Third_Notice_Days"].ToString());
                    objIBranchRateParametersView.CashLimit = Util.String2Decimal(objDR["Cash_Limit"].ToString());
                    objIBranchRateParametersView.BankLimit = Util.String2Decimal(objDR["Bank_Limit"].ToString());

                    objIBranchRateParametersView.Bkg_Freight = Util.String2Decimal(objDR["Bkg_Freight_Chg_Discount_Percent"].ToString());
                    objIBranchRateParametersView.Bkg_HamaliofBooking = Util.String2Decimal(objDR["Bkg_Hamali_Chg_Discount_Percent"].ToString());
                    objIBranchRateParametersView.Bkg_FovofBooking = Util.String2Decimal(objDR["Bkg_Fov_Chg_Discount_Percent"].ToString());
                    objIBranchRateParametersView.Bkg_TpCharge = Util.String2Decimal(objDR["Bkg_TP_Chg_Discount_Percent"].ToString());
                    objIBranchRateParametersView.Bkg_Ddcharge = Util.String2Decimal(objDR["Bkg_DD_Chg_Discount_Percent"].ToString());

                    objIBranchRateParametersView.Dly_Octroiformchargepercent = Util.String2Decimal(objDR["Dly_Oct_Form_Chg_Discount_Percent"].ToString());
                    objIBranchRateParametersView.Dly_Octroiservicechargepercent  = Util.String2Decimal(objDR["Dly_Oct_Service_Chg_Discount_Percent"].ToString());
                    objIBranchRateParametersView.Dly_GichargesofDel = Util.String2Decimal(objDR["Dly_GI_Chg_Discount_Percent"].ToString());
                    objIBranchRateParametersView.Dly_HamaliofDel = Util.String2Decimal(objDR["Dly_Hamali_Chg_Discount_Percent"].ToString());
                    objIBranchRateParametersView.Dly_Demurrage = Util.String2Decimal(objDR["Dly_Demurrage_Chg_Discount_Percent"].ToString());
                    objIBranchRateParametersView.HamaliArticle = Util.String2Decimal(objDR["Hamali_Per_Article"].ToString());
                    objIBranchRateParametersView.FOVRate = Util.String2Decimal(objDR["FOV_Rate"].ToString());
                    objIBranchRateParametersView.InvoiceRate = Util.String2Decimal(objDR["Invoice_Rate"].ToString());
                    objIBranchRateParametersView.InvoicePerHowManyRs = Util.String2Decimal(objDR["Invoice_Per_How_Many_Rs"].ToString());
                    objIBranchRateParametersView.MaxBiltyCharges = Util.String2Decimal(objDR["Max_Bilty_Charges"].ToString());
                    objIBranchRateParametersView.AOCPercent = Util.String2Decimal(objDR["AOC_Percent"].ToString());

            }
        }

        public void Save()
        {
            base.DBSave();
        }

    }
}