﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace supermarketSystem
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.OpenForms[0].Show();
            this.Close();
        }

        bool emptyFields()
        {
            if (fullName.Text == String.Empty) return false;
            if (password.Text == String.Empty) return false;
            if (email.Text == String.Empty) return false;
            if (address.Text == String.Empty) return false;
            if (phoneNumber.Text == String.Empty) return false;
            if (cnfrmPassword.Text == String.Empty) return false;
            return true;
        }

        void showUserMainMenu()
        {
            this.Hide();
            userMainMenu mainMenu = new userMainMenu();
            mainMenu.Show();
            this.Close();
        }

        int currID()
        {
            List<string> generalIdFile = Global.readFromFile(Global.fixedPathForGeneralID);
            int generalID = generalIdFile.Count == 0 ? 22 : int.Parse(generalIdFile[0]);
            generalID += 213;
            Global.clearFile(Global.fixedPathForGeneralID);
            Global.writeOnFile(Global.fixedPathForGeneralID, generalID.ToString());
            return generalID;
        }

        private void signUpBtn_Click(object sender, EventArgs e)
        {
            if (!emptyFields())
            {
                // If there is any empty field
                MessageBox.Show("Please fill all the fields", "Warning");
            }
            else if (Global.adminsCredentials.Contains(email.Text) || Global.usersCredentials.Contains(email.Text))
            {
                // If the email already exists
                MessageBox.Show("This email already exists !!", "Warning");
            }
            else if (password.Text != cnfrmPassword.Text)
            {
                // If the two passwords don't match
                MessageBox.Show("The passwords don't match !!", "Warning");
            }
            else
            {
                // Create a new customer in the system
                int generalID = currID();

                Global.clearFile(Global.fixedPathForGeneralID);
                Global.writeOnFile(Global.fixedPathForGeneralID, generalID.ToString());

                customer newCustomer = new customer(fullName.Text, password.Text, email.Text, phoneNumber.Text
                    , address.Text, generalID.ToString(), 500);
                Global.usersCredentials[email.Text] = password.Text;
                Global.customersIDs.Add(generalID.ToString());
                Global.allCustomers[generalID.ToString()] = newCustomer;

                Global.currCustomer = newCustomer;
                Global.writeOnFile(Global.fixedPathForAllCustomersIDs, newCustomer.Id);
                MessageBox.Show("You have complete the sign up successfully..", "Congrats !!");

                showUserMainMenu();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                password.PasswordChar = '*';
                cnfrmPassword.PasswordChar = '*';
            }
            else
            {
                password.PasswordChar = '\0';
                cnfrmPassword.PasswordChar = '\0';
            }

        }
    }
}
