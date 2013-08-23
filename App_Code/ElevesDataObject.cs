using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

/// <summary>
/// Create, Read, Update, Delete methods for Eleves based on 
/// </summary>
[DataObject(true)]
public static class EleveDataObject
{

  [DataObjectMethod(DataObjectMethodType.Select)]
  public static List<Eleve> GetEleves() 
  {
    //MySqlCommand cmd = new MySqlCommand("ShowAllEleves",
    //               new MySqlConnection(GetConnectionString()));
    //cmd.CommandType = CommandType.StoredProcedure;

    string sqlstring = "SELECT * FROM eleves ORDER BY Nom";
    MySqlCommand cmd = new MySqlCommand(sqlstring, ContactsSQLHelper.GetConnection());

    cmd.Connection.Open();
    MySqlDataReader dr =
       cmd.ExecuteReader(CommandBehavior.CloseConnection);

    List<Eleve> EleveList = new List<Eleve>();
    while (dr.Read())
    {
      Eleve contact = new Eleve();
      contact.Eleve_ID = Convert.ToInt32(dr["ID"]);
      contact.ActuellementInscrit = Convert.ToInt32(dr["ActuellementInscrit"]);
      contact.ContactID = Convert.ToInt32(dr["ContactID"]);
      contact.AdresseAutreParent = Convert.ToString(dr["AdresseAutreParent"]);
      contact.ClasseActuelle = Convert.ToString(dr["ClasseActuelle"]);
      contact.DateNaissance = Convert.ToDateTime(dr["DateNaissance"]); //Convert.ToDateTime(dr["DateNaissance"]);
      contact.DetailsMedicaux = Convert.ToString(dr["DetailsMedicaux"]);
      contact.Docteur = Convert.ToString(dr["Docteur"]);
      contact.EmailAutreParent = Convert.ToString(dr["EmailAutreParent"]);
      contact.FixeAutreParent = Convert.ToString(dr["FixeAutreParent"]);
      contact.Gateaux = Convert.ToInt32(dr["Gateaux"]);
      contact.Nationalite = Convert.ToString(dr["Nationalite"]);
      contact.Nom = Convert.ToString(dr["Nom"]);
      contact.NomAutreParent = Convert.ToString(dr["NomAutreParent"]);
      contact.PbMedicaux = Convert.ToInt32(dr["PbMedicaux"]);
      contact.PhotosClasse = Convert.ToInt32(dr["PhotosClasse"]);
      contact.PhotosWeb = Convert.ToInt32(dr["PhotosWeb"]);
      contact.PortableAutreParent = Convert.ToString(dr["PortableAutreParent"]);
      contact.PreInscrit = Convert.ToInt32(dr["PreInscrit"]);
      contact.PremiereRentree = Convert.ToDateTime(dr["PremiereRentree"]);
      contact.Prenom = Convert.ToString(dr["Prenom"]);
      contact.PrenomAutreParent = Convert.ToString(dr["PrenomAutreParent"]);
      contact.RelationAutreParent = Convert.ToString(dr["RelationAutreParent"]);
      contact.Sexe = Convert.ToString(dr["Sexe"]);
 
      EleveList.Add(contact);
    }
    dr.Close();
    return EleveList;

  }

  [DataObjectMethod(DataObjectMethodType.Select)]
  public static List<Eleve> GetElevesByContactID(Int32 contactid)
  {

    string sqlstring = "SELECT * FROM eleves WHERE (ContactID = ?key) ORDER BY DateNaissance;";

    //MySqlCommand cmd = new MySqlCommand("ShowElevesByContactID",
    //               new MySqlConnection(GetConnectionString()));
    //cmd.CommandType = CommandType.StoredProcedure;

    MySqlCommand cmd = ContactsSQLHelper.GetCommand(sqlstring);
    cmd.Parameters.Add(new MySqlParameter("key", contactid));

    cmd.Connection.Open();
    MySqlDataReader dr =
       cmd.ExecuteReader(CommandBehavior.CloseConnection); //eleves error

    List<Eleve> EleveList = new List<Eleve>();
    while (dr.Read())
    {
      Eleve contact = new Eleve();
      contact.Eleve_ID = Convert.ToInt32(dr["ID"]);
      contact.ActuellementInscrit = Convert.ToInt32(dr["ActuellementInscrit"]);
      contact.ContactID = Convert.ToInt32(dr["ContactID"]);
      contact.AdresseAutreParent = Convert.ToString(dr["AdresseAutreParent"]);
      contact.ClasseActuelle = Convert.ToString(dr["ClasseActuelle"]);
      contact.DateNaissance = Convert.ToDateTime(dr["DateNaissance"]); //Convert.ToDateTime(dr["DateNaissance"]);
      contact.DetailsMedicaux = Convert.ToString(dr["DetailsMedicaux"]);
      contact.Docteur = Convert.ToString(dr["Docteur"]);
      contact.EmailAutreParent = Convert.ToString(dr["EmailAutreParent"]);
      contact.FixeAutreParent = Convert.ToString(dr["FixeAutreParent"]);
      contact.Gateaux = String.IsNullOrEmpty(dr["Gateaux"].ToString()) ? 0 : Convert.ToInt32(dr["Gateaux"]);
      contact.Nationalite = Convert.ToString(dr["Nationalite"]);
      contact.Nom = Convert.ToString(dr["Nom"]);
      contact.NomAutreParent = Convert.ToString(dr["NomAutreParent"]);
      contact.PbMedicaux = String.IsNullOrEmpty(dr["PbMedicaux"].ToString()) ? 0 : Convert.ToInt32(dr["PbMedicaux"]);
      contact.PhotosClasse = String.IsNullOrEmpty(dr["PhotosClasse"].ToString()) ? 0 : Convert.ToInt32(dr["PhotosClasse"]);
      contact.PhotosWeb = String.IsNullOrEmpty(dr["PhotosWeb"].ToString()) ? 0 : Convert.ToInt32(dr["PhotosWeb"]);
      contact.PortableAutreParent = Convert.ToString(dr["PortableAutreParent"]);
      contact.PreInscrit = Convert.ToInt32(dr["PreInscrit"]);
      contact.PremiereRentree = Convert.ToDateTime(dr["PremiereRentree"]);
      contact.Prenom = Convert.ToString(dr["Prenom"]);
      contact.PrenomAutreParent = Convert.ToString(dr["PrenomAutreParent"]);
      contact.RelationAutreParent = Convert.ToString(dr["RelationAutreParent"]);
      contact.Sexe = Convert.ToString(dr["Sexe"]);

      EleveList.Add(contact);
    }
    dr.Close();
    return EleveList;

  }

  [DataObjectMethod(DataObjectMethodType.Insert)]
  public static Int32 InsertEleve(Eleve contact)
  {
    string sqlstring = "INSERT INTO `eleves` (`ContactID`, `Nom`,`Prenom`,`DateNaissance`,`Nationalite`,`PremiereRentree`,`ClasseActuelle`,`ActuellementInscrit`,`PreInscrit`,`Sexe`," +
      "`RelationAutreParent`, `NomAutreParent`, `PrenomAutreParent`, `AdresseAutreParent`, `FixeAutreParent`, `PortableAutreParent`, `EmailAutreParent`, `Docteur`, " +
      "`PhotosClasse`, `PhotosWeb`, `Gateaux`, `PbMedicaux`, `DetailsMedicaux`) ";

    sqlstring += " VALUES (?vContactID, ?vNom,?vPrenom,?vDateNaissance,?vNationalite,?vPremiereRentree,?vClasseActuelle,?vActuellementInscrit,?vPreInscrit,?vSexe," +
      "?vRelationAutreParent, ?vNomAutreParent, ?vPrenomAutreParent, ?vAdresseAutreParent, ?vFixeAutreParent, ?vPortableAutreParent, ?vEmailAutreParent, ?vDocteur, " +
      "?vPhotosClasse, ?vPhotosWeb, ?vGateaux, ?vPbMedicaux, ?vDetailsMedicaux)";

    
    //using(MySqlCommand cmd = new MySqlCommand("InsertEleve", new MySqlConnection(GetConnectionString())))
    using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(sqlstring))
    {
      //cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.Add(new MySqlParameter("vContactID", contact.ContactID));
      cmd.Parameters.Add(new MySqlParameter("vNom", contact.Nom));
      cmd.Parameters.Add(new MySqlParameter("vPrenom", contact.Prenom));
      cmd.Parameters.Add(new MySqlParameter("vDateNaissance", contact.DateNaissance));
      cmd.Parameters.Add(new MySqlParameter("vNationalite", contact.Nationalite));
      cmd.Parameters.Add(new MySqlParameter("vPremiereRentree", contact.PremiereRentree));
      cmd.Parameters.Add(new MySqlParameter("vClasseActuelle", contact.ClasseActuelle));
      cmd.Parameters.Add(new MySqlParameter("vActuellementInscrit", contact.ActuellementInscrit));
      cmd.Parameters.Add(new MySqlParameter("vPreInscrit", contact.PreInscrit));
      cmd.Parameters.Add(new MySqlParameter("vSexe", contact.Sexe));
      cmd.Parameters.Add(new MySqlParameter("vRelationAutreParent", contact.RelationAutreParent));
      cmd.Parameters.Add(new MySqlParameter("vNomAutreParent", contact.NomAutreParent));
      cmd.Parameters.Add(new MySqlParameter("vPrenomAutreParent", contact.PrenomAutreParent));
      cmd.Parameters.Add(new MySqlParameter("vAdresseAutreParent", contact.AdresseAutreParent));
      cmd.Parameters.Add(new MySqlParameter("vFixeAutreParent", contact.FixeAutreParent));
      cmd.Parameters.Add(new MySqlParameter("vPortableAutreParent", contact.PortableAutreParent));
      cmd.Parameters.Add(new MySqlParameter("vEmailAutreParent", contact.EmailAutreParent));
      cmd.Parameters.Add(new MySqlParameter("vDocteur", contact.Docteur));
      cmd.Parameters.Add(new MySqlParameter("vPhotosClasse", contact.PhotosClasse));
      cmd.Parameters.Add(new MySqlParameter("vPhotosWeb", contact.PhotosWeb));
      cmd.Parameters.Add(new MySqlParameter("vGateaux", contact.Gateaux));
      cmd.Parameters.Add(new MySqlParameter("vPbMedicaux", contact.PbMedicaux));
      cmd.Parameters.Add(new MySqlParameter("vDetailsMedicaux", contact.DetailsMedicaux));
      
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
  public static int UpdateEleve(Eleve contact)
  {
    string sqlstring = "UPDATE `eleves` SET `ContactID`=?vContactID, `Nom`=?vNom,`Prenom`=?vPrenom,`DateNaissance`=?vDateNaissance,`Nationalite`=?vNationalite,`PremiereRentree`=?vPremiereRentree,`ClasseActuelle`=?vClasseActuelle,`ActuellementInscrit`=?vActuellementInscrit,`PreInscrit`=?vPreInscrit,`Sexe`=?vSexe, " +
      "RelationAutreParent=?vRelationAutreParent, NomAutreParent=?vNomAutreParent, PrenomAutreParent=?vPrenomAutreParent, AdresseAutreParent=?vAdresseAutreParent, FixeAutreParent=?vFixeAutreParent, PortableAutreParent=?vPortableAutreParent, EmailAutreParent=?vEmailAutreParent, Docteur=?vDocteur, " +
      "PhotosClasse=?vPhotosClasse, PhotosWeb=?vPhotosWeb, Gateaux=?vGateaux, PbMedicaux=?vPbMedicaux, DetailsMedicaux=?vDetailsMedicaux WHERE id=?key";

    using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(sqlstring))
    {

      cmd.Parameters.Add(new MySqlParameter("key", contact.Eleve_ID));
      cmd.Parameters.Add(new MySqlParameter("vContactID", contact.ContactID));
      cmd.Parameters.Add(new MySqlParameter("vNom", contact.Nom));
      cmd.Parameters.Add(new MySqlParameter("vPrenom", contact.Prenom));
      cmd.Parameters.Add(new MySqlParameter("vDateNaissance", (String.IsNullOrEmpty(contact.DateNaissance.ToString()) ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(contact.DateNaissance))));
      cmd.Parameters.Add(new MySqlParameter("vNationalite", contact.Nationalite));
      cmd.Parameters.Add(new MySqlParameter("vPremiereRentree", contact.PremiereRentree));
      //cmd.Parameters.Add(new MySqlParameter("vNiveau", contact.Niveau));
      cmd.Parameters.Add(new MySqlParameter("vClasseActuelle", contact.ClasseActuelle));
      cmd.Parameters.Add(new MySqlParameter("vActuellementInscrit", Convert.ToInt32(contact.ActuellementInscrit)));
      cmd.Parameters.Add(new MySqlParameter("vPreInscrit", Convert.ToInt32(contact.PreInscrit)));
      cmd.Parameters.Add(new MySqlParameter("vSexe", contact.Sexe));
      cmd.Parameters.Add(new MySqlParameter("vRelationAutreParent", contact.RelationAutreParent));
      cmd.Parameters.Add(new MySqlParameter("vNomAutreParent", contact.NomAutreParent));
      cmd.Parameters.Add(new MySqlParameter("vPrenomAutreParent", contact.PrenomAutreParent));
      cmd.Parameters.Add(new MySqlParameter("vAdresseAutreParent", contact.AdresseAutreParent));
      cmd.Parameters.Add(new MySqlParameter("vFixeAutreParent", contact.FixeAutreParent));
      cmd.Parameters.Add(new MySqlParameter("vPortableAutreParent", contact.PortableAutreParent));
      cmd.Parameters.Add(new MySqlParameter("vEmailAutreParent", contact.EmailAutreParent));
      cmd.Parameters.Add(new MySqlParameter("vDocteur", contact.Docteur));
      cmd.Parameters.Add(new MySqlParameter("vPhotosClasse", Convert.ToInt32(contact.PhotosClasse)));
      cmd.Parameters.Add(new MySqlParameter("vPhotosWeb", Convert.ToInt32(contact.PhotosWeb)));
      cmd.Parameters.Add(new MySqlParameter("vGateaux", Convert.ToInt32(contact.Gateaux)));
      cmd.Parameters.Add(new MySqlParameter("vPbMedicaux", Convert.ToInt32(contact.PbMedicaux)));
      cmd.Parameters.Add(new MySqlParameter("vDetailsMedicaux", contact.DetailsMedicaux));

      cmd.Connection.Open();
      return cmd.ExecuteNonQuery();
    }
  }

  [DataObjectMethod(DataObjectMethodType.Delete)]
  public static int DeleteEleve(Eleve contact)
  {
    string sqlstring = "DELETE FROM `eleves` WHERE ID=?key";

    using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(sqlstring))
    {
      cmd.Parameters.Add(new MySqlParameter("key", contact.Eleve_ID));
      cmd.Connection.Open();
      int i = cmd.ExecuteNonQuery();
      cmd.Connection.Close();
      return i;
    }
  }

  // Other methods
  [DataObjectMethod(DataObjectMethodType.Update)]
  public static int UpdateClasse(String oldClass, String newClass)
  {
    string sqlstring = "UPDATE `eleves` SET `ClasseActuelle`=?newClass WHERE ClasseActuelle=?oldClass AND ActuellementInscrit=1";

    using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(sqlstring))
    {

      cmd.Parameters.Add(new MySqlParameter("newClass", newClass));
      cmd.Parameters.Add(new MySqlParameter("oldClass", oldClass));

      cmd.Connection.Open();
      return cmd.ExecuteNonQuery();
    }
  }


}

public class Eleve
{
  int _Eleve_ID;
  int _ContactID;
  string _Nom;
  string _Prenom;
  DateTime _DateNaissance;
  string _Nationalite;
  DateTime _PremiereRentree;
  //string Niveau;
  string _ClasseActuelle;
  int _ActuellementInscrit;
  int _PreInscrit;
  string _Sexe;
  string _RelationAutreParent;
  string _NomAutreParent;
  string _PrenomAutreParent;
  string _AdresseAutreParent;
  string _FixeAutreParent;
  string _PortableAutreParent;
  string _EmailAutreParent;
  string _Docteur;
  int _PhotosClasse;
  int _PhotosWeb;
  int _Gateaux;
  int _PbMedicaux;
  string _DetailsMedicaux;

  public Eleve()
  {
  }
  public Eleve(int Eleve_ID, int ContactID, string Nom, string Prenom, DateTime DateNaissance, string Nationalite,
  DateTime PremiereRentree,
    //string Niveau;
  string ClasseActuelle, int ActuellementInscrit, int PreInscrit, string _Sexe, string RelationAutreParent,
  string NomAutreParent, string PrenomAutreParent, string AdresseAutreParent, string FixeAutreParent,
  string PortableAutreParent, string EmailAutreParent, string Docteur, int PhotosClasse, int PhotosWeb,
  int Gateaux, int PbMedicaux, string DetailsMedicaux)
  {
    this.Eleve_ID = Eleve_ID;
    this.ContactID = ContactID;
    this.Nom = Nom;
    this.Prenom = Prenom;
    this.DateNaissance = DateNaissance;
    this.Nationalite = Nationalite;
    this.PremiereRentree = PremiereRentree;
    //string Niveau;
    this.ClasseActuelle = ClasseActuelle;
    this.ActuellementInscrit = ActuellementInscrit;
    this.PreInscrit = PreInscrit;
    this.Sexe = Sexe;
    this.RelationAutreParent = RelationAutreParent;
    this.NomAutreParent = NomAutreParent;
    this.PrenomAutreParent = PrenomAutreParent;
    this.AdresseAutreParent = AdresseAutreParent;
    this.FixeAutreParent = FixeAutreParent;
    this.PortableAutreParent = PortableAutreParent;
    this.EmailAutreParent = EmailAutreParent;
    this.Docteur = Docteur;
    this.PhotosClasse = PhotosClasse;
    this.PhotosWeb = PhotosWeb;
    this.Gateaux = Gateaux;
    this.PbMedicaux = PbMedicaux;
    this.DetailsMedicaux = DetailsMedicaux;
  }

  public int Eleve_ID
  {
    get { return _Eleve_ID; }
    set { _Eleve_ID = value; }
  }

  public int ContactID
  {
    get { return _ContactID; }
    set { _ContactID = value; }
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
  public DateTime DateNaissance
  {
    get { return _DateNaissance; }
    set { _DateNaissance = value; }
  }
  public string Nationalite
  {
    get { return _Nationalite; }
    set { _Nationalite = value; }
  }
  public DateTime PremiereRentree
  {
    get { return _PremiereRentree; }
    set { _PremiereRentree = value; }
  }
  //string Niveau;
  public string ClasseActuelle
  {
    get { return _ClasseActuelle; }
    set { _ClasseActuelle = value; }
  }
  public int ActuellementInscrit
  {
    get { return _ActuellementInscrit; }
    set { _ActuellementInscrit = value; }
  }
  public int PreInscrit
  {
    get { return _PreInscrit; }
    set { _PreInscrit = value; }
  }
  public string Sexe
  {
    get { return _Sexe; }
    set { _Sexe = value; }
  }
  public string RelationAutreParent
  {
    get { return _RelationAutreParent; }
    set { _RelationAutreParent = value; }
  }
  public string NomAutreParent
  {
    get { return _NomAutreParent; }
    set { _NomAutreParent = value; }
  }
  public string PrenomAutreParent
  {
    get { return _PrenomAutreParent; }
    set { _PrenomAutreParent = value; }
  }
  public string AdresseAutreParent
  {
    get { return _AdresseAutreParent; }
    set { _AdresseAutreParent = value; }
  }
  public string FixeAutreParent
  {
    get { return _FixeAutreParent; }
    set { _FixeAutreParent = value; }
  }
  public string PortableAutreParent
  {
    get { return _PortableAutreParent; }
    set { _PortableAutreParent = value; }
  }
  public string EmailAutreParent
  {
    get { return _EmailAutreParent; }
    set { _EmailAutreParent = value; }
  }
  public string Docteur
  {
    get { return _Docteur; }
    set { _Docteur = value; }
  }
  public int PhotosClasse
  {
    get { return _PhotosClasse; }
    set { _PhotosClasse = value; }
  }
  public int PhotosWeb
  {
    get { return _PhotosWeb; }
    set { _PhotosWeb = value; }
  }
  public int Gateaux
  {
    get { return _Gateaux; }
    set { _Gateaux = value; }
  }
  public int PbMedicaux
  {
    get { return _PbMedicaux; }
    set { _PbMedicaux = value; }
  }
  public string DetailsMedicaux
  {
    get { return _DetailsMedicaux; }
    set { _DetailsMedicaux = value; }
  }



}
