
function ValidateUI_VehicleInformation_For_Truck()
{

var lbl_Errors = document.getElementById('WucVehicle1_WucVehicleInformation1_lbl_Errors');
var txt_Vehicle_No = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Vehicle_No');
var txt_Number_Part1 = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Number_Part1');
var txt_Number_Part2 = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Number_Part2');
var txt_Number_Part3 = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Number_Part3');
var txt_Number_Part4 = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Number_Part4');

var ddl_Vehicle_type = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Vehicle_Type');
var ddl_Vehicle_Body = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Vehicle_Body');
var ddl_Carrier_Category = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Carrier_Category');
var ddl_Manufacturer = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Manufacturer');
var ddl_Model = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Model');
var txt_Yr_Manufacture = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Yr_Manufacture');
var txt_Owner_Name = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Owner_Name');
var ddl_Broker_Name = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Broker_Name_txtBoxddl_Broker_Name');
var ddl_driver1 = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Driver_1_txtBoxddl_Driver_1');
var ddl_driver2 = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Driver_2_txtBoxddl_Driver_2');

var hdn_Vehicle_Category_ID=document.getElementById("WucVehicle1_WucVehicleInformation1_hdn_Vehicle_Category_ID");
var td_BrokerName=document.getElementById("WucVehicle1_WucVehicleInformation1_td_BrokerName");

var ATS = false;

if(Trim(txt_Number_Part1.value).length < 2)
  {
  lbl_Errors.innerHTML ="Truck Number Should Not Be less than 2 characters";
  txt_Number_Part1.focus();
  }
else if(Trim(txt_Number_Part4.value).length < 1)
  {
  lbl_Errors.innerHTML ="Truck Number Should Should have atleast 1 Digit";
  txt_Number_Part4.focus();
  }
 else if (ddl_Vehicle_type.value == 0 &&  control_is_mandatory(ddl_Vehicle_type) == true)
  {
  lbl_Errors.innerHTML = "Please Select Vehicle Type";
  ddl_Vehicle_type.focus();
  }
 else if (ddl_Vehicle_Body.value == '0' &&  control_is_mandatory(ddl_Vehicle_Body) == true)
  {
  lbl_Errors.innerHTML = "Please Select Vehicle Body";
  ddl_Vehicle_Body.focus();
  }
  else if (ddl_Carrier_Category.value == '0' && control_is_mandatory(ddl_Carrier_Category) == true)
  {
  lbl_Errors.innerHTML = "Please Select Carrier Category";
  ddl_Carrier_Category.focus();
  }
else if (ddl_Manufacturer.value == '0' && control_is_mandatory(ddl_Manufacturer) == true)
  {
  lbl_Errors.innerHTML = "Please Select Manufacturer";
  ddl_Manufacturer.focus();
  }
else if (ddl_Model.value == '0' && control_is_mandatory(ddl_Model) == true)
  {
  lbl_Errors.innerHTML = "Please Select Vehicle Model";
  ddl_Model.focus();
  }
else if(txt_Yr_Manufacture.value > 0 && txt_Yr_Manufacture.value.length < 4)
{
     lbl_Errors.innerHTML = "Year of Manufacturer Should be 4 Digits";
     txt_Yr_Manufacture.focus();
}
else if (ddl_driver1.value != '' && ddl_driver2.value != '' && ddl_driver1.value == ddl_driver2.value)
  {
  lbl_Errors.innerHTML = "Driver1 And Driver2 Cannot Be Same";
  }
else if ((hdn_Vehicle_Category_ID.value == '2' || hdn_Vehicle_Category_ID.value == '3') && ddl_Broker_Name.value == '')//Not Own
{
    lbl_Errors.innerHTML = "Please Select Owner Name";
    ddl_Broker_Name.focus();
}
else if (hdn_Vehicle_Category_ID.value == '5' && ddl_Broker_Name.value == '' && control_is_mandatory(td_BrokerName) == true)//Not Own
{
    lbl_Errors.innerHTML = "Please Select Broker Name";
    ddl_Broker_Name.focus();
}
else if (hdn_Vehicle_Category_ID.value == '5' && txt_Owner_Name.value == '' && control_is_mandatory(txt_Owner_Name) == true) //Market
{
   lbl_Errors.innerHTML = "Please Enter Owner Name";
   txt_Owner_Name.focus();
}
else if (hdn_Vehicle_Category_ID.value == '5' && !ValidateWucTDS(lbl_Errors)) //Market
{
  
}
//else if (hdn_Vehicle_Category_ID.value != 1 && ValidateTDS() == false)
//{}
else
  ATS = true;

return ATS;
}

//-------------------------------------------------------------------------------------------

function ValidateTDS()
{

var Chk_Is_Tds = document.getElementById('WucVehicle1_WucVehicleInformation1_Chk_Is_Tds');
var rbl_TDSCertificate = document.getElementById('WucVehicle1_WucVehicleInformation1_rbl_TDSCertificate_1');
var ddl_Tds_Category = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Tds_Category');
var txt_Tds_Rate_Percent = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Tds_Rate_Percent');
var txt_Tds_Exemption_Limit = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Tds_Exemption_Limit');
var lbl_Errors = document.getElementById('WucVehicle1_WucVehicleInformation1_lbl_Errors');

    var ATS = true;
//    var objResource=new Resource('WucVehicle1_WucVehicleInformation1_hdf_ResourecString');
    if ((rbl_TDSCertificate.checked == true) && (Chk_Is_Tds.checked == true))
    {
        if (ddl_Tds_Category.value == 0)
        {
            lbl_Errors.innerHTML = "Please Select TDS Category";//objResource.GetMsg("MsgTDSCategory");
            ddl_Tds_Category.focus();
            ATS = false;
        }
        else if(txt_Tds_Rate_Percent.value <= 0)
        {
            lbl_Errors.innerHTML = "Please Enter TDS Percent";//objResource.GetMsg("MsgTDSPercent");
            txt_Tds_Rate_Percent.focus();
            ATS = false;
        }
//        else if (txt_Tds_Exemption_Limit.value <= 0)
//        {
//            lbl_Errors.innerHTML = "Please Enter TDS Exemption Limit";
//            txt_Tds_Exemption_Limit.focus();
//            ATS = false;
//        }
        else
            ATS = true;
    }
    return ATS;
}

//-----------------------------------------------------------------------------------

function ValidateUI_VehicleInformation_For_Att_Managed_Truck()
{
var hdn_Vehicle_Category_ID=document.getElementById("WucVehicle1_WucVehicleInformation1_hdn_Vehicle_Category_ID");
if (hdn_Vehicle_Category_ID.value == 1) //Owner
  return true;

var lbl_Errors = document.getElementById('WucVehicle1_WucVehicleInformation1_lbl_Errors');
var ddl_Broker_Name = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Broker_Name_txtBoxddl_Broker_Name');


var ATS = false;
//var objResource=new Resource('WucVehicle1_WucVehicleInformation1_hdf_ResourecString');

if (hdn_Vehicle_Category_ID.value!=5 && ddl_Broker_Name.value == '')
  {
  lbl_Errors.innerHTML = "Please Select Broker Name";//objResource.GetMsg("MsgVehicleBroker");  
  ddl_Broker_Name.focus();
  }
else
  ATS = true;
  
return ATS;
}

//-------------------------------------------------------------------------------------

function ValidateUI_VehicleInformation_For_Market_Truck()
{
var hdn_Vehicle_Category_ID=document.getElementById("WucVehicle1_WucVehicleInformation1_hdn_Vehicle_Category_ID");
if (hdn_Vehicle_Category_ID.value != 5) //Owner
  return true;

var lbl_Errors = document.getElementById('WucVehicle1_WucVehicleInformation1_lbl_Errors');
var txt_Owner_Name = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Owner_Name');

var ATS = false;
//var objResource=new Resource('WucVehicle1_WucVehicleInformation1_hdf_ResourecString');


if (hdn_Vehicle_Category_ID.value != 5 && txt_Owner_Name.value == '')
  {
  lbl_Errors.innerHTML = "Please Enter Owner Name";//objResource.GetMsg("MsgVehicleOwner1");  
  txt_Owner_Name.focus();
  }
else if (ValidateWucAddress(lbl_Errors) == false)
  {
  }
else
  ATS = true;
  
return ATS;
}


//function ValidateUI_VehicleInformation_For_Own_Market_Truck()
//{

//var lbl_Errors = document.getElementById('WucVehicle1_WucVehicleInformation1_lbl_Errors');
//var txt_Vehicle_No = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Vehicle_No');
//var txt_Number_Part1 = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Number_Part1');
//var txt_Number_Part2 = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Number_Part2');
//var txt_Number_Part3 = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Number_Part3');
//var txt_Number_Part4 = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Number_Part4');

//var ddl_Vehicle_type = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Vehicle_Type');
//var ddl_Vehicle_Body = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Vehicle_Body');
//var ddl_Carrier_Category = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Carrier_Category');
//var ddl_Manufacturer = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Manufacturer');
//var ddl_Model = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Model');

//var ddl_Driver_1 = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Driver_1_txtBoxddl_Driver_1');
//var ddl_Driver_2 = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Driver_2_txtBoxddl_Driver_2');
//var ddl_Cleaner = document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Cleaner');
//var txt_Yr_Manufacture = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Yr_Manufacture');
//var txt_GPS_Con_Id= document.getElementById('WucVehicle1_WucVehicleInformation1_txt_GPS_Con_Id');
//var ddl_Broker_Name=document.getElementById('WucVehicle1_WucVehicleInformation1_ddl_Broker_Name');

//var hdn_Vehicle_Category_ID=document.getElementById("WucVehicle1_WucVehicleInformation1_hdn_Vehicle_Category_ID");

//var OwnerOrBroker = '';

//if (hdn_Vehicle_Category_ID.value == 1) //1-own,
//  OwnerOrBroker = 'Owner';
//else//2-Managed,3-attached,5--market
//  OwnerOrBroker = 'Broker';
//  
//var ATS = false;

//if(Trim(txt_Number_Part1.value).length < 2)
//  {
//  lbl_Errors.innerHTML ="Truck Number Should Not Be less than 2 characters";
//  txt_Number_Part1.focus();
//  }
//else if(Trim(txt_Number_Part4.value).length < 4)
//  {
//  lbl_Errors.innerHTML ="Truck Number Should Not Be Less Than 4 Digits";
//  txt_Number_Part4.focus();
//  }
// else if (ddl_Vehicle_type.value == 0 || ddl_Vehicle_type.options.length <= 0)
//  {
//  lbl_Errors.innerHTML = "Please Select Vehicle Type";
//  ddl_Vehicle_type.focus();
//  }
//else if (ddl_Vehicle_Body.value == 0 || ddl_Vehicle_Body.options.length <= 0)
//  {
//  lbl_Errors.innerHTML = "Please Select Vehicle Body";
//  ddl_Vehicle_Body.focus();
//  }
// else if (ddl_Carrier_Category.value == 0 || ddl_Carrier_Category.options.length <= 0)
//  {
//  lbl_Errors.innerHTML = "Please Select Carrier Cateogry";
//  ddl_Carrier_Category.focus();
//  }
//else if (ddl_Manufacturer.value == 0 || ddl_Manufacturer.options.length <= 0)
//  {
//  lbl_Errors.innerHTML = "Please Select Manufacturer";
//  ddl_Manufacturer.focus();
//  }
//else if (ddl_Model.value == 0 || ddl_Model.options.length <= 0)
//  {
//  lbl_Errors.innerHTML = "Please Select Vehicle Model";
//  ddl_Model.focus();
//  }
//else if (Trim(txt_Yr_Manufacture.value) && Trim(txt_Yr_Manufacture.value).length < 4)
//  {
//  lbl_Errors.innerHTML = "Year of Manufacturer Should be 4 Digits";
//  txt_Yr_Manufacture.focus();
//  } 

////else if (ddl_Driver_1.value == '')
////  {
////  lbl_Errors.innerHTML = "Please Select Driver 1";
////  ddl_Driver_1.focus();
////  }
////else if (ddl_Driver_2.value == '')
////  {
////  lbl_Errors.innerHTML = "Please Select Driver 2";
////  ddl_Driver_2.focus();
////  }
////else if (ddl_Cleaner.value == 0 || ddl_Cleaner.options.length <= 0)
////  {
////  lbl_Errors.innerHTML = "Please Select Cleaner";
////  ddl_Cleaner.focus();
////  }
//else if (ddl_Broker_Name.value == 0 || ddl_Broker_Name.options.length <= 0)
//  {
//  lbl_Errors.innerHTML = "Please select " + OwnerOrBroker + " Name";   
//  ddl_Broker_Name.focus();
//  }
//else
//  ATS = true;

//return ATS;
//}



function CalculateOdometer()
{
  var hdn_Curr_Odometer = document.getElementById("WucVehicle1_WucVehicleInformation1_hdn_Curr_Odometer");
  var hdn_Old_Curr_Odometer=document.getElementById("WucVehicle1_WucVehicleInformation1_hdn_Old_Curr_Odometer");
  var hdn_Old_Open_Odometer=document.getElementById("WucVehicle1_WucVehicleInformation1_hdn_Old_Open_Odometer");
  var txt_Opening_Odometer=document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Opening_Odometer');
  var lbl_Current_Odometer=document.getElementById('WucVehicle1_WucVehicleInformation1_lbl_Current_Odometer');

  var Old_Curr_Odometer = parseFloat(hdn_Old_Curr_Odometer.value);
  var Old_Open_Odometer = parseFloat(hdn_Old_Open_Odometer.value);
  var Opening_Odometer = parseFloat(txt_Opening_Odometer.value);

  if (isNaN(Old_Curr_Odometer)) Old_Curr_Odometer = 0;
  if (isNaN(Old_Open_Odometer)) Old_Open_Odometer = 0;
  if (isNaN(Opening_Odometer)) Opening_Odometer = 0;

  var New_Current_Odometer=(parseFloat(Old_Curr_Odometer)-parseFloat(Old_Open_Odometer)) + parseFloat(Opening_Odometer)
  hdn_Curr_Odometer.value = parseFloat(New_Current_Odometer);
  lbl_Current_Odometer.innerHTML = parseFloat(New_Current_Odometer);
}

//-----------------------------------------------------------------------------------------

function setfocus(txtbox1,len,callfrom,user_action)
{
var control_to_focus_on;

if (user_action == 'add')
  {
if (callfrom == 1)
  control_to_focus_on = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Number_Part3');
else if (callfrom == 2)
  control_to_focus_on = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Number_Part4');
else if (callfrom == 3)
  control_to_focus_on = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Number_Part1');
 }
else
  {
  if (callfrom == 1)
    control_to_focus_on = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Number_Part3');
  else if (callfrom == 2)
    control_to_focus_on = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Number_Part4');
  else if (callfrom == 3)
    control_to_focus_on = document.getElementById('WucVehicle1_WucVehicleInformation1_txt_Number_Part1');
   }  
  
if (txtbox1.value.length >=len)
  control_to_focus_on.focus();
}