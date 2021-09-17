using System;
using System.Data;

using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;
using ClassLibraryMVP;

/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 17/10/2008
/// Summary description for FBTCategoriesPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class FBTCategoriesPresenter : ClassLibraryMVP.General.Presenter
    {
        private IFBTCategoriesView objIFBTCategoriesView;
        private FBTCategoriesModel objFBTCategoriesModel;
        DataSet objDS;

        public FBTCategoriesPresenter(IFBTCategoriesView fbtCategoriesView, bool IsPostBack)
        {
            objIFBTCategoriesView = fbtCategoriesView;
            objFBTCategoriesModel = new FBTCategoriesModel(objIFBTCategoriesView);

            base.Init(objIFBTCategoriesView, objFBTCategoriesModel);

            if (!IsPostBack)
            {
                initValues();
            }
        }

        public void initValues()
        {
            objIFBTCategoriesView.BindFBTCateroies = objFBTCategoriesModel.GetFBTCategoryValues();
            objIFBTCategoriesView.SessionFBTAssesseeCategory = objFBTCategoriesModel.GetAssesseeCategoryValues();
            SetSection();
            FillGrid();
        }
        public void SetSection()
        {
            DataTable DtSection = objFBTCategoriesModel.GetFBTCategoryValues();
            if (objIFBTCategoriesView.FBTCategoryId != 0)
            {
                foreach (DataRow dr in DtSection.Select("FBT_Category_Id=" + objIFBTCategoriesView.FBTCategoryId))
                {
                    objIFBTCategoriesView.FBTSection = dr["FBT_Section"].ToString();
                    break;
                }
            }
            else
            {
                objIFBTCategoriesView.FBTSection = "";
            }
        }
        public void FillGrid()
        {
            DataSet dsCategory = new DataSet();

            dsCategory = objFBTCategoriesModel.ReadValues();

            if (dsCategory.Tables["FBT_Category_Details"] == null)
            {
                DataTable dt = new DataTable("FBT_Category_Details");
                dt.Columns.Add("Assesse_Category_Id");
                dt.Columns.Add("Assesse_Category_Name");
                dt.Columns.Add("Applicable_From");
                dt.Columns.Add("Eligible", typeof(decimal));

                dsCategory.Tables.Add(dt);
                objIFBTCategoriesView.SessionCategoryDetailsGrid = dsCategory;
                objIFBTCategoriesView.BindCategoryDetailsGrid = dsCategory;
            }
            else
            {
                objIFBTCategoriesView.SessionCategoryDetailsGrid = dsCategory;
                objIFBTCategoriesView.BindCategoryDetailsGrid = dsCategory;
            }
        }

        public void Save()
        {
            base.DBSave();
            //if (objIFBTCategoriesView.validateUI())
            //{
            //    base.DBSave();
            //    objFBTCategoriesModel.Save();
            //}
        }
    }
}