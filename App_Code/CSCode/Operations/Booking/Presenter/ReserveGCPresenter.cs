using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;
using ClassLibraryMVP.General;
using ClassLibraryMVP;


/// <summary>
/// Summary description for ReserveGCPresenter
/// </summary>
namespace Raj.EC.OperationPresenter
{
    public class ReserveGCPresenter : Presenter
    {
        private IReserveGCView objIReserveGCView;
        private ReserveGCModel objReserveGCModel;
        private DataSet objDS;

        public ReserveGCPresenter(IReserveGCView ReserveGCView, bool isPostback)
        {
            objIReserveGCView = ReserveGCView;
            objReserveGCModel = new ReserveGCModel(objIReserveGCView);

            base.Init(objIReserveGCView, objReserveGCModel);

            if (!isPostback)
            {
                fillValues();
                Get_Company_GC_Parameter();
            }
        }

        private void fillValues()
        {
            objDS = objReserveGCModel.FillValues();
            objIReserveGCView.BindGCType = objDS.Tables[0];
            objIReserveGCView.BindVA = objDS.Tables[1];
            objIReserveGCView.BindReason = objDS.Tables[2];
        }



        public void Get_Company_GC_Parameter()
        {
            objDS = objReserveGCModel.Get_Company_GC_Parameter();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                objIReserveGCView.GC_No_Length = Util.String2Int(objDR["GC_No_Length"].ToString());
            }
        }


        public void Save()
        {
            //base.DBSave();
            objReserveGCModel.Save();
        }
    }
}

