using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Raj.CRM.TransactionView;
using Raj.CRM.TransactionModel;

/// <summary>
/// Summary description for IncludeExcludePresenter
/// </summary>
namespace Raj.CRM.TransactionPresenter
{

    public class IncludeExcludePresenter : ClassLibraryMVP.General.Presenter
    {

        private IIncludeExcludeView objIIncludeExcludeView;
        private IncludeExcludeModel objIncludeExcludeModel;
        DataSet objDS;

        public IncludeExcludePresenter(IIncludeExcludeView includeExcludeView, bool IsPostBack)
        {
            objIIncludeExcludeView = includeExcludeView;
            objIncludeExcludeModel = new IncludeExcludeModel(objIIncludeExcludeView);

            base.Init(objIIncludeExcludeView, objIncludeExcludeModel);

            if (!IsPostBack)
            {                
                initValues();
                FillGrid();
            }
        }

        public void initValues()
        {
            objDS = objIncludeExcludeModel.FillLabelValues();
            objIIncludeExcludeView.ComplaintNatureName = objDS.Tables[0].Rows[0]["Complaint_Nature"].ToString();
            objIIncludeExcludeView.ProfileName = objDS.Tables[0].Rows[0]["Profile_Name"].ToString(); 
        }
        public void FillGrid()
         {
             objIIncludeExcludeView.BindGrid = objIncludeExcludeModel.FillGrid();
        }

        public void Save()
        {
            //objIncludeExcludeModel.Save();
            base.DBSave();
        }

        }
	}
