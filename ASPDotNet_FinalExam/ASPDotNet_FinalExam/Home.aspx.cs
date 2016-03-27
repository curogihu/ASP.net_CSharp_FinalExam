using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPDotNet_FinalExam
{
    public partial class Home : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlConnection Con;
        System.Data.SqlClient.SqlDataAdapter Langda;
        System.Data.DataSet Langds;
        System.Data.SqlClient.SqlDataAdapter Menuda;
        System.Data.DataSet Menuds;
        System.Data.SqlClient.SqlDataAdapter Menu2da;
        System.Data.DataSet Menu2ds;
        int LangRecordCount, MenuRecordCount, Menu2RecordCount;
        string LANGUAGE_TABLE_NAME = "Languages";
        string MENUITEMS_TABLE_NAME = "MenuItems";

        protected void Page_Load(object sender, EventArgs e)
        {
            fillddlang();
            fillmenu();
        }

        protected void fillddlang()
        {
            Con = new System.Data.SqlClient.SqlConnection();
            Con.ConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\FakeSite.mdf;Integrated Security=True";
            Con.Open();
            String SqlStr = "SELECT * FROM " + LANGUAGE_TABLE_NAME;
            System.Data.SqlClient.SqlConnection SC = new System.Data.SqlClient.SqlConnection(Con.ConnectionString);
            Langda = new System.Data.SqlClient.SqlDataAdapter(SqlStr, Con);
            Langds = new System.Data.DataSet();
            Langda.Fill(Langds, LANGUAGE_TABLE_NAME);
            LangRecordCount = Langds.Tables[LANGUAGE_TABLE_NAME].Rows.Count;

            ListItem newItem = new ListItem();

            foreach (System.Data.DataRow dr in Langds.Tables[LANGUAGE_TABLE_NAME].Rows)
            {
                newItem = new ListItem();
                newItem.Text = dr["LanguageDescription"].ToString();
                newItem.Value = dr["LanguageID"].ToString();
                ddlLang.Items.Add(newItem);
            }
            //ddlLang.Items.Add(ipaddr);
            //Con.Close();
        }
        
        protected void fillmenu()
        {
            int sel;
            sel = Convert.ToInt32(ddlLang.SelectedValue);

            String SqlStr = "SELECT * From " + MENUITEMS_TABLE_NAME + " WHERE ParentID = 0 AND LanguageID = " + sel.ToString();
            Menuda = new System.Data.SqlClient.SqlDataAdapter(SqlStr, Con);
            Menuds = new System.Data.DataSet();
            Menuda.Fill(Menuds, MENUITEMS_TABLE_NAME);

            MenuRecordCount = Menuds.Tables[MENUITEMS_TABLE_NAME].Rows.Count;
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder("<ul>");
            int ParentId;

            foreach (System.Data.DataRow dr in Menuds.Tables[MENUITEMS_TABLE_NAME].Rows)
            {
                sb.Append(String.Format("<li class='has-sub'><a href='" + dr["TargetPage"].ToString() + "'><span>" + dr["MenuDescription"].ToString() + "</span></a>"));

                ParentId = Convert.ToInt32(dr["ManuID"]);
                String SecLevelSqlStr = "SELECT * FROM " + MENUITEMS_TABLE_NAME + " WHERE ParentID = " + Convert.ToInt32(ParentId) + " and LanguageID = " + sel.ToString();
                Menu2da = new System.Data.SqlClient.SqlDataAdapter(SecLevelSqlStr, Con);
                Menu2ds = new System.Data.DataSet();
                Menu2da.Fill(Menu2ds, "Menu2s");
                Menu2RecordCount = Menu2ds.Tables["Menu2s"].Rows.Count;

                sb.Append(String.Format("<ul>"));
                foreach (System.Data.DataRow dr2 in Menu2ds.Tables["Menu2s"].Rows)
                {

                    sb.Append(String.Format("<li><a href='" + dr2["TargetPage"].ToString() + "'><span>" + dr2["MenuDescription"].ToString() + "</span></a></Li>"));
                }
                sb.Append(String.Format("</Li>"));
                sb.Append("</ul>");
            }
            sb.Append("</ul>");
            ltMenus.Text = sb.ToString();

        }
        
    }
}