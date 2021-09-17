using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

/// <summary>
/// Summary description for LHPOAttachedBranchPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{
    public class LHPOAttachedBranchPresenter : Presenter
    {
        private ILHPOAttachedBranchView objILHPOAttachedBranchView;
        private LHPOAttachedBranchModel objLHPOAttachedBranchModel;
        private DataSet objDS;
        private int _keyID;
        public LHPOAttachedBranchPresenter(ILHPOAttachedBranchView lHPOAttachedBranchView, bool isPostBack)
        {
            objILHPOAttachedBranchView = lHPOAttachedBranchView;
            objLHPOAttachedBranchModel = new LHPOAttachedBranchModel(objILHPOAttachedBranchView);
            base.Init(objILHPOAttachedBranchView, objLHPOAttachedBranchModel);
            int LHPOTypeID = 0;
            int LHPOID = 0;

            if (System.Web.HttpContext.Current.Session["SessionLHPOTypeID"] != null && System.Web.HttpContext.Current.Session["SessionLHPOID"] != null)
            {
                LHPOTypeID = (int)System.Web.HttpContext.Current.Session["SessionLHPOTypeID"];
                LHPOID = (int)System.Web.HttpContext.Current.Session["SessionLHPOID"];
            }
            if (LHPOTypeID == 2 && objILHPOAttachedBranchView.keyID <= 0)
            {
                _keyID = LHPOID;

            }
            if (!isPostBack)
            {
                FillGrid();
            }
        }
        public void Save()
        {
            base.DBSave();
        }
        public void FillGrid()
        {
            objDS = objLHPOAttachedBranchModel.FillGrid();
            objILHPOAttachedBranchView.SessionAttachedLHPOBranchesGrid = objDS;
            objILHPOAttachedBranchView.Bind_dg_AttachedLHPOBranches = objDS;
        }
    }
}