<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ShowMenu.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <asp:Label ID="lblLang" runat="server" Text="Language" 
                        style="font-family: Constantia; font-size: small"></asp:Label>&nbsp;&nbsp;
       <asp:DropDownList ID="ddlLang" runat="server" AutoPostBack="True" >
       </asp:DropDownList>
    </div>
    <div id ="cssmenu">
    
        <asp:Literal ID="ltMenus" runat="server"></asp:Literal>
    
    </div>
    <div>
    
    </div>
    </form>
</body>
</html>
