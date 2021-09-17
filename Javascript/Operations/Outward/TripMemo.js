// JScript File
function AllowToSave()
{
  
   var ATS = false;
   var lbl_Errors = document.getElementById('lbl_Errors');
   var ddl_Driver1 = document.getElementById('DDLDriver_txtBoxDDLDriver');
   var hdnSelectedMemoCount = document.getElementById('hdnSelectedMemoCount');
   var DDL_Vehicle = document.getElementById('DDLVehicleSearch_ddl_Vehicle');
   var txt_vehicle_search = document.getElementById('DDLVehicleSearch_txt_Vehicle_Last_4_Digits');
   
    if (DDL_Vehicle.options.length == 0)
    {
        lbl_Errors.innerText = "Please Select Vehicle No";
        txt_vehicle_search.disabled=false;
        txt_vehicle_search.focus();
    }
    else if (ddl_Driver1.value == '')
    {
        lbl_Errors.innerText = "Please Enter Driver 1";
        ddl_Driver1.focus();
    }
    else if (val(hdnSelectedMemoCount.value) == 0)
    {
        lbl_Errors.innerText = "Please Select atleast One Manifest";
    }
    else
        ATS = true;

    return ATS;
}
//=================================================================================================
function Add_Driver_Window()
{
var hdn_Driver_path = document.getElementById('hdnDriverpath');

var w = screen.availWidth;
var h = screen.availHeight;
var popW = (w-100);
var popH = (h-100);
var leftPos = (w-popW)/2;
var topPos = (h-popH)/2;

if(hdn_Driver_path.value == '')
  {
  alert('You Don"t Have Rights to Add Driver');
  }
else
  {
  window.open(hdn_Driver_path.value, 'memo', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
  return false;
  }

return false;
}
//=================================================================================================
function Check_All(chk,gridname)
{
var grid = document.getElementById(gridname);
var checkbox,TotalGC,TotalArticles,TotalActualWT;
var CrossingCost,DeliveryCommision,ToPayCollection;
var sum_CrossingCost=0,sum_DeliveryCommision=0,sum_ToPayCollection=0;
var i,j=0;
var sum_TotalGC=0,sum_TotalArticles=0,sum_TotalActualWT=0;

var txt_TotalArticle = document.getElementById('txt_TotalArticle');
var txt_TotalArticleWT = document.getElementById('txt_TotalArticleWT');
var txt_TotalGC = document.getElementById('txt_TotalGC');
var ddl_LHPOType=document.getElementById('ddlTripMemoType');  
var hdn_TotalArticle= document.getElementById('hdn_TotalArticle');
var hdn_TotalArticleWT = document.getElementById('hdn_TotalArticleWT');
var hdn_Total_No_of_GC = document.getElementById('hdn_TotalGC');
var hdnSelectedMemoCount= document.getElementById('hdnSelectedMemoCount');

var max = (grid.rows.length - 1);
for(i=1;i<grid.rows.length;i++)
  {
  checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
  TotalGC = grid.rows[i].cells[5].getElementsByTagName('input');
  TotalArticles = grid.rows[i].cells[6].getElementsByTagName('input');
  TotalActualWT = grid.rows[i].cells[7].getElementsByTagName('input');          

  if(checkbox[0].type = 'checkbox')
    {
    checkbox[0].checked = chk.checked;
    }
  if(chk.checked == true)
    {
    if(TotalArticles[0].type =='text')
      {
      sum_TotalArticles = sum_TotalArticles + val(TotalArticles[0].value);
      }
    if(TotalActualWT[0].type =='text')
      {
      sum_TotalActualWT = sum_TotalActualWT + val(TotalActualWT[0].value);
      }
    if(TotalGC[0].type =='text')
      {
      sum_TotalGC = sum_TotalGC + val(TotalGC[0].value);
      }
    }//if(chk.checked == true)
  }//for(i=1;i<grid.rows.length;i++)

  txt_TotalGC.value = 0;
  txt_TotalArticle.value = 0;
  txt_TotalArticleWT.value = 0;
  hdn_TotalArticle.value = 0;
  hdn_TotalArticleWT.value = 0;
  hdn_Total_No_of_GC.value = 0;

  if(chk.checked == true)
    {
    hdnSelectedMemoCount.value = max;
    txt_TotalArticle.value = sum_TotalArticles;
    txt_TotalArticleWT.value = sum_TotalActualWT;
    txt_TotalGC.value = sum_TotalGC;
    hdn_TotalArticle.value = sum_TotalArticles;
    hdn_TotalArticleWT.value = sum_TotalActualWT;
    hdn_Total_No_of_GC.value = sum_TotalGC;
    }
}
//====================================================================================================
function Check_Single(chk,gridname)
{
var grid = document.getElementById(gridname);
var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
var row = chk.parentElement.parentElement;
var TotalGC,TotalArticles,TotalActualWT;
var CrossingCost,DeliveryCommision,ToPayCollection;
var sum_CrossingCost,sum_DeliveryCommision,sum_ToPayCollection;
var Total_CrossingCost=0,Total_DeliveryCommision=0,Total_ToPayCollection=0;

var txt_TotalArticle = document.getElementById('txt_TotalArticle');
var txt_TotalArticleWT = document.getElementById('txt_TotalArticleWT');
var txt_TotalGC = document.getElementById('txt_TotalGC');
var ddl_LHPOType=document.getElementById('ddlTripMemoType');  
var hdn_TotalArticle= document.getElementById('hdn_TotalArticle');
var hdn_TotalArticleWT = document.getElementById('hdn_TotalArticleWT');
var hdn_Total_No_of_GC = document.getElementById('hdn_TotalGC');
var hdnSelectedMemoCount= document.getElementById('hdnSelectedMemoCount');

TotalGC  = row.cells[5].getElementsByTagName('input');
TotalArticles  = row.cells[6].getElementsByTagName('input');
TotalActualWT = row.cells[7].getElementsByTagName('input');

if (chk.checked == true)
  {
  hdnSelectedMemoCount.value = val(hdnSelectedMemoCount.value) + 1;
  hdn_Total_No_of_GC.value = val(hdn_Total_No_of_GC.value) + val(TotalGC[0].value);
  hdn_TotalArticle.value = val(hdn_TotalArticle.value) + val(TotalArticles[0].value);
  hdn_TotalArticleWT.value  = val(hdn_TotalArticleWT.value)  + val(TotalActualWT[0].value);
  }
else
  {
  hdnSelectedMemoCount.value = val(hdnSelectedMemoCount.value) - 1;
  hdn_Total_No_of_GC.value = val(hdn_Total_No_of_GC.value) - val(TotalGC[0].value);
  hdn_TotalArticle.value = val(hdn_TotalArticle.value) - val(TotalArticles[0].value);
  hdn_TotalArticleWT.value  = val(hdn_TotalArticleWT.value)  - val(TotalActualWT[0].value);
  }

txt_TotalGC.value = hdn_Total_No_of_GC.value;
txt_TotalArticle.value = hdn_TotalArticle.value;
txt_TotalArticleWT.value  = hdn_TotalArticleWT.value;

if((grid.rows.length-1) == val(hdnSelectedMemoCount.value))
  checkall[0].checked = true;
else
  checkall[0].checked = false;
}


var lst_control_id;
function Search_txtSearch(e,txtbox,lstBox,length)
{
    lst_control_id = lstBox;
    if (txtbox.value == '')
    {
        Clear_listbox(lstBox);
        hidecontrol(document.getElementById(lstBox));
        return;
    }
    var txtvalue = txtbox.value.toUpperCase();
    if(txtvalue.length >= length)
    {
        if (keyCode != 37 && keyCode != 38 && keyCode != 39 && keyCode != 40)
        {
            if(oldvalue != txtvalue)
            {
                Raj.EF.CallBackFunction.CallBack.GetTxtSearchDriver(txtvalue,handleResults);
            }
        }
    }
    if (keyCode == 38 || keyCode == 40)
        listboxupdown(txtbox,lstBox);
}

function handleResults(results)
{
  var list_control = document.getElementById(lst_control_id);
  
  var tot = results.value.Rows.length -1;
  var count = 0;

  for (var count = list_control.options.length-1; count >-1; count--)
  {
    list_control.options[count] = null;
  }

  for (count = 0;count <= tot;count ++)
  { 
    list_control.options[count] = new Option(results.value.Rows[count][results.value.Columns[0].Name],results.value.Rows[count][results.value.Columns[1].Name]); 
  }
  
    if (list_control.options.length == 0)
      hidecontrol(list_control);
    else
      showcontrol(list_control);
}

function On_txtLostFocus(txtbox,list_control,hdn_control)
{
    var txtbox_value = document.getElementById(txtbox).value.toUpperCase();
    var listcontrol = document.getElementById(list_control); 
    var list_control_index = listcontrol.selectedIndex;
    var list_control_value;
    var list_control_text;
    
    hidecontrol(listcontrol);
    if (oldvalue != txtbox_value)
    {
    
        if (list_control_index != -1){
            list_control_value = listcontrol.options[list_control_index].value;
            list_control_text = listcontrol.options[list_control_index].text;
        }
        else{
            list_control_value = '0';
            list_control_text = '';
        }
    
        document.getElementById(hdn_control).value = list_control_value;
        document.getElementById(txtbox).value = list_control_text;
    }    
}

