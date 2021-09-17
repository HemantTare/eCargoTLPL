using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

/// <summary>
/// Summary description for DocumentSeriesPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class DocumentSeriesPresenter : Presenter
    {
        private IDocumentSeriesView objIDocumentSeriesView;
        private DocumentSeriesModel objDocumentSeriesModel;
        private DataSet objDS;

        public DocumentSeriesPresenter(IDocumentSeriesView documentSeriesView, bool isPostBack)
        {
            objIDocumentSeriesView = documentSeriesView;
            objDocumentSeriesModel = new DocumentSeriesModel(objIDocumentSeriesView);
            base.Init(objIDocumentSeriesView, objDocumentSeriesModel);
            if (!isPostBack)
            {
                objDS = objDocumentSeriesModel.FillValues();

                objIDocumentSeriesView.Bind_ddl_DocumentType = objDS.Tables["DocumentType"];
                objIDocumentSeriesView.Bind_dg_PrintedSeries = objDS.Tables["PrintedSeriesGrid"];
                initValues();

            }

        }
        public void Save()
        {
            //objDocumentSeriesModel.Save();
            base.DBSave();
        }
        private void initValues()
        {

            if (objIDocumentSeriesView.keyID > 0)
            {
                objDS = objDocumentSeriesModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIDocumentSeriesView.DocumentTypeID = Util.String2Int(DR["Document_Id"].ToString());
                    objIDocumentSeriesView.StartNo = Util.String2Int(DR["Start_No"].ToString());
                    objIDocumentSeriesView.EndNo = Util.String2Int(DR["End_No"].ToString());
                    objIDocumentSeriesView.DateofAllocation = Convert.ToDateTime(DR["Date_Of_Allocation"]);
                    objIDocumentSeriesView.PrintedSeriesID = Util.String2Int(DR["Series_Printing_ID"].ToString());
                    objIDocumentSeriesView.SetBranchID(DR["Branch_Name"].ToString(), DR["Branch_ID"].ToString());
                    objIDocumentSeriesView.BranchID = Util.String2Int(DR["Branch_ID"].ToString());
                    objIDocumentSeriesView.AreaID = Util.String2Int(DR["Area_ID"].ToString());
                    objIDocumentSeriesView.RegionID = Util.String2Int(DR["Region_ID"].ToString());
                    objIDocumentSeriesView.Is_HO = Util.String2Bool(DR["Is_HO"].ToString());

                    FillVA();
                    FillGridEdit();
                    GetMinMaxNo();
                }
            }
        }
        public bool CheckDuplicate()
        {
            bool is_Duplicate = false;
            is_Duplicate = objDocumentSeriesModel.CheckDuplicate();
            return is_Duplicate;
        }
        public void FillGrid()
        {
            objDS = objDocumentSeriesModel.FillGridAdd();
            objIDocumentSeriesView.Bind_dg_PrintedSeries = objDS.Tables[0];

        }
        public void FillGridEdit()
        {
            objDS = objDocumentSeriesModel.FillGrid();
            objIDocumentSeriesView.Bind_dg_PrintedSeries = objDS.Tables[0];

        }
        public void GetMinMaxNo()
        {
            objDocumentSeriesModel.GetMinMaxNumber();
        }
        public void FillVA()
        {
            objDS = objDocumentSeriesModel.FillVA();
            objIDocumentSeriesView.Bind_ddl_VA = objDS.Tables[0];

        }
        
    }
}