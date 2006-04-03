<%@ Page Language="C#" AutoEventWireup="true" CodeFile="usuarios.aspx.cs" Inherits="sistema_usuarios_usuarios" %>

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
					<!--#include file="../../imagenes/LogoEntidad.aspx" -->
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
                                            <table width="100%">
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox1" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Usuario<anthem:HiddenField ID="m_nId" runat="server" />
                                                    </td>
                                                    <td >
                                                        <anthem:TextBox ID="m_EUsuario" runat="server"></anthem:TextBox></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_EUsuario"
                                                            ErrorMessage="Por favor ingrese un usuario" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox2" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Grupo</td>
                                                    <td >
                                                        <anthem:DropDownList ID="m_CBGrupo" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBGrupo_SelectedIndexChanged">
                                                        </anthem:DropDownList></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="m_CBGrupo"
                                                            ErrorMessage="Por favor seleccione un grupo" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox3" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Nombre</td>
                                                    <td >
                                                        <anthem:TextBox ID="m_ENombre" runat="server"></anthem:TextBox></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="m_ENombre"
                                                            ErrorMessage="Por favor ingrese un nombre de usuario" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox4" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Contraseña</td>
                                                    <td >
                                                        <anthem:TextBox ID="m_EContrasena" runat="server" CausesValidation="True" TextMode="Password"></anthem:TextBox></td>
                                                    <td>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="m_EConfirmar"
                                                            ControlToValidate="m_EContrasena" ErrorMessage="Las contraseñas no coinciden"></asp:CompareValidator></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        Contraseña (Confirmar)</td>
                                                    <td>
                                                        <anthem:TextBox ID="m_EConfirmar" runat="server" CausesValidation="True" TextMode="Password"></anthem:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <anthem:CheckBox ID="CheckBox5" runat="server" Checked="True" Visible="False" /></td>
                                                    <td>
                                                        Activo</td>
                                                    <td>
                                                        <anthem:CheckBox ID="m_CActivo" runat="server" Checked="True" /></td>
                                                    <td style="height: 22px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox6" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Vigencias</td>
                                                    <td >
                                                        <anthem:DataGrid ID="m_LBVigencias" Runat="server" AutoGenerateColumns=false DataSourceID="ObjectDataSource2">
                                                        <HeaderStyle Font-Bold="True" />
                                                        </anthem:DataGrid>
                                                    </td>
                                                    <td>
                                                        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="Select"
                                                            TypeName="CTablaVigencias"></asp:ObjectDataSource>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        Vigencia de los entes
                                                        <anthem:DropDownList ID="m_CBVigencias" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBVigencias_SelectedIndexChanged">
                                                        </anthem:DropDownList></td>
                                                    <td>
                                                        <anthem:Label ID="m_SDVigencias" runat="server"></anthem:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        </td>
                                                    <td>
                                                        Entes Territoriales</td>
                                                    <td>
                                                    <anthem:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Vertical" Width="100%">
		                                            <anthem:DataGrid ID="m_LBEntes" Runat="server" AutoGenerateColumns=false DataSourceID="ObjectDataSource3" Width="100%">
                                                        <HeaderStyle Font-Bold="True" />
		                                            </anthem:DataGrid></anthem:Panel>
                                                    </td>
                                                    <td>
                                                        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="Select"
                                                            TypeName="CTablaEntes"></asp:ObjectDataSource>
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
