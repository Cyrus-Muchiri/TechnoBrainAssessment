using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace TechnoBrainClassLibrary
{
    /* 
@author Cyrus Muchiri
@date 5/31/2021
for Technobrain Interview
 */
    public class Employees
    {

        /*Employee List*/
        private List<Employee> EmployeesList = new List<Employee>();
        public Employees(string Csv_Text)
        {


            /* 1.  Take CSV input */
            TextFieldParser parser = new TextFieldParser(new StringReader(Csv_Text));
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");
            string[] fields;
            while (!parser.EndOfData)
            {
                fields = parser.ReadFields();

                /*2.  Ensure Salary is an interger */
                string salary = fields[2];
                int n;
                bool isNumeric = int.TryParse(salary, out n);
                if (!isNumeric)
                {
                    Console.WriteLine("A Salary in the input is not an integer");
                    Environment.Exit(0);
                }

                /* 3.  Validate that an employee does not report to more than one manager *******
                 */
                if (EmployeesList.Any(Employee => Employee.EmployeeId == fields[0] && Employee.ManagerId != fields[1]))
                {

                    Console.WriteLine("One employee cannot report to more than one manager");
                    Environment.Exit(0);
                }
                /* 4.  Validate that There is only one manager */
                /*Notes : A manager is an employee that  does not have a manager id. This means field[1] will be empty 
                        : The trick would be to check whether there is another employee in the list that is empty in the manager_id property
                 */

                if (String.IsNullOrEmpty(fields[1]) && EmployeesList.Any(Employee => String.IsNullOrEmpty(Employee.ManagerId)))
                {
                    Console.WriteLine("There can only be one manager");
                    Environment.Exit(0);
                }

                /*5 . Validate that There is no circular reference (a first employee reporting to a second employee that is also under the first employee.)
                 Note : field[0] - > employee_id , field[1] -> manager_id
                 */
                if (EmployeesList.Any(Employee => Employee.ManagerId == fields[0] && Employee.EmployeeId == fields[1]))
                {
                    Console.WriteLine("There cannot be cyclic reference");
                    Environment.Exit(0);
                }


                /*6 . There is no manager who is not an employee 
                 Notes : Every row must have an Employee Id
                 */
                if (String.IsNullOrEmpty(fields[0]))
                {
                    Console.WriteLine("There cannot exist a manager who is not an employee ");
                    Environment.Exit(0);
                }

                /*7 . Create an object of type employee pass the fields to the constructor*/
                Employee employee = new Employee(fields);
                /*8 .Add the employee object to the Employee list*/
                EmployeesList.Add(employee);
            }

            parser.Close();
            Console.WriteLine("Employee list successfully captured\n");



        }

        public long SalaryBudgetManager(String Employee_ID)
        {
            /*NOTES
             * Salary budget is sum of all employees plus manager */
            /* Returns the salary budget from the specified manager */

            /*1. Get a list of employees who have the provideed param as their manager*/
            var employees = EmployeesList.Where(Employee => Employee.ManagerId == Employee_ID);
            /*Sum the salaries from the result*/
            double employees_total = 0;
            foreach (var item in employees)
            {
                employees_total += item.EmployeeSalary;
            }


            /*2. Get salary of manager provided in parameter*/
            var manager = EmployeesList.SingleOrDefault(Employee => Employee.EmployeeId == Employee_ID);
            double manager_salary = 0;
            try
            {
                 manager_salary = manager.EmployeeSalary;

            }
            catch (Exception)
            {

                Console.WriteLine("Employee does not exist");
            }
          

            /*3. Sum the salary of the manager + employees */
            long salaryBudget = Convert.ToInt64(employees_total + manager_salary);

            return salaryBudget;
        }


    }


    /*Employee Class*/
    public class Employee
    {
        public String EmployeeId { get; set; }
        public String ManagerId { get; set; }
        public double EmployeeSalary { get; set; }

        public Employee(string[] fields)
        {
            EmployeeId = fields[0];
            ManagerId = fields[1];
            EmployeeSalary = double.Parse(fields[2]);
        }
        public override string ToString()
        {
            return "\n Employee_id :" + EmployeeId + "\n ManagerId :" + ManagerId + "\n EmployeeSalary :" + EmployeeSalary;
        }
    }
}
