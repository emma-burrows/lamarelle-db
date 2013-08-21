<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FamilyList.aspx.cs" Inherits="Admin_FamilyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:ObjectDataSource ID="odsContacts" runat="server" 
     OldValuesParameterFormatString="original_{0}"
     SelectMethod="GetContactsByStatus" 
     TypeName="ContactDataObject" 
     DataObjectTypeName="Contact" 
     InsertMethod="InsertContact" 
     UpdateMethod="UpdateContact" onselecting="odsContacts_Selecting" DeleteMethod="DeleteContact"
     >
     <DeleteParameters>
       <asp:Parameter Name="ContactID" Type="Int32" />
     </DeleteParameters>
     <SelectParameters>
       <asp:ControlParameter ControlID="RadioButtonList1" Name="inscrit" 
         PropertyName="SelectedValue" Type="Boolean" />
     </SelectParameters>
  </asp:ObjectDataSource>

  <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
    Width="257px">
    <asp:ListItem Value="true" Selected="True">Inscrits</asp:ListItem>
    <asp:ListItem Value="false">Pas inscrits</asp:ListItem>
  </asp:RadioButtonList>



  <asp:ListView ID="lvParents" runat="server" DataSourceID="odsContacts" >
    <ItemTemplate>
      <tr style="">
        <td>
          <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
        </td>
        <td>
          <asp:Label ID="NomLabel" runat="server" Text='<%# Eval("Nom") %>' />
        </td>
        <td>
          <asp:Label ID="PrenomLabel" runat="server" Text='<%# Eval("Prenom") %>' />
        </td>
        <td>
          <asp:Label ID="VilleLabel" runat="server" Text='<%# Eval("Ville") %>' />
        </td>
        <td>
          <asp:Label ID="CodePostalLabel" runat="server" 
            Text='<%# Eval("CodePostal") %>' />
        </td>
        <td>
          <asp:Label ID="FixeLabel" runat="server" Text='<%# Eval("Fixe") %>' />
        </td>
        <td>
          <asp:Label ID="PortableLabel" runat="server" Text='<%# Eval("Portable") %>' />
        </td>
        <td>
          <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
        </td>
        <td>
          <asp:CheckBox ID="ActuellementInscritCheckBox" runat="server" 
    Checked='<%# Convert.ToBoolean(Eval("ActuellementInscrit")) %>'  />
        </td>
        <td>
          <asp:CheckBox ID="ComiteCheckBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("Comite")) %>' />
        </td>
        <td>
          <asp:CheckBox ID="NePasContacterCheckBox" runat="server"  Checked='<%# Convert.ToBoolean(Eval("NePasContacter")) %>' />
        </td>
        <td>
          <asp:Label ID="NotesLabel" runat="server" Text='<%# Eval("Notes") %>' />
        </td>
      </tr>
      <tr>
        <asp:ListView ID="lvEnfants" runat="server" 
    DataSource='<%# EleveDataObject.GetElevesByContactID(Convert.ToInt32(Eval("Contact_ID"))) %>'>
          <ItemTemplate>
            <tr style="">
              <td>
                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
              </td>
              <td>
                <asp:Label ID="Eleve_IDLabel" runat="server" Text='<%# Eval("Eleve_ID") %>' />
              </td>
              <td>
                <asp:Label ID="ContactIDLabel" runat="server" Text='<%# Eval("ContactID") %>' />
              </td>
              <td>
                <asp:Label ID="NomLabel" runat="server" Text='<%# Eval("Nom") %>' />
              </td>
              <td>
                <asp:Label ID="PrenomLabel" runat="server" Text='<%# Eval("Prenom") %>' />
              </td>
              <td>
                <asp:Label ID="DateNaissanceLabel" runat="server" 
                  Text='<%# Eval("DateNaissance") %>' />
              </td>
              <td>
                <asp:Label ID="NationaliteLabel" runat="server" 
                  Text='<%# Eval("Nationalite") %>' />
              </td>
              <td>
                <asp:Label ID="PremiereRentreeLabel" runat="server" 
                  Text='<%# Eval("PremiereRentree") %>' />
              </td>
              <td>
                <asp:Label ID="ClasseActuelleLabel" runat="server" 
                  Text='<%# Eval("ClasseActuelle") %>' />
              </td>
              <td>
                <asp:Label ID="ActuellementInscritLabel" runat="server" 
                  Text='<%# Eval("ActuellementInscrit") %>' />
              </td>
              <td>
                <asp:Label ID="PreInscritLabel" runat="server" 
                  Text='<%# Eval("PreInscrit") %>' />
              </td>
              <td>
                <asp:Label ID="SexeLabel" runat="server" Text='<%# Eval("Sexe") %>' />
              </td>
              <td>
                <asp:Label ID="RelationAutreParentLabel" runat="server" 
                  Text='<%# Eval("RelationAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="NomAutreParentLabel" runat="server" 
                  Text='<%# Eval("NomAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="PrenomAutreParentLabel" runat="server" 
                  Text='<%# Eval("PrenomAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="AdresseAutreParentLabel" runat="server" 
                  Text='<%# Eval("AdresseAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="FixeAutreParentLabel" runat="server" 
                  Text='<%# Eval("FixeAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="PortableAutreParentLabel" runat="server" 
                  Text='<%# Eval("PortableAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="EmailAutreParentLabel" runat="server" 
                  Text='<%# Eval("EmailAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="DocteurLabel" runat="server" Text='<%# Eval("Docteur") %>' />
              </td>
<%--              <td>
                <asp:Label ID="PhotosClasseLabel" runat="server" 
                  Text='<%# Eval("PhotosClasse") %>' />
              </td>
              <td>
                <asp:Label ID="PhotosWebLabel" runat="server" Text='<%# Eval("PhotosWeb") %>' />
              </td>
              <td>
                <asp:Label ID="GateauxLabel" runat="server" Text='<%# Eval("Gateaux") %>' />
              </td>
              <td>
                <asp:Label ID="PbMedicauxLabel" runat="server" 
                  Text='<%# Eval("PbMedicaux") %>' />
              </td>--%>
              <td>
                <asp:Label ID="DetailsMedicauxLabel" runat="server" 
                  Text='<%# Eval("DetailsMedicaux") %>' />
              </td>
            </tr>
          </ItemTemplate>
          <AlternatingItemTemplate>
            <tr style="">
              <td>
                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
              </td>
              <td>
                <asp:Label ID="Eleve_IDLabel" runat="server" Text='<%# Eval("Eleve_ID") %>' />
              </td>
              <td>
                <asp:Label ID="ContactIDLabel" runat="server" Text='<%# Eval("ContactID") %>' />
              </td>
              <td>
                <asp:Label ID="NomLabel" runat="server" Text='<%# Eval("Nom") %>' />
              </td>
              <td>
                <asp:Label ID="PrenomLabel" runat="server" Text='<%# Eval("Prenom") %>' />
              </td>
              <td>
                <asp:Label ID="DateNaissanceLabel" runat="server" 
                  Text='<%# Eval("DateNaissance") %>' />
              </td>
              <td>
                <asp:Label ID="NationaliteLabel" runat="server" 
                  Text='<%# Eval("Nationalite") %>' />
              </td>
              <td>
                <asp:Label ID="PremiereRentreeLabel" runat="server" 
                  Text='<%# Eval("PremiereRentree") %>' />
              </td>
              <td>
                <asp:Label ID="ClasseActuelleLabel" runat="server" 
                  Text='<%# Eval("ClasseActuelle") %>' />
              </td>
              <td>
                <asp:Label ID="ActuellementInscritLabel" runat="server" 
                  Text='<%# Eval("ActuellementInscrit") %>' />
              </td>
              <td>
                <asp:Label ID="PreInscritLabel" runat="server" 
                  Text='<%# Eval("PreInscrit") %>' />
              </td>
              <td>
                <asp:Label ID="SexeLabel" runat="server" Text='<%# Eval("Sexe") %>' />
              </td>
              <td>
                <asp:Label ID="RelationAutreParentLabel" runat="server" 
                  Text='<%# Eval("RelationAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="NomAutreParentLabel" runat="server" 
                  Text='<%# Eval("NomAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="PrenomAutreParentLabel" runat="server" 
                  Text='<%# Eval("PrenomAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="AdresseAutreParentLabel" runat="server" 
                  Text='<%# Eval("AdresseAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="FixeAutreParentLabel" runat="server" 
                  Text='<%# Eval("FixeAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="PortableAutreParentLabel" runat="server" 
                  Text='<%# Eval("PortableAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="EmailAutreParentLabel" runat="server" 
                  Text='<%# Eval("EmailAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="DocteurLabel" runat="server" Text='<%# Eval("Docteur") %>' />
              </td>
              <td>
                <asp:Label ID="PhotosClasseLabel" runat="server" 
                  Text='<%# Eval("PhotosClasse") %>' />
              </td>
              <td>
                <asp:Label ID="PhotosWebLabel" runat="server" Text='<%# Eval("PhotosWeb") %>' />
              </td>
              <td>
                <asp:Label ID="GateauxLabel" runat="server" Text='<%# Eval("Gateaux") %>' />
              </td>
              <td>
                <asp:Label ID="PbMedicauxLabel" runat="server" 
                  Text='<%# Eval("PbMedicaux") %>' />
              </td>
              <td>
                <asp:Label ID="DetailsMedicauxLabel" runat="server" 
                  Text='<%# Eval("DetailsMedicaux") %>' />
              </td>
            </tr>
          </AlternatingItemTemplate>
          <EmptyDataTemplate>
            <table id="Table1" runat="server" style="">
              <tr>
                <td>
                  No data was returned.</td>
              </tr>
            </table>
          </EmptyDataTemplate>
          <InsertItemTemplate>
            <tr style="">
              <td>
                <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
                  Text="Insert" />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                  Text="Clear" />
              </td>
              <td>
                <asp:TextBox ID="Eleve_IDTextBox" runat="server" 
                  Text='<%# Bind("Eleve_ID") %>' />
              </td>
              <td>
                <asp:TextBox ID="ContactIDTextBox" runat="server" 
                  Text='<%# Bind("ContactID") %>' />
              </td>
              <td>
                <asp:TextBox ID="NomTextBox" runat="server" Text='<%# Bind("Nom") %>' />
              </td>
              <td>
                <asp:TextBox ID="PrenomTextBox" runat="server" Text='<%# Bind("Prenom") %>' />
              </td>
              <td>
                <asp:TextBox ID="DateNaissanceTextBox" runat="server" 
                  Text='<%# Bind("DateNaissance") %>' />
              </td>
              <td>
                <asp:TextBox ID="NationaliteTextBox" runat="server" 
                  Text='<%# Bind("Nationalite") %>' />
              </td>
              <td>
                <asp:TextBox ID="PremiereRentreeTextBox" runat="server" 
                  Text='<%# Bind("PremiereRentree") %>' />
              </td>
              <td>
                <asp:TextBox ID="ClasseActuelleTextBox" runat="server" 
                  Text='<%# Bind("ClasseActuelle") %>' />
              </td>
              <td>
                <asp:TextBox ID="ActuellementInscritTextBox" runat="server" 
                  Text='<%# Bind("ActuellementInscrit") %>' />
              </td>
              <td>
                <asp:TextBox ID="PreInscritTextBox" runat="server" 
                  Text='<%# Bind("PreInscrit") %>' />
              </td>
              <td>
                <asp:TextBox ID="SexeTextBox" runat="server" Text='<%# Bind("Sexe") %>' />
              </td>
              <td>
                <asp:TextBox ID="RelationAutreParentTextBox" runat="server" 
                  Text='<%# Bind("RelationAutreParent") %>' />
              </td>
              <td>
                <asp:TextBox ID="NomAutreParentTextBox" runat="server" 
                  Text='<%# Bind("NomAutreParent") %>' />
              </td>
              <td>
                <asp:TextBox ID="PrenomAutreParentTextBox" runat="server" 
                  Text='<%# Bind("PrenomAutreParent") %>' />
              </td>
              <td>
                <asp:TextBox ID="AdresseAutreParentTextBox" runat="server" 
                  Text='<%# Bind("AdresseAutreParent") %>' />
              </td>
              <td>
                <asp:TextBox ID="FixeAutreParentTextBox" runat="server" 
                  Text='<%# Bind("FixeAutreParent") %>' />
              </td>
              <td>
                <asp:TextBox ID="PortableAutreParentTextBox" runat="server" 
                  Text='<%# Bind("PortableAutreParent") %>' />
              </td>
              <td>
                <asp:TextBox ID="EmailAutreParentTextBox" runat="server" 
                  Text='<%# Bind("EmailAutreParent") %>' />
              </td>
              <td>
                <asp:TextBox ID="DocteurTextBox" runat="server" Text='<%# Bind("Docteur") %>' />
              </td>
              <td>
                <asp:TextBox ID="PhotosClasseTextBox" runat="server" 
                  Text='<%# Bind("PhotosClasse") %>' />
              </td>
              <td>
                <asp:TextBox ID="PhotosWebTextBox" runat="server" 
                  Text='<%# Bind("PhotosWeb") %>' />
              </td>
              <td>
                <asp:TextBox ID="GateauxTextBox" runat="server" Text='<%# Bind("Gateaux") %>' />
              </td>
              <td>
                <asp:TextBox ID="PbMedicauxTextBox" runat="server" 
                  Text='<%# Bind("PbMedicaux") %>' />
              </td>
              <td>
                <asp:TextBox ID="DetailsMedicauxTextBox" runat="server" 
                  Text='<%# Bind("DetailsMedicaux") %>' />
              </td>
            </tr>
          </InsertItemTemplate>
          <LayoutTemplate>
            <table id="Table2" runat="server">
              <tr id="Tr1" runat="server">
                <td id="Td1" runat="server">
                  <table ID="itemPlaceholderContainer" runat="server" border="0" style="">
                    <tr id="Tr2" runat="server" style="">
                      <th id="Th1" runat="server">
                      </th>
                      <th id="Th2" runat="server">
                        Eleve_ID</th>
                      <th id="Th3" runat="server">
                        ContactID</th>
                      <th id="Th4" runat="server">
                        Nom</th>
                      <th id="Th5" runat="server">
                        Prenom</th>
                      <th id="Th6" runat="server">
                        DateNaissance</th>
                      <th id="Th7" runat="server">
                        Nationalite</th>
                      <th id="Th8" runat="server">
                        PremiereRentree</th>
                      <th id="Th9" runat="server">
                        ClasseActuelle</th>
                      <th id="Th10" runat="server">
                        ActuellementInscrit</th>
                      <th id="Th11" runat="server">
                        PreInscrit</th>
                      <th id="Th12" runat="server">
                        Sexe</th>
                      <th id="Th13" runat="server">
                        RelationAutreParent</th>
                      <th id="Th14" runat="server">
                        NomAutreParent</th>
                      <th id="Th15" runat="server">
                        PrenomAutreParent</th>
                      <th id="Th16" runat="server">
                        AdresseAutreParent</th>
                      <th id="Th17" runat="server">
                        FixeAutreParent</th>
                      <th id="Th18" runat="server">
                        PortableAutreParent</th>
                      <th id="Th19" runat="server">
                        EmailAutreParent</th>
                      <th id="Th20" runat="server">
                        Docteur</th>
                      <th id="Th21" runat="server">
                        PhotosClasse</th>
                      <th id="Th22" runat="server">
                        PhotosWeb</th>
                      <th id="Th23" runat="server">
                        Gateaux</th>
                      <th id="Th24" runat="server">
                        PbMedicaux</th>
                      <th id="Th25" runat="server">
                        DetailsMedicaux</th>
                    </tr>
                    <tr ID="itemPlaceholder" runat="server">
                    </tr>
                  </table>
                </td>
              </tr>
              <tr id="Tr3" runat="server">
                <td id="Td2" runat="server" style="">
                </td>
              </tr>
            </table>
          </LayoutTemplate>
          <EditItemTemplate>
            <tr style="">
              <td>
                <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
                  Text="Update" />
                <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
                  Text="Cancel" />
              </td>
              <td>
                <asp:TextBox ID="Eleve_IDTextBox" runat="server" 
                  Text='<%# Bind("Eleve_ID") %>' />
              </td>
              <td>
                <asp:TextBox ID="ContactIDTextBox" runat="server" 
                  Text='<%# Bind("ContactID") %>' />
              </td>
              <td>
                <asp:TextBox ID="NomTextBox" runat="server" Text='<%# Bind("Nom") %>' />
              </td>
              <td>
                <asp:TextBox ID="PrenomTextBox" runat="server" Text='<%# Bind("Prenom") %>' />
              </td>
              <td>
                <asp:TextBox ID="DateNaissanceTextBox" runat="server" 
                  Text='<%# Bind("DateNaissance") %>' />
              </td>
              <td>
                <asp:TextBox ID="NationaliteTextBox" runat="server" 
                  Text='<%# Bind("Nationalite") %>' />
              </td>
              <td>
                <asp:TextBox ID="PremiereRentreeTextBox" runat="server" 
                  Text='<%# Bind("PremiereRentree") %>' />
              </td>
              <td>
                <asp:TextBox ID="ClasseActuelleTextBox" runat="server" 
                  Text='<%# Bind("ClasseActuelle") %>' />
              </td>
              <td>
                <asp:TextBox ID="ActuellementInscritTextBox" runat="server" 
                  Text='<%# Bind("ActuellementInscrit") %>' />
              </td>
              <td>
                <asp:TextBox ID="PreInscritTextBox" runat="server" 
                  Text='<%# Bind("PreInscrit") %>' />
              </td>
              <td>
                <asp:TextBox ID="SexeTextBox" runat="server" Text='<%# Bind("Sexe") %>' />
              </td>
              <td>
                <asp:TextBox ID="RelationAutreParentTextBox" runat="server" 
                  Text='<%# Bind("RelationAutreParent") %>' />
              </td>
              <td>
                <asp:TextBox ID="NomAutreParentTextBox" runat="server" 
                  Text='<%# Bind("NomAutreParent") %>' />
              </td>
              <td>
                <asp:TextBox ID="PrenomAutreParentTextBox" runat="server" 
                  Text='<%# Bind("PrenomAutreParent") %>' />
              </td>
              <td>
                <asp:TextBox ID="AdresseAutreParentTextBox" runat="server" 
                  Text='<%# Bind("AdresseAutreParent") %>' />
              </td>
              <td>
                <asp:TextBox ID="FixeAutreParentTextBox" runat="server" 
                  Text='<%# Bind("FixeAutreParent") %>' />
              </td>
              <td>
                <asp:TextBox ID="PortableAutreParentTextBox" runat="server" 
                  Text='<%# Bind("PortableAutreParent") %>' />
              </td>
              <td>
                <asp:TextBox ID="EmailAutreParentTextBox" runat="server" 
                  Text='<%# Bind("EmailAutreParent") %>' />
              </td>
              <td>
                <asp:TextBox ID="DocteurTextBox" runat="server" Text='<%# Bind("Docteur") %>' />
              </td>
              <td>
                <asp:TextBox ID="PhotosClasseTextBox" runat="server" 
                  Text='<%# Bind("PhotosClasse") %>' />
              </td>
              <td>
                <asp:TextBox ID="PhotosWebTextBox" runat="server" 
                  Text='<%# Bind("PhotosWeb") %>' />
              </td>
              <td>
                <asp:TextBox ID="GateauxTextBox" runat="server" Text='<%# Bind("Gateaux") %>' />
              </td>
              <td>
                <asp:TextBox ID="PbMedicauxTextBox" runat="server" 
                  Text='<%# Bind("PbMedicaux") %>' />
              </td>
              <td>
                <asp:TextBox ID="DetailsMedicauxTextBox" runat="server" 
                  Text='<%# Bind("DetailsMedicaux") %>' />
              </td>
            </tr>
          </EditItemTemplate>
          <SelectedItemTemplate>
            <tr style="">
              <td>
                <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
              </td>
              <td>
                <asp:Label ID="Eleve_IDLabel" runat="server" Text='<%# Eval("Eleve_ID") %>' />
              </td>
              <td>
                <asp:Label ID="ContactIDLabel" runat="server" Text='<%# Eval("ContactID") %>' />
              </td>
              <td>
                <asp:Label ID="NomLabel" runat="server" Text='<%# Eval("Nom") %>' />
              </td>
              <td>
                <asp:Label ID="PrenomLabel" runat="server" Text='<%# Eval("Prenom") %>' />
              </td>
              <td>
                <asp:Label ID="DateNaissanceLabel" runat="server" 
                  Text='<%# Eval("DateNaissance") %>' />
              </td>
              <td>
                <asp:Label ID="NationaliteLabel" runat="server" 
                  Text='<%# Eval("Nationalite") %>' />
              </td>
              <td>
                <asp:Label ID="PremiereRentreeLabel" runat="server" 
                  Text='<%# Eval("PremiereRentree") %>' />
              </td>
              <td>
                <asp:Label ID="ClasseActuelleLabel" runat="server" 
                  Text='<%# Eval("ClasseActuelle") %>' />
              </td>
              <td>
                <asp:Label ID="ActuellementInscritLabel" runat="server" 
                  Text='<%# Eval("ActuellementInscrit") %>' />
              </td>
              <td>
                <asp:Label ID="PreInscritLabel" runat="server" 
                  Text='<%# Eval("PreInscrit") %>' />
              </td>
              <td>
                <asp:Label ID="SexeLabel" runat="server" Text='<%# Eval("Sexe") %>' />
              </td>
              <td>
                <asp:Label ID="RelationAutreParentLabel" runat="server" 
                  Text='<%# Eval("RelationAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="NomAutreParentLabel" runat="server" 
                  Text='<%# Eval("NomAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="PrenomAutreParentLabel" runat="server" 
                  Text='<%# Eval("PrenomAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="AdresseAutreParentLabel" runat="server" 
                  Text='<%# Eval("AdresseAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="FixeAutreParentLabel" runat="server" 
                  Text='<%# Eval("FixeAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="PortableAutreParentLabel" runat="server" 
                  Text='<%# Eval("PortableAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="EmailAutreParentLabel" runat="server" 
                  Text='<%# Eval("EmailAutreParent") %>' />
              </td>
              <td>
                <asp:Label ID="DocteurLabel" runat="server" Text='<%# Eval("Docteur") %>' />
              </td>
              <td>
                <asp:Label ID="PhotosClasseLabel" runat="server" 
                  Text='<%# Eval("PhotosClasse") %>' />
              </td>
              <td>
                <asp:Label ID="PhotosWebLabel" runat="server" Text='<%# Eval("PhotosWeb") %>' />
              </td>
              <td>
                <asp:Label ID="GateauxLabel" runat="server" Text='<%# Eval("Gateaux") %>' />
              </td>
              <td>
                <asp:Label ID="PbMedicauxLabel" runat="server" 
                  Text='<%# Eval("PbMedicaux") %>' />
              </td>
              <td>
                <asp:Label ID="DetailsMedicauxLabel" runat="server" 
                  Text='<%# Eval("DetailsMedicaux") %>' />
              </td>
            </tr>
          </SelectedItemTemplate>
        </asp:ListView>
      </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
      <tr style="">
        <td>
          <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
        </td>
        <td>
          <asp:Label ID="NomLabel" runat="server" Text='<%# Eval("Nom") %>' />
        </td>
        <td>
          <asp:Label ID="PrenomLabel" runat="server" Text='<%# Eval("Prenom") %>' />
        </td>
        <td>
          <asp:Label ID="VilleLabel" runat="server" Text='<%# Eval("Ville") %>' />
        </td>
        <td>
          <asp:Label ID="CodePostalLabel" runat="server" 
            Text='<%# Eval("CodePostal") %>' />
        </td>
        <td>
          <asp:Label ID="FixeLabel" runat="server" Text='<%# Eval("Fixe") %>' />
        </td>
        <td>
          <asp:Label ID="PortableLabel" runat="server" Text='<%# Eval("Portable") %>' />
        </td>
        <td>
          <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
        </td>
        <td>
          <asp:CheckBox ID="ActuellementInscritCheckBox" runat="server" 
    Checked='<%# Convert.ToBoolean(Eval("ActuellementInscrit")) %>'  />
        </td>
        <td>
          <asp:CheckBox ID="ComiteCheckBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("Comite")) %>' />
        </td>
        <td>
          <asp:CheckBox ID="NePasContacterCheckBox" runat="server"  Checked='<%# Convert.ToBoolean(Eval("NePasContacter")) %>' />
        </td>
        <td>
          <asp:Label ID="NotesLabel" runat="server" Text='<%# Eval("Notes") %>' />
        </td>
      </tr>
    </AlternatingItemTemplate>
    <EmptyDataTemplate>
      <table runat="server" style="">
        <tr>
          <td>
            No data was returned.</td>
        </tr>
      </table>
    </EmptyDataTemplate>
    <InsertItemTemplate>
      <tr style="">
        <td>
          <asp:Button ID="InsertButton" runat="server" CommandName="Insert" 
            Text="Insert" />
          <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
            Text="Clear" />
        </td>
        <td>
          <asp:TextBox ID="Contact_IDTextBox" runat="server" 
            Text='<%# Bind("Contact_ID") %>' />
        </td>
        <td>
          <asp:TextBox ID="NomTextBox" runat="server" Text='<%# Bind("Nom") %>' />
        </td>
        <td>
          <asp:TextBox ID="PrenomTextBox" runat="server" Text='<%# Bind("Prenom") %>' />
        </td>
        <td>
          <asp:TextBox ID="VilleTextBox" runat="server" Text='<%# Bind("Ville") %>' />
        </td>
        <td>
          <asp:TextBox ID="CodePostalTextBox" runat="server" 
            Text='<%# Bind("CodePostal") %>' />
        </td>
        <td>
          <asp:TextBox ID="FixeTextBox" runat="server" Text='<%# Bind("Fixe") %>' />
        </td>
        <td>
          <asp:TextBox ID="PortableTextBox" runat="server" 
            Text='<%# Bind("Portable") %>' />
        </td>
        <td>
          <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
        </td>
        <td>
          <asp:TextBox ID="ActuellementInscritTextBox" runat="server" 
            Text='<%# Bind("ActuellementInscrit") %>' />
        </td>
        <td>
          <asp:TextBox ID="ComiteTextBox" runat="server" Text='<%# Bind("Comite") %>' />
        </td>
        <td>
          <asp:TextBox ID="NePasContacterTextBox" runat="server" 
            Text='<%# Bind("NePasContacter") %>' />
        </td>
        <td>
          <asp:TextBox ID="NotesTextBox" runat="server" Text='<%# Bind("Notes") %>' />
        </td>
      </tr>
    </InsertItemTemplate>
    <LayoutTemplate>
      <table runat="server">
        <tr runat="server">
          <td runat="server">
            <table ID="itemPlaceholderContainer" runat="server" border="0" style="">
              <tr runat="server" style="">
                <th runat="server">
                </th>
                <th runat="server">
                  Nom</th>
                <th runat="server">
                  Prenom</th>
                <th runat="server">
                  Ville</th>
                <th runat="server">
                  CodePostal</th>
                <th runat="server">
                  Fixe</th>
                <th runat="server">
                  Portable</th>
                <th runat="server">
                  Email</th>
                <th runat="server">
                  ActuellementInscrit</th>
                <th runat="server">
                  Comite</th>
                <th runat="server">
                  NePasContacter</th>
                <th runat="server">
                  Notes</th>
              </tr>
              <tr ID="itemPlaceholder" runat="server">
              </tr>
            </table>
          </td>
        </tr>
        <tr runat="server">
          <td runat="server" style="">
            <asp:DataPager ID="DataPager1" runat="server">
              <Fields>
                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                  ShowLastPageButton="True" />
              </Fields>
            </asp:DataPager>
          </td>
        </tr>
      </table>
    </LayoutTemplate>
    <EditItemTemplate>
      <tr style="">
        <td>
          <asp:Button ID="UpdateButton" runat="server" CommandName="Update" 
            Text="Update" />
          <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" 
            Text="Cancel" />
        </td>
        <td>
          <asp:TextBox ID="Contact_IDTextBox" runat="server" 
            Text='<%# Bind("Contact_ID") %>' />
        </td>
        <td>
          <asp:TextBox ID="NomTextBox" runat="server" Text='<%# Bind("Nom") %>' />
        </td>
        <td>
          <asp:TextBox ID="PrenomTextBox" runat="server" Text='<%# Bind("Prenom") %>' />
        </td>
        <td>
          <asp:TextBox ID="VilleTextBox" runat="server" Text='<%# Bind("Ville") %>' />
        </td>
        <td>
          <asp:TextBox ID="CodePostalTextBox" runat="server" 
            Text='<%# Bind("CodePostal") %>' />
        </td>
        <td>
          <asp:TextBox ID="FixeTextBox" runat="server" Text='<%# Bind("Fixe") %>' />
        </td>
        <td>
          <asp:TextBox ID="PortableTextBox" runat="server" 
            Text='<%# Bind("Portable") %>' />
        </td>
        <td>
          <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
        </td>
        <td>
          <asp:TextBox ID="ActuellementInscritTextBox" runat="server" 
            Text='<%# Bind("ActuellementInscrit") %>' />
        </td>
        <td>
          <asp:TextBox ID="ComiteTextBox" runat="server" Text='<%# Bind("Comite") %>' />
        </td>
        <td>
          <asp:TextBox ID="NePasContacterTextBox" runat="server" 
            Text='<%# Bind("NePasContacter") %>' />
        </td>
        <td>
          <asp:TextBox ID="NotesTextBox" runat="server" Text='<%# Bind("Notes") %>' />
        </td>
      </tr>
    </EditItemTemplate>
    <SelectedItemTemplate>
      <tr style="">
        <td>
          <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
        </td>
        <td>
          <asp:Label ID="NomLabel" runat="server" Text='<%# Eval("Nom") %>' />
        </td>
        <td>
          <asp:Label ID="PrenomLabel" runat="server" Text='<%# Eval("Prenom") %>' />
        </td>
        <td>
          <asp:Label ID="VilleLabel" runat="server" Text='<%# Eval("Ville") %>' />
        </td>
        <td>
          <asp:Label ID="CodePostalLabel" runat="server" 
            Text='<%# Eval("CodePostal") %>' />
        </td>
        <td>
          <asp:Label ID="FixeLabel" runat="server" Text='<%# Eval("Fixe") %>' />
        </td>
        <td>
          <asp:Label ID="PortableLabel" runat="server" Text='<%# Eval("Portable") %>' />
        </td>
        <td>
          <asp:Label ID="EmailLabel" runat="server" Text='<%# Eval("Email") %>' />
        </td>
        <td>
          <asp:Label ID="ActuellementInscritLabel" runat="server" 
            Text='<%# Eval("ActuellementInscrit") %>' />
        </td>
        <td>
          <asp:Label ID="ComiteLabel" runat="server" Text='<%# Eval("Comite") %>' />
        </td>
        <td>
          <asp:Label ID="NePasContacterLabel" runat="server" 
            Text='<%# Eval("NePasContacter") %>' />
        </td>
        <td>
          <asp:Label ID="NotesLabel" runat="server" Text='<%# Eval("Notes") %>' />
        </td>
      </tr>
    </SelectedItemTemplate>
  </asp:ListView>
</asp:Content>

