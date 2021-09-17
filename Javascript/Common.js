
//---------------------------------------------------------------
//Number Check Function
//---------------------------------------------------------------
function Only_Integers(f,evt) //only numeric no dot
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

//---------------------------------------------------------------
//Number Check Function
//---------------------------------------------------------------

function Only_Numbers_With_Comma(f) // Number with Only comma
{
if (window.event.keyCode == 37 || window.event.keyCode == 39 || window.event.keyCode == 9 || window.event.keyCode == 16) return;
f.value = f.value.replace(/[^\,|\d]/g,"");
}

//---------------------------------------------------------------
//Number Check Function
//---------------------------------------------------------------
function Only_Numbers(f,evt) // Number with Only one dot
{
//if (window.event.keyCode == 37 || window.event.keyCode == 39 || window.event.keyCode == 9 || window.event.keyCode == 16) return;
//f.value = f.value.replace(/[^.\d]/g,"");

var charCode = (evt.which) ? evt.which : event.keyCode

if(charCode == 46)
{
    var arr=f.value.split('.');

    if (arr.length > 1)
        {
            return false;
        }
    return true;
}
if (charCode > 31 && (charCode < 48 || charCode > 57))
    return false;

    return true;
}

//---------------------------------------------------------------
function Only_Numbers_With_Minus(f,evt)
{
//if (window.event.keyCode == 37 || window.event.keyCode == 39 || window.event.keyCode == 9 || window.event.keyCode == 16) return;
//f.value = f.value.replace(/[^.\d]/g,"");

var charCode = (evt.which) ? evt.which : event.keyCode

if(charCode == 46 || charCode == 45)
{

//    if(charCode == 45 && f.value.length > 0) return false;

    var arr=f.value.split('.');
    var arrminus=f.value.split('-');

    if (arr.length > 1 || arrminus.length > 1)
        {
            return false;
        }
    return true;
}


if (charCode == 48 || charCode == 49 || charCode == 50 || charCode == 51 || charCode == 52 ||
    charCode == 53 || charCode == 54 || charCode == 55 || charCode == 56 || charCode == 57 ||
    charCode == 45)
 return true;
else
 return false;
}
//---------------------------------------------------------------
function valid(f)
{
if (window.event == null || window.event.keyCode == 37 || window.event.keyCode == 39 || window.event.keyCode == 9 || window.event.keyCode == 16) return;
var old_value = f.value;
if (isNaN(old_value))
f.value = f.value.replace(/[^\.|\d]/g,"");
}
//---------------------------------------------------------------
function Uppercase(Txt_Box)
{
  Txt_Box.value=Txt_Box.value.toUpperCase()
}
//---------------------------------------------------------------
function Only_Numbers_With_Dot(f)  //Number with More than one dot
{
if (window.event.keyCode == 37 || window.event.keyCode == 39 || window.event.keyCode == 9 || window.event.keyCode == 16) return;
f.value = f.value.replace(/[^.\d]/g,"");
}

//---------------------------------------------------------------
//Trim Function trimming all the space from begining and ending
//---------------------------------------------------------------
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


function Resource(hiddenId)
{
    var hdf_ResourceString=document.getElementById(hiddenId);
    this.hiddenText = hdf_ResourceString.value;
}

Resource.prototype.GetMsg = function(key)
{
      var Msg='';
//      var s= this.hiddenText;
      var splitted1=this.hiddenText.split('Ö');
      
      for(var i=0;i<splitted1.length;i++)
      {
        var splitted2 = splitted1[i].split('~');
        if(splitted2[0]==key)
        {
          Msg = splitted2[1];
          break;
        }
      }
      
      return Msg;
}


//*******************************************************************
function  Is_NAN( Control )
    {
        var Value;
        Value=0;
        
        if (isNaN(Control.value)) 
            {
                Control.value = '0.00';
                Value=0;    
            }
        else
            {
                Value =  parseFloat(Control.value);
            }
           
        return Value;
    }
    //*******************************************************************
  function  Is_NAN_Lable( Control )
    {
        var Value;
        Value=0;
        
        if (isNaN(Control.innerHTML )) 
            {
                Control.innerHTML = '0.00';
                Value=0;    
            }
        else
            {
                Value =  parseFloat(Control.innerHTML );
            }
           
        return Value;
    }
    
    //*******************************************************************
    
    function val(value)
        {
            var return_value = 0;
            var return_value = parseFloat(value).toFixed(2);
            if (isNaN(return_value)) return_value = 0;
            return_value = Math.round(return_value*100)/100;
            
            return return_value;
        }
        
function onlycharacters(f)
{
if (window.event.keyCode == 37 || window.event.keyCode == 39 || window.event.keyCode == 9 || window.event.keyCode == 16) return;
f.value = f.value.replace(/[^\|\a-z,A-Z]/g,"");
}
    //*******************************************************************
function Only_characterswithspace(f,evt) //only Character with space,dot  no number or symbol
{

var charCode = (evt.which) ? evt.which : event.keyCode

if (charCode > 64 && charCode < 91 || charCode > 96 && charCode < 123 || charCode == 32 || charCode == 46)
    return true;
    
    return false;
    
}

function roundNumber(num, dec) 
    {
      if (isNaN(num) || num == 0 || num == '') num = '0.00';
	    var result = Math.round(num*Math.pow(10,dec))/Math.pow(10,dec);
	    return 		parseFloat(result).toFixed(2);
    }


//*******************************************************************
function control_is_mandatory(obj)    
    {
    if (obj.style.Mandatory == 'y')
      {
      return true;
      }
    else if (obj.style.Mandatory == 'n')
      {
      return false;
      }      
    else
      {
      return true;
      }
    }
    
   //*******************************************************************
   
   
   
////*******************************************************************
function txtbox_onfocus(txtbox)
{
    txtbox.style.backgroundColor = "yellow";
    txtbox.select();
}
////*******************************************************************

function txtbox_onlostfocus(txtbox, make_value_0)
{
    if (make_value_0 == true)
    {
        valid(txtbox);
        if (txtbox.value == '') txtbox.value =  val(0);
    }
    txtbox.value = txtbox.value.toUpperCase();
    txtbox.style.backgroundColor = "white";
}
////*******************************************************************
      
function Only_AlphaSpaceNumbers(f,evt) // Number with Only one dot
{    
    var charCode = (evt.which) ? evt.which : event.keyCode  
    if ((charCode == 32) || (charCode >= 48 && charCode <= 57) || (charCode > 64 && charCode < 91) || (charCode > 96 && charCode <= 123) || (charCode != evt.keyCode))
       { return true;}
    else
        {return false;} 
}      
     
////*******************************************************************

function Check_Max_Length_For_Multiline(Multiline_TextBox,evt ,Max_Length_Value)
{    
    if (Multiline_TextBox.value.length > Max_Length_Value)
    {
        if(window.event)//IE
            evt.returnValue = false;
        else//Firefox
            evt.preventDefault();
            
        //return false;
    }
    
    //return true;
    
   if(window.event)//IE
        evt.returnValue = true;
    else//Firefox
        evt.preventDefault();
}
////*******************************************************************


function ValidateGST(obj, GSTCode,Is_Casual, lbl_Errors) 
{
    var gstinVal = obj.value;
    var GSTStateCode = GSTCode;
    var Is_Casual_Taxable = Is_Casual;
    
     
    //var reggst = /^([1-9]){2}([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}([0-9]){1}([a-zA-Z]){1}([a-zA-Z0-9]){1}?$/;
    var reggst = /^([0-9]){2}([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}([1-9a-zA-Z]){1}([Z]){1}([a-zA-Z0-9]){1}?$/;
    var reggstsecond5val = /^([a-zA-Z]){5}?$/;
    var reggstthird4val = /^([0-9]){4}?$/;
    if (gstinVal.trim() != '') 
    {
          var first2val = '';
          var second5val = '';
          var third4val = '';
          var fourth1Val = '';
          var fifth1Val = '';
          var sixth1Val = '';
        if(gstinVal.length == 15) 
        {
          first2val = gstinVal.substr(0, 2);
          second5val = gstinVal.substr(2, 5);
          third4val = gstinVal.substr(7, 4);
          fourth1Val = gstinVal.substr(11, 1);
          fifth1Val = gstinVal.substr(12, 1);
          sixth1Val = gstinVal.substr(13, 1);
        }
        if (gstinVal.length < 15) 
        {
            lbl_Errors.innerText = 'Please Enter 15 digit GST No.';
            return false;
        }
        else if (Is_Casual_Taxable==false && first2val != GSTStateCode) 
        {
            lbl_Errors.innerText = 'First 2 digits must be GST state code : '+ first2val;
            return false;
        }
        else if (reggstsecond5val.test(second5val) == false) 
        {
            lbl_Errors.innerText = 'From 3rd digit to 7th digit must be characters :' + second5val;
            return false;
        }
        else if (reggstthird4val.test(third4val) == false) 
        {
            lbl_Errors.innerText = 'From 8th digit to 11th digit must be numbers :'+ third4val;
            return false;
        }
        else if (!isNaN(fourth1Val)) 
        {
            lbl_Errors.innerText = '12th digit must be character :'+ fourth1Val;
            return false;
        }
//        else if (isNaN(fifth1Val)) 
//        {
//            lbl_Errors.innerText = '13th digit must be number :'+ fifth1Val;
//            return false;
//        }
        else if (sixth1Val.toUpperCase() != 'Z') 
        {
            lbl_Errors.innerText = '14th digit must be Z :'+ sixth1Val;
            return false;
        }
        else if (reggst.test(gstinVal) == false){
            lbl_Errors.innerText = 'Please Enter Valid GST No.';
            return false;
        }
        else
            return true;
    }
    else{
      lbl_Errors.innerText = 'Please Enter 15 digit GST No.';
      return false;
    }
}

function ValidateGSTOnType(obj, GSTCode, Is_Casual) 
{

    var gstinVal = obj.value;
    var GSTStateCode = GSTCode;
    var Is_Casual_Taxable = Is_Casual;
    
    //    var reggst = /^([1-9]){2}([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}([0-9]){1}([a-zA-Z]){1}([a-zA-Z0-9]){1}?$/;
    var reggst = /^([0-9]){2}([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}([1-9]){1}([zZ]){1}([a-zA-Z0-9]){1}?$/;
    var reggstsecond5val = /^([a-zA-Z]){5}?$/;
    var reggstthird4val = /^([0-9]){4}?$/;
    if (gstinVal.trim() != '') 
    {
          var first2val = '';
          var second5val = '';
          var third4val = '';
          var fourth1Val = '';
          var fifth1Val = '';
          var sixth1Val = '';
        if(gstinVal.length == 15) 
        {
          first2val = gstinVal.substr(0, 2);
          second5val = gstinVal.substr(2, 5);
          third4val = gstinVal.substr(7, 4);
          fourth1Val = gstinVal.substr(11, 1);
          fifth1Val = gstinVal.substr(12, 1);
          sixth1Val = gstinVal.substr(13, 1);
        }
        //alert('first('+ first2val + ')second(' + second5val+ ')third('+third4val+')fourth('+fourth1Val+')fifth('+fifth1Val+')sixth('+sixth1Val+')');
        
        if (gstinVal.length < 15) 
        {
            alert('Please Enter 15 digit GST No.');
            obj.focus();
            return false;
        }
        else if (Is_Casual_Taxable == false && first2val != GSTStateCode) 
        {
            alert('First 2 digits must be GST state code : '+ first2val);
            obj.focus();
            return false;
        }
        else if (!reggstsecond5val.test(second5val)) 
        {
            alert('From 3rd digit to 7th digit must be characters :' + second5val);
            obj.focus();
            return false;
        }
        else if (!reggstthird4val.test(third4val)) 
        {
            alert('From 8th digit to 11th digit must be numbers :'+ third4val);
            obj.focus();
            return false;
        }
        else if (!isNaN(fourth1Val)) 
        {
            alert('12th digit must be character :'+ fourth1Val);
            obj.focus();
            return false;
        }
        else if (isNaN(fifth1Val)) 
        {
            alert('13th digit must be number :'+ fifth1Val);
            return false;
        }
        else if (sixth1Val.toUpperCase() != 'Z') 
        {
            alert('14th digit must be Z :'+ sixth1Val);
            obj.focus();
            return false;
        }
        else if (!reggst.test(gstinVal)) 
        {
            alert('Invalid GST No.');
            return false;
        }
        else
            return true;
    }
    else 
    {
        alert('Please Enter 15 digit GST No.');
        return false;
    }
}
