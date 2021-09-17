// JScript File

function ValidateUI_VehicleInsurancePremium()
{
    var hdn_VehicleTypeID = document.getElementById('hdn_VehicleTypeID');

    var ATS = false;

   var lbl_Errors = document.getElementById('lbl_Client_Errors');
   var DDL_Vehicle = document.getElementById('WucVehicleSearch1_ddl_Vehicle');
   var txt_vehicle_search = document.getElementById('WucVehicleSearch1_txt_Vehicle_Last_4_Digits');
   var hdn_EngineNo = document.getElementById('hdn_EngineNo');
   var hdn_VehicleId = document.getElementById('hdn_VehicleId');
   var hdn_ChasisNo = document.getElementById('hdn_ChasisNo');
   var ddl_InsuranceCompany = document.getElementById('ddl_InsuranceCompany');
   var ddl_IssuingBranch = document.getElementById('ddl_IssuingBranch');
   var txt_PolicyNo = document.getElementById('txt_PolicyNo');
   var ddl_Agent = document.getElementById('ddl_Agent');
   var txt_EngineNo = document.getElementById('txt_EngineNo');
   var txt_ChasisNo = document.getElementById('txt_ChasisNo');
   var txt_IDV = document.getElementById('txt_IDV');
   var txt_FPPremium = document.getElementById('txt_FPPremium');
   var txt_TPPremium = document.getElementById('txt_TPPremium');
   var txt_LoadingTPP = document.getElementById('txt_LoadingTPP');
   var txt_LoadingFPP = document.getElementById('txt_LoadingFPP');
   var txt_NCBFPP = document.getElementById('txt_NCBFPP');
   var txt_Cheque_No=document.getElementById('txt_Cheque_No');
   var ddl_Bank_Name=document.getElementById('ddl_Bank_Name');
   var radio_1=document.getElementById('rdl_Paid_By_1');
   
   var objResource=new Resource(formname + '_hdf_ResourecString');
 
 if (hdn_VehicleTypeID.value == '')hdn_VehicleTypeID.value ='0';

 if (hdn_VehicleTypeID.value <= '0' && txt_vehicle_search.value == '' && hdn_VehicleId.value < '0')
  {
      lbl_Errors.innerText =  objResource.GetMsg("Msg_VehicleTypeID");
      txt_vehicle_search.focus();
  }
 else if (hdn_VehicleTypeID.value <= '0' && DDL_Vehicle.options.length == 0 && hdn_VehicleId.value < '0')
  {
      lbl_Errors.innerText =  objResource.GetMsg("Msg_VehicleTypeID");
      txt_vehicle_search.focus();
  }
 else if (ddl_InsuranceCompany.value == '0')
  {
      lbl_Errors.innerText =  objResource.GetMsg("Msg_InsuranceCompanyID");
      ddl_InsuranceCompany.focus();
  }
  else if (ddl_IssuingBranch.value == '0')
  {
      lbl_Errors.innerText =  objResource.GetMsg("Msg_IssuingBranchID");
      ddl_IssuingBranch.focus();
  }
  else if (txt_PolicyNo.value == '')
  {
      lbl_Errors.innerText =  objResource.GetMsg("Msg_PolicyNo");
      txt_PolicyNo.focus();
  }
  else if (ddl_Agent.value == '0')
  {
      lbl_Errors.innerText =  objResource.GetMsg("Msg_AgentID");
      ddl_Agent.focus();
  }
  else if (hdn_VehicleTypeID.value <= '0' && txt_EngineNo.value == '')
  {
      lbl_Errors.innerText =  objResource.GetMsg("Msg_EngineNo");
      txt_EngineNo.focus();
  }
  else if (hdn_VehicleTypeID.value <= '0' && txt_EngineNo.value != hdn_EngineNo.value)
  {
      lbl_Errors.innerText =  objResource.GetMsg("Msg_Correct Engine No");
      txt_EngineNo.focus();
  }
  else if (hdn_VehicleTypeID.value <= '0' && txt_ChasisNo.value == '')
  {
      lbl_Errors.innerText = objResource.GetMsg("Msg_txt_ChasisNo"); 
      txt_ChasisNo.focus();
  }
  else if (hdn_VehicleTypeID.value <= '0' && txt_ChasisNo.value != hdn_ChasisNo.value)
  {
      lbl_Errors.innerText =  objResource.GetMsg("Msg_CorrectChasisNo");
      txt_ChasisNo.focus();
  }
  else if (txt_IDV.value == '' || txt_IDV.value <= '0')
  {
      lbl_Errors.innerText =  objResource.GetMsg("Msg_IDV");
      txt_IDV.focus();
  }
  
//  else if (txt_FPPremium.value == '' || txt_FPPremium.value == '0')
//  {
//      lbl_Errors.innerText = "Please Enter First Party Premium";
//      txt_FPPremium.focus();
//  }
   else if (txt_TPPremium.value == '' || txt_TPPremium.value == '0')
  {
      lbl_Errors.innerText =  objResource.GetMsg("Msg_ThirdPartyPremium");
      txt_TPPremium.focus();
  }
//  else if (txt_LoadingTPP.value == '' || txt_LoadingTPP.value == '0')
//  {
//      lbl_Errors.innerText = "Please Enter Loading TPP";
//      txt_LoadingTPP.focus();
//  }
//  else if (txt_LoadingFPP.value == '' || txt_LoadingFPP.value == '0')
//  {
//      lbl_Errors.innerText = "Please Enter Loading FPP";
//      txt_LoadingFPP.focus();
//  }
//  else if (txt_NCBFPP.value == '' || txt_NCBFPP.value == '0')
//  {
//      lbl_Errors.innerText = "Please Enter NCB On FPP";
//      txt_NCBFPP.focus();
//  }
  else if (radio_1.checked == true && txt_Cheque_No.value == '')
  {
      lbl_Errors.innerText =  objResource.GetMsg("Msg_ChequeNo");
      txt_Cheque_No.focus();
  }
  else if (radio_1.checked == true && txt_Cheque_No.value.length < '6')
  {
      lbl_Errors.innerText = objResource.GetMsg("Msg_ChequeNoLength");
      txt_Cheque_No.focus();
  }
  else if (radio_1.checked == true && ddl_Bank_Name.value == '0')
  {
      lbl_Errors.innerText =  objResource.GetMsg("Msg_Bank");
      ddl_Bank_Name.focus();
  }
  else
      ATS = true;

return ATS;
}


function GetVehicleID(txt,val)
{
    var  hdn_Vehicle_ID = document.getElementById('hdn_Vehicle_ID');
    var  hdn_EngineNo = document.getElementById('hdn_EngineNo');
    var  hdn_ChasisNo = document.getElementById('hdn_ChasisNo');
    var  VehicleID;
    VehicleID= val.split("Ö");
    if (val != '')
        {
        hdn_Vehicle_ID.value = VehicleID[0];
        hdn_ChasisNo.value=VehicleID[1];
        hdn_EngineNo.value=VehicleID[2];
        }
}

function Enabled_Disabled_Controls_On_Cheque()
{
        var txt_Cheque_No=document.getElementById('txt_Cheque_No');
        var ddl_Bank_Name=document.getElementById('ddl_Bank_Name');
        var tr_Cheque_Details=document.getElementById('tr_Cheque_Details');
        var tr_Bank_Name=document.getElementById('tr_Bank_Name');
        var radio_1=document.getElementById('rdl_Paid_By_1');
        var radio_0=document.getElementById('rdl_Paid_By_0');

        if(radio_1.checked==true)
        {
           tr_Cheque_Details.style.display = '';
           tr_Bank_Name.style.display = '';
           radio_1.value='1';
        }
        else
        {
            txt_Cheque_No.value='';
            ddl_Bank_Name.selectedIndex=0;
            tr_Cheque_Details.style.display = 'none';
            tr_Bank_Name.style.display = 'none';
            radio_0.value='0';
        }
         
}