using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

/// <summary>
/// Summary description for ContactDataObject
/// </summary>
[DataObject(true)]
public static class ContactDataObject
{

  private static string GetConnectionString()
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

  [DataObjectMethod(DataObjectMethodType.Select)]
  public static List<Contact> GetContacts() 
  {

    //MySqlCommand cmd = new MySqlCommand("ShowAllContact",
    //               new MySqlConnection(GetConnectionString()));
    //cmd.CommandType = CommandType.StoredProcedure;

    string sqlstring = "SELECT * FROM ct_contacts ORDER BY Nom";
    MySqlCommand cmd = new MySqlCommand(sqlstring, new MySqlConnection(GetConnectionString()));

    cmd.Connection.Open();
    MySqlDataReader dr =
       cmd.ExecuteReader(CommandBehavior.CloseConnection);

    List<Contact> ContactList = new List<Contact>();
    while (dr.Read())
    {
      Contact contact = new Contact();
      contact.Contact_ID = Convert.ToInt32(dr["ID"]);
      contact.ActuellementInscrit = Convert.ToInt32(dr["ActuellementInscrit"]);
      contact.Adresse1 = Convert.ToString(dr["Adresse1"]);
      contact.Adresse2 = Convert.ToString(dr["Adresse2"]);
      contact.Adresse3 = Convert.ToString(dr["Adresse3"]);
      contact.CodePostal = Convert.ToString(dr["Code Postal"]);
      contact.Comite = Convert.ToInt32(dr["Comite"]);
      contact.Email = Convert.ToString(dr["Email"]);
      contact.Fixe = Convert.ToString(dr["Fixe"]);
      contact.NePasContacter = Convert.ToInt32(dr["NePasContacter"]);
      contact.Nom = Convert.ToString(dr["Nom"]);
      contact.Notes = Convert.ToString(dr["Notes"]);
      contact.Portable = Convert.ToString(dr["Portable"]);
      contact.Prenom = Convert.ToString(dr["Prenom"]);
      contact.Ville = Convert.ToString(dr["Ville"]);
      ContactList.Add(contact);
    }
    dr.Close();
    return ContactList;

  }

  [DataObjectMethod(DataObjectMethodType.Select)]
  public static List<Contact> GetContactsById(Int32 ContactID, Int32 Status)
  {

    //MySqlCommand cmd = new MySqlCommand("ShowAllContact",
    //               new MySqlConnection(GetConnectionString()));
    //cmd.CommandType = CommandType.StoredProcedure;
    string sqlstring = "SELECT * FROM ct_contacts";
    if (ContactID == 0 && Status == -1)
    {
      sqlstring += " ORDER BY Nom";
    }
    else if (ContactID != 0)
    {
      sqlstring += " WHERE ID=?key ORDER BY Nom";
    }
    else if (ContactID == 0 && Status > -1)
    {
      sqlstring += " WHERE ActuellementInscrit=?inscrit ORDER BY Nom";
    }

    MySqlCommand cmd = new MySqlCommand(sqlstring, new MySqlConnection(GetConnectionString()));
    if (ContactID > 0) { 
      cmd.Parameters.Add(new MySqlParameter("key", ContactID)); 
    }
    if (Status >= 0)
    {
      cmd.Parameters.Add(new MySqlParameter("inscrit", Status)); 
    }
    cmd.Connection.Open();
    MySqlDataReader dr =
       cmd.ExecuteReader(CommandBehavior.CloseConnection);

    List<Contact> ContactList = new List<Contact>();
    while (dr.Read())
    {
      Contact contact = new Contact();
      contact.Contact_ID = Convert.ToInt32(dr["ID"]);
      contact.ActuellementInscrit = Convert.ToInt32(dr["ActuellementInscrit"]);
      contact.Adresse1 = Convert.ToString(dr["Adresse1"]);
      contact.Adresse2 = Convert.ToString(dr["Adresse2"]);
      contact.Adresse3 = Convert.ToString(dr["Adresse3"]);
      contact.CodePostal = Convert.ToString(dr["Code Postal"]);
      contact.Comite = Convert.ToInt32(dr["Comite"]);
      contact.Email = Convert.ToString(dr["Email"]);
      contact.Fixe = Convert.ToString(dr["Fixe"]);
      contact.NePasContacter = Convert.ToInt32(dr["NePasContacter"]);
      contact.Nom = Convert.ToString(dr["Nom"]);
      contact.Notes = Convert.ToString(dr["Notes"]);
      contact.Portable = Convert.ToString(dr["Portable"]);
      contact.Prenom = Convert.ToString(dr["Prenom"]);
      contact.Ville = Convert.ToString(dr["Ville"]);
      ContactList.Add(contact);
    }
    dr.Close();
    return ContactList;

  }

  [DataObjectMethod(DataObjectMethodType.Select)]
  public static List<Contact> GetContactsByStatus(Boolean inscrit)
  {

    //MySqlCommand cmd = new MySqlCommand("ShowAllContact",
    //               new MySqlConnection(GetConnectionString()));
    //cmd.CommandType = CommandType.StoredProcedure;
    string sqlstring = "";
    sqlstring = "SELECT * FROM ct_contacts WHERE ActuellementInscrit=?inscrit ORDER BY Nom";

    MySqlCommand cmd = new MySqlCommand(sqlstring, new MySqlConnection(GetConnectionString()));
    cmd.Parameters.Add(new MySqlParameter("inscrit", inscrit));
    cmd.Connection.Open();
    MySqlDataReader dr =
       cmd.ExecuteReader(CommandBehavior.CloseConnection);

    List<Contact> ContactList = new List<Contact>();
    while (dr.Read())
    {
      Contact contact = new Contact();
      contact.Contact_ID = Convert.ToInt32(dr["ID"]);
      contact.ActuellementInscrit = Convert.ToInt32(dr["ActuellementInscrit"]);
      contact.Adresse1 = Convert.ToString(dr["Adresse1"]);
      contact.Adresse2 = Convert.ToString(dr["Adresse2"]);
      contact.Adresse3 = Convert.ToString(dr["Adresse3"]);
      contact.CodePostal = Convert.ToString(dr["Code Postal"]);
      contact.Comite = Convert.ToInt32(dr["Comite"]);
      contact.Email = Convert.ToString(dr["Email"]);
      contact.Fixe = Convert.ToString(dr["Fixe"]);
      contact.NePasContacter = Convert.ToInt32(dr["NePasContacter"]);
      contact.Nom = Convert.ToString(dr["Nom"]);
      contact.Notes = Convert.ToString(dr["Notes"]);
      contact.Portable = Convert.ToString(dr["Portable"]);
      contact.Prenom = Convert.ToString(dr["Prenom"]);
      contact.Ville = Convert.ToString(dr["Ville"]);
      ContactList.Add(contact);
    }
    dr.Close();
    return ContactList;

  }

  [DataObjectMethod(DataObjectMethodType.Insert)]
  public static Int32 InsertContact(Contact contact)
  {
    string sqlstring = "INSERT INTO ct_contacts (`Nom`, `Prenom`, `Adresse1`, `Adresse2`, `Adresse3`, `Ville`, `Code Postal`, `Fixe`, `Portable`, `Email`, `Notes`, `ActuellementInscrit`, `Comite`, `NePasContacter`) ";
    sqlstring += "VALUES(?vNom, ?vPrenom, ?vAdresse1, ?vAdresse2, ?vAdresse3, ?vVille, ?vCodePostal, ?vFixe, ?vPortable, ?vEmail, ?vNotes, ?vActuellementInscrit, ?vComite, ?vNePasContacter)";

    //using (MySqlCommand cmd = new MySqlCommand("InsertContact", new MySqlConnection(GetConnectionString())))
    using(MySqlCommand cmd = new MySqlCommand(sqlstring, new MySqlConnection(GetConnectionString())))
    {
      //cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.Add(new MySqlParameter("vNom", contact.Nom));
      cmd.Parameters.Add(new MySqlParameter("vPrenom", contact.Prenom));
      cmd.Parameters.Add(new MySqlParameter("vAdresse1", contact.Adresse1));
      cmd.Parameters.Add(new MySqlParameter("vAdresse2", contact.Adresse2));
      cmd.Parameters.Add(new MySqlParameter("vAdresse3", contact.Adresse3));
      cmd.Parameters.Add(new MySqlParameter("vVille", contact.Ville));
      cmd.Parameters.Add(new MySqlParameter("vCodePostal", contact.CodePostal));
      cmd.Parameters.Add(new MySqlParameter("vFixe", contact.Fixe));
      cmd.Parameters.Add(new MySqlParameter("vPortable", contact.Portable));
      cmd.Parameters.Add(new MySqlParameter("vEmail", contact.Email));
      cmd.Parameters.Add(new MySqlParameter("vNotes", contact.Notes));
      cmd.Parameters.Add(new MySqlParameter("vActuellementInscrit", contact.ActuellementInscrit));
      cmd.Parameters.Add(new MySqlParameter("vComite", contact.Comite));
      cmd.Parameters.Add(new MySqlParameter("vNePasContacter", contact.NePasContacter));

      cmd.Connection.Open();
      cmd.ExecuteNonQuery();
      // If has last inserted id, add a parameter to hold it.
      if (cmd.LastInsertedId != null) cmd.Parameters.Add(
                  new MySqlParameter("newId", cmd.LastInsertedId));

      // Return the id of the new record. Convert from Int64 to Int32 (int).
      return Convert.ToInt32(cmd.Parameters["@newId"].Value);
    }
  }

  [DataObjectMethod(DataObjectMethodType.Update)]
  public static void UpdateContact(Contact contact)
  {
    string sqlstring = "UPDATE ct_contacts SET `Nom`=?vNom, `Prenom`=?vPrenom, `Adresse1`=?vAdresse1, `Adresse2`=?vAdresse2, `Adresse3`=?vAdresse3, `Ville`=?vVille, `Code Postal`=?vCodePostal, `Fixe`=?vFixe, `Portable`=?vPortable, `Email`=?vEmail, `Notes`=?vNotes, `ActuellementInscrit`=?vActuellementInscrit, `Comite`=?vComite, `NePasContacter`=?vNePasContacter WHERE ID = ?key";

    //using (MySqlCommand cmd = new MySqlCommand("UpdateContact", new MySqlConnection(GetConnectionString())))
    using (MySqlCommand cmd = new MySqlCommand(sqlstring, new MySqlConnection(GetConnectionString())))
    {
      //cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.Add(new MySqlParameter("key", contact.Contact_ID));
      cmd.Parameters.Add(new MySqlParameter("vNom", contact.Nom));
      cmd.Parameters.Add(new MySqlParameter("vPrenom", contact.Prenom));
      cmd.Parameters.Add(new MySqlParameter("vAdresse1", contact.Adresse1));
      cmd.Parameters.Add(new MySqlParameter("vAdresse2", contact.Adresse2));
      cmd.Parameters.Add(new MySqlParameter("vAdresse3", contact.Adresse3));
      cmd.Parameters.Add(new MySqlParameter("vVille", contact.Ville));
      cmd.Parameters.Add(new MySqlParameter("vCodePostal", contact.CodePostal));
      cmd.Parameters.Add(new MySqlParameter("vFixe", contact.Fixe));
      cmd.Parameters.Add(new MySqlParameter("vPortable", contact.Portable));
      cmd.Parameters.Add(new MySqlParameter("vEmail", contact.Email));
      cmd.Parameters.Add(new MySqlParameter("vNotes", contact.Notes));
      cmd.Parameters.Add(new MySqlParameter("vActuellementInscrit", contact.ActuellementInscrit));
      cmd.Parameters.Add(new MySqlParameter("vComite", contact.Comite));
      cmd.Parameters.Add(new MySqlParameter("vNePasContacter", contact.NePasContacter));

      cmd.Connection.Open();
      cmd.ExecuteNonQuery();
    }
  }

  [DataObjectMethod(DataObjectMethodType.Delete)]
  public static int DeleteContact(Contact contact)
  {
    //MySqlCommand cmd = new MySqlCommand("DeleteNews",
    //        new MySqlConnection(GetConnectionString()));
    //cmd.CommandType = CommandType.StoredProcedure;

    string sqlstring = "DELETE FROM ct_contacts WHERE ID=?key";

    using (MySqlCommand cmd = new MySqlCommand(sqlstring, new MySqlConnection(GetConnectionString())))
    {
      cmd.Parameters.Add(new MySqlParameter("key", contact.Contact_ID));
      cmd.Connection.Open();
      int i = cmd.ExecuteNonQuery();
      cmd.Connection.Close();
      return i;
    }
  }

}

public class Contact
{
  int _Contact_ID;
  string _Nom;
  string _Prenom;
  string _Adresse1;
  string _Adresse2;
  string _Adresse3;
  string _Ville;
  string _CodePostal;
  string _Fixe;
  string _Portable;
  string _Email;
  string _Notes;
  int _ActuellementInscrit;
  int _Comite;
  int _NePasContacter;

  public Contact()
  {
  }

  public Contact(int Contact_ID, string Nom, string Prenom, string Adresse1, string Adresse2, string Adresse3, string Ville, string CodePostal, string Fixe, string Portable, string Email, string Notes, int ActuellementInscrit, int Comite, int NePasContacter) 
  {
    this.Contact_ID = Contact_ID;
    this.Nom = Nom;
    this.Prenom = Prenom;
    this.Adresse1 = Adresse1;
    this.Adresse2 = Adresse2;
    this.Adresse3 = Adresse3;
    this.Ville = Ville;
    this.CodePostal = CodePostal;
    this.Fixe = Fixe;
    this.Portable = Portable;
    this.Email = Email;
    this.Notes = Notes;
    this.ActuellementInscrit = ActuellementInscrit;
    this.Comite = Comite;
    this.NePasContacter = NePasContacter;
  }

  public int Contact_ID
  {
    get { return _Contact_ID; }
    set { _Contact_ID = value; }
  }
  public string Nom
  {
    get { return _Nom; }
    set { _Nom = value; }
  }
  public string Prenom
  {
    get { return _Prenom; }
    set { _Prenom = value; }
  }
  public string Adresse1
  {
    get { return _Adresse1; }
    set { _Adresse1 = value; }
  }
  public string Adresse2 
  {
    get { return _Adresse2; }
    set { _Adresse2 = value; }
  }
  public string Adresse3
  {
    get { return _Adresse3;}
    set { _Adresse3 = value; }
  }
  public string Ville 
  {
    get { return _Ville; }
    set { _Ville = value; }
  }
  public string CodePostal
  {
    get { return _CodePostal; }
    set { _CodePostal = value; }
  }
  public string Fixe
  {
    get { return _Fixe; }
    set { _Fixe = value; }
  }
  public string Portable
  {
    get { return _Portable; }
    set { _Portable =value; }
  }
  public string Email
  {
    get { return _Email; }
    set { _Email = value; }
  }
  public string Notes
  {
    get { return _Notes; }
    set { _Notes = value; }
  }
  public int ActuellementInscrit
  {
    get { return _ActuellementInscrit; }
    set { _ActuellementInscrit = value; }
  }
  public int Comite
  {
    get { return _Comite; }
    set { _Comite = value; }
  }
  public int NePasContacter
  {
    get { return _NePasContacter; }
    set { _NePasContacter = value; }
  }

}
