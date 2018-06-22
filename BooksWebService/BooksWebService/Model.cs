using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.IO;
using Dapper;

namespace BooksWebService.Model
{
    public class Model
    {
        /// <summary>
        /// This is the class for the BooksWithAuthor view, the view joins the books and authors tables
        /// </summary>
        public class BooksWithAuthorName
        {
            /// <summary>
            /// This is for the bookTitle column in the books table
            /// </summary>
            public string BookTitle { get; set; }

            /// <summary>
            /// This is for the bookTitle column in the books table
            /// </summary>
            public string BookGenre { get; set; }

            /// <summary>
            /// This is for the authorName column in the authors table
            /// </summary>
            public string AuthorName { get; set; }

            /// <summary>
            /// This is for the bookId column in the books table
            /// </summary>
            public long BookId { get; set; }
        }

        /// <summary>
        /// This is the class for the author table 
        /// </summary>
        public class Author
        {
            /// <summary>
            /// This is for the authorId column in the authors table
            /// </summary>
            public long AuthorId { get; set; }

            /// <summary>
            /// This is for the authorName column in the authors table
            /// </summary>
            public string AuthorName { get; set; }
        }

        public static class Database
        {
            /// <summary>
            /// This is for connecting to the database
            /// </summary>
            /// <returns>New Connection to the database</returns>
            public static SQLiteConnection GetConnection()
            {
                var appDataFolder = HttpContext.Current.Server.MapPath("~/App_Data");
                var dbFileName = "Books.db";
                var createFullPath = Path.Combine(appDataFolder, dbFileName);
                var connectionString = $"Data Source={createFullPath}";
                return new SQLiteConnection(connectionString);
            }

            /// <summary>
            /// This creates the database from the database script
            /// </summary>
            public static void CreateDatabase()
            {
                var appDataFolder = HttpContext.Current.Server.MapPath("~/App_Data");
                var createScriptName = "dbScript.txt";
                var createScriptFullPath = Path.Combine(appDataFolder, createScriptName);
                var createScriptQuery = File.ReadAllText(createScriptFullPath);
                GetConnection().Execute(createScriptQuery);
            }
        }
    }
}