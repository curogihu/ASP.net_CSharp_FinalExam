<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Calc.aspx.cs" Inherits="FirstASPdotNet.Calc" %>

<!DOCTYPE html>
<link href="main.css" rel="stylesheet" />
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>My Fisrt asp.net example</title>
</head>
<body>
    <div id="top">
    </div>
    <form id="form1" runat="server">
    <div>
        Welcome to ASP.net!!!
        <br />
        <br />
        <br />
        Fisrt Number :&nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <br />
        Second Number :&nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Add" OnClick="Button1_Click" />
&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Sub" OnClick="Button2_Click" />
&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Text="Mul" OnClick="Button3_Click" />
&nbsp;
        <asp:Button ID="Button4" runat="server" Text="Div" OnClick="Button4_Click" />
        <br />
        <br />
        Result:&nbsp;
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
    </div>
    </form>
</body>
</html>
