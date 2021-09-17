using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for TransitDaysStateToStatePresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class TransitDaysStateToStatePresenter : ClassLibraryMVP.General.Presenter
    {

        private ITransitDaysStateToStateView objITransitDaysStateToStateView;
        private TransitDaysStateToStateModel objTransitDaysStateToStateModel;
        private DataSet objDS;

        public TransitDaysStateToStatePresenter(ITransitDaysStateToStateView transitDaysStateToStateView, bool isPostBack)
        {
            objITransitDaysStateToStateView = transitDaysStateToStateView;
            objTransitDaysStateToStateModel = new TransitDaysStateToStateModel(objITransitDaysStateToStateView);
            base.Init(objITransitDaysStateToStateView, objTransitDaysStateToStateModel);

            if (!isPostBack)
            {
                objDS = objTransitDaysStateToStateModel.FillValues();
                objITransitDaysStateToStateView.Bind_ddl_FromState = objDS.Tables["StateMaster"];
                objITransitDaysStateToStateView.Bind_ddl_ToState = objDS.Tables["StateMaster"];
                objITransitDaysStateToStateView.Bind_ddl_Vehicle = objDS.Tables["VehicleTypeMaster"];                           
            }
        }
        public int Save()
        {
            //objTransitDaysStateToStateModel.Save();
            
            //base.DBSave();


            Message objmsg = new Message();
            if (objITransitDaysStateToStateView.validateUI())
            {
                objmsg = objTransitDaysStateToStateModel.Save();
            }
            if (objmsg.message != "")
            {
                objITransitDaysStateToStateView.errorMessage = objmsg.message;
            }

            return objmsg.messageID;
        }
 
    }
}