<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
  <title>La Marelle - Connexion</title>
  <link type="text/css" href="../main.css" rel="Stylesheet" />
</head>
<%--    <!-- Place this asynchronous JavaScript just before your </body> tag -->
<script type="text/javascript">
  (function () {
    var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
    po.src = 'https://apis.google.com/js/client:plusone.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
  })();
</script>--%>
<body>
    <h1>&nbsp;La Marelle - Base de données</h1>
    
    <hr/>
    <form id="Form1" runat="server">
    <asp:Panel ID="ConnectionPanel" runat="server">
    <p>Veuillez vous connecter pour voir les données.</p>
    <table cellpadding="8">
      <tr>
        <td colspan="2">
          <%--<span id="signinButton">
  <span
    class="g-signin"
    data-callback="signinCallback"
    data-clientid="993005156425.apps.googleusercontent.com"
    data-cookiepolicy="single_host_origin"
    data-requestvisibleactions="http://schemas.google.com/AddActivity"
    data-scope="https://www.googleapis.com/auth/plus.login">
  </span>
</span>--%>
        </td>
      </tr>
        <tr>
            <td>Nom:</td>
            <td><asp:TextBox ID="Nom" RunAt="server" /></td>
        </tr>
        <tr>
            <td>Mot de passe:</td>
            <td><asp:TextBox ID="Password" TextMode="password" RunAt="server" /></td>
        </tr>
        <tr>
            <td><asp:Button ID="Connect" Text="Connexion" OnClick="OnLogIn" RunAt="server" /></td>
            <td>
              <asp:LinkButton ID="ChangePasswordLink" runat="server" 
                onclick="ChangePasswordLink_Click">Changer le mot de passe</asp:LinkButton>
            </td>
        </tr>
    </table>
    </asp:Panel>
    <hr/>
    <h3><asp:Label ID="labOutput" RunAt="server" /></h3>
    
    <asp:Panel ID="ChangePasswordPanel" Visible="false" runat="server" Wrap="False">
         Veuillez changer votre mot de passe ci-dessous.
    <table cellpadding="8" width="100%">
        <tr>
            <td>Nom:</td>
            <td><asp:TextBox ID="Name" RunAt="server" /><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator4" ControlToValidate="Name" runat="server" ErrorMessage="Veuillez entrer votre nom d'utilisateur actuel."/>
            </td>
        </tr>
        <tr>
            <td><b>Ancient</b> mot de passe:</td>
            <td><asp:TextBox ID="OldPassword" TextMode="password" RunAt="server" /><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator3" ControlToValidate="NewPasswordConfirm" runat="server" ErrorMessage="Veuillez entrer votre mot de passe actuel."/></td>
        </tr>
        <tr>
            <td><b>Nouveau</b> mot de passe:</td>
            <td><asp:TextBox ID="NewPassword" TextMode="password" RunAt="server" /> <asp:RequiredFieldValidator
                    ID="RequiredFieldValidator2" ControlToValidate="NewPassword" runat="server" ErrorMessage="Veuillez entrer un nouveau mot de passe."/></td>
        </tr>
        <tr>
            <td>Confirmer le mot de passe:</td>
            <td><asp:TextBox ID="NewPasswordConfirm" TextMode="password" RunAt="server" /><asp:RequiredFieldValidator
                    ID="RequiredFieldValidator1" ControlToValidate="NewPasswordConfirm" runat="server" ErrorMessage="Veuillez confirmer le nouveau mot de passe."/>&nbsp;<asp:CompareValidator 
                    ID="CompareValidator1" runat="server" 
                    ErrorMessage="Veuillez entrer le même nouveau mot de passe que ci-dessus." 
                    ControlToCompare="NewPassword" ControlToValidate="NewPasswordConfirm"></asp:CompareValidator>
            </td>
        </tr>
                <tr>
            <td>&nbsp;</td>
            <td><asp:Button ID="Change" Text="Changer" OnClick="Change_Click" RunAt="server" />&nbsp;
                <asp:Button ID="Cancel" Text="Annuler" RunAt="server" 
                onclick="Cancel_Click" />
            </td>
        </tr>
    </table>
   
    </asp:Panel>
     </form>
        </body>
    </html>
