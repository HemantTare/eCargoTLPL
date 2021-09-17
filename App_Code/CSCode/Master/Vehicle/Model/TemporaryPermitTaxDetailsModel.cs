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

public class TemporaryPermitTaxDetailsModel:IModel 
{
    private ITemporaryPermitTaxDetailsView objITemporaryPermitTaxDetailsView;
    private DAL objDAL = new DAL();
    private DataSet objDS;
    
    
    public TemporaryPermitTaxDetailsModel(ITemporaryPermitTaxDetailsView objTemporaryPermitTaxDetailsView)
	{
        objITemporaryPermitTaxDetailsView = objTemporaryPermitTaxDetailsView;
	}
    public DataSet Fill_State()
    {
        SqlParameter[] sqlParam ={ objDAL.MakeInParams("@KeyId", SqlDbType.Int, 0,objITemporaryPermitTaxDetailsView.keyID) };
        objDAL.RunProc("rstil22.EF_Mst_State_FillValues", sqlParam, ref objDS);
        return objDS;
    }
    public DataSet ReadValues()
    {
        return objDS;
    }
    public Message Save()
    {
        Message objMessage = new Message();
        return objMessage;
    }
}
