<%@ Page Language="C#" AutoEventWireup="true" CodeFile="asignacion.aspx.cs" Inherits="aprobacion_convenios_asignacion"  EnableEventValidation="false"%>

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
					<div align="center"><!--#include file="../../imagenes/LogoEntidad.aspx" -->
                    </div>
				</td>
			</tr>
			<tr>
				<td><table width="100%" border="1">
						<tr>
							<td id="leftmenu"  width="150" height="280" align="center" valign="top"><span id="lefttitulo"  class="Titulos">VIGENCIA <asp:DropDownList ID="m_CBVigenciasCombo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="m_CBVigenciasCombo_SelectedIndexChanged">
                                </asp:DropDownList></span><br />
								<div id="menu"><%GetMenu();%></div>
							</td>
							<td>&nbsp;
                                <table width="100%">
                                    <tr>
                                        <td>
                                            &nbsp;<table>
                                                <tr>
                                                    <td>
                                            <table cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <anthem:HiddenField ID="m_nId" runat="server" />
                                                        Grupo
                                                    </td>
                                                    <td>
                                                        <anthem:DropDownList ID="m_CBGrupo" runat="server" AutoCallBack="true" OnSelectedIndexChanged="m_CBGrupo_SelectedIndexChanged">
                                                        </anthem:DropDownList></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_CBGrupo" Display="Dynamic"
                                                            ErrorMessage="Por favor seleccione un Grupo" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr style="color: #000000">
                                                    <td>
                                                        Ente Territorial</td>
                                                    <td>
                                                        <anthem:DropDownList ID="m_CBEnte" runat="server" AutoCallBack="true" OnSelectedIndexChanged="m_CBEnte_SelectedIndexChanged">
                                                        </anthem:DropDownList></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_CBEnte" Display="Dynamic"
                                                            ErrorMessage="Por favor seleccione un ente" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
						<tr style="color: #000000">
                                                    <td >
                                                        <strong>
                                                        Partida preliminar</strong></td>
                                                    <td style="font-weight: bold" >
                                                        <anthem:TextBox ID="m_EPartida" runat="server" CssClass="inputnumerico"></anthem:TextBox></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="m_EPartida" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese una partida" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="m_EPartida" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="900000000000000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Currency"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Fecha</td>
                                                    <td >
                                                        <anthem:TextBox ID="m_EFecha" runat="server" CssClass="editfecha"></anthem:TextBox>
                                                        <input id="Button2" name="botonfecha"  onclick="setFecha('m_EFecha')" type="button" value="..." />
                                                        </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="m_EFecha" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese una fecha" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="m_EFecha" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                            SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Partida definitiva</td>
                                                    <td >
                                                        <anthem:TextBox ID="m_EPartidaDefinitiva" runat="server" CssClass="inputnumerico"></anthem:TextBox></td>
                                                    <td>
                                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="m_EPartidaDefinitiva" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="900000000000000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Currency"></asp:RangeValidator>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Fecha</td>
                                                    <td >
                                                        <anthem:TextBox ID="m_EFechaDefinitiva" runat="server" CssClass="editfecha"></anthem:TextBox>
                                                        <input id="Button3" name="botonfecha"  onclick="setFecha('m_EFechaDefinitiva')" type="button" value="..." />
                                                        </td>
                                                    <td>
                                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="m_EFechaDefinitiva" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                            SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Certificado aportes de programa de apoyo/82</td>
                                                    <td ><anthem:CheckBox ID="m_CCertificado" runat="server" Checked="True" Enabled="False" /></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Fecha certificado</td>
                                                    <td >
                                                        <anthem:Label ID="m_SFechaCertificado" runat="server"></anthem:Label></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <strong>Ampliación de matricula</strong></td>
                                                    <td >
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Actual</td>
                                                    <td>
                                                        <anthem:Label ID="m_EActual" runat="server"></anthem:Label></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Anterior</td>
                                                    <td>
                                                        <anthem:Label ID="m_SAnterior" runat="server"></anthem:Label></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Variación</td>
                                                    <td>
                                                        <anthem:Label ID="m_SVariacion" runat="server"></anthem:Label></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Observaciones</td>
                                                    <td>
                                                        <anthem:TextBox ID="m_EObservaciones" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                                    </td>
                                                    <td>
                                                        <anthem:DataList ID="m_DLResumen" runat="server" DataSourceID="ObjectDataSource2">
                                                            <ItemStyle BackColor="White" CssClass="celda" />
                                                            <HeaderStyle CssClass="celdaTitulo" />
                                                        <ItemTemplate>
                                                        <table border="1" style="background-color: #E3E4EA;">
                                                            <tr>
                                                                <td class="celdaTitulo">
                                                                Resumen
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                        <table border="1">
                                                          <tr>
                                                            <td class="celdaTitulo"><b>Partida Asignada</b></td>
                                                            <td style="background-color:White; text-align:right;"><%# CPageTemplate.GetFormatDouble(DataBinder.Eval(Container.DataItem, "Partida Asignada").ToString())%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="celdaTitulo"><b>Partida cubierta</b></td>
                                                            <td style="background-color:White; text-align:right;"><%# CPageTemplate.GetFormatDouble(DataBinder.Eval(Container.DataItem, "Partida cubierta").ToString())%></td>
                                                          </tr>
                                                          <tr>
                                                            <td class="celdaTitulo"><b>Saldo partida</b></td>
                                                            <td style="background-color:White; text-align:right;"><%# CPageTemplate.GetFormatDouble(DataBinder.Eval(Container.DataItem, "Saldo partida").ToString())%></td>
                                                          </tr>
                                                        </table>
                                                        </td>
                                                            </tr>
                                                        </table>
                                                        </ItemTemplate>
                                                        </anthem:DataList>
                                                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="Select"
                                                            TypeName="CResumenAsignacion"></asp:ObjectDataSource>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <anthem:Label ID="m_SMensaje" runat="server"></anthem:Label><br />
                                            <anthem:ImageButton ID="m_BInsertar" ToolTip="Insertar" runat="server" ImageUrl="~/imagenes/save.gif" OnClick="m_BInsertar_Click" /><anthem:ImageButton ID="m_BImprimir" runat="server" ToolTip="Imprimir" ImageUrl="~/imagenes/print.gif" OnClick="m_BImprimir_Click" CausesValidation="False" />
                                            <anthem:DropDownList ID="m_CBImprimir" runat="server">
                                                <asp:ListItem Value="1">Hoja de Cálculo</asp:ListItem>
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
		<div align="right">
            <asp:Label ID="m_SVersion" runat="server" Font-Bold="True"></asp:Label><br />
            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" Font-Size="Smaller"
                PostBackUrl="~/about.aspx">Desarrollado por SDG.</asp:LinkButton></div>
    </form>
</body>
</html>
