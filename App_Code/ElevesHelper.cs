using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Web.UI;

/// <summary>
/// Pupil ("élève") CRUD and stats methods.
/// </summary>
public class ElevesHelper
{

  public DataSet GetContactsDataSet(string SQL)
  {
    DataSet dataset = new DataSet();

    using ( MySqlCommand cmd = ContactsSQLHelper.GetCommand(SQL) )
    {
      MySqlDataAdapter adapter = new MySqlDataAdapter();
      adapter.SelectCommand = cmd;

      cmd.Connection.Open();
      adapter.Fill(dataset);
    }
    if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
    {
        return dataset;
    }
    else
    {
        return null;
    }

  }

  #region SQL statements - Public CRUD methods
    public int Insert(string tablename, Dictionary<string,string> values) 
    {
        string insertSQL = InsertString(tablename, values);

        using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(insertSQL))
        {
          return cmd.ExecuteNonQuery();
        }

    }
    
    public int Update(string tablename, int rowKey, Dictionary<string, string> values)
    {
      string updateSQL = UpdateString(tablename, rowKey, values);

      using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(updateSQL))
      {
        return cmd.ExecuteNonQuery();
      }
    }
    
    public int Delete(string tablename, int rowKey)
    {
      string deleteSQL = DeleteString(tablename, rowKey);

      using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(deleteSQL))
      {
        return cmd.ExecuteNonQuery();
      }
    }
  #endregion

  #region SQL statements - Private internal helper methods
    private string InsertString(string tablename, Dictionary<string, string> values)
    {
      // Create insert string command
      StringBuilder sb = new StringBuilder();
      sb.Append("INSERT INTO " + tablename + " ");
      sb.Append("(");

      string comma = "";

      foreach (string key in values.Keys)
      {
        sb.Append(comma);
        sb.Append(key);
        comma = ",";
      }

      sb.Append(") VALUES (");
      comma = "";
      int i = 0;

      foreach (KeyValuePair<string, string> kvp in values)
      {
        sb.Append(comma);

        if (kvp.Key == "motdepasse")
        {
            sb.Append("'" + LoginHelper.ToMD5(kvp.Value) + "'");
        }
        else
        {
            sb.Append("'" + kvp.Value + "'");
        }
        comma = ",";
      }

      sb.Append(")");

      if (sb.Length > 0)
          return sb.ToString();
      else
          return sb.ToString();
    }

    private string DeleteString(string tablename, int key)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("DELETE FROM " + tablename + " ");
        sb.Append("WHERE ID=" + key);
        sb.Append(")");

        if (sb.Length > 0)
            return sb.ToString();
        else
            return sb.ToString();
    }

    private string UpdateString(string tablename, int key, Dictionary<string, string> values)
    {
        // Create insert string command
        StringBuilder sb = new StringBuilder();
        sb.Append("UPDATE " + tablename + " ");
        sb.Append("SET ");

        string comma = "";
        foreach (KeyValuePair<string, string> pair in values)
        {
          sb.Append(comma);
          sb.Append(pair.Key + "='" + pair.Value + "'");
          
          comma = ", ";
        }

        sb.Append(" WHERE ID=" + key);

        if (sb.Length > 0)
            return sb.ToString();
        else
            return sb.ToString();
    }

    private DataSet GetDatasetFromSql(String SQL)
    {
      DataSet dataset = new DataSet();

      using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(SQL))
      {
        MySqlDataAdapter adapter = new MySqlDataAdapter();
        adapter.SelectCommand = cmd;

        cmd.Connection.Open();
        adapter.Fill(dataset);
      }
      if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
      {
        return dataset;
      }
      else
      {
        return null;
      }
    }


  #endregion

  public DataSet FamilyNamesLookup()
  {
    string SQL = "SELECT * FROM familynames";
    return GetDatasetFromSql(SQL);
  }

  public DataSet EmailsLookup()
  {
    string SQL = "SELECT * FROM enrolledemails";
    return GetDatasetFromSql(SQL);
  }

  public static DataSet ClassesInUseLookup()
  {
    DataSet dataset = new DataSet();
    string SQL = "SELECT * FROM classesinuse ORDER BY Niveau";

    using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(SQL))
    {
      MySqlDataAdapter adapter = new MySqlDataAdapter();
      adapter.SelectCommand = cmd;

      ContactsSQLHelper.GetConnection().Open();
      adapter.Fill(dataset);
    }
    if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
    {
      return dataset;
    }
    else
    {
      return null;
    }

  }

  public DataSet ElevesByFamilyLookup()
  {
    string SQL = "SELECT * FROM elevesbyfamily ORDER BY Family;";
    return GetDatasetFromSql(SQL);
  }

  public DataSet NumberOfFamilies()
  {
    string SQL = "SELECT count(*), famille from elevesbyfamily group by famille";
    return GetDatasetFromSql(SQL);
  }

  public DataSet ElevesByHour()
  {
    string SQL = "SELECT sum(eleves), heures FROM `classesinuse` group by heures";
    return GetDatasetFromSql(SQL);
  }


  public static void GetEmailFile(string path, Page current)
  {
    ElevesHelper eh = new ElevesHelper();
    System.Data.DataSet dsEmails = eh.EmailsLookup();
    System.Data.DataTable dt = dsEmails.Tables[0];

    String filepath = current.Server.MapPath(path);

    System.IO.StreamWriter sw = new System.IO.StreamWriter(filepath, false);

    int iColCount = dt.Columns.Count;

    foreach (System.Data.DataRow dr in dt.Rows)
    {
      for (int i = 0; i < iColCount; i++)
      {
        if (!Convert.IsDBNull(dr[i]))
        {
          sw.Write(dr[i].ToString());
        }
        if (i < iColCount - 1)
        {
          sw.Write(",");
        }
      }
      sw.Write(";");
      sw.Write(sw.NewLine);
    }
    sw.Close();
  }

}
