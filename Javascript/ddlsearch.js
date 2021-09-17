
// JScript File

var CurrentListID;
var CurrrentTxtID;
var AllowNewText;
function CallBackFunction(res)
{
    var list_control = document.getElementById(CurrentListID);
    var txt_control = document.getElementById(CurrrentTxtID);

    Fill_List(res,list_control,txt_control);
    //alert(res.value);
}
  
function Fill_List(res,lst_Control,txt_Control) 
{
    var rows = res.value.Rows.length;
    var columns = res.value.Columns.length
    
     
//    if(res.value.Rows.length==0)
//    {
//        if(!AllowNewText)
//        {txt_Control.value='';}
//    }
    
   lst_Control.options.length = 0;

    for (var  count = 0; count <= rows-1; count++)
    {          
        var ddl_value="";       
        for(var colcount=1; colcount <= columns-1; colcount++)
        {
            if(ddl_value=="")
                ddl_value = res.value.Rows[count][res.value.Columns[colcount].Name];
            else
                ddl_value = ddl_value + "Ö" +  res.value.Rows[count][res.value.Columns[colcount].Name];
        }
        
        //alert(ddl_value);
        lst_Control.options[count] = new Option(res.value.Rows[count][res.value.Columns[0].Name],ddl_value);
    }
    
     lst_Control.style.visibility  = 'visible';
}




function setfocus(txtControl)
{
    var txtCnt = document.getElementById(txtControl);
    txtCnt.focus();
}

function getText(id)
{
    txtID = 'txtBox'+id;
    var txtCnt = document.getElementById(id);
    return txtCnt.value;
}


function assignListValue(txtControl,lstControl,oldval,isPostBack,injectCall,allownewtext,hdnval,jsfunction)
{
    var txtCnt = document.getElementById(txtControl);
    var lstCnt = document.getElementById(lstControl);
    var hdnCnt = document.getElementById(hdnval);
    
    if(lstCnt.selectedIndex != "-1") 
    {
        txtCnt.value = lstCnt.options[lstCnt.selectedIndex].text;
        hdnCnt.value = lstCnt.options[lstCnt.selectedIndex].value;
        //alert(hdnCnt.value);
        lstCnt.style.visibility  = 'hidden';
    }
    else if (!allownewtext) txtCnt.value='';
        lstCnt.style.visibility  = 'hidden';

    if( injectCall != '') {
        injectCall = injectCall + "('" + txtCnt.value + "','" + hdnCnt.value + "')";
        
        eval(injectCall);
    }
    
    if(lstCnt.selectedIndex == "-1")
    {
        hdnCnt.value = '';
    }
    
    if( isPostBack == true) 
    {
        if( txtCnt.value!=oldval){ __doPostBack(txtControl,"");}
    }
    
     return false;
}


function autosearch(e,txtcnt,control_to_loop,allownewtext,iscallback,tablename,textfield,valuefield,othercolumns,callBackAfter,callBackFunction)
{
    var txtcontrol = document.getElementById(txtcnt);
    var txtbox_value = txtcontrol.value;
   // alert(txtbox_value);
    txtbox_value = txtbox_value.toUpperCase();
   // alert(txtbox_value);
    var control1 = document.getElementById(control_to_loop);
    var txtbox_text_length = txtbox_value.length;
   // alert(txtbox_text_length);

    var control_content;
    var control_content_uc;
    var Record_Found = false;
    CurrentListID = control_to_loop
    CurrrentTxtID=txtcnt;
    AllowNewText=allownewtext;
    //alert(iscallback);
    
    //if (iscallback && (txtbox_text_length == 2))
    //{
        //alert(iscallback);
        //ClassLibrary.UIControl.CallBack.GetSearchResult(txtbox_value,tablename,textfield,valuefield,CallBackFunction);
        
    //}
    
    //var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
    var keyCode = e.keyCode;
    
    if (keyCode == 13)
    {
        if (control1.selectedIndex != -1)
        {
            txtcontrol.value = control1.options[control1.selectedIndex].text;
            control1.style.visibility  = 'hidden';
        }
    }
    
    if (keyCode == 40)
    {
        if (control1.style.visibility != 'hidden')
        {
            if (control1.selectedIndex  != (control1.options.length-1))
            {
                control1.selectedIndex = control1.selectedIndex + 1;
                txtcontrol.value = control1.options[control1.selectedIndex].text;
            }
        }
    }
    
    else if (keyCode == 38)
    {
        if (control1.style.visibility != 'hidden')
        {
            if (control1.selectedIndex > 0)
            {
                control1.selectedIndex = control1.selectedIndex - 1;
                txtcontrol.value = control1.options[control1.selectedIndex].text;
            }
        }
    }
    
    else if ((keyCode >= 97 & keyCode <= 122)||(keyCode >= 48 & keyCode <= 90) || (keyCode == 8) || (keyCode == 32))
    {
   
    
        for (var  count = 0; count <= control1.options.length-1; count++)
        {
             if(control1.options.length==1)
             {
                 Record_Found = false;
                 break;
             }       
            control_content = control1.options[count].text;
            control_content_uc = control_content.toUpperCase();
            var value =  control1.options[count].value;
            if (txtbox_value.substring(0,txtbox_text_length) == control_content_uc.substring(0,txtbox_text_length))
            {
                control1.style.visibility  = 'visible';
                control1.selectedIndex = count;
                Record_Found = true;
                break;
            }
        }
        //alert(Record_Found);
       if (Record_Found == false) 
        {
            //alert(txtbox_text_length);
            var count = control1.options.length-1;
            control1.selectedIndex = -1;
           // alert(count);
            if(iscallback && (txtbox_text_length >= callBackAfter))
            {   
                //alert(txtbox_text_length);
                //alert(callBackAfter);
                
                var cfunction = callBackFunction + "(txtbox_value,tablename,textfield,valuefield,othercolumns,CallBackFunction)";
                eval(cfunction);
                //ClassLibrary.UIControl.CallBack.GetSearchResult(txtbox_value,tablename,textfield,valuefield,othercolumns,CallBackFunction);                
            }
            //control1.style.visibility  = 'hidden';
            
           // if (!allownewtext && !iscallback) txtcontrol.value='';     
           
            if (!allownewtext) txtcontrol.value='';                
            
        }
    }
}


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
    return {
    x:left, y:top
    };
}

function showhidelist(txtControl, lstControl)
{
    var lstCnt = document.getElementById(lstControl);
    var txtCnt = document.getElementById(txtControl);
    if(lstCnt.style.visibility  == 'hidden')
        lstCnt.style.visibility = 'visible';
    else
        lstCnt.style.visibility = 'hidden';
    //txtCnt.focus();
}


function initControl(txtControl,lstControl)
{
    var txtCnt = document.getElementById(txtControl);
    if(txtCnt!=null)
    {
        var ItemPos = getElementPosition(txtCnt);
        var x = ItemPos.x;
        var y = ItemPos.y;
        var lstCnt = document.getElementById(lstControl);
        lstCnt.style.left =x;
        lstCnt.style.top =y+21;
        lstCnt.style.width = txtCnt.style.width;
        lstCnt.style.height = 300;
        lstCnt.style.visibility  = 'hidden';
    }
}

  window.onload = function() { document.onkeydown = register;	document.onkeyup = register;}
 function register(e)
  {
 if (!e) e = window.event;
  var keyInfo = String.fromCharCode(e.keyCode);
 if(e.keyCode==27  )
 {return false;}
  }
  
  // for Show ToolTips where mouseover on DDLserach

function showDropDownToolTip(elementRef,divId,spanId) 
{
  elementRef = document.getElementById(elementRef);

 if ( elementRef.value == '' )
  return;
 
 //Div Control
 var imageRef = document.getElementById(divId);
 imageRef.src= elementRef.value;

 
//Span Control 
 var informationSpanRef = document.getElementById(spanId);
 informationSpanRef.innerHTML = '<b>'+elementRef.value+'</b>';
 
 
 var toolTipRef = document.getElementById(divId);
 toolTipRef.style.top = window.event.clientY + 20;
 toolTipRef.style.left = window.event.clientX;
 toolTipRef.style.display = 'inline';
 
}

//Hide ToolTips where mouseout from DDLSearch

function hideDropDownToolTip(divID)
{
 document.getElementById(divID).style.display = 'none';
}
