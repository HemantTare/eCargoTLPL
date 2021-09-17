using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;
 

/// <summary>
/// Summary description for SeriesGenerationPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{
     
    public class SeriesGenerationPresenter:Presenter 
    {
        private ISeriesGenerationView objISeriesGenerationView;
        private SeriesGenerationModel objSeriesGenerationModel;
        private DataSet objDS;


        public SeriesGenerationPresenter(ISeriesGenerationView seriesGenerationView, bool isPostBack)
        {
            objISeriesGenerationView = seriesGenerationView;
            objSeriesGenerationModel = new SeriesGenerationModel(objISeriesGenerationView);
            base.Init(objISeriesGenerationView, objSeriesGenerationModel);           
            if (!isPostBack)
            {
                objDS = objSeriesGenerationModel.FillValues();

                objISeriesGenerationView.Bind_ddl_DocumentType = objDS.Tables[0];
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

            if (objISeriesGenerationView.keyID > 0)
            {
                objDS = objSeriesGenerationModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objISeriesGenerationView.DocumentTypeID = Util.String2Int(DR["Document_Id"].ToString());
                    objISeriesGenerationView.StartNo = Util.String2Int(DR["Start_No"].ToString());
                    objISeriesGenerationView.EndNo = Util.String2Int(DR["End_No"].ToString());
                    objISeriesGenerationView.GeneratedDate = Convert.ToDateTime(DR["Generation_Date"]);
                    GetMinMaxNo();
                }
            }
        }
        public bool CheckDuplicate()
        {
            bool is_Duplicate=false;
            is_Duplicate = objSeriesGenerationModel.CheckDuplicate();
            return is_Duplicate;
        }
        public void GetMinMaxNo()
        {
            objSeriesGenerationModel.GetMinMaxNumber();
        }
    }
}