using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BooksWebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // The GetAllBooksWithAuthors method is used as data source for the gridView
            // on form load
            gvBookList.DataSource = webService.GetAllBooksWithAuthors();
            gvBookList.DataBind();

            // The GetAllAuthors method is used as data source for the dropDownList
            // on form load
            var listOfAuth = webService.GetAllAuthors();
            ddlAuthor.DataSource = listOfAuth;
            ddlAuthor.DataTextField = "authorName";
            ddlAuthor.DataValueField = "authorId";
            ddlAuthor.DataBind();

        }
        
        #region Navigation
        protected void btnFirst_Click(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // look in the Web Service for more information on the methods used
            try
            {
                var navData = webService.getFirstBookAndAuthor();
                lblBookId.Text = navData.BookId.ToString();
                lblBookTitle.Text = navData.BookTitle.ToString();
                lblBookGenre.Text = navData.BookGenre.ToString();
                lblBookAuthor.Text = navData.AuthorName.ToString();
                lblAddEditStatus.Text = "First Book";
            }
            catch
            {
                lblAddEditStatus.Text = "Could not find first Book";
            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // look in the Web Service for more information on the methods used
            try
            {
                var navData = webService.getLastBookAndAuthor();
                lblBookId.Text = navData.BookId.ToString();
                lblBookTitle.Text = navData.BookTitle.ToString();
                lblBookGenre.Text = navData.BookGenre.ToString();
                lblBookAuthor.Text = navData.AuthorName.ToString();
                lblAddEditStatus.Text = "Last Book";
            }
            catch
            {
                lblAddEditStatus.Text = "Could not find last Book";
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // here I get the minId value from the first book
            var getMin = webService.getFirstBookAndAuthor();
            // then store it in minId to be used in the getPreviousBookAndAuthor method 
            var minId = getMin.BookId.ToString();

            // look in the Web Service for more information on the methods used
            if (long.Parse(lblBookId.Text) <= long.Parse(minId))
            {
                lblAddEditStatus.Text = "This is the first Book";
            }
            else
            {
                var navData = webService.getPreviousBookAndAuthor(lblBookId.Text, minId);
                lblBookId.Text = navData.BookId.ToString();
                lblBookTitle.Text = navData.BookTitle.ToString();
                lblBookGenre.Text = navData.BookGenre.ToString();
                lblBookAuthor.Text = navData.AuthorName.ToString();
                lblAddEditStatus.Text = "Previous Book";
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // here I get the maxId value from the last book
            var getMax = webService.getLastBookAndAuthor();
            // then store it in maxId to be used in the getNextBookAndAuthor method 
            var maxId = getMax.BookId.ToString();

            // look in the Web Service for more information on the methods used
            if (long.Parse(lblBookId.Text) >= long.Parse(maxId))
            {
                lblAddEditStatus.Text = "This is the last Book";
            }
            else
            {
                var navData = webService.getNextBookAndAuthor(lblBookId.Text, maxId);
                lblBookId.Text = navData.BookId.ToString();
                lblBookTitle.Text = navData.BookTitle.ToString();
                lblBookGenre.Text = navData.BookGenre.ToString();
                lblBookAuthor.Text = navData.AuthorName.ToString();
                lblAddEditStatus.Text = "Next Book";
            }
        }
        
        protected void gvBookList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // gets the  
            var rowId = gvBookList.SelectedRow.Cells[4].Text; // test this 
            var book = webService.GetOneBookWithAuthor(rowId);

            lblBookId.Text = book.BookId.ToString();
            lblBookTitle.Text = book.BookTitle.ToString();
            lblBookGenre.Text = book.BookGenre.ToString();
            lblBookAuthor.Text = book.AuthorName.ToString();

        }

        // clears the fields to basic values
        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblBookId.Text = "0";
            lblBookTitle.Text = string.Empty;
            lblBookGenre.Text = string.Empty;
            lblBookAuthor.Text = string.Empty;
        }

        // refreshes the gridView, If new records have been added they will be displayed
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();
            gvBookList.DataSource = webService.GetAllBooksWithAuthors();
            gvBookList.DataBind();
        }
        #endregion

        #region BookAddEditButtons
        protected void btnBookSave_Click(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // if label value is "0" insert new author if not "0" update the author 
            // look in the Web Service for more information on the methods used
            if (lblBookEditID.Text == "0")
            {
                try
                {
                    webService.InsertBook(txtBookTitle.Text, txtBookGenre.Text, ddlAuthor.SelectedValue.ToString());
                    lblAddEditStatus.Text = "Successfully added an Book";
                }
                catch
                {
                    lblAddEditStatus.Text = "Something went wrong adding an Book";
                }
            }
            else
            {
                try
                {
                    webService.updateBooks(txtBookTitle.Text, txtBookGenre.Text, ddlAuthor.SelectedValue.ToString(), lblBookEditID.Text);
                    lblAddEditStatus.Text = "Successfully updated an Book";
                }
                catch
                {
                    lblAddEditStatus.Text = "Something went wrong updating an Book";
                }
            }
        }

        // clears the fields to basic values
        protected void btnBookNew_Click(object sender, EventArgs e)
        {
            lblBookEditID.Text = "0";
            txtBookTitle.Text = string.Empty;
            txtBookGenre.Text = string.Empty;
            ddlAuthor.SelectedValue = "1"; // not sure if this will break the DropDownList
        }
        #endregion

        #region BookNavigationButtons
        protected void btnBookFirst_Click(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // look in the Web Service for more information on the methods used
            try
            {
                var bookData = webService.getFirstBookAndAuthor();
                lblBookEditID.Text = bookData.BookId.ToString();
                txtBookTitle.Text = bookData.BookTitle.ToString();
                txtBookGenre.Text = bookData.BookGenre.ToString();
                ddlAuthor.SelectedItem.Text = bookData.AuthorName.ToString();

                lblAddEditStatus.Text = "First Book";
            }
            catch
            {
                lblAddEditStatus.Text = "Could not find first Book";
            }
        }

        protected void btnBookLast_Click(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // look in the Web Service for more information on the methods used
            try
            {
                var bookData = webService.getLastBookAndAuthor();
                lblBookEditID.Text = bookData.BookId.ToString();
                txtBookTitle.Text = bookData.BookTitle.ToString();
                txtBookGenre.Text = bookData.BookGenre.ToString();
                ddlAuthor.SelectedItem.Text = bookData.AuthorName.ToString();

                lblAddEditStatus.Text = "Last Book";
            }
            catch
            {
                lblAddEditStatus.Text = "Could not find last Book";
            }
        }

        protected void btnBookPrev_Click(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // here I get the minId value from the first book
            var getMin = webService.getFirstBookAndAuthor();
            // then store it in minId to be used in the getPreviousBookAndAuthor method 
            var minId = getMin.BookId.ToString();

            // look in the Web Service for more information on the methods used
            if (long.Parse(lblBookEditID.Text) <= long.Parse(minId))
            {
                lblAddEditStatus.Text = "This is the first Book";
            }
            else
            {
                var bookData = webService.getPreviousBookAndAuthor(lblBookEditID.Text, minId);
                lblBookEditID.Text = bookData.BookId.ToString();
                txtBookTitle.Text = bookData.BookTitle.ToString();
                txtBookGenre.Text = bookData.BookGenre.ToString();
                ddlAuthor.SelectedItem.Text = bookData.AuthorName.ToString();
                lblAddEditStatus.Text = "Previous Book";
            }
        }

        protected void btnBookNext_Click(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // here I get the maxId value from the last book
            var getMax = webService.getLastBookAndAuthor();
            // then store it in maxId to be used in the getNextBookAndAuthor method 
            var maxId = getMax.BookId.ToString();

            // look in the Web Service for more information on the methods used
            if (long.Parse(lblBookEditID.Text) >= long.Parse(maxId))
            {
                lblAddEditStatus.Text = "This is the last Book";
            }
            else
            {
                var bookData = webService.getNextBookAndAuthor(lblBookEditID.Text, maxId);
                lblBookEditID.Text = bookData.BookId.ToString();
                txtBookTitle.Text = bookData.BookTitle.ToString();
                txtBookGenre.Text = bookData.BookGenre.ToString();
                ddlAuthor.SelectedItem.Text = bookData.AuthorName.ToString();
                lblAddEditStatus.Text = "Next Book";
            }
        }
        #endregion

        #region AuthorAddEditButtons
        protected void btnAuthorSave_Click(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // if label value is "0" insert new author if not "0" update the author 
            // look in the Web Service for more information on the methods used
            if (lblAuthorID.Text == "0")
            {
                try
                {
                    webService.insertAuthor(txtAuthorName.Text);
                    lblAddEditStatus.Text = "Successfully added an Author";
                }
                catch
                {
                    lblAddEditStatus.Text = "Something went wrong adding an Author";
                }
            }
            else
            {
                try
                {
                    webService.updateAuthor(lblAuthorID.Text, txtAuthorName.Text);
                    lblAddEditStatus.Text = "Successfully updated an Author";
                }
                catch
                {
                    lblAddEditStatus.Text = "Something went wrong updating an Author";
                }
            }
        }

        // clears the fields to basic values
        protected void btnAuthorNew_Click(object sender, EventArgs e)
        {
            lblAuthorID.Text = "0";
            txtAuthorName.Text = string.Empty;
        }
        #endregion

        #region AuthorNavigationButtons
        protected void btnAuthorFirst_Click(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // look in the Web Service for more information on the methods used
            try
            {
                var authorData = webService.getFirstAuthor();
                lblAuthorID.Text = authorData.AuthorId.ToString();
                txtAuthorName.Text = authorData.AuthorName.ToString();
                lblAddEditStatus.Text = "First Author";
            }
            catch
            {
                lblAddEditStatus.Text = "Could not find first Author";
            }
        }

        protected void btnAuthorLast_Click(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // look in the Web Service for more information on the methods used
            try
            {
                var authorData = webService.getLastAuthor();
                lblAuthorID.Text = authorData.AuthorId.ToString();
                txtAuthorName.Text = authorData.AuthorName.ToString();
                lblAddEditStatus.Text = "Last Author";
            }
            catch
            {
                lblAddEditStatus.Text = "Could not find last Author";
            }
        }

        protected void btnAuthorPrevious_Click(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // here I get the minId value from the first author
            var getMin = webService.getFirstAuthor();
            // then store it in minId to be used in the getPreviousAuthor method 
            var minId = getMin.AuthorId.ToString();

            // look in the Web Service for more information on the methods used
            var authorData = webService.getPreviousAuthor(lblAuthorID.Text, minId);

            if (long.Parse(lblAuthorID.Text) <= long.Parse(minId))
            {
                lblAddEditStatus.Text = "This is the first Author";
            }
            else
            {
                lblAuthorID.Text = authorData.AuthorId.ToString();
                txtAuthorName.Text = authorData.AuthorName.ToString();
                lblAddEditStatus.Text = "Previous Author";
            }
        }

        protected void btnAuthorNext_Click(object sender, EventArgs e)
        {
            // Web Service methods are available webService.methodName() from this instance
            var webService = new BooksWebServiceProxy.BooksWebServiceSoapClient();

            // here I get the maxId value from the last author
            var getMax = webService.getLastAuthor();
            // then store it in minId to be used in the getNextAuthor method 
            var maxId = getMax.AuthorId.ToString();

            // look in the Web Service for more information on the methods used
            if (long.Parse(lblAuthorID.Text) >= long.Parse(maxId))
            {
                lblAddEditStatus.Text = "This is the last Author";
            }
            else
            {
                var authorData = webService.getNextAuthor(lblAuthorID.Text, maxId);
                lblAuthorID.Text = authorData.AuthorId.ToString();
                txtAuthorName.Text = authorData.AuthorName.ToString();
                lblAddEditStatus.Text = "Next Author";
            }  
        }
        #endregion
    }
}