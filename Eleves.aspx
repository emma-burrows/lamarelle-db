<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master"
EnableEventValidation="false" EnableViewState="true" AutoEventWireup="true" CodeFile="Eleves.aspx.cs" Inherits="Admin_Eleves" %>



<asp:Content ID="Content1" ContentPlaceHolderID="Jscript" Runat="Server">

 


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <%@ Register src="Admin/EleveSearch.ascx" tagname="EleveSearch" tagprefix="uc1" %>
  <asp:ScriptManager ID="ScriptManager1" runat="server" />

 
  <uc1:EleveSearch ID="EleveSearch1" runat="server" />
  
  <a name="top"></a>
  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    DataObjectTypeName="EleveWithContacts" 
    OldValuesParameterFormatString="original_{0}" 
    SelectMethod="GetElevesWithContacts" TypeName="ElevesWithContactsDataObject" 
    UpdateMethod="UpdateEleveWithContacts" SortParameterName="SortColumns" 
    onselecting="ObjectDataSource1_Selecting"></asp:ObjectDataSource>
    

  <asp:Panel ID="pnClasses" runat="server" Visible="false">
    <div class="form-horizontal">
      <div class="control-group" style="margin-bottom: 0px;">
        <label class="control-label"><asp:Literal ID="FraLabel" runat="server">Section francophone:</asp:Literal></label>
        <div class="controls">
          <asp:Repeater ID="Repeater1" Visible="true"  runat="server">
            <ItemTemplate><a href="#<%# Eval("classe") %>" class="FamilyButton"><%# Eval("classe") %></a></ItemTemplate>
            <SeparatorTemplate> - </SeparatorTemplate>
          </asp:Repeater>
        </div>
      </div>

      <div class="control-group" style="margin-bottom: 0px;">
        <label class="control-label"><asp:Literal ID="FleLabel" runat="server">Section anglophone:</asp:Literal></label>
        <div class="controls">
          <asp:Repeater ID="Repeater2" Visible="true"  runat="server">
            <ItemTemplate><a href="#<%# Eval("classe") %>" class="FamilyButton"><%# Eval("classe") %></a></ItemTemplate>
            <SeparatorTemplate> - </SeparatorTemplate>
          </asp:Repeater>
        </div>
      </div>
    </div>
  </asp:Panel>
 <br /> 


    
<asp:Button ID="FichierExcel" CssClass="btn" runat="server" onclick="Button1_Click" 
    Text="Fichier Excel" />
  <asp:Button ID="Emails" CssClass="btn" runat="server" Text="Emails des parents inscrits" 
    onclick="Emails_Click" />
  <asp:Label ID="MessageLabel" runat="server" Text="" />

    
  <asp:GridView ID="GridView1" runat="server" AllowPaging="True" CssClass="FamilyForm"
    AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" 
    AllowSorting="True" DataKeyNames="Eleve_ID,ContactID" PageSize="150" 
    onrowupdating="GridView1_RowUpdating" CellPadding="4" ForeColor="#333333" 
    GridLines="None" onrowdatabound="GridView1_RowDataBound" >
    <PagerSettings FirstPageText="&amp;lt;&amp;lt; Premi&amp;egrave;re Page" 
      LastPageText="Derni&amp;egrave;re page &amp;gt;&amp;gt;" 
      Position="TopAndBottom" />
    <RowStyle BackColor="#E3EAEB" />
    <Columns>
      <asp:CommandField ShowEditButton="True" ButtonType="Image" 
        CancelText="Annuler" DeleteText="Suprimer" EditText="Editer" 
        InsertText="Ins&amp;eacute;rer" NewText="Nouveau" 
        SelectText="S&amp;eacute;lectionner" UpdateText="Sauver" Visible="false" 
        CancelImageUrl="~/Images/cancelsm.png" EditImageUrl="~/Images/editsm.png" 
        UpdateImageUrl="~/Images/updatesm.png"/>
        
      <asp:TemplateField HeaderText="Inscrit" SortExpression="ActuellementInscrit, Preinscrit" Visible="false">
      <ItemTemplate>
        <asp:CheckBox ID="ActuellementInscritCheckBox" CssClass="TDValue checkbox" runat="server" Text="Inscrit(e)" Checked='<%# Convert.ToBoolean(Eval("ActuellementInscrit")) %>' /><br />
        <asp:CheckBox ID="PreinscritCheckBox" CssClass="TDValue checkbox" runat="server" Text="En attente" Checked='<%# Convert.ToBoolean(Eval("Preinscrit")) %>' />
      </ItemTemplate>
      <EditItemTemplate>
        <asp:CheckBox ID="ActuellementInscritCheckBox" CssClass="TDValue checkbox" runat="server" Text="Inscrit(e)" Checked='<%# Convert.ToBoolean(Eval("ActuellementInscrit")) %>' /><br />
        <asp:CheckBox ID="PreinscritCheckBox" CssClass="TDValue checkbox" runat="server" Text="En attente" Checked='<%# Convert.ToBoolean(Eval("Preinscrit")) %>' />
      </EditItemTemplate>
      </asp:TemplateField>

      <asp:TemplateField HeaderText="Nom" SortExpression="Nom">
        <ItemTemplate>
          <b><asp:Label ID="Label2" runat="server" Text='<%# Eval("Nom").ToString().ToUpper() + " " + Eval("Prenom")  %>'></asp:Label></b>
        </ItemTemplate>
        <EditItemTemplate>
          <span class="TDLabels">Pr&eacute;nom:</span>
          <asp:TextBox ID="PrenomTextBox" runat="server" Text='<%# Bind("Prenom") %>' />
           <span class="TDLabels">Nom:</span>
          <asp:TextBox ID="NomTextBox" runat="server" Text='<%# Bind("Nom") %>' /><br />
       </EditItemTemplate>
      </asp:TemplateField>
      
      <asp:BoundField DataField="DateNaissance" HeaderText="Date Naissance" 
        SortExpression="DateNaissance" ApplyFormatInEditMode="True" 
        DataFormatString="{0:d}" />
        
      <asp:BoundField DataField="Nationalite" HeaderText="Nationalit&eacute;" 
        SortExpression="Nationalite" Visible="True" />
        
      <asp:BoundField DataField="Sexe" HeaderText="Sexe" SortExpression="Sexe" />
      
      <asp:BoundField DataField="PremiereRentree" HeaderText="Premi&egrave;re Rentr&eacute;e" 
        SortExpression="PremiereRentree" DataFormatString="{0:MMM yyyy}" />
        
      <asp:BoundField DataField="ClasseActuelle" HeaderText="Classe" 
        SortExpression="ClasseActuelle" />
      
      <asp:TemplateField HeaderText="Probl&egrave;mes et Permissions" Visible="false" SortExpression="PbMedicaux, PhotosClasse, PhotosWeb, Gateaux">
        <ItemTemplate>
          <asp:CheckBox ID="PbMedicauxCheckBox" CssClass="TDValue checkbox" runat="server" Text="Pb. médicaux" Checked='<%# Convert.ToBoolean(Eval("PbMedicaux")) %>' /><br />
          <asp:Label ID="DetailsMedicauxLabel" runat="server" Text='<%# Eval("DetailsMedicaux") + "\n"%>'></asp:Label>
          <br></br>
          <asp:Panel ID="MorePermissionsItem" class="autre-contact" runat="server">
          <asp:CheckBox ID="PhotosClasseCheckBox" CssClass="TDValue checkbox" runat="server" Text="Photos en classe" Checked='<%# Convert.ToBoolean(Eval("PhotosClasse")) %>' /><br />
          <asp:CheckBox ID="PhotosWebCheckBox" CssClass="TDValue checkbox" runat="server" Text="Photos sur Web" Checked='<%# Convert.ToBoolean(Eval("PhotosWeb")) %>' /><br />
          <asp:CheckBox ID="GateauxCheckBox" CssClass="TDValue checkbox" runat="server" Text="Gateaux" Checked='<%# Convert.ToBoolean(Eval("Gateaux")) %>' />
          </asp:Panel>
        </ItemTemplate>
        <EditItemTemplate>
           <asp:CheckBox ID="PbMedicauxCheckBox" runat="server" Text="Pb. médicaux"
            Checked='<%# Convert.ToBoolean(Eval("PbMedicaux")) %>' /><br />
           <span class="TDLabels">Détails:</span>
            <asp:TextBox ID="DetailsMedicauxTextBox" runat="server" Text='<%# Bind("DetailsMedicaux") %>' /><br />

          <asp:Panel ID="MorePermissionsEdit" class="autre-contact" runat="server">
             <asp:CheckBox ID="PhotosClasseCheckBox" runat="server" Text="Photos en classe"
              Checked='<%# Convert.ToBoolean(Eval("PhotosClasse")) %>' /><br />
            <asp:CheckBox ID="PhotosWebCheckBox" runat="server" Text="Photos sur le Web"
              Checked='<%# Convert.ToBoolean(Eval("PhotosWeb")) %>' /><br />
            <asp:CheckBox ID="GateauxCheckBox" runat="server" Text="Gateaux"
              Checked='<%# Convert.ToBoolean(Eval("Gateaux")) %>' />
          </asp:Panel>
          
        </EditItemTemplate>
      </asp:TemplateField>

      <asp:TemplateField HeaderText="Contacts" SortExpression="NomContact">
        <ItemTemplate>
          <asp:HyperLink ID="NomContactHyperLink" runat="server" NavigateUrl='<%# "Admin/FamilyForm.aspx?c=" + Eval("ContactId") %>'
            Text='<%# Eval("PrenomContact") + " " + Eval("NomContact").ToString().ToUpper() %>'></asp:HyperLink>
            
          <asp:Panel ID="MoreContactsItem" runat="server" Visible="false">
            <span class="TDLabels">Autre contact:</span><br />
            <asp:HyperLink ID="AutreContactHyperLink" runat="server" NavigateUrl='<%# "Admin/FamilyForm.aspx?c=" + Eval("ContactId") %>'
            Text='<%# Eval("PrenomAutreParent") + " " + Eval("NomAutreParent").ToString().ToUpper() + " (" + Eval("RelationAutreParent") + ")" %>'></asp:HyperLink>
          </asp:Panel>
        </ItemTemplate>
        <EditItemTemplate>
          <span class="TDLabels">Pr&eacute;nom:</span>
          <asp:TextBox ID="PrenomContactTextBox" runat="server" Text='<%# Bind("PrenomContact") %>' /><br />
           <span class="TDLabels">Nom:</span>
          <asp:TextBox ID="NomContactTextBox" runat="server" Text='<%# Bind("NomContact") %>' /><br />
          
          <asp:Panel ID="MoreContactsEdit" runat="server" Visible="false">
            <span class="TDLabels">Autre contact:</span><br />
            <span class="TDLabels">Pr&eacute;nom:</span>
            <asp:TextBox ID="PrenomAutreParentTextBox" runat="server" Text='<%# Bind("PrenomAutreParent") %>' /><br />
            <span class="TDLabels">Nom:</span>
            <asp:TextBox ID="NomAutreParentTextBox" runat="server" Text='<%# Bind("NomAutreParent") %>' /><br />
            <span class="TDLabels">Relation:</span>
            <asp:TextBox ID="RelationAutreParentTextBox" runat="server" Text='<%# Bind("RelationAutreParent") %>' />
          </asp:Panel>
       </EditItemTemplate>
      </asp:TemplateField>
      
      <asp:TemplateField HeaderText="Téléphones" SortExpression="Fixe">
        <ItemTemplate>
          <asp:Label ID="Label3" runat="server" Text='<%# Eval("Fixe")%>'></asp:Label><br/>
		      <asp:Label ID="Label4" runat="server" Text='<%# Eval("Portable")%>' Font-Bold="True"></asp:Label><br />
		      
          <asp:Panel ID="MoreTelephonesItem" runat="server" Visible="false">
		        <span class="TDLabels">Autre contact:</span><br />
		        <asp:Label ID="FixeAutreParentLabel" runat="server" Text='<%# Eval("FixeAutreParent")%>'></asp:Label><br/>
		        <asp:Label ID="PortableAutreParentLabel" runat="server" Text='<%# Eval("PortableAutreParent")%>' Font-Bold="True"></asp:Label>
          </asp:Panel>
        </ItemTemplate>
        <EditItemTemplate>
          <span class="TDLabels">Fixe:</span>
          <asp:TextBox ID="FixeTextBox" runat="server" Text='<%# Bind("Fixe") %>' /><br />
          <span class="TDLabels">Portable:</span>
          <asp:TextBox ID="PortableTextBox" runat="server" Text='<%# Bind("Portable") %>' /><br />

          <asp:Panel ID="MoreTelephonesEdit" runat="server" Visible="false">
            <span class="TDLabels">Autre contact:</span><br />
            <span class="TDLabels">Fixe:</span>
            <asp:TextBox ID="FixeAutreParentTextBox" runat="server" Text='<%# Bind("FixeAutreParent") %>' /><br />
            <span class="TDLabels">Portable:</span>
            <asp:TextBox ID="PortableAutreParentTextBox" runat="server" Text='<%# Bind("PortableAutreParent") %>' />
          </asp:Panel>
        </EditItemTemplate>
    
      </asp:TemplateField>
      
      <asp:TemplateField HeaderText="Email" SortExpression="Email">
        <ItemTemplate>
          <asp:HyperLink ID="EmailHyperLink" runat="server" NavigateUrl='<%# "mailto:" + Eval("Email") %>'
            Text='<%# Eval("Email") %>'></asp:HyperLink>
            
          <asp:Panel ID="MoreEmailItem" runat="server" Visible="false">
            <span class="TDLabels">Autre contact:</span><br />
            <asp:HyperLink ID="AutreEmailHyperLink" runat="server" NavigateUrl='<%# "mailto:" + Eval("EmailAutreParent") %>'
            Text='<%# Eval("EmailAutreParent") %>'></asp:HyperLink>
          </asp:Panel>
        </ItemTemplate>
        <EditItemTemplate>
          <span class="TDLabels">Email principal:</span>
          <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' /><br />
       
          <asp:Panel ID="MoreEmailEdit" runat="server" Visible="false">
            <span class="TDLabels">Autre email:</span>
            <asp:TextBox ID="PrenomAutreParentTextBox" runat="server" Text='<%# Bind("EmailAutreParent") %>' /><br />
          </asp:Panel>
       </EditItemTemplate>
      </asp:TemplateField>

      
      <asp:TemplateField HeaderText="Code Postal" SortExpression="CodePostal">
        <ItemTemplate>
          <asp:HyperLink ID="CodePostalHyperLink" runat="server" NavigateUrl='<%# "https://maps.google.co.uk/maps?q=" + Eval("CodePostal").ToString().Replace(" ", "+") %>'
            Text='<%# Eval("CodePostal") %>'></asp:HyperLink>
        </ItemTemplate>
        <EditItemTemplate>
          <asp:TextBox ID="CodePostalTextBox" runat="server" Text='<%# Bind("CodePostal") %>' /><br />
        </EditItemTemplate>
      
      </asp:TemplateField>
        
    </Columns>
    
    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    <EditRowStyle BackColor="#7C6F57" />
    <AlternatingRowStyle BackColor="White" />
  </asp:GridView>



</asp:Content>

