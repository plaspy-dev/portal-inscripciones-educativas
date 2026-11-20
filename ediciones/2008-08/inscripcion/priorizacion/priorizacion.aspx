<%@ Page Language="C#" AutoEventWireup="true" CodeFile="priorizacion.aspx.cs" Inherits="inscripcion_priorizacion_priorizacion"  EnableEventValidation="false"%>

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
					<!--#include file="../../imagenes/LogoEntidad.aspx" -->
				</td>
			</tr>
			<tr>
				<td><table width="100%" border="1">
						<tr>
							<td id="leftmenu"  width="150" height="280" align="center" valign="top"><span  id="lefttitulo" class="Titulos">VIGENCIA <asp:DropDownList ID="m_CBVigenciasCombo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="m_CBVigenciasCombo_SelectedIndexChanged">
                                </asp:DropDownList></span><br />
								<div id="menu"><%GetMenu();%></div>
							</td>
							<td>&nbsp;
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <anthem:CheckBox ID="CheckBox1" runat="server" Checked="True" Visible="False" /></td>
                                                    <td>
                                                        Entes Territoriales
                                                    </td>
                                                    <td>
                                                        <anthem:DropDownList ID="m_EEntes" runat="server" OnSelectedIndexChanged="m_EEntes_SelectedIndexChanged" AutoCallBack="True">
                                                        </anthem:DropDownList></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_EEntes" Display="Dynamic"
                                                            ErrorMessage="Por favor seleccione un ente" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
                                            </table>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <strong>
                                                        Sin priorizar</strong></td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <strong>
                                                        Priorizados</strong></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <anthem:GridView ID="m_LCSinPriorizar" runat="server" EmptyDataText="No hay proyectos"  AutoUpdateAfterCallBack="true" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" DataSourceID="ObjectDataSource1" UpdateAfterCallBack="False" DataKeyNames="id" OnSelectedIndexChanged="m_LCSinPriorizar_SelectedIndexChanged">
                                                        <RowStyle CssClass="celda" BackColor="White" Wrap="True" />
                                                        <HeaderStyle CssClass="celdaTitulo" BackColor="#E3E4EA" />
                                                        <PagerStyle HorizontalAlign="Right" BackColor="#E3E4EA" />
                                                        <SelectedRowStyle BackColor="#316AC5" CssClass="celdasel" />
                                                        <Columns>
                                                        <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="../../imagenes/edit.gif" />
                                                        </Columns>
                                                        </anthem:GridView>
                                                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select" TypeName="CTablaSinPriorizar"></asp:ObjectDataSource>
                                                    </td>
                                                    <td><anthem:Button ID="m_BIzquierda" runat="server" Text="<<" CausesValidation="False" OnClick="m_BIzquierda_Click" /><br />
                                                        <anthem:Button ID="m_BDerecha" runat="server" Text=">>" CausesValidation="False" OnClick="m_BDerecha_Click" /></td>
                                                    <td>
                                                        <anthem:GridView ID="m_LCPriorizados" runat="server" EmptyDataText="No hay proyectos"  AutoUpdateAfterCallBack="true" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" DataSourceID="ObjectDataSource2" UpdateAfterCallBack="False" DataKeyNames="id" OnRowDataBound="m_LCPriorizados_RowDataBound" OnSelectedIndexChanged="m_LCPriorizados_SelectedIndexChanged">
                                                        <RowStyle CssClass="celda" BackColor="White" Wrap="True" />
                                                        <HeaderStyle CssClass="celdaTitulo" BackColor="#E3E4EA" />
                                                        <PagerStyle HorizontalAlign="Right" BackColor="#E3E4EA" />
                                                        <SelectedRowStyle BackColor="#316AC5" CssClass="celdasel" />
                                                        <Columns>
                                                        <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="../../imagenes/edit.gif" />
                                                        </Columns>
                                                        </anthem:GridView>
                                                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="Select" TypeName="CTablaPriorizados"></asp:ObjectDataSource>
                                                    </td>
                                                    <td>
                                                        <anthem:Button ID="m_ESubir" runat="server" Text="Subir" CausesValidation="False" OnClick="m_ESubir_Click" />
                                                        <br />
                                                        <anthem:Button ID="m_EBajar" runat="server" Text="Bajar" CausesValidation="False" OnClick="m_EBajar_Click" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <anthem:Label ID="m_SMensaje" runat="server"></anthem:Label><br />
                                            <anthem:ImageButton ID="m_BAceptar" runat="server" ToolTip="Guardar" ImageUrl="~/imagenes/save.gif" OnClick="m_BAceptar_Click" PreCallBackFunction="Priorizar_Precallback"/><anthem:ImageButton ID="m_BCancelar" runat="server" ToolTip="Cancelar" ImageUrl="~/imagenes/stop.gif" OnClick="m_BCancelar_Click" CausesValidation="False" /><anthem:ImageButton ID="m_BImprimir" runat="server" ToolTip="Imprimir" ImageUrl="~/imagenes/print.gif" OnClick="m_BImprimir_Click" CausesValidation="False" />
                                            <anthem:DropDownList ID="m_CBImprimir" runat="server">
                                                <asp:ListItem Value="1">Hoja de Cálculo</asp:ListItem>
                                                <asp:ListItem Value="0">En Pantalla</asp:ListItem>
                                            </anthem:DropDownList>&nbsp;
                                            <anthem:ImageButton ID="m_BAceptarP" runat="server" ImageUrl="~/imagenes/print.gif" ToolTip="Imprimir" CausesValidation="False" OnClick="m_BAceptarP_Click" /><anthem:ImageButton ID="m_BCancelarP" runat="server" ToolTip="Cancelar" ImageUrl="~/imagenes/stop.gif" CausesValidation="False" OnClick="m_BCancelarP_Click" />
                                            &nbsp;
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
            <asp:Label ID="m_SVersion" runat="server" Font-Bold="True"></asp:Label><br />
            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" Font-Size="Smaller"
                PostBackUrl="http://www.sdgltda.com">Desarrollado por SDG.</asp:LinkButton></div>
    </form>
</body>
</html>
