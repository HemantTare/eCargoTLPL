// JScript File

function Check_All(chk)
{              
  var grid = document.getElementById('dg_MemoInformation');
  var checkbox;

  for(var i=1;i<grid.rows.length;i++)
    {
      checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
      if(checkbox[0].type = 'checkbox')
      {
      if (checkbox[0].disabled == false)
        checkbox[0].checked = chk.checked;
      }
    }
}

function Allow_To_Save()
{
return true;
}
