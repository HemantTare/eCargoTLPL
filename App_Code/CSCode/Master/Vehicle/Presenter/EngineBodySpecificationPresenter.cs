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
using Raj.EF.MasterView;
using Raj.EF.MasterModel;

/// <summary>
/// Summary description for EngineBodySpecificationPresenter
/// </summary>
/// 
namespace Raj.EF.MasterPresenter
{
    
public class EngineBodySpecificationPresenter:Presenter 
{
    private IEngineBodySpecificationView objIEngionBodySpecificationView;
    private EngineBodySpecificationModel objEngineBodySpecificationModel;
    private DataSet objDS;

    public EngineBodySpecificationPresenter(IEngineBodySpecificationView engineBodySpecificationView, bool isPostBack)
	{
        objIEngionBodySpecificationView = engineBodySpecificationView;
        objEngineBodySpecificationModel = new EngineBodySpecificationModel(objIEngionBodySpecificationView);
        base.Init(objIEngionBodySpecificationView, objEngineBodySpecificationModel);

        if (!isPostBack)
        {
            fillValues();
            initValues();
        }
	}

    private void fillValues()
    {
        objIEngionBodySpecificationView.BindFuelType = objEngineBodySpecificationModel.FillValues();
    }

    private void initValues()
    {
        objDS = objEngineBodySpecificationModel.ReadValues();
        objIEngionBodySpecificationView.BindEngineBodyGrid = objDS.Tables[1];
        if (objIEngionBodySpecificationView.keyID > 0)
        {
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];
                objIEngionBodySpecificationView.ChasisNo = objDR["Chasis_No"].ToString();
                objIEngionBodySpecificationView.TrollyChasisNo = objDR["Trolly_Chasis_No"].ToString();
                objIEngionBodySpecificationView.EngineNo = objDR["Engine_No"].ToString();
                objIEngionBodySpecificationView.Power = objDR["Power_bhp"].ToString();
                objIEngionBodySpecificationView.PaintCode = objDR["Paint_Code"].ToString();
                objIEngionBodySpecificationView.PaintColor = objDR["Paint_Color"].ToString();
                objIEngionBodySpecificationView.IgnitionKeyCode = objDR["Ignition_Key_Code"].ToString();
                objIEngionBodySpecificationView.DoorKeyCode = objDR["Door_Key_Code"].ToString();

                objIEngionBodySpecificationView.FuelTypeID = Util.String2Int(objDR["Fuel_Type_ID"].ToString());
                objIEngionBodySpecificationView.GrossVehicleWt = Util.String2Int(objDR["Gross_Wt"].ToString());
                objIEngionBodySpecificationView.UnladenWt = Util.String2Int(objDR["Unladen_Wt"].ToString());
                objIEngionBodySpecificationView.VehicleCapacity = Util.String2Int(objDR["Vehicle_Capacity"].ToString());

                objIEngionBodySpecificationView.WheelBase = Util.String2Decimal(objDR["Wheel_Base"].ToString());
                objIEngionBodySpecificationView.Length = Util.String2Decimal(objDR["Length"].ToString());
                objIEngionBodySpecificationView.Height = Util.String2Decimal(objDR["Height"].ToString());
                objIEngionBodySpecificationView.Width = Util.String2Decimal(objDR["Width"].ToString());
                objIEngionBodySpecificationView.FuelTankCapacity = Util.String2Decimal(objDR["Fuel_Tank_Capacity"].ToString());

            }

        }
    }
    
}
}