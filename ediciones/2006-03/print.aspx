<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print.aspx.cs" Inherits="print" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" OnRowDataBound="GridView1_RowDataBound">
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="SelectPrint"
            TypeName="CTablaEx"></asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
