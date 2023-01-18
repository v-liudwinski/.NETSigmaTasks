using System.Data.SqlClient;
using Homework14_LiudvynskyiV.S.Entities;

namespace Homework14_LiudvynskyiV.S;

public class UserData
{
    private readonly string _connectionString;

    public UserData(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public int InsertUser(User user)
    {
 
        //Create the SQL Query for inserting an article
        var sqlQuery = String.Format("INSERT INTO user (firstname, secondname) VALUES ('{0}', '{1}');"
                                     + "SELECT @@Identity", user.FirstName, user.LastName);
 
        //Create and open a connection to SQL Server
        var connection = new SqlConnection(_connectionString);
        connection.Open();
 
        //Create a Command object
        var command = new SqlCommand(sqlQuery, connection);
 
        //Execute the command to SQL Server and return the newly created ID
        var newUserId = Convert.ToInt32((decimal)command.ExecuteScalar());
 
        //Close and dispose
        command.Dispose();
        connection.Close();
        connection.Dispose();
 
        // Set return value
        return newUserId;
    }
    
    public int SaveUser(User user)
        {
 
            //Create the SQL Query for inserting an article
            var createQuery  = String.Format("INSERT INTO user (firstname, secondname) VALUES ('{0}', '{1}');"
                                             + "SELECT @@Identity", user.FirstName, user.LastName);
            
            //Create the SQL Query for updating an article
            var updateQuery = String.Format("UPDATE user SET firstname='{0}', lastname= '{1}';",
                user.FirstName, user.LastName);
 
            //Create and open a connection to SQL Server
            var connection = new SqlConnection(_connectionString);
            connection.Open();
 
            //Create a Command object
            SqlCommand command = null;
 
            if (user.UserId != 0)
                command = new SqlCommand(updateQuery, connection);
            else
                command = new SqlCommand(createQuery, connection);
 
            int savedUserId = 0;
            try
            {
                //Execute the command to SQL Server and return the newly created ID
                var commandResult = command.ExecuteScalar();
                if (commandResult != null)
                {
                    savedUserId = Convert.ToInt32(commandResult);
                }
                else
                {
                    //the update SQL query will not return the primary key but if doesn't throw exception
                    //then we will take it from the already provided data
                    savedUserId = user.UserId;
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
 
            return savedUserId;
        }
 
 
 
        public User GetUserById(int userId)
        {
            var result = new User();
 
            //Create the SQL Query for returning an article category based on its primary key
            var sqlQuery = String.Format("select * from user where userid={0}", userId);
 
            //Create and open a connection to SQL Server
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
 
            var command = new SqlCommand(sqlQuery, connection);
 
            var dataReader = command.ExecuteReader();
 
            //load into the result object the returned row from the database
            if (!dataReader.HasRows) return result;
            while (dataReader.Read())
            {
                result.UserId = Convert.ToInt32(dataReader["userid"]);
                result.FirstName = dataReader["firstname"].ToString();
                result.LastName = dataReader["secondname"].ToString();
            }

            return result;
        }
 
        public List<User> GetUsers()
        {
 
            var result = new List<User>();
 
            //Create the SQL Query for returning all the articles
            var sqlQuery = string.Format("select * from user;");
 
            //Create and open a connection to SQL Server
            var connection = new SqlConnection(_connectionString);
            connection.Open();
 
            var command = new SqlCommand(sqlQuery, connection);
 
            //Create DataReader for storing the returning table into server memory
            var dataReader = command.ExecuteReader();

            //load into the result object the returned row from the database
            if (!dataReader.HasRows) return result;
            while (dataReader.Read())
            {
                var user = new User
                {
                    UserId = Convert.ToInt32(dataReader["userid"]),
                    FirstName = dataReader["firstname"].ToString(),
                    LastName = dataReader["secondname"].ToString()
                };

                result.Add(user);
            }

            return result;
 
        }
 
 
        public bool DeleteUser(int userId)
        {
            var result = false;
 
            //Create the SQL Query for deleting an article
            var sqlQuery = String.Format("delete from user where uerid = {0}", userId);
 
            //Create and open a connection to SQL Server
            var connection = new SqlConnection(_connectionString);
            connection.Open();
 
            //Create a Command object
            var command = new SqlCommand(sqlQuery, connection);
 
            // Execute the command
            var rowsDeletedCount = command.ExecuteNonQuery();
            if (rowsDeletedCount != 0)
                result = true;
            // Close and dispose
            command.Dispose();
            connection.Close();
            connection.Dispose();
 
 
            return result;
        }
}