<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ampliacion.aspx.cs" Inherits="aprobacion_proyectos_ampliacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>Portal de inscripciones e informes educativos</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>
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
					<div align="center"><!--#include file="../../imagenes/LogoEntidad.aspx" -->
                    </div>
				</td>
			</tr>
			<tr>
				<td><table width="100%" border="1">
						<tr>
							<td width="150" height="280" align="center" valign="top"><span class="Titulos">VIGENCIA <asp:DropDownList ID="m_CBVigenciasCombo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="m_CBVigenciasCombo_SelectedIndexChanged">
                                </asp:DropDownList></span><br />
								<div id="menu"><%GetMenu();%></div>
							</td>
							<td>&nbsp;
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <table cellspacing="0">
                                                <tr>
                                                    <td>
                                                        Ente Territorial<anthem:HiddenField ID="m_nId" runat="server" />
                                                    </td>
                                                    <td>
                                                        <anthem:DropDownList ID="m_CBEnte" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBEnte_SelectedIndexChanged">
                                                        </anthem:DropDownList></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_CBEnte"
                                                            ErrorMessage="Por favor seleccione un ente" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
						<tr>
                                                    <td >
                                                        Codigo del proyecto</td>
                                                    <td >
                                                        <anthem:DropDownList ID="m_CBProyecto" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBProyecto_SelectedIndexChanged">
                                                        </anthem:DropDownList></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_CBEnte"
                                                            ErrorMessage="Por favor seleccione un ente" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Institución educativa</td>
                                                    <td ><anthem:DropDownList ID="m_CBInstitucion" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBInstitucion_SelectedIndexChanged">
                                                    </anthem:DropDownList></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="m_CBInstitucion"
                                                            ErrorMessage="Por favor seleccione una institución" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <strong>Ampliación de matricula</strong></td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Actual</td>
                                                    <td >
                                                        <anthem:TextBox ID="m_EActual" runat="server" CssClass="inputnumerico"></anthem:TextBox></td>
                                                    <td>
                                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="m_EActual"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="1000000000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Anterior</td>
                                                    <td >
                                                        <anthem:Label ID="m_SAnterior" runat="server"></anthem:Label></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Variación</td>
                                                    <td >
                                                        <anthem:Label ID="m_SVariacion" runat="server"></anthem:Label></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Concepto Entidad educativa<anthem:HiddenField ID="m_nIdProyecto" runat="server" /></td>
                                                    <td ><anthem:DropDownList ID="m_CBConcepto" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBConcepto_SelectedIndexChanged">
                                                    </anthem:DropDownList></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Observaciones</td>
                                                    <td >
                                                        <anthem:TextBox ID="m_EObservaciones" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <anthem:Label ID="m_SMensaje" runat="server"></anthem:Label><br />
                                            <anthem:ImageButton ImageUrl="~/imagenes/print.gif" ID="m_BImprimir" runat="server" ToolTip="Imprimir" OnClick="m_BImprimir_Click" CausesValidation="False" />
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
		<div align="right">
            <asp:Label ID="m_SVersion" runat="server" Font-Bold="True"></asp:Label>&nbsp;</div>
    </form>
</body>
</html>
