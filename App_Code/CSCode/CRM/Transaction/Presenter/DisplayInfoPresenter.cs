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
using Raj.CRM.TransactionsView;
using Raj.CRM.TransactionsModel;
//using Raj.eCargo.Init;
using System.Globalization;

namespace Raj.CRM.TransactionsPresenter
{
    public class DisplayInfoPresenter
    {
        private IDisplayInfoView objIDisplayInfoView;
        private DisplayInfoModel objDisplayInfoModel;
        private DataSet objDS;
        //private bool _IsVT = (bool)UserManager.getUserParam().Is_VT;
        public DisplayInfoPresenter(IDisplayInfoView DisplayInfoView, bool IsPostBack)
        {
            objIDisplayInfoView = DisplayInfoView;
            objDisplayInfoModel = new DisplayInfoModel(objIDisplayInfoView);
            if (!IsPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            fillValues();
        }

        private void fillValues()
        {
            if (objIDisplayInfoView.Type == "TicketInfo")
            {
                objDS = objDisplayInfoModel.FillTicketInfo();
                objIDisplayInfoView.bind_dg_DisplayInfo = objDS.Tables[0];
            }
            else if (objIDisplayInfoView.Type == "ProfileUserInfo")
            {
                objDS = objDisplayInfoModel.FillProfileUserInfo();
                objIDisplayInfoView.bind_dg_DisplayInfo = objDS.Tables[0];
            }
            else if (objIDisplayInfoView.Type == "UserInfo" || objIDisplayInfoView.Type == "AssignedUserInfo")
            {
                objDS = objDisplayInfoModel.FillUserInfo();
                objIDisplayInfoView.bind_dg_DisplayInfo = objDS.Tables[0];
            }
        }

    }
}
