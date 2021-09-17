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
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;

/// <summary>
/// Author : Parikshit
/// Created On : 06-Oct-2008
/// Description : Build Navigation Bar With User Rights Checking.
/// </summary>

namespace Raj.EC.Security
{
    public class Menus
    {
       
        private DAL objDAL = new DAL();
        private Common objCommon = new Common();
        private DataSet dsMenuGroup=null;
        private string _path;
        private string _baseUrl;



        public Menus()
        {

            _baseUrl = Util.GetBaseURL();
        }

        public DataSet CheckMenuHeadRights(int UserId)
        {
            DataSet Ds = null;
            SqlParameter[] param =   { objDAL.MakeInParams("@UserId", SqlDbType.Int, 0, UserId)};
            objDAL.RunProc("COM_Adm_Check_MenuHead_Rights", param, ref Ds);
            return Ds;
        }

        private DataSet GetMenuGroups(int UserId,int MenuHeadId)
        {
            DataSet Ds = null;
            SqlParameter[] param =   {objDAL.MakeInParams("@UserId", SqlDbType.Int, 0, UserId ),objDAL.MakeInParams("@MenuHeadId", SqlDbType.Int, 0, MenuHeadId)};
            objDAL.RunProc("COM_Adm_Get_MenuGroups", param, ref Ds);
            return Ds;
        }

        /// <summary>
        /// This Function is called On Every LinkButton Click i.e MASTERS,ADMINISTRATION,...etc
        /// </summary>
        
        public void BuildNavigationBar(ComponentArt.Web.UI.NavBar NavBar, int UserId,int MenuHeadId, int MenuSystemId)
        {

            int i;
            dsMenuGroup = GetMenuGroups(UserId, MenuHeadId);
                     

            DataRow Drow;
            int Count = dsMenuGroup.Tables[0].Rows.Count;

            for (i = 0; i < Count; i++)
            {

                Drow = dsMenuGroup.Tables[0].Rows[i];

                ComponentArt.Web.UI.NavBarItem NavBarItem = new ComponentArt.Web.UI.NavBarItem();
                NavBarItem.Text = Convert.ToString(Drow["Menu_Group_Name"]);
                NavBarItem.DefaultSubItemLookId = "Level2ItemLook";
                NavBarItem.SubGroupCssClass = "LEVEL2GROUP";
                NavBarItem.SubGroupItemSpacing = 5;

                NavBar.Items.Insert(i, NavBarItem);

                CreateMenuItems(UserId, MenuSystemId, Convert.ToInt32(dsMenuGroup.Tables[0].Rows[i]["Menu_Group_Id"]), Convert.ToString(dsMenuGroup.Tables[0].Rows[i]["Menu_Group_Name"]), i, NavBar);
            }
        }


        private void CreateMenuItems(int UserId, int MenuSystemId,int MenuGroupId, string MenuGroupName, int Index, ComponentArt.Web.UI.NavBar NavBar)
        {

            DataTable dtMenuItems = null;


            dtMenuItems = Rights.GetObject().GetMenuItems(MenuGroupId);



            int i = 0;
            int MenuItemId;
            string MenuItemName;
            string LinkUrl;
            DataRow Drow;
            

            int Count = dtMenuItems.Rows.Count;
            if (Count > 0)
            {
                NavBar.Items[0].Expanded = true;
            }

            for (i = 0; i < Count; i++)
            {
                Drow = dtMenuItems.Rows[i];

                MenuItemId = Convert.ToInt32(Drow["Menu_Item_Id"]);
                MenuItemName = Convert.ToString(Drow["Menu_Item_Name"]);

                ComponentArt.Web.UI.NavBarItem NavBarItem = new ComponentArt.Web.UI.NavBarItem();
                NavBarItem.Text = MenuItemName;
                NavBarItem.Look.LeftIconUrl = "bullet.gif";

                NavBar.Items[Index].Items.Insert(i, NavBarItem);

                LinkUrl = Rights.GetObject().GetLinkDetails(MenuItemId).LinkUrl;


                _path = _baseUrl + "/" + LinkUrl + "";


                if (_path.ToString().Contains(".php"))
                {
                    string[] pathArray = _path.Split(new char[] { '/' });
                    _path = pathArray[0] + "//" + pathArray[2] + "/";
                    _path += Rights.GetObject().GetLinkDetails(MenuItemId).LinkUrl + "&function=1&user=" + UserManager.getUserParam().UserId + "&HierarchyCode=" + UserManager.getUserParam().HierarchyCode + "&MainId=" + UserManager.getUserParam().MainId + "&YearCode=" + UserManager.getUserParam().YearCode;
                }


                if (Rights.GetObject().GetLinkDetails(MenuItemId).IsPopupFromLink)
                {
                    NavBarItem.ClientSideCommand = "LoadPopUp('" + _path + "');";
                }
                else
                {
                    NavBarItem.ClientSideCommand = "LoadPage('" + _path + "');";
                }
                
            }



        }

    }
}
