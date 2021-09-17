using System;
using System.Data;
using System.Data.SqlClient;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;

/// <summary>
/// Summary description for ContractRangeModel
/// </summary>
/// 
/// <summary>
/// Author        : Ankit
/// Created On    : 11/10/2008
/// Description   : This Page is For General contract range
/// </summary>
/// 

namespace Raj.EC.MasterModel
{
    public class ContractRangeModel : ClassLibraryMVP.General.IModel 
    {
        private IContractRangeView  _iContractRangeView ;
        private DAL _objDAL = new DAL();
        private DataSet _ds;
        private int _user_ID = UserManager.getUserParam().UserId;
        

        public ContractRangeModel(IContractRangeView  iContractRangeView)
        {
            _iContractRangeView = iContractRangeView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] SqlPara = {_objDAL.MakeInParams("@RangeID", SqlDbType.Int, 0, _iContractRangeView.keyID)
            };
            _objDAL.RunProc("[dbo].[EC_Master_ContractRange_ReadValues]", SqlPara, ref _ds);
            return _ds;
                      
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@Range_Id", SqlDbType.Int, 0, _iContractRangeView.keyID ),
                                       _objDAL.MakeInParams("@Range_Type_Id", SqlDbType.Int, 0, _iContractRangeView.ContractRangeTypeId ),
                                       _objDAL.MakeInParams("@From_Unit",SqlDbType.Int , 250, _iContractRangeView.From_Unit ),
                                       _objDAL.MakeInParams("@To_Unit",SqlDbType.Int  ,1,_iContractRangeView.To_Unit ),
                                       _objDAL.MakeInParams("@Is_Active",SqlDbType.Bit   ,1,1 ),
                                       _objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _user_ID),
                                       _objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
                                       _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000)};

            _objDAL.RunProc("[dbo].[EC_Master_Contract_Range_Save]", sqlPara);

            objMessage.messageID = Convert.ToInt32(sqlPara[6].Value);
            objMessage.message = Convert.ToString(sqlPara[7].Value);

            //if (objMessage.messageID == 0)
            //{
            //    string _Msg;
            //    _Msg = "Saved SuccessFully";
            //    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            //}

            return objMessage;
        }


        public Boolean isDuplicate()
        {
            Boolean isdup;
            SqlParameter[] SqlPara = {_objDAL.MakeInParams("@Range_ID", SqlDbType.Int, 0, _iContractRangeView.keyID ),
                                        _objDAL.MakeInParams("@Range_Type_ID", SqlDbType.Int, 0, _iContractRangeView.ContractRangeTypeId ),
                                        _objDAL.MakeInParams("@unit_from", SqlDbType.Int, 0, _iContractRangeView.To_Unit ),
                                        _objDAL.MakeInParams("@unit_to", SqlDbType.Int, 0, _iContractRangeView.From_Unit ),
                                        _objDAL.MakeOutParams ("@Is_duplicate", SqlDbType.Bit,5)};

            _objDAL.RunProc("[dbo].[EC_Master_Check_MinMaxRange]", SqlPara, ref _ds);

            isdup = Convert.ToBoolean(SqlPara[4].Value);

            return isdup;
        }
    }
}