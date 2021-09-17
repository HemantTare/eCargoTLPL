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

namespace Raj.EF.MasterView
{
    public class RegistrationPermitModel:IModel 
    {
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private IRegistrationPermitView objIRegistrationPermitView;
        public RegistrationPermitModel(IRegistrationPermitView RegistrationPermitView)
        {
            objIRegistrationPermitView = RegistrationPermitView;

        }
       
        public DataSet FillValues()
        {
            SqlParameter[] sqlParam ={ objDAL.MakeInParams("@KeyId", SqlDbType.Int, 0, objIRegistrationPermitView.keyID) };
            objDAL.RunProc("[rstil22].[EF_Mst_RegistrationPermit_FillValues]", sqlParam, ref objDS);
            return objDS;
        }
        //public DataTable GetRtoAuthorisedPlace()
        //{
        //    SqlParameter[] sqlParam ={ objDAL.MakeInParams("@State_Id", SqlDbType.Int, 0, objIRegistrationPermitView.RegistrationStateId) };
        //    objDAL.RunProc("[rstil22].[EF_Mst_FillRtoAuthorisedPlaceOnStateChanged]",sqlParam,ref objDS);
        //    return objDS.Tables[0];
            
        //}
       
        public DataSet BindGridRegistrationPermit()
        {
            SqlParameter[] sqlParam ={ objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,objIRegistrationPermitView.keyID),
                                       objDAL.MakeInParams("@Type",SqlDbType.VarChar,50,"Permit")};

            objDAL.RunProc("[rstil22].[EF_Mst_Registration_Permit_ReadValues ]", sqlParam, ref objDS);
            return objDS;
        }
        public DataSet BindGridTempararyRegistrationPermit()
        {
            SqlParameter[] sqlParam ={ objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, objIRegistrationPermitView.keyID),
                                        objDAL.MakeInParams("@Type",SqlDbType.VarChar,50,"TempararyRegistrationPermit")};

            objDAL.RunProc("[rstil22].[EF_Mst_Registration_Permit_ReadValues]", sqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objIRegistrationPermitView.keyID) 
                                              };
            objDAL.RunProc("rstil7.EF_Master_Vehicle_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public bool ValidateDuplicatePermitNo()
        {
            SqlParameter[] objSqlParam = {  objDAL.MakeOutParams("@Is_Duplicate_Permit_No",SqlDbType.Bit,0),
                                            objDAL.MakeInParams("@Permit_No", SqlDbType.NVarChar, 50, objIRegistrationPermitView.PermitNo),
                                            objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,objIRegistrationPermitView.keyID),
                                            objDAL.MakeInParams("@PermitNoXML",SqlDbType.Xml,0,objIRegistrationPermitView.VehicleTemporaryPermitDetailsXML.ToLower())
                                         };
            objDAL.RunProc("[rstil7].[EF_Master_Vehicle_Is_Duplicate_Permit_No]", objSqlParam, ref objDS);
            return Util.String2Bool(objSqlParam[0].Value.ToString());

        }

        public Message Save()
        {
            Message objMessage = new Message();


            return objMessage;
        }
    }
}
