using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using Raj.eCargo.Operation.Muster.Model;
using Raj.eCargo.Operation.Muster.View;
//using Raj.eCargo.Init;


/// <summary>
/// Summary description for OperationMusterDailyEntryPresenter
/// </summary>
/// 
namespace Raj.eCargo.Operation.Muster.Presenter
{
public class OperationMusterDailyEntryPresenter :ClassLibraryMVP.General.Presenter
{
         private IOperationMusterDailyEntryView _objIMusterEntryDailyView;
         private OperationMusterDailyEntryModel _objMusterEntryDailyModel;
         private DataSet _ds = new DataSet();
       
	        public OperationMusterDailyEntryPresenter(IOperationMusterDailyEntryView IMusterEntry,bool isPostBack)
	        {
		       _objIMusterEntryDailyView = IMusterEntry;
               _objMusterEntryDailyModel = new OperationMusterDailyEntryModel(_objIMusterEntryDailyView);

                base.Init(_objIMusterEntryDailyView, _objMusterEntryDailyModel);
                    if (!isPostBack)
                    {
                        //initControl();
                    }
	        }

        public void Save()
        {
            if (_objIMusterEntryDailyView.validateUI() == true)
            {
                Message mObj = new Message();
                mObj = _objMusterEntryDailyModel.Save();

                //if (mObj.messageID == 2627)
                //{
                //    AppParam AppParamObj = new AppParam();
                //    int Error_Code;
                //    String Crypt = Utility.Int2String(mObj.messageID);
                //    Error_Code = Convert.ToInt32(ClassLibrary.crypt.DecryptToInt(Crypt));
                //    _objIMusterEntryDailyView.errorMessage = AppParamObj.Get_ErrorValues(mObj.messageID).Description;
                //}
                //else if (mObj.messageID != 0)
                //{
                //    AppErrorLog objAppErrorLog = new AppErrorLog();
                //    objAppErrorLog.Add_DB_Log(mObj, _objIShiftMasterView.formName);
                //    _objIMusterEntryDailyView.errorMessage = mObj.message;
                //}
                //else
                //{
                //  _objIMusterEntryDailyView.errorMessage = "Saved Successfully";
                //  Utility.Redirect(_LinkId);
                // }

                if (mObj.messageID == 0)
                {
                    _objIMusterEntryDailyView.check = true;
                }
                _objIMusterEntryDailyView.errorMessage = mObj.message;
               

            }
        }


        public void GetValues(string H_Code,int Main_Id,int Division_Id)
        {

            _ds = _objMusterEntryDailyModel.GetValues(H_Code, Main_Id, Division_Id);
            if (_ds.Tables[1].Rows.Count > 0)
            {
                _objIMusterEntryDailyView.SessionMusterDaily = _ds;
               // _objIMusterEntryDailyView.Bind_MusterEntryDaily = _ds.Tables[0];
            }
        }
    }
}


