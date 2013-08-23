using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

/// <summary>
/// Create, Read, Update, Delete methods for Classes 
/// </summary>
[DataObject(true)]
public static class ClassesDataObject
{

  [DataObjectMethod(DataObjectMethodType.Select)]
  public static List<Classe> GetClasses() 
  {
    string sqlstring = "SELECT * FROM classes ORDER BY Niveau, Nom";
    MySqlCommand cmd = ContactsSQLHelper.GetCommand(sqlstring);

    cmd.Connection.Open();
    MySqlDataReader dr =
       cmd.ExecuteReader(CommandBehavior.CloseConnection);

    List<Classe> ClasseList = new List<Classe>();
    while (dr.Read())
    {
      Classe classe = new Classe();
      classe.ID = Convert.ToInt32(dr["ID"]);
      classe.AgeDebut = Convert.ToInt32(dr["AgeDebut"]);
      classe.AgeFin = Convert.ToInt32(dr["AgeFin"]);
      classe.Enseignant = Convert.ToString(dr["Enseignant"]);
      classe.Niveau = Convert.ToInt32(dr["Niveau"]);
      classe.Nom = Convert.ToString(dr["Nom"]);
      classe.Section = Convert.ToString(dr["Section"]);
 
      ClasseList.Add(classe);
    }
    dr.Close();
    return ClasseList;

  }


  [DataObjectMethod(DataObjectMethodType.Insert)]
  public static Int32 InsertClasse(Classe classe)
  {
    string sqlstring = "INSERT INTO `classes` (`AgeDebut`, `AgeFin`, `Enseignant`, `Niveau`, `Nom`, `Section`) ";

    sqlstring += " VALUES (?vAgeDebut, ?vAgeFin, ?vEnseignant, ?vNiveau, ?vNom, ?vSection)";

    
    //using(MySqlCommand cmd = new MySqlCommand("InsertEleve", new MySqlConnection(GetConnectionString())))
    using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(sqlstring))
    {
      //cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.Add(new MySqlParameter("vAgeDebut", classe.AgeDebut));
      cmd.Parameters.Add(new MySqlParameter("vAgeFin", classe.AgeFin));
      cmd.Parameters.Add(new MySqlParameter("vEnseignant", classe.Enseignant));
      cmd.Parameters.Add(new MySqlParameter("vNiveau", classe.Niveau));
      cmd.Parameters.Add(new MySqlParameter("vNom", classe.Nom));
      cmd.Parameters.Add(new MySqlParameter("vSection", classe.Section));
      
      cmd.Connection.Open();
      cmd.ExecuteNonQuery();

      // If has last inserted id, add a parameter to hold it.
      if (cmd.LastInsertedId != 0L)
      {
        cmd.Parameters.Add( new MySqlParameter("newId", cmd.LastInsertedId) );
      }

      // Return the id of the new record. Convert from Int64 to Int32 (int).
      return Convert.ToInt32(cmd.Parameters["@newId"].Value);

    }
  }

  [DataObjectMethod(DataObjectMethodType.Update)]
  public static int UpdateClasse(Classe classe)
  {
    string sqlstring = "UPDATE `classes` SET `Niveau`=?vNiveau, `Nom`=?vNom, `Enseignant`=?vEnseignant, `AgeDebut`=?vAgeDebut, `AgeFin`=?vAgeFin, `Section`=?vSection WHERE id=?key";

    using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(sqlstring))
    {

      cmd.Parameters.Add(new MySqlParameter("key", classe.ID));
      cmd.Parameters.Add(new MySqlParameter("vNiveau", classe.Niveau));
      cmd.Parameters.Add(new MySqlParameter("vNom", classe.Nom));
      cmd.Parameters.Add(new MySqlParameter("vEnseignant", classe.Enseignant));
      cmd.Parameters.Add(new MySqlParameter("vAgeDebut", classe.AgeDebut));
      cmd.Parameters.Add(new MySqlParameter("vAgeFin", classe.AgeFin));
      cmd.Parameters.Add(new MySqlParameter("vSection", classe.Section));

      cmd.Connection.Open();
      int i = cmd.ExecuteNonQuery();
      cmd.Connection.Close();
      return i;
    }
  }

  [DataObjectMethod(DataObjectMethodType.Delete)]
  public static int DeleteClasse(Classe contact)
  {
    string sqlstring = "DELETE FROM `classes` WHERE ID=?key";

    using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(sqlstring))
    {
      cmd.Parameters.Add(new MySqlParameter("key", contact.ID));
      cmd.Connection.Open();
      int i = cmd.ExecuteNonQuery();
      cmd.Connection.Close();
      return i;
    }
  }

}

public class Classe
{
  int _ID;
  int _Niveau;
  string _Nom;
  string _Enseignant;
  int _AgeDebut;
  int _AgeFin;
  string _Section;

  public Classe()
  {
  }
  public Classe(int ID, int Niveau, string Nom, string Enseignant, int AgeDebut,
  int AgeFin, string Section)
  {
    this.ID = ID;
    this.Niveau = Niveau;
    this.Nom = Nom;
    this.Enseignant = Enseignant;
    this.AgeDebut = AgeDebut;
    this.AgeFin = AgeFin;
    this.Section = Section;
  }

  public int ID
  {
    get { return _ID; }
    set { _ID = value; }
  }

  public int Niveau
  {
    get { return _Niveau; }
    set { _Niveau = value; }
  }

  public string Nom
  {
    get { return _Nom; }
    set { _Nom = value; }
  }
  public string Enseignant
  {
    get { return _Enseignant; }
    set { _Enseignant = value; }
  }
  public int AgeDebut
  {
    get { return _AgeDebut; }
    set { _AgeDebut = value; }
  }
  public int AgeFin
  {
    get { return _AgeFin; }
    set { _AgeFin = value; }
  }
  public string Section
  {
    get { return _Section; }
    set { _Section = value; }
  }

}
