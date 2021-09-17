using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;

namespace Raj.EC.OperationModel
{
    public class AUSOtherAgencyUnloadingDetailsModel : IModel
    {
        private IAUSOtherAgencyUnloadingDetailsView objIAUSOtherAgencyUnloadingDetailsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _branchID = UserManager.getUserParam().MainId;

        public AUSOtherAgencyUnloadingDetailsModel(IAUSOtherAgencyUnloadingDetailsView AUSOtherAgencyUnloadingDetailsView)
        {
            objIAUSOtherAgencyUnloadingDetailsView = AUSOtherAgencyUnloadingDetailsView  ;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Opr_AUS_Other_Agency_FillValues", ref objDS);
            return objDS;
        }
        
        public DataSet Get_ToLocationDetails()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@To_Location_Id", SqlDbType.Int, 0, objIAUSOtherAgencyUnloadingDetailsView.ArrivedFromLoacationId ) };
            objDAL.RunProc("Ec_Opr_GC_Get_ToLocation_Details", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet Get_Agency_Ledger()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Agency_ID", SqlDbType.Int, 0, objIAUSOtherAgencyUnloadingDetailsView.AgencyId) };
            objDAL.RunProc("EC_Opr_GC_Other_Agency_Get_Ledger", objSqlParam, ref objDS);
            return objDS;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Actual_Unloading_Sheet_ID", SqlDbType.Int, 0, objIAUSOtherAgencyUnloadingDetailsView.keyID) ,
                                          objDAL.MakeInParams("@Agency_Id", SqlDbType.Int, 0, objIAUSOtherAgencyUnloadingDetailsView.AgencyId) ,
                                          objDAL.MakeInParams("@Agency_Ledger_Id", SqlDbType.Int, 0, objIAUSOtherAgencyUnloadingDetailsView.AgencyLedgerId) ,
                                          objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID) ,
                                          objDAL.MakeInParams("@Arrival_Date_From_Agency", SqlDbType.DateTime , 0, objIAUSOtherAgencyUnloadingDetailsView.AUS_Date)};

            objDAL.RunProc("dbo.EC_Opr_AUS_Other_Agency_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),
            objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0, UserManager.getUserParam().DivisionId  ),
            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0, UserManager.getUserParam().YearCode  ),
            objDAL.MakeInParams("@Un_Loading_Branch_ID",SqlDbType.Int,0, UserManager.getUserParam().MainId  ),
            objDAL.MakeInParams("@Actual_Unloading_Sheet_ID",SqlDbType.Int,0,objIAUSOtherAgencyUnloadingDetailsView.keyID  ),
            objDAL.MakeInParams("@Actual_Unloading_Sheet_No",SqlDbType.Int,0, 0 ),
            objDAL.MakeInParams("@Actual_Unloading_Sheet_No_For_Print",SqlDbType.VarChar,20,objIAUSOtherAgencyUnloadingDetailsView.TURNo   ),
            objDAL.MakeInParams("@Actual_Unloading_Sheet_Date",SqlDbType.DateTime,0,objIAUSOtherAgencyUnloadingDetailsView.AUS_Date  ),
            objDAL.MakeInParams("@Agency_Branch_ID",SqlDbType.Int,0,objIAUSOtherAgencyUnloadingDetailsView.AgencyId),
            objDAL.MakeInParams("@Agency_Ledger_ID",SqlDbType.Int,0,objIAUSOtherAgencyUnloadingDetailsView.AgencyLedgerId   ),
            objDAL.MakeInParams("@Arrived_From_Branch_ID",SqlDbType.Int,0,objIAUSOtherAgencyUnloadingDetailsView.ArrivedFromBranchId ),
            objDAL.MakeInParams("@Arrived_From_Location_ID",SqlDbType.Int,0,objIAUSOtherAgencyUnloadingDetailsView.ArrivedFromLoacationId  ),
            objDAL.MakeInParams("@Vehicle_No",SqlDbType.NVarChar,40, objIAUSOtherAgencyUnloadingDetailsView.VehicleNo),            
            objDAL.MakeInParams("@LHPO_No_For_Print",SqlDbType.NVarChar  ,40,objIAUSOtherAgencyUnloadingDetailsView.LHPO_No_For_Print ),            
            objDAL.MakeInParams("@LHPO_Date",SqlDbType.DateTime,0,objIAUSOtherAgencyUnloadingDetailsView.LHPO_Date  ),
            objDAL.MakeInParams("@Total_Actual_GCs",SqlDbType.Int,0,objIAUSOtherAgencyUnloadingDetailsView.Total_GC  ),
            objDAL.MakeInParams("@Total_Actual_Articles",SqlDbType.Int,0,objIAUSOtherAgencyUnloadingDetailsView.Total_Booking_Articles  ),
            objDAL.MakeInParams("@Total_Actual_Weight",SqlDbType.Decimal,0,objIAUSOtherAgencyUnloadingDetailsView.Total_Booking_Articles_Wt  ),
            objDAL.MakeInParams("@Total_Received_Articles",SqlDbType.Int,0,objIAUSOtherAgencyUnloadingDetailsView.Total_Received_Articles),
            objDAL.MakeInParams("@Total_Received_Weight",SqlDbType.Decimal,0,objIAUSOtherAgencyUnloadingDetailsView.Total_Received_Articles_Wt ),
            objDAL.MakeInParams("@Total_Loaded_Articles",SqlDbType.Int,0,objIAUSOtherAgencyUnloadingDetailsView.Total_Loaded_Articles),
            objDAL.MakeInParams("@Total_Loaded_Weight",SqlDbType.Decimal,0,objIAUSOtherAgencyUnloadingDetailsView.Total_Loaded_Articles_Wt),
            objDAL.MakeInParams("@Lorry_Hire",SqlDbType.Decimal,0,objIAUSOtherAgencyUnloadingDetailsView.Lorry_Hire),
            objDAL.MakeInParams("@Other_Payable",SqlDbType.Decimal,0,objIAUSOtherAgencyUnloadingDetailsView.Other_Payable),
            objDAL.MakeInParams("@Scheduled_Arrival_Date",SqlDbType.DateTime,0,objIAUSOtherAgencyUnloadingDetailsView.ActualArrivalDate),
            objDAL.MakeInParams("@Scheduled_Arrival_Time",SqlDbType.VarChar,5,objIAUSOtherAgencyUnloadingDetailsView.ActualArrivalTime),
            objDAL.MakeInParams("@Vehicle_Arrival_Date",SqlDbType.DateTime,0,objIAUSOtherAgencyUnloadingDetailsView.ActualArrivalDate ),
            objDAL.MakeInParams("@Vehicle_Arrival_Time",SqlDbType.VarChar  ,5,objIAUSOtherAgencyUnloadingDetailsView.ActualArrivalTime),
            objDAL.MakeInParams("@Truck_Unloaded_Time",SqlDbType.VarChar,5,objIAUSOtherAgencyUnloadingDetailsView.UnloadingTime),
            objDAL.MakeInParams("@Reason_For_Late_Unloading_ID",SqlDbType.Int,0,objIAUSOtherAgencyUnloadingDetailsView.Reason_For_Late_Uploading  ),
            objDAL.MakeInParams("@Total_Damaged_Leakage_Articles",SqlDbType.Int,0, objIAUSOtherAgencyUnloadingDetailsView.Total_Damage_Leakage_Articles ),
            objDAL.MakeInParams("@Total_Damaged_Leakage_Value",SqlDbType.Decimal,0,objIAUSOtherAgencyUnloadingDetailsView.Total_Damage_Leakage_Value  ),
            objDAL.MakeInParams("@Unloaded_Supervisor_ID",SqlDbType.Int,0,objIAUSOtherAgencyUnloadingDetailsView.Supervisor ),
            objDAL.MakeInParams("@Remarks",SqlDbType.VarChar,250, objIAUSOtherAgencyUnloadingDetailsView.Remarks ),
            objDAL.MakeInParams("@Unloading_Details_Xml",SqlDbType.Xml,0, objIAUSOtherAgencyUnloadingDetailsView.Unloading_Details_Xml ),
            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0, UserManager.getUserParam().UserId  ),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),                        
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0, Common.GetMenuItemId ())};

            objDAL.RunProc("EC_Opr_AUS_Other_Agency_Save", objSqlParam);
            
            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";
                //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                if (objIAUSOtherAgencyUnloadingDetailsView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Inward/FrmAUSOtherAgency.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIAUSOtherAgencyUnloadingDetailsView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objIAUSOtherAgencyUnloadingDetailsView.Flag == "SaveAndPrint")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
                    //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
            }
            return objMessage;
        }
    }
}