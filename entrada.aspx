<%@ Page Language="C#" AutoEventWireup="true" CodeFile="entrada.aspx.cs" Inherits="entrada" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Portal de inscripciones e informes educativos</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>
		<link href="estilos/estilos.css" rel="stylesheet" type="text/css" />
		<script type="text/javascript" language="JavaScript1.2" src="menu/JSCookMenu.js"></script>
		<script type="text/javascript" language="JavaScript1.2" src="menu/ThemeMiniBlack/theme.js"></script>
		<LINK REL="stylesheet" HREF="menu/ThemeMiniBlack/theme.css" TYPE="text/css">
		<style type="text/css">body { background-color: #E3E4EA; };</style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" border="0">
			<tr>
				<td>
					<!--#include file="imagenes/LogoEntidadi.aspx" -->
				</td>
			</tr>
			<tr>
				<td><table width="100%" border="1">
						<tr>
							<td width="150" height="280" align="center" valign="top"><span class="Titulos">VIGENCIA 
                                <asp:DropDownList ID="m_CBVigenciasCombo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="m_CBVigenciasCombo_SelectedIndexChanged">
                                </asp:DropDownList></span><br />
								<div id="menu"><%GetMenu();%></div>
							</td>
							<td>&nbsp;</td>
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
