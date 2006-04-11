<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contrasena.aspx.cs" Inherits="conf_contrasena" %>

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
							<td align="center" valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        Contraseña actual</td>
                                                    <td>
                                                        <asp:TextBox ID="m_EActual" runat="server" TextMode="Password"></asp:TextBox></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_EActual"
                                                            ErrorMessage="Por favor ingrese la contraseña actual" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
						<tr>
                                                    <td >
                                                        </td>
                                                    <td >
                                                        </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Contraseña nueva</td>
                                                    <td >
                                                        <asp:TextBox ID="m_ENueva" runat="server" TextMode="Password"></asp:TextBox></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_ENueva"
                                                            ErrorMessage="Por favor ingrese la nueva contraseña" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Confirmar contraseña</td>
                                                    <td >
                                                        <asp:TextBox ID="m_EConfirmar" runat="server" TextMode="Password"></asp:TextBox></td>
                                                    <td>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="m_EConfirmar"
                                                            ControlToValidate="m_ENueva" ErrorMessage="La contraseña nueva no coincide"></asp:CompareValidator></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="m_SMensaje" runat="server"></asp:Label><br />
                                            <anthem:ImageButton ID="m_BAceptar" runat="server" ImageUrl="~/imagenes/save.gif" ToolTip="Guardar" OnClick="m_BAceptar_Click" />
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