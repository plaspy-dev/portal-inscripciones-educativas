<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inscripcion.aspx.cs" Inherits="aprobacion_convenios_inscripcion"  EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
     <title>Portal de inscripciones e informes educativos</title>
		<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
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
		<td>
		<table width="100%" border="1">
		<tr valign="top">
			<td id="leftmenu"  width="150" height="280" align="center" valign="top">
			    <span id="lefttitulo"  class="Titulos">VIGENCIA <asp:DropDownList ID="m_CBVigenciasCombo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="m_CBVigenciasCombo_SelectedIndexChanged">
                                </asp:DropDownList></span>
			    <div id="menu"><%GetMenu();%></div>
			</td>
			<td>
			    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
					    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab1" runat="server" Text="Inscripción" Width="100px" CssClass="cselected" OnClick="OnTabsClick" CausesValidation="false" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab2" runat="server" Text="Documentos" Width="100px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
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
                          <anthem:Label ID="m_STitulo" runat="server" Text="Nuevo convenio" UpdateAfterCallBack="False"></anthem:Label></div>
                    </td>
                </tr>
                <tr>
                    <td class="ccontenido">
                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="View1" runat="server">
                                <table cellspacing="0">
                                    <tr>
                                        <td>
                                            Ente Territorial<anthem:HiddenField ID="m_nId" runat="server" />
                                        </td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBEnte" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBEnte_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_CBEnte" Display="Dynamic"
                                            ErrorMessage="Por favor seleccione un ente" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
						            <tr>
                                        <td >
                                            Numero de convenio</td>
                                        <td ><anthem:TextBox ID="m_ENumero" runat="server" CssClass="inputnumerico" MaxLength="3">
                                        </anthem:TextBox></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_ENumero" Display="Dynamic"
                                                ErrorMessage="Por favor ingrese un número"></asp:RequiredFieldValidator>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Valor convenio</td>
                                        <td ><anthem:TextBox ID="m_EValor" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox></td>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="m_EValor" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="900000000000000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Currency"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Tipo</td>
                                        <td >
                                            <anthem:DropDownList ID="m_CBTipo" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBTipo_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="m_CBTipo" Display="Dynamic"
                                                ErrorMessage="Por favor seleccione un tipo" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Objeto</td>
                                        <td><anthem:TextBox ID="m_EObjeto" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                                <tr>
                                                    <td >
                                                        Plazo total en dias</td>
                                                    <td ><anthem:TextBox ID="m_EPlazo" runat="server" CssClass="inputnumerico" MaxLength="3">
                                                    </anthem:TextBox></td>
                                                    <td>
                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="m_EPlazo" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="10000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Fecha de aprobación</td>
                                                    <td ><anthem:TextBox ID="m_EFechaAprobacion" runat="server" CssClass="editfecha"></anthem:TextBox><input id="botonfecha" name="botonfecha"  onclick="setFecha('m_EFechaAprobacion')" type="button" value="..." />
                                                    </td>
                                                    <td>
                                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="m_EFechaAprobacion" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                            SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Fecha de vencimiento</td>
                                                    <td ><anthem:Label ID="m_SFechaVencimiento" runat="server">
                                                    </anthem:Label></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        Estado</td>
                                                    <td >
                                                        <anthem:DropDownList ID="m_CBEstado" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBEstado_SelectedIndexChanged">
                                                        </anthem:DropDownList></td>
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
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:ImageButton  ImageUrl="~/imagenes/sig.gif"  ID="m_BSiguiente" runat="server" ToolTip="Siguiente" OnClick="OnNext"  CausesValidation="true"/></td>
                                    </tr>
                                            </table>
                                    <br />
                                            <anthem:Label ID="m_SMensaje" runat="server"></anthem:Label><br />
                                            <anthem:ImageButton ID="m_BInsertar" ToolTip="Insertar" runat="server" ImageUrl="~/imagenes/save.gif" OnClick="m_BInsertar_Click" /><anthem:ImageButton ID="m_BImprimir" runat="server" ToolTip="Imprimir" ImageUrl="~/imagenes/print.gif" OnClick="m_BImprimir_Click" CausesValidation="False" />
                                            <anthem:DropDownList ID="m_CBImprimir" runat="server">
                                                <asp:ListItem Value="1">Microsoft Excel</asp:ListItem>
                                                <asp:ListItem Value="0">En Pantalla</asp:ListItem>
                                            </anthem:DropDownList>
                                            <anthem:ImageButton ID="m_BAceptar" runat="server" ImageUrl="~/imagenes/save.gif" ToolTip="Guardar" OnClick="m_BAceptar_Click" /><anthem:ImageButton ID="m_BCancelar" ToolTip="Cancelar" runat="server" ImageUrl="~/imagenes/stop.gif" OnClick="m_BCancelar_Click" CausesValidation="False" /><anthem:ImageButton ID="m_BEliminar" runat="server" ImageUrl="~/imagenes/trash.gif" ToolTip="Eliminar" CausesValidation="False" OnClick="m_BEliminar_Click" PreCallBackFunction="Eliminar_PreCallBack"/>
                                            <anthem:ImageButton ID="m_BAceptarP" runat="server" ImageUrl="~/imagenes/print.gif" ToolTip="Imprimir" CausesValidation="False" OnClick="m_BAceptarP_Click" /><anthem:ImageButton ID="m_BCancelarP" runat="server" ToolTip="Cancelar" ImageUrl="~/imagenes/stop.gif" CausesValidation="False" OnClick="m_BCancelarP_Click" />
                                            
                                            
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    &nbsp;<table>
                                        <tr>
                                            <td>
                                    <table cellspacing="0">
                                    <tr>
                                        <td>
                                            <strong>
                                            NIT ente territorial</strong></td>
                                        <td style="font-weight: bold"><anthem:Label ID="m_SNIT" runat="server">
                                        </anthem:Label></td>
                                        <td style="font-weight: bold">
                                        </td>
                                    </tr>
                                        <tr style="font-weight: bold">
                                            <td>
                                                Ordenador del gasto</td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr style="font-weight: bold">
                                            <td>
                                                Tipo de gasto</td>
                                            <td><anthem:DropDownList ID="m_CBTipoGasto" runat="server">
                                            </anthem:DropDownList></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Registro presupuestal</td>
                                            <td>
                                                <anthem:TextBox ID="m_ERDP" runat="server" CssClass="editcomun"></anthem:TextBox>
                                                </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Fecha Registro</td>
                                            <td>
                                                <anthem:TextBox ID="m_EFechaRDP" runat="server" CssClass="editfecha"></anthem:TextBox>
                                                <input id="Button1" name="botonfecha"  onclick="setFecha('m_EFechaRDP')" type="button" value="..." />
                                                </td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="m_EFechaRDP" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                            SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Certificado de disponibilidad presupuestal</td>
                                            <td>
                                                <anthem:TextBox ID="m_ECDP" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Fecha Certificado</td>
                                            <td>
                                                <anthem:TextBox ID="m_EFechaCDP" runat="server" CssClass="editfecha"></anthem:TextBox>
                                                <input id="Button2" name="botonfecha"  onclick="setFecha('m_EFechaCDP')" type="button" value="..." />
                                                </td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="m_EFechaCDP" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                            SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="Select"
                                                    TypeName="CDocumentosConvenio"></asp:ObjectDataSource>
                                                </td>
                                            <td>
                                            </td>
                                            <td>
                                        </td>
                                        </tr>
                                    </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="m_LCDocumentos" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2"
                                                    OnRowDataBound="m_LCDocumentos_RowDataBound" DataKeyNames="id">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("did") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="id" HeaderText="id" Visible="false"/>
                                                        <asp:BoundField DataField="Documento" HeaderText="Documento"/>
                                                        <asp:TemplateField HeaderText="Fecha">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="m_EFecha" CssClass="editfecha" runat="server" Text='<%# Bind("Fecha") %>'></asp:TextBox><input id="botonfecha" name="botonfecha" type="button" value="..." runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Número">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="m_ENumero" runat="server" Text='<%# Bind("Número") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Observaciones">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="m_EObservaciones" runat="server" Text='<%# Bind("Observaciones") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                        <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:ImageButton ImageUrl="~/imagenes/save.gif" ID="m_BFinalizarE" runat="server" OnClick="m_BAceptar_Click" ToolTip="Finalizar"/>
                                                <asp:ImageButton ImageUrl="~/imagenes/save.gif" ID="m_BFinalizar" runat="server" OnClick="m_BInsertar_Click" ToolTip="Finalizar"/></td>
                                        </tr>
                                    </table>
                                    <br />
                                </asp:View>
                            </asp:MultiView>
                            
                            </td>
                            </tr>
                            <tr>
                            <td class="final">                            
                                <table width="100%">
                                    <tr>
                                        <td>
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
        </td></tr></table>
		<div align="right">
            <asp:Label ID="m_SVersion" runat="server" Font-Bold="True"></asp:Label>&nbsp;</div>
    </form>
</body>
</html>