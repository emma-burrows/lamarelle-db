<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EleveSearch.ascx.cs" Inherits="Admin_FamilySearch" %>

<div class="FamilySearch">

  <asp:RadioButtonList ID="RadioButtonList1" runat="server" CellSpacing="2"
    RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true"
    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
    <asp:ListItem Value="All">Tous les enfants</asp:ListItem>
    <asp:ListItem Selected="True" Value="Enrolled">Eleves inscrits</asp:ListItem>
    <asp:ListItem Value="Pre-Enrolled">Elèves pré-inscrits</asp:ListItem>
    <asp:ListItem Value="NotEnrolled">Enfants non-inscrits</asp:ListItem>
    <asp:ListItem Value="ByClass">Classes</asp:ListItem>
    <%--<asp:ListItem Value="Family">Nom de famille:</asp:ListItem>--%>
  </asp:RadioButtonList>
  
<%--<asp:DropDownList ID="ddlFamilySearch" Enabled="false" runat="server" Width="174px"/>--%>

<%--<asp:Button ID="Search" runat="server" Text="Chercher" onclick="Search_Click" />--%>


</div>