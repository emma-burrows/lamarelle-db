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
      MySqlDataAdapter adapter = new MySqlDataAdapter();

      using (adapter.SelectCommand = ContactsSQLHelper.GetCommand("SELECT * FROM employes WHERE utilisateur = ?user AND motdepasse = ?pw"))
      {
        adapter.SelectCommand.Parameters.Add("?user", username);
        adapter.SelectCommand.Parameters.Add("?pw", ToMD5(password));

        ContactsSQLHelper.GetConnection().Open();

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
    string insertSQL = "UPDATE employes SET motdepasse=?pw WHERE utilisateur=?user";

    using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(insertSQL))
    {
      cmd.Parameters.Add("?pw", ToMD5(newpassword));
      cmd.Parameters.Add("?user", username);

      ContactsSQLHelper.GetConnection().Open();

      return cmd.ExecuteNonQuery();
    }
  }

  // Get te
  private static string GetUserRole(string username)
  {
    string userrole = "";
    string selectSQL = "SELECT * FROM employes WHERE utilisateur=?user";

    using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(selectSQL))
    {
      cmd.Parameters.AddWithValue("?user", username);

      cmd.Connection.Open();

      using(MySqlDataReader dr = cmd.ExecuteReader())
      {
        dr.Read();
        if (dr.HasRows)
        {
          userrole = dr.GetString(dr.GetOrdinal("role"));
        }
        return userrole;
      }
    }
  }

  /// <summary>
  /// Get the MySQL comment attached to a particular column in a table.
  /// Originally used to put in user-visible hints for quick and dirty table editing, 
  /// but this is messy so marking obsolete until I can cull any references to it
  /// </summary>
  /// <param name="tablename"></param>
  /// <param name="columnname"></param>
  /// <returns></returns>
  [Obsolete]
  public static string GetColumnComment(string tablename, string columnname) 
  {
    string selectSQL = "SELECT column_comment FROM information_schema.columns WHERE ((columns.table_name=?tablename) AND (columns.column_name=?columnname))";

    using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(selectSQL))
    {
      cmd.Parameters.AddWithValue("?tablename", tablename);
      cmd.Parameters.AddWithValue("?columnname", columnname);

      ContactsSQLHelper.GetConnection().Open();

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

  //TODO: make user roles into an enum
  /// <summary>
  /// Returns the role of the specified user as a string.
  /// </summary>
  /// <param name="username">The username (email address) as a string.</param>
  /// <returns>User role - currently "visitor", "staff" and "admin"</returns>
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

