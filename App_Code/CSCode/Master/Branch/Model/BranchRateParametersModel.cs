using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;

/// <summary>
/// Summary description for BranchRateParametersModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{
    public class BranchRateParametersModel : IModel
    {
        private IBranchRateParametersView objIBranchRateParametersView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;

        public BranchRateParametersModel(IBranchRateParametersView BranchRateParametersView)
        {
            objIBranchRateParametersView = BranchRateParametersView;
        }
        
        public DataSet FillValuesAfterBranchselection()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, objIBranchRateParametersView.FromBranchID) };
            objDAL.RunProc("EC_Master_Branch_Rate_Parameters_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }
        public DataSet FillValues()
        {
            objDAL.RunProc("EC_Mst_BranchRateParameters_FillValues", ref objDS);
            return objDS;

        }
        public DataSet ReadValues()
        {
           
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, objIBranchRateParametersView.keyID)};
            objDAL.RunProc("EC_Master_Branch_Rate_Parameters_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Key_Id", SqlDbType.Int, 0,objIBranchRateParametersView.keyID),
            objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0,objIBranchRateParametersView.ToBranchID),
            objDAL.MakeInParams("@Min_Charge_Wt", SqlDbType.Decimal, 0,objIBranchRateParametersView.MinChargeWt),
            objDAL.MakeInParams("@Bilty_Charges", SqlDbType.Decimal, 0,objIBranchRateParametersView.BiltyCharges),
            objDAL.MakeInParams("@FOV_Percent", SqlDbType.Decimal, 0,objIBranchRateParametersView.FOVPercent),
            objDAL.MakeInParams("@Min_FOV", SqlDbType.Decimal, 0,objIBranchRateParametersView.MinFOV),
            objDAL.MakeInParams("@Hamali_Per_Kg", SqlDbType.Decimal,0,objIBranchRateParametersView.Hamali),
            objDAL.MakeInParams("@Min_Hamali", SqlDbType.Decimal, 0,objIBranchRateParametersView.MinHamali),
            objDAL.MakeInParams("@Door_Delivery_Charges",SqlDbType.Decimal,0,objIBranchRateParametersView.DoorDeliveryCharges),
            objDAL.MakeInParams("@To_Pay_Charges", SqlDbType.Decimal, 0,objIBranchRateParametersView.ToPayCharges),
            objDAL.MakeInParams("@DACC_Charges", SqlDbType.Decimal, 0,objIBranchRateParametersView.DACCCharges),
            objDAL.MakeInParams("@CFT_Factor", SqlDbType.Int, 0,objIBranchRateParametersView.CFTFactor),
            objDAL.MakeInParams("@Service_Tax_Percent", SqlDbType.Decimal, 0,objIBranchRateParametersView.ServiceTax),
            objDAL.MakeInParams("@Demurrage_Days", SqlDbType.Int,0,objIBranchRateParametersView.DemurrageDays),
            objDAL.MakeInParams("@Demurrage_Rate_Kg_Per_Day", SqlDbType.Decimal,0,objIBranchRateParametersView.DemurrageRate),
            objDAL.MakeInParams("@Octroi_Form_Charges", SqlDbType.Decimal, 0,objIBranchRateParametersView.OctroiFormCharges),
            objDAL.MakeInParams("@Octroi_Service_Charges", SqlDbType.Decimal, 0,objIBranchRateParametersView.OctroiServiceCharge),
            objDAL.MakeInParams("@GI_Charges", SqlDbType.Decimal, 0,objIBranchRateParametersView.GICharges),
            objDAL.MakeInParams("@Delivery_Commission",SqlDbType.Decimal,0,objIBranchRateParametersView.DeliveryCommission),
            objDAL.MakeInParams("@First_Notice_Days", SqlDbType.Int, 0,objIBranchRateParametersView.FirstNoticeDays),
            objDAL.MakeInParams("@Second_Notice_Days", SqlDbType.Int, 0,objIBranchRateParametersView.SecontNoticeDays),
            objDAL.MakeInParams("@Third_Notice_Days", SqlDbType.Int, 0,objIBranchRateParametersView.ThirdNoticeDays),
            objDAL.MakeInParams("@Cash_Limit", SqlDbType.Decimal,0,objIBranchRateParametersView.CashLimit),
            objDAL.MakeInParams("@Bank_Limit", SqlDbType.Decimal,0,objIBranchRateParametersView.BankLimit),
            objDAL.MakeInParams("@Bkg_Freight_Chg_Discount_Percent", SqlDbType.Decimal,0,objIBranchRateParametersView.Bkg_Freight),
            objDAL.MakeInParams("@Bkg_Hamali_Chg_Discount_Percent", SqlDbType.Decimal,0,objIBranchRateParametersView.Bkg_HamaliofBooking),
            objDAL.MakeInParams("@Bkg_Fov_Chg_Discount_Percent", SqlDbType.Decimal,0,objIBranchRateParametersView.Bkg_FovofBooking),
            objDAL.MakeInParams("@Bkg_TP_Chg_Discount_Percent", SqlDbType.Decimal,0,objIBranchRateParametersView.Bkg_TpCharge),
            objDAL.MakeInParams("@Bkg_DD_Chg_Discount_Percent", SqlDbType.Decimal,0,objIBranchRateParametersView.Bkg_Ddcharge),           
            objDAL.MakeInParams("@Dly_Oct_Form_Chg_Discount_Percent", SqlDbType.Decimal,0,objIBranchRateParametersView.Dly_Octroiformchargepercent),
            objDAL.MakeInParams("@Dly_Oct_Service_Chg_Discount_Percent", SqlDbType.Decimal,0,objIBranchRateParametersView.Dly_Octroiservicechargepercent),
            objDAL.MakeInParams("@Dly_GI_Chg_Discount_Percent", SqlDbType.Decimal,0,objIBranchRateParametersView.Dly_GichargesofDel),
            objDAL.MakeInParams("@Dly_Hamali_Chg_Discount_Percent", SqlDbType.Decimal,0,objIBranchRateParametersView.Dly_HamaliofDel),
            objDAL.MakeInParams("@Dly_Demurrage_Chg_Discount_Percent", SqlDbType.Decimal,0,objIBranchRateParametersView.Dly_Demurrage),

            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
            objDAL.MakeInParams("@HamaliPerArticle",SqlDbType.Decimal,0,objIBranchRateParametersView.HamaliArticle),
            objDAL.MakeInParams("@FOVRate",SqlDbType.Decimal,0,objIBranchRateParametersView.FOVRate),
            objDAL.MakeInParams("@InvoiceRate",SqlDbType.Decimal,0,objIBranchRateParametersView.InvoiceRate),
            objDAL.MakeInParams("@InvoicePerHowManyRs",SqlDbType.Decimal,0,objIBranchRateParametersView.InvoicePerHowManyRs),
            objDAL.MakeInParams("@MaxBiltyCharges",SqlDbType.Decimal,0,objIBranchRateParametersView.MaxBiltyCharges),
            objDAL.MakeInParams("@AOC_Percent",SqlDbType.Decimal,0,objIBranchRateParametersView.AOCPercent)
            };


            objDAL.RunProc("EC_Master_Branch_Rate_Parameters_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";
            }
            return objMessage;
        }
    }
}

