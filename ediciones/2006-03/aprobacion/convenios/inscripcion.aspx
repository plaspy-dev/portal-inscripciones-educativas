<%@ Page Language="C#" AutoEventWireup="true" CodeFile="inscripcion.aspx.cs" Inherits="aprobacion_convenios_inscripcion" %>

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
                        <anthem:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="View1" runat="server">
                                <table width="100%">
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
                                            ErrorMessage="Por favor seleccione un ente" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
						            <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox2" runat="server" Checked="True" Visible="False" />
                                        </td>
                                        <td >
                                            Numero de convenio</td>
                                        <td ><anthem:TextBox ID="m_ENumero" runat="server" CssClass="inputnumerico" MaxLength="3">
                                        </anthem:TextBox></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_ENumero"
                                                ErrorMessage="Por favor ingrese un número"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="m_ENumero"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="999" MinimumValue="100"
                                                            SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox3" runat="server" Checked="True" Visible="False" /></td>
                                        <td >
                                            Valor convenio</td>
                                        <td ><anthem:TextBox ID="m_EValor" runat="server" CssClass="inputnumerico">
                                        </anthem:TextBox></td>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="m_EValor"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="100000000000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Currency"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox4" runat="server" Checked="True" Visible="False" /></td>
                                        <td >
                                            Tipo</td>
                                        <td >
                                            <anthem:DropDownList ID="m_CBTipo" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBTipo_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="m_CBTipo"
                                                ErrorMessage="Por favor seleccione un tipo" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                                        <anthem:CheckBox ID="CheckBox5" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Objeto</td>
                                        <td><anthem:TextBox ID="m_EObjeto" runat="server">
                                        </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox6" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Plazo total en dias</td>
                                                    <td ><anthem:TextBox ID="m_EPlazo" runat="server" CssClass="inputnumerico" MaxLength="3">
                                                    </anthem:TextBox></td>
                                                    <td>
                                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="m_EPlazo"
                                                            ErrorMessage="Por favor ingrese un número aqui" MaximumValue="1000" MinimumValue="1"
                                                            SetFocusOnError="True" Type="Integer"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox7" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Fecha de aprobación</td>
                                                    <td ><anthem:TextBox ID="m_EFechaAprobacion" runat="server">
                                                    </anthem:TextBox><input id="botonfecha" name="botonfecha"  onclick="setFecha('m_EFechaAprobacion')" type="button" value="..." />
                                                    </td>
                                                    <td>
                                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="m_EFechaAprobacion"
                                                            ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                            SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox8" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Fecha de vencimiento</td>
                                                    <td ><anthem:TextBox ID="m_EFechaVencimiento" runat="server" ReadOnly="True">
                                                    </anthem:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                            <anthem:CheckBox ID="CheckBox9" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Estado</td>
                                                    <td >
                                                        <anthem:DropDownList ID="m_CBEstado" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBEstado_SelectedIndexChanged">
                                                        </anthem:DropDownList></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="m_CBEstado"
                                                            ErrorMessage="Por favor seleccione un estado" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
                                                </tr>
                                    <tr>
                                        <td>
                                            <anthem:CheckBox ID="CheckBox10" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            Observaciones</td>
                                        <td>
                                            <anthem:TextBox ID="m_EObservaciones" runat="server">
                                            </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                            </table>
                                    <div style="text-align: right"><asp:Button ID="m_BSiguiente" runat="server" Text="Siguiente" OnClick="OnNext"  CausesValidation="true"/></div>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <table width="100%">
                                    <tr>
                                        <td>
                                            <anthem:CheckBox ID="CheckBox11" runat="server" Checked="True" Visible="False" /></td>
                                        <td>
                                            NIT ente territorial</td>
                                        <td><anthem:TextBox ID="m_ENIT" runat="server" ReadOnly="True">
                                        </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <strong>Ordenador del gasto</strong></td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td><anthem:CheckBox ID="CheckBox12" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Tipo de gasto</td>
                                            <td><anthem:DropDownList ID="m_CBTipoGasto" runat="server">
                                            </anthem:DropDownList></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox13" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Registro presupuestal</td>
                                            <td>
                                                <anthem:TextBox ID="m_ERDP" runat="server">
                                                </anthem:TextBox>
                                                </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox14" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Fecha Registro</td>
                                            <td>
                                                <anthem:TextBox ID="m_EFechaRDP" runat="server">
                                                </anthem:TextBox>
                                                <input id="Button1" name="botonfecha"  onclick="setFecha('m_EFechaRDP')" type="button" value="..." />
                                                </td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="m_EFechaRDP"
                                                            ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                            SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox15" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Certificado de disponibilidad presupuestal</td>
                                            <td>
                                                <anthem:TextBox ID="m_ECDP" runat="server">
                                                </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <anthem:CheckBox ID="CheckBox16" runat="server" Checked="True" Visible="False" /></td>
                                            <td>
                                                Fecha Certificado</td>
                                            <td>
                                                <anthem:TextBox ID="m_EFechaCDP" runat="server">
                                                </anthem:TextBox>
                                                <input id="Button2" name="botonfecha"  onclick="setFecha('m_EFechaCDP')" type="button" value="..." />
                                                </td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="m_EFechaCDP"
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
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                            <anthem:DataGrid ID="m_LBDocumentos" Runat="server" AutoGenerateColumns="false" DataSourceID="ObjectDataSource2" OnItemDataBound="m_LBDocumentos_ItemDataBound">
                                                <HeaderStyle Font-Bold="True" />
		                                    </anthem:DataGrid>
		                                    <div style="text-align: right">
                                        <asp:Button ID="m_BAnterior" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" /></div>
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
                                            <anthem:Button ID="m_BInsertar" runat="server" Text="Insertar" OnClick="m_BInsertar_Click" /><anthem:Button ID="m_BImprimir" runat="server" Text="Imprimir" OnClick="m_BImprimir_Click" CausesValidation="False" />
                                            <anthem:DropDownList ID="m_CBImprimir" runat="server">
                                                <asp:ListItem Value="1">Microsoft Excel</asp:ListItem>
                                                <asp:ListItem Value="0">En Pantalla</asp:ListItem>
                                            </anthem:DropDownList>
                                            <anthem:Button ID="m_BAceptar" runat="server" Text="Aceptar" OnClick="m_BAceptar_Click" /><anthem:Button ID="m_BCancelar" runat="server" Text="Cancelar" OnClick="m_BCancelar_Click" CausesValidation="False" /><anthem:Button ID="m_BEliminar" runat="server" Text="Eliminar" CausesValidation="False" OnClick="m_BEliminar_Click" PreCallBackFunction="Eliminar_PreCallBack"/>
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