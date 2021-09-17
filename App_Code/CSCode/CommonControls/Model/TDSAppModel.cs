using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using System.Data.SqlClient;
using Raj.EC.ControlsView;


namespace Raj.EC.ControlsModel
{
    public class TDSAppModel:IModel
    {

        private ITDSAppView objITDSAppView;
        private DAL objDAL = new DAL();
        DataSet objDS = new DataSet();
        

        public TDSAppModel(ITDSAppView _objITDSAppView)
        {
            objITDSAppView = _objITDSAppView;
        }

        public DataSet FillDeducteeType()
        {
            objDAL.RunProc("EC_Mst_Controls_TDSApp_DeducteeType_Fill", ref objDS);
            return objDS;
        }


        public DataSet ReadValues()
        {
            if (objITDSAppView.Call_From == 0)
            {
                objITDSAppView.ID = objITDSAppView.keyID;
            }

            SqlParameter[] sqlpara =
                                    { 
                                   objDAL.MakeInParams("@CallFrom", SqlDbType.Int,0,objITDSAppView.Call_From), 
                                   objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int,0,Common.GetMenuItemId()), 
                                   objDAL.MakeInParams("@ID", SqlDbType.Int, 0,objITDSAppView.ID)
                                    };

            objDAL.RunProc("EC_Mst_Controls_TDSApp_Read",sqlpara, ref objDS);

            return objDS;
        
        }

        public Message Save()
        {
            Message ObjMsg = new Message();


            return ObjMsg;
        
        }
    }
}
