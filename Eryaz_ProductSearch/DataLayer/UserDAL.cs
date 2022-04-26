using Eryaz_ProductSearch.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Eryaz_ProductSearch.DataLayer
{
    public class UserDAL
    {
        public string cnn = "";
        public UserDAL()
        {
            var builder = new ConfigurationBuilder().SetBasePath
            (Directory.GetCurrentDirectory()).
            AddJsonFile("appsettings.json").Build();

            cnn = builder.GetSection("ConnectionStrings:DefaultConnection").Value;
        }

        public Users Register(Users addUser)
        {
            using (SqlConnection cn = new SqlConnection(cnn))
            {
                using (SqlCommand cmd = new SqlCommand("Register", cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
                    cmd.Parameters["@UserName"].Value = addUser.UserName;
                    cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
                    cmd.Parameters["@Password"].Value = addUser.Password;
                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
                    cmd.Parameters["@Email"].Value = addUser.Email;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (cn.State != ConnectionState.Open)
                        cn.Open();
                    IDataReader reader = cmd.ExecuteReader();                    
                }
            }
            return addUser;
        }

        public bool LogIn(Users loginUser)
        {
            bool isValid = false;
            using (SqlConnection cn = new SqlConnection(cnn))
            {
                using (SqlCommand cmd =new SqlCommand("spLogin",cn))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
                    cmd.Parameters["@UserName"].Value = loginUser.UserName;
                    cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
                    cmd.Parameters["@Password"].Value = loginUser.Password;
                    cmd.CommandType= CommandType.StoredProcedure;

                    if (cn.State != ConnectionState.Open)
                        cn.Open();
                    cmd.ExecuteScalar();
                    IDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                        isValid = true;
                    else
                        isValid = false;
                }
            }
            return isValid;
        }
    }
}
