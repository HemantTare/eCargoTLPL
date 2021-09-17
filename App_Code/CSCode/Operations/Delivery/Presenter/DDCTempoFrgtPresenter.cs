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
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

namespace Raj.EC.OperationPresenter
{
    public class DDCTempoFrgtPresenter : Presenter
    {
        private IDDCTempoFrgtView objIDDCTempoFrgtView;
        private DDCTempoFrgtModel objDDCTempoFrgtModel;
        private DataSet objDS;

        public DDCTempoFrgtPresenter(IDDCTempoFrgtView DDCTempoFrgtView, bool isPostBack)
        {
            objIDDCTempoFrgtView = DDCTempoFrgtView;
            objDDCTempoFrgtModel = new DDCTempoFrgtModel(objIDDCTempoFrgtView);
            base.Init(objIDDCTempoFrgtView, objDDCTempoFrgtModel);

            if (!isPostBack)
            {
                objIDDCTempoFrgtView.DDCTempoFrgtDate = DateTime.Now.Date;
                objIDDCTempoFrgtView.FromDate = DateTime.Now.Date;
                objIDDCTempoFrgtView.ToDate = DateTime.Now.Date;
                objIDDCTempoFrgtView.ChequeDate = DateTime.Now.Date;
                FillValues();
                initValues();
            }
        }

        public void FillValues()
        {
            objDS = objDDCTempoFrgtModel.FillValues(); 
        }

        public void fillgrid()
        {
            objDS = objDDCTempoFrgtModel.ReadValues();
            objIDDCTempoFrgtView.SessionBindDDCTempoFrgtGrid = objDS.Tables[0];
            if (objIDDCTempoFrgtView.keyID > 0)
            {
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[2].Rows[0];

                    objIDDCTempoFrgtView.Total_No_Of_Records = Util.String2Int(objDR["Total_No_Of_Records"].ToString());
                    objIDDCTempoFrgtView.Total_No_Of_GC = Util.String2Int(objDR["Total_No_Of_GC"].ToString());
                    objIDDCTempoFrgtView.Total_DDC_Articles = Util.String2Int(objDR["Total_Articles"].ToString());
                    objIDDCTempoFrgtView.Total_GC_Amount = Util.String2Decimal(objDR["Total_GC_Amount"].ToString());
                    objIDDCTempoFrgtView.Tempo_Freight_ToBePaid = Util.String2Decimal(objDR["Total_Tempo_Freight_ToBePaid"].ToString());
                    objIDDCTempoFrgtView.Bonus = Util.String2Decimal(objDR["Total_Bonus"].ToString());
                    objIDDCTempoFrgtView.AddTempoFrt = Util.String2Decimal(objDR["Total_AddTempoFrt"].ToString());
                    objIDDCTempoFrgtView.TotalTempoFrgtTBPaid = Util.String2Decimal(objDR["TotalTempoFrgtTBPaid"].ToString());

                    objIDDCTempoFrgtView.Remarks = objDR["Remarks"].ToString();
                }
            }
            
            if (objDS.Tables[1].Rows.Count > 0)
            { 
                DataRow objDR = objDS.Tables[1].Rows[0];
                objIDDCTempoFrgtView.FrgtSettlementTypeID = Util.String2Int(objDR["FreightSettlementId"].ToString());
                objIDDCTempoFrgtView.FrgtSettlementTypeName = objDR["FreightSettlementName"].ToString();
            }

        }

        private void initValues()
        {
            objDS = objDDCTempoFrgtModel.ReadValues();
            objIDDCTempoFrgtView.SessionBindDDCTempoFrgtGrid = objDS.Tables[0];

            if (objIDDCTempoFrgtView.keyID > 0)
            {
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR; 
                    if (objDS.Tables[2].Rows.Count > 0)
                    {
                        objDR = objDS.Tables[2].Rows[0];
                    }
                    else
                    { return; }
                    objIDDCTempoFrgtView.DDCTempoFrgtNo = objDR["Transaction_No_For_Print"].ToString();
                    objIDDCTempoFrgtView.DDCTempoFrgtDate = Convert.ToDateTime(objDR["Transaction_Date"].ToString());
                    objIDDCTempoFrgtView.VehicleID = Util.String2Int(objDR["Vehicle_ID"].ToString());
                    objIDDCTempoFrgtView.Total_No_Of_Records = Util.String2Int(objDR["Total_No_Of_Records"].ToString());
                    objIDDCTempoFrgtView.Total_No_Of_GC = Util.String2Int(objDR["Total_No_Of_GC"].ToString());
                    objIDDCTempoFrgtView.Total_DDC_Articles = Util.String2Int(objDR["Total_Articles"].ToString());
                    objIDDCTempoFrgtView.Total_GC_Amount = Util.String2Decimal(objDR["Total_GC_Amount"].ToString());
                    objIDDCTempoFrgtView.Tempo_Freight_ToBePaid = Util.String2Decimal(objDR["Total_Tempo_Freight_ToBePaid"].ToString());
                    objIDDCTempoFrgtView.Bonus = Util.String2Decimal(objDR["Total_Bonus"].ToString());
                    objIDDCTempoFrgtView.AddTempoFrt = Util.String2Decimal(objDR["Total_AddTempoFrt"].ToString());
                    objIDDCTempoFrgtView.TotalTempoFrgtTBPaid = Util.String2Decimal(objDR["TotalTempoFrgtTBPaid"].ToString());

                    objIDDCTempoFrgtView.Remarks = objDR["Remarks"].ToString();
                    if (objDR["IsCashCheque"].ToString().ToLower() == "false")
                    {
                        objIDDCTempoFrgtView.IsCashCheque = 0;
                    }
                    else
                    {
                        objIDDCTempoFrgtView.IsCashCheque = 1;
                    }


                    //objIDDCTempoFrgtView.IsCashCheque = Util.String2Int(objDR["IsCashCheque"].ToString());
                    objIDDCTempoFrgtView.CashAmount = Util.String2Decimal(objDR["CashAmount"].ToString());
                    objIDDCTempoFrgtView.ChequeAmount = Util.String2Decimal(objDR["ChequeAmount"].ToString());
                    objIDDCTempoFrgtView.ChequeNo = Util.String2Int(objDR["ChequeNo"].ToString());
                    objIDDCTempoFrgtView.ChequeDate = Convert.ToDateTime(objDR["ChequeDate"].ToString());

                }
            }
            if (objDS.Tables[1].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[1].Rows[0];
                objIDDCTempoFrgtView.FrgtSettlementTypeID = Util.String2Int(objDR["FreightSettlementId"].ToString());
                objIDDCTempoFrgtView.FrgtSettlementTypeName = objDR["FreightSettlementName"].ToString();
            }
        }

        public void CheckVehicle()
        {
            objDS = objDDCTempoFrgtModel.CheckVehicleStr();
            DataRow objDR;
            if (objDS.Tables[0].Rows.Count > 0)
            {
                objDR = objDS.Tables[0].Rows[0];
                objIDDCTempoFrgtView.strValidate_Vehicle = objDR["strMessage"].ToString();
            }
            else
            {
                objIDDCTempoFrgtView.strValidate_Vehicle = "";
            }
        }

        public void save()
        {
            base.DBSave();
        }
    }
}