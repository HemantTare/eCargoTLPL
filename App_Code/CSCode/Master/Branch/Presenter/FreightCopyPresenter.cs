using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for FreightCopyPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class FreightCopyPresenter : ClassLibraryMVP.General.Presenter
    {

        private IFreightCopyView objIFreightCopyView;
        private FreightCopyModel objFreightCopyModel;
        private DataSet objDS;

        public FreightCopyPresenter(IFreightCopyView freightCopyView, bool isPostBack)
        {
            objIFreightCopyView = freightCopyView;
            objFreightCopyModel = new FreightCopyModel(objIFreightCopyView);
            base.Init(objIFreightCopyView, objFreightCopyModel);

            if (!isPostBack)
            {
                objDS = objFreightCopyModel.FillValues();
                objIFreightCopyView.Bind_ddl_State = objDS.Tables["StateMaster"];
                objIFreightCopyView.Bind_ddl_CopyFromCityID = objDS.Tables["CityMaster"];
                objIFreightCopyView.Bind_ddl_FromCityID = objDS.Tables["CityMaster"];
            }
        }
        public int Save()
        {
          //  objFreightCopyModel.Save();
            //base.DBSave();
            Message objmsg = new Message();
            if (objIFreightCopyView.validateUI())
            {
                objmsg = objFreightCopyModel.Save();
            }
            if (objmsg.message != "")
            {
                objIFreightCopyView.errorMessage = objmsg.message;
            }
            return objmsg.messageID;
        }
    }
}