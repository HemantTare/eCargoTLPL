using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;

/// <summary>
/// Summary description for MRDeliveryDetailsPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class MRDeliveryDetailsPresenter : Presenter
    {
        private IMRDeliveryDetailsView ObjIMRDeliveryDetailsView;
        private MRDeliveryDetailsModel ObjMRDeliveryDetailsModel;
        private DataSet objds = new DataSet();

        public MRDeliveryDetailsPresenter(IMRDeliveryDetailsView MRDeliveryDetailsView, bool isPostBack)
        {
            ObjIMRDeliveryDetailsView = MRDeliveryDetailsView;
            ObjMRDeliveryDetailsModel = new MRDeliveryDetailsModel(ObjIMRDeliveryDetailsView);

            base.Init(ObjIMRDeliveryDetailsView, ObjMRDeliveryDetailsModel);

            if (!isPostBack)
            {
                initvalues();             
                
               
            }
        }        
        
        public void initvalues()
        {
            if (ObjIMRDeliveryDetailsView.keyID > 0)
            {

                objds = ObjMRDeliveryDetailsModel.ReadValues();

                if (objds.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objds.Tables[0].Rows[0];
                    ObjIMRDeliveryDetailsView.ThroughMr = DR["Through_MR"].ToString();
                    ObjIMRDeliveryDetailsView.DeliveredToID=Util.String2Int(DR["Delivered_To_ID"].ToString());
                    ObjIMRDeliveryDetailsView.BindDeliveredAgainst();
                    ObjIMRDeliveryDetailsView.DeliveryAgainstID = Util.String2Int(DR["Delivery_Against_Id"].ToString());

                }
            }
        }

    }
}
