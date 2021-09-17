using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for TransitDaysPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class TransitDaysPresenter:ClassLibraryMVP.General.Presenter 
    {
        private ITransitDaysView objITransitDaysView;
        private TransitDaysModel objTransitDaysModel;
        private DataSet objDS;

        public TransitDaysPresenter(ITransitDaysView transitDaysView, bool isPostBack)
        {
            objITransitDaysView = transitDaysView;
            objTransitDaysModel = new TransitDaysModel(objITransitDaysView);
            base.Init(objITransitDaysView, objTransitDaysModel);

            if (!isPostBack)
            {
                objDS = objTransitDaysModel.FillValues();
                objITransitDaysView.Bind_ddl_City = objDS.Tables["CityMaster"];
                objITransitDaysView.Bind_ddl_State = objDS.Tables["StateMaster"];
                objITransitDaysView.Bind_ddl_Vehicle = objDS.Tables["VehicleTypeMaster"];
                objITransitDaysView.Bind_dg_TransitDays = objDS.Tables["TransitDaysGrid"];
                initValues();
            }

        }
        private void initValues()
        {
            if (objITransitDaysView.keyID > 0)
            {
                objDS = objTransitDaysModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    //objITransitDaysView.StateID = objDR["Template_Name"];
                    //objITransitDaysView.VehicleID = objDR["Template_Description"];
                }
            }
        }

        public void Save()
        {
           // objTransitDaysModel.Save();
           //base.DBSave();

           Message objMsg = new Message();

           if (objITransitDaysView.validateUI())
           {
               objMsg = objTransitDaysModel.Save();
           }
           if (objMsg.message != "")
           {
               objITransitDaysView.errorMessage = objMsg.message;
           }
        }
        public void FillGrid()
        {
            objDS = objTransitDaysModel.FillGrid();
            objITransitDaysView.Bind_dg_TransitDays = objDS.Tables[0];
        }
        
    }
}