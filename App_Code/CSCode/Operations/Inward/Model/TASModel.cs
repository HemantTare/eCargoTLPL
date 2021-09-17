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

/// <summary>
/// Author       : Anita Gupta
/// Description  : Truck Arrival System Model
/// Date         : 16 Jan 09 
/// </summary>
/// 

namespace Raj.EC.OperationModel
{
    public class TASModel : IModel
    { 
        private ITASView objITASView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        private int _userId = UserManager.getUserParam().UserId;
        private int _divisionId = UserManager.getUserParam().DivisionId;

        public TASModel(ITASView TASView)
        {
            objITASView = TASView;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Opr_TAS_FillValues", ref objDS);
            return objDS;
        }


        public void Get_VehicleDetails()
        {
            SqlParameter[] objSqlParam ={  
                                    objDAL.MakeOutParams("@Vehicle_Category_ID", SqlDbType.VarChar , 20 ),
                                    objDAL.MakeOutParams("@Vehicle_Category", SqlDbType.VarChar , 20 ),                                                          
                                    objDAL.MakeOutParams("@NoofMinuteDifferenceForLate", SqlDbType.Int , 0 ),  
                                    objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objITASView.VehicleSearchView.VehicleID  )};
            
            objDAL.RunProc("EC_Opr_TAS_Get_Vehicle_Details", objSqlParam, ref objDS);

            objITASView.Vehicle_Category_Id =   Util.String2Int( objSqlParam[0].Value.ToString());
            objITASView.Vehicle_Category    =   objSqlParam[1].Value.ToString();
            objITASView.NoofMinuteDifferenceForLate = Util.String2Int(objSqlParam[2].Value.ToString());
        }
      
        public DataSet Get_LHPO()
        {
            SqlParameter[] objSqlParam ={
                                    objDAL.MakeInParams("@TAS_Branch_ID", SqlDbType.Int  , 0, UserManager.getUserParam().MainId  ),                                    
                                    objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objITASView.VehicleSearchView.VehicleID  ),
                                    objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionId),
                                    objDAL.MakeInParams("@TAS_Date", SqlDbType.DateTime  , 0, objITASView.TAS_Date  )};

            objDAL.RunProc("EC_opr_TAS_Get_LHPO", objSqlParam, ref objDS);
            
            return objDS;
        }


        public DataSet Get_LHPODetails()
        {
            SqlParameter[] objSqlParam ={   objDAL.MakeOutParams("@LHPO_Date", SqlDbType.VarChar  , 20 ) ,
                                            objDAL.MakeOutParams("@Schedule_Arrival_Delivery_Date", SqlDbType.VarChar  , 20 ) ,
                                            objDAL.MakeOutParams("@Schedule_Arrival_Delivery_Time", SqlDbType.VarChar  , 20 ) ,
                                            objDAL.MakeInParams("@TAS_Branch_ID", SqlDbType.Int  , 0, UserManager.getUserParam().MainId  ),                                    
                                            objDAL.MakeInParams("@LHPO_Id", SqlDbType.Int, 0, objITASView.LHPO_Id  ),
                                            objDAL.MakeInParams("@menuitem_id",SqlDbType.Int,0,objITASView.MenuItemId),
                                            objDAL.MakeOutParams("@LHPOFromLocation",SqlDbType.VarChar,20),
                                            objDAL.MakeOutParams("@LHPOToLocation",SqlDbType.VarChar,20)
                                            
                                        };
            
            objDAL.RunProc("EC_opr_TAS_Get_Memo_GC_Details", objSqlParam, ref objDS);

            objITASView.LHPO_Date  = objSqlParam[0].Value.ToString();
            objITASView.ScheduledArrivalDate = objSqlParam[1].Value.ToString();
            objITASView.ScheduledArrivalTime = objSqlParam[2].Value.ToString();
            objITASView.LHPOFromLocation = objSqlParam[6].Value.ToString();
            objITASView.LHPOToLocation = objSqlParam[7].Value.ToString();

            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={
                objDAL.MakeInParams("@TAS_ID", SqlDbType.Int, 0, objITASView.keyID) };


            objDAL.RunProc("dbo.EC_Opr_TAS_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }
         
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),
            objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,_divisionId),
            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@TAS_Branch_ID",SqlDbType.Int,0,UserManager.getUserParam().MainId),
            objDAL.MakeInParams("@TAS_ID",SqlDbType.Int,0,objITASView.keyID),
            objDAL.MakeInParams("@TAS_No",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@TAS_No_For_Print",SqlDbType.VarChar,20,objITASView.TASNo),
            objDAL.MakeInParams("@TAS_Date",SqlDbType.DateTime,0,objITASView.TAS_Date),
            objDAL.MakeInParams("@TAS_Time",SqlDbType.VarChar,8,objITASView.TAS_Time),
            objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,objITASView.Vehicle_Id),
            objDAL.MakeInParams("@AUS_ID",SqlDbType.Int,0,0),
            objDAL.MakeInParams("@Scheduled_Arrival_Date",SqlDbType.DateTime,0,objITASView.ScheduledArrivalDate),
            objDAL.MakeInParams("@Scheduled_Arrival_Time",SqlDbType.VarChar,8,objITASView.ScheduledArrivalTime),
            objDAL.MakeInParams("@Reason_For_Late_Arrival_ID",SqlDbType.Int,0,objITASView.Reason_For_Late_Arrival),
            objDAL.MakeInParams("@Remarks",SqlDbType.VarChar,250,objITASView.Remarks),
            objDAL.MakeInParams("@Is_Cancelled",SqlDbType.Bit,0,0),
            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,UserManager.getUserParam().UserId),
            objDAL.MakeInParams("@LHPO_ID",SqlDbType.Int,0,objITASView.LHPO_Id),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, UserManager.getUserParam().HierarchyCode),                       
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0, Common.GetMenuItemId ()),
            objDAL.MakeInParams("@TAS_Details_Xml",SqlDbType.Xml,0, objITASView.TAS_Details_Xml)
            };

            objDAL.RunProc("EC_Opr_TAS_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);


            if (objMessage.messageID == 0)
            {
                objITASView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                if (objITASView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Inward/FrmTAS.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objITASView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objITASView.Flag == "SaveAndPrint")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
                }
            }

            return objMessage;
        }

    }
}


