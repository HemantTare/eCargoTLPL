using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for PODDeliverySheetPresenter
/// </summary>
public class PODDeliverySheetPresenter:Presenter
{
    private IPODDeliverySheetView objPODDeliveryView;
    private PODDeliverySheetModel objPODDeliverySheetModel;
    private DataSet objDS = new DataSet();
    
    public PODDeliverySheetPresenter(IPODDeliverySheetView PODDeliverySheetView,bool isPostBack)
	{
        objPODDeliveryView = PODDeliverySheetView;
        objPODDeliverySheetModel = new PODDeliverySheetModel(objPODDeliveryView);
        base.Init(objPODDeliveryView, objPODDeliverySheetModel);
        
        if (!isPostBack)
        {
            objPODDeliveryView.PODDeliveryDate = DateTime.Now.Date;
           initvalues();           
        }
	}

    public void initvalues()
    {        
         FillGrid();
         ReadValues();
    }

    public void FillGrid()
    {
        objDS = objPODDeliverySheetModel.ReadValues();
        objPODDeliveryView.SessionPODDeliverySheet = objDS.Tables[0];
    }

    public void ReadValues()
    {
        objDS = objPODDeliverySheetModel.ReadValues();

        if (objPODDeliveryView.keyID > 0)
        {
            if(objDS.Tables[1].Rows.Count > 0)
            {
                DataRow dr = objDS.Tables[1].Rows[0];
                objPODDeliveryView.PODSentByView.SentByID = Util.String2Int(dr["Delivery_Sent_Type_ID"].ToString());
                objPODDeliveryView.PODSentByView.CourierDocketNo = dr["Courier_Docket_No"].ToString();
                objPODDeliveryView.PODSentByView.CourierName = dr["Courier_Name"].ToString();
                objPODDeliveryView.PODSentByView.VehicleID = Util.String2Int(dr["Vehicle_ID"].ToString());
                objPODDeliveryView.PODDeliverySheetNo = dr["POD_Delivery_Sheet_No_For_Print"].ToString();
                objPODDeliveryView.Remark = dr["Remarks"].ToString();
                objPODDeliveryView.PODDeliveryDate = Convert.ToDateTime(dr["POD_Delivery_Sheet_Date"]);
                objPODDeliveryView.PODSentByView.SetEmployeeId(dr["Emp_Name"].ToString(), dr["Emp_ID"].ToString());
                objPODDeliveryView.PODDeliveredTo = dr["POD_Delivered_To"].ToString();
            }
        }
    }

    public void Save()
    {
        base.DBSave();
    }
}
