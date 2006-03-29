<%@ Page Language="C#" AutoEventWireup="true" CodeFile="asignacion.aspx.cs" Inherits="aprobacion_convenios_asignacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>Portal de inscripciones e informes educativos</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
		<link href="../../estilos/estilos.css" rel="stylesheet" type="text/css" />
		<script type="text/javascript" language="JavaScript1.2" src="../../menu/JSCookMenu.js"></script>
		<script type="text/javascript" language="JavaScript1.2" src="../../menu/ThemeMiniBlack/theme.js"></script>
		<LINK REL="stylesheet" HREF="../../menu/ThemeMiniBlack/theme.css" TYPE="text/css">
		<style>body { background-color: #E3E4EA; };</style>
		<script type="text/javascript" language="JavaScript" src="../../scripts/comun.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" border="0">
			<tr>
				<td>
					<div align="center"><IMG height="60" src="../../Imagenes/LogoEntidad.jpg" width="760"><br />
                        <div style="text-align: left;" class="Titulos">
                        <%GetTitulo();%>
                        </div>
                    </div>
				</td>
			</tr>
			<tr>
				<td><table width="100%" border="1">
						<tr>
							<td width="150" height="280" align="center" valign="top"><span class="Titulos">VIGENCIA <%GetVigencia();%></span><br />
								<div id="menu"><%GetMenu();%></div>
							</td>
							<td>&nbsp;
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <anthem:CheckBox ID="CheckBox1" runat="server" Checked="True" Visible="False" /></td>
                                                    <td>
                                                        <anthem:HiddenField ID="m_nId" runat="server" />
                                                        Grupo
                                                    </td>
                                                    <td>
                                                        <anthem:DropDownList ID="m_CBGrupo" runat="server" AutoCallBack="true" OnSelectedIndexChanged="m_CBGrupo_SelectedIndexChanged">
                                                        </anthem:DropDownList></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_CBGrupo"
                                                            ErrorMessage="Por favor seleccione un Grupo" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <anthem:CheckBox ID="CheckBox2" runat="server" Checked="True" Visible="False" /></td>
                                                    <td>
                                                        Ente Territorial</td>
                                                    <td>
                                                        <anthem:DropDownList ID="m_CBEnte" runat="server" AutoCallBack="true" OnSelectedIndexChanged="m_CBEnte_SelectedIndexChanged">
                                                        </anthem:DropDownList></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_CBEnte"
                                                            ErrorMessage="Por favor seleccione un ente" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
						<tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox3" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Partida preliminar</td>
                                                    <td >
                                                        <anthem:TextBox ID="m_EPartida" runat="server" CssClass="inputnumerico"></anthem:TextBox></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="m_EPartida"
                                                            ErrorMessage="Por favor ingrese una partida" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="m_EPartida"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="100000000000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Currency"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox4" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Fecha</td>
                                                    <td >
                                                        <anthem:TextBox ID="m_EFecha" runat="server"></anthem:TextBox>
                                                        <input id="botonfecha" name="botonfecha"  onclick="setFecha('m_EFecha')" type="button" value="..." />
                                                        </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="m_EFecha"
                                                            ErrorMessage="Por favor ingrese una fecha" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="m_EFecha"
                                                            ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                            SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox5" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Partida definitiva</td>
                                                    <td >
                                                        <anthem:TextBox ID="m_EPartidaDefinitiva" runat="server" CssClass="inputnumerico"></anthem:TextBox></td>
                                                    <td>
                                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="m_EPartidaDefinitiva"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="100000000000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Currency"></asp:RangeValidator>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox6" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Fecha</td>
                                                    <td >
                                                        <anthem:TextBox ID="m_EFechaDefinitiva" runat="server"></anthem:TextBox>
                                                        <input id="Button1" name="botonfecha"  onclick="setFecha('m_EFechaDefinitiva')" type="button" value="..." />
                                                        </td>
                                                    <td>
                                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="m_EFechaDefinitiva"
                                                            ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                            SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        </td>
                                                    <td >
                                                        Certificado aportes de programa de apoyo/82</td>
                                                    <td ><anthem:CheckBox ID="m_CCertificado" runat="server" Checked="True" Enabled="False" /></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox7" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Fecha certificado</td>
                                                    <td >
                                                        <anthem:TextBox ID="m_EFechaCertificado" runat="server" ReadOnly="True"></anthem:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        </td>
                                                    <td >
                                                        <strong>Ampliación de matricula</strong></td>
                                                    <td >
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <anthem:CheckBox ID="CheckBox8" runat="server" Checked="True" Visible="False" /></td>
                                                    <td>
                                                        Actual</td>
                                                    <td>
                                                        <anthem:TextBox ID="m_EActual" runat="server" ReadOnly="True"></anthem:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <anthem:CheckBox ID="CheckBox9" runat="server" Checked="True" Visible="False" /></td>
                                                    <td>
                                                        Anterior</td>
                                                    <td>
                                                        <anthem:TextBox ID="m_EAnterior" runat="server" ReadOnly="True"></anthem:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <anthem:CheckBox ID="CheckBox10" runat="server" Checked="True" Visible="False" /></td>
                                                    <td>
                                                        Variación</td>
                                                    <td>
                                                        <anthem:TextBox ID="m_EVariacion" runat="server" ReadOnly="True"></anthem:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <anthem:CheckBox ID="CheckBox11" runat="server" Checked="True" Visible="False" /></td>
                                                    <td>
                                                        Observaciones</td>
                                                    <td>
                                                        <anthem:TextBox ID="m_EObservaciones" runat="server"></anthem:TextBox></td>
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
                                            <anthem:Button ID="m_BInsertar" runat="server" Text="Insertar" OnClick="m_BInsertar_Click" /><anthem:Button ID="m_BImprimir" runat="server" Text="Imprimir" OnClick="m_BImprimir_Click" CausesValidation="False" />
                                            <anthem:DropDownList ID="m_CBImprimir" runat="server">
                                                <asp:ListItem Value="1">Microsoft Excel</asp:ListItem>
                                                <asp:ListItem Value="0">En Pantalla</asp:ListItem>
                                            </anthem:DropDownList>
                                            <anthem:Button ID="m_BAceptar" runat="server" Text="Aceptar" OnClick="m_BAceptar_Click" /><anthem:Button ID="m_BCancelar" runat="server" Text="Cancelar" OnClick="m_BCancelar_Click" CausesValidation="False" /><anthem:Button ID="m_BEliminar" runat="server" Text="Eliminar" CausesValidation="False" OnClick="m_BEliminar_Click" PreCallBackFunction="Eliminar_PreCallBack"/>
                                            <anthem:Button ID="m_BAceptarP" runat="server" Text="Aceptar" CausesValidation="False" OnClick="m_BAceptarP_Click" PreCallBackFunction="preprintform" /><anthem:Button ID="m_BCancelarP" runat="server" Text="Cancelar" CausesValidation="False" OnClick="m_BCancelarP_Click" />
                                            <anthem:LinkButton ID="m_LBTodos" runat="server" CausesValidation="False" PreCallBackFunction="checkall">Todos</anthem:LinkButton>
                                            <anthem:LinkButton ID="m_LBNinguno" runat="server" CausesValidation="False" PreCallBackFunction="uncheckall">Ninguno</anthem:LinkButton>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                                                TypeName="CTablaEx"></asp:ObjectDataSource>
                                            <anthem:GridView ID="m_LCResultados" runat="server" AllowPaging="True" AllowSorting="True" EmptyDataText="No se encontraron resultados"
                                                Width="100%" DataSourceID="ObjectDataSource1" UpdateAfterCallBack="False" OnSelectedIndexChanged="m_LCResultados_SelectedIndexChanged" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" OnPageIndexChanged="m_LCResultados_PageIndexChanged" OnSorted="m_LCResultados_PageIndexChanged" OnRowDataBound="m_LCResultados_RowDataBound">
                                                <PagerSettings Mode="NextPreviousFirstLast" Position="Top" FirstPageText="&amp;lt;&amp;lt;  Primera p&#225;gina" LastPageText="&#218;ltima p&#225;gina &amp;gt;&amp;gt;" NextPageText="Siguiente &amp;gt;" PreviousPageText="&amp;lt; Anterior"/>
                                                <RowStyle CssClass="celda" BackColor="White" Wrap="True" />
                                                <HeaderStyle CssClass="celdaTitulo" BackColor="#E3E4EA" />
                                                <PagerStyle HorizontalAlign="Right" BackColor="#E3E4EA" />
                                                <Columns>
                                                <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="../../imagenes/edit.jpg" />
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
