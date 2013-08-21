using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public delegate void SendMessageToThePageHandler(string radioValue, string messageToThePage);


public partial class Admin_FamilySearch : System.Web.UI.UserControl
{
  public event SendMessageToThePageHandler sendMessageToThePage;

  protected void Page_Load(object sender, EventArgs e)
  {
    int radiovalue = 0;
    if (!Page.IsPostBack)
    {
      if (!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["s"]))
      {
        radiovalue = Convert.ToInt32(HttpContext.Current.Request.QueryString["s"]);
      }
      switch (radiovalue)
      {
        case -1:
          RadioButtonList1.SelectedValue = "All";
          break;

        case 0:
          RadioButtonList1.SelectedValue = "NotEnrolled";
          break;

        case 1:
          RadioButtonList1.SelectedValue = "Enrolled";
          break;

      }
      ElevesHelper eh = new ElevesHelper();
      ddlFamilySearch.DataSource = eh.FamilyNamesLookup();
      ddlFamilySearch.DataTextField = "Nom";
      ddlFamilySearch.DataValueField = "ID";
      ddlFamilySearch.DataBind();
    }
  }



  protected void Search_Click(object sender, EventArgs e)
  {
     if (ddlFamilySearch.SelectedItem.Value != null)
     {
       sendMessageToThePage(RadioButtonList1.SelectedValue, ddlFamilySearch.SelectedItem.Value + "~" + ddlFamilySearch.SelectedItem.Text);
     }
  }


  protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.RadioButtonList1.SelectedValue == "Family")
    {
      ddlFamilySearch.Enabled = true;
      Search.Enabled = true;
    }
    else
    {
      ddlFamilySearch.Enabled = false;
      Search.Enabled = false;
      sendMessageToThePage(RadioButtonList1.SelectedValue, "");
    }

  }
}
