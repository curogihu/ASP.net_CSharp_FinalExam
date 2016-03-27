using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;

namespace EmployeeLibrary
{
    abstract public class Employee
    {
        int employeeId;
        string name;
        string lastName;
        string dbType;
        string dbFileName;
        string dbTableName;

        public Employee()
        {
            employeeId = 0;
            name = "";
            lastName = "";
            dbType = "";
            dbFileName = "";
            dbTableName = "";
        }

        public Employee(int pEmployeeId,
                        string pName,
                        string pLastName)
        {
            employeeId = pEmployeeId;
            name = pName;
            lastName = pLastName;
        }

        ~Employee()
        {

        }

        public int getEmployeeId()
        {
            return employeeId;
        }

        public void setEmployeeId(int pEmployeeId)
        {
            employeeId = pEmployeeId;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string pName)
        {
            name = pName;
        }

        public string getLastName()
        {
            return lastName;
        }

        public void setLastName(string pLastName)
        {
            lastName = pLastName;
        }

        public string getDbType()
        {
            return dbType;
        }

        public void setDbType(string pDbType)
        {
            dbType = pDbType;
        }

        public string getDbFileName()
        {
            return dbFileName;
        }

        public void setDbFileName(string pDbFileName)
        {
            dbFileName = pDbFileName;
        }

        public string getDbTableName()
        {
            return dbTableName;
        }

        public void setDbTableName(string pDbTableName)
        {
            dbTableName = pDbTableName;
        }

        public virtual double Salary()
        {
            return 0;
        }

        public virtual void DBConnectivity(string pDbType, string pDbFileName, string pDbTableName)
        {

        }

        public virtual void SaveinDB()
        {

        }

        public virtual void LoadfromDB(int pEmployeeId)
        {

        }
    }

    public class FullTime : Employee
    {
        private double salary;

        public System.Data.OleDb.OleDbConnection cn_Access;
        public System.Data.OleDb.OleDbDataAdapter da_Access;
        public System.Data.OleDb.OleDbCommand cmd_Access;

        public System.Data.SqlClient.SqlConnection cn_LocalDB;
        public System.Data.SqlClient.SqlDataAdapter da_LocalDB;

        DataSet ds;

        long recordCount;

        public FullTime()
            : base()
        {
            salary = 0;
        }

        public FullTime(int pEmployeeId,
                        string pName,
                        string pLastName,
                        double pSalary)

            : base(pEmployeeId,
                        pName,
                        pLastName)
        {
            salary = pSalary;
        }

        public void setRecordCount(long pRecordCount)
        {
            recordCount = pRecordCount;
        }

        public long getRecordCount()
        {
            return recordCount;
        }

        public void setDataSet(DataSet pDataSet)
        {
            ds = pDataSet;
        }

        public DataSet getDataSet()
        {
            return ds;
        }

        override public double Salary()
        {
            return salary;
        }



        override public void DBConnectivity(string pDbType, string pDbFileName, string pDbTableName)
        {
            this.setDbType(pDbType);
            this.setDbFileName(pDbFileName);
            this.setDbTableName(pDbTableName);

            if (pDbType == "AccessMDB")
            {
                //making the actual dataadpter to get to the table
                cn_Access = new System.Data.OleDb.OleDbConnection();
                //cn_Access.ConnectionString = "Provider = Microsoft.jet.OLEDB.4.0;Data Source = " + DBFILE_FOLDER_PATH + pDbFileName;
                cn_Access.ConnectionString = "Provider = Microsoft.jet.OLEDB.4.0;Data Source = " + pDbFileName;
                cn_Access.Open();

                // initilizing two database
                String sqlstr = "SELECT * FROM " + pDbTableName;

                ds = new DataSet();

                da_Access = new System.Data.OleDb.OleDbDataAdapter(sqlstr, cn_Access);
                da_Access.Fill(ds, pDbTableName);

                recordCount = ds.Tables[pDbTableName].Rows.Count;

            }
            else if (pDbType == "LocalDB")
            {
                cn_LocalDB = new System.Data.SqlClient.SqlConnection();
                cn_LocalDB.ConnectionString = "Data Source=(LocalDB)\\v11.0; AttachDbFilename=|DataDirectory|\\" + pDbFileName + ";Integrated Security=True";
                cn_LocalDB.Open();

                String sqlstr = "SELECT * FROM " + pDbTableName;

                ds = new DataSet();

                da_LocalDB = new System.Data.SqlClient.SqlDataAdapter(sqlstr, cn_LocalDB);
                da_LocalDB.Fill(ds, pDbTableName);

                recordCount = ds.Tables[pDbTableName].Rows.Count;
            }
        }

        override public void SaveinDB()
        {
            string tmpDbType = this.getDbType();

            if (tmpDbType == "AccessMDB")
            {
                string tmpSql = "Insert into Employee values(@1, '@2','@3')";

                tmpSql = tmpSql.Replace("@1", this.getEmployeeId().ToString())
                        .Replace("@2", this.getName())
                        .Replace("@3", this.getLastName());

                cmd_Access = new System.Data.OleDb.OleDbCommand(tmpSql, cn_Access);
                cmd_Access.CommandType = CommandType.Text;
                cmd_Access.ExecuteNonQuery();

               // MessageBox.Show("Adding a record sucessfully.");
            }
            else if (tmpDbType == "LocalDB")
            {
                System.Data.DataRow[] foundRows;
                String sqlCondition;
                String tmpTableName = this.getDbTableName();


                sqlCondition = "EmployeeId = " + this.getEmployeeId();
                foundRows = ds.Tables[tmpTableName].Select(sqlCondition);

                if (foundRows.Length != 0)
                {
                    //MessageBox.Show("Duplicate ID, try Again!!!");
                }
                else
                {
                    //its a new record, we should be able to add 
                    System.Data.DataRow NewRow = ds.Tables[tmpTableName].NewRow();

                    //Next line is needed so we can update the database 
                    System.Data.SqlClient.SqlCommandBuilder Cb = new System.Data.SqlClient.SqlCommandBuilder(da_LocalDB);

                    NewRow.SetField<int>("EmployeeId", this.getEmployeeId());
                    NewRow.SetField<String>("Name", this.getName());
                    NewRow.SetField<String>("LastName", this.getLastName());

                    ds.Tables[tmpTableName].Rows.Add(NewRow);
                    da_LocalDB.Update(ds, tmpTableName);

                    recordCount++;
                    //System.Windows.Forms.MessageBox.Show("Record Added Succesfully");
                }
            }
        }

        override public void LoadfromDB(int pEmployeeId)
        {
            System.Data.DataRow[] foundRows;
            String sqlCondition;
            String tmpTableName = this.getDbTableName();
            int currentRow;

            sqlCondition = "EmployeeId = " + pEmployeeId.ToString();
            foundRows = ds.Tables[tmpTableName].Select(sqlCondition);

            if (foundRows.Length == 0)
            {
                //MessageBox.Show("Employee Id[" + pEmployeeId.ToString() + "] is not found.");
            }
            else
            {
                currentRow = ds.Tables[tmpTableName].Rows.IndexOf(foundRows[0]);

                this.setEmployeeId(ds.Tables[tmpTableName].Rows[currentRow].Field<int>("EmployeeId"));
                this.setName(ds.Tables[tmpTableName].Rows[currentRow].Field<String>("Name"));
                this.setLastName(ds.Tables[tmpTableName].Rows[currentRow].Field<String>("LastName"));

                //MessageBox.Show("Employee Id[" + pEmployeeId.ToString() + "] is found.");
            }
        }

    }

    public class CommissionBased : Employee
    {
        private double sales;

        public System.Data.OleDb.OleDbConnection cn_Access;
        public System.Data.OleDb.OleDbDataAdapter da_Access;
        public System.Data.OleDb.OleDbCommand cmd_Access;

        public System.Data.SqlClient.SqlConnection cn_LocalDB;
        public System.Data.SqlClient.SqlDataAdapter da_LocalDB;

        DataSet ds;

        long recordCount;

        public CommissionBased()
            : base()
        {
            sales = 0;
        }

        public CommissionBased(int pEmployeeId,
                        string pName,
                        string pLastName,
                        double pSales)

            : base(pEmployeeId,
                        pName,
                        pLastName)
        {
            sales = pSales;
        }

        public void setRecordCount(long pRecordCount)
        {
            recordCount = pRecordCount;
        }

        public long getRecordCount()
        {
            return recordCount;
        }

        public void setDataSet(DataSet pDataSet)
        {
            ds = pDataSet;
        }

        public DataSet getDataSet()
        {
            return ds;
        }

        override public double Salary()
        {
            return 250 + sales * 0.1;
        }
    }

    public class PieceWorker : Employee
    {
        int createAmount;

        public System.Data.OleDb.OleDbConnection cn_Access;
        public System.Data.OleDb.OleDbDataAdapter da_Access;
        public System.Data.OleDb.OleDbCommand cmd_Access;

        public System.Data.SqlClient.SqlConnection cn_LocalDB;
        public System.Data.SqlClient.SqlDataAdapter da_LocalDB;

        DataSet ds;

        long recordCount;

        public PieceWorker()
            : base()
        {
            createAmount = 0;
        }

        public PieceWorker(int pEmployeeId,
                        string pName,
                        string pLastName,
                        int pCreateAmount)

            : base(pEmployeeId,
                    pName,
                    pLastName)
        {
            createAmount = pCreateAmount;
        }

        public void setRecordCount(long pRecordCount)
        {
            recordCount = pRecordCount;
        }

        public long getRecordCount()
        {
            return recordCount;
        }

        public void setDataSet(DataSet pDataSet)
        {
            ds = pDataSet;
        }

        public DataSet getDataSet()
        {
            return ds;
        }

        override public double Salary()
        {
            return 250 + createAmount * 10;
        }

        override public void DBConnectivity(string pDbType, string pDbFileName, string pDbTableName)
        {
            this.setDbType(pDbType);
            this.setDbFileName(pDbFileName);
            this.setDbTableName(pDbTableName);

            if (pDbType == "AccessMDB")
            {
                //making the actual dataadpter to get to the table
                cn_Access = new System.Data.OleDb.OleDbConnection();
                //cn_Access.ConnectionString = "Provider = Microsoft.jet.OLEDB.4.0;Data Source = " + DBFILE_FOLDER_PATH + pDbFileName;
                cn_Access.ConnectionString = "Provider = Microsoft.jet.OLEDB.4.0;Data Source = " + pDbFileName;
                cn_Access.Open();

                // initilizing two database
                String sqlstr = "SELECT * FROM " + pDbTableName;

                ds = new DataSet();

                da_Access = new System.Data.OleDb.OleDbDataAdapter(sqlstr, cn_Access);
                da_Access.Fill(ds, pDbTableName);

                recordCount = ds.Tables[pDbTableName].Rows.Count;

            }
            else if (pDbType == "LocalDB")
            {
                cn_LocalDB = new System.Data.SqlClient.SqlConnection();
                cn_LocalDB.ConnectionString = "Data Source=(LocalDB)\\v11.0; AttachDbFilename=|DataDirectory|\\" + pDbFileName + ";Integrated Security=True";
                cn_LocalDB.Open();

                String sqlstr = "SELECT * FROM " + pDbTableName;

                ds = new DataSet();

                da_LocalDB = new System.Data.SqlClient.SqlDataAdapter(sqlstr, cn_LocalDB);
                da_LocalDB.Fill(ds, pDbTableName);

                recordCount = ds.Tables[pDbTableName].Rows.Count;
            }
        }

        override public void SaveinDB()
        {
            string tmpDbType = this.getDbType();

            if (tmpDbType == "AccessMDB")
            {
                string tmpSql = "Insert into Employee values(@1, '@2','@3')";

                tmpSql = tmpSql.Replace("@1", this.getEmployeeId().ToString())
                        .Replace("@2", this.getName())
                        .Replace("@3", this.getLastName());

                cmd_Access = new System.Data.OleDb.OleDbCommand(tmpSql, cn_Access);
                cmd_Access.CommandType = CommandType.Text;
                cmd_Access.ExecuteNonQuery();

                //MessageBox.Show("Adding a record sucessfully.");
            }
            else if (tmpDbType == "LocalDB")
            {
                System.Data.DataRow[] foundRows;
                String sqlCondition;
                String tmpTableName = this.getDbTableName();


                sqlCondition = "EmployeeId = " + this.getEmployeeId();
                foundRows = ds.Tables[tmpTableName].Select(sqlCondition);

                if (foundRows.Length != 0)
                {
                    //MessageBox.Show("Duplicate ID, try Again!!!");
                }
                else
                {
                    //its a new record, we should be able to add 
                    System.Data.DataRow NewRow = ds.Tables[tmpTableName].NewRow();

                    //Next line is needed so we can update the database 
                    System.Data.SqlClient.SqlCommandBuilder Cb = new System.Data.SqlClient.SqlCommandBuilder(da_LocalDB);

                    NewRow.SetField<int>("EmployeeId", this.getEmployeeId());
                    NewRow.SetField<String>("Name", this.getName());
                    NewRow.SetField<String>("LastName", this.getLastName());

                    ds.Tables[tmpTableName].Rows.Add(NewRow);
                    da_LocalDB.Update(ds, tmpTableName);

                    recordCount++;
                    //System.Windows.Forms.MessageBox.Show("Record Added Succesfully");
                }
            }
        }

        override public void LoadfromDB(int pEmployeeId)
        {
            System.Data.DataRow[] foundRows;
            String sqlCondition;
            String tmpTableName = this.getDbTableName();
            int currentRow;

            sqlCondition = "EmployeeId = " + pEmployeeId.ToString();
            foundRows = ds.Tables[tmpTableName].Select(sqlCondition);

            if (foundRows.Length == 0)
            {
                return;
            }
            else
            {
                currentRow = ds.Tables[tmpTableName].Rows.IndexOf(foundRows[0]);

                this.setEmployeeId(ds.Tables[tmpTableName].Rows[currentRow].Field<int>("EmployeeId"));
                this.setName(ds.Tables[tmpTableName].Rows[currentRow].Field<String>("Name"));
                this.setLastName(ds.Tables[tmpTableName].Rows[currentRow].Field<String>("LastName"));
            }
        }
    }

    public class HourlyBased : Employee
    {
        double defaultHourSalary;
        double workHour;

        public System.Data.OleDb.OleDbConnection cn_Access;
        public System.Data.OleDb.OleDbDataAdapter da_Access;
        public System.Data.OleDb.OleDbCommand cmd_Access;

        public System.Data.SqlClient.SqlConnection cn_LocalDB;
        public System.Data.SqlClient.SqlDataAdapter da_LocalDB;

        DataSet ds;

        long recordCount;

        public HourlyBased()
            : base()
        {
            defaultHourSalary = 0;
            workHour = 0;
        }

        public HourlyBased(int pEmployeeId,
                        string pName,
                        string pLastName,
                        double pDefaultHourSalary,
                        double pWorkHour)

            : base(pEmployeeId,
                        pName,
                        pLastName)
        {
            defaultHourSalary = pDefaultHourSalary;
            workHour = pWorkHour;
        }

        public void setRecordCount(long pRecordCount)
        {
            recordCount = pRecordCount;
        }

        public long getRecordCount()
        {
            return recordCount;
        }

        public void setDataSet(DataSet pDataSet)
        {
            ds = pDataSet;
        }

        public DataSet getDataSet()
        {
            return ds;
        }
        override public double Salary()
        {
            if (workHour <= 40)
            {
                return defaultHourSalary * workHour;
            }

            // in case of including overtime job
            return defaultHourSalary * 40 + defaultHourSalary * 1.5 * (workHour - 40);
        }

        override public void DBConnectivity(string pDbType, string pDbFileName, string pDbTableName)
        {
            this.setDbType(pDbType);
            this.setDbFileName(pDbFileName);
            this.setDbTableName(pDbTableName);

            if (pDbType == "AccessMDB")
            {
                //making the actual dataadpter to get to the table
                cn_Access = new System.Data.OleDb.OleDbConnection();
                //cn_Access.ConnectionString = "Provider = Microsoft.jet.OLEDB.4.0;Data Source = " + DBFILE_FOLDER_PATH + pDbFileName;
                cn_Access.ConnectionString = "Provider = Microsoft.jet.OLEDB.4.0;Data Source = " + pDbFileName;
                cn_Access.Open();

                // initilizing two database
                String sqlstr = "SELECT * FROM " + pDbTableName;

                ds = new DataSet();

                da_Access = new System.Data.OleDb.OleDbDataAdapter(sqlstr, cn_Access);
                da_Access.Fill(ds, pDbTableName);

                recordCount = ds.Tables[pDbTableName].Rows.Count;

            }
            else if (pDbType == "LocalDB")
            {
                cn_LocalDB = new System.Data.SqlClient.SqlConnection();
                cn_LocalDB.ConnectionString = "Data Source=(LocalDB)\\v11.0; AttachDbFilename=|DataDirectory|\\" + pDbFileName + ";Integrated Security=True";
                cn_LocalDB.Open();

                String sqlstr = "SELECT * FROM " + pDbTableName;

                ds = new DataSet();

                da_LocalDB = new System.Data.SqlClient.SqlDataAdapter(sqlstr, cn_LocalDB);
                da_LocalDB.Fill(ds, pDbTableName);

                recordCount = ds.Tables[pDbTableName].Rows.Count;
            }
        }

        override public void SaveinDB()
        {
            string tmpDbType = this.getDbType();

            if (tmpDbType == "AccessMDB")
            {
                string tmpSql = "Insert into Employee values(@1, '@2','@3')";

                tmpSql = tmpSql.Replace("@1", this.getEmployeeId().ToString())
                        .Replace("@2", this.getName())
                        .Replace("@3", this.getLastName());

                cmd_Access = new System.Data.OleDb.OleDbCommand(tmpSql, cn_Access);
                cmd_Access.CommandType = CommandType.Text;
                cmd_Access.ExecuteNonQuery();

                //MessageBox.Show("Adding a record sucessfully.");
            }
            else if (tmpDbType == "LocalDB")
            {
                System.Data.DataRow[] foundRows;
                String sqlCondition;
                String tmpTableName = this.getDbTableName();


                sqlCondition = "EmployeeId = " + this.getEmployeeId();
                foundRows = ds.Tables[tmpTableName].Select(sqlCondition);

                if (foundRows.Length != 0)
                {
                    //MessageBox.Show("Duplicate ID, try Again!!!");
                }
                else
                {
                    //its a new record, we should be able to add 
                    System.Data.DataRow NewRow = ds.Tables[tmpTableName].NewRow();

                    //Next line is needed so we can update the database 
                    System.Data.SqlClient.SqlCommandBuilder Cb = new System.Data.SqlClient.SqlCommandBuilder(da_LocalDB);

                    NewRow.SetField<int>("EmployeeId", this.getEmployeeId());
                    NewRow.SetField<String>("Name", this.getName());
                    NewRow.SetField<String>("LastName", this.getLastName());

                    ds.Tables[tmpTableName].Rows.Add(NewRow);
                    da_LocalDB.Update(ds, tmpTableName);

                    recordCount++;
                    //System.Windows.Forms.MessageBox.Show("Record Added Succesfully");
                }
            }
        }

        override public void LoadfromDB(int pEmployeeId)
        {
            System.Data.DataRow[] foundRows;
            String sqlCondition;
            String tmpTableName = this.getDbTableName();
            int currentRow;

            sqlCondition = "EmployeeId = " + pEmployeeId.ToString();
            foundRows = ds.Tables[tmpTableName].Select(sqlCondition);

            if (foundRows.Length == 0)
            {
                return;
            }
            else
            {
                currentRow = ds.Tables[tmpTableName].Rows.IndexOf(foundRows[0]);

                this.setEmployeeId(ds.Tables[tmpTableName].Rows[currentRow].Field<int>("EmployeeId"));
                this.setName(ds.Tables[tmpTableName].Rows[currentRow].Field<String>("Name"));
                this.setLastName(ds.Tables[tmpTableName].Rows[currentRow].Field<String>("LastName"));
            }
        }
    }
}
