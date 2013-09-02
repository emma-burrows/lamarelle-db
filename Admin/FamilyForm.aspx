<%@ Page Title="La Marelle - Famille" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FamilyForm.aspx.cs" Inherits="Admin_FamilyForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="FamilySearch.ascx" tagname="FamilySearch" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"/>

  <uc1:FamilySearch ID="FamilySearch1" runat="server" />

  <asp:ObjectDataSource ID="odsContacts" runat="server" 
    OldValuesParameterFormatString="original_{0}"
    SelectMethod="GetContactsById" 
    TypeName="ContactDataObject" 
    DataObjectTypeName="Contact" 
    InsertMethod="InsertContact" 
    UpdateMethod="UpdateContact" onselecting="odsContacts_Selecting" 
    oninserted="odsContacts_Inserted" DeleteMethod="DeleteContact">
     <DeleteParameters>
       <asp:Parameter Name="ContactID" Type="Int32" />
     </DeleteParameters>
     <SelectParameters>
       <asp:SessionParameter DefaultValue="0" Name="ContactID" 
         SessionField="ContactID" Type="Int32" />
       <asp:SessionParameter DefaultValue="0" Name="Status" 
         SessionField="Status" Type="Int32" />
     </SelectParameters>
  </asp:ObjectDataSource>


  <asp:ObjectDataSource ID="odsClasses" runat="server" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetClasses" 
    TypeName="ClassesDataObject">
  </asp:ObjectDataSource>


  <asp:Label ID="MessageLabel" runat="server" Text=""/>
  <asp:Label ID="SecretFamilyName" runat="server" Text="Toutes les familles" Visible="false" />
  <asp:ValidationSummary runat="server" ValidationGroup="Contacts"/>
    
  <div class="FamilyForm">

  
   <asp:FormView ID="fvContacts" runat="server" AllowPaging="True" 
      DataSourceID="odsContacts" DataKeyNames="Contact_ID" Width="100%" 
      onmodechanging="fvContacts_ModeChanging" ondatabound="fvContacts_DataBound" 
      oniteminserted="fvContacts_ItemInserted" oninit="fvContacts_Init" 
      onitemupdating="fvContacts_ItemUpdating" 
      oniteminserting="fvContacts_ItemInserting" 
      onprerender="fvContacts_PreRender" CssClass="form-horizontal">
    <PagerSettings Mode="NextPreviousFirstLast" Position="Top"/>
    
  <HeaderTemplate>
    <table width="100%" class="Pager" style="margin-left: auto; margin-right: auto;">
    <tr>
      <td><p class="Pager text-center"><asp:Button ID="NewHeaderButton"  Visible="true"
                   Text="Nouvelle famille"
                   CommandName="New"
                   RunAt="server" CssClass="FamilyButton"/></p></td>
    </tr>
    </table>
  
  </HeaderTemplate>
  <PagerTemplate>  

    <div class="row Pager" style="margin-left: auto; margin-right: auto;">
        <div class="span2">
          <asp:Button ID="FirstButton" CssClass="btn btn-small" CommandName="Page" CommandArgument="First" Text="<< Premier" RunAt="server"/>
          <asp:Button ID="PrevButton"  CssClass="btn btn-small" CommandName="Page" CommandArgument="Prev"  Text="< Pr&eacute;c&eacute;dent"  RunAt="server"/>
        </div>
        <div class="span2">Contact <asp:Label ID="CurrentPage" runat="server" Text="1" /> sur <asp:Label ID="TotalPages" runat="server" Text="1" /></div>
        <div class="span2">
          <asp:Button ID="NextButton"  CssClass="btn btn-small" CommandName="Page" CommandArgument="Next"  Text="Suivant >"  RunAt="server"/>
          <asp:Button ID="LastButton"  CssClass="btn btn-small" CommandName="Page" CommandArgument="Last"  Text="Dernier >>" RunAt="server"/>
        </div>
        <div class="span2"><asp:Button ID="NewPagerButton"  Visible="true"
                   Text="Nouvelle famille"
                   CommandName="New"
                   RunAt="server" CssClass="btn btn-small"/></div>
    </div>
  </PagerTemplate>
  
  <ItemTemplate>
  <!-- =========================== VIEW CONTACT ============================ -->
  <h2><asp:Label ID="Label3" runat="server" Text='<%# Eval("Nom").ToString().ToUpper() %>' /> 
  <asp:Label ID="Label4" runat="server" Text='<%# Eval("Prenom") %>' /></h2>

  <div class="row contact">
    <div class="span5 form-horizontal">
      <div class="control-group">
        <label class="control-label" for="NomLabel">Nom:</label>
        <div class="controls"><asp:Label ID="NomLabel" runat="server" Text='<%# Eval("Nom") %>' /></div>
      </div>

      <div class="control-group">
        <label class="control-label">Pr&eacute;nom:</label>
        <div class="controls"><asp:Label ID="PrenomLabel" runat="server" Text='<%# Eval("Prenom") %>' /></div>
      </div>

      <div class="control-group">
      <label class="control-label">Adresse 1:</label>
      <div class="controls"><asp:Label ID="Adresse1Label" runat="server" Text='<%# Eval("Adresse1") %>' /></div>
      </div>
      <div class="control-group">
        <label class="control-label">Adresse 2:</label>
        <div class="controls"><asp:Label ID="Adresse2Label" runat="server" Text='<%# Eval("Adresse2") %>' /></div>
      </div>
      <div class="control-group">
        <label class="control-label">Adresse 3:</label>
        <div class="controls"><asp:Label ID="Adresse3Label" runat="server" Text='<%# Eval("Adresse3") %>' /></div>
      </div>
      <div class="control-group">
        <label class="control-label">Ville:</label>
        <div class="controls"><asp:Label ID="VilleLabel" runat="server" Text='<%# Eval("Ville") %>' /></div>
      </div>
      <div class="control-group">
        <label class="control-label">CodePostal:</label>
        <div class="controls"><asp:Label ID="CodePostalLabel" runat="server" 
          Text='<%# Bind("CodePostal") %>' /></div>
      </div>
      <div class="control-group">
        <label class="control-label">Fixe:</label>
        <div class="controls"><asp:Label ID="FixeLabel" runat="server" Text='<%# Bind("Fixe") %>' /></div>  
      </div>
      <div class="control-group">
        <label class="control-label">Portable:</label>
        <div class="controls"><asp:Label ID="PortableLabel" runat="server" Text='<%# Bind("Portable") %>' /></div>
      </div>
      <div class="control-group">
        <label class="control-label">Email:</label>
        <div class="controls"><asp:Label ID="EmailLabel" runat="server" Text='<%# Bind("Email") %>' /></div>
      </div>

    </div>

  <div class="span6 form-horizontal">
    <div class="control-group">
      <label class="control-label">Notes:</label>
      <div class="controls NotesBox"><asp:Label ID="NotesLabel" runat="server" Text='<%# Eval("Notes").ToString().Replace("\r\n", "<br/>") %>' /></div>
    </div>
    <div class="control-group">
      <label class="control-label">Inscrit:</label>
      <div class="controls"><asp:CheckBox ID="ActuellementInscritCheckBox" runat="server" 
  Checked='<%# Convert.ToBoolean(Eval("ActuellementInscrit")) %>'  /> 
      </div>
    </div>
    <div class="control-group">
      <label class="control-label">Comit&eacute;:</label>
      <div class="controls">
      <asp:CheckBox ID="ComiteCheckBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("Comite")) %>' />
      </div>
    </div>
    <div class="control-group">
      <label class="control-label">NePasContacter:</label>
      <div class="controls"><asp:CheckBox ID="NePasContacterCheckBox" runat="server"  Checked='<%# Convert.ToBoolean(Eval("NePasContacter")) %>' /></div>
    </div>
   </div>
   </div>
   
     <p class="FamilyButtonList">
       <asp:Button ID="EditButton" Visible="false"
                   Text="Editer ce contact"                                           
                   CommandName="Edit"
                   RunAt="server" CssClass="btn btn-primary"/>

                        &nbsp;
       <asp:Button ID="DeleteButton" Visible="false"
                   Text="Supprimer le contact"
                   CommandName="Delete"
                   RunAt="server" CssClass="btn btn-danger"
                   OnClientClick="return confirm('Etes-vous s&ucirc;re de vouloir supprimer toute cette famille de mani&egrave;re permanente?'); "/>

                   
                          <asp:Label ID="Contact_IDLabel" runat="server" 
          Text='<%# Bind("Contact_ID") %>' /><br />

        </p>

  </ItemTemplate>
  
    
  <EditItemTemplate>
  <!-- =========================== EDIT CONTACT ============================ -->
  <div class="row contact edition">
  
  <div class="span5 form-horizontal">
      <div class="control-group">
        <label class="control-label">
          Nom:</label>
        <div class="controls">
          <asp:TextBox ID="NomTextBox" runat="server" Text='<%# Bind("Nom") %>' />
        </div>
      </div>
      <div class="control-group">
        <label class="control-label">
          Pr&eacute;nom:</label>
        <div class="controls">
          <asp:TextBox ID="PrenomTextBox" runat="server" Text='<%# Bind("Prenom") %>' />
        </div>
      </div>
      <div class="control-group">
        <label class="control-label">
          Adresse1:</label>
        <div class="controls">
          <asp:TextBox ID="Adresse1TextBox" runat="server" 
            Text='<%# Bind("Adresse1") %>' />
        </div>
      </div>
      <div class="control-group">
        <label class="control-label">
          Adresse2:</label>
        <div class="controls">
          <asp:TextBox ID="Adresse2TextBox" runat="server" 
            Text='<%# Bind("Adresse2") %>' />
        </div>
      </div>
      <div class="control-group">
        <label class="control-label">
          Adresse3:</label>
        <div class="controls">
          <asp:TextBox ID="Adresse3TextBox" runat="server" 
            Text='<%# Bind("Adresse3") %>' />
        </div>
      </div>
      <div class="control-group">
        <label class="control-label">
          Ville:</label>
        <div class="controls">
          <asp:TextBox ID="VilleTextBox" runat="server" Text='<%# Bind("Ville") %>' />
        </div>
      </div>
      <div class="control-group">
        <label class="control-label">
          CodePostal:</label>
        <div class="controls">
          <asp:TextBox ID="CodePostalTextBox" runat="server" 
            Text='<%# Bind("CodePostal") %>' />
        </div>
      </div>
      <div class="control-group">
        <label class="control-label">
          Fixe:</label>
        <div class="controls">
          <asp:TextBox ID="FixeTextBox" runat="server" Text='<%# Bind("Fixe") %>' />
        </div>
      </div>
      <div class="control-group">
        <label class="control-label">
          Portable:</label>
        <div class="controls">
          <asp:TextBox ID="PortableTextBox" runat="server" 
            Text='<%# Bind("Portable") %>' />
        </div>
      </div>
      <div class="control-group">
        <label class="control-label">
          Email:</label>
        <div class="controls">
          <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
        </div>
      </div>
  </div>
  <div class="span4 form-horizontal">
      <div class="control-group">
        <label class="control-label">Notes:</label>
        <div class="controls"><asp:TextBox ID="NotesTextBox" CssClass="NotesBox" runat="server" Text='<%# Bind("Notes") %>' TextMode="MultiLine" /></div>
    </div>
      <div class="control-group">
        <label class="control-label">Inscrit:</label>
        <div class="controls"><asp:CheckBox ID="ActuellementInscritCheckBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("ActuellementInscrit")) %>' /></div>
    </div>
      <div class="control-group">
        <label class="control-label">Comite:</label>
        <div class="controls"><asp:CheckBox ID="ComiteCheckBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("Comite")) %>' /></div>
    </div>
      <div class="control-group">
        <label class="control-label">Ne Pas Contacter:</label>
        <div class="controls"><asp:CheckBox ID="NePasContacterCheckBox" runat="server" Checked='<%# Convert.ToBoolean(Eval("NePasContacter")) %>' /></div>
    </div>
  </div>
  </div>
  
  <asp:Panel CssClass="FamilyButtonList" id="EditButtons" runat="server">
    <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Sauver" CssClass="btn btn-primary" />
    &nbsp;<asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Annuler" CssClass="btn " />
  </asp:Panel>

  <asp:Panel CssClass="FamilyButtonList" id="InsertButtons" runat="server">
    <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Cr&eacute;er la nouvelle famille" CssClass="btn btn-primary" />
    &nbsp;
    <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Annuler" CssClass="btn"  />
  </asp:Panel>

  </EditItemTemplate>
  </asp:FormView>
  <br />
    
  <!-- ================================================================================== -->
  <!-- =================================== ELEVES ======================================= -->
  <!-- ================================================================================== -->

    <asp:ObjectDataSource ID="odsEleves" runat="server" 
       DataObjectTypeName="Eleve" 
       InsertMethod="InsertEleve" 
       OldValuesParameterFormatString="original_{0}" 
       SelectMethod="GetElevesByContactID" 
       TypeName="EleveDataObject" 
       UpdateMethod="UpdateEleve"
       DeleteMethod="DeleteEleve">
       <DeleteParameters>
        <asp:Parameter Name="Eleve_ID" Type="Int32" />
       </DeleteParameters>
       <SelectParameters>
         <asp:ControlParameter ControlID="fvContacts" Name="contactid" 
           PropertyName="SelectedValue" Type="Int32" />
       </SelectParameters>
    </asp:ObjectDataSource>


    
  <asp:ListView ID="ListView1" runat="server" DataSourceID="odsEleves" 
      InsertItemPosition="LastItem" onitemupdating="ListView1_ItemUpdating" 
      oninit="ListView1_Init" ondatabound="ListView1_DataBound" 
      onitemdatabound="ListView1_ItemDataBound" DataKeyNames="Eleve_ID" 
      oniteminserting="ListView1_ItemInserting" >
    <LayoutTemplate>
      <div ID="itemPlaceholderContainer" runat="server">
        <div ID="itemPlaceholder" runat="server" />
      </div>
    </LayoutTemplate>
    
    <ItemTemplate>
    <div class="eleve container">
      <table width="100%">
        <tr>
          <td align="left">
            <h3>
              <asp:Label ID="NomLabel" runat="server" Text='<%# Eval("Nom").ToString().ToUpper() %>' />
              <asp:Label ID="PrenomLabel" runat="server" Text='<%# Eval("Prenom") %>' />
              <asp:Label ID="EleveIDLabel" runat="server" Text='<%# Bind("Eleve_ID") %>' Visible="false" />
            </h3>
          </td>
          <td align="right">
            <asp:Panel ID="Panel1" CssClass="inline right" runat="server">
              <asp:Button ID="ViewChildButton" Text="Voir cet enfant" runat="server" CssClass="btn ViewChild" data-toggle="collapse" data-parent="eleve" />
              &nbsp;
              <asp:Button ID="EditChildButton" Text="Editer cet enfant" CommandName="edit" runat="server"
                CssClass="btn" />
              &nbsp;
              <asp:Button ID="Button3" Visible="false" Text="Supprimer cet enfant" CommandName="Delete"
                runat="server" CssClass="btn btn-danger" OnClientClick="return confirm('Etes-vous s&ucirc;re de vouloir supprimer cet enfant de mani&egrave;re permanente?'); " />
            </asp:Panel>
          </td>
        </tr>
      </table>
      

      
      <div class="row details">
      <div class="span3 form-horizontal">
        <div class="control-group">
          <label class="control-label">Nom de famille:</label> 
          <div class="controls"><asp:Label ID="Label1" CssClass="TDValue" runat="server" Text='<%# Eval("Nom") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Pr&eacute;nom:</label> 
          <div class="controls"><asp:Label ID="Label2" CssClass="TDValue" runat="server" Text='<%# Eval("Prenom") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">N&eacute;(e) le:</label> 
          <div class="controls"><asp:Label ID="DateNaissanceLabel" CssClass="TDValue" runat="server" Text='<%# Eval("DateNaissance", "{0:d}") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Nationalit&eacute;:</label> 
          <div class="controls"><asp:Label ID="NationaliteLabel" CssClass="TDValue" runat="server" Text='<%# Eval("Nationalite") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Sexe:</label>
          <div class="controls"><asp:Label ID="SexeLabel" CssClass="TDValue" runat="server" Text='<%# Eval("Sexe") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Premi&egrave;re rentr&eacute;e:</label>
          <div class="controls"><asp:Label ID="PremiereRentreeLabel" CssClass="TDValue" runat="server" Text='<%# Eval("PremiereRentree", "{0:MMMM yyyy}") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Classe Actuelle:</label>
          <div class="controls"><asp:Label ID="ClasseActuelleLabel" CssClass="TDValue" runat="server" 
                       Text='<%# Eval("ClasseActuelle") %>' /></div>

        </div>
        <div class="control-group">
          <label class="control-label">Inscrit:</label>
          <div class="controls"><asp:CheckBox ID="ActuellementInscritCheckBox" CssClass="TDValue" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("ActuellementInscrit")) %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Pr&eacute;-inscrit/en attente:</label>
          <div class="controls"><asp:CheckBox ID="PreInscritCheckBox" CssClass="TDValue" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("PreInscrit")) %>' /></div>
        </div>
      </div>

          <div class="span1"></div>


      <div class="span3 form-horizontal">
        <h4>Autre parent</h4>
        <div class="control-group">
          <label class="control-label">Relation avec l'enfant:</label>
          <div class="controls"><asp:Label ID="RelationAutreParentLabel" CssClass="TDValue" runat="server" 
                       Text='<%# Eval("RelationAutreParent") %>' /></div>
        </div>
        
        <div class="control-group">
          <label class="control-label">Nom:</label>
          <div class="controls"><asp:Label ID="NomAutreParentLabel" CssClass="TDValue" runat="server" 
                       Text='<%# Eval("NomAutreParent") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Pr&eacute;nom:</label>
          <div class="controls"><asp:Label ID="PrenomAutreParentLabel" CssClass="TDValue" runat="server" 
                       Text='<%# Eval("PrenomAutreParent") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Portable:</label>
          <div class="controls"><asp:Label ID="PortableAutreParentLabel" CssClass="TDValue" runat="server" 
                       Text='<%# Eval("PortableAutreParent") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Fixe:</label>
          <div class="controls"><asp:Label ID="FixeAutreParentLabel" CssClass="TDValue" runat="server" 
                       Text='<%# Eval("FixeAutreParent") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Email:</label>
          <div class="controls"><asp:Label ID="EmailAutreParentLabel" CssClass="TDValue" runat="server" 
                       Text='<%# Eval("EmailAutreParent") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Adresse:</label>
          <div class="controls"><asp:Label ID="AdresseAutreParentLabel" CssClass="TDValue" runat="server" 
                       Text='<%# Eval("AdresseAutreParent") %>' /></div>
        </div>
      </div>
      
          <div class="span1"></div>

      
      <div class="span3 form-horizontal">
        <h4>D&eacute;tails m&eacute;dicaux</h4>
        <div class="control-group">
          <label class="control-label">Pb M&eacute;dicaux:</label>
          <div class="controls"><asp:CheckBox ID="PbMedicauxCheckBox" CssClass="TDValue" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("PbMedicaux")) %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Détails M&eacute;dicaux:</label>
          <div class="controls"><asp:Label ID="DetailsMedicauxLabel" runat="server" 
                      Text='<%# String.IsNullOrEmpty(Eval("DetailsMedicaux").ToString()) ? "(non)": Eval("DetailsMedicaux") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Docteur:</label>
          <div class="controls"><asp:Label ID="DocteurLabel" runat="server" 
                       Text='<%# String.IsNullOrEmpty(Eval("DetailsMedicaux").ToString()) ? "(non)": Eval("Docteur") %>'  Width="200" /></div>
        </div>
        <br />
        <h4>Permissions</h4>
        <div class="control-group">
          <label class="control-label">Photos en classe:</label>
          <div class="controls"><asp:CheckBox ID="PhotosClasseCheckBox" CssClass="TDValue" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("PhotosClasse")) %>' /></div>
        </div>

        <div class="control-group">
          <label class="control-label">Photos sur Web:</label>
          <div class="controls"><asp:CheckBox ID="PhotosWebCheckBox" CssClass="TDValue" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("PhotosWeb")) %>' /></div>
        </div>

        <div class="control-group">
          <label class="control-label">Gateaux:</label>
          <div class="controls"><asp:CheckBox ID="GateauxCheckBox" CssClass="TDValue" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("Gateaux")) %>' /></div>
        </div>
        <br />
      </div>
    </div>
&nbsp;
    </div>
    </ItemTemplate>
    
    <ItemSeparatorTemplate>
      <p class="divider">&nbsp;</p>
    </ItemSeparatorTemplate>
    
    <EditItemTemplate>
      <div class="eleve edition container">
              <table width="100%">
        <tr>
          <td align="left">
            <h3>
            Enfant: 
              <asp:Label ID="NomLabel" runat="server" Text='<%# Eval("Nom").ToString().ToUpper() %>' OnPreRender="Eleve_NomLabel_PreRender" />
              <asp:Label ID="PrenomLabel" runat="server" Text='<%# Eval("Prenom") %>' />
              <asp:TextBox ID="IdTextBox" Text='<%# Bind("Eleve_ID") %>' runat="server" Visible="false"/>
              <asp:TextBox ID="ContactIDTextBox" Text='<%# Bind("ContactId") %>' runat="server" Visible="false"/>
            </h3>
          </td>
          <td align="right">
          </td>
        </tr>
      </table>



      <div class="row">
      <div class="span3 form-horizontal">
        <div class="control-group">
          <label class="control-label">Nom de famille:</label> 
          <div class="controls"><asp:TextBox ID="NomTextBox" CssClass="TDValue" runat="server" Text='<%# Bind("Nom") %>' /></div>
        </div>
      <div class="control-group">
          <label class="control-label">Pr&eacute;nom:</label> 
          <div class="controls"><asp:TextBox ID="PrenomTextBox" CssClass="TDValue" runat="server" Text='<%# Bind("Prenom") %>' /></div>
        </div>
      <div class="control-group">
        <label class="control-label">N&eacute;(e) le:</label> 
        <div class="controls">
          <asp:TextBox ID="DateNaissanceTextBox" CssClass="TDValue" runat="server" Text='<%# Bind("DateNaissance", "{0:d}") %>' />
          <asp:CalendarExtender ID="DateNaissanceCalendarExtender" runat="server" TargetControlID="DateNaissanceTextBox" Format="dd/MM/yyyy" />
        </div>
      </div>
      <div class="control-group">
        <label class="control-label">Nationalit&eacute;:</label> 
        <div class="controls"><asp:TextBox ID="NationaliteTextBox" CssClass="TDValue" runat="server" Text='<%# Bind("Nationalite") %>' /></div>
      </div>
      <div class="control-group">
        <label class="control-label">Sexe:</label>
        <div class="controls"><asp:RadioButtonList SelectedValue='<%# Bind("Sexe") %>' ID="SexeRadioButtonList" CssClass="TDValue" RepeatDirection="Horizontal" runat="server">
          <asp:ListItem Text="M" Value="M"/>
          <asp:ListItem Text="F" Value="F" />
        </asp:RadioButtonList></div>
      </div>

      <div class="control-group">
        <label class="control-label">Premi&egrave;re rentr&eacute;e:</label>
        <div class="controls">
          <asp:TextBox ID="PremiereRentreeTextBox"  CssClass="TDValue" runat="server" Text='<%# Bind("PremiereRentree", "{0:MMMM yyyy}") %>' />
          <asp:CalendarExtender ID="PremiereRentreeCalendarExtender" runat="server" TargetControlID="PremiereRentreeTextBox" Format="MMMM yyyy" />
        </div>
      </div>
      <div class="control-group">
          <label class="control-label">Classe Actuelle:</label>
          <div class="controls"><asp:DropDownList ID="ClasseActuelleDropDown" CssClass="TDValue" runat="server" DataSourceID="odsClasses" DataTextField="Nom" DataValueField="Nom"></asp:DropDownList></div>
        </div>
        <div class="control-group">
          <label class="control-label">Inscrit:</label>
          <div class="controls"><asp:CheckBox ID="ActuellementInscritCheckBox" CssClass="TDValue" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("ActuellementInscrit")) %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Pr&eacute;-inscrit/en attente:</label>
          <div class="controls"><asp:CheckBox ID="PreInscritCheckBox" CssClass="TDValue" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("PreInscrit")) %>' /></div>
        </div>
      </div>
      
      <div class="span1"></div>
      <div class="span3 form-horizontal">
        <h4>Autre parent</h4>
        <div class="control-group">
          <label class="control-label">Relation avec l'enfant:</label>
          <div class="controls"><asp:TextBox ID="RelationAutreParentTextBox" CssClass="TDValue" runat="server" 
                       Text='<%# Bind("RelationAutreParent") %>' /></div>
        </div>
        
        <div class="control-group">
          <label class="control-label">Nom:</label>
          <div class="controls"><asp:TextBox ID="NomAutreParentTextBox" CssClass="TDValue" runat="server" 
                       Text='<%# Bind("NomAutreParent") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Pr&eacute;nom:</label>
          <div class="controls"><asp:TextBox ID="PrenomAutreParentTextBox" CssClass="TDValue" runat="server" 
                       Text='<%# Bind("PrenomAutreParent") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Portable:</label>
          <div class="controls"><asp:TextBox ID="PortableAutreParentTextBox" CssClass="TDValue" runat="server" 
                       Text='<%# Bind("PortableAutreParent") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Fixe:</label>
          <div class="controls"><asp:TextBox ID="FixeAutreParentTextBox" CssClass="TDValue" runat="server" 
                       Text='<%# Bind("FixeAutreParent") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Email:</label>
          <div class="controls"><asp:TextBox ID="EmailAutreParentTextBox" CssClass="TDValue" runat="server" 
                       Text='<%# Bind("EmailAutreParent") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Adresse:</label>
          <div class="controls"><asp:TextBox ID="AdresseAutreParentTextBox" CssClass="TDValue" runat="server" 
                       Text='<%# Bind("AdresseAutreParent") %>' /></div>
        </div>
      </div>

      <div class="span1"></div>

      <div class="span3 form-horizontal">
        <h4>D&eacute;tails m&eacute;dicaux</h4>
        <div class="control-group">
          <label class="control-label">Pb M&eacute;dicaux:</label>
          <div class="controls"><asp:CheckBox ID="PbMedicauxCheckBox" CssClass="TDValue" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("PbMedicaux")) %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">D&eacute;tails m&eacute;dicaux:</label>
          <div class="controls"><asp:TextBox ID="DetailsMedicauxTextBox"  CssClass="TDValue" runat="server" 
          Text='<%# Bind("DetailsMedicaux") %>' /></div>
        </div>
        <div class="control-group">
          <label class="control-label">Docteur:</label>
          <div class="controls"><asp:TextBox ID="DocteurTextBox" CssClass="TDValue" runat="server" Text='<%# Bind("Docteur") %>' Width="200" TextMode="MultiLine" /></div>
        </div>
        <br />
        <h4>Permissions</h4>
        <div class="control-group">
          <label class="control-label">Photos en classe:</label>
          <div class="controls"><asp:CheckBox ID="PhotosClasseCheckBox" CssClass="TDValue" runat="server" Text=""
          Checked='<%# Convert.ToBoolean(Eval("PhotosClasse")) %>' /></div>
        </div>
        
        <div class="control-group">
          <label class="control-label">Photos sur Web:</label>
          <div class="controls"><asp:CheckBox ID="PhotosWebCheckBox" CssClass="TDValue" runat="server" Text=""
          Checked='<%# Convert.ToBoolean(Eval("PhotosWeb")) %>' /></div>
        </div>
        
        <div class="control-group">
          <label class="control-label">Gateaux:</label>
          <div class="controls"><asp:CheckBox ID="GateauxCheckBox" CssClass="TDValue" runat="server" Text=""
          Checked='<%# Convert.ToBoolean(Eval("Gateaux")) %>' /></div>
        </div>
        <br />
      </div>
     </div>



        <asp:Panel CssClass="FamilyButtonList" id="EditButtons" Visible="false" runat="server">
          <asp:Button ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Sauver" CssClass="btn btn-primary" />
          &nbsp;<asp:Button ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Annuler" CssClass="btn" />
        </asp:Panel>

        <asp:Panel CssClass="FamilyButtonList" id="InsertButtons" runat="server">
          <asp:Button ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insérer cet enfant" CssClass="btn btn-primary" />
          &nbsp;
          <asp:Button ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Annuler" CssClass="btn" />
        </asp:Panel>

      </div>
    </EditItemTemplate>
    
    <EmptyDataTemplate>
      <span>Ce contact n'a pas d'enfants actuellement. Vous pouvez ajouter un enfant ci-dessous.</span>
    </EmptyDataTemplate>
       
    
  </asp:ListView>
    

  </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Jscript" runat="server">
    <script type="text/javascript">
      function toggleDetails(element) {
        if (element.parents(".eleve").children(".details").is(":hidden")) {
          element.parents(".eleve").children(".details").slideDown("slow");
          element.children("span.arrow").remove();
          element.prepend("<span class='arrow'>▲</span>");
          if (element.parents(".eleve").find(".ViewChild").val() == "Voir cet enfant") {
            element.parents(".eleve").find(".ViewChild").val("Cacher cet enfant");
          }
        }
        else {
          element.parents(".eleve").children(".details").slideUp("slow");
          if (element.children("span.arrow")) {
            element.children("span.arrow").remove();
            element.prepend("<span class='arrow'>▼</span>");
          }
          if (element.parents(".eleve").find(".ViewChild").val() == "Cacher cet enfant") {
            element.parents(".eleve").find(".ViewChild").val("Voir cet enfant");
          }
        }
      }

      $(document).ready(function() {
        // Toggle the expanding Eleve divs
        $(".eleve .details").hide();
        $(".eleve h3").prepend("<span class='arrow'>▼</span>");

        $(".eleve h3").click(function(event) {
          toggleDetails($(this));
          // Stop the link click from doing its normal thing
          event.preventDefault();
        });

        $(".ViewChild").click(function(event) {
          toggleDetails($(this));
          event.preventDefault();
        });

      });
      
      </script>

</asp:Content>
