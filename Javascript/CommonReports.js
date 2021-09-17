// JScript File
var btn_nullsession;

//window.onbeforeunload = clearvariables;
window.onload = make_btn_nullsession_invisible;

   //*******************************************************************

function clearvariables()
{
//alert(window.event.clientX);
//alert(window.event.clientY);
if (window.event.clientY < 0)
  {
  get_button_nullsession_clientid();
  btn_nullsession.click();
  }
}

   //*******************************************************************

function make_btn_nullsession_invisible()
{
get_button_nullsession_clientid();
if(btn_nullsession != null)
btn_nullsession.style.display='none';
}