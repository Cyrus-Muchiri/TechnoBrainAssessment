using System;
using TechnoBrainClassLibrary;
namespace ConsoleApp
{
    class ConsoleApp

    {
        static void Main(string[] args)
        {


            //  string[] csv_text = System.IO.File.ReadAllLines(@"input.txt");
            // string csv_text = System.IO.File.ReadAllText(@"input.txt");
            string csv_text = @"Emplyee4,Employee2,500
                          Emplyee2,Employee4,500
                          Employee3,Employee1,800
                          Employee1,,1000
                          Employee5,Employee1,500
                          Employee2,Employee1,500";



            Console.Write("Input CSV is  : "+csv_text+"\n");
            Console.WriteLine("The string has been captured. Processing \n");


            //Create Employees objecct
            Employees employee = new Employees(csv_text);



            //  Test the Salary Budget Method

            Console.Write("To test the Salary Budget Method , Enter an Employee Id to get the salary budget : ");
            String Employee_Id = Console.ReadLine();
            Console.WriteLine("The string has been captured. Processing \n");

            long salary = employee.SalaryBudgetManager(Employee_Id);
            Console.WriteLine("The salary budget for " + Employee_Id + " is :  " + salary);
        }
    }
}
