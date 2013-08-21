using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using AjaxControlToolkit;

/// <summary>
/// Code behind to handle Family 
/// </summary>
public partial class Admin_FamilyForm : System.Web.UI.Page
{
  private int ContactID = 0;
  private int Status = -1;
  private bool userIsAdmin;

  protected void Page_Load(object sender, EventArgs e)
  {
    userIsAdmin = ( LoginHelper.UserRole(Page.User.Identity.Name) == "admin" );

    if (!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["c"]) && !Page.IsPostBack)
    {
      ContactID = Convert.ToInt32(HttpContext.Current.Request.QueryString["c"]);
      ViewState.Add("status", Status);
      ViewState.Add("contactid", ContactID);
    }
    else
    {
      if (!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["s"]) && !Page.IsPostBack)
      {
        Status = Convert.ToInt32(HttpContext.Current.Request.QueryString["s"]);
        ViewState.Add("status", Status);
        ViewState.Add("contactid", ContactID);
      }

    }

    // Get results from FamilySearch user control
    FamilySearch1.sendMessageToThePage += delegate(string radiovalue, string message)
    {
      ListView1.Visible = true;

      if (radiovalue == "Family")
      {
        fvContacts.HeaderRow.Visible = true;

        fvContacts.ChangeMode(FormViewMode.ReadOnly);
        Status = -1;
        ContactID = Int32.Parse(message.Split('~')[0]);
        SecretFamilyName.Text = "Famille: " + message.Split('~')[1].ToUpper();
        ViewState.Add("contactid", ContactID);
        fvContacts.DataBind();
      }
      else
      {

        // Reset contact id so results aren't filtered
        ContactID = 0;
        //Status = -1;

        // TODO: handle specific cases
        switch (radiovalue)
        {
          case "All":
            fvContacts.ChangeMode(FormViewMode.ReadOnly);
            SecretFamilyName.Text = "Toutes les familles";
            Status = -1;
            break;

          case "Enrolled":
            fvContacts.ChangeMode(FormViewMode.ReadOnly);
            SecretFamilyName.Text = "Familles Inscrites";
            Status = 1;
            break;

          case "NotEnrolled":
            fvContacts.ChangeMode(FormViewMode.ReadOnly);
            SecretFamilyName.Text = "Familles non-inscrites";
            Status = 0; 
            break;
        }

        fvContacts.HeaderRow.Visible = false;

      }
      ViewState.Add("status", Status);
      ViewState.Add("contactid", ContactID);
      fvContacts.DataBind();

    };

    // Set page CSS class to highlight the right menu
    HtmlGenericControl body = (HtmlGenericControl)Master.FindControl("master");
    body.Attributes.Add("class", "admin");

  }


  #region Contacts FormView and ObjectDataSource
  
  //Read in the page contact id from ViewState when updating
  protected void odsContacts_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
  {
      e.InputParameters["ContactID"] = ViewState["contactid"]!=null ? (Int32)ViewState["contactid"] : 0;
      e.InputParameters["Status"] = ViewState["status"]!=null ? (Int32)ViewState["status"] : -1;
  }

  // Set the page contact id to the newly insert record's id, and bind the datasource and 
  // FamilySearch user control
  protected void odsContacts_Inserted(object sender, ObjectDataSourceStatusEventArgs e)
  {
    int retvalue = Convert.ToInt32(e.ReturnValue);
    ContactID = retvalue;
    ViewState.Add("contactid", ContactID);
    odsContacts.DataBind();
    FamilySearch1.DataBind();
    ListView1.Visible = true;
  }

  // Set the Insert template to be the same as the Edit template to cut down code
  protected void fvContacts_Init(object sender, EventArgs e)
  {
    fvContacts.InsertItemTemplate = fvContacts.EditItemTemplate;
  }

  protected void fvContacts_PreRender(object sender, EventArgs e)
  {
    if (fvContacts.TopPagerRow != null)
    {
      // Update the title of the view
      //( (Label)fvContacts.TopPagerRow.FindControl("Familyname") ).Text = SecretFamilyName.Text;

      // Add page count information
      ( (Label)fvContacts.TopPagerRow.FindControl("CurrentPage") ).Text = ( fvContacts.PageIndex + 1 ).ToString();
      ( (Label)fvContacts.TopPagerRow.FindControl("TotalPages") ).Text = fvContacts.PageCount.ToString();

      // Hide first/previous and last/next button on the first and last pages.
      ( (Button)fvContacts.TopPagerRow.FindControl("FirstButton") ).Visible = fvContacts.PageIndex != 0;
      ( (Button)fvContacts.TopPagerRow.FindControl("PrevButton") ).Visible = fvContacts.PageIndex != 0;
      ( (Button)fvContacts.TopPagerRow.FindControl("LastButton") ).Visible = fvContacts.PageCount != ( fvContacts.PageIndex + 1 );
      ( (Button)fvContacts.TopPagerRow.FindControl("NextButton") ).Visible = fvContacts.PageCount != ( fvContacts.PageIndex + 1 );

      // Hide header template if paging will be happening as the new button is on both
      if (fvContacts.PageCount > 1)
      {
        fvContacts.HeaderRow.Visible = false;
      }
      else
      {
        fvContacts.HeaderRow.Visible = true;
      }

    }

  }

  // Hide Eleves view when inserting a new contact
  protected void fvContacts_ModeChanging(object sender, FormViewModeEventArgs e)
  {
    if (e.NewMode == FormViewMode.Insert)
    {
      MessageLabel.Text = "";
      ListView1.Visible = false;
    }
    else
    {
      ListView1.Visible = true;
    }
  }
  
  // Initialise some non-null values when inserting a new record and
  // hide the Edit or Insert buttons as required
  protected void fvContacts_DataBound(object sender, EventArgs e)
  {
    Panel pnInsertPanel = fvContacts.FindControl("InsertButtons") as Panel;
    Panel pnEditPanel = fvContacts.FindControl("EditButtons") as Panel;

    if (LoginHelper.UserRole(Page.User.Identity.Name) == "admin")
    {
      if (fvContacts.CurrentMode == FormViewMode.ReadOnly)
      {
        fvContacts.FindControl("EditButton").Visible = true;
        fvContacts.HeaderRow.FindControl("NewHeaderButton").Visible = true;
      }
    }

    if (fvContacts.CurrentMode == FormViewMode.Insert)
    {
      pnInsertPanel.Visible = true;
      pnEditPanel.Visible = false;

    }
    else if (fvContacts.CurrentMode == FormViewMode.Edit)
    {
      pnInsertPanel.Visible = false;
      pnEditPanel.Visible = true;
    }

    ListView1.EditIndex = -1;
  }



  protected void fvContacts_ItemUpdating(object sender, FormViewUpdateEventArgs e)
  {
    CheckBox cbActuellementInscrit = (CheckBox)fvContacts.FindControl("ActuellementInscritCheckBox");
    e.NewValues["ActuellementInscrit"] = cbActuellementInscrit.Checked ? 1 : 0;

    CheckBox cbComite = (CheckBox)fvContacts.FindControl("ComiteCheckBox");
    e.NewValues["Comite"] = cbComite.Checked ? 1 : 0;

    CheckBox cbNePasContacter = (CheckBox)fvContacts.FindControl("NePasContacterCheckBox");
    e.NewValues["NePasContacter"] = cbNePasContacter.Checked ? 1 : 0;

  }

  protected void fvContacts_ItemInserting(object sender, FormViewInsertEventArgs e)
  {
    CheckBox cbActuellementInscrit = (CheckBox)fvContacts.FindControl("ActuellementInscritCheckBox");
    e.Values["ActuellementInscrit"] = cbActuellementInscrit.Checked ? 1 : 0;

    CheckBox cbComite = (CheckBox)fvContacts.FindControl("ComiteCheckBox");
    e.Values["Comite"] = cbComite.Checked ? 1 : 0;

    CheckBox cbNePasContacter = (CheckBox)fvContacts.FindControl("NePasContacterCheckBox");
    e.Values["NePasContacter"] = cbNePasContacter.Checked ? 1 : 0;
  }

  protected void fvContacts_ItemInserted(object sender, FormViewInsertedEventArgs e)
  {
    MessageLabel.Text = "Le nouveau contact a été inséré avec succès. Vous pouvez maintenant ajouter des enfants.";
  }
  #endregion


  #region Eleves ListView

  // Set templates so they share code
  protected void ListView1_Init(object sender, EventArgs e)
  {
    ListView1.InsertItemTemplate = ListView1.EditItemTemplate;
    ListView1.SelectedItemTemplate = ListView1.ItemTemplate;
  }


  // Temporarily force dates to US format so they get parsed on their way to MySQL (ASP.Net bug)
  protected void ListView1_ItemUpdating(object sender, ListViewUpdateEventArgs e)
  {
    foreach (DictionaryEntry de in e.NewValues)
    {
      DateTime currentDate = DateTime.MinValue;
      if (de.Value != null && DateTime.TryParse(de.Value.ToString(), out currentDate))
      {
        e.NewValues[de.Key] = currentDate.ToString(new CultureInfo("en-US"));
      }
    }

    e.NewValues["ActuellementInscrit"] = IsChecked("ActuellementInscrit");
    e.NewValues["PreInscrit"] = IsChecked("PreInscrit");
    e.NewValues["PbMedicaux"] = IsChecked("PbMedicaux");
    e.NewValues["PhotosClasse"] = IsChecked("PhotosClasse");
    e.NewValues["PhotosWeb"] = IsChecked("PhotosWeb");
    e.NewValues["Gateaux"] = IsChecked("Gateaux");

    e.NewValues["Sexe"] = ((RadioButtonList)ListView1.EditItem.FindControl("SexeRadioButtonList")).SelectedValue;

    e.NewValues["ClasseActuelle"] = ((DropDownList)ListView1.EditItem.FindControl("ClasseActuelleDropDown")).SelectedItem.Value;
  }



   // Turn on the Edit buttons if an item is being edited
  protected void ListView1_DataBound(object sender, EventArgs e)
  {
    TextBox tb = (TextBox)ListView1.InsertItem.FindControl("ContactIDTextBox");
    tb.Text = String.IsNullOrEmpty(fvContacts.DataKey.Value.ToString()) ? ContactID.ToString() : fvContacts.DataKey.Value.ToString();

    if (ListView1.EditIndex >= 0)
    {
      Panel pnInsertPanel = ListView1.EditItem.FindControl("InsertButtons") as Panel;
      Panel pnEditPanel = ListView1.EditItem.FindControl("EditButtons") as Panel; 
      pnInsertPanel.Visible = false;
      pnEditPanel.Visible = true;
    }
   

  }

  protected void Eleve_NomLabel_PreRender(object sender, EventArgs e)
  {
    Label tb = (Label)sender;
    if (String.IsNullOrEmpty(tb.Text))
    {
      tb.Text = "(Nouvel &eacute;l&egrave;ve)";
    }
  }

  protected void PremiereRentreeListBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    //PopupControlExtender pce = (PopupControlExtender)ListView1.EditItem.FindControl("PopupControlExtender1");
    ListBox lb = (ListBox)ListView1.EditItem.FindControl("PremiereRentreeListBox");
    //pce.Commit("Green");

    ((TextBox)ListView1.EditItem.FindControl("PremiereRentreeTextBox")).Text = lb.SelectedItem.ToString();
    lb.ClearSelection();
  }
  protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
  {

    if (ListView1.EditIndex == -1)
    {
      if (e.Item.FindControl("EditButtons") != null)
      {
        e.Item.FindControl("EditButtons").Visible = userIsAdmin;
      }
      e.Item.FindControl("EditChildButton").Visible = userIsAdmin;

      fvContacts.Visible = true;
    }
    else
    {
      // Editing mode
      fvContacts.Visible = false;
      if (((ListViewDataItem)e.Item).DisplayIndex != ListView1.EditIndex)
      {
        e.Item.Visible = false;
      }
      else
      {

        //Display the right ClasseActuelle
        String classeactuelle = DataBinder.Eval(((ListViewDataItem)e.Item).DataItem, "ClasseActuelle").ToString();
        DropDownList ddl = (DropDownList)e.Item.FindControl("ClasseActuelleDropDown");

        if (ddl != null)
        {
          ListItem selectedListItem = ddl.Items.FindByText(classeactuelle);

          if (selectedListItem != null)
          {
            selectedListItem.Selected = true;
          };
        }
      }

    }


  }

  protected void ListView1_ItemInserting(object sender, ListViewInsertEventArgs e)
  {
    foreach (DictionaryEntry de in e.Values)
    {
      DateTime currentDate = DateTime.MinValue;
      if (de.Value != null && DateTime.TryParse(de.Value.ToString(), out currentDate))
      {
        e.Values[de.Key] = currentDate.ToString(new CultureInfo("en-US"));
      }
    }

    e.Values["ActuellementInscrit"] = IsChecked("ActuellementInscrit");
    e.Values["PreInscrit"] = IsChecked("PreInscrit");
    e.Values["PbMedicaux"] = IsChecked("PbMedicaux");
    e.Values["PhotosClasse"] = IsChecked("PhotosClasse");
    e.Values["PhotosWeb"] = IsChecked("PhotosWeb");
    e.Values["Gateaux"] = IsChecked("Gateaux");

    e.Values["Sexe"] = ((RadioButtonList)ListView1.InsertItem.FindControl("SexeRadioButtonList")).SelectedValue;

    e.Values["ClasseActuelle"] = ((DropDownList)ListView1.InsertItem.FindControl("ClasseActuelleDropDown")).SelectedItem.Value;
  }

  #endregion



  #region Helpers
  private int IsChecked(string controlname)
  {
    string cbname = controlname + "CheckBox";
    CheckBox cb;
    if (ListView1.EditIndex >= 0)
    {
      cb = (CheckBox)ListView1.EditItem.FindControl(cbname);
    }
    else
    {
      cb = (CheckBox)ListView1.InsertItem.FindControl(cbname);
    }
    return cb.Checked ? 1 : 0;
  }
  #endregion

}


