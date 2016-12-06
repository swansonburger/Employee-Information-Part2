using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeInformation
{
    public class Employee
    {
        private String employeeName;
        private String employeeNum;
        private decimal hoursWorked;
        private const decimal PayRate = 10.50m;

        public Employee(String employeeName, String employeeNum, decimal hoursWorked)
        {
            EmployeeName = employeeName;
            EmployeeNum = employeeNum;
            HoursWorked = hoursWorked;
        }
        public string EmployeeName { get; set; }

        public string EmployeeNum { get; set; }
       
        public decimal HoursWorked
        {
            get
            {
                return hoursWorked;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Hours worked cannot be less than 0");
                }
                hoursWorked = value;
            }


        }
        public decimal getPay()
        {
            decimal pay = 0;
            pay = hoursWorked * PayRate;

            return pay;
        }

    }
}
