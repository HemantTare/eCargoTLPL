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
/// <summary>
/// Summary description for LabelPrintingModel
/// </summary>
namespace Raj.EC.OperationModel
{
    public class LabelPrintingModel : IModel
    {
        private ILabelPrintingView objILabelPrintingView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        private int _branchID = UserManager.getUserParam().MainId;
        private bool _isacctransferreq = UserManager.getUserParam().IsAccTransferReq;
        private int _divisionID = UserManager.getUserParam().DivisionId;
        private string _hierarchycode = UserManager.getUserParam().HierarchyCode;

        public LabelPrintingModel(ILabelPrintingView LabelPrintingView)
        {
            objILabelPrintingView = LabelPrintingView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, _branchID),  
            objDAL.MakeInParams("@GetGCXML", SqlDbType.Xml, 0, objILabelPrintingView.GetGCNoXML)  
            };

            objDAL.RunProc("dbo.EC_Opr_LabelPrinting_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }       
           
        //public DataSet gc_grid_validation()
        //{
        //    SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@memo_Id", SqlDbType.Int, 0, objILabelPrintingView.keyID), 
        //    objDAL.MakeInParams("@GetXML", SqlDbType.Xml, 0, objILabelPrintingView.LabelPrintingDetailsXML)};

        //    objDAL.RunProc("dbo.EC_Opr_Menifest_GridGC_Validation", objSqlParam, ref objDS);

        //    return objDS;
        //}

        public Message Save()
        {
            Message objMessage = new Message();

            ////SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            ////objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            ////objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),                
            ////objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,_divisionID),
            ////objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
            ////objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
            ////objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            ////objDAL.MakeInParams("@Memo_Id", SqlDbType.Int, 0,objILabelPrintingView.keyID),    
            ////objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, UserManager.getUserParam().UserId), 
            ////objDAL.MakeInParams("@MemoDetailsXML",SqlDbType.Xml,0,objILabelPrintingView.LabelPrintingDetailsXML) 

            ////};

            ////objDAL.RunProc("dbo.EC_Opr_Menifest_Save", objSqlParam);

            objMessage.messageID = 0; //Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = "";//Convert.ToString(objSqlParam[1].Value);
                        
            if (objMessage.messageID == 0)
            {
                ////objILabelPrintingView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                if (objILabelPrintingView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Outward/FrmMenifest.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objILabelPrintingView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objILabelPrintingView.Flag == "SaveAndPrint")
                { 
                    //script redirect on same page after clicking on save dialog box.
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    //int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Inward/Frm_LabelPrintingViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId)));
                }
            }

            return objMessage;
        }
    }
}
