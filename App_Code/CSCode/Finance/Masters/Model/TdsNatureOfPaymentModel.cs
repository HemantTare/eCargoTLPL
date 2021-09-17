using System;
using System.Data;
using System.Data.SqlClient;

using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;

/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 16/10/2008
/// Summary description for TdsNatureOfPaymentModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class TdsNatureOfPaymentModel : ClassLibraryMVP.General.IModel
    {
        private ITdsNatureOfPaymentView objITdsNatureOfPaymentView;
        private DAL _dalObj = new DAL();

        public TdsNatureOfPaymentModel(ITdsNatureOfPaymentView tdsNatureOfPaymentView)
        {
            objITdsNatureOfPaymentView = tdsNatureOfPaymentView;
        }

        public DataSet ReadValues()
        {
            DataSet ds = new DataSet();
            return ds;
        }

        //public DataSet FillDeducteeType()
        //{
        //    DataSet ds = new DataSet();
        //    _dalObj.RunProc("dbo.EC_FA_TdsNatureOfPayment_FillDeducteeType", ref ds);
        //    return ds;
        //}


        public DataTable Fill_Deductee_Type()
        {
            DataSet ds = new DataSet();
            _dalObj.RunProc("dbo.EC_FA_TdsNatureOfPayment_FillDeducteeTypeValues", ref ds);
            return ds.Tables[0];
        }

        public DataSet Fill_Payment_Details_Grid()
        {
            DataSet ds = new DataSet();
            SqlParameter[] sqlpar ={ _dalObj.MakeInParams("@TDS_Nature_Type_Id", SqlDbType.Int, 0, objITdsNatureOfPaymentView.Nature_Payment_ID) };
            _dalObj.RunProc("dbo.EC_FA_TdsNatureOfPayment_FillGrid",sqlpar, ref ds);
          //  Raj.EC.Common.SetPrimaryKeys(new string[] { "Nature_Payment_Id", "TDS_Deductee_Type_ID", "Applicable_From" }, ds.Tables[0]);
            return ds;
        }

        public DataSet Fill_Nature_Payment()
        {
            DataSet ds = new DataSet();
            _dalObj.RunProc("dbo.EC_FA_TdsNatureOfPayment_FillNatureOfPayment", ref ds);
            return ds;
        }

        public Message Save()
        {
            string _strXML;
            DataSet objDS = objITdsNatureOfPaymentView.Session_Payment_Details;
            _strXML = objDS.GetXml();
            DataSet ds = new DataSet();
            Message objMsg = new Message();


            SqlParameter[] param = {
                                        _dalObj.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
                                        _dalObj.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000) ,
                                        _dalObj.MakeInParams("@Nature_Payment_ID",SqlDbType.Int,0,objITdsNatureOfPaymentView.Nature_Payment_ID), 
                                        _dalObj.MakeInParams("@Nature_Xml",SqlDbType.Xml ,0,_strXML)
                                     };

            _dalObj.RunProc("[dbo].[EC_FA_TdsNatureOfPayment_Save]", param, ref ds);

            objMsg.messageID = Util.String2Int(param[0].Value.ToString());
            objMsg.message = param[1].Value.ToString();

            if (objMsg.messageID == 0)
            {
                objITdsNatureOfPaymentView.ClearVariables();
            }

            return objMsg;
        }
    }

}

