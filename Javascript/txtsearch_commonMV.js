// JScript File
var oldvalue = ''

var Search_Data = new Array;
var keyCode;
function on_keydown(e,txtbox,lstBox)
{
    this.keyCode = e.keyCode;
    var lstBox = document.getElementById(lstBox);
    var txtbox = document.getElementById(txtbox);
    var ret = true;
    
    if (keyCode == 9)
    {
        if(lstBox.selectedIndex > -1){
        var index = lstBox.selectedIndex;
        txtbox.value = lstBox.options[index].text;
        lstBox.value = lstBox.options[index].value;
        }
    } 
    else if (keyCode == 13)
        ret = false;
        
    return ret;
}

function On_LostFocus(txtbox,list_control,hdn_control)
{
    var txtbox_value = document.getElementById(txtbox).value.toUpperCase();
    var list_control = document.getElementById(list_control); 
    var list_control_index = list_control.selectedIndex;
    var list_control_value;
    var list_control_text;
    hidecontrol(list_control);
    
    if (oldvalue != txtbox_value)
    {
        if (list_control_index != -1){
            list_control_value = list_control.options[list_control_index].value;
            list_control_text = list_control.options[list_control_index].text;
            }
        else{
            list_control_value = '0';
            list_control_text = '';
            }
         document.getElementById(hdn_control).value = list_control_value;
         document.getElementById(txtbox).value = list_control_text;   
    }
}

function On_Focus(txtbox,lstBox)
{
    initControl3(txtbox,lstBox);
    txtcontrol = document.getElementById(txtbox);
 
    oldvalue = txtcontrol.value.toUpperCase();
     var lstcontrol = document.getElementById(lstBox);
    if (txtcontrol.value != '' && txtcontrol.readOnly == false) {
        showcontrol(lstcontrol);
    }
}
//*******************************************************************
function listboxonfocus(txt)
{
  document.getElementById(txt).focus();
}
////*******************************************************************
function txtbox_onfocus(txtbox)
{
    txtbox.style.backgroundColor = "yellow";
    txtbox.select();
}
////*******************************************************************
function txtbox_onlostfocus(txtbox)
{
    txtbox.value = txtbox.value.toUpperCase();
    txtbox.style.backgroundColor = "white";
}
//*******************************************************************

function listboxupdown(txt_Control,lst_Control)
{
  var lst_Control = document.getElementById(lst_Control);

  if(keyCode == 38)
  {
    if(lst_Control.selectedIndex == 0 || lst_Control.selectedIndex == -1)
        lst_Control.selectedIndex = lst_Control.options.length -1;
    else
        lst_Control.selectedIndex = lst_Control.selectedIndex - 1;
  }
  else if(keyCode == 40)
  {
      if(lst_Control.selectedIndex == (lst_Control.options.length - 1))
        lst_Control.selectedIndex = 0;
      else
        lst_Control.selectedIndex = lst_Control.selectedIndex + 1;
  }
    txt_Control.value =  lst_Control.options[lst_Control.selectedIndex].text;

}
//*******************************************************************************

function getElementPosition(obj){
    var left = 0;
    var top = 0;    
    while (obj.offsetParent){
        left += obj.offsetLeft;
        top += obj.offsetTop;
        obj = obj.offsetParent;
    }
    
    left += obj.offsetLeft;
    top += obj.offsetTop;

    return {x:left, y:top};
}

function getElementPositionForGrid(obj){

    var left = $("#"+obj).position().left;
    var top = $("#"+obj).position().top;    

    return {x:left, y:top};
}

function initControl1(txtControl,lstControl)
{
    var txtCnt = document.getElementById(txtControl);
    var ItemPos = getElementPosition(txtControl);
    var x = ItemPos.x;
    var y = ItemPos.y;
    var lstCnt = document.getElementById(lstControl);
   
    lstCnt.style.left = x + 'px';
    lstCnt.style.top = y + 17 + 'px';    
}

function initControl3(txtControl,lstControl)
{
    var txtCnt = document.getElementById(txtControl);
    var ItemPos = getElementPositionForGrid(txtControl);
    var x = ItemPos.x;
    var y = ItemPos.y;
    var lstCnt = document.getElementById(lstControl);
   
    lstCnt.style.left = x + 'px';
    lstCnt.style.top = y + 20 + 'px';    
}
//*******************************************************************************

function initControl2(txtControl,lstControl)
{
    var txtCnt = document.getElementById(txtControl);
    var ItemPos = getElementPosition(txtCnt);
    var x = ItemPos.x;
    var y = ItemPos.y;
    var lstCnt = document.getElementById(lstControl);

    lstCnt.style.width = 550 + 'px';
    lstCnt.style.left = 350 + 'px';
    lstCnt.style.top =y+17 + 'px';

    lstCnt.style.visibility  = 'hidden';
}
//********************************************************************************

function Clear_listbox(lstbox)
{
  var selectObject = document.getElementById(lstbox);
  for(var count = selectObject.options.length - 1; count >- 1; count--)
  {
	  selectObject.options[count] = null;
  }
}
function showcontrol(list_control)
{
    list_control.style.visibility= 'visible';
    if(list_control.selectedIndex == -1)
        list_control.selectedIndex = 0;
        
}
function hidecontrol(list_control)
{
    list_control.style.visibility = 'hidden'; 
}
