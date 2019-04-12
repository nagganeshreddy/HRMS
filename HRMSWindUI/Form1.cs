using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HRMSBusinessLib;
using HRMSEnitiesLib;


namespace HRMSWindUI
{
    public partial class Form1 : Form
    {
        HRMSBussiness bll;
        public Form1()
        {
            bll = new HRMSBussiness();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAddEmp_Click(object sender, EventArgs e)
        {
            //Take input form TextBox
            Employee emp = new Employee
            {
                Ecode =int.Parse( txtEcode.Text),
                Ename=txtEname.Text,
                salary=int.Parse(txtSalary.Text),
                Deptid=int.Parse(txtDeptid.Text)
            };
            //Add Records Using Business layer
            bll.AddEmployee(emp);
            MessageBox.Show("record inserted");

            //Display All Employees records
            List<Employee> result = bll.GetAllEmps();
            dgvEmps.DataSource = null;
            dgvEmps.DataSource = result;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int ecode = int.Parse(txtEcode.Text);

               // if (bll.GetEmpById(ecode) == null)
               // {
                 //   MessageBox.Show("Employee does not Exist");

               // }
               // else
               // {
                    bll.RemoveEmpById(ecode);
                    MessageBox.Show("Employee Update");
                    List<Employee> result = bll.GetAllEmps();
                    dgvEmps.DataSource = null;
                    dgvEmps.DataSource = result;

               // }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int ecode = int.Parse(txtEcode.Text);
            int salary = int.Parse(txtSalary.Text);
            if (bll.GetEmpById(ecode) == null)
            {
                Console.WriteLine("Employee does't Exist");

            }
            else
            {
                bll.UpdateEmpolyeeById(ecode, salary);
                MessageBox.Show("Employee Update");
                List<Employee> result = bll.GetAllEmps();
                dgvEmps.DataSource = null;
                dgvEmps.DataSource = result;
            }
        }

        private void btnFindbyId_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            int Ecode = int.Parse(txtEcode.Text);
            emp = bll.GetEmpById(Ecode);
            if ( emp== null)
            {
                MessageBox.Show(emp.Ecode + "doesnot exist");
            }
            else
            {
                List<Employee> empp = new List<Employee>();
                empp.Add(emp);
                dgvEmps.DataSource = null;
                dgvEmps.DataSource = empp;
            }
        }

        private void btnCalSP_Click(object sender, EventArgs e)
        {
            int ecode = int.Parse(txtEcode.Text);
            int salary = int.Parse(txtSalary.Text);
            if (bll.GetEmpById(ecode) == null)
            {
                Console.WriteLine("Employee does't Exist");

            }
            else
            {
               // bll.UpdateSalUsingSp(ecode, salary);
                MessageBox.Show("Salary Updated using SP");
                List<Employee> result = bll.GetAllEmps();
                dgvEmps.DataSource = null;
                dgvEmps.DataSource = result;
            }
        }

        private void btnBonus_Click(object sender, EventArgs e)
        {
            int salary;
            double bonus;
            salary = int.Parse(txtSalary.Text);
           // bonus = bll.GetBonus(salary);
           // MessageBox.Show("Bonus:" + bonus);

        }

        private void DoTransaction_Click(object sender, EventArgs e)
        {
            try
            {
              //  bll.DoTransaction();
                MessageBox.Show("Transaction Complted");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Transaction rolled back:" + ex.Message);
            }


        }

        private void btnDisplayAll_Click(object sender, EventArgs e)
        {
            List<Employee> emplst = bll.GetAllEmps();
            //bind to DataGridView
            dgvEmps.DataSource = null;
            dgvEmps.DataSource = emplst;
        }
    }
}
