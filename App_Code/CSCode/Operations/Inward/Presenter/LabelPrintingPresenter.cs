using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;
/// <summary>
/// Summary description for LabelPrintingPresenter
/// </summary>
namespace Raj.EC.OperationPresenter
{
    public class LabelPrintingPresenter : Presenter
    {
        private ILabelPrintingView objILabelPrintingView;
        private LabelPrintingModel objLabelPrintingModel;
        private DataSet objDS;
        private DataSet DS = new DataSet();

        public LabelPrintingPresenter(ILabelPrintingView LabelPrintingView, bool isPostback)
        {
            objILabelPrintingView = LabelPrintingView;
            objLabelPrintingModel = new LabelPrintingModel(objILabelPrintingView);
            base.Init(objILabelPrintingView, objLabelPrintingModel);

            if (!isPostback)
            { 
                
                initValues();
            }
        }
       

        public void fillgrid()
        {
            objDS = objLabelPrintingModel.ReadValues();
            objILabelPrintingView.SessionBindLabelPrintingGrid = objDS.Tables[0];
        }

        //public DataSet gc_grid_validation()
        //{
        //    objDS = objLabelPrintingModel.gc_grid_validation();
        //    return objDS;
        //}
 

        private void initValues()
        {
           
            objDS = objLabelPrintingModel.ReadValues();            
            objILabelPrintingView.SessionBindLabelPrintingGrid = objDS.Tables[0];

             
 
        }

        public void Save()
        {
            base.DBSave();
        }
    }
}
