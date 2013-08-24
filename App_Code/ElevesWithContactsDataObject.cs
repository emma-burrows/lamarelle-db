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
public static class ElevesWithContactsDataObject
{

  [DataObjectMethod(DataObjectMethodType.Select)]
  public static List<EleveWithContacts> GetElevesWithContacts(string SortColumns, string Filter) 
  {
    //MySqlCommand cmd = new MySqlCommand("ShowAllEleves",
    //               new MySqlConnection(GetConnectionString()));
    //cmd.CommandType = CommandType.StoredProcedure;
    string verifiedsort = VerifySortColumns(SortColumns);

    string filter = Filter;

    string sqlstring = "SELECT *, ct_contacts.nom AS NomContact, ct_contacts.prenom AS PrenomContact, " +
      "ct_contacts.ActuellementInscrit AS ActuellementInscritContact, classes.Niveau " +
      "FROM eleves LEFT JOIN ct_contacts ON eleves.contactid = ct_contacts.id LEFT JOIN classes ON eleves.ClasseActuelle = classes.Nom";

    if (!String.IsNullOrEmpty(filter))
    {
      sqlstring += " WHERE " + filter;
    }

    if (verifiedsort.Trim() == "")
      sqlstring += " ORDER BY eleves.Nom";
    else
      sqlstring += " ORDER BY " + verifiedsort;


    using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(sqlstring))
    {

      cmd.Connection.Open();
      MySqlDataReader dr =
         cmd.ExecuteReader(CommandBehavior.CloseConnection);

      List<EleveWithContacts> EleveList = new List<EleveWithContacts>();
      while (dr.Read())
      {
        EleveWithContacts contact = new EleveWithContacts();
        contact.Eleve_ID = Convert.ToInt32(dr["ID"]);
        contact.ActuellementInscrit = MakeBool(dr["ActuellementInscrit"].ToString());
        contact.ContactID = Convert.ToInt32(dr["ContactID"]);
        contact.AdresseAutreParent = Convert.ToString(dr["AdresseAutreParent"]);
        contact.ClasseActuelle = Convert.ToString(dr["ClasseActuelle"]);
        contact.DateNaissance = String.IsNullOrEmpty(dr["DateNaissance"].ToString()) ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["DateNaissance"]); //Convert.ToDateTime(dr["DateNaissance"]);
        contact.DetailsMedicaux = Convert.ToString(dr["DetailsMedicaux"]);
        contact.Docteur = Convert.ToString(dr["Docteur"]);
        contact.EmailAutreParent = Convert.ToString(dr["EmailAutreParent"]);
        contact.FixeAutreParent = Convert.ToString(dr["FixeAutreParent"]);
        contact.Gateaux = MakeBool(dr["Gateaux"].ToString());
        contact.Nationalite = Convert.ToString(dr["Nationalite"]);
        contact.Nom = Convert.ToString(dr["Nom"]);
        contact.NomAutreParent = Convert.ToString(dr["NomAutreParent"]);
        contact.PbMedicaux = MakeBool(dr["PbMedicaux"].ToString());
        contact.PhotosClasse = MakeBool(dr["PhotosClasse"].ToString());
        contact.PhotosWeb = MakeBool(dr["PhotosWeb"].ToString());
        contact.PortableAutreParent = Convert.ToString(dr["PortableAutreParent"]);
        contact.PreInscrit = MakeBool(dr["PreInscrit"].ToString());
        contact.PremiereRentree = String.IsNullOrEmpty(dr["PremiereRentree"].ToString()) ? Convert.ToDateTime("01/01/0001") : Convert.ToDateTime(dr["PremiereRentree"]);
        contact.Prenom = Convert.ToString(dr["Prenom"]);
        contact.PrenomAutreParent = Convert.ToString(dr["PrenomAutreParent"]);
        contact.RelationAutreParent = Convert.ToString(dr["RelationAutreParent"]);
        contact.Sexe = Convert.ToString(dr["Sexe"]);

        // Information from Contacts table
        contact.ActuellementInscritContact = MakeBool(dr["ActuellementInscritContact"].ToString());
        contact.Adresse1 = Convert.ToString(dr["Adresse1"]);
        contact.Adresse2 = Convert.ToString(dr["Adresse2"]);
        contact.Adresse3 = Convert.ToString(dr["Adresse3"]);
        contact.CodePostal = Convert.ToString(dr["Code Postal"]);
        contact.Comite = MakeBool(dr["Comite"].ToString());
        contact.Email = Convert.ToString(dr["Email"]);
        contact.Fixe = Convert.ToString(dr["Fixe"]);
        contact.NePasContacter = MakeBool(dr["NePasContacter"].ToString());
        contact.NomContact = Convert.ToString(dr["NomContact"]);
        contact.Notes = Convert.ToString(dr["Notes"]);
        contact.Portable = Convert.ToString(dr["Portable"]);
        contact.PrenomContact = Convert.ToString(dr["PrenomContact"]);
        contact.Ville = Convert.ToString(dr["Ville"]);

        EleveList.Add(contact);
      }
      dr.Close();
      return EleveList;
    }
  }


  [DataObjectMethod(DataObjectMethodType.Update)]
  public static void UpdateEleveWithContacts(EleveWithContacts contact)
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
      cmd.Parameters.Add(new MySqlParameter("vDateNaissance", (String.IsNullOrEmpty(contact.DateNaissance.ToString()) ? Convert.ToDateTime("01/01/0001") : contact.DateNaissance)));
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
      cmd.ExecuteNonQuery();
    }

    sqlstring = "UPDATE ct_contacts SET `Nom`=?vNom, `Prenom`=?vPrenom, `Adresse1`=?vAdresse1, `Adresse2`=?vAdresse2, `Adresse3`=?vAdresse3, `Ville`=?vVille, `Code Postal`=?vCodePostal, `Fixe`=?vFixe, `Portable`=?vPortable, `Email`=?vEmail, `Notes`=?vNotes, `ActuellementInscrit`=?vActuellementInscrit, `Comite`=?vComite, `NePasContacter`=?vNePasContacter WHERE ID = ?vContactID";

    using (MySqlCommand cmd = ContactsSQLHelper.GetCommand(sqlstring))
    {
      //cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.Add(new MySqlParameter("vContactID", contact.ContactID));
      cmd.Parameters.Add(new MySqlParameter("vNom", contact.NomContact));
      cmd.Parameters.Add(new MySqlParameter("vPrenom", contact.PrenomContact));
      cmd.Parameters.Add(new MySqlParameter("vAdresse1", contact.Adresse1));
      cmd.Parameters.Add(new MySqlParameter("vAdresse2", contact.Adresse2));
      cmd.Parameters.Add(new MySqlParameter("vAdresse3", contact.Adresse3));
      cmd.Parameters.Add(new MySqlParameter("vVille", contact.Ville));
      cmd.Parameters.Add(new MySqlParameter("vCodePostal", contact.CodePostal));
      cmd.Parameters.Add(new MySqlParameter("vFixe", contact.Fixe));
      cmd.Parameters.Add(new MySqlParameter("vPortable", contact.Portable));
      cmd.Parameters.Add(new MySqlParameter("vEmail", contact.Email));
      cmd.Parameters.Add(new MySqlParameter("vNotes", contact.Notes));
      cmd.Parameters.Add(new MySqlParameter("vActuellementInscrit", Convert.ToInt32(contact.ActuellementInscritContact)));
      cmd.Parameters.Add(new MySqlParameter("vComite", Convert.ToInt32(contact.Comite)));
      cmd.Parameters.Add(new MySqlParameter("vNePasContacter", Convert.ToInt32(contact.NePasContacter)));

      cmd.Connection.Open();
      cmd.ExecuteNonQuery();
    }

  }

  private static bool MakeBool(string checkbox)
  {
    bool result = false;
    if (Boolean.TryParse(checkbox, out result))
    {
    }
    else
    {
      result = String.IsNullOrEmpty(checkbox) ? false : Convert.ToBoolean(Convert.ToInt32(checkbox));
    }
    return result;
  }

  private static string VerifySortColumns(string sortColumns)
  {
    string fixedsort = "";
    string order = "";
    if (sortColumns.ToLowerInvariant().EndsWith(" desc"))
    {
      sortColumns = sortColumns.Substring(0, sortColumns.Length - 5);
      order = " DESC";
    }

    string[] columnNames = sortColumns.Split(',');

    string comma = "";
    foreach (string columnName in columnNames)
    {
      //if (columnName == ElevesHelper.ClassesSort)
      //{
      //  fixedsort += comma + columnName.Trim();
      //}
      //else
      //{
        switch (columnName.Trim().ToLowerInvariant())
        {
          // Unambiguous fields
          case "eleve_id":
          case "contactid":
          case "datenaissance":
          case "nationalite":
          case "premiererentree":
          case "classeactuelle":
          case "preinscrit":
          case "sexe":
          case "relationautreparent":
          case "nomautreparent":
          case "prenomautreparent":
          case "adresseautreparent":
          case "fixeautreparent":
          case "portableautreparent":
          case "emailautreparent":
          case "docteur":
          case "photosclasse":
          case "photosweb":
          case "gateaux":
          case "pbmedicaux":
          case "detailsmedicaux":
          case "adresse1":
          case "adresse2":
          case "adresse3":
          case "ville":
          case "codepostal":
          case "fixe":
          case "portable":
          case "email":
          case "notes":
          case "comite":
          case "nepascontacter":
          case "niveau":
            fixedsort += comma + columnName.Trim();
            break;

          // Potentially ambiguous fields from eleves
          case "nom":
          case "prenom":
          case "actuellementinscrit":
            fixedsort += comma + "eleves." + columnName.Trim();
            break;

          // Potentially ambiguous fields from contacts
          case "nomcontact":
          case "prenomcontact":
          case "actuellementinscritcontact":
            fixedsort += comma + "ct_contacts." + columnName.Trim().Substring(0, columnName.Trim().Length - 7);
            break;

          case "":
            break;
          default:
            throw new ArgumentException("SortColumns contains an invalid column name. (" + sortColumns.ToString() + ")");
        }
      //}
      comma = ",";
    }
    fixedsort += order;

    return fixedsort;
  }

}

public class EleveWithContacts
{
  int _Eleve_ID;
  int _ContactID;
  string _Nom;
  string _Prenom;
  DateTime _DateNaissance;
  string _Nationalite;
  DateTime _PremiereRentree;
  bool _ActuellementInscrit;
  bool _PreInscrit;
  string _Sexe;
  string _RelationAutreParent;
  string _NomAutreParent;
  string _PrenomAutreParent;
  string _AdresseAutreParent;
  string _FixeAutreParent;
  string _PortableAutreParent;
  string _EmailAutreParent;
  string _Docteur;
  bool _PhotosClasse;
  bool _PhotosWeb;
  bool _Gateaux;
  bool _PbMedicaux;
  string _DetailsMedicaux;


  string _NomContact;
  string _PrenomContact;
  string _Adresse1;
  string _Adresse2;
  string _Adresse3;
  string _Ville;
  string _CodePostal;
  string _Fixe;
  string _Portable;
  string _Email;
  string _Notes;
  bool _ActuellementInscritContact;
  bool _Comite;
  bool _NePasContacter;

  string _Niveau;
  string _ClasseActuelle;

  public EleveWithContacts()
  {
  }
  //public Eleve(int Eleve_ID, int ContactID, string Nom, string Prenom, DateTime DateNaissance, string Nationalite,
  //DateTime PremiereRentree,
  //  //string Niveau;
  //string ClasseActuelle, int ActuellementInscrit, int PreInscrit, string _Sexe, string RelationAutreParent,
  //string NomAutreParent, string PrenomAutreParent, string AdresseAutreParent, string FixeAutreParent,
  //string PortableAutreParent, string EmailAutreParent, string Docteur, int PhotosClasse, int PhotosWeb,
  //int Gateaux, int PbMedicaux, string DetailsMedicaux)
  //{
  //  this.Eleve_ID = Eleve_ID;
  //  this.ContactID = ContactID;
  //  this.Nom = Nom;
  //  this.Prenom = Prenom;
  //  this.DateNaissance = DateNaissance;
  //  this.Nationalite = Nationalite;
  //  this.PremiereRentree = PremiereRentree;
  //  //string Niveau;
  //  this.ClasseActuelle = ClasseActuelle;
  //  this.ActuellementInscrit = ActuellementInscrit;
  //  this.PreInscrit = PreInscrit;
  //  this.Sexe = Sexe;
  //  this.RelationAutreParent = RelationAutreParent;
  //  this.NomAutreParent = NomAutreParent;
  //  this.PrenomAutreParent = PrenomAutreParent;
  //  this.AdresseAutreParent = AdresseAutreParent;
  //  this.FixeAutreParent = FixeAutreParent;
  //  this.PortableAutreParent = PortableAutreParent;
  //  this.EmailAutreParent = EmailAutreParent;
  //  this.Docteur = Docteur;
  //  this.PhotosClasse = PhotosClasse;
  //  this.PhotosWeb = PhotosWeb;
  //  this.Gateaux = Gateaux;
  //  this.PbMedicaux = PbMedicaux;
  //  this.DetailsMedicaux = DetailsMedicaux;
  //}

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
  public bool ActuellementInscrit
  {
    get { return _ActuellementInscrit; }
    set { _ActuellementInscrit = value; }
  }
  public bool PreInscrit
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
  public bool PhotosClasse
  {
    get { return _PhotosClasse; }
    set { _PhotosClasse = value; }
  }
  public bool PhotosWeb
  {
    get { return _PhotosWeb; }
    set { _PhotosWeb = value; }
  }
  public bool Gateaux
  {
    get { return _Gateaux; }
    set { _Gateaux = value; }
  }
  public bool PbMedicaux
  {
    get { return _PbMedicaux; }
    set { _PbMedicaux = value; }
  }
  public string DetailsMedicaux
  {
    get { return _DetailsMedicaux; }
    set { _DetailsMedicaux = value; }
  }

  //Contacts
  public string NomContact
  {
    get { return _NomContact; }
    set { _NomContact = value; }
  }
  public string PrenomContact
  {
    get { return _PrenomContact; }
    set { _PrenomContact = value; }
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
    get { return _Adresse3; }
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
    set { _Portable = value; }
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
  public bool ActuellementInscritContact
  {
    get { return _ActuellementInscritContact; }
    set { _ActuellementInscritContact = value; }
  }
  public bool Comite
  {
    get { return _Comite; }
    set { _Comite = value; }
  }
  public bool NePasContacter
  {
    get { return _NePasContacter; }
    set { _NePasContacter = value; }
  }

  // Classes
  public string ClasseActuelle
  {
    get { return _ClasseActuelle; }
    set { _ClasseActuelle = value; }
  }
  // Niveau is read-only as it belongs to the Classes table 
  // and is only used for sorting
  public string Niveau
  {
    get { return _Niveau; }
  }
}
