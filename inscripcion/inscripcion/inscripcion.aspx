<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inscripcion.aspx.cs" Inherits="inscripcion_inscripcion_inscripcion" %>

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
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab1" runat="server" Text="Inscripción" Width="100px" CssClass="cselected" OnClick="OnTabsClick" CausesValidation="false" UseSubmitBehavior="False"/>
                            </td>
                            <td style="height: 20px;">
                                <asp:Button ID="m_BTab2" runat="server" Text="Datos generales I.E." CssClass="cinftab" OnClick="OnTabsClick" CausesValidation="true" UseSubmitBehavior="False"/>
                            </td>
                            <td style="height: 20px;">
                                <asp:Button ID="m_BTab3" runat="server" Text="Datos generales proyecto" CssClass="cinftab" OnClick="OnTabsClick" CausesValidation="true" UseSubmitBehavior="False"/>
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
                          <anthem:Label ID="m_ETitulo" runat="server" Text="Nuevo Proyecto"></anthem:Label></div>
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
                                            Código del proyecto<anthem:HiddenField ID="m_nId" runat="server" />
                                        </td>
                                        <td>
                                            <anthem:TextBox ID="m_ECodigo" runat="server" CssClass="inputnumerico"></anthem:TextBox></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_ECodigo"
                                            ErrorMessage="Por favor ingrese un código" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="m_ECodigo"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="1000000000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                                        </td>
                                    </tr>
						            <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox2" runat="server" Checked="True" Visible="False" />
                                        </td>
                                        <td >
                                            Ente Territorial</td>
                                        <td >
                                            <anthem:DropDownList ID="m_EEnte" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_EEnte_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_EEnte"
                                                ErrorMessage="Por favor seleccione un ente territorial" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox3" runat="server" Checked="True" Visible="False" /></td>
                                        <td >
                                            Municipio</td>
                                        <td >
                                            <anthem:DropDownList ID="m_EMunicipio" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_EMunicipio_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox4" runat="server" Checked="True" Visible="False" /></td>
                                        <td >
                                            Código DANE institución educativa</td>
                                        <td ><anthem:TextBox ID="m_EInstitucion" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="m_EInstitucion"
                                                ErrorMessage="Por favor ingrese un código" SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;
                                                </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Nombre Institución educativa</td>
                                        <td><anthem:HiddenField ID="m_nIdInstitucion" runat="server" />
                                            <anthem:Label ID="m_ENombreInstitucion" runat="server"></anthem:Label></td>
                                        <td><anthem:Button ID="m_BVerificar" runat="server" Text="Verificar" CausesValidation="False" OnClick="m_BVerificar_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="m_BSiguiente" runat="server" Text="Siguiente" OnClick="OnNext"  CausesValidation="true"/></td>
                                    </tr>
                                            </table>
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                    <table width="100%">
                                    <tr>
                                        <td>
                                            <br />
                                            <anthem:Label ID="m_SMensaje" runat="server"></anthem:Label><br />
                                            <anthem:Button ID="m_BInsertar" runat="server" Text="Insertar" OnClick="m_BInsertar_Click" /><anthem:Button ID="m_BImprimir" runat="server" Text="Imprimir" OnClick="m_BImprimir_Click" CausesValidation="False" />
                                            <anthem:DropDownList ID="m_CBImprimir" runat="server">
                                                <asp:ListItem Value="1">Microsoft Excel</asp:ListItem>
                                                <asp:ListItem Value="0">En Pantalla</asp:ListItem>
                                            </anthem:DropDownList>
                                            <anthem:Button ID="m_BAceptarP" runat="server" Text="Aceptar" CausesValidation="False" OnClick="m_BAceptarP_Click" PreCallBackFunction="preprintform" /><anthem:Button ID="m_BCancelarP" runat="server" Text="Cancelar" CausesValidation="False" OnClick="m_BCancelarP_Click" />
                                            <anthem:LinkButton ID="m_LBTodos" runat="server" CausesValidation="False" PreCallBackFunction="checkall">Todos</anthem:LinkButton>
                                            <anthem:LinkButton ID="m_LBNinguno" runat="server" CausesValidation="False" PreCallBackFunction="uncheckall">Ninguno</anthem:LinkButton>
                                        <anthem:Button ID="m_BAceptar" runat="server" Text="Aceptar" OnClick="m_BAceptar_Click" ValidationGroup="tab" /><anthem:Button ID="m_BCancelar" runat="server" Text="Cancelar" OnClick="m_BCancelar_Click" CausesValidation="False" /><anthem:Button ID="m_BEliminar" runat="server" Text="Eliminar" CausesValidation="False" OnClick="m_BEliminar_Click" PreCallBackFunction="Eliminar_PreCallBack"/></td>
                                    </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <table>
                                    <tr>
                                        <td>
                                            <anthem:CheckBox ID="CheckBox5" runat="server" Checked="True" Visible="False" />
                                        </td>
                                        <td>
                                            Jornadas</td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBJornadas" runat="server" AutoCallBack="False">
                                            </anthem:DropDownList></td>
                                        <td>
                                            </td>
                                    </tr>
						            <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox6" runat="server" Checked="True" Visible="False" />
                                        </td>
                                        <td >
                                            No. Resolucion Media</td>
                                        <td ><anthem:TextBox ID="m_EResolucion" runat="server">
                                        </anthem:TextBox></td>
                                        <td>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox7" runat="server" Checked="True" Visible="False" /></td>
                                        <td >
                                            Fecha de resolución</td>
                                        <td ><anthem:TextBox ID="m_EFechaResolucion" runat="server">
                                        </anthem:TextBox><input id="Button2" name="botonfecha"  onclick="setFecha('m_EFechaResolucion')" type="button" value="..." /></td>
                                        <td><asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="m_EFechaResolucion"
                                                            ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                            SetFocusOnError="True" Type="Date" ></asp:RangeValidator>
                                            </td>
                                    </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <strong>Ampliación de matricula</strong></td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <anthem:CheckBox ID="CheckBox8" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Año actual</td>
                                            <td>
                                                <anthem:TextBox ID="m_EActual" runat="server" ReadOnly="True"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox9" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Año pasado</td>
                                            <td>
                                                <anthem:TextBox ID="m_EAnterior" runat="server" ReadOnly="True"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox10" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Variación</td>
                                            <td>
                                                <anthem:TextBox ID="m_EVariacion" runat="server" ReadOnly="True"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <strong>Identificación</strong></td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox11" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Nombre rector</td>
                                            <td>
                                                <anthem:TextBox ID="m_ENombre" runat="server">
                                        </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox12" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Dirección</td>
                                            <td>
                                                <anthem:TextBox ID="m_EDireccion" runat="server">
                                        </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox13" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Teléfono</td>
                                            <td>
                                                <anthem:TextBox ID="m_ETelefono" runat="server">
                                        </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox14" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Celular</td>
                                            <td>
                                                <anthem:TextBox ID="m_ECelular" runat="server">
                                        </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox15" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Fax</td>
                                            <td>
                                                <anthem:TextBox ID="m_EFax" runat="server">
                                        </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox16" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                E-mail</td>
                                            <td>
                                                <anthem:TextBox ID="m_EEmail" runat="server">
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
                                                <asp:Button ID="m_BAnterior" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:Button ID="Button1" runat="server" Text="Siguiente" OnClick="OnNext"  CausesValidation="true"/></td>
                                        </tr>
                                            </table>
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                    <table>
                                    <tr>
                                        <td>
                                            <anthem:CheckBox ID="CheckBox17" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Criticidad</td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBCriticidad" runat="server" AutoCallBack="False">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="m_CBCriticidad"
                                            ErrorMessage="Por favor seleccione un valor" InitialValue="-1" SetFocusOnError="True" ValidationGroup="tab" ></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox18" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Tipo</td>
                                            <td>
                                                <anthem:DropDownList ID="m_CBTipo" runat="server" AutoCallBack="False">
                                                </anthem:DropDownList></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="m_CBTipo"
                                                    ErrorMessage="Por favor seleccione tipo" InitialValue="-1" SetFocusOnError="True" ValidationGroup="tab" ></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox19" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Número de resolución de emergencia</td>
                                            <td>
                                                <anthem:TextBox ID="m_EResolucionEmergencia" runat="server">
                                                </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox20" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Fecha resolución</td>
                                            <td>
                                                <anthem:TextBox ID="m_EFechaEmergencia" runat="server">
                                                </anthem:TextBox><input id="botonfecha" name="botonfecha"  onclick="setFecha('m_EFechaEmergencia')" type="button" value="..." /></td>
                                            <td>
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="m_EFechaEmergencia"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date" ValidationGroup="tab"></asp:RangeValidator></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <strong>Objeto</strong></td>
                                            <td><anthem:HiddenField ID="m_nIdObjeto" runat="server" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox21" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Concepto</td>
                                            <td>
                                                <anthem:DropDownList ID="m_CBConcepto" runat="server" AutoCallBack="False">
                                                </anthem:DropDownList></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="m_CBConcepto"
                                                    ErrorMessage="Por favor seleccione un concepto" InitialValue="-1" SetFocusOnError="True" ValidationGroup="objeto"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox22" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Cantidad</td>
                                            <td>
                                                <anthem:TextBox ID="m_ECantidad" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="m_ECantidad"
                                                    ErrorMessage="Por favor ingrese una cantidad" SetFocusOnError="True" ValidationGroup="objeto"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="m_ECantidad"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="100000000000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Currency" ValidationGroup="objeto"></asp:RangeValidator>
                                                    </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox23" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Unidades</td>
                                            <td>
                                                <anthem:DropDownList ID="m_CBUnidades" runat="server" AutoCallBack="False">
                                                </anthem:DropDownList></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="m_CBUnidades"
                                                    ErrorMessage="Por favor seleccione unas unidades" InitialValue="-1" SetFocusOnError="True" ValidationGroup="objeto"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox24" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Valor</td>
                                            <td>
                                                <anthem:TextBox ID="m_EValor" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="m_EValor"
                                                    ErrorMessage="Por favor ingrese un valor" SetFocusOnError="True" ValidationGroup="objeto"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="m_EValor"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="100000000000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Currency" ValidationGroup="objeto"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td><anthem:Button ID="m_BInsertarObjeto" runat="server" Text="Insertar" ValidationGroup="objeto" OnClick="m_BInsertarObjeto_Click" />
                                                <anthem:Button ID="m_BAceptarObjeto" runat="server" Text="Aceptar" ValidationGroup="objeto" OnClick="m_BAceptarObjeto_Click" />
                                                <anthem:Button ID="m_BCancelarObjeto" runat="server" Text="Cancelar" CausesValidation="false" OnClick="m_BCancelarObjeto_Click" />
                                                <anthem:Button ID="m_BEliminarObjeto" runat="server" Text="Eliminar" CausesValidation="False" PreCallBackFunction="Eliminar_PreCallBack" OnClick="m_BEliminarObjeto_Click" /></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="Select"
                                                    TypeName="CTablaObjeto"></asp:ObjectDataSource>
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
                                                <strong>Presupuesto</strong></td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox25" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Porcentaje aportes Entidad educativa</td>
                                            <td>
                                                <anthem:DropDownList ID="m_CBPorcentaje" runat="server" AutoCallBack="False">
                                                </anthem:DropDownList></td>
                                            <td>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox26" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Estudiantes Beneficiados por aulas nuevas</td>
                                            <td>
                                                <anthem:TextBox ID="m_EEstudiantesBeneficiados" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox27" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Estudiantes beneficiados por dotación</td>
                                            <td>
                                                <anthem:TextBox ID="m_EDotacion" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox28" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Observaciones</td>
                                            <td>
                                                <anthem:TextBox ID="m_EObservaciones" runat="server">
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
                                        <asp:Button ID="m_BFinalizarE" runat="server" Text="Finalizar" OnClick="m_BAceptar_Click" ValidationGroup="tab" />
                                        <asp:Button ID="m_BFinalizar" runat="server" Text="Finalizar" OnClick="m_BInsertar_Click" ValidationGroup="tab" /></td>
                                        </tr>
                                            </table>
                                    <div style="text-align: right">
                                        &nbsp;
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                            </td>
                            </tr>
                            <tr>
                            <td class="final">                            
                                <table width="100%">
                                    <tr>
                                        <td>
                                            &nbsp;</td>
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