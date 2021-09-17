using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;
/// <summary>
/// Summary description for ReBookGCModel
/// </summary>
namespace Raj.EC.OperationModel
{
    public class ReBookGCModel : IModel
    {
        private IReBookGCView objIReBookGCView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        
        public ReBookGCModel(IReBookGCView ReBookGCView)
        {
            objIReBookGCView = ReBookGCView;
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
        public Boolean Allow_To_ReBook()
        {
            Boolean Is_Allow_To_ReBook;

            SqlParameter[] objSqlParam ={ 
                                        objDAL.MakeOutParams("@Is_Allow_To_ReBook", SqlDbType.Bit , 0 ),
                                        objDAL.MakeOutParams("@ReBook_GC_Id", SqlDbType.Int  , 0 ),
                                        objDAL.MakeOutParams("@Is_Octroi_Updated",SqlDbType.Bit,0),
                                        objDAL.MakeOutParams("@Is_Octroi_Applicable",SqlDbType.Bit,0),
                                        objDAL.MakeInParams("@GC_No_For_Print", SqlDbType.VarChar , 0, objIReBookGCView.GC_No_For_Print),
                                        objDAL.MakeInParams("@Branch_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId) ,
                                        objDAL.MakeInParams("@year_code",SqlDbType.Int,0,UserManager.getUserParam().YearCode  ),
                                        objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,UserManager.getUserParam().DivisionId )};

            objDAL.RunProc("Ec_Opr_GC_Allow_To_ReBook", objSqlParam, ref objDS);

            Is_Allow_To_ReBook = Convert.ToBoolean(objSqlParam[0].Value.ToString());
            objIReBookGCView.ReBook_GC_Id = Convert.ToInt32  (objSqlParam[1].Value.ToString());

            objIReBookGCView.Is_ReBookGC_Octroi_Updated  = Convert.ToBoolean(objSqlParam[2].Value.ToString());
            objIReBookGCView.Is_ReBookGC_Octroi_Applicable  = Convert.ToBoolean(objSqlParam[3].Value.ToString());
            return Is_Allow_To_ReBook;
        }

        public Message Save()
        {
            Message objMessage = new Message();             
            return objMessage;            
        }
    }
}