using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Retrieves contacts information from the database
/// </summary>
[WebService(Namespace = "http://lamarelle.org.uk/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ContactsService : System.Web.Services.WebService 
{

  public ContactsService () {
  }

  [WebMethod]
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  public string HelloWorld() 
  {
      return "Hello World";
  }

  [WebMethod]
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  public Contact ContactById(int id)
  {
    return ContactDataObject.GetContactsById(id, 0)[0];
  }

  [WebMethod]
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  public List<Eleve> ChildrenByContactId(int id)
  {
    return EleveDataObject.GetElevesByContactID(id);
  } 
}
