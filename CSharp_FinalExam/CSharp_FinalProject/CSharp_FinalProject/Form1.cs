using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EmployeeLibrary;

namespace CSharp_FinalProject
{
    public partial class Form1 : Form
    {
        FullTime ftObj = new FullTime();
        CommissionBased cbObj = new CommissionBased();
        PieceWorker pwObj = new PieceWorker();
        HourlyBased hbObj = new HourlyBased();

        FullTime formFtObj;
        CommissionBased formCbObj;
        PieceWorker formPwObj;
        HourlyBased formHbObj;

        // For selecting First, Last, Next, and Previous. 
        // I use a sub-class by utilizing connection method.
        FullTime searchFtObj;

        const string DBFILE_FOLDER_PATH = "C:\\Users\\ITD3\\Desktop\\sample\\CSharp_FinalProject\\";
        const string TABLE_NAME = "Employee";

        int targetRow;

        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox5.Text)
            {
                case "FullTime":
                    formFtObj = new FullTime(Convert.ToInt16(textBox1.Text),
                                    textBox2.Text,
                                    textBox3.Text,
                                    0);
                    break;

                case "CommissionBased":
                    formCbObj = new CommissionBased(Convert.ToInt16(textBox1.Text),
                                    textBox2.Text,
                                    textBox3.Text,
                                    0);
                    break;

                case "PieceWorker":
                    formPwObj = new PieceWorker(Convert.ToInt16(textBox1.Text),
                                    textBox2.Text,
                                    textBox3.Text,
                                    0);
                    break;

                case "HourlyBased":
                    formHbObj = new HourlyBased(Convert.ToInt16(textBox1.Text),
                                    textBox2.Text,
                                    textBox3.Text,
                                    0,
                                    0);
                    break;
            }

            MessageBox.Show("Saved Successfully.");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // initilizing comboboxes
            comboBox1.Text = "FullTime";
            comboBox2.Text = "LocalDB";
            comboBox4.Text = "LocalDB";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int showEmployeeId;
            string showName;
            string showLastName;

            switch (comboBox5.Text)
            {
                case "FullTime":
                    showEmployeeId = formFtObj.getEmployeeId();
                    showName = formFtObj.getName();
                    showLastName = formFtObj.getLastName();
                    break;

                case "CommissionBased":
                    showEmployeeId = formCbObj.getEmployeeId();
                    showName = formCbObj.getName();
                    showLastName = formCbObj.getLastName();
                    break;

                case "PieceWorker":
                    showEmployeeId = formPwObj.getEmployeeId();
                    showName = formPwObj.getName();
                    showLastName = formPwObj.getLastName();
                    break;

                case "HourlyBased":
                    showEmployeeId = formHbObj.getEmployeeId();
                    showName = formHbObj.getName();
                    showLastName = formHbObj.getLastName();
                    break;

                default:
                    return;
            }

            textBox1.Text = showEmployeeId.ToString();
            textBox2.Text = showName;
            textBox3.Text = showLastName;

            MessageBox.Show("Loaded Successfully.");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            double condition1, condition2;
            int condition3;

            // skip inputting personal data because show only salary.
            switch (comboBox1.Text)
            {
                case "FullTime":
                    if (double.TryParse(textBox10.Text, out condition1))
                    {
                        ftObj = new FullTime(0, "", "", condition1);

                        MessageBox.Show("Full Time' salary is C$" + ftObj.Salary() + ".");

                    }
                    else
                    {
                        MessageBox.Show("Please input Weekly salary.");
                    }


                    break;

                case "CommissionBased":
                    if (double.TryParse(textBox11.Text, out condition1)) { 
                        cbObj = new CommissionBased(0, "", "", condition1);

                        MessageBox.Show("Comission Based' salary is C$" + cbObj.Salary() + ".");
                    }
                    else
                    {
                        MessageBox.Show("Please input Weekly sales.");
                    }

                    break;

                case "PieceWorker":
                    if (int.TryParse(textBox12.Text, out condition3))
                    {
                        pwObj = new PieceWorker(0, "", "", condition3);

                        MessageBox.Show("PieceWorker' salary is C$" + pwObj.Salary() + ".");
                    }
                    else
                    {
                        MessageBox.Show("Please input the amount you created.");
                    }

                    break;

                case "HourlyBased":


                    if (double.TryParse(textBox13.Text, out condition1) && double.TryParse(textBox14.Text, out condition2))
                    {
                        hbObj = new HourlyBased(0, "", "",
                                                Convert.ToDouble(textBox13.Text),
                                                Convert.ToDouble(textBox14.Text));

                        MessageBox.Show("Hourly Based' salary is C$" + hbObj.Salary() + ".");
                    }
                    else
                    {
                        MessageBox.Show("Please input hourly salary and weekly work hours.");
                    }
                        
                    break;

                default:
                    MessageBox.Show("Selected noexpected employee genre");
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Deal with an case that customer add data after reseaching.
            searchFtObj = new FullTime();
            targetRow = 0;
            

            switch (comboBox2.Text)
            {
                case "LocalDB":
                    searchFtObj.DBConnectivity("LocalDB", "Employees.mdf", "Employee");
                    ShowRecord(searchFtObj.getDataSet(), targetRow);
                    break;

                case "AccessMDB":
                    searchFtObj.DBConnectivity("AccessMDB", DBFILE_FOLDER_PATH + "Employees.mdb", "Employee");
                    ShowRecord(searchFtObj.getDataSet(), targetRow);
                    break;
            }

            
        }

        public void ShowRecord(DataSet pDs, int pIdx)
        {
            textBox7.Text = pDs.Tables[TABLE_NAME].Rows[pIdx].Field<int>("EmployeeId").ToString();
            textBox8.Text = pDs.Tables[TABLE_NAME].Rows[pIdx].Field<String>("Name");
            textBox9.Text = pDs.Tables[TABLE_NAME].Rows[pIdx].Field<String>("LastName");
        }


        private void button5_Click(object sender, EventArgs e)
        {
            // Deal with an case that customer add data after reseaching.
            searchFtObj = new FullTime();

            switch (comboBox2.Text)
            {
                case "LocalDB":
                    searchFtObj.DBConnectivity("LocalDB", "Employees.mdf", "Employee");

                    targetRow = Convert.ToInt16(searchFtObj.getRecordCount()) - 1;
                    ShowRecord(searchFtObj.getDataSet(), targetRow);
                    break;

                case "AccessMDB":
                    searchFtObj.DBConnectivity("AccessMDB", DBFILE_FOLDER_PATH + "Employees.mdb", "Employee");

                    targetRow = Convert.ToInt16(searchFtObj.getRecordCount()) - 1;
                    ShowRecord(searchFtObj.getDataSet(), targetRow);
                    break;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Deal with an case that customer add data after reseaching.
            searchFtObj = new FullTime();

            targetRow++;
            int maxRow;

            switch (comboBox2.Text)
            {
                case "LocalDB":
                    searchFtObj.DBConnectivity("LocalDB", "Employees.mdf", "Employee");

                    maxRow = Convert.ToInt16(searchFtObj.getRecordCount()) - 1;

                    if (targetRow > maxRow)
                    {
                        MessageBox.Show("Ending of file Encountered");
                        targetRow = maxRow;
                    }

                    ShowRecord(searchFtObj.getDataSet(), targetRow);
                    break;

                case "AccessMDB":
                    searchFtObj.DBConnectivity("AccessMDB", DBFILE_FOLDER_PATH + "Employees.mdb", "Employee");

                    maxRow = Convert.ToInt16(searchFtObj.getRecordCount()) - 1;

                    if (targetRow > maxRow)
                    {
                        targetRow = maxRow;
                    }

                    ShowRecord(searchFtObj.getDataSet(), targetRow);
                    break;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Deal with an case that customer add data after reseaching.
            searchFtObj = new FullTime();

            targetRow--;
            if (targetRow < 0)
            {
                MessageBox.Show("Beginning of file Encountered");
                targetRow = 0;
            }

            switch (comboBox2.Text)
            {
                case "LocalDB":
                    searchFtObj.DBConnectivity("LocalDB", "Employees.mdf", "Employee");

                    if (targetRow > searchFtObj.getRecordCount())
                    {
                        targetRow = Convert.ToInt16(searchFtObj.getRecordCount()) - 1;
                    }

                    ShowRecord(searchFtObj.getDataSet(), targetRow);
                    break;

                case "AccessMDB":
                    searchFtObj.DBConnectivity("AccessMDB", DBFILE_FOLDER_PATH + "Employees.mdb", "Employee");

                    if (targetRow > searchFtObj.getRecordCount())
                    {
                        targetRow = Convert.ToInt16(searchFtObj.getRecordCount()) - 1;
                    }

                    ShowRecord(searchFtObj.getDataSet(), targetRow);
                    break;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Employee[] eArr = new Employee[4];
            string showStr = "";

            ftObj = new FullTime(0, "", "", 300);
            cbObj = new CommissionBased(0, "", "", 20000);
            pwObj = new PieceWorker(0, "", "", 5);
            hbObj = new HourlyBased(0, "", "", 15, 50);

            eArr[0] = ftObj;
            eArr[1] = cbObj;
            eArr[2] = pwObj;
            eArr[3] = hbObj;

            for (int i = 0; i < eArr.Length; i++)
            {
                switch (i)
                {
                    // FullTime
                    case 0:
                        showStr += "Full timer worker' salary is C$" + 
                                    eArr[i].Salary().ToString() +
                                    Environment.NewLine + Environment.NewLine;
                        break;

                    // CommissionBased
                    case 1:
                        showStr += "When sales is C$20,000, CommissionBased worker' salary is C$" + 
                                    eArr[i].Salary().ToString() +
                                    Environment.NewLine + Environment.NewLine;
                        break;

                    // PieceWorker
                    case 2:
                        showStr += "When creating five things, PieceWorker worker' salary is C$" + 
                                    eArr[i].Salary().ToString() +
                                    Environment.NewLine + Environment.NewLine;
                        break;

                    // HourlyBased
                    case 3:
                        showStr += "When working 50 hours, HourlyBased worker, which gets C$15 per hour, salary is C$" + 
                                    eArr[i].Salary().ToString() +
                                    Environment.NewLine + Environment.NewLine;
                        break;
                }
            }

            richTextBox1.Text = showStr;
        }

        public string getRecord(DataSet pDs, int pIdx)
        {
            return "[" + pDs.Tables[TABLE_NAME].Rows[pIdx].Field<int>("EmployeeId").ToString() + "]-" +
                    "[" + pDs.Tables[TABLE_NAME].Rows[pIdx].Field<String>("Name") + "]-" +
                    "[" + pDs.Tables[TABLE_NAME].Rows[pIdx].Field<String>("LastName") + "]" + Environment.NewLine; ;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.Text)
            {
                case "LocalDB":
                    textBox5.Text = "Employees.mdf";
                    textBox6.Text = "Employee";
                    break;

                case "AccessMDB":
                    textBox5.Text = "Employees.mdb";
                    textBox6.Text = "Employee";
                    break;

                default:
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (comboBox5.Text)
            {
                case "FullTime":
                    formFtObj.DBConnectivity(comboBox4.Text, textBox5.Text, textBox6.Text);
                    formFtObj.SaveinDB();


                    break;

                case "CommissionBased":
                    formCbObj.DBConnectivity(comboBox4.Text, textBox5.Text, textBox6.Text);
                    formCbObj.SaveinDB();

                    break;

                case "PieceWorker":
                    formPwObj.DBConnectivity(comboBox4.Text, textBox5.Text, textBox6.Text);
                    formPwObj.SaveinDB();

                    break;

                case "HourlyBased":
                    formHbObj.DBConnectivity(comboBox4.Text, textBox5.Text, textBox6.Text);
                    formHbObj.SaveinDB();

                    break;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int targetId = Convert.ToInt16(textBox1.Text);


            switch (comboBox5.Text)
            {
                case "FullTime":
                    ftObj.DBConnectivity(comboBox4.Text, textBox5.Text, textBox6.Text);
                    ftObj.LoadfromDB(targetId);

                    textBox1.Text = ftObj.getEmployeeId().ToString();
                    textBox2.Text = ftObj.getName();
                    textBox3.Text = ftObj.getLastName();

                    break;

                case "CommissionBased":
                    cbObj.DBConnectivity(comboBox4.Text, textBox5.Text, textBox6.Text);
                    cbObj.LoadfromDB(targetId);

                    textBox1.Text = cbObj.getEmployeeId().ToString();
                    textBox2.Text = cbObj.getName();
                    textBox3.Text = cbObj.getLastName();

                    break;

                case "PieceWorker":
                    pwObj.DBConnectivity(comboBox4.Text, textBox5.Text, textBox6.Text);
                    pwObj.LoadfromDB(targetId);

                    textBox1.Text = pwObj.getEmployeeId().ToString();
                    textBox2.Text = pwObj.getName();
                    textBox3.Text = pwObj.getLastName();

                    break;

                case "HourlyBased":
                    hbObj.DBConnectivity(comboBox4.Text, textBox5.Text, textBox6.Text);
                    hbObj.LoadfromDB(targetId);

                    textBox1.Text = hbObj.getEmployeeId().ToString();
                    textBox2.Text = hbObj.getName();
                    textBox3.Text = hbObj.getLastName();

                    break;
            }
        }
    }
}
