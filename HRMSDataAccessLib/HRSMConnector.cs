using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMSEnitiesLib;
using System.Data;
using System.Data.SqlClient;

using System.Data.SqlClient;//ADO.net classed
using System.Data; //used for db connection as well as disconnected entities

namespace HRMSDataAccessLib
{
    interface IHRMSAdoConnector
    {
        void InsertEmployee(Employee emp);
        void DeleteEmpById(int ecode);
        void UpdateEmpolyeeById(int ecode, int salary);
        List<Employee> SelectAllEmps();
        Employee SelectEmpById(int ecode);
        void UpdateSalaryUsingSP(int ecode, int salary);
        double GetBonus(int salary);
        void DoTransaction();

    }
    public class HRMSAdoConnector : IHRMSAdoConnector
    {
        SqlConnection con;
        SqlCommand cmd;

        public HRMSAdoConnector()
        {
            //configure connection
            con = new SqlConnection();
            //servername,db name,authenticaion 
            con.ConnectionString = @"Data Source=.;Initial Catalog=IBMDB;User ID=sa;Password=password@123";
            //Configure command and attach connection
            cmd = new SqlCommand();
            cmd.Connection = con;
        }

        public void DeleteEmpById(int ecode)
        {
            Employee emp = null;
            try
            {
                cmd.CommandText = "Delete  from tbl_employee where ecode= " + ecode;
                cmd.CommandType = CommandType.Text;
                con.Open();
               int i= cmd.ExecuteNonQuery();
                if(i==0)
                {
                    throw new Exception("Record doesnot exit");
                }
                
            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void DoTransaction()
        {
            SqlTransaction T = null; 
            try
            {
                SqlCommand cmd1 = new SqlCommand(); 
                SqlCommand cmd2 = new SqlCommand();

                cmd1.CommandText = "delete from tbl_employee where ecode=101";
                cmd2.CommandText = "update tbl_employee set salary=salary+20000 where ecode=111";

                
                cmd1.Connection = con;
                cmd2.Connection = con;
                con.Open();
                T = con.BeginTransaction();

                cmd1.Transaction = T;
                cmd2.Transaction = T;

                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                T.Commit();



            }
            catch (SqlException ex)
            {
                T.Rollback();
                throw ex;
                
            }
            finally
            {
                con.Close();
            }
        }

        public double GetBonus(int salary)
        {
            cmd.CommandText = "sp_calcbonus";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@sal", salary);
            double bonus = 0;
            cmd.Parameters.AddWithValue("@bonus", bonus);
            cmd.Parameters[1].Direction = ParameterDirection.Output;
            con.Open();
            cmd.ExecuteNonQuery();
            //get the update value of bonus parameter out of sp
            bonus = (double)cmd.Parameters[1].Value;
            con.Close();
            return bonus;
        }

        public void InsertEmployee(Employee emp)
        {
            try
            {
                // config command txt for Insert operation
                // cmd.CommandText = "insert into tbl_employee values("+emp.Ecode+","+emp.Ename+","+emp.salary+","+emp.Deptid+")";
                
                cmd.CommandText = "insert into tbl_employee values(@ec,@en,@sal,@did,201)";
                //Config Command parameters and vslues;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ec", emp.Ecode);
                cmd.Parameters.AddWithValue("@en", emp.Ename);
                cmd.Parameters.AddWithValue("@sal", emp.salary);
                cmd.Parameters.AddWithValue("@did", emp.Deptid);
                //open the connection
                con.Open();

                int recordsAffected = cmd.ExecuteNonQuery();//Execute the command
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }


        }

        public List<Employee> SelectAllEmps()
        {
            List<Employee> lstEmps = null;
            try
            {
                //configure command text for Select all
                cmd.CommandText = "select * from tbl_employee";
                cmd.CommandType = CommandType.Text;
                //open Connection
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                lstEmps = new List<Employee>();
                while(sdr.Read())
                {
                    Employee emp = new Employee
                    {
                        Ecode =(int)sdr[0],
                        Ename=sdr[1].ToString(),
                        salary =(int)sdr[2],
                        Deptid=(int)sdr[3]
                    };
                    // add the employee object to the collection
                    lstEmps.Add(emp);

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return lstEmps;
        }

        public Employee SelectEmpById(int ecode)
        {
            Employee emp = null;

            try
            {
                cmd.CommandText = "select * from tbl_employee where ecode="+ecode;
                cmd.CommandType = CommandType.Text;
                //open connection
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if(sdr.Read())
                {
                    emp = new Employee
                    {
                        Ecode = (int)sdr[0],
                        Ename = sdr[1].ToString(),
                        salary = (int)sdr[2],
                        Deptid = (int)sdr[3]


                    };
                    sdr.Close();

                }
                else
                {
                    throw new Exception("no records");
                }

            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return emp;
           
        }

        public void UpdateEmpolyeeById(int ecode, int salary)
        {
            try
            {
                cmd.CommandText = "update tbl_employee set salary=salary+@sal, where ecode=@ec";
                    cmd.CommandType = CommandType.Text;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ec", ecode);
                cmd.Parameters.AddWithValue("@sal", salary);

                con.Open();
                int i = cmd.ExecuteNonQuery();
                if(i==0)
                {
                    throw new Exception("ecode  doesnot exist");
                }
            }
            catch (SqlException ex)
            {
                throw ex;

            }
        }

        public void UpdateSalaryUsingSP(int ecode, int salary)
        {
            try
            {
                //configure for the command text for store Procedure 
                cmd.CommandText = "sp_UpdateSalaryById";
                cmd.CommandType = CommandType.StoredProcedure;
                //Config the parameter
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ec",ecode);
                cmd.Parameters.AddWithValue("@sal",salary);
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch(SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();

            }
        }
    }

}