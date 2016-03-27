using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShowMenu
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
        protected void Page_Load(object sender, EventArgs e)
        {
            fillddlang();
            fillmenu();
        }
        protected void  fillddlang()
        {
            Con = new System.Data.SqlClient.SqlConnection();
            Con.ConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Database1.mdf;Integrated Security=True";
            Con.Open();
            String SqlStr = "SELECT * FROM Languages";
            System.Data.SqlClient.SqlConnection SC = new System.Data.SqlClient.SqlConnection(Con.ConnectionString);
            Langda = new System.Data.SqlClient.SqlDataAdapter(SqlStr, Con);
            Langds = new System.Data.DataSet();
            Langda.Fill(Langds, "Languages");
            LangRecordCount = Langds.Tables["Languages"].Rows.Count;

            ListItem newItem = new ListItem();

            foreach (System.Data.DataRow dr in Langds.Tables["Languages"].Rows)
            {
                newItem = new ListItem();
                newItem.Text = dr["LangDesk"].ToString();
                newItem.Value = dr["LangID"].ToString();
                ddlLang.Items.Add(newItem);
            }
            //ddlLang.Items.Add(ipaddr);
            //Con.Close();
        }
        protected void fillmenu()
        {
            int sel;
            sel = Convert.ToInt32( ddlLang.SelectedValue);
            
            String SqlStr = "SELECT * FROM Menu WHERE ParentID=0 AND LangID=" + sel.ToString();
            //System.Data.SqlClient.SqlConnection SC = new System.Data.SqlClient.SqlConnection(Con.ConnectionString);
            Menuda = new System.Data.SqlClient.SqlDataAdapter(SqlStr, Con);
            Menuds = new System.Data.DataSet();
            Menuda.Fill(Menuds, "Menus");
            MenuRecordCount = Menuds.Tables["Menus"].Rows.Count;
            System.Text.StringBuilder sb = new System.Text.StringBuilder("<ul>");
            int ParentId;
            foreach (System.Data.DataRow dr in Menuds.Tables["Menus"].Rows)
            {
                sb.Append(String.Format("<li class='has-sub'><a href='" + dr["TargetPage"].ToString() + "'><span>" + dr["MenuText"].ToString() + "</span></a>"));
                
                ParentId = Convert.ToInt32 (dr["MenuID"]);
                String SecLevelSqlStr = "SELECT * FROM Menu WHERE ParentID=" + Convert.ToInt32(ParentId) + "and LangID=" + sel.ToString() ;
                Menu2da = new System.Data.SqlClient.SqlDataAdapter(SecLevelSqlStr, Con);
                Menu2ds = new System.Data.DataSet();
                Menu2da.Fill(Menu2ds, "Menu2s");
                Menu2RecordCount = Menu2ds.Tables["Menu2s"].Rows.Count;
                
                sb.Append(String.Format("<ul>"));
                foreach (System.Data.DataRow dr2 in Menu2ds.Tables["Menu2s"].Rows)
                {
               
                 sb.Append(String.Format("<li><a href='" + dr2["TargetPage"].ToString() + "'><span>" + dr2["MenuText"].ToString() + "</span></a></Li>"));
                }
                sb.Append(String.Format("</Li>"));
                sb.Append("</ul>");
            }
            sb.Append("</ul>");
            ltMenus.Text = sb.ToString();


        }
    }
}