using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

/// <summary>
/// Summary description for LHPOAlertsBranchesPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class LHPOAlertsBranchesPresenter : Presenter
    {
        private ILHPOAlertsBranchesView objILHPOAlertsBranchesView;
        private LHPOAlertsBranchesModel objLHPOAlertsBranchesModel;
        private DataSet objDS;
        private int _keyID;
        public LHPOAlertsBranchesPresenter(ILHPOAlertsBranchesView lHPOAlertsBranchesView, bool isPostBack)
        {
            objILHPOAlertsBranchesView = lHPOAlertsBranchesView;
            objLHPOAlertsBranchesModel = new LHPOAlertsBranchesModel(objILHPOAlertsBranchesView);
            base.Init(objILHPOAlertsBranchesView, objLHPOAlertsBranchesModel);
            int LHPOTypeID = 0;
            int LHPOID = 0;

            if (System.Web.HttpContext.Current.Session["SessionLHPOTypeID"] != null && System.Web.HttpContext.Current.Session["SessionLHPOID"] != null)
            {
                LHPOTypeID = (int)System.Web.HttpContext.Current.Session["SessionLHPOTypeID"];
                LHPOID = (int)System.Web.HttpContext.Current.Session["SessionLHPOID"];
            }
            if (LHPOTypeID == 2 && objILHPOAlertsBranchesView.keyID <= 0)
            {
                _keyID = LHPOID;
            }   
            if (!isPostBack)
            {
                FillGridOnAttachedLHPONoChanged();
            }
        }
        public void Save()
        {
            base.DBSave();
        }       
        public  void FillGridOnAttachedLHPONoChanged()
        {
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();
            objDS = objLHPOAlertsBranchesModel.FillValues();
            ds.Tables.Add(objDS.Tables["AlertBranchGrid"].Copy());
            ds1.Tables.Add(objDS.Tables["ATHGrid"].Copy());           
            objILHPOAlertsBranchesView.SessionAlertBranchesGrid = ds;
            objILHPOAlertsBranchesView.Bind_dg_AlertBranches = ds;
            objILHPOAlertsBranchesView.SessionATHDetailsGrid = ds1;
            objILHPOAlertsBranchesView.Bind_dg_ATHDetails = ds1;  
        }      
    }
}