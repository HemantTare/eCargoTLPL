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

public class RegistrationFitnessModel:IModel 
{
    private IRegistrationFitnessView objIRegistrationFitnessView;
    private DAL objDAL = new DAL();
    private DataSet objDS;
 
   // private int _userID = UserManager.getUserParam().UserId;
    public RegistrationFitnessModel(IRegistrationFitnessView RegistrationFitnessView)
	{
        objIRegistrationFitnessView = RegistrationFitnessView;
	}

    public DataTable FillValues ()
    {
        SqlParameter[] sqlParam ={ objDAL.MakeInParams("@KeyId", SqlDbType.Int, 0, objIRegistrationFitnessView.keyID) };
        objDAL.RunProc("rstil22.EF_Mst_State_FillValues",sqlParam,ref objDS);
        return objDS.Tables[0];
    }

    public DataTable FillRTO()
    {
        SqlParameter[] sqlParam ={ objDAL.MakeInParams("@State_Id", SqlDbType.Int, 0,objIRegistrationFitnessView.RegistrationStateID) };
        objDAL.RunProc("[rstil22].[EF_Mst_FillRtoAuthorisedPlaceOnStateChanged]", sqlParam, ref objDS);
        return objDS.Tables[0];

    }

    public DataSet ReadValues()
    {
        SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objIRegistrationFitnessView.keyID) 
                                              };
        objDAL.RunProc("rstil7.EF_Master_Vehicle_ReadValues", objSqlParam, ref objDS);
        return objDS;
    }

    public Message Save()
    {
        Message objMessage = new Message();

        return objMessage;
    }

}
