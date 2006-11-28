<%@ Page Language="C#" AutoEventWireup="true" CodeFile="galeriav.aspx.cs" Inherits="fotos_proyectos_galeriav" EnableEventValidation="false" %>
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>Portal de inscripciones e informes educativos</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1"/>
		<link href="../../estilos/estilos.css" rel="stylesheet" type="text/css" />
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
				<td align="center">
                    <table>
                        <tr>
                            <td align="center">
                                <asp:Label ID="m_SContrato" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="center">
                                Ver &nbsp;<asp:DropDownList ID="m_CBVista" runat="server" AutoPostBack="True" OnSelectedIndexChanged="m_CBVista_SelectedIndexChanged">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="View1" runat="server">
                                        <asp:DataList ID="m_LBFotos" runat="server" RepeatDirection="Horizontal" DataKeyField="id" DataSourceID="ObjectDataSource1" OnSelectedIndexChanged="m_LBFotos_SelectedIndexChanged" RepeatColumns="5">
                                        <ItemTemplate>
                                        <table border="0">
                                          <tr>
                                            <td>&nbsp;</td>
                                            <td align="center"><%# CPageTemplate.GetFecha(Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"fecha"))) %></td>
                                            <td>&nbsp;</td>
                                          </tr>
                                          <tr>
                                            <td>&nbsp;</td>
                                            <td align="center">
                                            <a id="A1" href='<%# DataBinder.Eval(Container.DataItem,"id", "../fotos.aspx?id={0}")%>' target="_blank" runat="server">
                                            <img id="Img1" src='<%# DataBinder.Eval(Container.DataItem,"id", "../fotos.aspx?id={0}&h=100&w=100")%>' width="100" height="100" runat="server"/>
                                            </a>
                                            </td>
                                            <td>&nbsp;</td>
                                          </tr>
                                          <tr>
                                            <td>&nbsp;</td>
                                            <td align="center">
                                                <asp:linkbutton ID="Linkbutton1" Text='<%# DeleteString()%>' CommandName="Select" runat="server"/>
                                            </td>
                                            <td>&nbsp;</td>
                                          </tr>
                                        </table>
                                    </ItemTemplate>
                                    </asp:DataList>
                                    </asp:View>
                                    <asp:View ID="View2" runat="server">
                                        <div style="text-align: center;">
                                            <anthem:Label ID="m_SFotoR" runat="server" Text=""></anthem:Label>
                                        </div>
                                        <anthem:GridView ID="m_LCFotos" runat="server" AllowPaging="True" AllowSorting="True" EmptyDataText="No hay fotos" DataSourceID="ObjectDataSource1" UpdateAfterCallBack="False"  AutoUpdateAfterCallBack="true" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" PageSize="1" OnSelectedIndexChanged="m_LCFotos_SelectedIndexChanged" OnPageIndexChanged="m_LCFotos_PageIndexChanged" DataKeyNames="id">
                                                <PagerSettings Mode="NextPreviousFirstLast" Position="Top" FirstPageImageUrl="~/imagenes/inicio.gif" LastPageImageUrl="~/imagenes/final.gif" NextPageImageUrl="~/imagenes/adelante.gif" PreviousPageImageUrl="~/imagenes/atras.gif" FirstPageText="Primera foto" LastPageText="&#218;ltima foto" NextPageText="Foto siguiente" PreviousPageText="Foto anterior"/>
                                                <PagerStyle HorizontalAlign="Right" BackColor="#E3E4EA" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <headertemplate>
                                                    
</headertemplate>
                                                    <itemtemplate>
                                            <table border="0">
                                          <tr>
                                            <td>&nbsp;</td>
                                            <td align="center"><%# CPageTemplate.GetFecha(Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"fecha"))) %></td>
                                            <td>&nbsp;</td>
                                          </tr>
                                          <tr>
                                            <td>&nbsp;</td>
                                            <td align="center">
                                            <a id="A1" href='<%# DataBinder.Eval(Container.DataItem,"id", "../fotos.aspx?id={0}")%>' target="_blank" runat="server">
                                            <img id="Img1" src='<%# DataBinder.Eval(Container.DataItem,"id", "../fotos.aspx?id={0}")%>' runat="server"/>
                                            </a>
                                            </td>
                                            <td>&nbsp;</td>
                                          </tr>
                                          <tr>
                                            <td>&nbsp;</td>
                                            <td align="center">
                                                <asp:linkbutton ID="Linkbutton1" Text='<%# DeleteString()%>' CommandName="Select" runat="server"/>
                                            </td>
                                            <td>&nbsp;</td>
                                          </tr>
                                        </table>
                                            
</itemtemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </anthem:GridView>
                                        </asp:View>
                                </asp:MultiView>
                                </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <anthem:TextBox ID="m_EFechaRegistro" runat="server" CssClass="editfecha"></anthem:TextBox><input
                                    id="Button4" name="botonfecha" onclick="setFecha('m_EFechaRegistro')" type="button"
                                    value="..." />
                                <asp:RangeValidator ID="RangeValidator7" runat="server" ControlToValidate="m_EFechaRegistro" Display="Dynamic"
                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="1960/01/01"
                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator></td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:FileUpload ID="m_ERegistro" runat="server" />
                                <asp:Button ID="m_BSubir" runat="server" OnClick="m_BSubir_Click"
                                    Text="Subir" /></td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <anthem:Label ID="m_SMen" runat="server"></anthem:Label></td>
                        </tr>
                    </table>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select" TypeName="CGaleriaV"></asp:ObjectDataSource>
				</td>
			</tr>
		</table>
		<div align="right">
            <asp:Label ID="m_SVersion" runat="server" Font-Bold="True"></asp:Label><br />
            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" Font-Size="Smaller"
                PostBackUrl="~/about.aspx">Desarrollado por SDG.</asp:LinkButton></div>
    </form>
</body>
</html>