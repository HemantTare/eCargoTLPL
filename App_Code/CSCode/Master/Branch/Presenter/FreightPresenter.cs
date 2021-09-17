using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;



/// <summary>
/// Summary description for FreightPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class FreightPresenter:ClassLibraryMVP.General.Presenter 
    {
        private IFreightView objIFreightView;
        private FreightModel objFreightModel;
        private DataSet objDS;

        public FreightPresenter(IFreightView freightView, bool isPostBack)
        {
            objIFreightView = freightView;
            objFreightModel = new FreightModel(objIFreightView);
            base.Init(objIFreightView, objFreightModel);

            if (!isPostBack)
            {
                objDS = objFreightModel.FillValues();
                objIFreightView.Bind_ddl_City = objDS.Tables["CityMaster"];
                objIFreightView.Bind_ddl_State = objDS.Tables["StateMaster"];
                objIFreightView.Bind_dg_Freight = objDS.Tables["FreightGrid"];
                
            }

        }        
        public void Save()
        {
            //objFreightModel.Save();
            //base.DBSave();

            Message objMsg = new Message();

            if (objIFreightView.validateUI())
            {
                objMsg = objFreightModel.Save();
            }
            if (objMsg.message != "")
            {
                objIFreightView.errorMessage = objMsg.message;
            }
        }
        public void FillGrid()
        {
            objDS = objFreightModel.FillGrid();
            objIFreightView.Bind_dg_Freight = objDS.Tables[0];
        }
    }
}