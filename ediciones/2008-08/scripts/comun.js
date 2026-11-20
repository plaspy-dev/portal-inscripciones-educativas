function Anthem_PreCallBack() {
	var loading = document.createElement("div");
	loading.id = "loading";
	loading.style.color = "white";
	loading.style.backgroundColor = "red";
	loading.style.paddingLeft = "5px";
	loading.style.paddingRight = "5px";
	loading.style.position = "absolute";
	loading.style.right = "1%";
	loading.style.bottom = "1%";
	loading.style.zIndex = "9999";
	loading.innerHTML = "Cargando...";
	document.body.appendChild(loading);
}
function Anthem_PostCallBack() {
	var loading = document.getElementById("loading");
	document.body.removeChild(loading);
}
function Eliminar_PreCallBack(button) {
	return confirm("¿Esta seguro de eliminar este elemento?");
}
function Anthem_Error(resultado) {
	alert('Anthem_Error was invoked with the following error message: ' + resultado.error);
}
function printForm(titulo) {
    var url= "/OrganizationExample22/print.aspx";
    url+="?titulo="+titulo;
    var combo= document.getElementById("m_CBImprimir");
    url+="&modo=" + (combo ? combo.selectedIndex : 0);
    var i=1;
    var iter=1;
    var total=0;
    while(combo=document.getElementById("CheckBox"+i)) {
        if(combo.checked)
            total+=iter;
        iter*=2;
        i++;
    }
    url+="&campos="+total;
    window.open(url);
}
function NoEnviar() {
    return false;
}
// funciones de validacion
function isnb() {
	if (event.keyCode < 48 || event.keyCode > 57) event.returnValue = false;
}
function isfloat(elemento) {
	if (event.keyCode < 46 || event.keyCode > 57)  {
		event.returnValue = false;
	} else {
		if(event.keyCode==46) {
			if(elemento.value.length==0 || elemento.value.indexOf('.')!=-1)
				event.returnValue = false;
		}
	}
}
function qcomas(texto) {
	
	var pos;
	do {
		texto= texto.replace(".", "");
		pos= texto.indexOf(".");
	} while(pos!=-1);
	texto= texto.replace(",", ".");
	return texto;
}
function tcomas(e) {
	var texto= e.value;
	if( !isNaN(e.value)) {
		pos=texto.indexOf('.');
		var decimal= "";
		if( pos!=-1) {
			decimal= texto.substr(pos+1);
			texto= texto.substr(0, pos);
		}
		pos=0;
		var textof=new Array();
		for(i=texto.length-3; i>=0; i-=3) {
			textof[pos]= texto.substr(i, 3);
			pos++;
		}
		if( (i=texto.length%3)) {
			textof[pos]=texto.substr(0, i);
		}
		texto="";
		for( i=textof.length-1; i>=0; i--) {
			texto+= textof[i];
			if(i)
				texto+=".";
		}
		if( decimal.length>1)
			texto+="," + decimal;
		e.value=texto;
	}
}
function scomas(e) {
	e.value=qcomas(e.value);
	e.select();
}
var activeElement=null;
function setFecha( elementName )	{

	var activeElement=null;
	activeElement = document.getElementById(elementName);
	if(window.showModalDialog) {
		activeElement.value = window.showModalDialog("../../estilos/DateSelector.html#", activeElement.value, 'dialogHeight:290px;dialogWidth:310px;center:Yes;help:No;scroll:No;resizable:No;status:No;');
	} else {
	 var w = window.open("../../estilos/DateSelector.html#", "Calendario", 'width=310;height=200;dependent=Yes;center=Yes;scroll=No;resizable=No;status=No;');
	 w.focus();
	}
}
function setFechaEx(elementName )	{

	var activeElement=null;
	activeElement = document.getElementByName(elementName);
	if(window.showModalDialog) {
		activeElement.value = window.showModalDialog("../../estilos/DateSelector.html#", activeElement.value, 'dialogHeight:290px;dialogWidth:310px;center:Yes;help:No;scroll:No;resizable:No;status:No;');
	} else {
	 var w = window.open("../../estilos/DateSelector.html#", "Calendario", 'width=310;height=200;dependent=Yes;center=Yes;scroll=No;resizable=No;status=No;');
	 w.focus();
	}
}
function GetDateSelectorDate() { return activeElement.value; }
function SetDateSelectorDate( dateString ) { activeElement.value = dateString; }
// inicio de funciones de permisos
function updateOtros(checkbox, obj, evaluar) {
    var chkbox;
    var i=1;
    while( chkbox=document.getElementById(obj+i)) {
        chkbox.checked= checkbox.checked;
        i++;
    }
}

function updateTodos(checkbox, obj, evaluar) {
    var chkbox;
    var i=1;
    var todos=false;
    while( chkbox=document.getElementById(obj+i)) {
        if( !chkbox.checked) {
            todos=false;
            break;
        }
        todos= true;
        i++;
    }
    if(chkbox=document.getElementById(obj+0)) {
        chkbox.checked= todos;
    }
}

function DGSelectOrUnselectAllTodos(grdid,obj,objlist){ 
    updateOtros(obj,grdid+"_ctl01__ctl", true);
    DGSelectAll(grdid,objlist,obj.checked);
} 

function DGSelectOrUnselectAllOtros(grdid,obj,objlist){ 
    updateTodos(obj,grdid+"_ctl01__ctl", true);
    DGSelectAll(grdid,objlist,obj.checked);
} 
 
function DGSelectAll(grdid,objid, checkear){ 

    var chkbox; 
    var i=2; 

    chkbox=document.getElementById(grdid + 
               '_ctl0' + i + objid); 

    while(chkbox!=null){ 
        chkbox.checked=checkear; 
        if(chkbox.onclick)
            chkbox.onclick();
        i=i+1; 
        chkbox=document.getElementById(grdid + 
                   '_ctl' + (i<10 ? '0' : '') + i + objid); 
    }
}
// seccion de viabilizacion para buscar el nit
function VerificaNIT(elemento) {
    if(elemento.value.length>5) {
        elemento = document.getElementById("m_BBuscarNIT");
        elemento.onclick();
    }
}
// pertenece a la seccion de inscripcion de proyectos quitar de aqui
function Verificacodigo(elemento) {
    if(elemento.value.length>5) {
        elemento = document.getElementById("m_BVerificar");
        elemento.onclick();
    }        
}
// pertenece a la priorizacion
function Priorizar_Precallback(button) {
	return confirm("¿Esta seguro de continuar con la priorizacion?");
}

// pertenece a la seccion de seguimiento de convenios
function InsertarModificacion_Precalback(button) {
    
    var adicion= document.getElementById("m_EAdicionValor");
    var prorroga= document.getElementById("m_EProrroga");
    if( adicion && prorroga) {
        if( adicion.value!="") {
            adicion= document.getElementById("m_EFechaAdicion");
            if( adicion && adicion.value=="") {
                alert("Por favor ingrese un valor para la fecha de la adicion");
                adicion.focus();
                return false;
            }
        } else if( prorroga.value!="") {
            adicion= document.getElementById("m_EFechaProrroga");
            if( adicion && adicion.value=="") {
                alert("Por favor ingrese un valor para la fecha de la prorroga");
                adicion.focus();
                return false;
            }
        } else {
            alert("por favor ingrese un valor para prorroga o para adicion.");
            adicion.focus();
            return false;
        }
    }
    return true;
}
// para miscelaneos manejo del arbol
function hidetree(id) {
	var e= document.getElementById("t"+id);
	if(e) {
		e.style.display= e.style.display=="" ? "none" : "";
	}
}

function hidemenu(mostrar) {
	var mimenu= document.getElementById("menu");
	if(mimenu) {
		mimenu.style.display= mostrar ? "" : "none";
		if(mimenu= document.getElementById("m_CBVigenciasCombo")) {
			mimenu.style.display= mostrar ? "" : "none";
		}
		if(mimenu= document.getElementById("lefttitulo")) {
			mimenu.style.display=mostrar ? "" : "none";
		}
		if(mimenu= document.getElementById("leftmenu")) {
			mimenu.style.width=mostrar ? "150" : "0";
		}
		if(mimenu= document.getElementById("dfinmenu")) {
			mimenu.innerHTML= mostrar ? "<a href='javascript:hidemenu(false);'>&lt;&lt;</a>" 
			 : "<a href='javascript:hidemenu(true);'>&gt;&gt;</a>";
		}
		SetCookie("showmenu", mostrar ? "1" : "0");
	}
}
function getCookieVal (offset)
{
  var endstr = document.cookie.indexOf (";", offset);
  if (endstr == -1)
    endstr = document.cookie.length;
  return unescape(document.cookie.substring(offset, endstr));
}
function GetCookie (name)
{
  var arg = name + "=";
  var alen = arg.length;
  var clen = document.cookie.length;
  var i = 0;
  while (i < clen) {
    var j = i + alen;
    if (document.cookie.substring(i, j) == arg)
      return getCookieVal (j);
    i = document.cookie.indexOf(" ", i) + 1;
    if (i == 0) break; 
  }
  return "";
}
function SetCookie (name, value)
{
  var argv = SetCookie.arguments;
  var argc = SetCookie.arguments.length;
  var expires = (argc > 2) ? argv[2] : null;
  if(!expires) {
	  expires= new Date();
	  expires.setTime(expires.getTime()+(14*24*60*60*1000));
  }
  var path = (argc > 3) ? argv[3] : "/";
  var domain = (argc > 4) ? argv[4] : null;
  var secure = (argc > 5) ? argv[5] : false;
  document.cookie = name + "=" + escape (value) +
    ((expires == null) ? "" : ("; expires=" + expires.toGMTString())) +
    ((path == null) ? "" : ("; path=" + path)) +
    ((domain == null) ? "" : ("; domain=" + domain)) +
    ((secure == true) ? "; secure" : "");
}










var bIniciando=true;

function hideMe(hide){
    
  var elemento=document.getElementById("boxHandle");
  if(elemento) {
    elemento.style.visibility= hide ? "hidden" : "visible";
  }
}

function vermenu() {

    if(bIniciando) {
        bIniciando=false;
        window.setTimeout("renewSession();", 180000);
        if(ToolMan) {
            var drag = ToolMan.drag();
            if(drag) {
	            var boxHandle = document.getElementById("boxHandle");
	            if(boxHandle)
	                group = drag.createSimpleGroup(boxHandle, document.getElementById("handle"));
	        }
	    }
    }
    hidemenu(GetCookie("showmenu")=="1");
}

function OnAyuda(titulo) {

    var elemento= document.getElementById("myhelpframe");
    if(elemento) {
        var ruta= window.location.pathname;
        ruta= ruta.replace(".aspx", "");
        ruta+='h.aspx';
        ruta+="?c="+titulo;
        elemento.src=ruta;
    }
    hideMe(false);
}
// version 1.19
function renewSession()
{
    var img= document.getElementById("renewSession");
    if(img) {
        img.src= "../../renewSes.aspx?par=" + Math.random();
        // alert(img.src);
        window.setTimeout("renewSession();", 180000);
    }
}


