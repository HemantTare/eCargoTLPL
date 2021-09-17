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
/// <summary>
/// Author : Parikshit
/// Created On : 06-Oct-2008
/// Description : Store All Necesaary details which are Required on Login for A particular User 
///             in Session Object.here,We have InitUser Function whcih is called On Login button Click
/// </summary>
/// 

public class UserParam
    {
        private int _userID, _mainID, _profileID, _yearCode, _divisionID,_systemID, _stdBasicFreightUnitId;
    private string _hierarchyCode, _userName, _firstName, _lastName, _mainName, _companyName, _GC_Caption, _LHPO_Caption, _division_name, _LoginTime;
    private DateTime _startDate, _endDate, _bkg_start_time, _bkg_end_time, _TodaysDate;
    private Boolean _isDivisionReq, _isAccTransferReq, _IsMemoSeriesReq, _IsLHPOSeriesReq, _isColoaderReq, _Is_CSA, _allowbooking;
        private Decimal _stdFreightRatePer;
    
        public int UserId
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public int ProfileId
        {
            get { return _profileID; }
            set { _profileID = value; }
        }

        public int MainId
        {
            get { return _mainID; }
            set { _mainID = value; }
        }

        public string MainName
        {
            get { return _mainName; }
            set { _mainName = value; }
        }

        public string HierarchyCode
        {
            get { return _hierarchyCode; }
            set { _hierarchyCode = value; }
        }
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public int YearCode
        {
            get { return _yearCode; }
            set { _yearCode = value; }
        }

        public int DivisionId
        {
            get { return _divisionID; }
            set { _divisionID  = value; }
        }

        public int SystemId
        {
            get { return _systemID; }
            set { _systemID = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

    public DateTime Bkg_Start_Time
        {
            get { return _bkg_start_time; }
            set { _bkg_start_time = value; }
        }

    public DateTime Bkg_End_Time
        {
            get { return _bkg_end_time; }
            set { _bkg_end_time = value; }
        }

    public Boolean AllowBooking
        {
            get { return _allowbooking; }
            set { _allowbooking = value; }
        }

        public String FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

    public String LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }

    public Boolean IsDivisionReq
    {
        get { return _isDivisionReq; }
        set { _isDivisionReq = value; }
    }

    public Boolean IsAccTransferReq
    {
        get { return _isAccTransferReq; }
        set { _isAccTransferReq = value; }
    }
    
    public Boolean IsMemoSeriesReq
    {
        get { return _IsMemoSeriesReq; }
        set { _IsMemoSeriesReq = value; }
    }

    public Boolean IsLHPOSeriesReq
    {
        get { return _IsLHPOSeriesReq; }
        set { _IsLHPOSeriesReq = value; }
    }

    public Boolean IsColoaderReq
    {
        get { return _isColoaderReq; }
        set { _isColoaderReq= value; }
    }

    public int StdBasicFreightUnitId
    {
        get { return _stdBasicFreightUnitId; }
        set { _stdBasicFreightUnitId = value; }
    }

    public Decimal StdFreightRatePer
    {
        get { return _stdFreightRatePer; }
        set { _stdFreightRatePer = value; }
    }
    
    public string CompanyName
    {
        get { return _companyName; }
        set { _companyName = value; }
    }

    public string GC_Caption
    {
        get { return _GC_Caption; }
        set { _GC_Caption = value; }
    }

    public string LHPO_Caption
    {
        get { return _LHPO_Caption; }
        set { _LHPO_Caption = value; }
    }

    public string DivisionName
    {
        get { return _division_name; }
        set { _division_name = value; }
    }

    public bool Is_CSA
    {
        get { return _Is_CSA; }
        set { _Is_CSA = value; }
    }

    public DateTime TodaysDate
    {
        get { return _TodaysDate; }
        set { _TodaysDate = value; }
    }

    public string LoginTime
    {
        get { return _LoginTime; }
        set { _LoginTime = value; }
    }

}

    public class UserManager
    {
        public static void InitUser(int UserId, string HierarchyCode, int MainId,
            int ProfileId, DateTime StartDate, DateTime EndDate, DateTime Bkg_Start_Time, DateTime Bkg_End_Time,
            bool AllowBooking, string UserName, int YearCode, int DivisionId, int SystemId, 
            string FirstName, string LastName, 
            bool IsDivisionReq, bool IsAccTransferReq, bool IsColoaderReq,
            bool IsMemoSeriesRequired, bool IsLHPOSeriesRequired,bool IsCSA,
            int StdBasicFreightUnitId, decimal StdFreightRatePer,
            string MainName,string CompanyName,string GC_Caption,string LHPO_Caption,
            string DivisionName, DateTime TodaysDate, string LoginTime)
        {
            UserParam objUserParam = new UserParam();

            objUserParam.UserId = UserId;
            objUserParam.HierarchyCode = HierarchyCode;
            objUserParam.MainId = MainId;
            objUserParam.ProfileId = ProfileId;
            objUserParam.StartDate = StartDate;
            objUserParam.EndDate = EndDate;
            objUserParam.Bkg_Start_Time = Bkg_Start_Time;
            objUserParam.Bkg_End_Time = Bkg_End_Time;
            objUserParam.AllowBooking = AllowBooking;
            objUserParam.UserName = UserName;
            objUserParam.YearCode = YearCode;
            objUserParam.DivisionId = DivisionId;
            objUserParam.SystemId = SystemId;
            objUserParam.FirstName= FirstName;
            objUserParam.LastName = LastName;
            objUserParam.IsDivisionReq = IsDivisionReq;
            objUserParam.IsAccTransferReq = IsAccTransferReq;
            objUserParam.IsColoaderReq = IsColoaderReq;
            objUserParam.IsMemoSeriesReq = IsMemoSeriesRequired;
            objUserParam.IsLHPOSeriesReq = IsLHPOSeriesRequired;
            objUserParam.StdBasicFreightUnitId = StdBasicFreightUnitId;
            objUserParam.StdFreightRatePer = StdFreightRatePer;
            objUserParam.MainName= MainName;
            objUserParam.CompanyName = CompanyName ;
            objUserParam.GC_Caption = GC_Caption;
            objUserParam.LHPO_Caption = LHPO_Caption;
            objUserParam.DivisionName = DivisionName;
            objUserParam.Is_CSA = IsCSA;
            objUserParam.TodaysDate = TodaysDate;
            objUserParam.LoginTime = LoginTime;

            StateManager.SaveState("UserParam", objUserParam);

            //System.Web.HttpContext.Current.Session["UserParam"] = objUserParam;
        }


        public static UserParam getUserParam()
        {
            UserParam objUserParam;
            //objUserParam = (UserParam)System.Web.HttpContext.Current.Session["UserParam"];
            objUserParam = StateManager.GetState<UserParam>("UserParam");
            return objUserParam;
        }

        public void SetUserSessiontoNothing()
        {
            UserParam objUserParam;
            objUserParam = StateManager.GetState<UserParam>("UserParam");
            objUserParam = null;
        }

    }
