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
/// Summary description for ReBookGCPresenter
/// </summary>
namespace Raj.EC.OperationPresenter
{
    public class ReBookGCPresenter : Presenter
    {
        private IReBookGCView objIReBookGCView;
        private ReBookGCModel objReBookGCModel;
        private DataSet objDS;

        public ReBookGCPresenter(IReBookGCView ReBookGCView, bool isPostback)
        {
            objIReBookGCView = ReBookGCView;
            objReBookGCModel = new ReBookGCModel(objIReBookGCView);

            base.Init(objIReBookGCView, objReBookGCModel);

            if (!isPostback)
            {
                Get_Company_GC_Parameter();
            }
        }

        public void Get_Company_GC_Parameter()
        {
            objDS = objReBookGCModel.Get_Company_GC_Parameter();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                objIReBookGCView.GC_No_Length = Util.String2Int(objDS.Tables[0].Rows[0]["GC_No_Length"].ToString());
            }
        }

        public bool Allow_To_ReBook()
        {
            return objReBookGCModel.Allow_To_ReBook();
        }       
    }
}

