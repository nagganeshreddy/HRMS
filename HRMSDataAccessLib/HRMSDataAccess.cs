using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HRMSEnitiesLib;
namespace HRMSDataAccessLib
{
    interface IHRMSDataAccess
    {
        void InsertEmployee(Employee emp);
        void DeleteEmpById(int ecode);
        void UpdateEmpolyeeById(int ecode, int salary);
        List<Employee> SelectAllEmps();
        Employee SelectEmpById(int ecode);
    }
    public class HRMSDataAccess : IHRMSDataAccess
    {
        List<Employee> lstEmps;
        public HRMSDataAccess()
        {
            lstEmps = new List<Employee>();
        }
        public void DeleteEmpById(int ecode)
        {
            for (int i=0;i<lstEmps.Count;i++)
            {
                if(lstEmps[i].Ecode==ecode)
                {
                    lstEmps.RemoveAt(i);

                    break;
                }
            }
        }

        public void InsertEmployee(Employee emp)
        {
            lstEmps.Add(emp);
        }

        public List<Employee> SelectAllEmps()
        {
            List<Employee> result = new List<Employee>();
            result = lstEmps;
            return result;
        }

        public Employee SelectEmpById(int ecode)
        {
            Employee result = null;
            foreach(Employee e in lstEmps)
            {
                if (e.Ecode==e.Ecode)
                {                  
                    result = e;
                    break;
                }
             
            }

            return result;
        }

        public void UpdateEmpolyeeById(int ecode, int salary)
        {
            for (int i=0; i<lstEmps.Count;i++)
            {
                if(lstEmps[i].Ecode==ecode)
                {
                    //update the salary
                    lstEmps[i].salary = salary;
                    break;
                }
            }
        }
    }
}
