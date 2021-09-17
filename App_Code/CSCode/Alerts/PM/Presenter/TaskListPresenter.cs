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
/// Summary description for TaskListPresenter
/// </summary>
/// 
namespace Raj.EF.MasterPresenter
{
    public class TaskListPresenter : Presenter
    {
        private ITaskListView objITaskListView;
        private TaskListModel objTaskListModel;
        private DataSet objDS;

        public TaskListPresenter(ITaskListView taskListView, bool isPostBack)
        {
            objITaskListView = taskListView;
            objTaskListModel = new TaskListModel(objITaskListView);
            base.Init(objITaskListView, objTaskListModel);

            if (!isPostBack)
            {               
                objITaskListView.BindTaskListGrid = objTaskListModel.FillGrid();
            }
        }

        public void Save()
        {
            base.DBSave();
        }
    }
}
