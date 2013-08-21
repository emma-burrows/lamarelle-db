using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for PageHelper
/// </summary>
public static class PageHelper
{

    public static ArrayList ColumnNames(DataTable table)
    {
        ArrayList cols = new ArrayList();
        // For each DataTable, print the ColumnName.

        foreach (DataColumn column in table.Columns)
        {
            cols.Add(column.ColumnName);
        }
        return cols;
    }

    public static TextBox[] CreateTextBoxes(ArrayList cols)
    {
        TextBox[] tbs = new TextBox[cols.Count];
       
        for (int index = 0; index < tbs.Length; index++)
        {
            tbs[index] = new TextBox();
            tbs[index].ID = "tb" + cols[index].ToString();
            if (cols[index].ToString() == "ID"){
              tbs[index].Visible = false;
            }
            if (cols[index].ToString() == "motdepasse")
            {
              tbs[index].TextMode = TextBoxMode.Password;
            }
        }
        return tbs;
    }
}
