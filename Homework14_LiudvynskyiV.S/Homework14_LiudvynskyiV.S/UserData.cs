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
        var sqlQuery = String.Format("INSERT INTO user (firstname, secondname) VALUES ('{0}', '{1}');"
                                     + "SELECT @@Identity", user.FirstName, user.LastName);
        
        var connection = new SqlConnection(_connectionString);
        connection.Open();
        
        var command = new SqlCommand(sqlQuery, connection);
        
        var newUserId = Convert.ToInt32((decimal)command.ExecuteScalar());
        
        command.Dispose();
        connection.Close();
        connection.Dispose();
        
        return newUserId;
    }
    
    public int SaveUser(User user)
        {
            var createQuery  = String.Format("INSERT INTO user (firstname, secondname) VALUES ('{0}', '{1}');"
                                             + "SELECT @@Identity", user.FirstName, user.LastName);
            
            var updateQuery = String.Format("UPDATE user SET firstname='{0}', lastname= '{1}';",
                user.FirstName, user.LastName);
            
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            
            SqlCommand command = null;
 
            command = user.UserId != 0 
                ? new SqlCommand(updateQuery, connection) 
                : new SqlCommand(createQuery, connection);
 
            var savedUserId = 0;
            try
            {
                var commandResult = command.ExecuteScalar();
                savedUserId = commandResult != null ? Convert.ToInt32(commandResult) : user.UserId;
            }
            catch (Exception ex)
            {
                throw;
            }
            
            command.Dispose();
            connection.Close();
            connection.Dispose();
 
            return savedUserId;
        }
 
 
 
        public User GetUserById(int userId)
        {
            var result = new User();
            
            var sqlQuery = String.Format("select * from user where userid={0}", userId);
            
            var connection = new SqlConnection(_connectionString);
            connection.Open();
 
            var command = new SqlCommand(sqlQuery, connection);
 
            var dataReader = command.ExecuteReader();
            
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
            
            var sqlQuery = string.Format("select * from user;");
            
            var connection = new SqlConnection(_connectionString);
            connection.Open();
 
            var command = new SqlCommand(sqlQuery, connection);
            
            var dataReader = command.ExecuteReader();
            
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
            
            var sqlQuery = String.Format("delete from user where uerid = {0}", userId);
            
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            
            var command = new SqlCommand(sqlQuery, connection);
            
            var rowsDeletedCount = command.ExecuteNonQuery();
            if (rowsDeletedCount != 0)
                result = true;
            
            command.Dispose();
            connection.Close();
            connection.Dispose();
 
 
            return result;
        }
}