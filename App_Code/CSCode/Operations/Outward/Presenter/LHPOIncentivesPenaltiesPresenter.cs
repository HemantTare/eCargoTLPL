using System;
using System.Data;

using Raj.EC.OperationView;
using Raj.EC.OperationModel;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Name : Ankit champaneriya
/// Date : 03-11-2008
/// Summary description for LHPOIncentivesPenaltiesPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{
    public class LHPOIncentivesPenaltiesPresenter : Presenter
    {
        private ILHPOIncentivesPenaltiesView _iLHPOIncentivesPenaltiesView;
        private LHPOIncentivesPenaltiesModel _LHPOIncentivesPenaltiesModel;
        private DataSet objDS;
        int _keyID;
        public LHPOIncentivesPenaltiesPresenter(ILHPOIncentivesPenaltiesView iLHPOIncentiviesPenaltiesView, bool isPostBack)
        {
            _iLHPOIncentivesPenaltiesView = iLHPOIncentiviesPenaltiesView;
            _LHPOIncentivesPenaltiesModel = new LHPOIncentivesPenaltiesModel(_iLHPOIncentivesPenaltiesView);
            base.Init(_iLHPOIncentivesPenaltiesView, _LHPOIncentivesPenaltiesModel);
            int LHPOTypeID = 0;
            int LHPOID = 0;

            if (System.Web.HttpContext.Current.Session["SessionLHPOTypeID"] != null && System.Web.HttpContext.Current.Session["SessionLHPOID"] != null)
            {
                LHPOTypeID = (int)System.Web.HttpContext.Current.Session["SessionLHPOTypeID"];
                LHPOID = (int)System.Web.HttpContext.Current.Session["SessionLHPOID"];
            }
            if (LHPOTypeID == 2 && _iLHPOIncentivesPenaltiesView.keyID <= 0)
            {
                _keyID = LHPOID;
            }   
            if (!isPostBack)
            {
                initValues();
            }
        }

        private void fillValues()
        {
            //objDS = _LHPOIncentivesPenaltiesModel.FillValues();
        }

        public void initValues()
        {
            objDS = _LHPOIncentivesPenaltiesModel.ReadValues();
            _iLHPOIncentivesPenaltiesView.SessionBindLHPOIncentiveGrid = objDS.Tables[0];
            _iLHPOIncentivesPenaltiesView.SessionBindLHPOPenaltiesGrid = objDS.Tables[1];
        }
    }
}