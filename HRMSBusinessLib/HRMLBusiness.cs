using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMSEnitiesLib;
using HRMSDataAccessLib;


namespace HRMSBusinessLib
{
    interface IHRMSBussiness
    {
        void AddEmployee(Employee emp);
        void RemoveEmpById(int ecode);
        void UpdateEmpolyeeById(int ecode, int salary);
        List<Employee> GetAllEmps();
        Employee GetEmpById(int ecode);
        //void UpdateSalUsingSp(int ecode, int salary);
       // double GetBonus(int salary);
      //  void DoTransaction();
    }
    public class HRMSBussiness : IHRMSBussiness
    {
        // HRMSDataAccess dal;
        // HRMSAdoConnector dal;
        HRMSAdoDisconnected dal;
        public HRMSBussiness()
        {
            // dal = new HRMSDataAccess();

            // dal = new HRMSAdoConnector();
            dal = new HRMSAdoDisconnected();
        }
        public void AddEmployee(Employee emp)
        {
            dal.InsertEmployee(emp);
        }

        //public void DoTransaction()
        //{
        //  //  dal.DoTransaction();
        //}

        public List<Employee> GetAllEmps()
        {
            List<Employee> lstEmps;
            lstEmps = dal.SelectAllEmps();
            return lstEmps;
        }

       //// public double GetBonus(int salary)
       // {
       //     double bonus = 0;
       //     bonus = dal.GetBonus(salary);
       //     return bonus;
       // }

        public Employee GetEmpById(int ecode)
        {
            Employee emp;
            emp = dal.SelectEmpById(ecode);

            return emp;
        }

        public void RemoveEmpById(int ecode)
        {
            dal.DeleteEmpById(ecode);
        }

        public void UpdateEmpolyeeById(int ecode, int salary)
        {
            dal.UpdateEmpolyeeById(ecode, salary);

        }

        //public void UpdateSalUsingSp(int ecode, int salary)
        //{
        //    dal.UpdateSalaryUsingSP(ecode,salary);
        //}
    }
}
