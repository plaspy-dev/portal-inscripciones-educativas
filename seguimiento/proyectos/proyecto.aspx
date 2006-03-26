<%@ Page Language="C#" AutoEventWireup="true" CodeFile="proyecto.aspx.cs" Inherits="seguimiento_proyectos_proyecto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>Portal de inscripciones e informes educativos</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
		<link href="../../estilos/estilos.css" rel="stylesheet" type="text/css" />
		<script type="text/javascript" language="JavaScript1.2" src="../../menu/JSCookMenu.js"></script>
		<script type="text/javascript" language="JavaScript1.2" src="../../menu/ThemeMiniBlack/theme.js"></script>
		<LINK REL="stylesheet" HREF="../../menu/ThemeMiniBlack/theme.css" TYPE="text/css" />
		<style>body { background-color: #E3E4EA; };</style>
		<script type="text/javascript" language="JavaScript" src="../../scripts/comun.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" align="center" border="0">
	<tr>
		<td>
		<div align="center"><img height="60" src="../../Imagenes/LogoEntidad.jpg" width="760" />
        <div style="text-align: left;" class="Titulos"><%GetTitulo();%>
        </div>
        </div>
		</td>
	</tr>
	<tr>
		<td>
		<table width="100%" border="1">
		<tr valign="top">
			<td width="150" height="280" align="center" valign="top">
			    <span class="Titulos">VIGENCIA <%GetVigencia();%></span>
			    <div id="menu"><%GetMenu();%></div>
			</td>
			<td>
			    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
					    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab1" runat="server" Text="Proyecto" Width="100px" CssClass="cselected" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab2" runat="server" Text="Contratos" Width="100px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab3" runat="server" Text="Modificaciones" Width="100px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab4" runat="server" Text="Presupuesto" Width="100px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab5" runat="server" Text="Historia" Width="100px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab6" runat="server" Text="Saldos" Width="100px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab7" runat="server" Text="Documentos" Width="100px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100%; height: 20px;" class="cinf">&nbsp;
                            </td>
                        </tr>
                        </table>
                    </td>
                </tr>
				<tr>
                    <td class="final">
                          <div id="divinstitucion" align="center">
                          <anthem:Label ID="m_STitulo" runat="server" Text="Seleccione un proyecto" UpdateAfterCallBack="False"></anthem:Label></div>
                    </td>
                </tr>
                <tr>
                    <td class="ccontenido">
                        <anthem:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="View1" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <anthem:CheckBox ID="CheckBox1" runat="server" Checked="True" Visible="False" />
                                        </td>
                                        <td>
                                            Entidad Territorial<anthem:HiddenField ID="m_nId" runat="server" />
                                        </td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBEnte" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBEnte_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_CBEnte"
                                            ErrorMessage="Por favor seleccione una entidad" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
						            <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox2" runat="server" Checked="True" Visible="False" />
                                        </td>
                                        <td >
                                            Código del proyecto</td>
                                        <td >
                                            <anthem:DropDownList ID="m_LCProyecto" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_LCProyecto_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_LCProyecto"
                                            ErrorMessage="Por favor seleccione un proyecto" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox3" runat="server" Checked="True" Visible="False" /></td>
                                        <td >
                                            Institución educativa</td>
                                        <td ><anthem:TextBox ID="m_ENombre" runat="server" ReadOnly="True">
                                        </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox4" runat="server" Checked="True" Visible="False" /></td>
                                        <td >
                                            Fecha de aprobación proyecto</td>
                                        <td ><anthem:TextBox ID="m_EFechaAprobacion" runat="server" ReadOnly="True">
                                        </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox5" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Observaciones</td>
                                                    <td ><anthem:TextBox ID="m_EObservaciones" runat="server" ReadOnly="True">
                                                    </anthem:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                    <div style="text-align: right"><anthem:Button ID="m_BSiguiente" runat="server" Text="Siguiente" OnClick="OnNext"  CausesValidation="false"/></div>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    Contratos<br />
                                    <div style="text-align: right"><anthem:Button ID="m_BAnterior" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" /></div>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                    Modificaciones<br />
                                    <div style="text-align: right"><anthem:Button ID="m_BAnterior" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" /></div>
                                </asp:View>
                                <asp:View ID="View4" runat="server">
                                    Presupuesto<br />
                                    <div style="text-align: right"><anthem:Button ID="m_BAnterior" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" /></div>
                                </asp:View>
                                <asp:View ID="View5" runat="server">
                                    Historia<br />
                                    <div style="text-align: right"><anthem:Button ID="m_BAnterior" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" /></div>
                                </asp:View>
                                <asp:View ID="View6" runat="server">
                                    Saldos<br />
                                    <div style="text-align: right"><anthem:Button ID="m_BAnterior" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" /></div>
                                </asp:View>
                                <asp:View ID="View7" runat="server">
                                    Documentos<br />
                                    <div style="text-align: right"><anthem:Button ID="m_BAnterior" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" /></div>
                                </asp:View>
                            </anthem:MultiView>
                            
                            </td>
                            </tr>
                            <tr>
                            <td class="final">                            
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <br />
                                            <anthem:Label ID="m_SMensaje" runat="server"></anthem:Label><br />
                                            <anthem:Button ID="m_BImprimir" runat="server" Text="Imprimir" OnClick="m_BImprimir_Click" CausesValidation="False" />
                                            <anthem:DropDownList ID="m_CBImprimir" runat="server">
                                                <asp:ListItem Value="1">Microsoft Excel</asp:ListItem>
                                                <asp:ListItem Value="0">En Pantalla</asp:ListItem>
                                            </anthem:DropDownList>
                                            <anthem:Button ID="m_BAceptar" runat="server" Text="Aceptar" OnClick="m_BAceptar_Click" /><anthem:Button ID="m_BCancelar" runat="server" Text="Cancelar" OnClick="m_BCancelar_Click" CausesValidation="False" />
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
        </td></tr></table>
		<div align="right">
            <asp:Label ID="m_SVersion" runat="server" Font-Bold="True"></asp:Label>&nbsp;</div>
    </form>
</body>
</html>