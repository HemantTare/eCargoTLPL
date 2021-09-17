
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
using Raj.EC;
/// <summary>
/// Summary description for TaskTemplatePresenter
/// </summary>

namespace Raj.EF.MasterPresenter
{
    public class TaskTemplatePresenter : Presenter
    {
        private ITaskTemplateView objITaskTemplateView;
        private TaskTemplateModel objTaskTemplateModel;

        private DataSet objDS;

        public TaskTemplatePresenter(ITaskTemplateView TaskTemplateView, bool isPostBack)
        {
            objITaskTemplateView = TaskTemplateView;
            objTaskTemplateModel = new TaskTemplateModel(objITaskTemplateView);
            base.Init(objITaskTemplateView, objTaskTemplateModel);

            if (!isPostBack)
            {
                Get_DropDown();
                FillAllDropdownsAndGrid();

                //objITaskTemplateView.Last_Permormed_Date = DateTime.Now;
                initValues();
            }
        }


        private void FillAllDropdownsAndGrid()
        {
            objDS = objTaskTemplateModel.BindGrid();
            objITaskTemplateView.SessiondgCustomAlertOnDetailsGrid = objDS;

            if (objITaskTemplateView.Form_Type == "VehiclePM")
            {

                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objITaskTemplateView.CustomAlert_On_Value = Convert.ToInt32(objDR["Alert_Before_Value_Calculated"]);

                    objITaskTemplateView.Last_Permormed_On = Convert.ToInt32(objDR["Reference_Value"]);
                    objITaskTemplateView.Last_Permormed_Date = Convert.ToDateTime(objDR["Reference_Date"]);
                }
                else
                {
                    objITaskTemplateView.Last_Permormed_Date = DateTime.Now;
                    objITaskTemplateView.Last_Permormed_On = 0;
                }
            }
            else
            {
                objITaskTemplateView.Last_Permormed_Date = DateTime.Now;
                objITaskTemplateView.Last_Permormed_On = 0;
            }
        }

        private void initValues()
        {
            if (objITaskTemplateView.keyID > 0)
            {
                objDS = objTaskTemplateModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {

                    DataRow objDR_CanEdit = objDS.Tables[1].Rows[0];
                    DataRow objDR = objDS.Tables[0].Rows[0];

                    if (Convert.ToBoolean(objDR_CanEdit["CanEdit"]) == false)
                    {
                        if (ClassLibraryMVP.General.Mode.EDIT == objITaskTemplateView.Mode)
                        {
                            Common.DisplayErrors(-4300);
                        }
                    }

                    objITaskTemplateView.TaskTemplateName = objDR["Task_Name"].ToString();
                    objITaskTemplateView.Template_ID = Convert.ToInt32(objDR["Template_ID"]);
                    objITaskTemplateView.Description = objDR["Task_Description"].ToString();
                    objITaskTemplateView.Schedule_By = Convert.ToInt32(objDR["Task_Schedule_By_ID"]);

                    objITaskTemplateView.Cost = Convert.ToInt32(objDR["Cost"]);

                    objITaskTemplateView.Vehicle_Manufacturer_ID  = Convert.ToInt32(objDR["Manufacturer_ID"]);

                    FillVehicleDDL_For(" Manufacturer_ID=" + Convert.ToInt32(objDR["Manufacturer_ID"]));

                    objITaskTemplateView.Vehicle_Model_ID = Convert.ToInt32(objDR["Vehicle_Model_ID"]);

                    objITaskTemplateView.Task_Completion_ID = Convert.ToInt32(objDR["Task_Completion_ID"]);
                    objITaskTemplateView.Repair_Service_ID = Convert.ToInt32(objDR["Service_ID"]);

                    objITaskTemplateView.Alert_Before_Days = Convert.ToInt32(objDR["Alert_Before_Days_Calculated"]);
                    objITaskTemplateView.Alert_Before_Value = Convert.ToInt32(objDR["Alert_Before_Value_Calculated"]);

                    objITaskTemplateView.Is_Days_Selected = Convert.ToBoolean(objDR["Is_Days_Selected"]);

                    if (Convert.ToBoolean(objDR["Is_Days_Selected"]))
                    {
                        objITaskTemplateView.DueOn_Days = Convert.ToInt32(objDR["Due_On_Days"]);
                    }
                    else
                    {
                        objITaskTemplateView.DueOn_Days = Convert.ToInt32(objDR["Month_Value"]);
                    }
                    objITaskTemplateView.Is_Custom = Convert.ToBoolean(objDR["Is_Custom"]);
                    objITaskTemplateView.DueOn_Value = Convert.ToInt32(objDR["Due_On_Value"]);
                    objITaskTemplateView.Repair_Service_Category_ID = Convert.ToInt32(objDR["Service_Category_ID"]);

                    FillDDL_For(" Service_Category_ID=" + Convert.ToInt32(objDR["Service_Category_ID"]));

                    objITaskTemplateView.Repair_Service_ID = Convert.ToInt32(objDR["Service_ID"]);

                    if (Convert.ToInt32(objDR["Branch_ID"]) > 0)
                    {
                        objITaskTemplateView.To_Be_Worked_At_Type = "Location:";
                        objITaskTemplateView.SetLocationId(objDR["To_Be_Worked_At_Name"].ToString(), objDR["To_Be_Worked_At_ID"].ToString());

                        objITaskTemplateView.Branch_Id = Convert.ToInt32(objDR["Branch_ID"]);
                        objITaskTemplateView.Vendor_Id = 0;
                    }
                    else
                    {
                        objITaskTemplateView.To_Be_Worked_At_Type = "Preffered Vendor:";
                        objITaskTemplateView.SetVendorId(objDR["To_Be_Worked_At_Name"].ToString(), objDR["To_Be_Worked_At_ID"].ToString());

                        objITaskTemplateView.Vendor_Id = Convert.ToInt32(objDR["Vendor_ID"]);
                        objITaskTemplateView.Branch_Id = 0;
                    }

                    objITaskTemplateView.CompanyWorkShop = Convert.ToInt32(objDR["WorkedAtCompanyWorkShop"].ToString());

                }
            }
        }




        private void Get_DropDown()
        {
            objDS = objTaskTemplateModel.FillValues();
            objITaskTemplateView.Bind_ddl_Task_Template = objDS.Tables[0];
            objITaskTemplateView.Bind_ddl_Repair_Service_Category = objDS.Tables[1];
            objITaskTemplateView.Bind_rblst_Schedule_By = objDS.Tables[2];
            objITaskTemplateView.Bind_ddl_Completion = objDS.Tables[3];
            objITaskTemplateView.Bind_ddl_VehicleManufacturer = objDS.Tables[4];
        }

        public void FillVehicleDDL_For(string Where_Condition)
        {
            objDS = objTaskTemplateModel.FillVehicleDDL_For(Where_Condition);

            objITaskTemplateView.Bind_ddl_VehicleModel = objDS.Tables[0];
        }

        public void FillDDL_For(string Where_Condition)
        {
            objDS = objTaskTemplateModel.FillDDL_For(Where_Condition);

            objITaskTemplateView.Bind_ddl_Repair_Service = objDS.Tables[0];
        }

        public void Save()
        {
            base.DBSave();
            // objTaskTemplateModel.Save();
        }
    }
}

