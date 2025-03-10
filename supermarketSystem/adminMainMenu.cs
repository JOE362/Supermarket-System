﻿using iTextSharp.text.pdf.qrcode;
using supermarketSystem.Properties;
using System;
using System.Collections;
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
    public partial class adminMainMenu : Form
    {
        public adminMainMenu()
        {
            InitializeComponent();
        }
        bool close = true;
        public void generatecontrols()
        {
            lblcash.Text = Global.CashBalance.ToString() + " $";
            lblname.Text = Global.currAdmin.FullName;
            flowLayoutPanel1.Controls.Clear();
            if (Global.allProducts.Count > 0)
            {
                int n = Global.allProducts.Count;
                MyItem[] item = new MyItem[n];
                int i = 0;
                foreach (KeyValuePair<string, product> de in Global.allProducts)
                {
                    item[i] = new MyItem();
                    item[i].Icon = de.Value.image;
                    item[i].Tiltle = de.Value.Name;
                    item[i].Price = de.Value.Price.ToString();
                    item[i].Product = de.Value;
                    item[i].id = (string)de.Key;
                    item[i].Menu = this;
                    item[i].quantity = de.Value.Quantity.ToString(); 
                    flowLayoutPanel1.Controls.Add(item[i]);
                    i++;
                }
            }
            MyItem add = new MyItem();
            add.add_item();
            add.Menu = this;
            flowLayoutPanel1.Controls.Add(add);
            add.Click += new System.EventHandler(this.MyItem_click);
        }
        void MyItem_click(object sender, EventArgs e)
        {           
            createProduct CP = new createProduct();          
            CP.ShowDialog();
            generatecontrols();
        }
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void adminMainMenu_Load(object sender, EventArgs e)
        {
            generatecontrols();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void lblname_Click(object sender, EventArgs e)
        {

        }

        private void btnlogs_Click(object sender, EventArgs e)
        {
            accessLog al = new accessLog();
            al.ShowDialog();
        }

        private void btnsign_Click(object sender, EventArgs e)
        {
            Global.currAdmin = null;
            Application.OpenForms[0].Show();
            close = false;
            this.Close();
        }

        private void btnabout_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
            close = false;
            this.Close();
        }

        private void adminMainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (close == true)
            {
                DialogResult result = MessageBox.Show("Are you sure you wish to Quit?", "Exit Application", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    //Application.Exit();
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
