<%@ Register TagPrefix="anthem" Namespace="Anthem" Assembly="Anthem" %>
<anthem:Timer ID="timerapp" runat="server" Enabled="true" Interval="60000" OnTick="timer_Tick" />
<table width="100%"  border="0" cellpadding="0" cellspacing="0" background="../../imagenes/fondo.jpg">
  <tr>
    <td><table width="100%"  border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td><div align="center" class="ESistema">SISTEMA SEGUIMIENTO A PROYECTOS</div></td>
      </tr>
      <tr>
        <td><div align="center" class="EFinanciado">Programa de apoyo a proyectos educativos</div></td>
      </tr>
      <tr>
        <td><table width="100%"  border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td><div align="left">
                  <asp:Label CssClass="ETitulos" ID="m_STituloLargo" runat="server" Text=""></asp:Label></div></td>
            <td><div align="center"><anthem:Label CssClass="ETitulos" ID="m_SFechaActual" runat="server" Text=""></anthem:Label></div></td>
            <td><div align="right"><asp:Label ID="m_SUsuarioActual" CssClass="ETitulos" runat="server" Text=""></asp:Label></div></td>
          </tr>
        </table></td>
      </tr>
    </table></td>
    <td><div align="center" class="Estilo1">Portal de proyectos educativos</div></td>
  </tr>
</table>
<div id="boxHandle" class="box">
	<div id="handle" class="handle">Ayuda<span class="spantitle"><img src="../../imagenes/close.gif" border="0" title="Close" align="right" WIDTH="11" HEIGHT="11" onclick="hideMe(true)">&nbsp;</b></span></div>
	<iframe id="myhelpframe" src="" width="100%" height="100%" marginwidth="0" marginheight="0" hspace="0" vspace="0" frameborder="0" scrolling="auto" bordercolor="#000000"></iframe>
</div>
<script runat="server">
    virtual public void timer_Tick(object sender, EventArgs e)
    {
        m_SFechaActual.Text = CPageTemplate.GetFechaHora(DateTime.Now);
        m_SFechaActual.UpdateAfterCallBack = true;
    }
</script>

