using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;
/// <summary>
/// Summary description for GCRectificationModel
/// </summary>
namespace Raj.EC.OperationModel
{
    public class GCRectificationModel : IModel
    {
        private IGCRectificationView objIGCRectificationView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        public GCRectificationModel(IGCRectificationView GCRectificationView)
        {
            objIGCRectificationView = GCRectificationView;
        }
        public DataSet ReadValues()
        {
            return objDS;
        }

        public DataSet Get_Company_GC_Parameter()
        {
            objDAL.RunProc("Get_Company_GC_Parameter", ref objDS);
            return objDS;
        }

        public Boolean Allow_To_Rectify()
        {
            Boolean Is_Allow_To_Rectify;

            SqlParameter[] objSqlParam ={ objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                                        objDAL.MakeOutParams("@Is_Allow_To_Rectify", SqlDbType.Bit , 0 ),
                                        objDAL.MakeOutParams("@Rectification_GC_Id", SqlDbType.Int  , 0 ),
                                        objDAL.MakeOutParams("@Is_Octroi_Updated",SqlDbType.Bit,0),
                                        objDAL.MakeOutParams("@Is_Octroi_Applicable",SqlDbType.Bit,0),
                                        objDAL.MakeInParams("@GC_No_For_Print", SqlDbType.VarChar , 0, objIGCRectificationView.GC_No_For_Print),
                                        objDAL.MakeInParams("@Branch_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId) ,
                                        objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,5,UserManager.getUserParam().HierarchyCode) ,
                                        objDAL.MakeInParams("@year_code",SqlDbType.Int,0,UserManager.getUserParam().YearCode  ),
                                        objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,UserManager.getUserParam().DivisionId )};

            objDAL.RunProc("Ec_Opr_GC_Allow_To_Rectify", objSqlParam, ref objDS);

            objIGCRectificationView.errorMessage  = objSqlParam[0].Value.ToString();
            Is_Allow_To_Rectify = Convert.ToBoolean(objSqlParam[1].Value.ToString());

            objIGCRectificationView.Rectification_GC_Id = Convert.ToInt32(objSqlParam[2].Value.ToString());

            objIGCRectificationView.Is_GCRectification_Octroi_Updated = Convert.ToBoolean(objSqlParam[3].Value.ToString());
            objIGCRectificationView.Is_GCRectification_Octroi_Applicable = Convert.ToBoolean(objSqlParam[4].Value.ToString());

            return Is_Allow_To_Rectify;
        }
        public Message Save()
        {
            Message objMessage = new Message();             
            return objMessage;
            
        }
    }
}