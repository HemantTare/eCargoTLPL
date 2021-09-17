using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;


/// <summary>
/// Summary description for CompanyGeneralDetailsPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class CompanyGeneralDetailsPresenter : Presenter
    {
        private ICompanyGeneralDetailsView objICompanyGeneralDetailsView;
        private CompanyGeneralDetailsModel objCompanyGeneralDetailsModel;
        private DataSet objDS;

        public CompanyGeneralDetailsPresenter(ICompanyGeneralDetailsView companyGeneralDetailsView, bool IsPostBack)
        {
            objICompanyGeneralDetailsView = companyGeneralDetailsView;
            objCompanyGeneralDetailsModel = new CompanyGeneralDetailsModel(objICompanyGeneralDetailsView);

            base.Init(objICompanyGeneralDetailsView, objCompanyGeneralDetailsModel);

            if (!IsPostBack)
            {
                
                initValues();
            }
        }

       
        public void Save()
        {
            //base.DBSave();
            objCompanyGeneralDetailsModel.Save();
        }

        
        private void initValues()
        {


                objDS = objCompanyGeneralDetailsModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objICompanyGeneralDetailsView.CompanyId =Util.String2Int(DR["CompanyId"].ToString());
                    objICompanyGeneralDetailsView.CompanyName = DR["Company_Name"].ToString();
                    objICompanyGeneralDetailsView.MailingName = DR["Mailing_Name"].ToString();
                    objICompanyGeneralDetailsView.AddressView.AddressLine1 = DR["Address_Line_1"].ToString();
                    objICompanyGeneralDetailsView.AddressView.AddressLine2 = DR["Address_Line_2"].ToString();
                    objICompanyGeneralDetailsView.AddressView.PinCode = DR["Pin_Code"].ToString();
                    objICompanyGeneralDetailsView.AddressView.CityId = Util.String2Int(DR["City_Id"].ToString());
                    objICompanyGeneralDetailsView.AddressView.Phone1 = DR["Phone_1"].ToString();
                    objICompanyGeneralDetailsView.AddressView.Phone2 = DR["Phone_2"].ToString();
                    objICompanyGeneralDetailsView.AddressView.StdCode = DR["Std_Code"].ToString();
                    objICompanyGeneralDetailsView.AddressView.FaxNo = DR["Fax"].ToString();
                    objICompanyGeneralDetailsView.AddressView.EmailId = DR["email"].ToString();
                    objICompanyGeneralDetailsView.SetHOLedgerId(DR["HOLedger"].ToString(), DR["HO_Ledger_Id"].ToString());
                    objICompanyGeneralDetailsView.SetPFALedgerId(DR["PFALedger"].ToString(), DR["PFA_Ledger_Id"].ToString());
                    objICompanyGeneralDetailsView.ClientCode = DR["Client_Code"].ToString();
                    objICompanyGeneralDetailsView.SetHOBankLedger_Id(DR["HOBankLedgerName"].ToString(), DR["HO_Bank_Ledger_Id"].ToString());
                    objICompanyGeneralDetailsView.SetHOCashLedger_Id(DR["HOCashLedgerName"].ToString(), DR["HO_Cash_Ledger_Id"].ToString());
                }
               
            }



        }

    }
