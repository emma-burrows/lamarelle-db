using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;


public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        labOutput.Text = "";
    }

    /// <summary>
    /// Check whether the user's password is a default one.
    /// </summary>
    /// <param name="username">User's login (corresponds to 'utilisateur' field in contacts/employes)</param>
    /// <param name="password">User's current password</param>
    /// <returns>True if the user needs to reset their password; false if the password is all right.</returns>
      public bool NeedsNewPassword(string username, string password)
      {
          if (username != password && password != "lamarelle")
          {
              return false;
          }
          else { return true; }

      }

    /// <summary>
    /// Runs when the user clicks on the login button. It first checks that the user's credentials are in the database,
    /// then sees if they're still using a default password.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
      protected void OnLogIn(object sender, EventArgs e)
      {
          if (LoginHelper.ValidateLogin(Nom.Text, Password.Text))
          {
              // Login was successful but need to check for unchanged password 
              if (NeedsNewPassword(Nom.Text, Password.Text))
              {
                  Name.Text = Nom.Text;
                  Connect.Visible = false;
                  ChangePasswordPanel.Visible = true;
                  labOutput.Text = "Vous utilisez encore le mot de passe d'origine. Pour plus de sécurité, veuillez changer ce mot de passe ci-dessous.";
              }
              else
              {
                  labOutput.Text = "Connexion avec succès";
                  FormsAuthentication.RedirectFromLoginPage(Nom.Text, false);
              }
          }
          else
          { 
              // Login failed
              labOutput.Text = "La connexion a échoué. Veuillez vérifier votre nom d'utilisateur et votre mot de passe.";
          }
      }

    /// <summary>
    /// Runs when the user clicks on 'Changer' to change their password. Writes the new password to the database and
    /// then redirects to the page originally requested.
    /// </summary>
    /// <param name="sender"></param>
      /// <param name="e"></param>
      protected void Change_Click(object sender, EventArgs e)
      {
          LoginHelper.ChangePassword(Name.Text, NewPassword.Text);
          FormsAuthentication.RedirectFromLoginPage(Nom.Text, false);
      }

  /// <summary>
  /// Run when the user clicks on 'Changer le mot de passe' link.
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
      protected void ChangePasswordLink_Click(object sender, EventArgs e)
      {

        Name.Visible = true;
        Connect.Visible = false;
        ChangePasswordPanel.Visible = true;
      }

  /// <summary>
  /// Run when the user clicks on 'Annuler' button
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void Cancel_Click(object sender, EventArgs e)
  {
    if (LoginHelper.ValidateLogin(Nom.Text, Password.Text))
    {
      labOutput.Text = "Connection avec succès";
      FormsAuthentication.RedirectFromLoginPage(Nom.Text, false);
    }
    else
    {
      labOutput.Text = "La connexion a échoué. Veuillez vérifier votre nom d'utilisateur et votre mot de passe.";
      ChangePasswordPanel.Visible = false;
      ConnectionPanel.Visible = true;
    }

  }
}
