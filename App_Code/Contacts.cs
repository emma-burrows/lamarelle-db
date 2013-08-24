using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

/// <summary>
/// Web service to retrieve contact information from external services.
/// </summary>
[ScriptService]
[WebService(Namespace = "http://lamarelle.org.uk/")]
public class ContactService : System.Web.Services.WebService 
{

  public ContactService() 
  {
  }

  [WebMethod]
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  public string HelloWorld() 
  {
      return "Hello World";
  }

  [WebMethod]
  [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
  public Contact GetUser(string id)
  {
    Contact contact = new Contact(1, "Holmes", "Sherlock", "221B Baker Street", "", "", "London", "NW1 6XE", "", "", "sh@thescienceofdeduction.com", "Fictional", 1, 1, 1);

    return contact;
  }
}

