<%@ Page Language="C#" AutoEventWireup="true" CodeFile="viabilizacion.aspx.cs" Inherits="viabilizacion_proyectos_viabilizacion" %>

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
		<td>
		<table width="100%" border="1">
		<tr valign="top">
			<td width="150" height="280" align="center" valign="top">
			    <span class="Titulos">VIGENCIA <asp:DropDownList ID="m_CBVigenciasCombo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="m_CBVigenciasCombo_SelectedIndexChanged">
                                </asp:DropDownList></span>
			    <div id="menu"><%GetMenu();%></div>
			</td>
			<td>
			    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
					    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td>
                                <asp:Button ID="m_BTab1" runat="server" Text="Datos Generales" CssClass="cselected" OnClick="OnTabsClick" UseSubmitBehavior="False" Width="130px"/>
                            </td>
                            <td >
                                <asp:Button ID="m_BTab2" runat="server" Text="Aspectos Juridicos y formulación" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False" Width="200px"/>
                            </td>
                            <td >
                                <asp:Button ID="m_BTab3" runat="server" Text="Aspectos técnicos" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False" Width="120px"/>
                            </td>
                            <td >
                                <asp:Button ID="m_BTab4" runat="server" Text="Tipos de intervención y población atendida" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False" Width="280px"/>
                            </td>
                            <td>
                                <asp:Button ID="m_BTab5" runat="server" Text="Areas y costos M2" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False" Width="150px"/>
                            </td>
                            <td style="width: 100%;" class="cinf">&nbsp;
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
                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="View1" runat="server">
                                <table cellspacing="0">
                                    <tr>
                                        <td>
                                            Ente Territorial<anthem:HiddenField ID="m_nId" runat="server" />
                                        </td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBEnte" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBEnte_SelectedIndexChanged" ValidationGroup="normal">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_CBEnte" Display="Dynamic"
                                            ErrorMessage="Por favor seleccione un ente territorial" InitialValue="-1" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
						            <tr>
                                        <td >
                                            Municipio</td>
                                        <td ><anthem:DropDownList ID="m_CBMunicipio" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBMunicipio_SelectedIndexChanged" ValidationGroup="normal">
                                        </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_CBEnte" Display="Dynamic"
                                                ErrorMessage="Por favor seleccione un municipio" InitialValue="-1" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Código proyecto</td>
                                        <td ><anthem:DropDownList ID="m_CBProyecto" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBProyecto_SelectedIndexChanged" ValidationGroup="normal">
                                        </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="m_CBProyecto" Display="Dynamic"
                                                ErrorMessage="Por favor seleccione un proyecto" InitialValue="-1" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Institución educativa<anthem:HiddenField ID="m_nIdInstitucion" runat="server" /></td>
                                        <td >
                                            <anthem:Label id="m_SInstitucion" runat="server" Text="">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Objeto viabilizado</strong></td>
                                        <td>
                                            <anthem:HiddenField ID="m_nIdObjeto" runat="server" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Concepto</td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBConcepto" runat="server" ValidationGroup="objeto">
                                            </anthem:DropDownList></td>
                                        <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="m_CBConcepto" Display="Dynamic"
                                                    ErrorMessage="Por favor seleccione un concepto" InitialValue="-1" SetFocusOnError="True"
                                                    ValidationGroup="objeto">*</asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                                Cantidad</td>
                                        <td>
                                            <anthem:TextBox ID="m_ECantidad" runat="server" CssClass="inputnumerico" ValidationGroup="objeto"></anthem:TextBox></td>
                                        <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="m_ECantidad" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese una cantidad" SetFocusOnError="True" ValidationGroup="objeto">*</asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="m_ECantidad" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese un número aqui" MaximumValue="900000000000000" MinimumValue="1"
                                                    SetFocusOnError="True" Type="Currency" ValidationGroup="objeto"></asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Valor</td>
                                        <td>
                                            <anthem:TextBox ID="m_EValor" runat="server" CssClass="inputnumerico" ValidationGroup="objeto"></anthem:TextBox></td>
                                        <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="m_EValor" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese un valor" SetFocusOnError="True" ValidationGroup="objeto">*</asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="m_EValor" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese un número aqui" MaximumValue="900000000000000" MinimumValue="1"
                                                    SetFocusOnError="True" Type="Currency" ValidationGroup="objeto"></asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                                <anthem:Button ID="m_BInsertarObjeto" runat="server" Text="Insertar" ValidationGroup="objeto" OnClick="m_BInsertarObjeto_Click" /><anthem:Button ID="m_BAceptarObjeto" runat="server" Text="Aceptar" ValidationGroup="objeto" OnClick="m_BAceptarObjeto_Click" /><anthem:Button ID="m_BCancelarObjeto" runat="server" Text="Cancelar" CausesValidation="false" OnClick="m_BCancelarObjeto_Click" /><anthem:Button ID="m_BEliminarObjeto" runat="server" Text="Eliminar" CausesValidation="False" PreCallBackFunction="Eliminar_PreCallBack" OnClick="m_BEliminarObjeto_Click" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="Select"
                                                    TypeName="CTablaObjetoVia"></asp:ObjectDataSource>
                                        </td>
                                        <td>
                                                <anthem:GridView ID="m_LCObjetos" runat="server" EmptyDataText="No hay objetos"
                                                Width="100%" UpdateAfterCallBack="False" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" DataKeyNames="id" DataSourceID="ObjectDataSource2" OnSelectedIndexChanged="m_LCObjetos_SelectedIndexChanged" OnRowDataBound="m_LCObjetos_RowDataBound">
                                                <RowStyle CssClass="celda" BackColor="White" Wrap="True" />
                                                <HeaderStyle CssClass="celdaTitulo" BackColor="#E3E4EA" />
                                                <PagerStyle HorizontalAlign="Right" BackColor="#E3E4EA" />
                                                <SelectedRowStyle BackColor="#316AC5" CssClass="celdasel" />
                                                <Columns>
                                                <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="../../imagenes/edit.gif" />
                                                </Columns>
                                            </anthem:GridView>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Presupuesto Viabilizado</strong></td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Valor total proyecto</td>
                                        <td>
                                            <anthem:Label ID="m_SValorTotal" runat="server" Text="">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                                Valor partida asignada por el Entidad</td>
                                        <td>
                                            <anthem:TextBox ID="m_EValorPartidaMEN" runat="server" CssClass="inputnumerico" ValidationGroup="normal"></anthem:TextBox></td>
                                        <td>
                                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="m_EValorPartidaMEN" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese un número aqui" MaximumValue="900000000000000" MinimumValue="1"
                                                    SetFocusOnError="True" Type="Currency"></asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Porcentaje aportes Entidad educativa</td>
                                        <td>
                                            <anthem:Label ID="m_SPorcentajeMEN" runat="server" Text="">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                                Valor partida asignada municipio proyecto aprobado</td>
                                        <td>
                                            <anthem:Label ID="m_SValorMun" runat="server" Text="">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                                Porcentaje aportes cofinaciación</td>
                                        <td>
                                            <anthem:Label ID="m_SPorcentajeMun" runat="server" Text="">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Concepto de viabilidad</td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBConceptoViablizacion" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBConceptoViablizacion_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                                        Estado</td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBEstado" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBEstado_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Fecha ultima revisión</td>
                                        <td>
                                            <anthem:Label ID="m_EFechaUltima" runat="server" Text="">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                                Fecha Aprobación</td>
                                        <td>
                                            <anthem:TextBox ID="m_EFechaAprobacion" runat="server" CssClass="editfecha" ValidationGroup="normal"></anthem:TextBox><input id="botonfecha" name="botonfecha" onclick="setFecha('m_EFechaAprobacion')" type="button" value="..." /></td>
                                        <td>
                                                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="m_EFechaAprobacion" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator></td>
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
                                            <strong>Responsable de Viablización</strong></td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            NIT de la firma</td>
                                        <td><anthem:TextBox ID="m_ENITFirma" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Nombre de la firma</td>
                                        <td><anthem:TextBox ID="m_ENombreFirma" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Representante legal</td>
                                        <td><anthem:TextBox ID="m_ERepresentante" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Dirección</td>
                                        <td><anthem:TextBox ID="m_EDireccion" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Telefono</td>
                                        <td><anthem:TextBox ID="m_ETelefono" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Fax</td>
                                        <td><anthem:TextBox ID="m_EFax" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            E-Mail</td>
                                        <td><anthem:TextBox ID="m_EMail" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="m_BSiguiente" runat="server" ToolTip="Siguiente" OnClick="OnNext"/></td>
                                    </tr>
                                            </table>
                                    <br />
                                            <anthem:Label ID="m_SMensaje" runat="server"></anthem:Label><br />
                                            <anthem:ImageButton ID="m_BInsertar" ToolTip="Insertar" runat="server" ImageUrl="~/imagenes/save.gif" OnClick="m_BInsertar_Click" ValidationGroup="normal" /><anthem:ImageButton ID="m_BImprimir" runat="server" ToolTip="Imprimir" ImageUrl="~/imagenes/print.gif" OnClick="m_BImprimir_Click" CausesValidation="False" />
                                            <anthem:DropDownList ID="m_CBImprimir" runat="server">
                                                <asp:ListItem Value="1">Microsoft Excel</asp:ListItem>
                                                <asp:ListItem Value="0">En Pantalla</asp:ListItem>
                                            </anthem:DropDownList>
                                            <anthem:ImageButton ID="m_BAceptar" runat="server" ImageUrl="~/imagenes/save.gif" ToolTip="Guardar" OnClick="m_BAceptar_Click" ValidationGroup="normal" /><anthem:ImageButton ID="m_BCancelar" ToolTip="Cancelar" runat="server" ImageUrl="~/imagenes/stop.gif" OnClick="m_BCancelar_Click" CausesValidation="False" /><anthem:ImageButton ID="m_BEliminar" runat="server" ImageUrl="~/imagenes/trash.gif" ToolTip="Eliminar" CausesValidation="False" OnClick="m_BEliminar_Click" PreCallBackFunction="Eliminar_PreCallBack"/>
                                            <anthem:ImageButton ID="m_BAceptarP" runat="server" ImageUrl="~/imagenes/print.gif" ToolTip="Imprimir" CausesValidation="False" OnClick="m_BAceptarP_Click" /><anthem:ImageButton ID="m_BCancelarP" runat="server" ToolTip="Cancelar" ImageUrl="~/imagenes/stop.gif" CausesValidation="False" OnClick="m_BCancelarP_Click" />
                                            
                                            
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="Select"
                                                    TypeName="CDocumentosProyecto"></asp:ObjectDataSource>
                                    <asp:GridView ID="m_LCDocumentos" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource3"
                                    DataKeyNames="id" OnRowDataBound="m_LCDocumentos_RowDataBound">
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
                                    <table cellspacing="0">
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Observaciones</td>
                                            <td>
                                                <anthem:TextBox ID="m_EObservacionesDocumentos" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior2" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="Button2" runat="server" ToolTip="Siguiente" OnClick="OnNext"/></td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                 
                                 <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="Select"
                                                    TypeName="CAspectosTecnicos"></asp:ObjectDataSource>
                                                    
                                 <asp:GridView ID="m_LCTecnicos" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource4"
                                    DataKeyNames="id" OnRowDataBound="m_LCTecnicos_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("did") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Documento" HeaderText="Aspecto Técnico"/>
                                        <asp:TemplateField HeaderText="Cantidad">
                                            <ItemTemplate>
                                                <asp:TextBox ID="m_ENumero" CssClass="inputnumerico" runat="server" Text='<%# Bind("Cantidad") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <table>
                                        <tr>
                                            <td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Observaciones</td>
                                            <td><anthem:TextBox ID="m_EObservacionesTecnicas" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior3" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="Button3" runat="server" ToolTip="Siguiente" OnClick="OnNext"/></td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="View4" runat="server">
                                    <table cellspacing="0">
                                    <tr>
                                        <td>
                                            Tipo</td>
                                        <td><anthem:DropDownList ID="m_CBTipo" runat="server">
                                        </anthem:DropDownList>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Infraestructura</strong></td>
                                        <td>
                                        <td>
                                        </td>
                                    </tr>
                                        <tr>
                                            <td>
                                                Número de aulas nuevas</td>
                                            <td>
                                                <anthem:TextBox ID="m_EAulasN" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                                <asp:RangeValidator ID="RangeValidator5" Display="Dynamic" ControlToValidate="m_EAulasN" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="integer" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Número de aulas reposición</td>
                                            <td>
                                                <anthem:TextBox ID="m_EAulasR" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Número de estudiantes promedio aula</td>
                                            <td>
                                                <anthem:TextBox ID="m_EEstudiantesP" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator6" Display="Dynamic" ControlToValidate="m_EEstudiantesP" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="integer" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Número de jornadas</td>
                                            <td>
                                                <anthem:TextBox ID="m_EJornadas" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator7" Display="Dynamic" ControlToValidate="m_EJornadas" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="integer" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Total ampliación de cobertura</td>
                                            <td>
                                                <anthem:Label ID="m_STotalACobertura" runat="server">
                                                </anthem:Label></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Total estudiantes beneficiados aulas nuevas</td>
                                            <td>
                                                <anthem:Label ID="m_STotalBeneficiados" runat="server">
                                                </anthem:Label></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Total estudiantes beneficiados aulas reposición</td>
                                            <td>
                                                <anthem:Label ID="m_SBeneficiadosReposicion" runat="server">
                                                </anthem:Label></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Numero de baños</td>
                                            <td>
                                                <anthem:TextBox ID="m_EBanos" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator11" Display="Dynamic" ControlToValidate="m_EBanos" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="integer" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Aparatos sanitarios</td>
                                            <td>
                                                <anthem:TextBox ID="m_EAparatosS" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator12" Display="Dynamic" ControlToValidate="m_EAparatosS" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="integer" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Dotación</strong></td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Numero sillas</td>
                                            <td>
                                                <anthem:TextBox ID="m_ESillas" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator13" Display="Dynamic" ControlToValidate="m_ESillas" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="integer" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Valor total dotación</td>
                                            <td>
                                                <anthem:TextBox ID="m_EValorTotalD" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator14" Display="Dynamic" ControlToValidate="m_EValorTotalD" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Numero de estudiantes beneficiados por dotación</td>
                                            <td>
                                                <anthem:Label ID="m_SBeneficiadosD" runat="server">
                                                </anthem:Label></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Observaciones</td>
                                            <td>
                                                <anthem:TextBox ID="m_EObservacionesInfraestructura" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior4" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="Button4" runat="server" ToolTip="Siguiente" OnClick="OnNext"/></td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="View5" runat="server">
                                    <table cellspacing="0">
                                    <tr>
                                        <td>
                                            Valor total proyecto</td>
                                        <td>
                                            <anthem:Label ID="m_SValorTotalProyecto" runat="server" Text="" CssClass="inputnumerico">
                                            </anthem:Label><td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Aulas</td>
                                        <td><anthem:TextBox ID="m_EAulas" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator8" Display="Dynamic" ControlToValidate="m_EAulas" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Baños</td>
                                        <td><anthem:TextBox ID="m_EBanosV" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator9" Display="Dynamic" ControlToValidate="m_EBanosV" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1">
                                                </asp:RangeValidator>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Circulaciones y puntos fijos</td>
                                        <td><anthem:TextBox ID="m_ECirculaciones" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator10" Display="Dynamic" ControlToValidate="m_ECirculaciones" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Área total proyecto</td>
                                        <td>
                                            <anthem:Label ID="m_SAreaTotal" runat="server" Text="" CssClass="inputnumerico">
                                            </anthem:Label><td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            M2 de aulas</td>
                                        <td><anthem:TextBox ID="m_EM2Aulas" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator15" Display="Dynamic" ControlToValidate="m_EM2Aulas" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1">
                                                </asp:RangeValidator>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            M2 de baños</td>
                                        <td><anthem:TextBox ID="m_EM2Banos" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator16" Display="Dynamic" ControlToValidate="m_EM2Banos" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            M2 circulaciones y puntos fijos</td>
                                        <td><anthem:TextBox ID="m_EM2Circulaciones" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator17" Display="Dynamic" ControlToValidate="m_EM2Circulaciones" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Valor total m2 proyecto</td>
                                        <td>
                                            <anthem:Label ID="m_SValorM2Total" runat="server" Text="" CssClass="inputnumerico">
                                            </anthem:Label><td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Valor/m2 aula</td>
                                        <td><anthem:TextBox ID="m_EValorM2Aula" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator18" Display="Dynamic" ControlToValidate="m_EValorM2Aula" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Valor/m2 baños</td>
                                        <td><anthem:TextBox ID="m_EValorM2Banos" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator19" Display="Dynamic" ControlToValidate="m_EValorM2Banos" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                        </td>
                                    </tr>
                                        <tr>
                                            <td>
                                                Valor/m2 circulaciones y puntos fijos</td>
                                            <td>
                                                <anthem:TextBox ID="m_EValorM2Circulaciones" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator20" Display="Dynamic" ControlToValidate="m_EValorM2Circulaciones" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Índice de ocupación</td>
                                            <td>
                                                <anthem:TextBox ID="m_EIndiceOcupacion" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator21" Display="Dynamic" ControlToValidate="m_EIndiceOcupacion" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Índice construcción</td>
                                            <td>
                                                <anthem:TextBox ID="m_EIndiceConstruccion2" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator22" Display="Dynamic" ControlToValidate="m_EIndiceConstruccion2" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Observaciones</td>
                                            <td>
                                                <anthem:TextBox ID="m_EObservacionesValores" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior5" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                                <asp:ImageButton ImageUrl="~/imagenes/save.gif" ID="m_BFinalizarE" runat="server" ToolTip="Finalizar" OnClick="m_BAceptar_Click" ValidationGroup="tab" />
                                        <asp:ImageButton ImageUrl="~/imagenes/save.gif" ID="m_BFinalizar" runat="server" ToolTip="Finalizar" OnClick="m_BInsertar_Click" ValidationGroup="tab" /></td>
                                        </tr>
                                    </table>
                                </asp:View>
                            </asp:MultiView>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                            
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
                                                Width="100%" DataSourceID="ObjectDataSource1" UpdateAfterCallBack="False" OnSelectedIndexChanged="m_LCResultados_SelectedIndexChanged" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" OnPageIndexChanged="m_LCResultados_PageIndexChanged" OnSorted="m_LCResultados_PageIndexChanged" OnRowDataBound="m_LCResultados_RowDataBound">
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