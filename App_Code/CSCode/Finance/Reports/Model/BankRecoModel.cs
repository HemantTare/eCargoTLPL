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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.FA.ReportsView;
/// <summary>
/// Summary description for BankRecoModel
/// </summary>
/// 

namespace Raj.FA.ReportsModel
{
    public class BankRecoModel : IModel
    {
        private IBankRecoView _objIBankRecoView;
        private DAL _objDAL = new DAL();
        private DataSet Ds = new DataSet();
        public BankRecoModel(IBankRecoView objIBankRecoView)
        {
            _objIBankRecoView = objIBankRecoView;
        }


        public DataSet ReadValues()
        {
            SqlParameter[] param =   {      
                                        _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2,_objIBankRecoView.Hierarchy_Code),//"HO"),
                                        _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, _objIBankRecoView.Main_Id),//1),
                                        _objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int, 0,_objIBankRecoView.keyID ),//31527),
                                        _objDAL.MakeInParams("@Start_Date", SqlDbType.DateTime, 0,_objIBankRecoView.Start_Date),//Convert.ToDateTime("1/11/2009")),
                                        _objDAL.MakeInParams("@End_Date", SqlDbType.DateTime, 0,_objIBankRecoView.End_Date),//Convert.ToDateTime("30/11/2009")),
                                         _objDAL.MakeInParams("@Is_Consol", SqlDbType.Bit, 0, _objIBankRecoView.Is_Consol),
                                         _objDAL.MakeInParams("@Is_Uncleared", SqlDbType.Bit, 0, _objIBankRecoView.Is_Uncleared)
                                  };

            //_objDAL.RunProc("dbo.FA_RPT_Bank_Reco_Fill_VT", param, ref Ds);
            _objDAL.RunProc("dbo.FA_RPT_Bank_Reco_Fill_Temp", param, ref Ds);

            return Ds;
        }

 
        public Message Save()
        {
            Message _objMessage = new Message();
            string _bankRecoXml = _objIBankRecoView.get_Ds_BankReco.GetXml();
            SqlParameter[] param = {      
                                    _objDAL.MakeOutParams("@ErrorCode", SqlDbType.Int, 0),
                                    _objDAL.MakeOutParams("@ErrorDesc", SqlDbType.VarChar, 4000),
                                    _objDAL.MakeInParams("@Ledger_ID", SqlDbType.Int, 0, _objIBankRecoView.keyID),
                                    _objDAL.MakeInParams("@BankRecoXML", SqlDbType.Xml, 0,_bankRecoXml ),
                                    _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, _objIBankRecoView.Hierarchy_Code),
                                    _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, _objIBankRecoView.Main_Id)
                                 };

            _objDAL.RunProc("dbo.FA_RPT_Bank_Reco_Update", param);

            _objMessage.messageID = Convert.ToInt32(param[0].Value);
            _objMessage.message = Convert.ToString(param[1].Value);

            return _objMessage;

        }


    }
}