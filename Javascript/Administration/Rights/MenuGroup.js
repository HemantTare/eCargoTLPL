

/// <summary>
/// Author        : Ankit
/// Created On    : 04/10/2008
/// Description   : This Page is For  Administration Menu group
/// </summary>
/// 

// JScript File
function ValidateUI()
{
var ATS = false;
var lbl_Errors = document.getElementById('WucMenuGroup1_lbl_Errors');
var txt_MenuGroupName = document.getElementById('WucMenuGroup1_txt_MenuGroupName');
var txt_SerialNo = document.getElementById('WucMenuGroup1_txt_SerialNo');
var ddl_MenuTypeId = document.getElementById('WucMenuGroup1_ddl_MenuTypeId');
var txt_Description = document.getElementById('WucMenuGroup1_txt_Description');
var ddl_SystemName = document.getElementById('WucMenuGroup1_ddl_SystemName');
var ddl_MenuHeadId = document.getElementById('WucMenuGroup1_ddl_MenuHeadId');

var objResource = new Resource('WucMenuGroup1_hdf_ResourceString');

lbl_Errors.innerText=" ";
 
 if(Trim(txt_MenuGroupName.value) == '')
 {
 lbl_Errors.innerText = objResource.GetMsg("Msg_MenuGroupName") //"Please Enter Menu Group Name";
 txt_MenuGroupName.focus();
 }
 else if(Trim(txt_SerialNo.value) == '')
 {
 lbl_Errors.innerText = objResource.GetMsg("Msg_SerialNo"); // "Please Enter Serial Number"; 
 txt_SerialNo.focus();
 }
 else if(ddl_SystemName.options.lenght <= 0)
 {
 lbl_Errors.innerText = objResource.GetMsg("Msg_SystemName"); //"Please Select System Name";
 ddl_SystemName.focus();
 }
 else if(ddl_MenuHeadId.options.length <= 0)
 {
 lbl_Errors.innerText = objResource.GetMsg("Msg_MenuHead");  //"Please Select Menu Head";
 ddl_MenuHeadId.focus();
 } 
 else if(ddl_MenuTypeId.options.lenght <= 0)
 {
 lbl_Errors.innerText = objResource.GetMsg("Msg_MenuType"); //"Please Select Menu Type";
 ddl_MenuTypeId.focus();
 }
 else if(Trim(txt_Description.value) == '')
 {
 lbl_Errors.innerText = objResource.GetMsg("Msg_Description"); //"Please Enter Description";
 txt_Description.focus();
 }
 
 else 
 ATS = true;

 return ATS;
}

function Trim(sString) 
{
    //Begining space Removes
    while (sString.substring(0,1) == ' ')
        {
             sString = sString.substring(1, sString.length);
        }
    //Ending space Removes
    while (sString.substring(sString.length-1, sString.length) == ' ')
        {
             sString = sString.substring(0,sString.length-1);
        }
     return sString;
}

function valid(f)
{
if (window.event == null || window.event.keyCode == 37 || window.event.keyCode == 39 || window.event.keyCode == 9 || window.event.keyCode == 16) return;
var old_value = f.value;
if (isNaN(old_value))
f.value = f.value.replace(/[^\.|\d]/g,"");
}

function Uppercase(Txt_To_Be_Changed)
{
  Txt_To_Be_Changed.value=Txt_To_Be_Changed.value.toUpperCase()
}



//---------------------------------------------------------------
//Number Check Function
//---------------------------------------------------------------
function Only_Integers(f,evt)
{
//if (window.event.keyCode == 37 || window.event.keyCode == 39 || window.event.keyCode == 9 || window.event.keyCode == 16) return;
//f.value = f.value.replace(/[^.\d]/g,"");

var charCode = (evt.which) ? evt.which : event.keyCode



if(charCode == 46)
{
    return false;
}
if (charCode > 31 && (charCode < 48 || charCode > 57))
    return false;
    return true;
}
