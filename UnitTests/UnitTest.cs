using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechnoBrainClassLibrary;
namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {

        [TestMethod]
        public void Constructor_SalaryHasToBeInteger()
        {
            // Arrange
            string csv = @"Emplyee4,Employee2,b
                        Employee3,Employee1,800
                        Employee1,,1000
                        Employee5,Employee1,500
                        Employee2,Employee1,500";


            // Act
            Employees employees = new Employees(csv);

            // Assert
            Assert.IsNotNull(employees, "Salaries have to be integer");
        }

        [TestMethod]
        public void Constructor_EmployeeDoesNotReportToMoreThanOneManager()
        {
            // Arrange
            string csv = @"Emplyee4,Employee2,500
                        Employee3,Employee1,800
                        Employee3,Employee2,800
                        Employee1,,1000
                        Employee5,Employee1,500
                        Employee2,Employee1,500";


            // Act
            Employees employees = new Employees(csv);

            // Assert
            Assert.IsNotNull(employees, "An Employee should not report to more than one manager");
        }
        [TestMethod]
        public void Constructor_OnlyOneEmployeeWithNoManager()
        {
            // Arrange
            string csv = @"Emplyee4,Employee2,200
                        Employee3,Employee1,800
                        Employee1,,1000
                        Employee7,,1000
                        Employee5,Employee1,500
                        Employee2,Employee1,500";


            // Act
            Employees employees = new Employees(csv);

            // Assert
            Assert.IsNotNull(employees, "There should only be one CEO");
        }

        [TestMethod]
        public void Constructor_NoCircularReference()
        {
            // Arrange
            string csv = @"Emplyee4,Employee2,500
                        Emplyee2,Employee4,500
                        Employee3,Employee1,800
                        Employee1,,1000
                        Employee5,Employee1,500
                        Employee2,Employee1,500";


            // Act
            Employees employees = new Employees(csv);

            // Assert
            Assert.IsNotNull(employees, "There should not be circular reference");
        }

        [TestMethod]
        public void Constructor_NoManagerIsNotAnEmployee()
        {
            // Arrange
            string csv = @"Emplyee4,Employee2,b
                        Employee3,Employee1,800
                        ,,1000
                        Employee5,Employee1,500
                        Employee2,Employee1,500";


            // Act
            Employees employees = new Employees(csv);

            // Assert
            Assert.IsNotNull(employees, "No manager is not an employee");
        }

        [TestMethod]
        public void InstanceMenthod_ReturnsSalaryBudget()
        {
            // Arrange
            string csv = @"Emplyee4,Employee2,b
                        Employee3,Employee1,800
                        ,,1000
                        Employee5,Employee1,500
                        Employee2,Employee1,500";
            string manager_id = "employee1";


            // Act
            Employees employees = new Employees(csv);
            long total_salary = employees.SalaryBudgetManager(manager_id);
            // Assert,
            Assert.AreEqual(total_salary, 2800, "Expected answer is 2300");
        }
    }
}
