using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

/// <summary>
/// Summary description for LHPOPenaltiesPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class LHPOPenaltiesPresenter : Presenter
    {
        private ILHPOPenaltiesView objILHPOPenaltiesView;
        private LHPOPenaltiesModel objLHPOPenaltiesModel;
        private DataSet objDS;

        public LHPOPenaltiesPresenter(ILHPOPenaltiesView lHPOPenaltiesView, bool isPostBack)
        {
            objILHPOPenaltiesView = lHPOPenaltiesView;
            objLHPOPenaltiesModel = new LHPOPenaltiesModel(objILHPOPenaltiesView);
            base.Init(objILHPOPenaltiesView, objLHPOPenaltiesModel);
            if (!isPostBack)
            {
                objDS = objLHPOPenaltiesModel.FillValues();

                // objILHPOHireDetailsView.Bind_ddl_DocumentType = objDS.Tables[0];
                initValues();

            }

        }
        public void Save()
        {
            //objSeriesGenerationModel.Save();
            base.DBSave();
        }
        private void initValues()
        {

            if (objILHPOPenaltiesView.keyID > 0)
            {
                objDS = objLHPOPenaltiesModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    //objILHPOHireDetailsView.DocumentTypeID = Util.String2Int(DR["Document_Id"].ToString());
                    // objILHPOHireDetailsView.StartNo = Util.String2Int(DR["Start_No"].ToString());
                    // objILHPOHireDetailsView.EndNo = Util.String2Int(DR["End_No"].ToString());
                    // objILHPOHireDetailsView.GeneratedDate = Convert.ToDateTime(DR["Generation_Date"]);

                }
            }
        }
    }
}