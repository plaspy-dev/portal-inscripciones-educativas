<%@ Page Language="C#" AutoEventWireup="true" CodeFile="control.aspx.cs" Inherits="seguimiento_convenios_control"  EnableEventValidation="false"%>

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
		<!--#include file="../../imagenes/LogoEntidad.aspx" -->
		</td>
	</tr>
	<tr>
		<td>
		<table width="100%" border="1">
		<tr valign="top">
			<td id="leftmenu"  width="150" height="280" align="center" valign="top">
			    <span  id="lefttitulo" class="Titulos">VIGENCIA <asp:DropDownList ID="m_CBVigenciasCombo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="m_CBVigenciasCombo_SelectedIndexChanged">
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
                                <table cellspacing="0">
                                    <tr>
                                        <td>
                                            Entidad Territorial<anthem:HiddenField ID="m_nId" runat="server" />
                                        </td>
                                        <td>
                                            <anthem:DropDownList ID="m_CBEnte" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBEnte_SelectedIndexChanged">
                                            </anthem:DropDownList></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="m_CBEnte" Display="Dynamic"
                                            ErrorMessage="Por favor seleccione una entidad" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
						            <tr>
                                        <td >
                                            Número de Convenio</td>
                                        <td ><anthem:DropDownList ID="m_CBConvenio" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBConvenio_SelectedIndexChanged">
                                        </anthem:DropDownList></td>
                                        <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="m_CBConvenio" Display="Dynamic"
                                            ErrorMessage="Por favor seleccione un convenio" InitialValue="-1" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Valor</td>
                                        <td >
                                            <anthem:Label ID="m_SValor" runat="server"></anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            Estado</td>
                                        <td ><anthem:DropDownList ID="m_CBEstadoConvenio" runat="server" AutoCallBack="True" OnSelectedIndexChanged="m_CBEstadoConvenio_SelectedIndexChanged" >
                                        </anthem:DropDownList></td>
                                        <td>
                                        </td>
                                    </tr>
                                                <tr>
                                                    <td >
                                                        Observaciones</td>
                                                    <td ><anthem:TextBox ID="m_EObservaciones" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                                    <td>
                                                    </td>
                                                </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="m_BSiguiente" runat="server" ToolTip="Siguiente" OnClick="OnNext"/></td>
                                    </tr>
                                            </table>
                                            <br />
                                            <anthem:Label ID="m_SMensaje" runat="server"></anthem:Label><br />
                                            <anthem:ImageButton ID="m_BImprimir" runat="server" ToolTip="Imprimir" ImageUrl="~/imagenes/print.gif" OnClick="m_BImprimir_Click" CausesValidation="False" />
                                            <anthem:DropDownList ID="m_CBImprimir" runat="server">
                                                <asp:ListItem Value="1">Hoja de Cálculo</asp:ListItem>
                                                <asp:ListItem Value="0">En Pantalla</asp:ListItem>
                                            </anthem:DropDownList>
                                            <anthem:ImageButton ID="m_BAceptar" runat="server" ImageUrl="~/imagenes/save.gif" ToolTip="Guardar" OnClick="m_BAceptar_Click" /><anthem:ImageButton ID="m_BCancelar" ToolTip="Cancelar" runat="server" ImageUrl="~/imagenes/stop.gif" OnClick="m_BCancelar_Click" CausesValidation="False" />
                                            <anthem:ImageButton ID="m_BAceptarP" runat="server" ImageUrl="~/imagenes/print.gif" ToolTip="Imprimir" CausesValidation="False" OnClick="m_BAceptarP_Click" /><anthem:ImageButton ID="m_BCancelarP" runat="server" ToolTip="Cancelar" ImageUrl="~/imagenes/stop.gif" CausesValidation="False" OnClick="m_BCancelarP_Click" />
                                            
                                            
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    &nbsp;<table>
                                        <tr>
                                            <td>
                                    <table cellspacing="0">
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
                                            <asp:RangeValidator ID="RangeValidator1" Display="Dynamic" ControlToValidate="m_EAdicionValor" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1" ValidationGroup="objeto"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Fecha adición</td>
                                            <td><anthem:TextBox ID="m_EFechaAdicion" runat="server" CssClass="editfecha"></anthem:TextBox><input id="botonfecha" name="botonfecha"  onclick="setFecha('m_EFechaAdicion')" type="button" value="..." /></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="m_EFechaAdicion" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="1995/01/01"
                                                    SetFocusOnError="True" Type="Date" ValidationGroup="objeto"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Certificado de disponibilidad presupuestal</td>
                                            <td><anthem:TextBox ID="m_ECDPAdicion" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Fecha certificado</td>
                                            <td><anthem:TextBox ID="m_EFechaCDP" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button5" name="botonfecha"  onclick="setFecha('m_EFechaCDP')" type="button" value="..." /></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="m_EFechaCDP" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="1995/01/01"
                                                    SetFocusOnError="True" Type="Date" ValidationGroup="objeto"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Registro presupuestal</td>
                                            <td><anthem:TextBox ID="m_ERDPAdicion" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Fecha registro</td>
                                            <td><anthem:TextBox ID="m_EFechaRDP" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button6" name="botonfecha"  onclick="setFecha('m_EFechaRDP')" type="button" value="..." /></td>
                                            <td>
                                                <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="m_EFechaRDP" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="1995/01/01"
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
                                            <asp:RangeValidator ID="RangeValidator2" Display="Dynamic" ControlToValidate="m_EProrroga" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="integer" MaximumValue="10000000" MinimumValue="1" ValidationGroup="objeto"></asp:RangeValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Fecha prorroga</td>
                                            <td><anthem:TextBox ID="m_EFechaProrroga" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button7" name="botonfecha"  onclick="setFecha('m_EFechaProrroga')" type="button" value="..." /></td>
                                            <td>
                                            <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="m_EFechaProrroga" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="1995/01/01"
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="m_CBEstado" Display="Dynamic"
                                            ErrorMessage="Por favor seleccione un estado" InitialValue="-1" SetFocusOnError="True" ValidationGroup="objeto"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                Observaciones</td>
                                            <td>
                                                <anthem:TextBox ID="m_EObservacionesAdicion" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                            <td>
                                            </td>
                                        </tr>
                                    <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            <anthem:ImageButton ID="m_BInsertarObjeto" runat="server" ImageUrl="~/imagenes/save.gif" ToolTip="Guardar" OnClick="m_BInsertarObjeto_Click" PreCallBackFunction="InsertarModificacion_Precalback" />
                                            <anthem:ImageButton ID="m_BAceptarObjeto" runat="server" ImageUrl="~/imagenes/save.gif" ToolTip="Guardar" OnClick="m_BAceptarObjeto_Click" PreCallBackFunction="InsertarModificacion_Precalback"/>
                                            <anthem:ImageButton ID="m_BCancelarObjeto" ToolTip="Cancelar" runat="server" ImageUrl="~/imagenes/stop.gif" OnClick="m_BCancelarObjeto_Click" CausesValidation="False" />
                                            <anthem:ImageButton ID="m_BEliminarObjeto" runat="server" CausesValidation="False" ImageUrl="~/imagenes/trash.gif" OnClick="m_BEliminarObjeto_Click" PreCallBackFunction="Eliminar_PreCallBack" ToolTip="Eliminar" />
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
                                                <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior1" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="Button1" runat="server" ToolTip="Siguiente" OnClick="OnNext"/></td>
                                        </tr>
                                    </table>
                                </asp:View>
                                <asp:View ID="View3" runat="server">
                                    &nbsp;<table cellspacing="0">
                                        <tr>
                                            <td>
                                    <table cellspacing="0">
                                    <tr>
                                        <td>
                                                <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="Select"
                                                    TypeName="CDocumentosConvenio"></asp:ObjectDataSource>
                                            </td>
                                        <td>
                                            NIT ente territorial</td>
                                        <td><anthem:Label ID="m_SNIT" runat="server">
                                        </anthem:Label></td>
                                        <td>
                                        </td>
                                    </tr>
                                    </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="m_LCDocumentos" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource3"
                                                    OnRowDataBound="m_LCDocumentos_RowDataBound" DataKeyNames="id">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("did") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="id" HeaderText="id" Visible="False"/>
                                                        <asp:BoundField DataField="Documento" HeaderText="Documento"/>
                                                        <asp:TemplateField HeaderText="Fecha">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="m_EFecha" CssClass="editfecha" runat="server" Text='<%# Bind("Fecha") %>'></asp:TextBox><input id="botonfecha" name="botonfecha" type="button" value="..." runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="N&#250;mero">
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
                                            <td align="right"><asp:ImageButton ImageUrl="~/imagenes/save.gif" ID="ImageButton2" runat="server" OnClick="m_BAceptar_Click" ToolTip="Finalizar"/>
                                                <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior2" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="Button2" runat="server" ToolTip="Siguiente" OnClick="OnNext"/></td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div style="text-align: right">
                                        &nbsp;</div>
                                </asp:View>
                                <asp:View ID="View4" runat="server"><table cellspacing="0">
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="m_EValorDesembolso" Display="Dynamic"
                                                ErrorMessage="Por favor ingrese un valor" SetFocusOnError="True" ValidationGroup="desembolso"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator7" Display="Dynamic" ControlToValidate="m_EValorDesembolso" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha desembolso</td>
                                        <td>
                                            <anthem:TextBox ID="m_EFechaDesembolso" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button8" name="botonfecha"  onclick="setFecha('m_EFechaDesembolso')" type="button" value="..." /></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="m_EFechaDesembolso" Display="Dynamic"
                                                ErrorMessage="Por favor ingrese una fecha de desembolso" SetFocusOnError="True"
                                                ValidationGroup="desembolso"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator8" runat="server" ControlToValidate="m_EFechaDesembolso" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="1995/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Número de comprobante</td>
                                        <td>
                                            <anthem:TextBox ID="m_EComprobanteDesembolso" runat="server" CssClass="editcomun"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha de comprobante</td>
                                        <td>
                                            <anthem:TextBox ID="m_EFechaComprobanteDesembolso" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button9" name="botonfecha"  onclick="setFecha('m_EFechaComprobanteDesembolso')" type="button" value="..." /></td>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator9" runat="server" ControlToValidate="m_EFechaComprobanteDesembolso" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="1995/01/01"
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
                                            <anthem:TextBox ID="m_EObservacionesDesembolso" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td><asp:ObjectDataSource ID="ObjectDataSource4" runat="server" SelectMethod="Select"
                                                    TypeName="CDesembolsosConvenio"></asp:ObjectDataSource>
                                        </td>
                                        <td>
                                        <anthem:ImageButton ID="m_BInsertarDesembolso" runat="server" ImageUrl="~/imagenes/save.gif" ToolTip="Guardar" OnClick="m_BInsertarDesembolso_Click" ValidationGroup="desembolso"/>
                                            <anthem:ImageButton ID="m_BAceptarDesembolso" runat="server" ImageUrl="~/imagenes/save.gif" ToolTip="Guardar" OnClick="m_BAceptarDesembolso_Click" ValidationGroup="desembolso"/>
                                            <anthem:ImageButton ID="m_BCancelarDesembolso" ToolTip="Cancelar" runat="server" ImageUrl="~/imagenes/stop.gif" OnClick="m_BCancelarDesembolso_Click" CausesValidation="False" />
                                            <anthem:ImageButton ID="m_BEliminarDesembolso" runat="server" CausesValidation="False" ImageUrl="~/imagenes/trash.gif" OnClick="m_BEliminarDesembolso_Click" PreCallBackFunction="Eliminar_PreCallBack" ToolTip="Eliminar" />
                                        </td>
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
                                            <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior3" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="Button3" runat="server" ToolTip="Siguiente" OnClick="OnNext"/></td>
                                    </tr>
                                </table>
                                    <br />
                                </asp:View>
                                <asp:View ID="View5" runat="server"><table cellspacing="0">
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Valor<anthem:HiddenField ID="m_nIdRendimiento" runat="server" /></td>
                                        <td>
                                            <anthem:TextBox ID="m_EValorRendimiento" runat="server" CssClass="inputnumerico">
                                            </anthem:TextBox></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="m_EValorRendimiento" Display="Dynamic"
                                                ErrorMessage="Por favor ingrese un valor" SetFocusOnError="True" ValidationGroup="desembolso"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator10" Display="Dynamic" ControlToValidate="m_EValorRendimiento" runat="server" ErrorMessage="Por favor ingrese un número aqui" Type="currency" MaximumValue="900000000000000" MinimumValue="1"></asp:RangeValidator>
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="m_CBMes" Display="Dynamic"
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="m_LCAno" Display="Dynamic"
                                                ErrorMessage="Por favor seleccione un año" InitialValue="-1" SetFocusOnError="True"
                                                ValidationGroup="desembolso"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Fecha comprobante</td>
                                        <td>
                                            <anthem:TextBox ID="m_EFechaComprobanteRendimiento" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button10" name="botonfecha"  onclick="setFecha('m_EFechaComprobanteRendimiento')" type="button" value="..." />
                                            </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="m_EFechaComprobanteRendimiento" Display="Dynamic"
                                                ErrorMessage="Por favor ingrese una fecha" SetFocusOnError="True" ValidationGroup="desembolso"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator11" runat="server" ControlToValidate="m_EFechaComprobanteRendimiento" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="1995/01/01"
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="m_CBEstadoRendimiento" Display="Dynamic"
                                                ErrorMessage="Por favor seleccione un estado" InitialValue="-1" SetFocusOnError="True"
                                                ValidationGroup="desembolso"></asp:RequiredFieldValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Observaciones</td>
                                        <td>
                                            <anthem:TextBox ID="m_EObservacionesRendimiento" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td><asp:ObjectDataSource ID="ObjectDataSource5" runat="server" SelectMethod="Select"
                                                    TypeName="CRendimientosConvenio"></asp:ObjectDataSource>
                                        </td>
                                        <td>
                                        <anthem:ImageButton ID="m_BInsertarRendimiento" runat="server" ValidationGroup="rendimiento" ImageUrl="~/imagenes/save.gif" ToolTip="Guardar" OnClick="m_BInsertarRendimiento_Click" />
                                            <anthem:ImageButton ID="m_BAceptarRendimiento" ValidationGroup="rendimiento" runat="server" ImageUrl="~/imagenes/save.gif" ToolTip="Guardar" OnClick="m_CBAceptarRendimiento_Click" />
                                            <anthem:ImageButton ID="m_BCancelarRendimiento" ToolTip="Cancelar" runat="server" ImageUrl="~/imagenes/stop.gif" OnClick="m_BCancelarRendimiento_Click" CausesValidation="False" />
                                            <anthem:ImageButton ID="m_BEliminarRendimiento" runat="server" CausesValidation="False" ImageUrl="~/imagenes/trash.gif" OnClick="m_BEliminarRendimiento_Click" PreCallBackFunction="Eliminar_PreCallBack" ToolTip="Eliminar" />
                                        </td>
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
                                        <td><asp:ImageButton ImageUrl="~/imagenes/save.gif" ID="ImageButton1" runat="server" OnClick="m_BAceptar_Click" ToolTip="Finalizar"/></td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior4" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                        <asp:ImageButton  ImageUrl="~/imagenes/sig.gif" ID="Button4" runat="server" ToolTip="Siguiente" OnClick="OnNext"/></td>
                                    </tr>
                                </table>
                                    <br />
                                </asp:View>
                                <asp:View ID="View6" runat="server"><table cellspacing="0">
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
                                            <anthem:TextBox ID="m_EFechaInforme" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button11" name="botonfecha"  onclick="setFecha('m_EFechaInforme')" type="button" value="..." /></td>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator12" runat="server" ControlToValidate="m_EFechaInforme" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="1995/01/01"
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
                                            <anthem:TextBox ID="m_EFechaActa" runat="server" CssClass="editfecha"></anthem:TextBox><input id="Button12" name="botonfecha"  onclick="setFecha('m_EFechaActa')" type="button" value="..." /></td>
                                        <td>
                                        <asp:RangeValidator ID="RangeValidator13" runat="server" ControlToValidate="m_EFechaActa" Display="Dynamic"
                                                    ErrorMessage="Por favor ingrese una fecha" MaximumValue="2010/01/01" MinimumValue="1995/01/01"
                                                    SetFocusOnError="True" Type="Date"></asp:RangeValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            Observaciones</td>
                                        <td>
                                            <anthem:TextBox ID="m_EObservacionesLiquidacion" runat="server" CssClass="editobs" TextMode="MultiLine"></anthem:TextBox></td>
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
                                            <asp:ImageButton ImageUrl="~/imagenes/ant.gif" ID="m_BAnterior5" runat="server" ToolTip="Anterior" OnClick="OnPrev" CausesValidation="false" />
                                            <asp:ImageButton ImageUrl="~/imagenes/save.gif" ID="m_BFinalizarE" runat="server" OnClick="m_BAceptar_Click" ToolTip="Finalizar"/>
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
                                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Select"
                                                TypeName="CTablaEx"></asp:ObjectDataSource>
                                                <anthem:GridView ID="m_LCAdiciones" runat="server" EmptyDataText="No hay Adiciones" UpdateAfterCallBack="False"  AutoUpdateAfterCallBack="true" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" DataKeyNames="id" DataSourceID="ObjectDataSource2" OnSelectedIndexChanged="m_LCAdiciones_SelectedIndexChanged" OnRowDataBound="m_LCAdiciones_RowDataBound">
                                        <columns>
<asp:CommandField SelectImageUrl="../../imagenes/edit.gif" ButtonType="Image" ShowSelectButton="True"></asp:CommandField>
</columns>
                                        <RowStyle CssClass="celda" BackColor="White" Wrap="True" />
                                        <SelectedRowStyle BackColor="#316AC5" CssClass="celdasel" />
                                        <PagerStyle HorizontalAlign="Right" BackColor="#E3E4EA" />
                                        <HeaderStyle CssClass="celdaTitulo" BackColor="#E3E4EA" />
                                    </anthem:GridView>
                                            <anthem:GridView ID="m_LCRendimientos" runat="server" EmptyDataText="No hay Rendimientos" UpdateAfterCallBack="False"  AutoUpdateAfterCallBack="true" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" DataKeyNames="id" DataSourceID="ObjectDataSource5" OnRowDataBound="m_LCRendimientos_RowDataBound" OnSelectedIndexChanged="m_LCRendimientos_SelectedIndexChanged" >
                                            <columns>
<asp:CommandField SelectImageUrl="../../imagenes/edit.gif" ButtonType="Image" ShowSelectButton="True"></asp:CommandField>
</columns>
                                            <RowStyle CssClass="celda" BackColor="White" Wrap="True" />
                                            <SelectedRowStyle BackColor="#316AC5" CssClass="celdasel" />
                                            <PagerStyle HorizontalAlign="Right" BackColor="#E3E4EA" />
                                            <HeaderStyle CssClass="celdaTitulo" BackColor="#E3E4EA" />
                                        </anthem:GridView>
                                            <anthem:GridView ID="m_LCDesembolsos" runat="server" EmptyDataText="No hay Desembolsos" UpdateAfterCallBack="False"  AutoUpdateAfterCallBack="true" AutoGenerateColumns="False" CellPadding="1" CellSpacing="1" DataKeyNames="id" DataSourceID="ObjectDataSource4" OnRowDataBound="m_LCDesembolsos_RowDataBound" OnSelectedIndexChanged="m_LCDesembolsos_SelectedIndexChanged" >
                                                <columns>
<asp:CommandField SelectImageUrl="../../imagenes/edit.gif" ButtonType="Image" ShowSelectButton="True"></asp:CommandField>
</columns>
                                                <RowStyle CssClass="celda" BackColor="White" Wrap="True" />
                                                <SelectedRowStyle BackColor="#316AC5" CssClass="celdasel" />
                                                <PagerStyle HorizontalAlign="Right" BackColor="#E3E4EA" />
                                                <HeaderStyle CssClass="celdaTitulo" BackColor="#E3E4EA" />
                                            </anthem:GridView>
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
            <asp:Label ID="m_SVersion" runat="server" Font-Bold="True"></asp:Label><br />
            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" Font-Size="Smaller"
                PostBackUrl="~/about.aspx">Desarrollado por SDG.</asp:LinkButton></div>
    </form>
</body>
</html>