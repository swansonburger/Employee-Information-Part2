/*
 * Name: Fraser Swan/Shivam Gulati
 * Date: December 6, 2016
 * Purpose: To be able to record as many employee's information as you would like and save them to a file. 
 * Also be able to go through all the employees entered and view all information entered also weekly pay.         
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace EmployeeInformation
{
    public partial class frmOutput : Form
    {
        //Module level variables
        private int index = 0;     
        private StreamReader employeeStreamReader;
        private List<Employee> employees = new List<Employee>();

        public frmOutput()
        {
            InitializeComponent();
        }

        public String filePath { get; set; }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Close stream reader
            if (employeeStreamReader != null)
            {
                employeeStreamReader.Close();
            }
            
            //Close form
            this.Close();

            //Close hidden input form
            frmInput newfrmInput = new frmInput();
            newfrmInput.Close();
        }

        private void openFile()
        {
            String EmployeeName, EmployeeNum;
            Decimal HoursWorked;

            //Set some properties of the file open dialog
            openFileDialog1.InitialDirectory = Path.GetDirectoryName(filePath);
            openFileDialog1.FileName = Path.GetFileName(filePath);

            try
            {
                employeeStreamReader = new StreamReader(openFileDialog1.FileName);

                //While there is data in the file
                while (employeeStreamReader.Peek() != -1)
                {
                    EmployeeName = employeeStreamReader.ReadLine();
                    EmployeeNum = employeeStreamReader.ReadLine();
                    HoursWorked = Decimal.Parse(employeeStreamReader.ReadLine());

                    Employee eInfo = new Employee(EmployeeName, EmployeeNum, HoursWorked);
                    
                    employees.Add(eInfo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (employeeStreamReader != null)
                {
                    employeeStreamReader.Close();
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //Inserting next files into text boxes
            txtEmployeeName.Text = employees[index + 1].EmployeeName;
            txtEmployeeNum.Text = employees[index + 1].EmployeeNum;
            txtHoursWorked.Text = employees[index + 1].HoursWorked.ToString();
            txtWeeklyPay.Text = employees[index + 1].getPay().ToString("c");

            //Increase the index by 1
            index++;

            //If index is the same value as the amount of files in the list,
            //than display message - No more records 
            if (index == employees.Count() - 1)
            {
                MessageBox.Show("There are no other employee records", "No More Records", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnNext.Visible = false;
            }
        }

        private void frmOutput_Load(object sender, EventArgs e)
        {
            //Call openFile method
            openFile();

            //Display first record in the list as soon as form loads
            txtEmployeeName.Text = employees[index].EmployeeName;
            txtEmployeeNum.Text = employees[index].EmployeeNum;
            txtHoursWorked.Text = employees[index].HoursWorked.ToString();
            txtWeeklyPay.Text = employees[index].getPay().ToString("c");
        }
    }         
}
