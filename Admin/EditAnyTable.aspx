<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditAnyTable.aspx.cs" Inherits="EditAnyTable" %>

<%@ Register src="FamilySearch.ascx" tagname="FamilySearch" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<p>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </p>
    
    
    <asp:Panel ID="AllPanel" runat="server">
    <p>Editer la table intitulée <asp:Literal ID="litTableName" runat="server"/> </p>
    

    <asp:Panel ID="InputPanel" runat="server">
    </asp:Panel>     
    
<br />

    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
    Text="Enregistrer dans la base" />

    <br />
    <br />


<p>Tous les résultats dans cette table:</p>

    <asp:GridView ID="GridView1" runat="server" 
        EnableSortingAndPagingCallbacks="True" onrowdeleting="GridView1_RowDeleting" 
        DataKeyNames="ID" ondatabinding="GridView1_DataBinding" 
        ondatabound="GridView1_DataBound" onrowdatabound="GridView1_RowDataBound" 
        onrowupdating="GridView1_RowUpdating" 
        onrowcancelingedit="GridView1_RowCancelingEdit" 
        onrowediting="GridView1_RowEditing">
    </asp:GridView>

  </asp:Panel>
</asp:Content>

