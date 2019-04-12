using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMSBusinessLib;
using HRMSEnitiesLib;

namespace HRMS.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee emp;
            HRMSBussiness bll = new HRMSBussiness();
            int choice;
            do
            {
                DispalyMenu();
                Console.Write("enter ur choice:");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        //take input for Insert
                        emp = TakeEmpInput();
                        bll.AddEmployee(emp);
                        Console.WriteLine("\n Record inserted");
                        DisplayAllEmps(bll);
                        break;
                    case 2:
                        //take input for Delete
                        Console.Write("enter Ecode");
                        int ecode = int.Parse(Console.ReadLine());
                        List<Employee> lst = bll.GetAllEmps();
                        int r = 0;
                        foreach (Employee e in lst)
                        {
                            if (e.Ecode == ecode)
                            {
                              bll.RemoveEmpById(ecode);
                                r = 1;
                                break;
                            }
                            
                           
                        }
                        if (r == 1)
                        {
                            Console.WriteLine("record is deleted sucessfully");
                        }
                        else
                        {
                            Console.WriteLine("record is not Avalible");
                        }


                        DisplayAllEmps(bll);
                        break;
                    case 3:
                        //take input for Update
                        Console.Write("\n enter ecode");
                        int ec = int.Parse(Console.ReadLine());
                        Console.Write("enter salary for update:");
                        int sal = int.Parse(Console.ReadLine());

                        //upadate using Business layer
                        bll.UpdateEmpolyeeById(ec, sal);
                        Console.WriteLine("record updated");
                        DisplayAllEmps(bll);
                        break;

                    case 4:
                        //display all Employees
                        DisplayAllEmps(bll);
                        break;
                    case 5:
                        //take inputs for ecode for searching
                        Console.Write("\n enter ecode:");
                        int id = int.Parse(Console.ReadLine());
                        emp = null;
                        emp = bll.GetEmpById(id);
                        //
                        if(emp == null)
                        {
                            Console.WriteLine("Ecode is not exist");
                        }
                        else
                        {
                            Console.WriteLine(emp.Ecode + "\t" + emp.Ename + "\t" + emp.salary + "\t" + emp.Deptid);
                        }
                        break;
                    case 6:
                        Console.WriteLine("Exiting");
                        break;
                    default:
                        Console.WriteLine("Invalid choice,check ur input");
                        break;

                }

            } while (choice !=6);
        }

        private static void DisplayAllEmps(HRMSBussiness bll)
        {
            List<Employee> result = bll.GetAllEmps();
            foreach (Employee e in result)
            {
                Console.WriteLine(e.Ecode + "\t" + e.Ecode + "\t" + e.salary + "\t" + e.Deptid);

            }
        }

        private static Employee TakeEmpInput()
        {
            Employee emp = new Employee();
            Console.Write("enter ecode:");
            emp.Ecode = int.Parse(Console.ReadLine());
            Console.Write("enter ename:");
            emp.Ename = Console.ReadLine();
            Console.Write("enter salary:");
            emp.salary = int.Parse(Console.ReadLine());
            Console.Write("enter deptid:");
            emp.Deptid = int.Parse(Console.ReadLine());
            return emp;
        }

        private static void DispalyMenu()
        {
            Console.WriteLine("1.Add Employee");
            Console.WriteLine("2.Delect Employee");
            Console.WriteLine("3.Updaye Employee");
            Console.WriteLine("4.Display All Employee");
            Console.WriteLine("5.Display Employee By Id");
            Console.WriteLine("6.Exit");
        }
    }
}
