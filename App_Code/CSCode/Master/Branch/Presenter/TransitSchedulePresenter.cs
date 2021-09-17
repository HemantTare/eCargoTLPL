using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for TransitSchedulePresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class TransitSchedulePresenter:ClassLibraryMVP.General.Presenter 
    {
        private ITransitScheduleView objITransitScheduleView;
        private TransitScheduleModel objTransitScheduleModel;
        private DataSet objDS;

        public TransitSchedulePresenter(ITransitScheduleView transitScheduleView, bool isPostBack)
        {
            objITransitScheduleView = transitScheduleView;
            objTransitScheduleModel = new TransitScheduleModel(objITransitScheduleView);
            base.Init(objITransitScheduleView, objTransitScheduleModel);

            if (!isPostBack)
            {
                objDS = objTransitScheduleModel.FillValues();
                objITransitScheduleView.Bind_ddl_FromState = objDS.Tables["StateMaster"];
                objITransitScheduleView.Bind_ddl_ToState = objDS.Tables["StateMaster"];
                objITransitScheduleView.Bind_ddl_Vehicle = objDS.Tables["VehicleTypeMaster"];
                //objITransitDaysView.Bind_dg_TransitDays = objDS.Tables["TransitDaysGrid"];
               
            }

        }
        public void Save()
        {
            //objTransitScheduleModel.Save();
            base.DBSave();
        }
        public void FillGrid(int FromStateId,int ToStateId,int VehicleTypeId)
        {   
            objDS=objTransitScheduleModel.Get_Transit_Schedule(FromStateId,ToStateId,VehicleTypeId);
            objITransitScheduleView.Bind_dg_TransitSchedule=objDS;
        }
    }
}