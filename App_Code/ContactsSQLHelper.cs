using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

/// <summary>
/// Helper classes to connect to MySQL contacts database.
/// </summary>
public static class ContactsSQLHelper
{
  private static string ConnectionString { get; set; }

	static ContactsSQLHelper()
	{
    ConnectionString = GetConnectionString();
  }

  /// <summary>
  /// Returns a new connection to the appropriate MySQL server
  /// </summary>
  /// <returns>MySQL connection</returns>
  public static MySqlConnection GetConnection()
  {
    return new MySqlConnection(ConnectionString);
  }

  /// <summary>
  /// Returns a connection string to the 'contacts' database based on the hostname
  /// </summary>
  /// <returns>Connection string</returns>
  private static string GetConnectionString()
  {
    string webconfig = "";
    String servername = HttpContext.Current.Request.Url.Host;
    String apppath = HttpContext.Current.Request.ApplicationPath;
    if ((servername == "lamarelle.org.uk" || servername == "www.lamarelle.org.uk") && apppath == "/pefd-db")
    {
      webconfig = "MySqlContactsConnectionString";
    }
    else if (servername == "eburrows.co.uk" || servername == "www.eburrows.co.uk" || apppath == "/db-test")
    {
      webconfig = "TestContactsConnectionString";
    }
    else
    {
      webconfig = "LocalMySqlContactsConnectionString";
    }

    return ConfigurationManager.ConnectionStrings[webconfig].ConnectionString;
  }

  /// <summary>
  /// Get a command object for the 'contacts' database
  /// </summary>
  /// <param name="cmd">The SQL string to use for the command</param>
  /// <param name="parameters">(Optional) Parameters which will be added to the SQL string, as consecutive key/value pairs (eg: param[0]='?user', param[1]='usernamestring')</param>
  /// <returns>MySqlCommand for 'contacts' based on the original input parameters</returns>
  public static MySqlCommand GetCommand(string cmd)
  {
      MySqlConnection conn = GetConnection();
      if (conn == null) return null;
      MySqlCommand command;
      try
      {
          command = new MySqlCommand(cmd, conn);
      }
      catch { command = null; }
      return command;
  }

}
