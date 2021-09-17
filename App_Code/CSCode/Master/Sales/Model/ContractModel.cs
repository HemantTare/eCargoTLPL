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
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;

/// <summary>
/// Summary description for ContractModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{

    public class ContractModel : IModel
    {
        private IContractView objIContractView;
        private DAL objDAL = new DAL();
        DataSet objDS = null;
        private int _userID = UserManager.getUserParam().UserId;
        private int _divisionID = UserManager.getUserParam().DivisionId;
        private int _yearCODE = UserManager.getUserParam().YearCode;
        private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _Menu_Item_ID = Raj.EC.Common.GetMenuItemId();
        private int _Main_ID = UserManager.getUserParam().MainId;


        public ContractModel(IContractView contractView)
        {
            objIContractView = contractView;
        }
        public DataSet ReadValues()
        {
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                   objDAL.MakeOutParams("@ERROR_DESC", SqlDbType.VarChar, 4000),                                    
                                   objDAL.MakeInParams("@Contract_ID", SqlDbType.Int, 0,objIContractView.keyID),
                                   objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,_yearCODE),
                                   objDAL.MakeInParams("@Contract_Branch_ID", SqlDbType.Int, 0,objIContractView.ContractGeneralView.BranchID),
                                   objDAL.MakeInParams("@Contract_No", SqlDbType.VarChar,50,objIContractView.ContractGeneralView.ContractNo),
                                   objDAL.MakeInParams("@Contract_Name",SqlDbType.VarChar,50,objIContractView.ContractGeneralView.ContractName),
                                   objDAL.MakeInParams("@Contract_Date", SqlDbType.DateTime, 0,objIContractView.ContractGeneralView.ContractDate),
                                   objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0,objIContractView.ContractGeneralView.ClientNameID),
                                   objDAL.MakeInParams("@Client_PO_No", SqlDbType.NVarChar, 0,objIContractView.ContractGeneralView.ClientPONo),
                                   objDAL.MakeInParams("@Client_PO_Date", SqlDbType.DateTime, 0,objIContractView.ContractGeneralView.PODate),
                                   objDAL.MakeInParams("@PO_Max_Limmit", SqlDbType.Decimal, 0,objIContractView.ContractGeneralView.POMaxLimit),
                                   objDAL.MakeInParams("@Valid_From", SqlDbType.DateTime , 0,objIContractView.ContractGeneralView.ValidFromDate),                                   
                                   objDAL.MakeInParams("@Valid_UpTo", SqlDbType.DateTime, 0,objIContractView.ContractGeneralView.ValidUptoDate),
                                   objDAL.MakeInParams("@Promissed_Wt_Per_Month", SqlDbType.Decimal , 0,objIContractView.ContractGeneralView.Weight),
                                   objDAL.MakeInParams("@Promissed_Freight_Per_Month", SqlDbType.Decimal, 0,objIContractView.ContractGeneralView.Freight),
                                   objDAL.MakeInParams("@Credit_Limit", SqlDbType.Decimal, 0,objIContractView.ContractGeneralView.Amount),
                                   objDAL.MakeInParams("@Credit_Period", SqlDbType.Int, 0,objIContractView.ContractGeneralView.Days),
                                   objDAL.MakeInParams("@Billing_Client_ID", SqlDbType.Int, 0,objIContractView.ContractGeneralView.BillingClientID),                                   
                                   objDAL.MakeInParams("@Billing_Branch_ID", SqlDbType.Int, 0,objIContractView.ContractGeneralView.BillingBranchID),
                                   objDAL.MakeInParams("@Billing_Hierarchy",SqlDbType.VarChar,2,objIContractView.ContractGeneralView.BillingHierarchy),                    
                                   objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250, objIContractView.ContractGeneralView.Remark),
                                   objDAL.MakeInParams("@Freight_Unit_ID", SqlDbType.Int, 0,objIContractView.ContractFreightDetailsView.UnitOfFreightID),
                                   objDAL.MakeInParams("@Freight_Sub_Unit_ID", SqlDbType.Int, 0, objIContractView.ContractFreightDetailsView.SubUnitID),
                                   objDAL.MakeInParams("@Freight_Basis_ID", SqlDbType.Int, 0, objIContractView.ContractFreightDetailsView.FreightBasisID), 
                                   objDAL.MakeInParams("@ContractTermsXML", SqlDbType.Xml, 0, objIContractView.ContractTermsView.ContractTermsXML ),
                                   objDAL.MakeInParams("@FreightDetailsGridXML", SqlDbType.Xml, 0, objIContractView.ContractFreightDetailsView.FreightDetailsGridXML ),  
                                   objDAL.MakeInParams("@OtherChargesFreightRateDetailsXML", SqlDbType.Xml, 0, objIContractView.ContractFreightDetailsView.OtherChargesFreightRateDetailsXML ), 
                                   objDAL.MakeInParams("@Freight_Unit_Item_ID", SqlDbType.Int, 0,objIContractView.ContractFreightDetailsView.UnitFreightID),
                                   objDAL.MakeInParams("@Freight_Sub_Unit_Item_ID", SqlDbType.Int, 0, objIContractView.ContractFreightDetailsView.SubUnitFreightID),
                                   objDAL.MakeInParams("@CFT_Factor", SqlDbType.Decimal, 0, objIContractView.ContractFreightDetailsView.CFTFactor), 
                                   objDAL.MakeInParams("@AttachmentFormId", SqlDbType.Int, 0, objIContractView.AttachmentsView.AttachmentFormId), 
                                   objDAL.MakeInParams("@AttachmentsXML", SqlDbType.Xml, 0, objIContractView.AttachmentsView.AttachmentsXML),  
                                   objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
                                   objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_divisionID),
                                   objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 20,_HierarchyCode),
                                   objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int,0,_Menu_Item_ID),
                                   objDAL.MakeInParams("@Main_ID", SqlDbType.Int,0,_Main_ID),
                                   objDAL.MakeInParams("@GCRiskTypeId",SqlDbType.Int,0,objIContractView.ContractGeneralView.GCRiskId),
                                   objDAL.MakeInParams("@ConsignmentTypeId",SqlDbType.Int,0,objIContractView.ContractGeneralView.ConsignmentTypeId)
                                   };


             objDAL.RunProc("EC_Mst_Contract_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                objIContractView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";

                if (objIContractView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Master/Sales/FrmContract.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIContractView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
            }

            return objMessage;
        }
    }
}