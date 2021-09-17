using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;


/// <summary>
/// Summary description for CompanyTDSFBTDetailsModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class CompanyTDSFBTDetailsModel : IModel
    {
        private ICompanyTDSFBTDetailsView objICompanyTDSFBTDetailsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;


        public CompanyTDSFBTDetailsModel(ICompanyTDSFBTDetailsView companyTDSFBTDetailsView)
        {
            objICompanyTDSFBTDetailsView = companyTDSFBTDetailsView;
        }

        public DataSet ReadValues()
        {
            //SqlParameter[] objSqlParam = { objDAL.MakeInParams("@KeyId", SqlDbType.Int, 0, objICompanyTDSFBTDetailsView.keyID)
            //                             };
            objDAL.RunProc("dbo.EC_Mst_CompanyDetails_ReadValues", ref objDS);
            return objDS;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Mst_CompanyDetails_FillValues", ref objDS);
            _setTableName(new string[] { "dbo.EC_Master_Employee",
                                         "dbo.FA_Mst_FBT_Assessee_Type",
                                         "dbo.FA_Mst_FBT_Assessee_Category" ,
                                         "dbo.FA_Mst_Ledger_TDS_Deductee_Type"                       
                                         }
                                         );
            return objDS;
        }

        private void _setTableName(string[] nameList)
        {
            for (int i = 0; i < nameList.Length; i++)
            {
                objDS.Tables[i].TableName = nameList[i];
            }

        }
        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;

        }
    }
}


