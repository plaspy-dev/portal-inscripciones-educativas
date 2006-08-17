<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ieducativas.aspx.cs" Inherits="aplicativo_institucion_ieducativas"  EnableEventValidation="false"%>

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
		<script language="JavaScript" type="text/javascript" src="../../scripts/core.js"></script>
        <script language="JavaScript" type="text/javascript" src="../../scripts/events.js"></script>
        <script language="JavaScript" type="text/javascript" src="../../scripts/css.js"></script>
        <script language="JavaScript" type="text/javascript" src="../../scripts/coordinates.js"></script>
        <script language="JavaScript" type="text/javascript" src="../../scripts/drag.js"></script>
        <script type="text/javascript" language="JavaScript" src="../../scripts/comun.js"></script>
</head>
<body onload="vermenu();">
    <form id="form1" runat="server">
    <table width="100%" align="center" border="0">
	<tr>
		<td>
		<div align="center"><!--#include file="../../imagenes/LogoEntidad.aspx" -->
        </div>
		</td>
	</tr>
	<tr>
		<td>
		<table width="100%" border="1">
		<tr valign="top">
			<td  id="leftmenu" width="150" height="280" align="center" valign="top">
			    <span id="lefttitulo"  class="Titulos">VIGENCIA <asp:DropDownList ID="m_CBVigenciasCombo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="m_CBVigenciasCombo_SelectedIndexChanged">
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
                                <anthem:Button ID="m_BTab1" runat="server" Text="Institución" Width="100px" CssClass="cselected" OnClick="OnTabsClick" CausesValidation="false" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <anthem:Button ID="m_BTab2" runat="server" Text="Jornadas" Width="100px" CssClass="cinftab" OnClick="OnTabsClick" CausesValidation="false" UseSubmitBehavior="False"/>
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
                          <anthem:Label ID="Label1" runat="server" Text="Nueva Institución"></anthem:Label></div>
                    </td>
                </tr>
                <tr>
                    <td class="ccontenido">
                        <anthem:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="View1" runat="server">
                                <table cellspacing="0">
                                    <tr>
                                        <td>
                                            Código DANE<anthem:HiddenField ID="m_nId" runat="server" />
                                        </td>
                                        <td>
                                            <anthem:TextBox ID="m_ECodigo" runat="server" CssClass="inputnumerico"></anthem:TextBox></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_ECodigo" Display="Dynamic"
                                            ErrorMessage="Por favor ingrese un código" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="m_ECodigo" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="1000000000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                                        </td>
                                    </tr>
						            <tr>
                                        <td >
                                            Nombre</td>
                                        <td ><anthem:TextBox ID="m_ENombre" runat="server" CssClass="editcomun ">
                                        </anthem:TextBox></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_ENombre" Display="Dynamic"
                                                ErrorMessage="Por favor ingrese un nombre" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Calendario Escolar</td>
                                        <td >
                                            <anthem:DropDownList ID="m_CBCalendario" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBCalendario_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="m_CBCalendario" Display="Dynamic"
                                                ErrorMessage="Por favor selecccione un calendario" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Tipo de Población</td>
                                        <td ><anthem:DropDownList ID="m_CBTipo" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBTipo_SelectedIndexChanged">
                                        </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="m_CBTipo" Display="Dynamic"
                                                ErrorMessage="Por favor seleccione un tipo" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                                <tr>
                                                    <td >
                                                        Sector Educativo</td>
                                                    <td ><anthem:DropDownList ID="m_CBSector" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBSector_SelectedIndexChanged">
                                                    </anthem:DropDownList></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="m_CBSector" Display="Dynamic"
                                                            ErrorMessage="Por favor seleccione un sector" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Departamento</td>
                                                    <td ><anthem:DropDownList ID="m_CBDepartamento" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBDepartamento_SelectedIndexChanged">
                                                    </anthem:DropDownList></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="m_CBDepartamento" Display="Dynamic"
                                                            ErrorMessage="Por favor seleccione un departamento" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Municipio</td>
                                                    <td ><anthem:DropDownList ID="m_CBMunicipio" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBMunicipio_SelectedIndexChanged">
                                                    </anthem:DropDownList></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="m_CBMunicipio" Display="Dynamic"
                                                            ErrorMessage="Por favor seleccione un municipio" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Modalidad</td>
                                                    <td ><anthem:DropDownList ID="m_CBModalidad" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBModalidad_SelectedIndexChanged">
                                                    </anthem:DropDownList></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="m_CBModalidad" Display="Dynamic"
                                                            ErrorMessage="Por favor seleccione una modalidad" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
                                    <tr>
                                        <td>
                                            Rector</td>
                                        <td><anthem:DropDownList ID="m_CBRector" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBRector_SelectedIndexChanged">
                                        </anthem:DropDownList></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Zona</td>
                                        <td><anthem:DropDownList ID="m_CBZona" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBZona_SelectedIndexChanged">
                                        </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="m_CBZona" Display="Dynamic"
                                                ErrorMessage="Por favor seleccione una zona" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Barrio Inspección</td>
                                        <td>
                                            <anthem:TextBox ID="m_EBarrio" runat="server" CssClass="editcomun ">
                                            </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Dirección</td>
                                        <td>
                                            <anthem:TextBox ID="m_EDireccion" runat="server" CssClass="editcomun ">
                                            </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Teléfonos</td>
                                        <td>
                                            <anthem:TextBox ID="m_ETelefono" runat="server" CssClass="editcomun ">
                                            </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Fax</td>
                                        <td>
                                            <anthem:TextBox ID="m_EFax" runat="server" CssClass="editcomun ">
                                            </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Web</td>
                                        <td>
                                            <anthem:TextBox ID="m_EWeb" runat="server" CssClass="editcomun ">
                                            </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            E-Mail</td>
                                        <td>
                                            <anthem:TextBox ID="m_EMail" runat="server" CssClass="editcomun ">
                                            </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                            </table>
                                    <div style="text-align: right"><anthem:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="m_BSiguiente" runat="server" ToolTip="Siguiente" OnClick="OnNext" CausesValidation="false"/></div>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    Sin desarrollar<br />
                                    <div style="text-align: right"><anthem:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" /></div>
                                </asp:View>
                            </anthem:MultiView>
                            
                            </td>
                            </tr>
                            <tr>
                            <td class="final">                            
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <br />
                                            <anthem:Label ID="m_SMensaje" runat="server"></anthem:Label><br />
                                            <anthem:ImageButton ID="m_BInsertar" ToolTip="Insertar" runat="server" ImageUrl="~/imagenes/save.gif" OnClick="m_BInsertar_Click" /><anthem:ImageButton ID="m_BImprimir" runat="server" ToolTip="Imprimir" ImageUrl="~/imagenes/print.gif" OnClick="m_BImprimir_Click" CausesValidation="False" />
                                            <anthem:DropDownList ID="m_CBImprimir" runat="server">
                                                <asp:ListItem Value="1">Microsoft Excel</asp:ListItem>
                                                <asp:ListItem Value="0">En Pantalla</asp:ListItem>
                                            </anthem:DropDownList>
                                            <anthem:ImageButton ID="m_BAceptar" runat="server" ImageUrl="~/imagenes/save.gif" ToolTip="Guardar" OnClick="m_BAceptar_Click" /><anthem:ImageButton ID="m_BCancelar" ToolTip="Cancelar" runat="server" ImageUrl="~/imagenes/stop.gif" OnClick="m_BCancelar_Click" CausesValidation="False" /><anthem:ImageButton ID="m_BEliminar" runat="server" ImageUrl="~/imagenes/trash.gif" ToolTip="Eliminar" CausesValidation="False" OnClick="m_BEliminar_Click" PreCallBackFunction="Eliminar_PreCallBack"/>
                                            <anthem:ImageButton ID="m_BAceptarP" runat="server" ImageUrl="~/imagenes/print.gif" ToolTip="Imprimir" CausesValidation="False" OnClick="m_BAceptarP_Click" /><anthem:ImageButton ID="m_BCancelarP" runat="server" ToolTip="Cancelar" ImageUrl="~/imagenes/stop.gif" CausesValidation="False" OnClick="m_BCancelarP_Click" />
                                            
                                            
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                                                TypeName="CTablaEx"></asp:ObjectDataSource>
                                            <anthem:GridView ID="m_LCResultados" runat="server" AllowPaging="True" AllowSorting="True" EmptyDataText="No se encontraron resultados"
                                                Width="100%" DataSourceID="ObjectDataSource1" UpdateAfterCallBack="False" OnSelectedIndexChanged="m_LCResultados_SelectedIndexChanged"  AutoUpdateAfterCallBack="true" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" OnPageIndexChanged="m_LCResultados_PageIndexChanged" OnSorted="m_LCResultados_PageIndexChanged" OnRowDataBound="m_LCResultados_RowDataBound">
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