<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Stats.aspx.cs" Inherits="Admin_ChangeClasses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Jscript" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="FamilySearch">
<h2>Nombre d'élèves par famille</h2>



</div>

<div class="FamilySearch" style="float:left;">
  
  <h2>Modifier les classes</h2>
  

  
  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    DataObjectTypeName="Classe" DeleteMethod="DeleteClasse" 
    InsertMethod="InsertClasse" OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetClasses" TypeName="ClassesDataObject" 
    UpdateMethod="UpdateClasse"></asp:ObjectDataSource>
  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    DataSourceID="ObjectDataSource1" CellPadding="4" ForeColor="#333333" 
    GridLines="None" onrowdatabound="GridView1_RowDataBound" 
    onrowupdating="GridView1_RowUpdating">
    <RowStyle BackColor="#E3EAEB" />
    <Columns>
      <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
      <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" 
        Visible="False" />
      <asp:BoundField DataField="Niveau" HeaderText="Niveau" 
        SortExpression="Niveau" />
      <asp:BoundField DataField="Nom" HeaderText="Nom" SortExpression="Nom" />
      <asp:BoundField DataField="Enseignant" HeaderText="Enseignant" 
        SortExpression="Enseignant" />
      <asp:BoundField DataField="AgeDebut" HeaderText="AgeDebut" 
        SortExpression="AgeDebut" />
      <asp:BoundField DataField="AgeFin" HeaderText="AgeFin" 
        SortExpression="AgeFin" />
            <asp:BoundField DataField="Section" HeaderText="Section" 
        SortExpression="Section" />
    </Columns>
    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <EditRowStyle BackColor="#7C6F57" />
    <AlternatingRowStyle BackColor="White" />
  </asp:GridView>
  </div>
  
  <div class="FamilySearch" style="float:left;">
    <h2>Nouvelle classe</h2>
    <p>Niveau = utilisé pour le tri sur la page des élèves par classe.</p>
    <asp:Label runat="server" ID="message" Visible="false" />
    
    <table>
      <tr>
        <td class="TDLabels">Nom de la classe</td>
        <td><asp:TextBox ID="Nom" runat="server"/></td>
      </tr>
      <tr>
        <td class="TDLabels">Niveau</td>
        <td><asp:TextBox ID="Niveau" Text="0" runat="server"/><asp:RegularExpressionValidator
            ID="RegularExpressionValidator1" runat="server" ErrorMessage="Numéro entier supérieur à 1." ControlToValidate="Niveau" Text="Numéro entier supérieur à 1." Font-Bold="False" Display="Static" ValidationExpression="\d+"></asp:RegularExpressionValidator></td>
      </tr>
      <tr>
        <td class="TDLabels">Enseignant</td>
        <td><asp:TextBox ID="Enseignant" runat="server"/></td>
      </tr>
      <tr>
        <td class="TDLabels">Age début</td>
        <td><asp:TextBox ID="AgeDebut" Text="0" runat="server"/></td>
      </tr>
      <tr>
        <td class="TDLabels">Age fin</td>
        <td><asp:TextBox ID="AgeFin" Text="0" runat="server"/></td>
      </tr>
      <tr>
        <td class="TDLabels">Section</td>
        <td>
          <asp:DropDownList ID="DropDownList1"  runat="server">
          <asp:ListItem Text="FRA" Value="FRA"/>
          <asp:ListItem Text="FLE" Value="FLE" />
          </asp:DropDownList>
        </td>
      </tr>
    </table>
      
    <asp:Button ID="Ajouter" runat="server" Text="Ajouter" 
      onclick="Ajouter_Click" />
  </div>
  <br />

</asp:Content>

