using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DotNetCrud.Web.Data.Models;

namespace DotNetCrud.Web.Data.Services.ADO
{
    public abstract class ADODataService<T> where T : IDataTableObject
    {
        private string ConnectionString = string.Empty;

        public ADODataService()
        {
            ConnectionString =
                "Server=(LocalDb)\\MSSQLLocalDB;Database=Shop;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        public int Insert(T obj)
        {
            //Create the SQL Query for inserting an article
            string sqlQuery = GetInsertQuery(obj);

            //Create and open a connection to SQL Server
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            //Create a Command object
            SqlCommand command = new SqlCommand(sqlQuery, connection);

            //Execute the command to SQL Server and return the newly created ID
            int newObjId =
                Convert.ToInt32((decimal) command.ExecuteScalar());

            //Close and dispose
            command.Dispose();
            connection.Close();
            connection.Dispose();

            // Set return value
            return newObjId;
        }

        public abstract string GetInsertQuery(T obj);

        public T GetById(int id)
        {
            T result = GetNewObj();

            // Create the SQL Query for returning an article category based on its primary key
            string sqlQuery = GetSelectByIdQuery(id);

            //Create and open a connection to SQL Server
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(sqlQuery, connection);

            SqlDataReader dataReader = command.ExecuteReader();

            //load into the result object the returned row from the database
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    PopulateObjFromDataReader(dataReader, result);
                }
            }

            return result;
        }

        protected abstract void PopulateObjFromDataReader(SqlDataReader dataReader, T obj);

        protected abstract T GetNewObj();

        public abstract string GetSelectByIdQuery(int id);

        public int Save(T article)
        {
            //Create the SQL Query for inserting an article
            string createQuery = GetInsertQuery(article);

            //Create the SQL Query for updating an article
            string updateQuery = GetUpdateQuery(article);

            //Create and open a connection to SQL Server
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            //Create a Command object
            SqlCommand command = null;

            if (article.Id != 0)
                command = new SqlCommand(updateQuery, connection);
            else
                command = new SqlCommand(createQuery, connection);

            int savedArticleID = 0;
            try
            {
                //Execute the command to SQL Server and return the newly created ID
                var commandResult = command.ExecuteScalar();
                if (commandResult != null)
                {
                    savedArticleID = Convert.ToInt32(commandResult);
                }
                else
                {
                    //the update SQL query will not return the primary key but if doesn't throw exception
                    //then we will take it from the already provided data
                    savedArticleID = article.Id;
                }
            }
            catch (Exception ex)
            {
                //there was a problem executing the script
            }

            //Close and dispose
            command.Dispose();
            connection.Close();
            connection.Dispose();

            return savedArticleID;
        }

        protected abstract string GetUpdateQuery(T obj);

        public List<T> GetList()
        {
            List<T> result = new List<T>();
 
            //Create the SQL Query for returning all the articles
            string sqlQuery = GetSelectAllQuery();

            //Create and open a connection to SQL Server
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
 
            SqlCommand command = new SqlCommand(sqlQuery, connection);

            //Create DataReader for storing the returning table into server memory
            SqlDataReader dataReader = command.ExecuteReader();

            T obj = default(T);

            //load into the result object the returned row from the database
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    obj = GetNewObj();

                    PopulateObjFromDataReader(dataReader, obj);

                    result.Add(obj);
                }
            }

            return result;
 
        }

        protected abstract string GetSelectAllQuery();

        public bool DeleteArticle(int objId)
        {
            bool result = false;

            //Create the SQL Query for deleting an article
            string sqlQuery = GetDeleteQuery(objId);

            //Create and open a connection to SQL Server
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            //Create a Command object
            SqlCommand command = new SqlCommand(sqlQuery, connection);

            // Execute the command
            int rowsDeletedCount = command.ExecuteNonQuery();

            if (rowsDeletedCount != 0)
                result = true;

            // Close and dispose
            command.Dispose();
            connection.Close();
            connection.Dispose();

            return result;
        }

        protected abstract string GetDeleteQuery(int objId);
    }
}