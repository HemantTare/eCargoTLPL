using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

/// <summary>
/// Summary description for PrintingStationaryPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class PrintingStationaryPresenter : Presenter
    {
        private IPrintingStationaryView objIPrintingStationaryView;
        private PrintingStationaryModel objPrintingStationaryModel;
        private DataSet objDS;

        public PrintingStationaryPresenter(IPrintingStationaryView printingStationaryView, bool isPostBack)
        {
            objIPrintingStationaryView = printingStationaryView;
            objPrintingStationaryModel = new PrintingStationaryModel(objIPrintingStationaryView);
            base.Init(objIPrintingStationaryView, objPrintingStationaryModel);
            if (!isPostBack)
            {
                objDS = objPrintingStationaryModel.FillValues();

                objIPrintingStationaryView.Bind_ddl_DocumentType = objDS.Tables["DocumentType"];
                objIPrintingStationaryView.Bind_dg_GeneratedSeries = objDS.Tables["GeneratedSeriesGrid"];
                initValues();

            }

        }
        public void Save()
        {
            //objPrintingStationaryModel.Save();
            base.DBSave();
        }
        private void initValues()
        {

            if (objIPrintingStationaryView.keyID > 0)
            {
                objDS = objPrintingStationaryModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIPrintingStationaryView.DocumentTypeID = Util.String2Int(DR["Document_Id"].ToString());
                    objIPrintingStationaryView.StartNo = Util.String2Int(DR["Start_No"].ToString());
                    objIPrintingStationaryView.EndNo = Util.String2Int(DR["End_No"].ToString());
                    objIPrintingStationaryView.DateofPrinting = Convert.ToDateTime(DR["Date_Of_Printing"]);
                    objIPrintingStationaryView.SeriesGenerationID = Util.String2Int(DR["Series_Generation_ID"].ToString());
                    FillGridEdit();
                    GetMinMaxNo();
                }
            }
        }
        public bool CheckDuplicate()
        {
            bool is_Duplicate = false;
            is_Duplicate = objPrintingStationaryModel.CheckDuplicate();
            return is_Duplicate;
        }
        public void FillGrid()
        {
            objDS = objPrintingStationaryModel.FillGridAdd();           
            objIPrintingStationaryView.Bind_dg_GeneratedSeries = objDS.Tables[0];

        }
        public void FillGridEdit()
        {
            objDS = objPrintingStationaryModel.FillGrid();
            objIPrintingStationaryView.Bind_dg_GeneratedSeries = objDS.Tables[0];

        }
        public void GetMinMaxNo()
        {
            objPrintingStationaryModel.GetMinMaxNumber();
        }
 
    }
}