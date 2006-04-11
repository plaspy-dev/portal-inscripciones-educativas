<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Portal de inscripciones e informes educativos</title>
    <style type="text/css">body { background-color: #E3E4EA; };</style>
    <link href="estilos/estilos.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
		<table width="761" align="center" border="0">
			<tr>
				<td>
					<!--#include file="imagenes/LogoEntidadi.aspx" -->
				</td>
			</tr>
			<tr>
				<td>
					<table align="center" border="0">
							<tr>
								<td><span class="textologin">Usuario</span></td>
								<td><label>
                                    &nbsp;<asp:TextBox ID="Usuario" runat="server" Width="120px"></asp:TextBox>
                                    </label></td>
                                <td>
                                    <asp:RequiredFieldValidator id="VUsuario" runat="server" ErrorMessage="Por favor ingrese un usuario" SetFocusOnError="True" ControlToValidate="Usuario"></asp:RequiredFieldValidator></td>
							</tr>
							<tr>
								<td class="textologin">Clave</td>
								<td><label>
                                    &nbsp;<asp:TextBox ID="Contrasena" runat="server" TextMode="Password" Width="120px"></asp:TextBox>
                                    </label></td>
                                <td>
                                    <asp:RequiredFieldValidator ID="VContrasena" runat="server" ControlToValidate="Contrasena"
                                        ErrorMessage="Por favor ingrese una contraseña" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td><label>
                                    <asp:Button ID="bIngreso" runat="server" Text="Ingresar" OnClick="bIngreso_Click" /></label></td>
                                <td>
                                    </td>
							</tr>
						</table>
						<span class="textologin">
                            <div align="center"><asp:Label ID="mensaje" runat="server" Text=""></asp:Label></div>
						</span>
				</td>
			</tr>
		</table>
    </form>
</body>
</html>
