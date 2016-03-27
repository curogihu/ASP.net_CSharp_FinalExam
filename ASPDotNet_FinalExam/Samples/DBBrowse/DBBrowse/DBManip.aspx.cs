using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DBBrowse
{
    public partial class DBManip : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlConnection Con;
        System.Data.SqlClient.SqlDataAdapter da;
        System.Data.SqlClient.SqlTransaction trans;
        System.Data.DataSet ds;
        int RecordCount;
        
        public static int CurrentRow;
        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Table' table. You can move, or remove it, as needed.
            //this.tableTableAdapter.Fill(this.database1DataSet.Table);
            Con = new System.Data.SqlClient.SqlConnection();
            Con.ConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\Books.mdf;Integrated Security=True";
            Con.Open();
            String SqlStr = "SELECT * FROM Titles";
            System.Data.SqlClient.SqlConnection SC = new System.Data.SqlClient.SqlConnection(Con.ConnectionString);
            da = new System.Data.SqlClient.SqlDataAdapter(SqlStr, Con);
            ds = new System.Data.DataSet();
            da.Fill(ds, "Titles");
            RecordCount = ds.Tables["Titles"].Rows.Count;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            CurrentRow = 0;
            ShowRecord(CurrentRow);
        }
        public void ShowRecord(int ThisRow)
        {

            TextBox1.Text = ds.Tables["Titles"].Rows[ThisRow]["ID"].ToString();
            TextBox2.Text = ds.Tables["Titles"].Rows[ThisRow]["BookTitle"].ToString();
            TextBox3.Text = ds.Tables["Titles"].Rows[ThisRow]["Genre"].ToString();
            TextBox4.Text = ds.Tables["Titles"].Rows[ThisRow]["Price"].ToString();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            CurrentRow++;
            if (CurrentRow > RecordCount - 1)
            {
                Response.Write(@"<script language='javascript'>alert('End of File ecncountered')</script>");
                //MessageBox.Show("End of file Encountered");
                CurrentRow--;
            }

            ShowRecord(CurrentRow); 
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            CurrentRow--;
            if (CurrentRow < 0)
            {
                Response.Write(@"<script language='javascript'>alert('Begin of File ecncountered')</script>");
                //MessageBox.Show("Beginning of file Encountered");
                CurrentRow++;
            }
            ShowRecord(CurrentRow);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            CurrentRow = Convert.ToInt16(RecordCount) - 1;
            ShowRecord(CurrentRow);
        }
    }
}