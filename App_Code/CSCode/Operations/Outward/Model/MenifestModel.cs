using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;

using System.Text;
using System.Net;
using System.IO;

/// <summary>
/// Summary description for MenifestModel
/// </summary>
namespace Raj.EC.OperationModel
{
    public class MenifestModel : IModel
    {
        private IMenifestView objIMenifestView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        private int _branchID = UserManager.getUserParam().MainId;
        private bool _isacctransferreq = UserManager.getUserParam().IsAccTransferReq;
        private int _divisionID = UserManager.getUserParam().DivisionId;
        private string _hierarchycode = UserManager.getUserParam().HierarchyCode;

        public MenifestModel(IMenifestView MenifestView)
        {
            objIMenifestView = MenifestView;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Opr_Menifest_FillValues", ref objDS);
            return objDS;
        }

        public DataSet Fill_ALS_On_Vehicle_Selection()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID),
            objDAL.MakeInParams("@Menifest_Date", SqlDbType.DateTime, 0, objIMenifestView.MenifestDate),
            objDAL.MakeInParams("@Menifest_Id", SqlDbType.Int, 0, objIMenifestView.keyID),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, objIMenifestView.VehicleID),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0, _divisionID),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, _hierarchycode)};

            objDAL.RunProc("dbo.EC_Opr_Menifest_Fill_ALS_On_Vehicle_Selection", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet Fill_ALS_Date_For_Validation()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@ALS_Id", SqlDbType.Int, 0, objIMenifestView.ALSID) };

            objDAL.RunProc("dbo.EC_Opr_Menifest_Fill_ALS_Date", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet Set_SheduledArrival_Date()
        {
            SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Current_Branch_ID", SqlDbType.Int, 0, _branchID),
            objDAL.MakeInParams("@To_Branch_ID", SqlDbType.Int, 0, objIMenifestView.MenifestToID),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, objIMenifestView.VehicleID),
            objDAL.MakeInParams("@Memo_Date", SqlDbType.DateTime, 0, objIMenifestView.MenifestDate)};

            objDAL.RunProc("dbo.EC_Opr_Menifest_Set_Sheduled_Arrival_Date", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet fillMemoDetailsOnALSSelection()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID),
            objDAL.MakeInParams("@ALS_Id", SqlDbType.Int, 0, objIMenifestView.ALSID),
            objDAL.MakeInParams("@Menifest_Id", SqlDbType.Int, 0, objIMenifestView.keyID),
            objDAL.MakeInParams("@MenifestType_Id", SqlDbType.Int, 0, objIMenifestView.MenifestTypeID),
            objDAL.MakeInParams("@Menifest_Date", SqlDbType.DateTime, 0, objIMenifestView.MenifestDate)};

            objDAL.RunProc("dbo.EC_Opr_FillMenifest_Details_On_ALS_Selection", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID),
            objDAL.MakeInParams("@MenifestType_Id", SqlDbType.Int, 0, objIMenifestView.MenifestTypeID),
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionID) ,
            objDAL.MakeInParams("@Menifest_Id", SqlDbType.Int, 0, objIMenifestView.keyID) ,
            objDAL.MakeInParams("@IsAccTransferReq", SqlDbType.Bit, 0, _isacctransferreq) ,
            objDAL.MakeInParams("@GetGCXML", SqlDbType.Xml, 0, objIMenifestView.GetGCNoXML) ,
            objDAL.MakeInParams("@Menifest_Date", SqlDbType.DateTime, 0, objIMenifestView.MenifestDate),
            objDAL.MakeInParams("@IsFrom_Edit", SqlDbType.Bit, 0, objIMenifestView.IsFrom_Edit),
            objDAL.MakeInParams("@CallFrom", SqlDbType.TinyInt, 0, 1)};

            objDAL.RunProc("dbo.EC_Opr_Menifest_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }       
           
        public DataSet gc_grid_validation()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@memo_Id", SqlDbType.Int, 0, objIMenifestView.keyID),
                objDAL.MakeInParams("@memotype_Id", SqlDbType.Int, 0, objIMenifestView.MenifestTypeID),
                objDAL.MakeInParams("@vehicle_Id", SqlDbType.Int, 0, objIMenifestView.VehicleID) ,
            objDAL.MakeInParams("@menifest_toId", SqlDbType.Int, 0, objIMenifestView.MenifestToID) ,
            objDAL.MakeInParams("@GetXML", SqlDbType.Xml, 0, objIMenifestView.MenifestDetailsXML)};

            objDAL.RunProc("dbo.EC_Opr_Menifest_GridGC_Validation", objSqlParam, ref objDS);

            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),                
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_divisionID),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@Memo_Id", SqlDbType.Int, 0,objIMenifestView.keyID),
            objDAL.MakeInParams("@ALS_Id", SqlDbType.Int, 0,objIMenifestView.ALSID),
            objDAL.MakeInParams("@Memo_Date", SqlDbType.DateTime,0,objIMenifestView.MenifestDate),
            objDAL.MakeInParams("@Memo_Branch_Id", SqlDbType.Int,0,_branchID),
            objDAL.MakeInParams("@Memo_Type_Id", SqlDbType.Int, 0,objIMenifestView.MenifestTypeID),
            objDAL.MakeInParams("@To_Branch_Id", SqlDbType.Int, 0,objIMenifestView.MenifestToID),
            objDAL.MakeInParams("@Loaded_By_Id", SqlDbType.Int, 0,objIMenifestView.LoadedById),
            objDAL.MakeInParams("@To_Name", SqlDbType.VarChar, 50,objIMenifestView.MenifestTo),
            objDAL.MakeInParams("@Vehicle_Category_ID", SqlDbType.Int, 0,objIMenifestView.VehicleCotegoryID),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0,objIMenifestView.VehicleID),
            objDAL.MakeInParams("@Total_Actual_Weight", SqlDbType.Decimal, 0,objIMenifestView.Total_ActualWt),
            objDAL.MakeInParams("@Total_To_Pay_Collection", SqlDbType.Decimal, 0,objIMenifestView.Total_ToPayCollection),
            objDAL.MakeInParams("@Booking_Actual_Wt", SqlDbType.Decimal, 0,objIMenifestView.Book_ActualWt),
            objDAL.MakeInParams("@Booking_To_Pay_Collection", SqlDbType.Decimal, 0,objIMenifestView.Book_ToPayCollection),
            objDAL.MakeInParams("@Crossing_Actual_Wt", SqlDbType.Decimal, 0,objIMenifestView.Cros_ActualWt),
            objDAL.MakeInParams("@Crossing_To_Pay_Collection", SqlDbType.Decimal, 0,objIMenifestView.Cros_ToPayCollection),
            objDAL.MakeInParams("@Schedule_Arrival_Delivery_Date", SqlDbType.DateTime, 0,objIMenifestView.ArrivalDeliveryDate),
            objDAL.MakeInParams("@Schedule_Arrival_Delivery_Time", SqlDbType.VarChar, 5,objIMenifestView.ArrivalDeliveryTime),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, UserManager.getUserParam().UserId),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objIMenifestView.Remarks),
            objDAL.MakeInParams("@MemoDetailsXML",SqlDbType.Xml,0,objIMenifestView.MenifestDetailsXML),
            objDAL.MakeInParams("@Memo_No",SqlDbType.Int,0,objIMenifestView.Next_No),
            objDAL.MakeInParams("@Memo_No_For_Print",SqlDbType.VarChar,20,objIMenifestView.MEMO_No),
            objDAL.MakeInParams("@Document_Series_Allocation_ID",SqlDbType.Int,0,objIMenifestView.Document_Series_Allocation_ID)
            };

            objDAL.RunProc("dbo.EC_Opr_Menifest_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);
                        
            if (objMessage.messageID == 0)
            {
                //string shorturl = "";
                //shorturl = SetShortUrl();

                Get_Vehicle_Tracking_SMS(Convert.ToInt32(objSqlParam[2].Value), objIMenifestView.ShortUrl);

                objIMenifestView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                if (objIMenifestView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Outward/FrmMenifest.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIMenifestView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objIMenifestView.Flag == "SaveAndPrint")
                { 
                    //script redirect on same page after clicking on save dialog box.
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
                }

                
            }

            return objMessage;
        }


        public void Get_Vehicle_Tracking_SMS(int MemoID,string ShortUrl)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();

            SqlParameter[] sqlPara = { objdal.MakeInParams("@MemoID", SqlDbType.Int, 0, MemoID),
             objdal.MakeInParams("@ShortUrl", SqlDbType.VarChar , 500, ShortUrl)};
            objdal.RunProc("Ec_Opr_Get_Memo_SMS_MSG", sqlPara, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable SessionDT = (DataTable)ds.Tables[0];

                int i;
                string MobileNo, SMSMessage;

                if (SessionDT.Rows.Count > 0)
                {
                    for (i = 0; i <= SessionDT.Rows.Count - 1; i++)
                    {
                        MobileNo = "";
                        SMSMessage = "";
                        MobileNo = Convert.ToString(SessionDT.Rows[i]["Mobile_No"]);
                        SMSMessage = Convert.ToString(SessionDT.Rows[i]["SMSMessage"]);
                        Sent_Vehicle_Tracking_SMS(MobileNo, SMSMessage);
                    }
                }
            }
        }

        public void Sent_Vehicle_Tracking_SMS(string MobileNo, string SMSMessage)
        {
            string result = "";
            WebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                String sendToPhoneNumber = MobileNo;
                string msg = SMSMessage;

                if (ValidateMobileDetails(sendToPhoneNumber, msg))
                {
                    String userid = "2000126072";
                    String passwd = "Rajan@1234";

                    String url = "http://enterprise.smsgupshup.com/GatewayAPI/rest?userid=" + userid + "&password=" + passwd + "&method=SendMessage&send_to=" + sendToPhoneNumber + "&msg=" + msg + "&msg_type=TEXT&auth_scheme=plain&v=1.1&format=text";

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
