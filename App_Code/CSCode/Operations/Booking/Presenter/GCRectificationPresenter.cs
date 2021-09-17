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
/// Summary description for GCRectificationPresenter
/// </summary>
namespace Raj.EC.OperationPresenter
{
    public class GCRectificationPresenter : Presenter
    {
        private IGCRectificationView objIGCRectificationView;
        private GCRectificationModel objGCRectificationModel;
        private DataSet objDS;

        public GCRectificationPresenter(IGCRectificationView GCRectificationView, bool isPostback)
        {
            objIGCRectificationView = GCRectificationView;
            objGCRectificationModel = new GCRectificationModel(objIGCRectificationView);

            base.Init(objIGCRectificationView, objGCRectificationModel);

            if (!isPostback)
            {
                Get_Company_GC_Parameter();
            }
        }     
        public Boolean Allow_To_Rectify()
        {
            return objGCRectificationModel.Allow_To_Rectify();
        }

        public void Get_Company_GC_Parameter()
        {
            objDS = objGCRectificationModel.Get_Company_GC_Parameter();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];               
                objIGCRectificationView.GC_No_Length = Util.String2Int(objDR["GC_No_Length"].ToString());
            }
        }      
    }
}

