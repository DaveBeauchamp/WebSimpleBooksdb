<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BooksWebApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Books db</title>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <style>
        #btnFirst, #btnLast, #btnPrev, #btnNext, #btnBookSave, #btnBookNew, #btnBookFirst, #btnBookLast, #btnBookPrev, #btnBookNext, 
#btnAuthorSave, #btnAuthorNew, #btnAuthorFirst, #btnAuthorLast, #btnAuthorPrevious, #btnAuthorNext {
            width: 100px;
        }
        #btnClear, #btnRefresh {
            width: 200px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-sm-4">
                <h3>Books</h3>
                <br />
                <asp:Label runat="server" Text="Book ID: "/><asp:Label runat="server" ID="lblBookId" Text="0"/> <br />
                <asp:Label runat="server" Text="Book Title: " /><asp:Label runat="server" ID="lblBookTitle" /> <br />
                <asp:Label runat="server" Text="Book Genre: " /><asp:Label runat="server" ID="lblBookGenre" /> <br />
                <asp:Label runat="server" Text="Author: " /><asp:Label runat="server" ID="lblBookAuthor" /> <br />

                <h3>Navigation</h3>
                <table>
                    <tr>
                        <td><asp:Button runat="server" ID="btnFirst" Text="First" OnClick="btnFirst_Click"/><asp:Button runat="server" ID="btnLast" Text="Last" OnClick="btnLast_Click"/></td>
                    </tr>
                    <tr>
                        <td><asp:Button runat="server" ID="btnPrev" Text="Previous" OnClick="btnPrev_Click"/><asp:Button runat="server" ID="btnNext" Text="Next" OnClick="btnNext_Click" /></td>
                    </tr>
                    <tr>
                        <td><asp:Button runat="server" ID="btnClear" Text="Clear Book" OnClick="btnClear_Click" /></td>
                    </tr>
                    <tr>
                        <td><asp:Button runat="server" ID="btnRefresh" Text="Refresh Table" OnClick="btnRefresh_Click" /></td>
                    </tr>
                </table>
            </div>
            <div class="col-sm-8">
                <br />
                <asp:GridView runat="server" id="gvBookList" OnSelectedIndexChanged="gvBookList_SelectedIndexChanged">
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-6">
                <h3>Add & Edit Books</h3>
                <table>
                    <tr>
                        <td><asp:Label runat="server" Text="Book ID: " /></td>
                        <td><asp:Label runat="server" ID="lblBookEditID" Text="0"/></td>
                    </tr>
                    <tr>
                        <td><asp:Label runat="server" Text="Book Title: " /></td>
                        <td><asp:TextBox runat="server" ID="txtBookTitle" /></td>
                    </tr>
                    <tr>
                        <td><asp:Label runat="server" Text="Book Genre: " /></td>
                        <td><asp:TextBox runat="server" ID="txtBookGenre" /></td>
                    </tr>
                    <tr>
                        <td><asp:Label runat="server" Text="Author: " /></td>
                        <td><asp:DropDownList runat="server" ID="ddlAuthor" EnableViewState="true" /></td>
                    </tr>
                    <tr>
                        <td><asp:Button runat="server" ID="btnBookSave" Text="Save Book" OnClick="btnBookSave_Click" /><asp:Button runat="server" ID="btnBookNew" Text="New Book" OnClick="btnBookNew_Click" /></td>
                    </tr>
                    <tr>
                        <td><asp:Button runat="server" ID="btnBookFirst" Text="First Book" OnClick="btnBookFirst_Click" /><asp:Button runat="server" ID="btnBookLast" Text="Last Book" OnClick="btnBookLast_Click" /></td>
                    </tr>
                    <tr>
                        <td><asp:Button runat="server" ID="btnBookPrev" Text="Prev Book" OnClick="btnBookPrev_Click" /><asp:Button runat="server" ID="btnBookNext" Text="Next Book" OnClick="btnBookNext_Click" /></td>
                    </tr>
                </table>
            </div>
            <div class="col-sm-6">
                <h3>Add & Edit Authors</h3>
                <table>
                    <tr>
                        <td><asp:Label runat="server" Text="Author ID: " /></td>
                        <td><asp:Label runat="server" ID="lblAuthorID" Text="0"/></td>
                    </tr>
                    <tr>
                        <td><asp:Label runat="server" Text="Author Name: " /></td>
                        <td><asp:TextBox runat="server" ID="txtAuthorName" /></td>
                    </tr>
                    <tr>
                        <td><asp:Button runat="server" ID="btnAuthorSave" Text="Save Author" OnClick="btnAuthorSave_Click" /><asp:Button runat="server" ID="btnAuthorNew" Text="New Author" OnClick="btnAuthorNew_Click" /></td>
                    </tr>
                    <tr>
                        <td><asp:Button runat="server" ID="btnAuthorFirst" Text="First Author" OnClick="btnAuthorFirst_Click" /><asp:Button runat="server" ID="btnAuthorLast" Text="Last Author" OnClick="btnAuthorLast_Click" /></td>
                    </tr>
                    <tr>
                        <td><asp:Button runat="server" ID="btnAuthorPrevious" Text="Prev Author" OnClick="btnAuthorPrevious_Click" /><asp:Button runat="server" ID="btnAuthorNext" Text="Next Author" OnClick="btnAuthorNext_Click" /></td>
                    </tr>
                </table>
                <br />
                <table>
                    <tr>
                        <td><asp:Label runat="server" Text="Add/Edit Status: " /></td>
                        <td><asp:Label runat="server" ID="lblAddEditStatus" /></td>
                    </tr>
                </table>

            </div>
        </div>
    </form>
</body>
</html>
