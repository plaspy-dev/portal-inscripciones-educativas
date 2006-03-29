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
                            <td>
                                <asp:Button ID="m_BTab1" runat="server" Text="Datos Generales" CssClass="cselected" OnClick="OnTabsClick" UseSubmitBehavior="False" Width="130px"/>
                            </td>
                            <td >
                                <asp:Button ID="m_BTab2" runat="server" Text="Objeto y presupuesto" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False" Width="150px"/>
                            </td>
                            <td >
                                <asp:Button ID="m_BTab3" runat="server" Text="Aspectos Juridicos y formulación" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False" Width="200px"/>
                            </td>
                            <td >
                                <asp:Button ID="m_BTab4" runat="server" Text="Aspectos técnicos" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False" Width="120px"/>
                            </td>
                            <td >
                                <asp:Button ID="m_BTab5" runat="server" Text="Tipos de intervención y población atendida" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False" Width="280px"/>
                            </td>
                            <td>
                                <asp:Button ID="m_BTab6" runat="server" Text="Areas y costos M2" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False" Width="150px"/>
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
                                <table>
                                    <tr>
                                        <td>
                                            <anthem:CheckBox ID="CheckBox1" runat="server" Checked="True" Visible="False" />
                                        </td>
                                        <td>
                                            Ente Territorial<anthem:HiddenField ID="m_nId" runat="server" />
                                        </td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBEnte" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBEnte_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_CBEnte"
                                            ErrorMessage="Por favor seleccione un ente territorial" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
						            <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox2" runat="server" Checked="True" Visible="False" />
                                        </td>
                                        <td >
                                            Municipio</td>
                                        <td ><anthem:DropDownList ID="m_CBMunicipio" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBMunicipio_SelectedIndexChanged">
                                        </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_CBEnte"
                                                ErrorMessage="Por favor seleccione un ente territorial" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox3" runat="server" Checked="True" Visible="False" /></td>
                                        <td >
                                            Código proyecto</td>
                                        <td ><anthem:DropDownList ID="m_CBProyecto" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBProyecto_SelectedIndexChanged">
                                        </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="m_CBProyecto"
                                                ErrorMessage="Por favor seleccione un proyecto" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox4" runat="server" Checked="True" Visible="False" /></td>
                                        <td >
                                            Institución educativa<anthem:HiddenField ID="m_nIdInstitucion" runat="server" /></td>
                                        <td >
                                            <anthem:Label id="m_SInstitucion" runat="server" Text="">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox5" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Fecha ultima revisión</td>
                                                    <td ><anthem:Label ID="m_SFechaUltimaRevision" runat="server" Text="">
                                                    </anthem:Label></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox6" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Concepto de viabilidad</td>
                                                    <td ><anthem:Label ID="m_SConceptoViabilidad" runat="server" Text="">
                                                    </anthem:Label></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox7" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Estado</td>
                                                    <td ><anthem:Label ID="m_SEstado" runat="server" Text="">
                                                    </anthem:Label></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox8" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Observaciones</td>
                                                    <td ><anthem:Label ID="m_SObservaciones" runat="server" Text="">
                                                    </anthem:Label></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Responsable de Viablización</strong></td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <anthem:CheckBox ID="CheckBox9" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            NIT de la firma</td>
                                        <td><anthem:TextBox ID="m_ENITFirma" runat="server">
                                        </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <anthem:CheckBox ID="CheckBox10" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Nombre de la firma</td>
                                        <td><anthem:TextBox ID="m_ENombreFirma" runat="server">
                                        </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <anthem:CheckBox ID="CheckBox11" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Representante legal</td>
                                        <td><anthem:TextBox ID="m_ERepresentante" runat="server">
                                        </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <anthem:CheckBox ID="CheckBox12" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Dirección</td>
                                        <td><anthem:TextBox ID="m_EDireccion" runat="server">
                                        </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <anthem:CheckBox ID="CheckBox13" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Telefono</td>
                                        <td><anthem:TextBox ID="m_ETelefono" runat="server">
                                        </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <anthem:CheckBox ID="CheckBox14" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Fax</td>
                                        <td><anthem:TextBox ID="m_EFax" runat="server">
                                        </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <anthem:CheckBox ID="CheckBox15" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            E-Mail</td>
                                        <td><anthem:TextBox ID="m_EMail" runat="server">
                                        </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="m_BSiguiente" runat="server" Text="Siguiente" OnClick="OnNext"/></td>
                                    </tr>
                                            </table>
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
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <table>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <anthem:HiddenField ID="m_nIdObjeto" runat="server" /><strong>Objeto viabilizado</strong></td>
                                        <td>
                                        <td>
                                        </td>
                                    </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox16" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Concepto</td>
                                            <td><anthem:DropDownList ID="m_CBConcepto" runat="server">
                                            </anthem:DropDownList></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="m_CBConcepto"
                                                    ErrorMessage="Por favor seleccione un concepto" InitialValue="-1" SetFocusOnError="True"
                                                    ValidationGroup="objeto"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox17" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Cantidad</td>
                                            <td><anthem:TextBox ID="m_ECantidad" runat="server" CssClass="inputnumerico">
                                            </anthem:TextBox></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="m_ECantidad"
                                                    ErrorMessage="Por favor ingrese una cantidad" SetFocusOnError="True" ValidationGroup="objeto"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="m_ECantidad"
                                                    ErrorMessage="Por favor ingrese un número aqui" MaximumValue="100000000000" MinimumValue="1"
                                                    SetFocusOnError="True" Type="Currency" ValidationGroup="objeto"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox18" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Unidades</td>
                                            <td><anthem:DropDownList ID="m_CBUnidades" runat="server">
                                            </anthem:DropDownList></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="m_CBUnidades"
                                                    ErrorMessage="Por favor seleccione unidades" InitialValue="-1" SetFocusOnError="True"
                                                    ValidationGroup="objeto"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox19" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Valor</td>
                                            <td><anthem:TextBox ID="m_EValor" runat="server" CssClass="inputnumerico">
                                            </anthem:TextBox></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="m_EValor"
                                                    ErrorMessage="Por favor ingrese un valor" SetFocusOnError="True" ValidationGroup="objeto"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="m_EValor"
                                                    ErrorMessage="Por favor ingrese un número aqui" MaximumValue="100000000000" MinimumValue="1"
                                                    SetFocusOnError="True" Type="Currency" ValidationGroup="objeto"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <anthem:Button ID="m_BInsertarObjeto" runat="server" Text="Insertar" ValidationGroup="objeto" OnClick="m_BInsertarObjeto_Click" />
                                                <anthem:Button ID="m_BAceptarObjeto" runat="server" Text="Aceptar" ValidationGroup="objeto" OnClick="m_BAceptarObjeto_Click" />
                                                <anthem:Button ID="m_BCancelarObjeto" runat="server" Text="Cancelar" CausesValidation="false" OnClick="m_BCancelarObjeto_Click" />
                                                <anthem:Button ID="m_BEliminarObjeto" runat="server" Text="Eliminar" CausesValidation="False" PreCallBackFunction="Eliminar_PreCallBack" OnClick="m_BEliminarObjeto_Click" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
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
                                                <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="../../imagenes/edit.jpg" />
                                                </Columns>
                                            </anthem:GridView>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <strong>Presupuesto Viabilizado</strong></td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox20" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Valor total proyecto</td>
                                            <td><anthem:Label ID="m_SValorTotal" runat="server" Text="">
                                            </anthem:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox21" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Valor partida asignada por el Entidad</td>
                                            <td><anthem:TextBox ID="m_EValorPartidaMEN" runat="server" CssClass="inputnumerico">
                                            </anthem:TextBox></td>
                                            <td>
                                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="m_EValorPartidaMEN"
                                                    ErrorMessage="Por favor ingrese un número aqui" MaximumValue="100000000000" MinimumValue="1"
                                                    SetFocusOnError="True" Type="Currency"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox22" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Porcentaje aportes Entidad educativa</td>
                                            <td><anthem:Label ID="m_SPorcentajeMEN" runat="server" Text="">
                                            </anthem:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox23" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Valor partida asignada municipio proyecto aprobado</td>
                                            <td><anthem:Label ID="m_SValorMun" runat="server" Text="">
                                            </anthem:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox24" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Porcentaje aportes cofinaciación</td>
                                            <td><anthem:Label ID="m_SPorcentajeMun" runat="server" Text="">
                                            </anthem:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox25" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Concepto de viabilidad</td>
                                            <td><anthem:DropDownList ID="m_CBConceptoViablizacion" runat="server">
                                            </anthem:DropDownList></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox26" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Fecha ultima revisión</td>
                                            <td><anthem:Label ID="m_EFechaUltima" runat="server" Text="">
                                            </anthem:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox27" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Fecha Aprobación</td>
                                            <td><anthem:TextBox ID="m_EFechaAprobacion" runat="server">
                                            </anthem:TextBox>
                                                <input id="botonfecha" name="botonfecha" onclick="setFecha('m_EFechaAprobacion')" type="button" value="..." /></td>
                                            <td>
                                                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="m_EFechaAprobacion"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox28" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Observaciones</td>
                                            <td><anthem:TextBox ID="m_EObservaciones" runat="server">
                                            </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="m_BAnterior1" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:Button ID="Button1" runat="server" Text="Siguiente" OnClick="OnNext"/></td>
                                        </tr>
                                    </table>
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="Select"
                                                    TypeName="CDocumentosProyecto"></asp:ObjectDataSource>
                                    <anthem:DataGrid ID="m_LBDocumentos" Runat="server" AutoGenerateColumns="false" DataSourceID="ObjectDataSource3" OnItemDataBound="m_LBDocumentos_ItemDataBound">
                                                <HeaderStyle Font-Bold="True" />
		                                    </anthem:DataGrid>
                                    <table>
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
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox47" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Observaciones</td>
                                            <td>
                                                <anthem:TextBox ID="m_EObservacionesDocumentos" runat="server">
                                                </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="m_BAnterior2" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:Button ID="Button2" runat="server" Text="Siguiente" OnClick="OnNext"/></td>
                                        </tr>
                                    </table>
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                </asp:View>
                                <asp:View ID="View4" runat="server">
                                 <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="Select"
                                                    TypeName="CAspectosTecnicos"></asp:ObjectDataSource>
                                    <anthem:DataGrid ID="m_LBTecnicos" Runat="server" AutoGenerateColumns="false" DataSourceID="ObjectDataSource4">
                                                <HeaderStyle Font-Bold="True" />
		                                    </anthem:DataGrid>
                                    <table>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox64" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Observaciones</td>
                                            <td><anthem:TextBox ID="m_EObservacionesTecnicas" runat="server">
                                            </anthem:TextBox></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            <asp:Button ID="m_BAnterior3" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:Button ID="Button3" runat="server" Text="Siguiente" OnClick="OnNext"/></td>
                                        </tr>
                                    </table>
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                </asp:View>
                                <asp:View ID="View5" runat="server">
                                    <table>
                                    <tr>
                                        <td><anthem:CheckBox ID="CheckBox65" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Tipo</td>
                                        <td><anthem:DropDownList ID="m_CBTipo" runat="server">
                                        </anthem:DropDownList>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Infraestructura</strong></td>
                                        <td>
                                        <td>
                                        </td>
                                    </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Número de aulas nuevas</td>
                                            <td>
                                                <anthem:TextBox ID="m_EAulasN" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                                <asp:RangeValidator ID="RangeValidator5" ControlToValidate="m_EAulasN" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="integer" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
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
                                            </td>
                                            <td>
                                                Número de estudiantes promedio aula</td>
                                            <td>
                                                <anthem:TextBox ID="m_EEstudiantesP" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator6" ControlToValidate="m_EEstudiantesP" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="integer" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Número de jornadas</td>
                                            <td>
                                                <anthem:TextBox ID="m_EJornadas" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator7" ControlToValidate="m_EJornadas" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="integer" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Total ampliación de cobertura</td>
                                            <td>
                                                <anthem:TextBox ID="m_ETotalACobertura" runat="server" CssClass="inputnumerico" ReadOnly="True">
                                                </anthem:TextBox></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Total estudiantes beneficiados aulas nuevas</td>
                                            <td>
                                                <anthem:TextBox ID="m_ETotalBeneficiados" runat="server" CssClass="inputnumerico" ReadOnly="True">
                                                </anthem:TextBox></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Total estudiantes beneficiados aulas reposición</td>
                                            <td>
                                                <anthem:TextBox ID="m_EBeneficiadosReposicion" runat="server" CssClass="inputnumerico" ReadOnly="True">
                                                </anthem:TextBox></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Numero de baños</td>
                                            <td>
                                                <anthem:TextBox ID="m_EBanos" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator11" ControlToValidate="m_EBanos" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="integer" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Aparatos sanitarios</td>
                                            <td>
                                                <anthem:TextBox ID="m_EAparatosS" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator12" ControlToValidate="m_EAparatosS" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="integer" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <strong>Dotación</strong></td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Numero sillas</td>
                                            <td>
                                                <anthem:TextBox ID="m_ESillas" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator13" ControlToValidate="m_ESillas" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="integer" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Valor total dotación</td>
                                            <td>
                                                <anthem:TextBox ID="m_EValorTotalD" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator14" ControlToValidate="m_EValorTotalD" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Numero de estudiantes beneficiados por dotación</td>
                                            <td>
                                                <anthem:TextBox ID="m_EBeneficiadosD" runat="server" CssClass="inputnumerico" ReadOnly="True">
                                                </anthem:TextBox></td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Observaciones</td>
                                            <td>
                                                <anthem:TextBox ID="m_EObservacionesInfraestructura" runat="server">
                                                </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="m_BAnterior4" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:Button ID="Button4" runat="server" Text="Siguiente" OnClick="OnNext"/></td>
                                        </tr>
                                    </table>
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                </asp:View>
                                <asp:View ID="View6" runat="server">
                                    <table>
                                    <tr>
                                        <td><anthem:CheckBox ID="CheckBox79" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Valor total proyecto</td>
                                        <td>
                                            <anthem:Label ID="m_SValorTotalProyecto" runat="server" Text="" CssClass="inputnumerico">
                                            </anthem:Label><td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><anthem:CheckBox ID="CheckBox80" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Aulas</td>
                                        <td><anthem:TextBox ID="m_EAulas" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator8" ControlToValidate="m_EAulas" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><anthem:CheckBox ID="CheckBox81" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Baños</td>
                                        <td><anthem:TextBox ID="m_EBanosV" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator9" ControlToValidate="m_EBanosV" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td><anthem:CheckBox ID="CheckBox82" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Circulaciones y puntos fijos</td>
                                        <td><anthem:TextBox ID="m_ECirculaciones" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator10" ControlToValidate="m_ECirculaciones" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><anthem:CheckBox ID="CheckBox83" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Área total proyecto</td>
                                        <td>
                                            <anthem:Label ID="m_SAreaTotal" runat="server" Text="" CssClass="inputnumerico">
                                            </anthem:Label><td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><anthem:CheckBox ID="CheckBox84" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            M2 de aulas</td>
                                        <td><anthem:TextBox ID="m_EM2Aulas" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator15" ControlToValidate="m_EM2Aulas" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td><anthem:CheckBox ID="CheckBox85" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            M2 de baños</td>
                                        <td><anthem:TextBox ID="m_EM2Banos" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator16" ControlToValidate="m_EM2Banos" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><anthem:CheckBox ID="CheckBox86" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            M2 circulaciones y puntos fijos</td>
                                        <td><anthem:TextBox ID="m_EM2Circulaciones" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator17" ControlToValidate="m_EM2Circulaciones" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><anthem:CheckBox ID="CheckBox87" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Valor total m2 proyecto</td>
                                        <td>
                                            <anthem:Label ID="m_SValorM2Total" runat="server" Text="" CssClass="inputnumerico">
                                            </anthem:Label><td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><anthem:CheckBox ID="CheckBox88" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Valor/m2 aula</td>
                                        <td><anthem:TextBox ID="m_EValorM2Aula" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator18" ControlToValidate="m_EValorM2Aula" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><anthem:CheckBox ID="CheckBox89" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Valor/m2 baños</td>
                                        <td><anthem:TextBox ID="m_EValorM2Banos" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator19" ControlToValidate="m_EValorM2Banos" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                        </td>
                                    </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox90" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Valor/m2 circulaciones y puntos fijos</td>
                                            <td>
                                                <anthem:TextBox ID="m_EValorM2Circulaciones" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator20" ControlToValidate="m_EValorM2Circulaciones" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox91" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Índice de ocupación</td>
                                            <td>
                                                <anthem:TextBox ID="m_EIndiceOcupacion" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator21" ControlToValidate="m_EIndiceOcupacion" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox92" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Índice construcción</td>
                                            <td>
                                                <anthem:TextBox ID="m_EIndiceConstruccion2" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator22" ControlToValidate="m_EIndiceConstruccion2" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1">
                                                </asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox93" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Observaciones</td>
                                            <td>
                                                <anthem:TextBox ID="m_EObservacionesValores" runat="server">
                                                </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="m_BAnterior5" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                                <asp:Button ID="m_BFinalizarE" runat="server" Text="Finalizar" OnClick="m_BAceptar_Click" ValidationGroup="tab" />
                                        <asp:Button ID="m_BFinalizar" runat="server" Text="Finalizar" OnClick="m_BInsertar_Click" ValidationGroup="tab" /></td>
                                        </tr>
                                    </table>
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