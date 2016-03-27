using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FirstASPdotNet
{
    public partial class Calc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int sum;
            sum = 0; 
            sum  = Convert.ToInt32(TextBox1.Text) + Convert.ToInt32(TextBox2.Text);
            TextBox3.Text = sum.ToString();  
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int sub;
            sub = 0;
            sub = Convert.ToInt32(TextBox1.Text) - Convert.ToInt32(TextBox2.Text);
            TextBox3.Text = sub.ToString();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int mul;
            mul = 1;
            mul = Convert.ToInt32(TextBox1.Text) * Convert.ToInt32(TextBox2.Text);
            TextBox3.Text = mul.ToString();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            double div;
            div = 0;
            if (Convert.ToInt32(TextBox2.Text) == 0)
            {
                TextBox3.Text = "Err";
                return;
            }
            div = Convert.ToInt32(TextBox1.Text) / Convert.ToInt32(TextBox2.Text);
            TextBox3.Text = div.ToString();
            
        }
    }
}