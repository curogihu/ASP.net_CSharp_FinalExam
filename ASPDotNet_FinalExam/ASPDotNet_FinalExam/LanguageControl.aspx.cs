using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace ASPDotNet_FinalExam
{
    public partial class home : System.Web.UI.Page
    {
        System.Data.SqlClient.SqlConnection cn;
        System.Data.SqlClient.SqlDataAdapter da;
        //System.Data.SqlClient.SqlTransaction trans;
        System.Data.DataSet ds;
        int recordCnt;

        const string TABLE_NAME = "Languages";

        protected void Page_Load(object sender, EventArgs e)
        {
            cn = new System.Data.SqlClient.SqlConnection();
            cn.ConnectionString = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\FakeSite.mdf;Integrated Security=True";
            cn.Open();

            String SqlStr = "SELECT * FROM " + TABLE_NAME;
            
            System.Data.SqlClient.SqlConnection SC = new System.Data.SqlClient.SqlConnection(cn.ConnectionString);
            da = new System.Data.SqlClient.SqlDataAdapter(SqlStr, cn);
            ds = new System.Data.DataSet();
            da.Fill(ds, TABLE_NAME);

            recordCnt = ds.Tables[TABLE_NAME].Rows.Count;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Data.DataRow[] foundRows;
            String sqlCondition;
            String tmpTableName = TABLE_NAME;


            sqlCondition = "LanguageID = " + TextBox1.Text;
            foundRows = ds.Tables[tmpTableName].Select(sqlCondition);

            if (foundRows.Length != 0)
            {
                Response.Write(@"<script language='javascript'>alert('Duplicate ID, try Again!!!')</script>");
            }
            else
            {
                //its a new record, we should be able to add 
                System.Data.DataRow NewRow = ds.Tables[tmpTableName].NewRow();

                //Next line is needed so we can update the database 
                System.Data.SqlClient.SqlCommandBuilder Cb = new System.Data.SqlClient.SqlCommandBuilder(da);


                NewRow.SetField<int>("LanguageID", Convert.ToInt16(TextBox1.Text));
                NewRow.SetField<String>("LanguageDescription", TextBox2.Text);


                ds.Tables[tmpTableName].Rows.Add(NewRow);
                da.Update(ds, tmpTableName);

                recordCnt++;

                Response.Write(@"<script language='javascript'>alert('Added successfully.')</script>");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            System.Data.DataRow[] foundRows;
            String sqlCondition;
            String tmpTableName = TABLE_NAME;
            int rowIndex;


            sqlCondition = "LanguageID = " + TextBox3.Text;
            foundRows = ds.Tables[tmpTableName].Select(sqlCondition);

            if (foundRows.Length == 0)
            {
                Response.Write(@"<script language='javascript'>alert('No data related to the ID you inputed, try Again!!!')</script>");
            }
            else
            {
                rowIndex = ds.Tables[tmpTableName].Rows.IndexOf(foundRows[0]);
                ds.Tables[tmpTableName].Rows[rowIndex].SetField<int>("LanguageID", Convert.ToInt32(TextBox3.Text));
                ds.Tables[tmpTableName].Rows[rowIndex].SetField<String>("LanguageDescription", TextBox4.Text);

                Response.Write(@"<script language='javascript'>alert('Modified successfully.')</script>");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            System.Data.DataRow[] foundRows;
            String sqlCondition;
            String tmpTableName = TABLE_NAME;
            int rowIndex;

            sqlCondition = "LanguageID = " + TextBox5.Text;
            foundRows = ds.Tables[tmpTableName].Select(sqlCondition);

            if (foundRows.Length == 0)
            {
                Response.Write(@"<script language='javascript'>alert('No data related to the ID you inputed, try Again!!!')</script>");
            }
            else
            {
                rowIndex = ds.Tables[tmpTableName].Rows.IndexOf(foundRows[0]);
                ds.Tables[tmpTableName].Rows[rowIndex].Delete();

                Response.Write(@"<script language='javascript'>alert('Deleted successfully.')</script>");
            }
        }

    }
}