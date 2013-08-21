<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FamilySearch.ascx.cs" Inherits="Admin_FamilySearch" %>

<div class="FamilySearch">

  <asp:RadioButtonList ID="RadioButtonList1" runat="server" CellSpacing="2"
    RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true"
    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
    <asp:ListItem Selected="True" Value="All">Toutes les familles</asp:ListItem>
    <asp:ListItem Value="Enrolled">Familles inscrites</asp:ListItem>
    <asp:ListItem Value="NotEnrolled">Familles non-inscrites</asp:ListItem>
    <asp:ListItem Value="Family">Une famille:</asp:ListItem>
  </asp:RadioButtonList>
  
<asp:DropDownList ID="ddlFamilySearch" Enabled="false" runat="server" Width="174px"/>

<asp:Button ID="Search" runat="server" Text="Chercher" onclick="Search_Click" />


</div>