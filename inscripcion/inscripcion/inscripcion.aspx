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
                                <table cellspacing="0">
                                    <tr>
                                        <td>
                                            Código del proyecto<anthem:HiddenField ID="m_nId" runat="server" />
                                        </td>
                                        <td>
                                            <anthem:TextBox ID="m_ECodigo" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_ECodigo" Display="Dynamic"
                                            ErrorMessage="Por favor ingrese un código" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
						            <tr>
                                        <td >
                                            Ente Territorial</td>
                                        <td >
                                            <anthem:DropDownList ID="m_EEnte" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_EEnte_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_EEnte" Display="Dynamic"
                                                ErrorMessage="Por favor seleccione un ente territorial" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
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
                                            Código DANE institución educativa</td>
                                        <td ><anthem:TextBox ID="m_EInstitucion" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="m_EInstitucion" Display="Dynamic"
                                                ErrorMessage="Por favor ingrese un código" SetFocusOnError="True"></asp:RequiredFieldValidator>&nbsp;
                                                </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Nombre Institución educativa</td>
                                        <td><anthem:HiddenField ID="m_nIdInstitucion" runat="server" />
                                            <anthem:Label ID="m_ENombreInstitucion" runat="server"></anthem:Label></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        <anthem:Button ID="m_BVerificar" runat="server" Text="Verificar" CausesValidation="False" OnClick="m_BVerificar_Click" /></td>
                                        <td>
                                            <asp:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="m_BSiguiente" runat="server" ToolTip="Siguiente" OnClick="OnNext"  CausesValidation="true"/></td>
                                    </tr>
                                            </table>
                                    <table width="100%">
                                    <tr>
                                        <td>
                                            <br />
                                            <anthem:Label ID="m_SMensaje" runat="server"></anthem:Label><br />
                                            <anthem:ImageButton ID="m_BInsertar" ToolTip="Insertar" runat="server" ImageUrl="~/imagenes/save.gif" OnClick="m_BInsertar_Click" /><anthem:ImageButton ID="m_BImprimir" runat="server" ToolTip="Imprimir" ImageUrl="~/imagenes/print.gif" OnClick="m_BImprimir_Click" CausesValidation="False" />
                                            <anthem:DropDownList ID="m_CBImprimir" runat="server">
                                                <asp:ListItem Value="1">Microsoft Excel</asp:ListItem>
                                                <asp:ListItem Value="0">En Pantalla</asp:ListItem>
                                            </anthem:DropDownList>
                                            <anthem:ImageButton ID="m_BAceptarP" runat="server" ImageUrl="~/imagenes/print.gif" ToolTip="Imprimir" CausesValidation="False" OnClick="m_BAceptarP_Click" /><anthem:ImageButton ID="m_BCancelarP" runat="server" ToolTip="Cancelar" ImageUrl="~/imagenes/stop.gif" CausesValidation="False" OnClick="m_BCancelarP_Click" />
                                            
                                            
                                        <anthem:ImageButton ID="m_BAceptar" runat="server" ImageUrl="~/imagenes/save.gif" ToolTip="Guardar" OnClick="m_BAceptar_Click" /><anthem:ImageButton ID="m_BCancelar" ToolTip="Cancelar" runat="server" ImageUrl="~/imagenes/stop.gif" OnClick="m_BCancelar_Click" CausesValidation="False" /><anthem:ImageButton ID="m_BEliminar" runat="server" ImageUrl="~/imagenes/trash.gif" ToolTip="Eliminar" CausesValidation="False" OnClick="m_BEliminar_Click" PreCallBackFunction="Eliminar_PreCallBack"/></td>
                                    </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <table cellspacing="0">
                                    <tr>
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
                                            No. Resolucion Media</td>
                                        <td ><anthem:TextBox ID="m_EResolucion" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Fecha de resolución</td>
                                        <td ><anthem:TextBox ID="m_EFechaResolucion" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button2" name="botonfecha"  onclick="setFecha('m_EFechaResolucion')" type="button" value="..." /></td>
                                        <td><asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="m_EFechaResolucion" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                            SetFocusOnError="True" Type="Date" ></asp:RangeValidator>
                                            </td>
                                    </tr>
                                        <tr>
                                            <td>
                                                <strong>Ampliación de matricula</strong></td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Año actual</td>
                                            <td>
                                                <anthem:Label ID="m_SActual" runat="server"></anthem:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Año pasado</td>
                                            <td>
                                                <anthem:Label ID="m_SAnterior" runat="server"></anthem:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Variación</td>
                                            <td>
                                                <anthem:Label ID="m_SVariacion" runat="server"></anthem:Label></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Identificación</strong></td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Nombre rector</td>
                                            <td>
                                                <anthem:TextBox ID="m_ENombre" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Dirección</td>
                                            <td>
                                                <anthem:TextBox ID="m_EDireccion" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Teléfono</td>
                                            <td>
                                                <anthem:TextBox ID="m_ETelefono" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Celular</td>
                                            <td>
                                                <anthem:TextBox ID="m_ECelular" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Fax</td>
                                            <td>
                                                <anthem:TextBox ID="m_EFax" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                E-mail</td>
                                            <td>
                                                <anthem:TextBox ID="m_EEmail" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="Button1" runat="server" ToolTip="Siguiente" OnClick="OnNext"  CausesValidation="true"/></td>
                                        </tr>
                                            </table>
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                    <table cellspacing="0">
                                    <tr>
                                        <td>
                                            Criticidad</td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBCriticidad" runat="server" AutoCallBack="False">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="m_CBCriticidad" Display="Dynamic"
                                            ErrorMessage="Por favor seleccione un valor" InitialValue="-1" SetFocusOnError="True" ValidationGroup="tab" ></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                        <tr>
                                            <td>
                                                Tipo</td>
                                            <td>
                                                <anthem:DropDownList ID="m_CBTipo" runat="server" AutoCallBack="False">
                                                </anthem:DropDownList></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="m_CBTipo" Display="Dynamic"
                                                    ErrorMessage="Por favor seleccione tipo" InitialValue="-1" SetFocusOnError="True" ValidationGroup="tab" ></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Número de resolución de emergencia</td>
                                            <td>
                                                <anthem:TextBox ID="m_EResolucionEmergencia" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Fecha resolución</td>
                                            <td>
                                                <anthem:TextBox ID="m_EFechaEmergencia" runat="server" CssClass="editfecha"></anthem:TextBox><input id="botonfecha" name="botonfecha"  onclick="setFecha('m_EFechaEmergencia')" type="button" value="..." /></td>
                                            <td>
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="m_EFechaEmergencia" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date" ValidationGroup="tab"></asp:RangeValidator></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Objeto</strong></td>
                                            <td><anthem:HiddenField ID="m_nIdObjeto" runat="server" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Concepto</td>
                                            <td>
                                                <anthem:DropDownList ID="m_CBConcepto" runat="server" AutoCallBack="False">
                                                </anthem:DropDownList></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="m_CBConcepto" Display="Dynamic"
                                                    ErrorMessage="Por favor seleccione un concepto" InitialValue="-1" SetFocusOnError="True" ValidationGroup="objeto"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Cantidad</td>
                                            <td>
                                                <anthem:TextBox ID="m_ECantidad" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="m_ECantidad" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese una cantidad" SetFocusOnError="True" ValidationGroup="objeto"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="m_ECantidad" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="900000000000000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Currency" ValidationGroup="objeto"></asp:RangeValidator>
                                                    </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Unidades</td>
                                            <td>
                                                <anthem:DropDownList ID="m_CBUnidades" runat="server" AutoCallBack="False">
                                                </anthem:DropDownList></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="m_CBUnidades" Display="Dynamic"
                                                    ErrorMessage="Por favor seleccione unas unidades" InitialValue="-1" SetFocusOnError="True" ValidationGroup="objeto"></asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Valor</td>
                                            <td>
                                                <anthem:TextBox ID="m_EValor" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="m_EValor" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese un valor" SetFocusOnError="True" ValidationGroup="objeto"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="m_EValor" Display="Dynamic"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="900000000000000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Currency" ValidationGroup="objeto"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
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
                                                <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="../../imagenes/edit.gif" />
                                                </Columns>
                                            </anthem:GridView>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Presupuesto</strong></td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Porcentaje aportes Entidad educativa</td>
                                            <td>
                                                <anthem:DropDownList ID="m_CBPorcentaje" runat="server" AutoCallBack="False">
                                                </anthem:DropDownList></td>
                                            <td>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Estudiantes Beneficiados por aulas nuevas</td>
                                            <td>
                                                <anthem:TextBox ID="m_EEstudiantesBeneficiados" runat="server" CssClass="inputnumerico">
                                                </anthem:TextBox></td>
                                            <td>
                                                </td>
                                        </tr>
                                        <tr>
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
                                                <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior2" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:ImageButton ImageUrl="~/imagenes/save.gif" ID="m_BFinalizarE" runat="server" ToolTip="Finalizar" OnClick="m_BAceptar_Click" ValidationGroup="tab" />
                                        <asp:ImageButton ImageUrl="~/imagenes/save.gif" ID="m_BFinalizar" runat="server" ToolTip="Finalizar" OnClick="m_BInsertar_Click" ValidationGroup="tab" /></td>
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