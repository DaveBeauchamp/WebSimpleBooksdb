using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SQLite;
using Dapper;
using BooksWebService.Model;

namespace BooksWebService
{

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class BooksWebService : System.Web.Services.WebService
    {
        #region GetAllMethods
        /// <summary>
        /// Query to get all of the records in the BooksWithAuthor view
        /// </summary>
        /// <returns>A list containing all of the records in the BooksWithAuthor view</returns>
        [WebMethod]
        public List<Model.Model.BooksWithAuthorName> GetAllBooksWithAuthors()
        {
            using (SQLiteConnection db = Model.Model.Database.GetConnection())
            {
                try
                {
                    string query = "SELECT * FROM BooksWithAuthor";
                    List<Model.Model.BooksWithAuthorName> result = db.Query<Model.Model.BooksWithAuthorName>(query).ToList();
                    return result;
                }
                catch
                {
                    return new List<Model.Model.BooksWithAuthorName>();
                }
            }
        }

        /// <summary>
        /// Query to get all of the records in the authors table
        /// </summary>
        /// <returns>A list containing all of the records in the authors table</returns>
        [WebMethod]
        public List<Model.Model.Author> GetAllAuthors()
        {
            using (var db = Model.Model.Database.GetConnection())
            {
                try
                {
                    string query = "SELECT * FROM authors";
                    var list = db.Query<Model.Model.Author>(query).ToList();
                    return list;
                }
                catch
                {
                    return new List<Model.Model.Author>();
                }
            }
        }

        /// <summary>
        /// Query to get a single record in the BooksWithAuthor view
        /// </summary>
        /// <returns>A single row in the BooksWithAuthor view</returns>
        [WebMethod]
        public Model.Model.BooksWithAuthorName GetOneBookWithAuthor(string bookId)
        {
            using (var db = Model.Model.Database.GetConnection())
            {
                try
                {
                    var query = "SELECT * FROM BooksWithAuthor WHERE bookId = @id";
                    var param = new { id = bookId };
                    var result = db.QuerySingle<Model.Model.BooksWithAuthorName>(query, param);
                    return result;
                }
                catch
                {
                    return new Model.Model.BooksWithAuthorName();
                }
            }
        }
        #endregion

        #region AddEditAuthorsAndBooks
        /// <summary>
        /// Query to Insert records in the authors table
        /// </summary>
        /// <param name="authorName">The value for this param is taken from txtAuthorName from Default.aspx</param>
        [WebMethod]
        public void insertAuthor(string authorName)
        {
            using (var db = Model.Model.Database.GetConnection().OpenAndReturn())
            using (var trans = db.BeginTransaction())
            {
                try
                {
                    string query = "INSERT INTO authors (authorName) VALUES (@an)";
                    var param = new { an = authorName };
                    db.Execute(query, param, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }
        }

        /// <summary>
        /// Query to Update records in the authors table
        /// </summary>
        /// <param name="authorId">The value for this param is taken from lblAuthorID from Default.aspx</param>
        /// <param name="authorName">The value for this param is taken from txtAuthorName from Default.aspx</param>
        [WebMethod]
        public void updateAuthor(string authorId, string authorName)
        {
            using (var db = Model.Model.Database.GetConnection().OpenAndReturn())
            using (var trans = db.BeginTransaction())
            {
                try
                {
                    string query = "UPDATE authors SET authorName = @an WHERE authorId = @id";
                    var param = new { an = authorName, id = authorId };
                    db.Execute(query, param, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }
        }

        /// <summary>
        /// Query to Insert records in the authors table
        /// </summary>
        /// <param name="bookTitle">The value for this param is taken from txtBookTitle from Default.aspx</param>
        /// <param name="bookGenre">The value for this param is taken from txtBookGenre from Default.aspx</param>
        /// <param name="authorDropdown">The value for this param is taken from cboAuthor from Default.aspx</param>
        [WebMethod]
        public void InsertBook(string bookTitle, string bookGenre, string authorDropdown)
        {
            using (var db = Model.Model.Database.GetConnection().OpenAndReturn())
            using (var trans = db.BeginTransaction())
            {
                try
                {
                    string query = "INSERT INTO books (bookTitle, bookGenre, author) VALUES (@bt, @bg, @a)";
                    var param = new { bt = bookTitle, bg = bookGenre, a = authorDropdown };
                    db.Execute(query, param, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }
        }

        /// <summary>
        /// Query to Update records in the authors table
        /// </summary>
        /// <param name="bookTitle">The value for this param is taken from txtBookTitle from Default.aspx</param>
        /// <param name="bookGenre">The value for this param is taken from txtBookGenre from Default.aspx</param>
        /// <param name="authorDropdown">The value for this param is taken from cboAuthor from Default.aspx</param>
        /// <param name="bookId">The value for this param is taken from lblBookEditID from Default.aspx</param>
        [WebMethod]
        public void updateBooks(string bookTitle, string bookGenre, string authorDropdown, string bookId)
        {
            using (var db = Model.Model.Database.GetConnection().OpenAndReturn())
            using (var trans = db.BeginTransaction())
            {
                try
                {
                    string query = "UPDATE books SET bookTitle = @bt, bookGenre = @bg, author = @a WHERE bookId = @bi";
                    var param = new { bt = bookTitle, bg = bookGenre, a = authorDropdown, bi = bookId };
                    db.Execute(query, param, trans);
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }
        }
        #endregion

        #region AuthorNavigationButtons
        /// <summary>
        /// Query to get the first record in the authors table
        /// </summary>
        /// <returns>An object containing a single row in the authors table</returns>
        [WebMethod]
        public Model.Model.Author getFirstAuthor()
        {
            using (var db = Model.Model.Database.GetConnection())
            {
                var query = "SELECT * FROM authors ORDER BY authorId ASC LIMIT 1";
                var result = db.QuerySingle<Model.Model.Author>(query);
                return result;
            }
        }

        /// <summary>
        /// Query to get the last record in the authors table
        /// </summary>
        /// <returns>An object containing a single row in the authors table</returns>
        [WebMethod]
        public Model.Model.Author getLastAuthor()
        {
            using (var db = Model.Model.Database.GetConnection())
            {
                var query = "SELECT * FROM authors ORDER BY authorId DESC LIMIT 1";
                var result = db.QuerySingle<Model.Model.Author>(query);
                return result;
            }
        }

        /// <summary>
        /// Query to get the next record in the authors table, will return the last record if
        /// the next record is greater than last
        /// </summary>
        /// <param name="authorId">The value for this param is taken from lblAuthorID from Default.aspx</param>
        /// <returns>An object containing a single row in the authors table</returns>
        [WebMethod]
        public Model.Model.Author getNextAuthor(string authorId, string maxAuthorId)
        {
            using (var db = Model.Model.Database.GetConnection())
            {
                if (long.Parse(authorId) > long.Parse(maxAuthorId))
                {
                    return getLastAuthor();
                }
                else
                {
                    var query = "SELECT * FROM authors WHERE authorId > @id LIMIT 1";
                    var param = new { id = authorId };
                    var result = db.QuerySingle<Model.Model.Author>(query, param);
                    return result;
                }
            }
        }

        /// <summary>
        /// Query to get the previous record in the authors table, will return the first record if
        /// the next record is less than first
        /// </summary>
        /// <param name="authorId">The value for this param is taken from lblAuthorID from Default.aspx</param>
        /// <returns>An object containing a single row in the authors table</returns>
        [WebMethod]
        public Model.Model.Author getPreviousAuthor(string authorId, string minAuthorId)
        {
            using (var db = Model.Model.Database.GetConnection())
            {
                if (long.Parse(authorId) <= long.Parse(minAuthorId))
                {
                    return getFirstAuthor();
                }
                else
                {
                    var query = "SELECT * FROM authors WHERE authorId < @id ORDER BY authorId DESC LIMIT 1";
                    var param = new { id = authorId };
                    var result = db.QuerySingle<Model.Model.Author>(query, param);
                    return result;
                }
            }
        }
        #endregion

        #region AllBooksNavigation
        /// <summary>
        /// Query to get the first record in the BooksWithAuthor view
        /// </summary>
        /// <returns>An object containing a single row in the BooksWithAuthor view</returns>
        [WebMethod]
        public Model.Model.BooksWithAuthorName getFirstBookAndAuthor()
        {
            using (var db = Model.Model.Database.GetConnection())
            {
                var query = "SELECT * FROM BooksWithAuthor ORDER BY bookId ASC LIMIT 1";
                var result = db.QuerySingle<Model.Model.BooksWithAuthorName>(query);
                return result;
            }
        }

        /// <summary>
        /// Query to get the last record in the BooksWithAuthor view
        /// </summary>
        /// <returns>An object containing a single row in the BooksWithAuthor view</returns>
        [WebMethod]
        public Model.Model.BooksWithAuthorName getLastBookAndAuthor()
        {
            using (var db = Model.Model.Database.GetConnection())
            {
                var query = "SELECT * FROM BooksWithAuthor ORDER BY bookId DESC LIMIT 1";
                var result = db.QuerySingle<Model.Model.BooksWithAuthorName>(query);
                return result;
            }
        }

        /// <summary>
        /// Query to get the next record in the BooksWithAuthor view, will return the last record if
        /// the next record is greater than last
        /// </summary>
        /// <param name="bookId">The value for this param is taken from lblBookEditID for 
        /// the book add and edit and lblBookId for the book navigation on Default.aspx</param>
        /// <returns>An object containing a single row in the BooksWithAuthor view</returns>
        [WebMethod]
        public Model.Model.BooksWithAuthorName getNextBookAndAuthor(string bookId, string maxBookId)
        {
            using (var db = Model.Model.Database.GetConnection())
            {
                if (long.Parse(bookId) > long.Parse(maxBookId))
                {
                    return getLastBookAndAuthor();
                }
                else
                {
                    var query = "SELECT * FROM BooksWithAuthor WHERE bookId > @id LIMIT 1";
                    var param = new { id = bookId };
                    var result = db.QuerySingle<Model.Model.BooksWithAuthorName>(query, param);
                    return result;
                }
            }
        }

        /// <summary>
        /// Query to get the previous record in the BooksWithAuthor view, will return the first record if
        /// the next record is less than first
        /// </summary>
        /// <param name="bookId">The value for this param is taken from lblBookEditID for 
        /// the book add and edit and lblBookId for the book navigation on Default.aspx</param>
        /// <returns>An object containing a single row in the BooksWithAuthor view</returns>
        [WebMethod]
        public Model.Model.BooksWithAuthorName getPreviousBookAndAuthor(string bookId, string minBookId)
        {
            using (var db = Model.Model.Database.GetConnection())
            {

                if (long.Parse(bookId) <= long.Parse(minBookId))
                {
                    return getFirstBookAndAuthor();
                }
                else
                {
                    var query = "SELECT * FROM BooksWithAuthor WHERE bookId < @id ORDER BY bookId DESC LIMIT 1";
                    var param = new { id = bookId };
                    var result = db.QuerySingle<Model.Model.BooksWithAuthorName>(query, param);
                    return result;
                }
            }
        }
        #endregion
    }
}
