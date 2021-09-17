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
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;
using System.Text;
using System.Net;
using System.IO;


/// <summary>
/// Summary description for AUSModel
/// </summary>
/// 
namespace Raj.EC.OperationModel
{
    public class AUSModel : ClassLibraryMVP.General.IModel    
    {

        private IAUSView objIAUSView;
        private DAL objDAL = new DAL();
 

        private int _userId = Convert.ToInt32(UserManager.getUserParam().UserId);

        public AUSModel(IAUSView AUSView)
        {
            objIAUSView = AUSView;
        }

        public DataSet ReadValues()
        {

            DataSet DS = null;
            return DS;
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
            objDAL.MakeInParams("@Actual_Unloading_Sheet_ID",SqlDbType.Int,0,objIAUSView.keyID  ),
            objDAL.MakeInParams("@Actual_Unloading_Sheet_No",SqlDbType.Int,0, 0 ),
            objDAL.MakeInParams("@Actual_Unloading_Sheet_No_For_Print",SqlDbType.VarChar,20,objIAUSView.AUSUnloadingDetailsView.TURNo   ),
            objDAL.MakeInParams("@Actual_Unloading_Sheet_Date",SqlDbType.DateTime,0,objIAUSView.AUSUnloadingDetailsView.AUS_Date  ),
            objDAL.MakeInParams("@Manual_TUR_No",SqlDbType.NVarChar,40,objIAUSView.AUSUnloadingDetailsView.Manual_TUR_No ),
            objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,objIAUSView.AUSUnloadingDetailsView.VehicleSearchView.VehicleID  ),
            objDAL.MakeInParams("@Vehicle_Type_ID",SqlDbType.Int,0,objIAUSView.AUSUnloadingDetailsView.Vehicle_Category_Id  ),
            objDAL.MakeInParams("@LHPO_ID",SqlDbType.Int,0, objIAUSView.AUSUnloadingDetailsView.LHPO_Id ),
            objDAL.MakeInParams("@Total_Actual_GCs",SqlDbType.Int,0,objIAUSView.AUSUnloadingDetailsView.Total_GC  ),
            objDAL.MakeInParams("@Total_Actual_Weight",SqlDbType.Decimal,0,objIAUSView.AUSUnloadingDetailsView.Total_Booking_Articles_Wt  ),
            objDAL.MakeInParams("@Total_Received_Weight",SqlDbType.Decimal,0,objIAUSView.AUSUnloadingDetailsView.Total_Received_Articles_Wt  ),
            objDAL.MakeInParams("@Total_Actual_Articles",SqlDbType.Int,0,objIAUSView.AUSUnloadingDetailsView.Total_Booking_Articles  ),
            objDAL.MakeInParams("@Total_Received_Articles",SqlDbType.Int,0,objIAUSView.AUSUnloadingDetailsView.Total_Received_Articles),
            objDAL.MakeInParams("@Scheduled_Arrival_Date",SqlDbType.DateTime,0,objIAUSView.AUSUnloadingDetailsView.ScheduledArivalDate ),
            objDAL.MakeInParams("@Scheduled_Arrival_Time",SqlDbType.VarChar,0,objIAUSView.AUSUnloadingDetailsView.ScheduledArivalTime),
            objDAL.MakeInParams("@Vehicle_Arrival_Date",SqlDbType.DateTime,0,objIAUSView.AUSUnloadingDetailsView.TASDate),
            objDAL.MakeInParams("@Vehicle_Arrival_Time",SqlDbType.VarChar  ,0,objIAUSView.AUSUnloadingDetailsView.TASTime),
            objDAL.MakeInParams("@Truck_Unloaded_Time",SqlDbType.VarChar,0,objIAUSView.AUSUnloadingDetailsView.UnloadingTime  ),
            objDAL.MakeInParams("@Reason_For_Late_Arrival_ID",SqlDbType.Int,0,objIAUSView.AUSUnloadingDetailsView.Reason_For_Late_Arrival  ),
            objDAL.MakeInParams("@Reason_For_Late_Unloading_ID",SqlDbType.Int,0,objIAUSView.AUSUnloadingDetailsView.Reason_For_Late_Uploading  ),
            objDAL.MakeInParams("@Total_Short_Articles",SqlDbType.Int,0,objIAUSView.AUSUnloadingDetailsView.TotalShortArticlesValue  ),
            objDAL.MakeInParams("@Total_Excess_Articles",SqlDbType.Int,0,objIAUSView.AUSUnloadingDetailsView.TotalExcessArticlesValue  ),
            objDAL.MakeInParams("@Total_Damaged_Leakage_Articles",SqlDbType.Int,0, objIAUSView.AUSUnloadingDetailsView.Total_Damage_Leakage_Articles ),
            objDAL.MakeInParams("@Total_Damaged_Leakage_Value",SqlDbType.Decimal,0,objIAUSView.AUSUnloadingDetailsView.Total_Damage_Leakage_Value  ),

            objDAL.MakeInParams("@Total_Additional_Freight",SqlDbType.Decimal,0,objIAUSView.AUSUnloadingDetailsView.Total_Additional_Freight  ),
            objDAL.MakeInParams("@Total_Delivery_Commision",SqlDbType.Decimal,0,objIAUSView.AUSUnloadingDetailsView.Total_Delivery_Commision  ),
            objDAL.MakeInParams("@Total_To_Pay_Collection",SqlDbType.Decimal,0,objIAUSView.AUSUnloadingDetailsView.Total_To_Pay_Collection    ),
            objDAL.MakeInParams("@Lorry_Hire",SqlDbType.Decimal,0,objIAUSView.AUSUnloadingDetailsView.Lorry_Hire   ),
            objDAL.MakeInParams("@Other_Receavable_Charges",SqlDbType.Decimal,0,objIAUSView.AUSUnloadingDetailsView.Other_Receavable ),
            objDAL.MakeInParams("@Other_Payable_Charges",SqlDbType.Decimal,0,objIAUSView.AUSUnloadingDetailsView.Other_Payable  ),
            objDAL.MakeInParams("@Total_Receable",SqlDbType.Decimal,0,objIAUSView.AUSUnloadingDetailsView.Total_Receable  ),
            objDAL.MakeInParams("@Total_Payable",SqlDbType.Decimal,0,objIAUSView.AUSUnloadingDetailsView.Total_Payable  ),

            objDAL.MakeInParams("@Unloaded_Supervisor_ID",SqlDbType.Int,0,objIAUSView.AUSUnloadingDetailsView.Supervisor ),
            objDAL.MakeInParams("@Remarks",SqlDbType.VarChar,250, objIAUSView.AUSUnloadingDetailsView.Remarks ),
            objDAL.MakeInParams("@Unloading_Details_Xml",SqlDbType.Xml,0, objIAUSView.AUSUnloadingDetailsView.Unloading_Details_Xml ),
            objDAL.MakeInParams("@Excess_Details_Xml",SqlDbType.Xml,0, objIAUSView.AUSExcessDetailsView.Excess_Details_Xml  ),
            objDAL.MakeInParams("@Is_Cancelled",SqlDbType.Bit,0,0),
            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0, UserManager.getUserParam().UserId  ),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, UserManager.getUserParam().HierarchyCode),                        
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0, Common.GetMenuItemId ()),
            objDAL.MakeInParams("@TAS_ID",SqlDbType.Int,0,objIAUSView.AUSUnloadingDetailsView.TAS_Id),
            objDAL.MakeInParams("@UpCountry_Receivable",SqlDbType.Decimal,0,objIAUSView.AUSUnloadingDetailsView.UpCountryReceivable),
            objDAL.MakeInParams("@UpCountry_Crossing_Cost",SqlDbType.Decimal,0,objIAUSView.AUSUnloadingDetailsView.UpcountryCrossingCost)
            };

            objDAL.RunProc("EC_Opr_AUS_Save", objSqlParam);
           

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);
           

            if (objMessage.messageID == 0)
            {
               
                string _Msg;
                _Msg = "Saved SuccessFully";

                if (objIAUSView.keyID == -1)
                {
                    get_GCIDs_AUSID(Convert.ToInt32(objSqlParam[2].Value));
                }
                objIAUSView.ClearVariables();
                if (objIAUSView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Inward/FrmAUS.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIAUSView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objIAUSView.Flag == "SaveAndPrint")
                {
                    string AUSTYPE = "";
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID) + "&AUSTYPE=" + ClassLibraryMVP.Util.EncryptString(AUSTYPE)));
                }

            }

            return objMessage;
        }

        public void PrintLabel()
        {

            string _Msg;
            _Msg = null;

            int MenuItemId = Common.GetMenuItemId();
            string Mode = HttpContext.Current.Request.QueryString["Mode"];
            int Document_ID = objIAUSView.keyID;
            string AUSTYPE = "label";
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID) + "&AUSTYPE=" + ClassLibraryMVP.Util.EncryptString(AUSTYPE)));

        }

        public void get_GCIDs_AUSID(int AUSID)
        {
            DataTable SessionDT = null;

            SessionDT = objIAUSView.AUSUnloadingDetailsView.SessionUnloadingDetailsGrid;
            
            int i, DocumentID;
            DocumentID = AUSID;
            if (SessionDT.Rows.Count > 0)
            {
                for (i = 0; i <= SessionDT.Rows.Count - 1; i++)
                {
                    int GC_ID = 0;
                    GC_ID = Convert.ToInt32(SessionDT.Rows[i]["GC_ID"]);
                    Get_Consignor_Conginee_SMS(GC_ID, DocumentID);
                }
            }
        }

        public void Get_Consignor_Conginee_SMS(int GC_ID, int DocumentID)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();
            int MenuItemId = Common.GetMenuItemId();
            SqlParameter[] sqlPara = { objdal.MakeInParams("@GC_ID", SqlDbType.Int, 0, GC_ID)
             ,objdal.MakeInParams("@MenuItemID", SqlDbType.Int, 0, MenuItemId)
             ,objdal.MakeInParams("@DocumentID", SqlDbType.Int, 0, DocumentID) };

            objdal.RunProc("Ec_Opr_Get_Consignor_Conginee_SMS_MSG", sqlPara, ref ds);


            if (ds.Tables[1].Rows.Count > 0)
            {
                string result = "";
                WebRequest request = null;
                HttpWebResponse response = null;
                try
                {

                    String sendToPhoneNumber = ds.Tables[1].Rows[0]["Consignee_Mobile_No"].ToString();
                    string msg = ds.Tables[1].Rows[0]["MsgConsignee"].ToString();

                    if (ValidateMobileDetails(sendToPhoneNumber, msg))
                    {

                        String userid = "2000126072";
                        String passwd = "Rajan@1234";


                        String url = "http://enterprise.smsgupshup.com/GatewayAPI/rest?userid=" + userid + "&password=" + passwd + "&method=SendMessage&send_to=" + sendToPhoneNumber + "&msg=" + msg + "&msg_type=TEXT&auth_scheme=plain&v=1.1&format=text";
                        //String url = "http://raj.com/GatewayAPI/rest?userid=" + userid + "&password=" + passwd + "&method=SendMessage&send_to=" + sendToPhoneNumber + "&msg=" + msg + "&msg_type=TEXT&auth_scheme=plain&v=1.1&format=text";

                        request = WebRequest.Create(url);

                        response = (HttpWebResponse)request.GetResponse();
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                        }
                        Stream stream = response.GetResponseStream();
                        Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                        StreamReader reader = new System.IO.StreamReader(stream, ec);
                        result = reader.ReadToEnd();
                        Console.WriteLine(result);
                        reader.Close();
                        stream.Close();
                    }
                }
                catch (Exception exp)
                {
                    string excep = exp.ToString();
                }
                finally
                {
                    if (response != null)
                        response.Close();
                }
            }
             

        }
        public bool ValidateMobileDetails(String sendToPhoneNumber, string msg)
        {
            bool Is_Valid = false;
            if (sendToPhoneNumber == "0" || msg == "")
            {
                Is_Valid = false;
            }
            else
            {
                Is_Valid = true;
            }

            return Is_Valid;
        }
    }
}