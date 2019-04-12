using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMSEnitiesLib;
using HRMSDataAccessLib;
using System.Data.SqlClient;
using System.Data;

namespace HRMSDataAccessLib
{
    interface IHRMSAdoDisconnected
    {
        void InsertEmployee(Employee emp);
        void DeleteEmpById(int ecode);
        void UpdateEmpolyeeById(int ecode, int salary);
        List<Employee> SelectAllEmps();
        Employee SelectEmpById(int ecode);
    }

    public class HRMSAdoDisconnected : IHRMSAdoDisconnected
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        public HRMSAdoDisconnected()
        {
            //config connection
            con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=IBMDB;User ID=sa;Password=password@123";
            //config DataAdapter
            da = new SqlDataAdapter("select * from tbl_employee", con);
            //create dataset
            ds = new DataSet();
            //fill the dataset dataAdapter
            da.Fill(ds, "tbl_employee");
            //add the Contrains 
            ds.Tables[0].Constraints.Add("pk1", ds.Tables[0]. Columns[0], true);



        }

        public void DeleteEmpById(int ecode)
        {
            //find the record to be deleted based on key column
           DataRow row= ds.Tables[0].Rows.Find(ecode);
            if (row ==null)
            {
                throw new Exception("ecode not found ");
            }
            else
            {
                
                row.Delete();
                SqlCommandBuilder cb = new SqlCommandBuilder();
                da.Update(ds, "tbl_employee");
            }

            
                ////find record to be deleted from the dataset table
                //DataRow[] rows = ds.Tables[0].Select("ecode=" + ecode);
                //if (rows.Length == 0)
                //{
                //    throw new Exception("ecode doesnot Exists");
                //}
                //else
                //{
                //    rows[0].Delete();
                //    //save the changes to DB using DataAdapter
                //    SqlCommandBuilder cb = new SqlCommandBuilder(da);
                //    da.Update(ds, "tbl_employee");
                //}
            
           

        }

        public void InsertEmployee(Employee emp)
        {
            //create a new row as per DataSet Table
            DataRow row = ds.Tables[0].NewRow();
            //supply values to the columns of the new row created
            row[0] = emp.Ecode;
            row[1] = emp.Ename;
            row[2] = emp.salary;
            row[3] = emp.Deptid;
            //add the new row to the rows of the DataTable of Ds 
            ds.Tables[0].Rows.Add(row);
            //save the record to DB using dataAdapter
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            da.Update(ds, "tbl_employee");

        }

        public List<Employee> SelectAllEmps()
        {
            List<Employee> emplst = new List<Employee>();
            //traverse the dataset for rows
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                //create employee each row values
                Employee emp = new Employee
                {
                    Ecode = (int)row[0],
                    Ename = row[1].ToString(),
                    salary = (int)row[2],
                    Deptid = (int)row[3]

                };
                //add the employee to the collection 
                emplst.Add(emp);

            }
            return emplst;
        }

        public Employee SelectEmpById(int ecode)
        {
            Employee emp = null;
            DataRow row = ds.Tables[0].Rows.Find(ecode);
            if (row == null)
            {
                throw new Exception("ecode not found");
            }
            else
            {
                emp = new Employee
                {
                    Ecode = (int)row[0],
                    Ename = row[1].ToString(),
                    salary = (int)row[2],
                    Deptid = (int)row[3]


                };
            }
            return emp;
        }

        public void UpdateEmpolyeeById(int ecode, int salary)
        {
            //find record to be update from the dataset table
            DataRow[] rows = ds.Tables[0].Select("ecode=" + ecode);
            if (rows.Length == 0)
            {
                throw new Exception("ecode doesnot Exists");
            }
            else
            {
                rows[0][2] = (int)rows[0][2] + salary;
            }
                //save the changes to DB using DataAdapter
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                da.Update(ds, "tbl_employee");
            

        }
    }
}
    
