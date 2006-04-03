<%@ Page Language="C#" AutoEventWireup="true" CodeFile="control.aspx.cs" Inherits="seguimiento_convenios_control" %>

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
                                <asp:Button ID="m_BTab1" runat="server" Text="Convenios Marco" Width="150px" CssClass="cselected" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab2" runat="server" Text="Modificaciones" Width="120px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab3" runat="server" Text="Actualización de documentos" Width="200px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab4" runat="server" Text="Desembolsos" Width="100px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab5" runat="server" Text="Rendimientos" Width="100px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
                            </td>
                            <td style="width: 100px; height: 20px;">
                                <asp:Button ID="m_BTab6" runat="server" Text="Liquidación" Width="100px" CssClass="cinftab" OnClick="OnTabsClick" UseSubmitBehavior="False"/>
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
                          <anthem:Label ID="m_STitulo" runat="server" Text="Seleccione un convenio"></anthem:Label></div>
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
                                            Número de Convenio</td>
                                        <td ><anthem:DropDownList ID="m_CBConvenio" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBConvenio_SelectedIndexChanged">
                                        </anthem:DropDownList></td>
                                        <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_CBConvenio"
                                            ErrorMessage="Por favor seleccione un convenio" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox3" runat="server" Checked="True" Visible="False" /></td>
                                        <td >
                                            Valor</td>
                                        <td >
                                            <anthem:TextBox ID="m_EValor" runat="server" ReadOnly="True"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <anthem:CheckBox ID="CheckBox4" runat="server" Checked="True" Visible="False" /></td>
                                        <td >
                                            Estado</td>
                                        <td ><anthem:DropDownList ID="m_CBEstadoConvenio" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBEstadoConvenio_SelectedIndexChanged" >
                                        </anthem:DropDownList></td>
                                        <td>
                                        </td>
                                    </tr>
                                                <tr>
                                                    <td >
                                                        <anthem:CheckBox ID="CheckBox5" runat="server" Checked="True" Visible="False" /></td>
                                                    <td >
                                                        Observaciones</td>
                                                    <td ><anthem:TextBox ID="m_EObservaciones" runat="server">
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
                                            <anthem:Button ID="m_BImprimir" runat="server" Text="Imprimir" OnClick="m_BImprimir_Click" CausesValidation="False" />
                                            <anthem:DropDownList ID="m_CBImprimir" runat="server">
                                                <asp:ListItem Value="1">Microsoft Excel</asp:ListItem>
                                                <asp:ListItem Value="0">En Pantalla</asp:ListItem>
                                            </anthem:DropDownList>
                                            <anthem:Button ID="m_BAceptar" runat="server" Text="Aceptar" OnClick="m_BAceptar_Click" /><anthem:Button ID="m_BCancelar" runat="server" Text="Cancelar" OnClick="m_BCancelar_Click" CausesValidation="False" />
                                            <anthem:Button ID="m_BAceptarP" runat="server" Text="Aceptar" CausesValidation="False" OnClick="m_BAceptarP_Click" PreCallBackFunction="preprintform" /><anthem:Button ID="m_BCancelarP" runat="server" Text="Cancelar" CausesValidation="False" OnClick="m_BCancelarP_Click" />
                                            <anthem:LinkButton ID="m_LBTodos" runat="server" CausesValidation="False" PreCallBackFunction="checkall">Todos</anthem:LinkButton>
                                            <anthem:LinkButton ID="m_LBNinguno" runat="server" CausesValidation="False" PreCallBackFunction="uncheckall">Ninguno</anthem:LinkButton>
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    &nbsp;<table>
                                        <tr>
                                            <td>
                                    <table>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Adicional no.</td>
                                            <td><anthem:Label ID="m_SNoAdicion" runat="server" Text="" >
                                            </anthem:Label></td>
                                            <td><anthem:HiddenField ID="m_nIdAdicion" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Adición en valor</td>
                                            <td><anthem:TextBox ID="m_EAdicionValor" runat="server" CssClass="inputnumerico">
                                            </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator1" ControlToValidate="m_EAdicionValor" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1" ValidationGroup="objeto"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Fecha adición</td>
                                            <td><anthem:TextBox ID="m_EFechaAdicion" runat="server">
                                            </anthem:TextBox><input id="botonfecha" name="botonfecha"  onclick="setFecha('m_EFechaAdicion')" type="button" value="..." /></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="m_EFechaAdicion"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date" ValidationGroup="objeto"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Certificado de disponibilidad presupuestal</td>
                                            <td><anthem:TextBox ID="m_ECDPAdicion" runat="server">
                                            </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Fecha certificado</td>
                                            <td><anthem:TextBox ID="m_EFechaCDP" runat="server">
                                            </anthem:TextBox><input id="Button5" name="botonfecha"  onclick="setFecha('m_EFechaCDP')" type="button" value="..." /></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="m_EFechaCDP"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date" ValidationGroup="objeto"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Registro presupuestal</td>
                                            <td><anthem:TextBox ID="m_ERDPAdicion" runat="server">
                                            </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Fecha registro</td>
                                            <td><anthem:TextBox ID="m_EFechaRDP" runat="server">
                                            </anthem:TextBox><input id="Button6" name="botonfecha"  onclick="setFecha('m_EFechaRDP')" type="button" value="..." /></td>
                                            <td>
                                                <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="m_EFechaRDP"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date" ValidationGroup="objeto"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Prorroga (en días calendario)</td>
                                            <td><anthem:TextBox ID="m_EProrroga" runat="server" CssClass="inputnumerico">
                                            </anthem:TextBox></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator2" ControlToValidate="m_EProrroga" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="integer" MaximumValue="10000000" MinimumValue="1" ValidationGroup="objeto"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Fecha prorroga</td>
                                            <td><anthem:TextBox ID="m_EFechaProrroga" runat="server">
                                            </anthem:TextBox><input id="Button7" name="botonfecha"  onclick="setFecha('m_EFechaProrroga')" type="button" value="..." /></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="m_EFechaProrroga"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date" ValidationGroup="objeto"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Estado</td>
                                            <td>
                                                <anthem:DropDownList ID="m_CBEstado" runat="server" AutoCallBack="False" OnSelectedIndexChanged="m_CBConvenio_SelectedIndexChanged">
                                                </anthem:DropDownList></td>
                                            <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="m_CBEstado"
                                            ErrorMessage="Por favor seleccione un estado" InitialValue="-1" SetFocusOnError="True" ValidationGroup="objeto"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Observaciones</td>
                                            <td>
                                                <anthem:TextBox ID="m_EObservacionesAdicion" runat="server">
                                                </anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                    <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td><anthem:Button ID="m_BInsertarObjeto" runat="server" Text="Insertar" ValidationGroup="objeto" OnClick="m_BInsertarObjeto_Click" PreCallBackFunction="InsertarModificacion_Precalback"/>
                                                <anthem:Button ID="m_BAceptarObjeto" runat="server" Text="Aceptar" ValidationGroup="objeto" OnClick="m_BAceptarObjeto_Click" PreCallBackFunction="InsertarModificacion_Precalback"/>
                                                <anthem:Button ID="m_BCancelarObjeto" runat="server" Text="Cancelar" CausesValidation="false" OnClick="m_BCancelarObjeto_Click"/>
                                                <anthem:Button ID="m_BEliminarObjeto" runat="server" Text="Eliminar" CausesValidation="False" PreCallBackFunction="Eliminar_PreCallBack" OnClick="m_BEliminarObjeto_Click"/></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="Select"
                                                    TypeName="CAdicionesConvenio"></asp:ObjectDataSource>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Button ID="m_BAnterior1" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:Button ID="Button1" runat="server" Text="Siguiente" OnClick="OnNext"/></td>
                                        </tr>
                                    </table>
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                    &nbsp;<table>
                                        <tr>
                                            <td>
                                    <table>
                                    <tr>
                                        <td>
                                                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="Select"
                                                    TypeName="CDocumentosConvenio"></asp:ObjectDataSource>
                                            </td>
                                        <td>
                                            NIT ente territorial</td>
                                        <td><anthem:TextBox ID="m_ENIT" runat="server" ReadOnly="True">
                                        </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            <anthem:DataGrid ID="m_LBDocumentos" Runat="server" AutoGenerateColumns="false" DataSourceID="ObjectDataSource3" OnItemDataBound="m_LBDocumentos_ItemDataBound">
                                                <HeaderStyle Font-Bold="True" />
		                                    </anthem:DataGrid></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Button ID="m_BAnterior2" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:Button ID="Button2" runat="server" Text="Siguiente" OnClick="OnNext"/></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                </asp:View>
                                <asp:View ID="View4" runat="server"><table>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Valor<anthem:HiddenField ID="m_nIdDesembolso" runat="server" /></td>
                                        <td>
                                            <anthem:TextBox ID="m_EValorDesembolso" runat="server" CssClass="inputnumerico">
                                            </anthem:TextBox>
                                            </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="m_EValorDesembolso"
                                                ErrorMessage="Por favor ingrese un valor" SetFocusOnError="True" ValidationGroup="desembolso"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator7" ControlToValidate="m_EValorDesembolso" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha desembolso</td>
                                        <td>
                                            <anthem:TextBox ID="m_EFechaDesembolso" runat="server">
                                            </anthem:TextBox><input id="Button8" name="botonfecha"  onclick="setFecha('m_EFechaDesembolso')" type="button" value="..." /></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="m_EFechaDesembolso"
                                                ErrorMessage="Por favor ingrese una fecha de desembolso" SetFocusOnError="True"
                                                ValidationGroup="desembolso"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator8" runat="server" ControlToValidate="m_EFechaDesembolso"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Número de comprobante</td>
                                        <td>
                                            <anthem:TextBox ID="m_EComprobanteDesembolso" runat="server">
                                            </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha de comprobante</td>
                                        <td>
                                            <anthem:TextBox ID="m_EFechaComprobanteDesembolso" runat="server">
                                            </anthem:TextBox><input id="Button9" name="botonfecha"  onclick="setFecha('m_EFechaComprobanteDesembolso')" type="button" value="..." /></td>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator9" runat="server" ControlToValidate="m_EFechaComprobanteDesembolso"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Banco</td>
                                        <td>
                                            <anthem:Label id="m_SBancoDesembolso" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Cuenta bancaria</td>
                                        <td>
                                            <anthem:Label id="m_SCuentaDesembolso" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Tipo de cuenta</td>
                                        <td>
                                            <anthem:Label id="m_STipoCuentaDesembolso" runat="server" UpdateAfterCallBack="False">
                                            </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Observaciones</td>
                                        <td>
                                            <anthem:TextBox ID="m_EObservacionesDesembolso" runat="server">
                                            </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td><asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="Select"
                                                    TypeName="CDesembolsosConvenio"></asp:ObjectDataSource>
                                        </td>
                                        <td><anthem:Button ID="m_BInsertarDesembolso" runat="server" Text="Insertar" OnClick="m_BInsertarDesembolso_Click" ValidationGroup="desembolso" /><anthem:Button ID="m_BAceptarDesembolso" runat="server" Text="Aceptar" OnClick="m_BAceptarDesembolso_Click" ValidationGroup="desembolso" /><anthem:Button ID="m_BCancelarDesembolso" runat="server" Text="Cancelar" CausesValidation="false" OnClick="m_BCancelarDesembolso_Click"/><anthem:Button ID="m_BEliminarDesembolso" runat="server" Text="Eliminar" CausesValidation="False" PreCallBackFunction="Eliminar_PreCallBack" OnClick="m_BEliminarDesembolso_Click"/></td>
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
                                    <br />
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                </asp:View>
                                <asp:View ID="View5" runat="server"><table>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Valor<anthem:HiddenField ID="m_nIdRendimiento" runat="server" /></td>
                                        <td>
                                            <anthem:TextBox ID="m_EValorRendimiento" runat="server" CssClass="inputnumerico">
                                            </anthem:TextBox></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="m_EValorRendimiento"
                                                ErrorMessage="Por favor ingrese un valor" SetFocusOnError="True" ValidationGroup="desembolso"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator10" ControlToValidate="m_EValorRendimiento" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="10000000" MinimumValue="1"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Mes reportado</td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBMes" runat="server" AutoCallBack="False">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="m_CBMes"
                                                ErrorMessage="Por favor ingrese seleccione un mes" InitialValue="-1" SetFocusOnError="True"
                                                ValidationGroup="desembolso"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Año reportado</td>
                                        <td>
                                            <anthem:DropDownList ID="m_LCAno" runat="server" AutoCallBack="False" >
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="m_LCAno"
                                                ErrorMessage="Por favor seleccione un año" InitialValue="-1" SetFocusOnError="True"
                                                ValidationGroup="desembolso"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha comprobante</td>
                                        <td>
                                            <anthem:TextBox ID="m_EFechaComprobanteRendimiento" runat="server">
                                            </anthem:TextBox><input id="Button10" name="botonfecha"  onclick="setFecha('m_EFechaComprobanteRendimiento')" type="button" value="..." />
                                            </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="m_EFechaComprobanteRendimiento"
                                                ErrorMessage="Por favor ingrese una fecha" SetFocusOnError="True" ValidationGroup="desembolso"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator11" runat="server" ControlToValidate="m_EFechaComprobanteRendimiento"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Estado</td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBEstadoRendimiento" runat="server" AutoCallBack="False">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="m_CBEstadoRendimiento"
                                                ErrorMessage="Por favor seleccione un estado" InitialValue="-1" SetFocusOnError="True"
                                                ValidationGroup="desembolso"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Observaciones</td>
                                        <td>
                                            <anthem:TextBox ID="m_EObservacionesRendimiento" runat="server">
                                            </anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td><asp:ObjectDataSource ID="ObjectDataSource5" runat="server" SelectMethod="Select"
                                                    TypeName="CRendimientosConvenio"></asp:ObjectDataSource>
                                        </td>
                                        <td><anthem:Button ID="m_BInsertarRendimiento" runat="server" Text="Insertar" ValidationGroup="rendimiento" OnClick="m_BInsertarRendimiento_Click" /><anthem:Button ID="m_BAceptarRendimiento" runat="server" Text="Aceptar" ValidationGroup="rendimiento" OnClick="m_CBAceptarRendimiento_Click" /><anthem:Button ID="m_BCancelarRendimiento" runat="server" Text="Cancelar" CausesValidation="false" OnClick="m_BCancelarRendimiento_Click" /><anthem:Button ID="m_BEliminarRendimiento" runat="server" Text="Eliminar" CausesValidation="False" PreCallBackFunction="Eliminar_PreCallBack" OnClick="m_BEliminarRendimiento_Click" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td></td>
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
                                            <asp:Button ID="m_BAnterior4" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:Button ID="Button4" runat="server" Text="Siguiente" OnClick="OnNext"/></td>
                                    </tr>
                                </table>
                                    <br />
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                </asp:View>
                                <asp:View ID="View6" runat="server"><table>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Informe final de gestión</td>
                                        <td>
                                            <anthem:CheckBox ID="m_CInformeFinal" runat="server" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha Informe</td>
                                        <td>
                                            <anthem:TextBox ID="m_EFechaInforme" runat="server">
                                            </anthem:TextBox><input id="Button11" name="botonfecha"  onclick="setFecha('m_EFechaInforme')" type="button" value="..." /></td>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator12" runat="server" ControlToValidate="m_EFechaInforme"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Documentos completos liquidación de obra</td>
                                        <td>
                                            <anthem:CheckBox ID="m_CDocumentosCompletos" runat="server" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Acta de liquidación</td>
                                        <td>
                                            <anthem:CheckBox ID="m_CActaLiquidacion" runat="server" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha del acta</td>
                                        <td>
                                            <anthem:TextBox ID="m_EFechaActa" runat="server">
                                            </anthem:TextBox><input id="Button12" name="botonfecha"  onclick="setFecha('m_EFechaActa')" type="button" value="..." /></td>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator13" runat="server" ControlToValidate="m_EFechaActa"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="2000/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Observaciones</td>
                                        <td>
                                            <anthem:TextBox ID="m_EObservacionesLiquidacion" runat="server">
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
                                            <asp:Button ID="m_BAnterior5" runat="server" Text="Anterior" OnClick="OnPrev" CausesValidation="false" /><asp:Button
                                                ID="m_BFinalizarE" runat="server" OnClick="m_BAceptar_Click" Text="Finalizar"
                                                ValidationGroup="tab" />
                                        </td>
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
                                                <anthem:GridView ID="m_LCAdiciones" runat="server" EmptyDataText="No hay Adiciones" UpdateAfterCallBack="False" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" DataKeyNames="id" DataSourceID="ObjectDataSource2" OnSelectedIndexChanged="m_LCAdiciones_SelectedIndexChanged" OnRowDataBound="m_LCAdiciones_RowDataBound">
                                        <columns>
<asp:CommandField SelectImageUrl="../../imagenes/edit.jpg" ButtonType="Image" ShowSelectButton="True"></asp:CommandField>
</columns>
                                        <RowStyle CssClass="celda" BackColor="White" Wrap="True" />
                                        <SelectedRowStyle BackColor="#316AC5" CssClass="celdasel" />
                                        <PagerStyle HorizontalAlign="Right" BackColor="#E3E4EA" />
                                        <HeaderStyle CssClass="celdaTitulo" BackColor="#E3E4EA" />
                                    </anthem:GridView>
                                            <anthem:GridView ID="m_LCRendimientos" runat="server" EmptyDataText="No hay Rendimientos" UpdateAfterCallBack="False" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" DataKeyNames="id" DataSourceID="ObjectDataSource5" OnRowDataBound="m_LCRendimientos_RowDataBound" OnSelectedIndexChanged="m_LCRendimientos_SelectedIndexChanged" >
                                            <columns>
<asp:CommandField SelectImageUrl="../../imagenes/edit.jpg" ButtonType="Image" ShowSelectButton="True"></asp:CommandField>
</columns>
                                            <RowStyle CssClass="celda" BackColor="White" Wrap="True" />
                                            <SelectedRowStyle BackColor="#316AC5" CssClass="celdasel" />
                                            <PagerStyle HorizontalAlign="Right" BackColor="#E3E4EA" />
                                            <HeaderStyle CssClass="celdaTitulo" BackColor="#E3E4EA" />
                                        </anthem:GridView>
                                            <anthem:GridView ID="m_LCDesembolsos" runat="server" EmptyDataText="No hay Desembolsos" UpdateAfterCallBack="False" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" DataKeyNames="id" DataSourceID="ObjectDataSource4" OnRowDataBound="m_LCDesembolsos_RowDataBound" OnSelectedIndexChanged="m_LCDesembolsos_SelectedIndexChanged" >
                                                <columns>
<asp:CommandField SelectImageUrl="../../imagenes/edit.jpg" ButtonType="Image" ShowSelectButton="True"></asp:CommandField>
</columns>
                                                <RowStyle CssClass="celda" BackColor="White" Wrap="True" />
                                                <SelectedRowStyle BackColor="#316AC5" CssClass="celdasel" />
                                                <PagerStyle HorizontalAlign="Right" BackColor="#E3E4EA" />
                                                <HeaderStyle CssClass="celdaTitulo" BackColor="#E3E4EA" />
                                            </anthem:GridView>
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