<%@ Page Language="C#" AutoEventWireup="true" CodeFile="proyecto.aspx.cs" Inherits="seguimiento_proyectos_proyecto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>Portal de inscripciones e informes educativos</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
		<link href="../../estilos/estilos.css" rel="stylesheet" type="text/css" />
		<script type="text/javascript" language="JavaScript1.2" src="../../menu/JSCookMenu.js"></script>
		<script type="text/javascript" language="JavaScript1.2" src="../../menu/ThemeMiniBlack/theme.js"></script>
		<link rel="stylesheet" href="../../menu/ThemeMiniBlack/theme.css" type="text/css" />
		<style type="text/css">body { background-color: #E3E4EA; };</style>
		<script type="text/javascript" language="JavaScript" src="../../scripts/comun.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" border="0">
	<tr>
		<td>
		<!--#include file="../../imagenes/LogoEntidad.aspx" -->
		</td>
	</tr>
	<tr>
		<td>
		<table width="100%" border="1">
		<tr valign="top">
			<td width="150" height="280" align="center" valign="top">
			    <span class="Titulos">VIGENCIA <asp:DropDownList ID="m_CBVigenciasCombo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="m_CBVigenciasCombo_SelectedIndexChanged">
                                </asp:DropDownList></span>
			    <div id="menu"><%GetMenu();%></div>
			</td>
			<td>
			    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
					    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab1" runat="server" Text="Proyecto" Width="100px" CssClass="cselected" OnClick="OnTabsClick" UseSubmitBehavior="False" CausesValidation="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab2" runat="server" Text="Contratos" Width="100px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab3" runat="server" Text="Saldos" Width="100px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab4" runat="server" Text="Documentos" Width="100px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100%; height: 20px;" class="cinf">&nbsp;
                            </td>
                        </tr>
                        </table>
                    </td>
                </tr>
				<tr>
                    <td class="final">
                          <div id="divinstitucion" align="center">
                          <anthem:Label ID="m_STitulo" runat="server" Text="Seleccione un proyecto" UpdateAfterCallBack="False"></anthem:Label></div>
                    </td>
                </tr>
                <tr>
                    <td class="ccontenido">
                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="View1" runat="server">
                                <table cellspacing="0">
                                    <tr>
                                        <td>
                                            Entidad Territorial<anthem:HiddenField ID="m_nId" runat="server" />
                                        </td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBEnte" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBEnte_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_CBEnte"
                                            ErrorMessage="Por favor seleccione una entidad" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
						            <tr>
                                        <td >
                                            Código del proyecto</td>
                                        <td >
                                            <anthem:DropDownList ID="m_LCProyecto" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_LCProyecto_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_LCProyecto"
                                            ErrorMessage="Por favor seleccione un proyecto" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Institución educativa</td>
                                        <td >
                                            <anthem:Label id="m_SInstitucion" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Fecha de aprobación proyecto</td>
                                        <td ><anthem:Label id="m_SFechaAprobacion" runat="server" UpdateAfterCallBack="False">
                                        </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                                <tr>
                                                    <td >
                                                        Observaciones</td>
                                                    <td ><anthem:TextBox ID="m_EObservaciones" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="m_BSiguiente" runat="server" ToolTip="Siguiente" OnClick="OnNext"/></td>
                                    </tr>
                                            </table>
                                     <br />
                                            <anthem:Label ID="m_SMensaje" runat="server"></anthem:Label><br />
                                            <anthem:ImageButton ID="m_BImprimir" runat="server" ToolTip="Imprimir" ImageUrl="~/imagenes/print.gif" OnClick="m_BImprimir_Click" CausesValidation="False" />
                                            <anthem:DropDownList ID="m_CBImprimir" runat="server">
                                                <asp:ListItem Value="1">Microsoft Excel</asp:ListItem>
                                                <asp:ListItem Value="0">En Pantalla</asp:ListItem>
                                            </anthem:DropDownList>
                                            <anthem:ImageButton ID="m_BAceptar" runat="server" ImageUrl="~/imagenes/save.gif" ToolTip="Guardar" OnClick="m_BAceptar_Click" /><anthem:ImageButton ID="m_BCancelar" ToolTip="Cancelar" runat="server" ImageUrl="~/imagenes/stop.gif" OnClick="m_BCancelar_Click" CausesValidation="False" />
                                            <anthem:ImageButton ID="m_BAceptarP" runat="server" ImageUrl="~/imagenes/print.gif" ToolTip="Imprimir" CausesValidation="False" OnClick="m_BAceptarP_Click" /><anthem:ImageButton ID="m_BCancelarP" runat="server" ToolTip="Cancelar" ImageUrl="~/imagenes/stop.gif" CausesValidation="False" OnClick="m_BCancelarP_Click" />
                                            
                                            
                                </asp:View>
                                <asp:View ID="View2" runat="server"><table cellspacing="0">
                                    <tr>
                                        <td >
                                            </td>
                                        <td >
                                            Numero de contrato<anthem:HiddenField ID="m_nIdContrato" runat="server" /></td>
                                        <td ><anthem:TextBox ID="m_EContrato" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="m_EContrato"
                                                ErrorMessage="Por favor ingrese un numero de contrato" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha suscripción contrato</td>
                                        <td><anthem:TextBox ID="m_EFechaS" runat="server" CssClass="editfecha"></anthem:TextBox><input id="botonfecha" name="botonfecha"  onclick="setFecha('m_EFechaS')" type="button" value="..." />
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="m_EFechaS"
                                                ErrorMessage="Por favor ingrese un numero una fecha de suscripción" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="m_EFechaS"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                         </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha acta de inicio contrato</td>
                                        <td><anthem:TextBox ID="m_EActaInicio" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button1" name="botonfecha"  onclick="setFecha('m_EActaInicio')" type="button" value="..." />                                        
                                        </td>
                                        <td>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="m_EActaInicio"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                                    </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Clase de contrato</td>
                                        <td><anthem:DropDownList ID="m_CBClaseContrato" runat="server" AutoCallBack="False" >
                                        </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="m_CBClaseContrato"
                                                ErrorMessage="Por favor seleccione una clase" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Objeto del contrato</td>
                                        <td><anthem:DropDownList ID="m_CBObjeto" runat="server" AutoCallBack="False" >
                                        </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="m_CBObjeto"
                                                ErrorMessage="Por favor seleccione un objeto" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Plazo (incluidos adiciones y suspensiones)</td>
                                        <td>
                                            <anthem:TextBox ID="m_EPlazoContrato" runat="server" CssClass="inputnumerico">
                                            </anthem:TextBox></td>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="m_EPlazoContrato"
                                                ErrorMessage="Por favor ingrese un número aqui" MaximumValue="100000000" MinimumValue="1"
                                                SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha de vencimiento del contrato</td>
                                        <td>
                                            <anthem:Label id="m_SFechaV" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Valor total contratado (incluidos adicionales)</td>
                                        <td>
                                            <anthem:TextBox ID="m_EValorContratado" runat="server" CssClass="inputnumerico">
                                            </anthem:TextBox></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="m_EValorContratado"
                                                ErrorMessage="Por favor ingrese un valor" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator2" ControlToValidate="m_EValorContratado" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1"></asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Valor aportes Entidad educativa al contrato</td>
                                        <td>
                                            <anthem:Label id="m_SValorMen" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Valor aportes municipio al contrato (incluidas adiciones)</td>
                                        <td>
                                            <anthem:Label id="m_SValorMun" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Contratista</td>
                                        <td>
                                            <anthem:TextBox ID="m_EContratista" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="m_EContratista"
                                                ErrorMessage="Por favor ingrese un contratista" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Representante legal</td>
                                        <td>
                                            <anthem:TextBox ID="m_ERepresentante" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Valor avance de obra acumulado a ultimo reporte
                                        </td>
                                        <td>
                                            <anthem:TextBox ID="m_EValorAvance" runat="server" CssClass="inputnumerico">
                                            </anthem:TextBox></td>
                                        <td><asp:RangeValidator ID="RangeValidator5" ControlToValidate="m_EValorAvance" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha del reporte</td>
                                        <td>
                                            <anthem:TextBox ID="m_EFechaR" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button3" name="botonfecha"  onclick="setFecha('m_EFechaR')" type="button" value="..." /></td>
                                        <td>
                                                <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="m_EFechaR"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            % de ejecución sobre el total del valor del contrato</td>
                                        <td>
                                            <anthem:Label id="m_SPEjecucion" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            tipo de reporte
                                        </td>
                                        <td><anthem:DropDownList ID="m_CBTipoR" runat="server" AutoCallBack="false">
                                        </anthem:DropDownList></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Estado del contrato</td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBEstadoContrato" runat="server" AutoCallBack="false">
                                            </anthem:DropDownList></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Observaciones sobre revisión de informes</td>
                                        <td>
                                            <anthem:TextBox ID="m_EObservacionesReporte" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Historial del proyecto
                                        </td>
                                        <td>
                                            <anthem:TextBox ID="m_EHistorial" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Registro fotográfico</td>
                                        <td>
                                            <asp:FileUpload ID="m_ERegistro" runat="server" />
                                            <asp:Button ID="Button2" runat="server" Text="Subir" /></td>
                                        <td>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha ultimo registro</td>
                                        <td>
                                            <anthem:TextBox ID="m_EFechaRegistro" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button4" name="botonfecha"  onclick="setFecha('m_EFechaRegistro')" type="button" value="..." /></td>
                                        <td>
                                            &nbsp;<asp:RangeValidator ID="RangeValidator7" runat="server" ControlToValidate="m_EFechaRegistro"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Interventor</td>
                                        <td>
                                            <anthem:TextBox ID="m_EInterventor" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Cargo</td>
                                        <td>
                                            <anthem:TextBox ID="m_ECargo" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Numero de contrato</td>
                                        <td>
                                            <anthem:Label id="m_SContrato" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha acta de inicio</td>
                                        <td>
                                            <anthem:Label id="m_SFechaActa" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Plazo (incluidas adiciones y suspensiones)</td>
                                        <td>
                                            <anthem:Label id="m_SPlazo" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha vencimiento</td>
                                        <td>
                                            <anthem:Label id="m_SFechaVencimiento" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Observaciones</td>
                                        <td>
                                            <anthem:TextBox ID="m_EObservacionesContrato" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="Select"
                                                TypeName="CContratos"></asp:ObjectDataSource>
                                        </td>
                                        <td>
                                            <anthem:Button id="m_BInsertarContrato" runat="server" Text="Insertar" OnClick="m_BInsertarContrato_Click">
                                            </anthem:Button><anthem:Button id="m_BAceptarContrato" runat="server" Text="Aceptar" OnClick="m_BAceptarContrato_Click"></anthem:Button><anthem:Button
                                                    id="m_BCancelarContrato" runat="server" CausesValidation="false" Text="Cancelar" OnClick="m_BCancelarContrato_Click"></anthem:Button><anthem:Button id="m_BEliminarContrato" runat="server"
                                                        CausesValidation="False" PreCallBackFunction="Eliminar_PreCallBack"
                                                        Text="Eliminar" OnClick="m_BEliminarContrato_Click"></anthem:Button></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior1" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="Button5" runat="server" ToolTip="Siguiente" OnClick="OnNext"/></td>
                                    </tr>
                                </table>
                                    <br />
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                </asp:View>
                                <asp:View ID="View3" runat="server"><table cellspacing="0">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Diferencia valor aprobado proyecto y valor contratado
                                        </td>
                                        <td align="right">
                                            <anthem:Label id="m_SDifProy" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Diferencia valor aporte Entidad educativa y valor contratado Entidad</td>
                                        <td align="right">
                                            <anthem:Label id="m_SDifMen" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Diferencia valor aportes MUN y valor contratado municipio</td>
                                        <td align="right">
                                            <anthem:Label id="m_SDifMun" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Estado</td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBEstadoSaldo" runat="server" AutoCallBack="False">
                                            </anthem:DropDownList></td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Observaciones</td>
                                        <td>
                                            <anthem:TextBox ID="m_EObservacionesSaldo" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="Select"
                                                TypeName="CContratosSaldos"></asp:ObjectDataSource>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior2" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="Button6" runat="server" ToolTip="Siguiente" OnClick="OnNext"/></td>
                                    </tr>
                                </table>
                                    <br />
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                </asp:View>
                                <asp:View ID="View4" runat="server"><table cellspacing="0">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Informe final de gestión</td>
                                        <td>
                                            <anthem:TextBox ID="m_EInforme" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button8" name="botonfecha"  onclick="setFecha('m_EInforme')" type="button" value="..." /></td>
                                        <td>
                                                <asp:RangeValidator ID="RangeValidator8" runat="server" ControlToValidate="m_EInforme"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Acta terminación obra</td>
                                        <td>
                                            <anthem:TextBox ID="m_EActa" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button9" name="botonfecha"  onclick="setFecha('m_EActa')" type="button" value="..." /></td>
                                        <td>
                                            <asp:RangeValidator ID="RangeValidator9" runat="server" ControlToValidate="m_EActa"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Certificado de cumplimiento expedido por la interventoría</td>
                                        <td>
                                            <anthem:TextBox ID="m_EInterventoria" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button10" name="botonfecha"  onclick="setFecha('m_EInterventoria')" type="button" value="..." /></td>
                                        <td>
                                            <asp:RangeValidator ID="RangeValidator10" runat="server" ControlToValidate="m_EInterventoria"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Certificado de cumplimiento secretario de hacienda municipal</td>
                                        <td>
                                            <anthem:TextBox ID="m_EHacienda" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button11" name="botonfecha"  onclick="setFecha('m_EHacienda')" type="button" value="..." /></td>
                                        <td>
                                                <asp:RangeValidator ID="RangeValidator11" runat="server" ControlToValidate="m_EHacienda"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Certificado de cumplimiento municipal</td>
                                        <td>
                                            <anthem:TextBox ID="m_EMunicipio" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button12" name="botonfecha" onclick="setFecha('m_EMunicipio')" type="button" value="..." />
                                            </td>
                                        <td>
                                            <asp:RangeValidator ID="RangeValidator12" runat="server" ControlToValidate="m_EMunicipio"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Observaciones</td>
                                        <td>
                                            <anthem:TextBox ID="m_EObservacionesFinal" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior3" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                            <asp:ImageButton ImageUrl="~/imagenes/save.gif" ID="m_BFinalizarE" runat="server" OnClick="m_BAceptar_Click" ToolTip="Finalizar"/></td>
                                    </tr>
                                </table>
                                    <br />
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                </asp:View>
                            </asp:MultiView>
                            
                            </td>
                            </tr>
                            <tr>
                            <td class="final">                            
                                <table width="100%">
                                    <tr>
                                        <td>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;<anthem:GridView id="m_LCContratos" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                        CellSpacing="1" DataKeyNames="id" DataSourceID="ObjectDataSource2" EmptyDataText="No hay Contratos"
                                        UpdateAfterCallBack="False" OnRowDataBound="m_LCContratos_RowDataBound" OnSelectedIndexChanged="m_LCContratos_SelectedIndexChanged">
                                        <columns>
<asp:CommandField SelectImageUrl="../../imagenes/edit.gif" ButtonType="Image" ShowSelectButton="True"></asp:CommandField>
</columns>
                                        <rowstyle backcolor="White" cssclass="celda" wrap="True" />
                                        <selectedrowstyle backcolor="#316AC5" cssclass="celdasel" />
                                        <pagerstyle backcolor="#E3E4EA" horizontalalign="Right" />
                                        <headerstyle backcolor="#E3E4EA" cssclass="celdaTitulo" />
                                    </anthem:GridView>
                                    <anthem:GridView id="m_LCContratosSaldos" runat="server" AutoGenerateColumns="False" CellPadding="1"
                                        CellSpacing="1" DataKeyNames="id" DataSourceID="ObjectDataSource3" EmptyDataText="No hay Contratos"
                                        UpdateAfterCallBack="False" OnRowDataBound="m_LCContratosSaldos_RowDataBound">
                                    <rowstyle backcolor="White" cssclass="celda" wrap="True" />
                                    <selectedrowstyle backcolor="#316AC5" cssclass="celdasel" />
                                    <pagerstyle backcolor="#E3E4EA" horizontalalign="Right" />
                                    <headerstyle backcolor="#E3E4EA" cssclass="celdaTitulo" />
                                </anthem:GridView>
                                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                                                TypeName="CTablaEx"></asp:ObjectDataSource>
                                            <anthem:GridView ID="m_LCResultados" runat="server" AllowPaging="True" AllowSorting="True" EmptyDataText="No se encontraron resultados"
                                                Width="100%" DataSourceID="ObjectDataSource1" UpdateAfterCallBack="False" OnSelectedIndexChanged="m_LCResultados_SelectedIndexChanged" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" OnPageIndexChanged="m_LCResultados_PageIndexChanged" OnSorted="m_LCResultados_PageIndexChanged" OnRowDataBound="m_LCResultados_RowDataBound">
                                                <PagerSettings Mode="NextPreviousFirstLast" Position="Top" FirstPageImageUrl="~/imagenes/inicio.gif" LastPageImageUrl="~/imagenes/final.gif" NextPageImageUrl="~/imagenes/adelante.gif" PreviousPageImageUrl="~/imagenes/atras.gif" FirstPageText="Primera p&#225;gina" LastPageText="&#218;ltima p&#225;gina" NextPageText="P&#225;gina siguiente" PreviousPageText="P&#225;gina anterior"/>
                                                <RowStyle CssClass="celda" BackColor="White" Wrap="True" />
                                                <HeaderStyle CssClass="celdaTitulo" BackColor="#E3E4EA" />
                                                <PagerStyle HorizontalAlign="Right" BackColor="#E3E4EA" />
                                                <Columns>
                                                <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="../../imagenes/edit.gif" />
                                                </Columns>
                                                <SelectedRowStyle BackColor="#316AC5" CssClass="celdasel" />
                                            </anthem:GridView>
					    <%GetResumen();%>
                                            <div align="right"><anthem:Label ID="m_SResumen" runat="server" ForeColor="Red" UpdateAfterCallBack="False"></anthem:Label></div>
                                            </td>
                                    </tr>
                                </table>
                            </td>
						</tr>
					</table>
					</td>
                </tr>
        </table>
        </td></tr></table>
		<div align="right">
            <asp:Label ID="m_SVersion" runat="server" Font-Bold="True"></asp:Label>&nbsp;</div>
    </form>
</body>
</html>