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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC;
using System.Text;
using System.Net;
using System.IO;
//using ClassLibrary;


/// <summary> 
/// Common Class throughout the application
/// Here,All Common Procedures Will be called used all over the Application
/// </summary>
/// 

namespace Raj.EC
{
    public class Common
    {

        private DAL _objDal = new DAL();
        private DataSet _objDS = null;

        private static string _AUTO_SERIES_NAME = "AUTO SERIES";

        public Common()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static DataSet GetOneRowDs(string textColumnName, string valueColumnName, string text, string value)
        {
            DataSet objDS = new DataSet();
            DataTable objDT = new DataTable();
            DataRow objDR = null;
            objDT.Columns.Add(textColumnName);
            objDT.Columns.Add(valueColumnName);
            objDR = objDT.NewRow();
            objDR[textColumnName] = text;
            objDR[valueColumnName] = value;
            objDT.Rows.Add(objDR);
            objDS.Tables.Add(objDT);
            return objDS;
        }

        public static void SetValueToDDLSearch(string text, string value, ClassLibrary.UIControl.DDLSearch objDDLSearch)
        {
            //ListBox list = (ListBox)objDDLSearch.Controls[2];
            //list.Items.Clear();
            //list.Items.Add(new ListItem(text, value));

            objDDLSearch.DataSource = null;
            objDDLSearch.DataBind();

            objDDLSearch.DataSource = GetOneRowDs(objDDLSearch.DataTextField, objDDLSearch.DataValueField, text, value);
            objDDLSearch.DataBind();
            objDDLSearch.SelectedValue = value;
        }

        public string getBaseURL()
        {
            string urlString;

            urlString = "http://" + System.Web.HttpContext.Current.Request.Url.Host;
            if (System.Web.HttpContext.Current.Request.Url.IsDefaultPort == false)
            {
                urlString = urlString + ":" + System.Web.HttpContext.Current.Request.Url.Port;
            }

            urlString = urlString + System.Web.HttpContext.Current.Request.ApplicationPath;
            return urlString;

        }

        public DataSet FillCRMInboxGrid(String Type)
        {
            SqlParameter[] sqlpara = {
                _objDal.MakeInParams("@User_Id", SqlDbType.Int, 0, UserManager.getUserParam().UserId),
                _objDal.MakeInParams("@Year_Code", SqlDbType.Int, 0, UserManager.getUserParam().YearCode),
                _objDal.MakeInParams("@Type", SqlDbType.VarChar, 100, Type)};

            _objDal.RunProc("EC_CRM_Fill_Inbox_Grid", sqlpara, ref _objDS);
            return _objDS;
        }

        public DataSet FillDetailsInGrid(int MenuItemId, string ColName, string SearchText, Boolean IsPageLoad)
        {

            int MenuHeadId = StateManager.GetState<int>("MenuHeadId");

            //pankaj 23 oct 08 //FOR Fleet related form in ecargo
            Boolean IsFleetMenuIneCargo;
            SqlParameter[] sqlPara1 = {
                                        _objDal.MakeOutParams("@IsFleetMenuIneCargo", SqlDbType.Bit, 0),
                                        _objDal.MakeInParams("@MenuItemId", SqlDbType.Int, 0, MenuItemId)
                                        
                                     };
            _objDal.RunProc("EC_Menu_IsFleetMenuIneCargo", sqlPara1);
            IsFleetMenuIneCargo = Convert.ToBoolean(sqlPara1[0].Value);

            if (IsFleetMenuIneCargo == true)
            {
                //FOR Fleet related form in ecargo
                SqlParameter[] sqlPara = {
                                        _objDal.MakeInParams("@MenuItemId", SqlDbType.Int, 0, MenuItemId), 
                                        _objDal.MakeInParams("@ColName", SqlDbType.VarChar, 100, ColName), 
                                        _objDal.MakeInParams("@SearchText", SqlDbType.VarChar, 50, SearchText), 
                                        _objDal.MakeInParams("@IsPageLoad", SqlDbType.Bit, 1, IsPageLoad),
                                         _objDal.MakeInParams("@HierarchyCode", SqlDbType.VarChar, 50, UserManager.getUserParam().HierarchyCode), 
                                        _objDal.MakeInParams("@MainId", SqlDbType.Int, 0, UserManager.getUserParam().MainId)
                                     };
                _objDal.RunProc("rstil43.EF_Mst_Fill_details_In_Grid", sqlPara, ref _objDS);

            }//pankaj 23 oct 08
            else if (MenuHeadId == 1)
            {
                //FOR ADMINISTRATION
                SqlParameter[] sqlPara = {
                                        _objDal.MakeInParams("@MenuItemId", SqlDbType.Int, 0, MenuItemId), 
                                        _objDal.MakeInParams("@ColName", SqlDbType.VarChar, 100, ColName), 
                                        _objDal.MakeInParams("@SearchText", SqlDbType.VarChar, 50, SearchText), 
                                        _objDal.MakeInParams("@IsPageLoad", SqlDbType.Bit, 1, IsPageLoad),
                                         _objDal.MakeInParams("@HierarchyCode", SqlDbType.VarChar, 50, UserManager.getUserParam().HierarchyCode), 
                                        _objDal.MakeInParams("@MainId", SqlDbType.Int, 0, UserManager.getUserParam().MainId)
                                     };
                _objDal.RunProc("EC_Admin_Fill_details_In_Grid", sqlPara, ref _objDS);

            }
            else if (MenuHeadId == 2)
            {
                //FOR MASTERS
                SqlParameter[] sqlPara = { 
                                        _objDal.MakeInParams("@MenuItemId", SqlDbType.Int, 0, MenuItemId), 
                                        _objDal.MakeInParams("@ColName", SqlDbType.VarChar, 100, ColName), 
                                        _objDal.MakeInParams("@SearchText", SqlDbType.VarChar, 50, SearchText), 
                                        _objDal.MakeInParams("@IsPageLoad", SqlDbType.Bit, 1, IsPageLoad),
                                        _objDal.MakeInParams("@HierarchyCode", SqlDbType.VarChar, 50, UserManager.getUserParam().HierarchyCode), 
                                        _objDal.MakeInParams("@MainId", SqlDbType.Int, 0, UserManager.getUserParam().MainId),
                                        _objDal.MakeInParams("@DivisionId", SqlDbType.Int, 0, UserManager.getUserParam().DivisionId)
                                     };
                _objDal.RunProc("EC_Mst_Fill_details_In_Grid", sqlPara, ref _objDS);

            }

            else if (MenuHeadId == 3)
            {
                //FOR OPERATIONS
                DateTime FromDate = StateManager.GetState<DateTime>("FromDate");
                DateTime ToDate = StateManager.GetState<DateTime>("ToDate");
                // DivisionID - Added by Ankit 11-02-09 : 6.00 pm
                SqlParameter[] sqlPara = {  
                                        _objDal.MakeInParams("@MenuItemId", SqlDbType.Int, 0, MenuItemId), 
                                        _objDal.MakeInParams("@ColName", SqlDbType.VarChar, 100, ColName), 
                                        _objDal.MakeInParams("@SearchText", SqlDbType.VarChar, 50, SearchText), 
                                        _objDal.MakeInParams("@IsPageLoad", SqlDbType.Bit, 1, IsPageLoad),
                                         _objDal.MakeInParams("@HierarchyCode", SqlDbType.VarChar, 50, UserManager.getUserParam().HierarchyCode), 
                                        _objDal.MakeInParams("@MainId", SqlDbType.Int, 0, UserManager.getUserParam().MainId),
                                        _objDal.MakeInParams("@YearCode", SqlDbType.Int, 0, UserManager.getUserParam().YearCode),
                                        _objDal.MakeInParams("@FromDate", SqlDbType.DateTime, 0, FromDate),
                                        _objDal.MakeInParams("@ToDate", SqlDbType.DateTime, 0, ToDate),
                                        _objDal.MakeInParams("@Division_ID",SqlDbType.Int ,0,UserManager.getUserParam().DivisionId )
                                     } ;
                _objDal.RunProc("EC_Opr_Fill_details_In_Grid", sqlPara, ref _objDS);

            }
            else if (MenuHeadId == 4)
            {
                DateTime FromDate;
                DateTime ToDate;
                if (StateManager.IsValidSession("FromDate"))
                {
                    FromDate = StateManager.GetState<DateTime>("FromDate");
                }
                else
                {
                    FromDate = DateTime.Now;
                }

                if (StateManager.IsValidSession("ToDate"))
                {
                    ToDate = StateManager.GetState<DateTime>("ToDate");
                }
                else
                {
                    ToDate = DateTime.Now;
                }


                int qrStringValue = -1;
                if (StateManager.IsValidSession("QueryString"))
                {
                    qrStringValue = Util.String2Int(StateManager.GetState<string>("QueryString"));
                }

                //FOR MASTERS
                SqlParameter[] sqlPara = { 
                                        _objDal.MakeInParams("@MenuItemId", SqlDbType.Int, 0, MenuItemId),
                                        _objDal.MakeInParams("@MenuItemCode",SqlDbType.Int,0,GetMenuItemCode(MenuItemId)),
                                        _objDal.MakeInParams("@ColName", SqlDbType.VarChar, 100, ColName), 
                                        _objDal.MakeInParams("@SearchText", SqlDbType.VarChar, 50, SearchText), 
                                        _objDal.MakeInParams("@IsPageLoad", SqlDbType.Bit, 1, IsPageLoad),
                                        _objDal.MakeInParams("@HierarchyCode", SqlDbType.VarChar, 50, UserManager.getUserParam().HierarchyCode),
                                        _objDal.MakeInParams("@MainId", SqlDbType.Int, 0, UserManager.getUserParam().MainId),
                                        _objDal.MakeInParams("@DivisionId", SqlDbType.Int, 0, UserManager.getUserParam().DivisionId),
                                        _objDal.MakeInParams("@FromDate", SqlDbType.DateTime, 0, FromDate),
                                        _objDal.MakeInParams("@ToDate", SqlDbType.DateTime, 0, ToDate),
                                        _objDal.MakeInParams("@YearCode", SqlDbType.Int, 0, UserManager.getUserParam().YearCode),
                                        // Added by harshal on 08-01-10
                                        _objDal.MakeInParams("@IdValue", SqlDbType.Int, 0,GetMenuQueryString(MenuItemId)),
                                        // Commented by harshal on 08-01-10
                                        //_objDal.MakeInParams("@IdValue", SqlDbType.Int, 0, Util.String2Int(GetMenuQueryString(MenuItemId))),
                                        };
                _objDal.RunProc("EC_FA_Fill_details_In_Grid", sqlPara, ref _objDS);
            }

            else if (MenuHeadId == 6)
            {
                //shiv 22 dec 08 //FOR CRM related form

                DateTime FromDate;
                DateTime ToDate;
                if (StateManager.IsValidSession("FromDate"))
                {
                    FromDate = StateManager.GetState<DateTime>("FromDate");
                }
                else
                {
                    FromDate = DateTime.Now;
                }

                if (StateManager.IsValidSession("ToDate"))
                {
                    ToDate = StateManager.GetState<DateTime>("ToDate");
                }
                else
                {
                    ToDate = DateTime.Now;
                }

                SqlParameter[] sqlPara = { 
                                        _objDal.MakeInParams("@MenuItemId", SqlDbType.Int, 0, MenuItemId), 
                                        _objDal.MakeInParams("@ColName", SqlDbType.VarChar, 100, ColName), 
                                        _objDal.MakeInParams("@SearchText", SqlDbType.VarChar, 50, SearchText), 
                                        _objDal.MakeInParams("@IsPageLoad", SqlDbType.Bit, 1, IsPageLoad),
                                         _objDal.MakeInParams("@HierarchyCode", SqlDbType.VarChar, 50, UserManager.getUserParam().HierarchyCode), 
                                        _objDal.MakeInParams("@MainId", SqlDbType.Int, 0, UserManager.getUserParam().MainId),
                                        _objDal.MakeInParams("@UserId", SqlDbType.Int, 0, UserManager.getUserParam().UserId),
                                        _objDal.MakeInParams("@FromDate", SqlDbType.DateTime, 0, FromDate),
                                        _objDal.MakeInParams("@ToDate", SqlDbType.DateTime, 0, ToDate),
                                     };
                _objDal.RunProc("EC_Mst_CRM_Fill_details_In_Grid", sqlPara, ref _objDS);
            }

            return _objDS;
        }

        public static void InsertItem(DropDownList ddl_ID)
        {
            ddl_ID.Items.Insert(0, new ListItem("Select One", "0"));
        }

        public static void SetTableName(string[] nameList, DataSet objDS)
        {
            for (int i = 0; i < nameList.Length; i++)
            {
                objDS.Tables[i].TableName = nameList[i];
            }
        }

        /// <summary>
        /// For Avoiding To Insert Duplicate Values In DatTable Set Primary Key Constraints 
        /// Try To Call In Modal.
        /// </summary>
        /// <param name="keyList">Array[] Of Primary Key Coulmn Names</param>
        /// <param name="objDT"></param>
        public static void SetPrimaryKeys(string[] keyList, DataTable objDT)
        {
            DataColumn[] objDC = new DataColumn[keyList.Length];
            for (int i = 0; i < keyList.Length; i++)
            {
                try
                {
                    objDC[i] = objDT.Columns[keyList[i]];
                }
                catch (System.Data.DataException e)
                {
                    DisplayExceptions(e);
                }
            }
            objDT.PrimaryKey = objDC;
        }

        /// <summary>
        /// Add Sr_No Column In DataTable which is Auto Increamented
        /// </summary>
        /// <param name="objDT"></param>
        public static void AddSrNoColumn(DataTable objDT)
        {
            objDT.Columns.Add("Sr_No", typeof(System.Int32));
            if (objDT.Rows.Count > 0)
            {
                for (int i = 0; i < objDT.Rows.Count; i++)
                {
                    objDT.Rows[i]["Sr_No"] = i;
                }
            }
            objDT.Columns["Sr_No"].AutoIncrement = true;
            objDT.Columns["Sr_No"].AutoIncrementSeed = objDT.Rows.Count;
            objDT.Columns["Sr_No"].AutoIncrementStep = 1;
            objDT.AcceptChanges();
        }

        public static void DisplayExceptions(Exception e)
        {
            ClassLibraryMVP.ErrorManager.ErrorHandler objErrorHandler = new ClassLibraryMVP.ErrorManager.ErrorHandler();
            string _errorMsg = e.Message + "Ö" + e.Source + "Ö" + e.StackTrace + "Ö" + e.TargetSite;
            objErrorHandler.DisplayError("", _errorMsg);
        }

        public static void DisplayErrors(int ErrorCode)
        {
            ClassLibraryMVP.ErrorManager.ErrorHandler objErrorHandler = new ClassLibraryMVP.ErrorManager.ErrorHandler();
            objErrorHandler.DisplayError(ErrorCode);
        }

        public static void DisplayErrors(string ErrorMsg)
        {
            ClassLibraryMVP.ErrorManager.ErrorHandler objErrorHandler = new ClassLibraryMVP.ErrorManager.ErrorHandler();
            objErrorHandler.DisplayError(ErrorMsg);
        }

        public DataView Get_View_Table(DataTable DT, string FilterExpression)
        {
            DataView View = new DataView();
            View.Table = DT;
            View.RowFilter = FilterExpression;
            View.RowStateFilter = DataViewRowState.CurrentRows;

            return View;
        }

        public DataView Get_View_Table(DataTable DT, string FilterExpression, string SortBy)
        {
            DataView View = new DataView();
            View.Table = DT;
            View.RowFilter = FilterExpression;
            View.RowStateFilter = DataViewRowState.CurrentRows;
            View.Sort = SortBy;
            return View;
        }

        public bool IsNumeric(string TxtValue)
        {
            if (Util.String2Int(TxtValue) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string CheckNumeric(string TxtValue)
        {
            if (Util.String2Int(TxtValue) < 0)
            {
                return  "0" ;
            }
            else
            {
                return TxtValue;
            }
        }

        public bool IsValidDate(DateTime _date)
        {
            if (_date <= DateTime.Today && _date >= UserManager.getUserParam().StartDate  && _date<= UserManager.getUserParam().EndDate)
            { 
                return true;
            }
            else
            {
                return false;
            }
        }


        //public void CheckBeforeEdit(int Key_Id)
        //{
        //    CheckBeforeEdit(Key_Id, -1);
        //}


        //public void CheckBeforeEdit(int Key_Id, int OtherId)
        //{
        //    if (ClassLibrary.General.Mode.EDIT == GetPageMode())
        //    {
        //        int ErrorCode = getErrorCode(Key_Id, OtherId);
        //        if (ErrorCode != 0)
        //        {
        //            DisplayErrors(ErrorCode);
        //        }
        //    }
        //}

        //private int getErrorCode(int Key_Id, int OtherId)
        //{
        //    SqlParameter[] objSqlParam = { _objDal.MakeOutParams("@ERROR_CODE",SqlDbType.Int,0),
        //                                       _objDal.MakeInParams("@Key_Id",SqlDbType.Int,0,Key_Id),
        //                                       _objDal.MakeInParams("@MenuItemId",SqlDbType.Int,0,GetMenuItemId()),
        //                                       _objDal.MakeInParams("@OtherId",SqlDbType.Int,0,OtherId)
        //                                     };
        //    _objDal.RunProc("[rstil27].[EF_Trn_CheckIsEditableRecord]", objSqlParam);

        //    int ErrorCode;
        //    ErrorCode = Convert.ToInt32(objSqlParam[0].Value.ToString());
        //    return ErrorCode;
        //}


        public string GetResourceString(string filepath)
        {
            string path = HttpContext.Current.Request.PhysicalApplicationPath + filepath;

            DataSet objDS = new DataSet();
            objDS.ReadXml(path);
            objDS.Tables["data"].Columns.RemoveAt(2);
            objDS.Tables["data"].Columns.RemoveAt(2);
            objDS.Tables["data"].Columns.RemoveAt(2);
            objDS.Tables["data"].Columns.RemoveAt(2);
            objDS.Tables["data"].AcceptChanges();
            DataView objDV = new DataView(objDS.Tables["data"].Copy(), "", "name DESC", DataViewRowState.CurrentRows);
            objDS = null;
            string text = " ~ ";
            bool found = false;
            foreach (DataRow Dr in objDV.ToTable().Rows)
            {
                if (Dr["name"].ToString().Contains("Msg"))
                {
                    text = text + "Ö" + Dr["name"].ToString() + "~" + Dr["value"].ToString();
                    found = true;
                    continue;
                }

                if (found == true)
                { break; }
            }
            objDV = null;
            return text;
        }

        public static int GetMenuItemId()
        {
            string Crypt = "";
            int MenuItemId;

            Crypt = System.Web.HttpContext.Current.Request.QueryString["Menu_Item_Id"];
            MenuItemId = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            if (MenuItemId == -1)
            {
                DisplayErrors(1000);
            }
            return MenuItemId;
        }

        public static int GetMenuItemCode()
        {
            return Util.String2Int(Rights.GetObject().GetLinkDetails(GetMenuItemId()).MenuItemCode);
        }
        public static int GetMenuItemCode(int MenuItemId)
        {
            return Util.String2Int(Rights.GetObject().GetLinkDetails(MenuItemId).MenuItemCode);
        }
        // Added by Harshal on 08-01-10
        public static int GetMenuQueryString(int MenuItemId)
        {
            int QueryString;
            if (Rights.GetObject().GetLinkDetails(MenuItemId).QueryString == "")
            {
                QueryString = 0;
            }
            else
            {
                QueryString = Util.String2Int(Rights.GetObject().GetLinkDetails(MenuItemId).QueryString);
            }
            return QueryString;
        }

        // Commented by Harshal on 08-01-10
        //public static string GetMenuQueryString(int MenuItemId)
        //{
            //return Rights.GetObject().GetLinkDetails(MenuItemId).QueryString;
        //}

        public static int GetMode()
        {
            string Crypt = "";
            int Mode;

            Crypt = System.Web.HttpContext.Current.Request.QueryString["Mode"];
            Mode = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            if (Mode == -1)
            {
                DisplayErrors(1000);
            }
            return Mode;
        }

        public static Boolean IsPopupMenuItem(int MenuItemId)
        {
            int i;
            Boolean IsFound = false;
            String CommaSepMenuItems = GetParameter("MenuItemsForPopup");
            char[] Sep = { ',' };
            Array MenuItems = CommaSepMenuItems.Split(Sep);

            for (i = 0; i < MenuItems.Length; i++)
            {
                if (MenuItemId == Util.String2Int(MenuItems.GetValue(i).ToString()))
                {
                    IsFound = true;
                    break;
                }
            }
            return IsFound;
        }

        public static Boolean IsLinkPopupMenuItem(int MenuItemId)
        {
            DAL _objDal = new DAL();
            DataSet _objDS = new DataSet();
            string Query = "Select Is_PopUp_From_Link from COM_Adm_Menu_Item where MenuItem_ID = " + MenuItemId.ToString();

            SqlParameter[] sqlPara = {  
            _objDal.MakeInParams("@Query", SqlDbType.NVarChar, 4000, Query)};
            _objDal.RunProc("EC_Common_Pass_Query", sqlPara, ref _objDS);
            
            return (Util.String2Bool(_objDS.Tables[0].Rows[0]["Is_PopUp_From_Link"].ToString()));

            //int i;
            //Boolean IsFound = false;
            //String CommaSepMenuItems = GetParameter("MenuItemsForLinkPopup");
            //char[] Sep = { ',' };
            //Array MenuItems = CommaSepMenuItems.Split(Sep);

            //for (i = 0; i < MenuItems.Length; i++)
            //{
            //    if (MenuItemId == Util.String2Int(MenuItems.GetValue(i).ToString()))
            //    {
            //        IsFound = true;
            //        break;
            //    }
            //}
            //return true;
        }

        public static string GetParameter(String KeyName)
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get(KeyName);
        }

        public DataSet EC_Common_Pass_Query(String Query)
        {
            SqlParameter[] sqlPara = {  
        _objDal.MakeInParams("@Query", SqlDbType.NVarChar, 4000, Query)};

            _objDal.RunProc("EC_Common_Pass_Query", sqlPara, ref _objDS);
            return _objDS;
        }
               
        // ------- only for disabled all save button on any save button ckick ---------//added by shiv 28 feb 09

        public string ClickedOnceScript_For_JS_Validation(System.Web.UI.Page page, System.Web.UI.Control btn1)
        {
            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
            sbValid.Append("if (Allow_To_Save() ==  false) {return false;}");
            sbValid.Append("this.value = 'Wait...';");
            sbValid.Append("this.disabled = true;");
            sbValid.Append(page.ClientScript.GetPostBackEventReference(btn1, ""));
            sbValid.Append(";");

            return sbValid.ToString();
        }

        public string ClickedOnceScript_For_JS_Validation(System.Web.UI.Page page, System.Web.UI.Control btn1, System.Web.UI.Control btn2)
        {
            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
            sbValid.Append("if (Allow_To_Save() ==  false) {return false;}");
            sbValid.Append("this.value = 'Wait...';");
            sbValid.Append("this.disabled = true;");
            sbValid.Append(btn2.ClientID + ".disabled = true;");
            sbValid.Append(page.ClientScript.GetPostBackEventReference(btn1, ""));
            sbValid.Append(";");

            return sbValid.ToString();
        }

        public string ClickedOnceScript_For_JS_Validation(System.Web.UI.Page page, System.Web.UI.Control btn1, System.Web.UI.Control btn2, System.Web.UI.Control btn3)
        {
            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
            sbValid.Append("if (Allow_To_Save() ==  false) {return false;}");
            sbValid.Append("this.value = 'Wait...';");
            sbValid.Append("this.disabled = true;");
            sbValid.Append(btn2.ClientID + ".disabled = true;");
            sbValid.Append(btn3.ClientID + ".disabled = true;");
            sbValid.Append(page.ClientScript.GetPostBackEventReference(btn1, ""));
            sbValid.Append(";");

            return sbValid.ToString();
        }

        public string ClickedOnceScript_For_JS_Validation(System.Web.UI.Page page, System.Web.UI.Control btn1, System.Web.UI.Control btn2, System.Web.UI.Control btn3, System.Web.UI.Control btn4)
        {
            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
            sbValid.Append("if (Allow_To_Save() ==  false) {return false;}");
            sbValid.Append("this.value = 'Wait...';");
            sbValid.Append("this.disabled = true;");
            sbValid.Append(btn2.ClientID + ".disabled = true;");
            sbValid.Append(btn3.ClientID + ".disabled = true;");
            sbValid.Append(btn4.ClientID + ".disabled = true;");
            sbValid.Append(page.ClientScript.GetPostBackEventReference(btn1, ""));
            sbValid.Append(";");

            return sbValid.ToString();
        }

        public string ClickedOnceScript_For_JS_Validation(System.Web.UI.Page page, System.Web.UI.Control btn1, System.Web.UI.Control btn2, System.Web.UI.Control btn3, System.Web.UI.Control btn4, System.Web.UI.Control btn5)
        {
            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
            sbValid.Append("if (Allow_To_Save() ==  false) {return false;}");
            sbValid.Append("this.value = 'Wait...';");
            sbValid.Append("this.disabled = true;");
            sbValid.Append(btn2.ClientID + ".disabled = true;");
            sbValid.Append(btn3.ClientID + ".disabled = true;");
            sbValid.Append(btn4.ClientID + ".disabled = true;");
            sbValid.Append(btn5.ClientID + ".disabled = true;");
            sbValid.Append(page.ClientScript.GetPostBackEventReference(btn1, ""));
            sbValid.Append(";");

            return sbValid.ToString();
        }

        public void Disable_save_button_on_click(System.Web.UI.Page page,Button btn_save,string jsfunctionname)
        {
            System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
            sbValid.Append("if (" + jsfunctionname + "==  false) {return false;}");
            sbValid.Append("this.value = 'Wait...';");
            sbValid.Append("this.disabled = true;");
            sbValid.Append(page.ClientScript.GetPostBackEventReference(btn_save, ""));
            sbValid.Append(";");
            btn_save.Attributes.Add("OnClick", sbValid.ToString());
        }

        // end ------- only for disabled all save button on any save button ckick ---------/

        public Boolean IsCheck_Duplicate(string Flag, int Id, string Value_To_Check)
        {
            Boolean Is_DuplicateName;
            SqlParameter[] objSqlParam = { _objDal.MakeInParams("@Master_Name", SqlDbType.VarChar, 100, Flag),
                                    _objDal.MakeInParams("@id", SqlDbType.Int, 0, Id),
                                    _objDal.MakeInParams("@Value_To_Check", SqlDbType.VarChar, 100, Value_To_Check),
                                    _objDal.MakeOutParams("@is_duplicate", SqlDbType.Bit, 1)};

            _objDal.RunProc("[EC_Mst_Check_DuplicateNames]", objSqlParam);

            Is_DuplicateName = Util.String2Bool(objSqlParam[3].Value.ToString());
            return Is_DuplicateName;
        }

        public DataSet Get_Values_Where(String Table_Name, String Comma_Seperated_Fields, String Where_Condition, String Order_by_Field_name, Boolean Is_Active)
        {
            SqlParameter[] sqlpar = {_objDal.MakeInParams("@Table_Name", SqlDbType.VarChar, 50, Table_Name),
            _objDal.MakeInParams("@Comma_Seperated_Fields", SqlDbType.VarChar, 1000, Comma_Seperated_Fields),
            _objDal.MakeInParams("@Order_by_Field_name", SqlDbType.VarChar, 50, Order_by_Field_name),
            _objDal.MakeInParams("@Where_Condition", SqlDbType.VarChar, 4000, Where_Condition),
            _objDal.MakeInParams("@Is_Active", SqlDbType.Bit, 1, Is_Active)};
            _objDal.RunProc("EC_Select_All_With_WhereCondition", sqlpar, ref _objDS);
            return _objDS;
        }

        public string Get_Next_Number()
        {
            SqlParameter[] SqlPara = {_objDal.MakeOutParams("@Next_No_For_Print", SqlDbType.VarChar, 20),
             _objDal.MakeOutParams("@Next_No", SqlDbType.Int, 0),
             _objDal.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
             _objDal.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, UserManager.getUserParam().HierarchyCode),
             _objDal.MakeInParams("@Main_ID", SqlDbType.Int, 0, UserManager.getUserParam().MainId), 
             _objDal.MakeInParams("@Division_ID", SqlDbType.Int, 0, UserManager.getUserParam().DivisionId), 
             _objDal.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0, GetMenuItemId()),
             _objDal.MakeInParams("@Call_For_Display", SqlDbType.Bit, 0, true)};

            _objDal.RunProc("dbo.EC_Opr_Generate_Next_No", SqlPara);

            return SqlPara[0].Value.ToString();
        }

        public string Get_Next_Number(int Menu_Item_Id)
        {
            SqlParameter[] SqlPara = {_objDal.MakeOutParams("@Next_No_For_Print", SqlDbType.VarChar, 20),
             _objDal.MakeOutParams("@Next_No", SqlDbType.Int, 0),
             _objDal.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
             _objDal.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, UserManager.getUserParam().HierarchyCode),
             _objDal.MakeInParams("@Main_ID", SqlDbType.Int, 0, UserManager.getUserParam().MainId), 
             _objDal.MakeInParams("@Division_ID", SqlDbType.Int, 0, UserManager.getUserParam().DivisionId), 
             _objDal.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0, Menu_Item_Id),
             _objDal.MakeInParams("@Call_For_Display", SqlDbType.Bit, 0, true)};

            _objDal.RunProc("dbo.EC_Opr_Generate_Next_No", SqlPara);

            return SqlPara[0].Value.ToString();
        }
        // dinesh 171208

        public string Get_Next_Number_For_AUS_Other_Agency()
        {
            SqlParameter[] SqlPara = {_objDal.MakeOutParams("@Next_No_For_Print", SqlDbType.VarChar, 20),
             _objDal.MakeOutParams("@Next_No", SqlDbType.Int, 0),
             _objDal.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
             _objDal.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, UserManager.getUserParam().HierarchyCode),
             _objDal.MakeInParams("@Main_ID", SqlDbType.Int, 0, UserManager.getUserParam().MainId), 
             _objDal.MakeInParams("@Division_ID", SqlDbType.Int, 0, UserManager.getUserParam().DivisionId), 
             _objDal.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0, 72),  // which is of AUS
             _objDal.MakeInParams("@Call_For_Display", SqlDbType.Int, 0, true)};

            _objDal.RunProc("dbo.EC_Opr_Generate_Next_No", SqlPara);

            return SqlPara[0].Value.ToString();
        }

        public DataSet GetUserInfo(int IdValue)
        {
            //string TableName = StateManager.GetState<string>("TableName");
            //string IdColName = StateManager.GetState<string>("IdColName");
            int MenuItemId = Common.GetMenuItemId();

            SqlParameter[] sqlPara = {
                                        //_objDal.MakeInParams("@TableName",SqlDbType.VarChar,250,TableName),
                                        //_objDal.MakeInParams("@IdColName", SqlDbType.VarChar, 100, IdColName),
                                        _objDal.MakeInParams("@MenuItemId",SqlDbType.Int,0,MenuItemId),
                                        _objDal.MakeInParams("@IdValue", SqlDbType.Int, 0, IdValue)};

            _objDal.RunProc("EF_CC_ViewUserInfo", sqlPara, ref _objDS);
            return _objDS;
        }

        public void ForceRights(ref bool can_read, ref  bool can_add, ref bool can_edit, ref bool can_cancel)
        {
            int MenuItemId = Common.GetMenuItemId();

            SqlParameter[] sqlPara = {
                                        _objDal.MakeOutParams("@Can_read", SqlDbType.Bit, 0),
                                        _objDal.MakeOutParams("@Can_Add", SqlDbType.Bit, 0) ,
                                        _objDal.MakeOutParams("@Can_Edit", SqlDbType.Bit, 0),
                                        _objDal.MakeOutParams("@Can_cancel", SqlDbType.Bit, 0),
                                        _objDal.MakeInParams("@Heirarchy_Code",SqlDbType.VarChar,5,UserManager.getUserParam().HierarchyCode),
                                        _objDal.MakeInParams("@MenuItemId",SqlDbType.Int,0,MenuItemId)};

            _objDal.RunProc("EC_ForceRights_On_Menuitem", sqlPara, ref _objDS);

            can_read = Util.String2Bool(sqlPara[0].Value.ToString());
            can_add = Util.String2Bool(sqlPara[1].Value.ToString());
            can_edit = Util.String2Bool(sqlPara[2].Value.ToString());
            can_cancel = Util.String2Bool(sqlPara[3].Value.ToString());
        }

        public void CheckRights(ref bool can_view, ref  bool can_add, ref bool can_edit, ref bool can_cancel)
        {
            int MenuItemId = Common.GetMenuItemId();

            UserRights uObj;
            uObj = StateManager.GetState<UserRights>("UserRights");
            FormRights fRights = uObj.getForm_Rights(MenuItemId);
            can_add = fRights.canAdd();
            can_edit = fRights.canEdit();
            can_view = fRights.canRead();
            can_cancel = fRights.canDelete();
        }

        public void CheckFormRights(Button btn_Save)
        {
            bool can_view = false, can_add = false, can_edit = false, can_cancel = false;
            bool can_view1 = false, can_add1 = false, can_edit1 = false, can_cancel1 = false;

            CheckRights(ref can_view, ref can_add, ref can_edit, ref can_cancel);
            ForceRights(ref can_view1, ref can_add1, ref can_edit1, ref can_cancel1);

            btn_Save.Enabled = can_add;
            if (can_add1 == false)
                btn_Save.Enabled = false;
        }


        public void Get_Document_Allocation_Details(ref int Document_Series_Allocation_ID, ref int Start_No,
            ref int End_No, ref int Next_No, int VA_ID, int Branch_ID, int Document_Id)
        {
            SqlParameter[] objSqlParam ={  
                                    _objDal.MakeOutParams("@Document_Allocation_Id", SqlDbType.Int, 0 ),
                                    _objDal.MakeOutParams("@Start_No", SqlDbType.Int, 0 ),
                                    _objDal.MakeOutParams("@End_No", SqlDbType.Int, 0 ),
                                    _objDal.MakeOutParams("@Next_No", SqlDbType.Int, 0 ),
                                    _objDal.MakeInParams("@VA_ID", SqlDbType.Int, 0, VA_ID ),
                                    _objDal.MakeInParams("@Main_Id", SqlDbType.Int, 0, Branch_ID ),
                                    _objDal.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
                                    _objDal.MakeInParams("@Document_Id", SqlDbType.Int, 0, Document_Id )};

            _objDal.RunProc("Ec_Opr_Get_Document_Allocation_Details", objSqlParam, ref _objDS);

            Document_Series_Allocation_ID = Util.String2Int(objSqlParam[0].Value.ToString()); 
            Start_No = Util.String2Int(objSqlParam[1].Value.ToString());
            End_No = Util.String2Int(objSqlParam[2].Value.ToString());
            Next_No = Util.String2Int(objSqlParam[3].Value.ToString());
        }

        public void Get_Document_Next_Counter_No(ref int Document_Next_Counter_No, int Branch_ID, int Document_Type_Id)
        {
            SqlParameter[] objSqlParam ={  
                                    _objDal.MakeOutParams("@Document_Next_Counter_No", SqlDbType.Int, 0 ),                                    
                                    _objDal.MakeInParams("@Branch_id", SqlDbType.Int, 0, Branch_ID ),
                                    _objDal.MakeInParams("@Document_Type_Id", SqlDbType.Int, 0, Document_Type_Id)};

            _objDal.RunProc("Ec_Opr_Get_Document_Next_Counter_No", objSqlParam, ref _objDS);
            Document_Next_Counter_No  = Util.String2Int(objSqlParam[0].Value.ToString());            
        }

        public DataSet FillDetailsInGrid1()
        {
            DataSet objDs = new DataSet();
            objDs = _objDal.RunQuery("SELECT TOP 50 FA_Opr_UnApproved_Voucher.Voucher_Id,FA_Opr_UnApproved_Voucher.Voucher_No,dbo.DateOnlyDisplay(FA_Opr_UnApproved_Voucher.Voucher_Date) as Voucher_Date,'' as Ledger_Name,'' as Ref_No,'' as Debit,'' as Credit FROM FA_Opr_UnApproved_Voucher ORDER BY FA_Opr_UnApproved_Voucher.Voucher_Date DESC");
            return objDs;
        }

        public void SetStandardCaptionForGrid(DataGrid dg_Grid)
        {
            string col_header = "";

            for (int i = 0; i <= dg_Grid.Columns.Count - 1; i++)
            {
                col_header = dg_Grid.Columns[i].HeaderText;

                if (col_header.ToLower().Contains("gc_caption"))
                {
                    dg_Grid.Columns[i].HeaderText = col_header.Replace("gc_caption", CompanyManager.getCompanyParam().GcCaption);
                }
                else if (col_header.ToLower().Contains("lhpo_caption"))
                {
                    dg_Grid.Columns[i].HeaderText = col_header.Replace("lhpo_caption", CompanyManager.getCompanyParam().LHPOCaption);
                }
            }
        }

        public void SetStandardCaptionForDataset(DataTable dt)
        {
            string col_header = "";

            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                col_header = dt.Columns[i].ColumnName;

                if (col_header.ToLower().Contains("gc_caption"))
                {
                    dt.Columns[i].ColumnName = col_header.Replace("gc_caption", CompanyManager.getCompanyParam().GcCaption);
                }
                else if (col_header.ToLower().Contains("lhpo_caption"))
                {
                    dt.Columns[i].ColumnName = col_header.Replace("lhpo_caption", CompanyManager.getCompanyParam().LHPOCaption);
                }
            }
        }

        public void ValidateReportForm(DataGrid dg, DataTable dt,
            string CallFrom,Label lblError)
        {
            if (CallFrom != "exporttoexcelusercontrol")
            {
                dg.DataSource = dt;
                dg.DataBind();
            }

            if (dt.Rows.Count > 0)
            {
                lblError.Text = "";
                //lblError.Text = "Total " + " " + dt.Rows.Count + " " + "Record/(s) Found";
               
                dg.Visible = true;
            }
            else
            {
                lblError.Text = "No Record(s) Found";
                dg.Visible = false;
            }
        }

       public void ValidateReportForm(DataGrid dg, DataTable dt,
            string CallFrom,Label lblError,string TotalRecords)
        {
            if (CallFrom != "exporttoexcelusercontrol")
            {
                dg.DataSource = dt;
                dg.DataBind();
            }

            if (dt.Rows.Count > 0)
            {
               // lblError.Text = "";
                //lblError.Text = "Total " + " " + dt.Rows.Count + " " + "Record/(s) Found";
                lblError.Text = "Total" + " " + TotalRecords + " " + "Record/(s) Found";
                dg.Visible = true;
            }
            else
            {
                lblError.Text = "No Record(s) Found";
                dg.Visible = false;
            }
        }

        public decimal Get_Service_Tax_Percent(DateTime Applicable_From)
        {
            SqlParameter[] objSqlParam ={  
            _objDal.MakeOutParams("@Service_Tax_Percent", SqlDbType.Float, 0 ),                                    
                _objDal.MakeInParams("@date", SqlDbType.DateTime  , 0, Applicable_From )};

            _objDal.RunProc("Ec_Get_Service_Tax_Percent",objSqlParam);

            return Util.String2Decimal(objSqlParam[0].Value.ToString());
        }

        public static int Get_Selected_DivisionId()
        {
            if (System.Web.HttpContext.Current.Session["GetSelectedDivisionId"] != null)
            {
                return Util.String2Int(System.Web.HttpContext.Current.Session["GetSelectedDivisionId"].ToString());
            }
            else 
            { 
                return UserManager.getUserParam().DivisionId; 
            }
        }


        public DataTable GetDeliveryArea(int BranchID, Boolean WithUnknownGC, Boolean IsActive, Boolean BasedOnCityId, int CityID)
        {
            SqlParameter[] objSqlParam ={  
                                    _objDal.MakeInParams("@BranchID", SqlDbType.Int, 0, BranchID),
                                    _objDal.MakeInParams("@WithUnknownGC", SqlDbType.Bit, 0, WithUnknownGC),
                                    _objDal.MakeInParams("@IsActive", SqlDbType.Bit, 0, IsActive),
                                    _objDal.MakeInParams("@BasedOnCityId", SqlDbType.Bit, 0, BasedOnCityId),
                                    _objDal.MakeInParams("@CityID", SqlDbType.Int, 0, CityID)};

            _objDal.RunProc("Ec_Opr_Get_DeliveryArea", objSqlParam, ref _objDS);
            return _objDS.Tables[0];
        }


        public void Get_Consignor_Conginee_SMS(int GC_ID, int MenuItemId, int DocumentID)
        {
            DAL objdal = new DAL();
            DataSet ds = new DataSet();
            
            SqlParameter[] sqlPara = { objdal.MakeInParams("@GC_ID", SqlDbType.Int, 0, GC_ID)
             ,objdal.MakeInParams("@MenuItemID", SqlDbType.Int, 0, MenuItemId)
             ,objdal.MakeInParams("@DocumentID", SqlDbType.Int, 0, DocumentID) };

            objdal.RunProc("Ec_Opr_Get_Consignor_Conginee_SMS_MSG", sqlPara, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                string result = "";
                WebRequest request = null;
                HttpWebResponse response = null;
                String sendToPhoneNumber="";
                string msg="";
                try
                {
                    if (MenuItemId == 80 || MenuItemId == 82 )
                    {
                         sendToPhoneNumber = ds.Tables[0].Rows[0]["Consignor_Mobile_No"].ToString();
                         msg = ds.Tables[0].Rows[0]["MsgConsignor"].ToString();
                    }
                    else if (MenuItemId == 72 || MenuItemId == 77)
                    {
                         sendToPhoneNumber = ds.Tables[1].Rows[0]["Consignee_Mobile_No"].ToString();
                         msg = ds.Tables[1].Rows[0]["MsgConsignee"].ToString();
                    }

                    if (ValidateMobileDetails(sendToPhoneNumber, msg))
                    {
                        String userid = "2000126072";
                        String passwd = "Rajan@1234"; //  "Rajan@1234";

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

            if (MenuItemId == 80)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    string result = "";
                    WebRequest request = null;
                    HttpWebResponse response = null;
                    String sendToPhoneNumber = "";
                    string msg = "";
                    try
                    {
                        sendToPhoneNumber = ds.Tables[1].Rows[0]["Consignee_Mobile_No"].ToString();
                        msg = ds.Tables[1].Rows[0]["MsgConsignee"].ToString();

                        if (ValidateMobileDetails(sendToPhoneNumber, msg))
                        {
                            String userid = "2000126072";
                            String passwd = "Rajan@1234"; // "Rajan@1234";

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

        public DataSet GetPendingMemo()
        {
            SqlParameter[] sqlPara = {
             _objDal.MakeInParams("@BranchID", SqlDbType.Int, 0, UserManager.getUserParam().MainId)};
            _objDal.RunProc("EC_Rpt_Pending_Memo_Against_Loading_Plan", sqlPara, ref _objDS);
            return _objDS;
        }

        public DataSet GetPMTaskAlertCount(String CallFrom)
        {
            SqlParameter[] sqlPara = {
             _objDal.MakeInParams("@CallFrom", SqlDbType.VarChar , 10,CallFrom)};
            _objDal.RunProc("dbo.EF_Trn_PM_Task_Alert_GetCount",sqlPara, ref _objDS);
            return _objDS;
        }

        public void RunTaskScheduler()
        {
            _objDal.RunProc("dbo.EF_Trn_PM_Task_Alert_Run");
        }
        public DataSet FillVendorInDDL(string SearchFor, int KeyId)
        {
            SqlParameter[] sqlPara = { _objDal.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor), 
                                       _objDal.MakeInParams("@KeyId", SqlDbType.Int, 0, KeyId) };

            _objDal.RunProc("dbo.EF_Mst_Vendor_FillDDL", sqlPara, ref _objDS);
            return _objDS;
        }
    }

}