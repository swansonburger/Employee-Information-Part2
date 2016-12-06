/*
 * Name: Fraser Swan/Shivam Gulati
 * Date: December 6, 2016
 * Purpose: To be able to record as many employee's information as you would like and save them to a file. 
 * Also be able to go through all the employees entered and view all information entered also weekly pay.         
 */
using System;
using System.IO;
using System.Windows.Forms;

namespace EmployeeInformation
{
    public partial class frmInput : Form
    {
        //Module level variables
        private StreamWriter employeeStreamWriter;
        private String path;

        public frmInput()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Close StreamWriter
            if (employeeStreamWriter != null)
            {
                employeeStreamWriter.Close();
            }
            //Close form
            this.Close();
        }

        //Makes sure data is valid
        private bool Valid()
        {
            return Validator.IsPresent(txtEmployeeName) &&
                   Validator.IsPresent(txtEmployeeNum) &&
                   Validator.IsPresent(txtHoursWorked) &&
                   Validator.IsInteger(txtHoursWorked) &&
                   Validator.IsWithinRange(txtHoursWorked, 0, 40);
        }

        private void saveFile()
        {      
            //Set some properties of the file save dialog
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Title = "Select File or Directory";
            saveFileDialog1.Filter = "Text File (*.txt)|*.txt|All Files(*.*)|*.*";

            //Shows a open file dialog and prompts the user to select a file
            DialogResult response = saveFileDialog1.ShowDialog();
            
            if (response != DialogResult.Cancel)
            {
                try
                {
                    //Creates file
                    employeeStreamWriter = new StreamWriter(saveFileDialog1.FileName, true);

                    //Saves file path
                    path = Path.GetFullPath(saveFileDialog1.FileName);

                    this.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //Close form            
                this.Close();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Clear all text boxes
            txtEmployeeName.Clear();
            txtEmployeeNum.Clear();
            txtHoursWorked.Clear();

            //Focus back to EmployeeName text box
            txtEmployeeName.Focus();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            //Close StreamWriter
            employeeStreamWriter.Close();

            //Creates new frmOutput
            frmOutput outputForm = new frmOutput();
            outputForm.filePath = path;

            //Hide input form
            this.Hide();
            outputForm.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Valid())
            {
                try
                {
                    //Saves text from textboxes into employeeStreamWriter
                    employeeStreamWriter.WriteLine(txtEmployeeName.Text);
                    employeeStreamWriter.WriteLine(txtEmployeeNum.Text);
                    employeeStreamWriter.WriteLine(txtHoursWorked.Text);

                    //Confirm message - tells user info has been saved
                    MessageBox.Show("Employee information has been saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.None);

                    //Clears all text boxes, for next employee
                    txtEmployeeName.Clear();
                    txtEmployeeNum.Clear();
                    txtHoursWorked.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmInput_Load(object sender, EventArgs e)
        {
            //Call saveFile method 
            saveFile();
        }
        
    }
}
