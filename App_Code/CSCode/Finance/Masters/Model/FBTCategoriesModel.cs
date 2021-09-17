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
/// Summary description for FBTCategoriesModel
/// </summary>

namespace Raj.EC.FinanceModel
{

    public class FBTCategoriesModel : ClassLibraryMVP.General.IModel
    {
        private IFBTCategoriesView objIFBTCategoriesView;
        private DAL _objDal = new DAL();
        DataSet objDS;
        public FBTCategoriesModel(IFBTCategoriesView fbtCategoriesView)
        {
            objIFBTCategoriesView = fbtCategoriesView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] param ={
                                     _objDal.MakeInParams("@FBT_Category_Id",SqlDbType.Int,0,objIFBTCategoriesView.FBTCategoryId)     
                                  };
            _objDal.RunProc("[dbo].[EC_FA_FBT_CategoryDetails_FillGrid]", param, ref  objDS);
            objDS.Tables[0].TableName = "FBT_Category_Details";
            return objDS;
        }
        public DataTable GetAssesseeCategoryValues()
        {
            _objDal.RunProc("dbo.EC_FA_FBT_AssesseeCategory_FillValues", ref objDS);
            return objDS.Tables[0];
        }
        public DataTable GetFBTCategoryValues()
        {
            _objDal.RunProc("dbo.EC_FA_FBT_FBTCategory_FillValues", ref objDS);
            return objDS.Tables[0];
        }

        public Message Save()
        {
            Message objMsg = new Message();

            SqlParameter[] param ={ 
                                    _objDal.MakeOutParams("@Error_Code",SqlDbType.Int,0),
                                    _objDal.MakeOutParams("@Error_Msg",SqlDbType.VarChar,4000),
                                    _objDal.MakeInParams("@FBT_Category",SqlDbType.Int,0,objIFBTCategoriesView.keyID),
                                    _objDal.MakeInParams("@FBT_Category_Id",SqlDbType.Int,0,objIFBTCategoriesView.FBTCategoryId),
                                    _objDal.MakeInParams("@FBT_Category_Details",SqlDbType.Xml,0,objIFBTCategoriesView.SessionCategoryDetailsGrid.GetXml())
                                    
                                  };
            _objDal.RunProc("[dbo].[EC_FA_FBT_CategoryDetails_Save]", param, ref  objDS);

            objMsg.messageID = Convert.ToInt32(param[0].Value);
            objMsg.message = Convert.ToString(param[1].Value);

            if (objMsg.messageID == 0)
            {
                objIFBTCategoriesView.ClearVariables();
                string _Msg = "";
                _Msg = "Saved Successfully";

                objIFBTCategoriesView.errorMessage = _Msg;
            }
            return objMsg;
        }
    }
}