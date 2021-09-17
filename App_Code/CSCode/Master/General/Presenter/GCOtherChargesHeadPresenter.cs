using System;
using System.Data;

using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Created :Ankit champaneriya
/// Date    : 18/12/08 
/// Summary description for GCOtherChargesHeadPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{
    public class GCOtherChargesHeadPresenter : ClassLibraryMVP.General.Presenter 
    {
        private IGCOtherChargesHeadView objIGCOtherChargesHeadView;
        private GCOtherChargesHeadModel objGCOtherChargesHeadModel;
        private DataSet objDS;

        public GCOtherChargesHeadPresenter(IGCOtherChargesHeadView GCOtherChargesHeadView,bool isPostBack)
        {
            objIGCOtherChargesHeadView = GCOtherChargesHeadView;
            objGCOtherChargesHeadModel = new GCOtherChargesHeadModel(objIGCOtherChargesHeadView);
            base.Init(objIGCOtherChargesHeadView, objGCOtherChargesHeadModel);

            if (!isPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            if (objIGCOtherChargesHeadView.keyID > 0)
            {
                ReadValues();
            }
        }

        public void ReadValues()
        {
            objDS = objGCOtherChargesHeadModel.ReadValues();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                objIGCOtherChargesHeadView.GC_Other_Charges_Head = Convert.ToString(objDS.Tables[0].Rows[0]["GC_Other_Charge_Head"]);
            }
        }

        public void save()
        {
            base.DBSave();
        }
    }
}