using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ChangeClasses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      message.Text = "";
      message.Visible = false;
      ObjectDataSource1.DataBind();
      GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
      //EleveDataObject.UpdateClasse(oldClass.Text, newClass.Text);
    }

    protected void Ajouter_Click(object sender, EventArgs e)
    {
      if (String.IsNullOrEmpty(AgeDebut.Text))   AgeDebut.Text = "0";
      if (String.IsNullOrEmpty(AgeFin.Text)) AgeFin.Text = "0";

      Classe classe = new Classe(1, Convert.ToInt32(Niveau.Text), Nom.Text, Enseignant.Text, Convert.ToInt32(AgeDebut.Text), Convert.ToInt32(AgeFin.Text), Convert.ToString(DropDownList1.SelectedValue));
      int result = ClassesDataObject.InsertClasse(classe);
      if (result != 0)
      {
        message.Text = "Classe '" + Nom.Text + "' créée.";
      }
      else
      {
        message.Text = "Une erreur est survenue. (Erreur: " + result + ")";
      }
      Niveau.Text = Nom.Text = Enseignant.Text = AgeDebut.Text = AgeFin.Text = "";
      message.Visible = true;
      ObjectDataSource1.DataBind();
      GridView1.DataBind();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      if (e.Row.RowType == DataControlRowType.DataRow)
      {
        LinkButton btnDelete = new LinkButton();
        btnDelete = e.Row.Cells[0].Controls[2] as LinkButton;

        Label lbNom = new Label();
        
        lbNom.Text = ((Classe)e.Row.DataItem).Nom;

        if (btnDelete.CommandName == "Delete")
          btnDelete.OnClientClick = "return confirm('La classe " + lbNom.Text + " sera supprimée. Continuer ?');"; 

      }

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      //Classe newClasse = new Classe(Convert.ToInt32(e.NewValues["ID"]), Convert.ToInt32(e.NewValues["Niveau"]), Convert.ToString(e.NewValues["Nom"]), Convert.ToString(e.NewValues["Enseignant"]), Convert.ToInt32(e.NewValues["AgeDebut"]), Convert.ToInt32(e.NewValues["AgeFin"]));
      //ObjectDataSource1.UpdateParameters.Add(new Parameter("classes"));
      //ObjectDataSource1.UpdateParameters["classes"]. = newClasse;
      ObjectDataSource1.Update();
    }
}
