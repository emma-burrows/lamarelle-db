using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

/// <summary>
/// Helper classes to connect to MySQL contacts database.
/// </summary>
public class ContactsSQLHelper
{
  public MySqlConnection Connection;

	public ContactsSQLHelper()
	{
        MySqlConnection objMyCon = new MySqlConnection(GetConnectionString());
        Connection = objMyCon;
    }

    /// <summary>
    /// Returns a connection string to the 'contacts' database based on the hostname
    /// </summary>
    /// <returns>Connection string</returns>
    private string GetConnectionString()
    {
      string webconfig = "";
      if (System.Environment.MachineName == "ASTELE" || System.Environment.MachineName == "PC0343")
      {
        webconfig = "LocalMySqlContactsConnectionString";
      }
      else
      {
        webconfig = "MySqlContactsConnectionString";
      }
      return ConfigurationManager.ConnectionStrings[webconfig].ConnectionString;
    }

    /// <summary>
    /// Get a connection object to the 'contacts' database
    /// </summary>
    /// <returns>MySqlConnection connection to 'contacts'</returns>
    private MySqlConnection GetConnection()
    {
        MySqlConnection objMyCon = new MySqlConnection( GetConnectionString() );
        return objMyCon;
    }

    /// <summary>
    /// Get a command object for the 'contacts' database
    /// </summary>
    /// <param name="cmd">The SQL string to use for the command</param>
    /// <param name="parameters">(Optional) Parameters which will be added to the SQL string, as consecutive key/value pairs (eg: param[0]='?user', param[1]='usernamestring')</param>
    /// <returns>MySqlCommand for 'contacts' based on the original input parameters</returns>
    public MySqlCommand GetCommand(string cmd)
    {
        MySqlConnection conn = Connection;
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
