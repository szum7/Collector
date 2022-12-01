//<script type="text/javascript" src="add_take_borders.js"></script>
document.onkeyup = KeyCheck;
function KeyCheck(e)
{
   var KeyID = (window.event) ? event.keyCode : e.keyCode;
   switch(KeyID)
   {
      //karakter "0"
	  case 48:
      takeborder();
      break;
      //karakter "1"
      case 49:
      addborder("black");
      break;
	  //karakter "2"
	  case 50:
      addborder("blue");
      break;
   }
}
function addborder(color){
  divlength = document.getElementsByTagName("div").length;
  tablelength = document.getElementsByTagName("table").length;
  trlength = document.getElementsByTagName("tr").length;
  tdlength = document.getElementsByTagName("td").length;
  for (i=0; i < divlength; i++){
    if (color == "black"){
	  document.getElementsByTagName("div")[i].style.border = "1px solid black";
	}else if (color == "blue"){
	  document.getElementsByTagName("div")[i].style.border = "1px solid blue";
	}
  }
  for (i=0; i < tablelength; i++){
    if (color == "black"){
	  document.getElementsByTagName("table")[i].style.border = "1px solid black";
	}else if (color == "blue"){
	  document.getElementsByTagName("table")[i].style.border = "1px solid blue";
	}
  }
  for (i=0; i < trlength; i++){
    if (color == "black"){
	  document.getElementsByTagName("tr")[i].style.border = "1px solid black";
	}else if (color == "blue"){
	  document.getElementsByTagName("tr")[i].style.border = "1px solid blue";
	}
  }
  for (i=0; i < tdlength; i++){
    if (color == "black"){
	  document.getElementsByTagName("td")[i].style.border = "1px solid black";
	}else if (color == "blue"){
	  document.getElementsByTagName("td")[i].style.border = "1px solid blue";
	}
  }
}
function takeborder(){
  divlength = document.getElementsByTagName("div").length;
  tablelength = document.getElementsByTagName("table").length;
  trlength = document.getElementsByTagName("tr").length;
  tdlength = document.getElementsByTagName("td").length;
  for (i=0; i < divlength; i++){
    document.getElementsByTagName("div")[i].style.border = "0px solid black";
  }
  for (i=0; i < tablelength; i++){
    document.getElementsByTagName("table")[i].style.border = "0px solid black";
  }
  for (i=0; i < trlength; i++){
    document.getElementsByTagName("tr")[i].style.border = "0px solid black";
  }
  for (i=0; i < tdlength; i++){
    document.getElementsByTagName("td")[i].style.border = "0px solid black";
  }
}