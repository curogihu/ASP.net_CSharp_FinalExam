<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuItems.aspx.cs" Inherits="ASPDotNet_FinalExam.MenuItems" %>

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
              
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataKeyNames="ManuID" DataSourceID="SqlDataSource2">
            <Columns>
                <asp:BoundField DataField="ManuID" HeaderText="ManuID" ReadOnly="True" SortExpression="ManuID" />
                <asp:BoundField DataField="ParentID" HeaderText="ParentID" SortExpression="ParentID" />
                <asp:BoundField DataField="MenuDescription" HeaderText="MenuDescription" SortExpression="MenuDescription" />
                <asp:BoundField DataField="TargetPage" HeaderText="TargetPage" SortExpression="TargetPage" />
                <asp:BoundField DataField="LanguageID" HeaderText="LanguageID" SortExpression="LanguageID" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [MenuItems]"></asp:SqlDataSource>
        
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="ManuID" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label3" runat="server" Text="ParentID" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label7" runat="server" Text="MenuDescription" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label8" runat="server" Text="TargetPage" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label9" runat="server" Text="LanguageID" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Add" Width="70px" OnClick="Button1_Click" />

        <br />
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="ManuID" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox6" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label5" runat="server" Text="ParentID" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox7" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label6" runat="server" Text="MenuDescription" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox8" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label10" runat="server" Text="TargetPage" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox9" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label11" runat="server" Text="LanguageID" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Modify" Width="70px" OnClick="Button2_Click" />

        <br />
        <br />
        <br />
        <asp:Label ID="Label12" runat="server" Text="ManuID" Width="150px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Text="Delete" Width="70px" OnClick="Button3_Click" />




    </form>
</body>
</html>
