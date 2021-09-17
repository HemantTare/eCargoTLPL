using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;

using Raj.EF.MasterPresenter;
using Raj.EF.MasterView;
using Raj.EC;
/// <summary>
/// Author        : Vajiha
/// Created On    : 19/04/2008
/// Description   : This Page is For Master Family Relation 
/// </summary>




public partial class Master_General_WucFamilyRelation : System.Web.UI.UserControl, IFamilyRelationView
{
    #region ClassVariables
    FamilyRelationPresenter objFamilyRelationPresenter;
    #endregion

    #region ControlsValues
    public string FamilyRelationName
    {
        set
        {
            txt_Family_Relation_Name.Text = value;
        }
        get
        {
            return txt_Family_Relation_Name.Text;
        }
    }

    public int Gender_ID
    {
        set 
        {
            ddl_Gender_Name.SelectedValue = Util.Int2String(value); 
        }
        get 
        { 
           return Util.String2Int(ddl_Gender_Name.SelectedValue); 
           
        }
       
    }
    #endregion

    #region ControlsBind
    public DataSet BindGender
    {
        set
        {
            ddl_Gender_Name.DataTextField = "Gender";
            ddl_Gender_Name.DataValueField = "Gender_ID";
            ddl_Gender_Name.DataSource = value;
            ddl_Gender_Name.DataBind();

        }

    }
    

    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_Family_Relation_Name.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Family Relation  Name";// GetLocalResourceObject("Msg_txt_FamilyRelationName").ToString();
            _isValid = false;
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;

        }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]); 
            //return -1;
        }
    }
    #endregion

    #region OtherProperties

    #endregion

    #region OtherMethods

    #endregion

    #region ControlEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Common ObjCommon = new Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/General/App_LocalResources/WucFamilyRelation.ascx.resx");
        //}
        objFamilyRelationPresenter = new FamilyRelationPresenter(this, IsPostBack);
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objFamilyRelationPresenter.Save();
    }

    #endregion

}
