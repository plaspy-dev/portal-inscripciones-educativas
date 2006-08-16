<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="informes_institucion_Default" EnableEventValidation="false"%>
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
                                            &nbsp;<table>
                                                <tr>
                                                    <td valign="top">
                                            <table>
                                                <tr>
                                                    <td>
                                                        Entidad Territorial</td>
                                                    <td>
                                                        <anthem:DropDownList ID="m_CBEnte" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBEnte_SelectedIndexChanged">
                                                        </anthem:DropDownList></td>
                                                    <td>
                                                        <anthem:HiddenField ID="m_nID" runat="server" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Municipio</td>
                                                    <td>
                                                        <anthem:DropDownList ID="m_CBMunicipio" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBMunicipio_SelectedIndexChanged"></anthem:DropDownList></td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Código Dane
                                                    </td>
                                                    <td>
                                                        <anthem:TextBox ID="m_ECodigo" runat="server"></anthem:TextBox></td>
                                                    <td>
                                                        <anthem:Button ID="m_BCodigo" runat="server" Text="Buscar" TextDuringCallBack="Buscando" ValidationGroup="codigo" OnClick="m_BCodigo_Click" /></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_ECodigo" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese un código" SetFocusOnError="True" ValidationGroup="codigo"></asp:RequiredFieldValidator></td>
                                                </tr>
						<tr>
                                                    <td >
                                                        Nombre de la institución</td>
                                                    <td >
                                                        <anthem:TextBox ID="m_ENombre" runat="server"></anthem:TextBox></td>
                            <td>
                                <anthem:Button ID="m_BNombre" runat="server" Text="Buscar" TextDuringCallBack="Buscando" ValidationGroup="nombre" OnClick="m_BNombre_Click" /></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_ENombre"
                                                            Display="Dynamic" ErrorMessage="Por favor ingrese un nombre" SetFocusOnError="True" ValidationGroup="nombre"></asp:RequiredFieldValidator></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                                    </td>
                                                    <td>
                                            <anthem:DetailsView ID="DetailsView1" runat="server" DataSourceID="ObjectDataSource2">
                                            </anthem:DetailsView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                                                TypeName="CTablaEx"></asp:ObjectDataSource>
                                            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="Select"
                                                TypeName="CInstDetalles">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="m_LCResultados" Name="id" PropertyName="SelectedValue" Type="int32" />
                                                </SelectParameters>
                                                </asp:ObjectDataSource>
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
            <asp:Label ID="m_SVersion" runat="server" Font-Bold="True"></asp:Label>&nbsp;</div>
    </form>
</body>
</html>
