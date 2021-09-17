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
/// Summary description for FBTAssesseeDetailsModel
/// </summary>
/// 

namespace Raj.EC.FinanceModel
{
    public class FBTAssesseeDetailsModel : ClassLibraryMVP.General.IModel
    {
        private IFBTAssesseeDetailsView objIFBTAssesseeDetailsView;
        private DataSet objDS;
        private DAL _objDAL = new DAL();
        private int _userId = UserManager.getUserParam().UserId;

        public FBTAssesseeDetailsModel(IFBTAssesseeDetailsView fbtAssesseeDetailsView)
        {
            objIFBTAssesseeDetailsView = fbtAssesseeDetailsView;
        }

        public DataSet FillValues()
        {
            DataSet ds = new DataSet();

            _objDAL.RunProc("dbo.EC_FA_Mst_FBT_AssesseeType_FillAssesseeTypeValues", ref ds);
            return ds;
        }

        public DataSet FillGrid()
        {
            DataSet ds = new DataSet();
            SqlParameter[] param = { 
                _objDAL.MakeInParams("@Assessee_Type_Id",SqlDbType.Int,0, objIFBTAssesseeDetailsView.Assessee_Type_Id)
            };
            _objDAL.RunProc("dbo.EC_FA_Mst_FBT_AssesseeDetails_FillGrid", param, ref ds);
            return ds;
        }

        public Message Save()
        {
            DataSet ds = new DataSet();
            Message mObj = new Message();

            SqlParameter[] sqlPara = {      
                               
                                _objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                _objDAL.MakeInParams("@Assessee_Type_Id",SqlDbType.Int,0,objIFBTAssesseeDetailsView.Assessee_Type_Id),
                                _objDAL.MakeInParams("@Assessee_Details", SqlDbType.Xml , 0,objIFBTAssesseeDetailsView.SessionAssesseeDetails.GetXml()), 
                                    };

            _objDAL.RunProc("dbo.EC_FA_Mst_FBT_Assessee_Details_Save", sqlPara);

            mObj.messageID = Convert.ToInt32(sqlPara[0].Value);
            mObj.message = Convert.ToString(sqlPara[1].Value);

            if(mObj.messageID==0)
            {
                objIFBTAssesseeDetailsView.ClearVariables();
            }

            return mObj;
        }
         public DataSet ReadValues()
        {
             return objDS;
        }


	}
}
