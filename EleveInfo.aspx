<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Détails de l'élève" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByID" 
        TypeName="ElevesDataSetTableAdapters.ElevesContactsInfosTableAdapter">
    <SelectParameters>
      <asp:QueryStringParameter DefaultValue="53" Name="ID" QueryStringField="id" Type="Int32" />
    </SelectParameters>
    </asp:ObjectDataSource>
    <p>
        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" 
            DataSourceID="ObjectDataSource1" Height="50px" Width="700px">
            <Fields>
      <asp:BoundField DataField="Eleves_Nom" HeaderText="Nom" SortExpression="Eleves_Nom" />
      <asp:BoundField DataField="Prenom" HeaderText="Pr&#233;nom" SortExpression="Prenom" />
      <asp:BoundField DataField="DateNaissance" HeaderText="Date de naissance" SortExpression="DateNaissance" DataFormatString="{0:d}" />
      <asp:BoundField DataField="Nationalite" HeaderText="Nationalit&#233;" SortExpression="Nationalite" />
      <asp:BoundField DataField="Sexe" HeaderText="Sexe" SortExpression="Sexe" />
      <asp:BoundField DataField="AgeActuel" HeaderText="Age Actuel" ReadOnly="True" SortExpression="AgeActuel" />
      <asp:BoundField DataField="ClasseActuelle" HeaderText="Classe Actuelle" SortExpression="ClasseActuelle" />
      <asp:BoundField DataField="PremiereRentree" HeaderText="Premiere Rentr&#233;e"
        SortExpression="PremiereRentree" DataFormatString="{0:y}" />
      <asp:CheckBoxField DataField="ActuellementInscrit" HeaderText="Actuellement Inscrit"
        SortExpression="ActuellementInscrit" />
      <asp:CheckBoxField DataField="PreInscrit" HeaderText="Pre-inscrit" SortExpression="PreInscrit" />
      <asp:TemplateField HeaderText="Contact principal" SortExpression="NomContact" InsertVisible="False" >
        <ItemTemplate>
          <asp:Label ID="Contact" runat="server" Text='<%# Eval("PrénomContact") + " " + Eval("NomContact") + " (" + Eval("Téléphone portable") + " - " %>'></asp:Label>
          <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "mailto:" + Eval("Email") %>'
            Text='<%# Eval("Email") %>'></asp:HyperLink>
          <asp:Label ID="Contact2" runat="server" Text='<%# ")" %>' />
        </ItemTemplate>
      </asp:TemplateField>
       <asp:TemplateField HeaderText="Autre Contact" SortExpression="NomRelation" InsertVisible="False" >
        <ItemTemplate>
          <asp:Label ID="Contact" runat="server" Text='<%# Eval("PrénomRelation") + " " + Eval("NomRelation") + " (" + Eval("Relation") + ") " + Eval("TelephonePortable") + " - " %>'></asp:Label>
          <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "mailto:" + Eval("EmailRelation") %>'
            Text='<%# Eval("EmailRelation") %>'></asp:HyperLink>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:BoundField DataField="Adresse 1" HeaderText="Adresse 1" SortExpression="Adresse 1" />
      <asp:BoundField DataField="Adresse 2" HeaderText="Adresse 2" SortExpression="Adresse 2" />
      <asp:BoundField DataField="Adresse 3" HeaderText="Adresse 3" SortExpression="Adresse 3" />
      <asp:BoundField DataField="Ville" HeaderText="Ville" SortExpression="Ville" />
      <asp:BoundField DataField="Code Postal" HeaderText="Code Postal" SortExpression="Code Postal" />
      <asp:BoundField DataField="T&#233;l&#233;phone fixe" HeaderText="T&#233;l&#233;phone fixe"
        SortExpression="T&#233;l&#233;phone fixe" />
      <asp:TemplateField HeaderText="Sibling 1" SortExpression="Sibling1Nom" InsertVisible="False" >
        <ItemTemplate>
          <asp:Label ID="Sibling1" runat="server" Text='<%# Eval("Sibling1Nom") + ", " + Eval("Sibling1Prenom") + " (" + Eval("Sibling1Date", "{0:d}") + ")"%>'></asp:Label>
        </ItemTemplate>
      </asp:TemplateField>
      <asp:TemplateField HeaderText="Sibling2" SortExpression="Sibling2Nom" InsertVisible="False">
        <ItemTemplate>
          <asp:Label ID="Sibling2" runat="server" Text='<%# Eval("Sibling2Nom") + ", " + Eval("Sibling2Prenom") + " (" + Eval("Sibling2Date", "{0:d}") + ")"%>'></asp:Label>
        </ItemTemplate>
      </asp:TemplateField>
                <asp:CheckBoxField DataField="PbMedicaux" HeaderText="Problèmes Medicaux ?" 
                    SortExpression="PbMedicaux" />
                <asp:BoundField DataField="Détails médicaux" HeaderText="Détails médicaux" 
                    SortExpression="Détails médicaux" />

                <asp:BoundField DataField="Docteur" HeaderText="Médecin" 
                    SortExpression="Docteur" />
                <asp:CheckBoxField DataField="PhotosClasse" HeaderText="Photos en classe ?" 
                    SortExpression="PhotosClasse" />
                <asp:CheckBoxField DataField="PhotosWeb" HeaderText="Photos sur le Web ?" 
                    SortExpression="PhotosWeb" />
                <asp:CheckBoxField DataField="Gateaux" HeaderText="Gateaux ?" 
                    SortExpression="Gateaux" />
            </Fields>
    <PagerSettings Mode="NextPrevious" />
    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <CommandRowStyle BackColor="#FFFFC0" Font-Bold="True" />
    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
    <FieldHeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
    <AlternatingRowStyle BackColor="White" />
    <HeaderTemplate>
      <asp:Label ID="Label1" runat="server" Font-Names="Cursif,French Script MT" Text='<%# Eval("Prenom") + " " + Eval("Eleves_Nom") %>' Font-Bold="False" Font-Size="16pt"></asp:Label>
    </HeaderTemplate>
  </asp:DetailsView>
    </p>

<!--
                <asp:BoundField DataField="AdultePermis1" HeaderText="AdultePermis1" 
                    SortExpression="AdultePermis1" />
                <asp:BoundField DataField="AdultePermis1Tel" HeaderText="AdultePermis1Tel" 
                    SortExpression="AdultePermis1Tel" />
                <asp:BoundField DataField="AdultePermis2" HeaderText="AdultePermis2" 
                    SortExpression="AdultePermis2" />
                <asp:BoundField DataField="AdultePermis2Tel" HeaderText="AdultePermis2Tel" 
                    SortExpression="AdultePermis2Tel" />
                <asp:BoundField DataField="AdultePermis3" HeaderText="AdultePermis3" 
                    SortExpression="AdultePermis3" />
                <asp:BoundField DataField="AdultePermis3Tel" HeaderText="AdultePermis3Tel" 
                    SortExpression="AdultePermis3Tel" />
                    -->

</asp:Content>

