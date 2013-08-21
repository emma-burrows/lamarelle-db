<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="La Marelle - Base de donn�es" %>

<script runat="server">
  int Span = 6;
    
    protected void Page_Load()
    {
      string userrole = LoginHelper.UserRole(Page.User.Identity.Name);
      if (userrole == "admin")
      {
        litAccueilAdmin.Visible = true;
        Span = 4;    
      }
      ElevesHelper.GetEmailFile("fichiers/emails.txt", Page);
    }



</script>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <div class="hero-unit">
    <h1>Base de donn�es</h1>
    <p>Ce site contient les d�tails de nos �l�ves et leurs familles, enregistr�s dans notre base de donn�es. 
    Ces donn�es confidentielles sont g�r�es et conserv�es directement par 
    La Marelle, conform�ment aux r�gles de The Information Commission avec qui nous sommes enregistr�s. 
    Seuls les utilisateurs autoris�s peuvent �diter les donn�es sur nos �l�ves.</p>

    <p>Veuillez utiliser le menu en haut de cette page ou les liens ci-dessous pour naviguer le site.</p>
  </div>
  <div class="row">
    <asp:Panel ID="litAccueilAdmin" CssClass="hero" Visible="false" runat="server">
      <div class="span4" id="accueil-admin">
        <h1>Administration</h1>
        <p>Cette section, tout comme le menu correspondant, n'est visible que pour les administrateurs du site.</p>
        <h4>Familles et �l�ves</h4>
        <a href="Admin/FamilyForm.aspx" id="editfamily">Editer ou cr�er de nouvelles familles</a><br />
        <h4>Employ�s</h4>
        <a href="Admin/EditAnyTable.aspx?t=employes" id="editusers">Editer les utilisateurs</a> - attention, ce sont les vrai logins des vrais utilisateurs!<br />
        <h4>Fichiers utiles</h4>
        <a href="fichiers/emails.txt" id="emails">Addresses emails des familles inscrites (pour le bulletin)</a>
      </div>
    </asp:Panel>

    <div class="span<%= Span %>">
      <h1>El�ves</h1>
      <p>Liste des �l�ves inscrits ou des enfants enregistr�s dans la base.</p>
      <p>
        <a href="Eleves.aspx?s=2">El�ves inscrits</a><br />
        <a href="Eleves.aspx?s=4">El�ves par classe</a><br />
        <a href="Eleves.aspx?s=3">El�ves pre-inscrits ou temporairement absents</a><br />
      </p>
    </div>


    <div class="span<%= Span %>">
      <h1>Autres Ressources</h1>
      <p>
        Les autres sites utilis�s par La Marelle. Si vous n'avez pas de compte pour ces sites, veuillez contacter un membre de l'�quipe administrative.
      </p>
 
      <h4><a href="https://docs.google.com/folder/d/0B1xPFggBK7XULUNiVzliZTQtWlE/edit" target="_blank">Documents Administratifs</a></h4>
      <p>
        Documents utiles pour tout le staff � La Marelle; fiches d'inscription, originaux des carnets 
et livrets que nous publions, etc. (Vous pouvez stocker vos propres documents sur votre <a href="http://drive.google.com/">compte Google Drive</a> si vous avez une adresse Gmail)
      </p>

    </div>


  </div>
  <br />


</asp:Content>

