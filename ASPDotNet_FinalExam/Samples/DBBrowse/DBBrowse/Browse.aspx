<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Browse.aspx.cs" Inherits="DBBrowse.Browse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    Welcome to Database Browse Program 
    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" EmptyDataText="There are no data records to display.">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="BookTitle" HeaderText="BookTitle" SortExpression="BookTitle" />
                <asp:BoundField DataField="Genre" HeaderText="Genre" SortExpression="Genre" />
                <asp:BoundField DataField="price" HeaderText="price" SortExpression="price" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:booksConnectionString1 %>" DeleteCommand="DELETE FROM [Titles] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Titles] ([Id], [BookTitle], [Genre], [price]) VALUES (@Id, @BookTitle, @Genre, @price)" ProviderName="<%$ ConnectionStrings:booksConnectionString1.ProviderName %>" SelectCommand="SELECT [Id], [BookTitle], [Genre], [price] FROM [Titles]" UpdateCommand="UPDATE [Titles] SET [BookTitle] = @BookTitle, [Genre] = @Genre, [price] = @price WHERE [Id] = @Id">
            <DeleteParameters>
                <asp:Parameter Name="Id" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Id" Type="Int32" />
                <asp:Parameter Name="BookTitle" Type="String" />
                <asp:Parameter Name="Genre" Type="String" />
                <asp:Parameter Name="price" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="BookTitle" Type="String" />
                <asp:Parameter Name="Genre" Type="String" />
                <asp:Parameter Name="price" Type="String" />
                <asp:Parameter Name="Id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
