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
using ClassLibraryMVP.DataAccess ;
using ClassLibraryMVP.General;
using Raj.EC.MasterView;
 
//using Raj.eCargo.Init; 


namespace  Raj.EC.MasterModel
{
    /// <summary>
    /// Summary description for PetrolPumpGeneralModel
    /// </summary>
    public class PetrolPumpGeneralModel : ClassLibraryMVP.General.IModel    
    {
        private IPetrolPumpGeneralView objIPetrolPumpGeneralView;
        private DataSet _ds;
        private DAL _objDAL = new DAL();
            
     

        public PetrolPumpGeneralModel(IPetrolPumpGeneralView PetrolPumpGeneralView)
        {
            objIPetrolPumpGeneralView = PetrolPumpGeneralView;
        }

        public DataSet ReadValues()
        {

            SqlParameter[] objSqlParam ={ _objDAL.MakeInParams("@Petrol_Pump_ID", SqlDbType.Int, 0, objIPetrolPumpGeneralView.keyID) };


            _objDAL.RunProc("dbo.EC_Mst_Petrol_Pump_ReadValues", objSqlParam, ref _ds);

            return _ds;


        }

        public Message Save()
        {
            Message objMsg = new Message();

             
         
            return objMsg;

        }

        public DataSet Fill_Values()
        {
                        
            _objDAL.RunProc("EC_Mst_Petrol_Pump_General_FillValues",  ref _ds);            
            
            return _ds;
        }






 


         

    }
}