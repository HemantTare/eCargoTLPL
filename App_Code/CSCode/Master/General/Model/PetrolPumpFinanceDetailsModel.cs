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
 


namespace  Raj.EC.MasterModel
{
    /// <summary>
    /// Summary description for PetrolPumpFinanceDetailsModel
    /// </summary>
    public class PetrolPumpFinanceDetailsModel : ClassLibraryMVP.General.IModel    
    {
        private IPetrolPumpFinanceDetailsView objIPetrolPumpFinanceDetailsView;
        private DataSet _ds;
        private DAL _objDAL = new DAL();
              
   
        public PetrolPumpFinanceDetailsModel(IPetrolPumpFinanceDetailsView PetrolPumpFinanceDetailsView)
        {
            objIPetrolPumpFinanceDetailsView = PetrolPumpFinanceDetailsView;
        }

        public DataSet ReadValues()
        {

            SqlParameter[] objSqlParam ={ _objDAL.MakeInParams("@Petrol_Pump_ID", SqlDbType.Int, 0, objIPetrolPumpFinanceDetailsView.keyID) };


            _objDAL.RunProc("dbo.EC_Mst_Petrol_Pump_ReadValues", objSqlParam, ref _ds);

            return _ds;

           
        }





        public DataSet ReadDivisionsValues()
        {

            SqlParameter[] objSqlParam ={ _objDAL.MakeInParams("@Petrol_Pump_ID", SqlDbType.Int, 0, objIPetrolPumpFinanceDetailsView.keyID) };


            _objDAL.RunProc("dbo.EC_Mst_Petrol_Pump_ReadDivisionsValues", objSqlParam, ref _ds);

            return _ds;


        }

        public Message Save()
        {
            Message objMsg = new Message();

            
         
            return objMsg;

        }

        public DataSet Fill_Values()
        {
                        
            _objDAL.RunProc("EC_Mst_Petrol_Pump_Finance_Details_FillValues",  ref _ds);            
            
            return _ds;
        }


        public DataSet Fill_Ledger()
        {
            SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@LedgerGroupId", SqlDbType.Int, 0, objIPetrolPumpFinanceDetailsView.LedgerGroupId  ) };


            _objDAL.RunProc("EC_Mst_Petrol_Pump_Finance_Details_FillLedger", objSqlParam, ref _ds);

            return _ds;
        }


         public DataSet Get_LedgerDetails()
        {
            SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@LedgerId", SqlDbType.Int, 0, objIPetrolPumpFinanceDetailsView.LedgerId  ) };


            _objDAL.RunProc("EC_Mst_Petrol_Pump_Finance_Details_Get_Ledger_Details", objSqlParam, ref _ds);

            return _ds;
        } 

       
         
         
       


       

    }
}