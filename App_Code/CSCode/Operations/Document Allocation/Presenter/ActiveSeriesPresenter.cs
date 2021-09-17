using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

/// <summary>
/// Summary description for ActiveSeriesPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class ActiveSeriesPresenter : Presenter
    {
        private IActiveSeriesView objIActiveSeriesView;
        private ActiveSeriesModel objActiveSeriesModel;        
        
        private DataSet objDS;

        public ActiveSeriesPresenter(IActiveSeriesView documentSeriesView, bool isPostBack)
        {
            objIActiveSeriesView = documentSeriesView;
            objActiveSeriesModel = new ActiveSeriesModel(objIActiveSeriesView);
            base.Init(objIActiveSeriesView, objActiveSeriesModel);
            if (!isPostBack)
            {
                objDS = objActiveSeriesModel.FillValues();

                objIActiveSeriesView.Bind_ddl_DocumentType = objDS.Tables["DocumentType"];
                objIActiveSeriesView.Bind_dg_DocumentSeries = objDS.Tables["DocumentSeriesGrid"];
                initValues();
            }
        }

        public void Save()
        {
            //objActiveSeriesModel.Save();
            base.DBSave();
        }
        private void initValues()
        {

            if (objIActiveSeriesView.keyID > 0)
            {
                objDS = objActiveSeriesModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIActiveSeriesView.DocumentTypeID = Util.String2Int(DR["Document_Id"].ToString());                   
                    //objIActiveSeriesView.SetBranchID(DR["Branch_Name"].ToString(), DR["Branch_ID"].ToString());
                    FillGrid();                    
                }
            }
        }
        public void FillGrid()
        {
            objDS = objActiveSeriesModel.FillGrid();
            objIActiveSeriesView.Bind_dg_DocumentSeries = objDS.Tables[0];

        }
        public void FillVA()
        {
            objDS = objActiveSeriesModel.FillVA();
            objIActiveSeriesView.Bind_ddl_VA = objDS.Tables[0];

        }
    }
}