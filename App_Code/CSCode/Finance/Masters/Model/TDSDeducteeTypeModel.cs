using System;
using System.Data;
using System.Data.SqlClient;

using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;

/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 17/10/2008
/// Summary description for TDSDeducteeTypeModel
/// </summary>
namespace Raj.EC.FinanceModel
{

    public class TDSDeducteeTypeModel : ClassLibraryMVP.General.IModel
    {
        private ITDSDeducteeTypeView objITDSDeducteeTypeView;
        private DAL _dalObj = new DAL();

        public TDSDeducteeTypeModel(ITDSDeducteeTypeView tdsDeducteeTypeView)
        {
            objITDSDeducteeTypeView = tdsDeducteeTypeView;
        }

        public DataSet ReadValues()
        {
            DataSet ds = new DataSet();
            return ds;
        }

        public DataTable Fill_Deductee_Type()
        {
            DataSet ds = new DataSet();
            _dalObj.RunProc("dbo.EC_FA_TDSDeducteeType_FillValues ", ref ds);
            return ds.Tables[0];
        }

        public DataSet Fill_Deductee_Details_Grid()
        {
            DataSet ds = new DataSet();
            SqlParameter[] sqlpar ={ _dalObj.MakeInParams("@TDS_Deductee_Type_Id", SqlDbType.Int, 0, objITDSDeducteeTypeView.Deductee_Type_ID) };
            _dalObj.RunProc("[dbo].[EC_FA_TDSDeducteeType_FillGrid]", sqlpar, ref ds);
            Raj.EC.Common.SetPrimaryKeys(new string[] { "Applicable_From" }, ds.Tables[0]);
            
            return ds;
        }

        public Message Save()
        {
            string _strXML;
            DataSet objDS = objITDSDeducteeTypeView.Session_Deductee_Details;
            _strXML = objDS.GetXml();
            DataSet ds = new DataSet();
            Message mObj = new Message();

            SqlParameter[] param = {
                                        _dalObj.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
                                        _dalObj.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000) ,
                                        _dalObj.MakeInParams("@Deductee_Type_ID",SqlDbType.Int,0,objITDSDeducteeTypeView.Deductee_Type_ID), 
                                        _dalObj.MakeInParams("@Deductee_Xml",SqlDbType.Xml ,0, _strXML)
                                     };

            _dalObj.RunProc("[dbo].[EC_FA_TDSDeducteeType_Save]", param, ref ds);

            mObj.messageID = Util.String2Int(param[0].Value.ToString());
            mObj.message = param[1].Value.ToString();

            if (mObj.messageID == 0)
            {
                objITDSDeducteeTypeView.ClearVariables();
            }

            return mObj;
        }
    }
}
