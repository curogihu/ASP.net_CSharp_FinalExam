<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LanguageControl.aspx.cs" Inherits="ASPDotNet_FinalExam.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>

        <asp:Label ID="Label1" runat="server" Text="Current Data"></asp:Label>
        
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="LanguageID" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="LanguageID" HeaderText="LanguageID" ReadOnly="True" SortExpression="LanguageID" />
                <asp:BoundField DataField="LanguageDescription" HeaderText="LanguageDescription" SortExpression="LanguageDescription" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Languages]"></asp:SqlDataSource>
        <br />
        <br />
        
        <asp:Label ID="Label2" runat="server" Text="LanguageID" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label3" runat="server" Text="LanguageDescription" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
        <asp:Button ID="Button2" runat="server" Text="Add" Width="70px" OnClick="Button1_Click" />

        <br />
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="LanguageID" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label5" runat="server" Text="LanguageDescription" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox><br />
        <asp:Button ID="Button3" runat="server" Text="Modify" Width="70px" OnClick="Button2_Click" />

                <br />
        <br />
        <br />
        <asp:Label ID="Label6" runat="server" Text="LanguageID" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox><br />
        <asp:Button ID="Button4" runat="server" Text="Delete" Width="70px" OnClick="Button3_Click" />

    </form>
</body>
</html>
