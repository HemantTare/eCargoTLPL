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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EF.MasterView;


/// <summary>
/// Summary description for FamilyRelationModel
/// </summary>
namespace Raj.EF.MasterModel
{
    class FamilyRelationModel : IModel
    {
        private IFamilyRelationView objIFamilyRelationView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public FamilyRelationModel(IFamilyRelationView familyRelationView)
        {
            objIFamilyRelationView = familyRelationView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Family_Relation_Id", SqlDbType.Int, 0, objIFamilyRelationView.keyID) 
                                         };
            objDAL.RunProc("rstil43.EF_Mst_Family_Relation_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }


        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@Family_Relation", SqlDbType.VarChar, 50,objIFamilyRelationView.FamilyRelationName), 
                                               objDAL.MakeInParams("@Family_Relation_Id", SqlDbType.Int, 0, objIFamilyRelationView.keyID ),
                                               objDAL.MakeInParams("@Gender",SqlDbType.VarChar,1,objIFamilyRelationView.Gender_ID),
                                               objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID)
                                         };


            objDAL.RunProc("rstil43.EF_Mst_Family_Relation_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }




    }
}

