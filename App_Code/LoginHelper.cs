using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for LoginHelper
/// </summary>
public static class LoginHelper
{

  /// <summary>
  /// Returns an MD5 hashed version of a string for use in the MySQL database.
  /// </summary>
  /// <param name="value">The string to hash.</param>
  /// <returns>The hashed string.</returns>
    public static string ToMD5(string value)
    {
        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] data = System.Text.Encoding.ASCII.GetBytes(value);
        data = x.ComputeHash(data);
        string ret = "";
        for (int i = 0; i < data.Length; i++)
            ret += data[i].ToString("x2").ToLower();
        return ret;
    }

    /// <summary>
    /// Checks that the username and password combination exist in the database.
    /// </summary>
    /// <param name="username">Username to check</param>
    /// <param name="password">Password corresponding to the user</param>
    /// <returns></returns>
    public static bool ValidateLogin(string username, string password)
    {
      DataSet dataset = new DataSet();
      ContactsSQLHelper conSql = new ContactsSQLHelper();

      using (MySqlDataAdapter adapter = new MySqlDataAdapter())
      {
        adapter.SelectCommand = conSql.GetCommand("SELECT * FROM employes WHERE utilisateur = ?user AND motdepasse = ?pw");
        adapter.SelectCommand.Parameters.Add("?user", username);
        adapter.SelectCommand.Parameters.Add("?pw", ToMD5(password));

        conSql.Connection.Open();

        adapter.Fill(dataset);

        if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
      }

    }

  /// <summary>
  /// Write the new password to the database
  /// </summary>
  /// <param name="username">Username for the user to change</param>
  /// <param name="newpassword">New password</param>
  /// <returns>An int specifying whether the SQL query to update the record was successful or not.</returns>
    public static int ChangePassword(string username, string newpassword)
    {
        ContactsSQLHelper conSql = new ContactsSQLHelper();
        string insertSQL = "UPDATE employes SET motdepasse=?pw WHERE utilisateur=?user";

        using (MySqlCommand cmd = conSql.GetCommand(insertSQL))
        {
          cmd.Parameters.Add("?pw", ToMD5(newpassword));
          cmd.Parameters.Add("?user", username);

          conSql.Connection.Open();
          return cmd.ExecuteNonQuery();
        }
    }

    private static string GetUserRole(string username)
    {
        ContactsSQLHelper conSql = new ContactsSQLHelper();
        string selectSQL = "SELECT * FROM employes WHERE utilisateur=?user";

        using (MySqlCommand cmd = conSql.GetCommand(selectSQL))
        {
           cmd.Parameters.Add("?user", username);

          conSql.Connection.Open();
          using(MySqlDataReader dr = cmd.ExecuteReader())
          {
              dr.Read();
              if (dr.HasRows)
              {
                return dr.GetString(dr.GetOrdinal("role"));
              }
              else
              {
                return "";
              }
          }
        }
    }

    public static string GetColumnComment(string tablename, string columnname) {
      string comment = "";

      ContactsSQLHelper conSql = new ContactsSQLHelper();
      string selectSQL = "SELECT column_comment FROM information_schema.columns WHERE ((columns.table_name=?tablename) AND (columns.column_name=?columnname))";

      using (MySqlCommand cmd = conSql.GetCommand(selectSQL))
      {
        cmd.Parameters.Add("?tablename", tablename);
        cmd.Parameters.Add("?columnname", columnname);

        conSql.Connection.Open();
        using (MySqlDataReader dr = cmd.ExecuteReader())
        {
          dr.Read();
          if (dr.HasRows)
          {
            return dr.GetString(0);
          }
          else
          {
            return "";
          }
        }
      }

    }

    public static string UserRole(string username)
    {
        string role = "visitor";
        HttpCookie cookieCheck = null;
        
        cookieCheck = HttpContext.Current.Request.Cookies["LaMarelleDB"];


        if (cookieCheck == null)
        {
            role = GetUserRole(username);
            HttpCookie cookieCreate = new HttpCookie("LaMarelleDB");
            cookieCreate.Expires = DateTime.MinValue;
            HttpContext.Current.Response.Cookies.Add(cookieCreate);

            HttpContext.Current.Request.Cookies["LaMarelleDB"][username] = role;
            return role;
        }
        else
        {
            return HttpContext.Current.Request.Cookies["LaMarelleDB"][username];
        }
    }

}

