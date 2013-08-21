using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Collections.Specialized;
using System.Reflection;


public partial class Admin_Eleves : System.Web.UI.Page
{
  private int ContactID = 0;
  private int Status = 2;
  private string TempClassName = "";
  private DataSet Classes = ElevesHelper.ClassesInUseLookup();

  public int showDialog = 0;
  public string dialogTitle = string.Empty;

  private int offset = 2;


  protected void Page_Load(object sender, EventArgs e)
  {
    DataView view = new DataView(Classes.Tables[0]);
    view.RowFilter = "section='FRA'";
    Repeater1.DataSource = view;
    Repeater1.DataBind();

    view.RowFilter = "section='FLE'";
    Repeater2.DataSource = view;
    Repeater2.DataBind();

    if (!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["s"]))
    {
      Status = Convert.ToInt32(HttpContext.Current.Request.QueryString["s"]);

      ViewState.Add("status", Status);
      ViewState.Add("contactid", ContactID);
      GridView1.DataBind();
    }

    // Get results from FamilySearch user control
    EleveSearch1.sendMessageToThePage += delegate(string radiovalue, string message)
    {
      GridView1.EditIndex = -1;
      if (radiovalue == "Family")
      {
        Status = 0;
        ContactID = Int32.Parse(message.Split('~')[0]);
      }
      else
      {
        // Reset contact id so results aren't filtered
        ContactID = 0;
        Status = 2;
        pnClasses.Visible = false;

        // TODO: handle specific cases
        switch (radiovalue)
        {
          case "All":
            Status = 0;
            break;

          case "NotEnrolled":
            Status = 1;
            break;

          case "Enrolled":
            Status = 2;
            break;

          case "Pre-Enrolled":
            Status = 3;
            break;

          case "ByClass":
            Status = 4;
            pnClasses.Visible = true;
            HtmlGenericControl body = (HtmlGenericControl)Master.FindControl("master");
            body.Attributes.Add("class", "classes");

            break;
        }

      }
      ViewState.Add("status", Status);
      ViewState.Add("contactid", ContactID);
      GridView1.DataBind();

    };

    if (Status == 4)
    {
      pnClasses.Visible = true;
      HtmlGenericControl body = (HtmlGenericControl)Master.FindControl("master");
      body.Attributes.Add("class", "classes");
    }
    else
    {
      // Set page CSS class to highlight the right menu
      HtmlGenericControl body = (HtmlGenericControl)Master.FindControl("master");
      body.Attributes.Add("class", "eleves");
    }
  }

  protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
  {
    foreach (DictionaryEntry de in e.NewValues)
    {
      DateTime currentDate = DateTime.MinValue;
      if (de.Value != null && DateTime.TryParse(de.Value.ToString(), out currentDate))
      {
        e.NewValues[de.Key] = currentDate.ToString(new CultureInfo("en-US"));
      }
    }

    IDictionary values = GetValues(GridView1.Rows[GridView1.EditIndex]);
    EleveWithContacts current = (EleveWithContacts)GridView1.Rows[e.RowIndex].DataItem;
    int inscritcol = GetColumnIndexByHeaderText(GridView1, "Inscrit");
    int permissioncol = GetColumnIndexByHeaderText(GridView1, "Problèmes et Permissions");

    int rowindex = e.RowIndex;
    if (GridView1.Columns[1].Visible)
    {
      e.NewValues["ActuellementInscrit"] = IsChecked("ActuellementInscrit", rowindex, 1);
      e.NewValues["PreInscrit"] = IsChecked("PreInscrit", rowindex, 1);
    }
    else
    {
      e.NewValues["ActuellementInscrit"] = e.OldValues["ActuellementInscrit"];
      e.NewValues["PreInscrit"] = e.OldValues["PreInscrit"];
    }
    if (GridView1.Columns[8].Visible)
    {
      e.NewValues["PbMedicaux"] = IsChecked("PbMedicaux", rowindex, 8);
      e.NewValues["PhotosClasse"] = IsChecked("PhotosClasse", rowindex, 8);
      e.NewValues["PhotosWeb"] = IsChecked("PhotosWeb", rowindex, 8);
      e.NewValues["Gateaux"] = IsChecked("Gateaux", rowindex, 8);
    }
    else
    {
      e.NewValues["PbMedicaux"] = e.OldValues["PbMedicaux"];
      e.NewValues["PhotosClasse"] = e.OldValues["PhotosClasse"];
      e.NewValues["PhotosWeb"] = e.OldValues["PhotosWeb"];
      e.NewValues["Gateaux"] = e.OldValues["Gateaux"];
    }


  }

  protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
  {
    // Show registered pupils by default
    string filter = "eleves.ActuellementInscrit=1";

    // Check whether the viewstate is filtering the list by family or by status
    if (Convert.ToInt32(ViewState["contactid"]) > 0)
    {
      filter = "ContactID=" + ViewState["contactid"];
    }
    else
    {
      ViewState["contactid"] = 0;
      switch (Convert.ToInt32(ViewState["status"]))
      {
        case 1: // Not Enrolled
          filter = "eleves.ActuellementInscrit=0";
          break;
        case 2: // Enrolled
          filter = "eleves.ActuellementInscrit=1";
          break;
        case 3: // Pre-Enrolled
          filter = "eleves.ActuellementInscrit=0 AND eleves.PreInscrit=1";
          break;
        case 4: // ByClass
          filter = "eleves.ActuellementInscrit=1";
          e.Arguments.SortExpression = "niveau, nom, prenom";
          break;
        default:
          filter = "";
          break;
      }
    }
    e.InputParameters["Filter"] = filter;
  }


  private bool IsChecked(string controlname, int rowindex, int colindex)
  {
    string cbname = controlname + "CheckBox";
    CheckBox cb = (CheckBox)GridView1.Rows[rowindex].Cells[colindex].FindControl(cbname);
    return cb.Checked ? true : false;
  }

  protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
  {
    //if (LoginHelper.UserRole(Page.User.Identity.Name) == "admin")
    //{
    //  GridView1.Columns[0].Visible = true;
    //}
    //else
    //{
    //  GridView1.Columns[0].Visible = false;
    //}

    if (Status == 4) //Ordered by class - need to insert headers to indicate the class name
    {
      GridView1.Columns[0].Visible = false;
      GridView1.Columns[GetColumnIndexByHeaderText(GridView1, "Classe")].Visible = false;

      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        EleveWithContacts current = (EleveWithContacts)e.Row.DataItem;

        Table tbl = e.Row.Parent as Table;
        GroupByClass(tbl, current.ClasseActuelle, false);
      }
    }
  }

  static public int GetColumnIndexByHeaderText(GridView aGridView, String ColumnText)
  {
    string headertext;
    for (int Index = 0; Index < aGridView.Columns.Count; Index++)
    {
      headertext = aGridView.Columns[Index].HeaderText;
      if (headertext == ColumnText)
        return Index;
    }
    return -1;
  }

  public static IDictionary GetValues(GridViewRow row)
  {
    IOrderedDictionary values = new OrderedDictionary();

    foreach (DataControlFieldCell cell in row.Cells)
    {
        // Extract values from the cell
        cell.ContainingField.ExtractValuesFromCell(values, cell, row.RowState, true);
    }
    return values;

  }

  private void GroupByClass(Table tbl, String current, bool excel)
  {

    if (TempClassName != current)
    {
      TempClassName = String.IsNullOrEmpty(current) ? "Aucune Classe" : current;

      //Insert a row into the autogenerated table
      if (tbl != null)
      {
        tbl.ID = TempClassName.Replace(" ", "") + "Table";
        GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
        row.ID = tbl.ID + "Row";
        TableCell cell = new TableCell();
        cell.ID = tbl.ID + "Cell1";
        TableCell cell2 = new TableCell();
        cell2.ID = tbl.ID + "Cell2";


        // Span the row across almost all the columns in the Gridview
        cell.ColumnSpan = this.GridView1.Columns.Count - 6;

        //cell.Width = Unit.Percentage(100);
        cell.Style.Add("font-weight", "bold");
        cell.Style.Add("background-color", "#ffffff");
        cell.Style.Add("color", "black");

        Literal lab = new Literal();
        lab.ID = tbl.ID + "Lit1";

        lab.Text = "<a name='" + TempClassName + "'></a><h2>" + TempClassName + " <a href='#top'>^</a></h2>";

        cell.Controls.Add(lab);


        cell2.ColumnSpan = 2;
        cell2.Style.Add("font-weight", "bold");
        cell2.Style.Add("background-color", "#ffffff");
        cell2.Style.Add("color", "black");

        Literal lit2 = new Literal();
        lit2.ID = tbl.ID + "Lit2";

        DataRow[] foundRows;
        foundRows = Classes.Tables[0].Select("classe ='" + TempClassName + "'");
        if (foundRows == null || foundRows.Length == 0)
        {
          lit2.Text = "0 élèves";
        }
        else
        {
          lit2.Text = foundRows[0]["eleves"] + " élèves - " + foundRows[0]["enseignant"];
        }
        if (Status == 4)
        {
          cell2.Controls.Add(lit2);
          row.Cells.Add(cell);
          row.Cells.Add(cell2);
        }


        if (excel && foundRows!=null && foundRows.Length!=0)
        {
          if (offset < tbl.Rows.Count)
          {
            tbl.Rows.AddAt(offset, row);
            offset += Convert.ToInt32(foundRows[0]["eleves"]) + 1; // +1 because the class rows are exported as two merged cells
          }
        }
        else
        {
          tbl.Rows.AddAt(tbl.Rows.Count - 1, row);
        }
      }
    }
  }


  protected void Button1_Click(object sender, EventArgs e)
  {
    MessageLabel.Text = "Chargement du fichier Excel en cours, veuillez patienter...";
    

    String filename = "";
    switch (Convert.ToInt32(ViewState["status"]))
    {
      case 1:
        GridView1.AllowSorting = false;
        GridView1.AllowPaging = false;
        GridView1.DataBind();
        filename = "Non-Inscrits-" ;
        break;

      case 2:
        GridView1.AllowSorting = false;
        GridView1.AllowPaging = false;
        GridView1.DataBind();
        filename = "Eleves-Inscrits-";
        break;

      case 3:
        GridView1.AllowSorting = false;
        GridView1.AllowPaging = false;
        GridView1.DataBind();
        filename = "Eleves-Pre-Inscrits-";
        break;

      case 4:
        GridView1.Columns[GetColumnIndexByHeaderText(GridView1, "Classe")].Visible = true;
        GridView1.DataBind();
        filename = "Classes-";
        break;

      default:
        filename = "La-Marelle-";
        break;
    }



    Response.Clear();
    Response.AddHeader("content-disposition", "attachment;filename='" + filename + System.DateTime.Now.ToString() + ".xls'");
    Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
    //Response.Charset = "";

    // If you want the option to open the Excel file without saving then
    // comment out the line below
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.ContentType = "application/vnd.ms-excel";
    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
    
    GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
    GridView1.HeaderRow.Style.Add("color", "#000000");
    for (int i = 0; i < GridView1.Rows.Count; i++)
    {
      GridViewRow row = GridView1.Rows[i];

      GroupByClass(row.Parent as Table, 
        System.Web.HttpUtility.HtmlDecode(row.Cells[GetColumnIndexByHeaderText(GridView1, "Classe")].Text), 
        true);

      //Change Color back to white   
      row.BackColor = System.Drawing.Color.White;
      //Apply text style to each Row   
      row.Attributes.Add("class", "textmode");
    } 

    GridView1.RenderControl(htmlWrite);
    Response.Write(stringWrite.ToString());
    Response.End();
    //Response.Flush();
    //HttpContext.Current.ApplicationInstance.CompleteRequest();


  }



  public override void VerifyRenderingInServerForm(Control control)
  {
    /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
       server control at run time. */
  }

  protected void Emails_Click(object sender, EventArgs e)
  {
    ElevesHelper.GetEmailFile("fichiers.txt", Page);
    Response.Redirect("~/fichiers/emails.txt");
  }
}
