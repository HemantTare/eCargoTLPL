using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

/// <summary>
/// Summary description for LHPOPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class LHPOPresenter : Presenter
    {
        private ILHPOView objILHPOView;
        private LHPOModel objLHPOModel;
        private DataSet objDS;

        public LHPOPresenter(ILHPOView lHPOView, bool isPostBack)
        {
            objILHPOView = lHPOView;
            objLHPOModel = new LHPOModel(objILHPOView);
            base.Init(objILHPOView, objLHPOModel);
            if (!isPostBack)
            {
                objDS = objLHPOModel.FillValues();
                // objILHPOHireDetailsView.Bind_ddl_DocumentType = objDS.Tables[0];
                initValues();

            }

        }
        public void Save()
        {
            if (MenuItemID == 198)
            {
                objLHPOModel.SaveLHPORectification();
            }
            else
            {
                base.DBSave();
               // objLHPOModel.Save();
            }
        }
        private void initValues()
        {

            if (objILHPOView.keyID > 0)
            {
                objDS = objLHPOModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    //objILHPOHireDetailsView.DocumentTypeID = Util.String2Int(DR["Document_Id"].ToString());
                    // objILHPOHireDetailsView.StartNo = Util.String2Int(DR["Start_No"].ToString());
                    // objILHPOHireDetailsView.EndNo = Util.String2Int(DR["End_No"].ToString());
                    // objILHPOHireDetailsView.GeneratedDate = Convert.ToDateTime(DR["Generation_Date"]);
                   // objILHPOView.LHPOAlertsBranchesView.TotalAdvance = Util.String2Decimal(DR["Total_Advance_To_Be_Paid"].ToString());
                }
            }
        }
    }
}