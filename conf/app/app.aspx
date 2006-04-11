<%@ Page Language="C#" AutoEventWireup="true" CodeFile="app.aspx.cs" Inherits="conf_app" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Portal de inscripciones e informes educativos</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>
		<link href="../../estilos/estilos.css" rel="stylesheet" type="text/css" />
		<script type="text/javascript" language="JavaScript1.2" src="../../menu/JSCookMenu.js"></script>
		<script type="text/javascript" language="JavaScript1.2" src="../../menu/ThemeMiniBlack/theme.js">function TABLE1_onclick() {

}

</script>
		<link rel="stylesheet" href="../../menu/ThemeMiniBlack/theme.css" type="text/css" />
		<style type="text/css">body { background-color: #E3E4EA; };</style>
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
							<td align="center" valign="top">&nbsp;
                                <table>
                                    <tr>
                                        <td style="text-align: left">
                                            <table>
                                    <tr>
                                        <td align="left">
                                            Resultados por página</td>
                                        <td align="left">
                                            <asp:DropDownList ID="m_CBPagina" runat="server">
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                <tr>
                                    <td align="left">
                                        Limitar celdas a</td>
                                    <td align="left">
                                        <asp:TextBox ID="m_ELimite" runat="server"></asp:TextBox></td>
                                </tr>
                                </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <strong>Formato de fecha</strong></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            Ejemplo:
                                            <anthem:Label ID="m_SEjemplo" runat="server"></anthem:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        Campo</td>
                                                    <td>
                                                        Sep.</td>
                                                    <td>
                                                        Campo</td>
                                                    <td>
                                                        Sep.</td>
                                                    <td>
                                                        Campo</td>
                                                    <td>
                                                        Sep.</td>
                                                    <td>
                                                        Campo</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <anthem:DropDownList ID="m_CBCampo1" runat="server" OnSelectedIndexChanged="m_CBCampo1_SelectedIndexChanged" AutoCallBack="true">
                                                        </anthem:DropDownList></td>
                                                    <td>
                                                        <anthem:TextBox ID="m_ESeparador1" runat="server" Width="30px"></anthem:TextBox></td>
                                                    <td>
                                                        <anthem:DropDownList ID="m_CBCampo2" runat="server" OnSelectedIndexChanged="m_CBCampo1_SelectedIndexChanged" AutoCallBack="true">
                                                        </anthem:DropDownList></td>
                                                    <td>
                                                        <anthem:TextBox ID="m_ESeparador2" runat="server" Width="30px"></anthem:TextBox></td>
                                                    <td>
                                                        <anthem:DropDownList ID="m_CBCampo3" runat="server" OnSelectedIndexChanged="m_CBCampo1_SelectedIndexChanged" AutoCallBack="true">
                                                        </anthem:DropDownList></td>
                                                    <td>
                                                        <anthem:TextBox ID="m_ESeparador3" runat="server" Width="30px"></anthem:TextBox></td>
                                                    <td>
                                                        <anthem:DropDownList ID="m_CBCampo4" runat="server" OnSelectedIndexChanged="m_CBCampo1_SelectedIndexChanged" AutoCallBack="true">
                                                        </anthem:DropDownList></td>
                                                </tr>
                                            </table>
                                            <anthem:Button ID="m_BAplicar" runat="server" OnClick="m_BAplicar_Click" Text="Aplicar" />
                                            <anthem:Button ID="m_BCorta" runat="server" Text="Corta" OnClick="m_BCorta_Click" />
                                            <anthem:Button ID="m_BLarga" runat="server" Text="Larga" OnClick="m_BLarga_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            &nbsp;<asp:ImageButton ID="m_BAceptar" runat="server" ImageUrl="~/imagenes/save.gif" ToolTip="Guardar" OnClick="Button1_Click" />
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
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
			<h5><%CConector.GetCurrentVersion();%></h5>
		</div>
		<p>&nbsp;</p>
    </form>
</body>
</html>
