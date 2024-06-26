using System.Data.Entity;
using System.Data.SqlClient;
using System.DirectoryServices.ActiveDirectory;

namespace _06182024
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection("server=adminsystem; database=mydatabase; integrated security=true");

        Form2 form2 = new Form2();
        Form3 form3 = new Form3();
        Form4 form4 = new Form4();
        Form5 form5 = new Form5();
        Form6 form6 = new Form6();

        Dictionary<string, Button> buttonMap = new Dictionary<string, Button>();
        public Form1()
        {
            InitializeComponent();

            buttonMap.Add("co01", btnc1);
            buttonMap.Add("co02", btnc2);
            buttonMap.Add("co03", btnc3);
            buttonMap.Add("co04", btnc4);
            buttonMap.Add("co05", btnc5);
            buttonMap.Add("co06", btnc6);
            buttonMap.Add("co07", btnc7);
            buttonMap.Add("co08", btnc8);
            buttonMap.Add("co09", btnc9);
            buttonMap.Add("co10", btnc10);
            buttonMap.Add("co11", btnc11);
            buttonMap.Add("co12", btnc12);
            buttonMap.Add("co13", btnc13);
            buttonMap.Add("co14", btnc14);
            buttonMap.Add("co15", btnc15);
            buttonMap.Add("co16", btnc16);

            this.Load += new EventHandler(Form1_Load);
            btnfree.Click += new EventHandler(Form1_Load);
            btnassign.Click += new EventHandler(Form1_Load);
            btnc1.Click += new EventHandler(btnSelection);
            btnc2.Click += new EventHandler(btnSelection);
            btnc3.Click += new EventHandler(btnSelection);
            btnc4.Click += new EventHandler(btnSelection);
            btnc5.Click += new EventHandler(btnSelection);
            btnc6.Click += new EventHandler(btnSelection);
            btnc7.Click += new EventHandler(btnSelection);
            btnc8.Click += new EventHandler(btnSelection);
            btnc9.Click += new EventHandler(btnSelection);
            btnc10.Click += new EventHandler(btnSelection);
            btnc11.Click += new EventHandler(btnSelection);
            btnc12.Click += new EventHandler(btnSelection);
            btnc13.Click += new EventHandler(btnSelection);
            btnc14.Click += new EventHandler(btnSelection);
            btnc15.Click += new EventHandler(btnSelection);
            btnc16.Click += new EventHandler(btnSelection);

            btnc1.MouseLeave += new EventHandler(panelLeave);
            btnc1.MouseEnter += new EventHandler(panelEnter);
            btnc3.MouseEnter += new EventHandler(panelEnter);
            btnc3.MouseLeave += new EventHandler(panelLeave);



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            methodLoad(sender, e);
            availableCount(sender, e);
            busyCount(sender, e);
            totalCount(sender, e);

        }

        private void methodLoad(object sender, EventArgs e)
        {
            connection.Open();

            try
            {
                string code = ("select code, isbusy from airplane");
                SqlCommand command = new SqlCommand(code, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string codeID = reader["code"].ToString();
                    int isbusy = Convert.ToInt32(reader["isbusy"]);

                    if (buttonMap.ContainsKey(codeID))
                    {
                        Button btn = buttonMap[codeID];
                        if (isbusy == 1)
                        {
                            btn.FlatAppearance.BorderSize = 1;
                            btn.BackColor = Color.Red;
                            btn.ForeColor = Color.White;
                        }
                        else if (isbusy == 0)
                        {
                            btn.FlatAppearance.BorderSize = 1;
                            btn.BackColor = Color.White;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                txtClear(sender, e);
                form3.Show();
            }

            connection.Close();
        }

        private void btnassign_Click(object sender, EventArgs e)
        {
            if (btnassign.Tag == null)
            {
                txtClear(sender, e);
                form5.Show();
            }
            else
            {
                TextBox[] textBoxes = { txtfisrtname, txtlastname, txtdocument, txtemail };

                foreach (TextBox textBox in textBoxes)
                {
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        txtClear(sender, e);
                        form5.Show();

                    }
                    else
                    {
                        connection.Open();

                        try
                        {
                            string code = ("update airplane set isbusy = 1, firstname='" + txtfisrtname.Text + "', lastname='" + txtlastname.Text + "', documment='" + txtdocument.Text + "', email='" + txtemail.Text + "' where code = '" + btnassign.Tag + "'");
                            SqlCommand command = new SqlCommand(code, connection);
                            command.ExecuteNonQuery();
                            txtClear(sender, e);
                            form6.Show();
                        }
                        catch (Exception ex)
                        {
                            txtClear(sender, e);
                            form3.Show();
                        }

                        connection.Close();
                    }
                }
            }



        }

        private void btnfree_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string buttonCode = button.Text;

            TextBox[] textBoxes = { txtfisrtname, txtlastname, txtdocument, txtemail };

            foreach (TextBox textBox in textBoxes)
            {
                if (!string.IsNullOrEmpty(textBox.Text))
                {
                    txtClear(sender, e);
                    form5.Show();
                }
                else
                {
                    connection.Open();

                    try
                    {
                        string code = ("update airplane set isbusy = 0, firstname='', lastname='', documment='', email='' where code = '" + btnassign.Tag + "'");
                        SqlCommand command = new SqlCommand(code, connection);
                        command.ExecuteNonQuery();
                        txtClear(sender, e);
                        //if ( == Color.White) 
                        //{
                        //    btnc14.ForeColor = Color.Black;
                        //}
                        form6.Show();
                    }
                    catch (Exception ex)
                    {
                        txtClear(sender, e);
                        form3.Show();
                    }

                    connection.Close();
                }
            }

        }

        private void btnSelection(object sender, EventArgs e)
        {
            Button[] buttons = { btnc1, btnc2, btnc3, btnc4, btnc5, btnc6, btnc7, btnc8, btnc9, btnc10, btnc11, btnc12, btnc13, btnc14, btnc15, btnc16 };
            foreach (Button button in buttons)
            {
                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.BorderColor = Color.Black;
            }

            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {


            }
        }

        private void panelLeave(object sender, EventArgs e)
        {
            pnlinfo.Visible = false;
        }
        private void panelEnter(object sender, EventArgs e)
        {
            loadPanelInfo(sender, e);
            pnlinfo.Visible = true;
            pnlinfo.Location = new Point(1142, 62);

        }
        private void btnc1_Click(object sender, EventArgs e)
        {
            btnassign.Tag = "co01";
            btnfree.Tag = "co01";
        }

        private void btnc2_Click(object sender, EventArgs e)
        {

            btnassign.Tag = "co02";
            btnfree.Tag = "co02";
        }

        private void btnc3_Click(object sender, EventArgs e)
        {

            btnassign.Tag = "co03";
            btnfree.Tag = "co03";
        }

        private void btnc4_Click(object sender, EventArgs e)
        {

            btnassign.Tag = "co04";
            btnfree.Tag = "co04";
        }

        private void btnc5_Click(object sender, EventArgs e)
        {

            btnassign.Tag = "co05";
            btnfree.Tag = "co05";
        }

        private void btnc6_Click(object sender, EventArgs e)
        {

            btnassign.Tag = "co06";
            btnfree.Tag = "co06";
        }

        private void btnc7_Click(object sender, EventArgs e)
        {

            btnassign.Tag = "co07";
            btnfree.Tag = "co07";
        }

        private void btnc8_Click(object sender, EventArgs e)
        {

            btnassign.Tag = "co08";
            btnfree.Tag = "co08";
        }

        private void availableCount(object sender, EventArgs e)
        {
            connection.Open();
            string code = ("select count(*) from airplane where isbusy = 0");
            SqlCommand command = new SqlCommand(code, connection);

            int count = (int)command.ExecuteScalar();
            btnavailable.Text = count.ToString();
            connection.Close();
        }

        private void busyCount(object sender, EventArgs e)
        {
            connection.Open();
            string code = ("select count(*) from airplane where isbusy = 1");
            SqlCommand command = new SqlCommand(code, connection);
            int count = (int)command.ExecuteScalar();
            btnbusy.Text = count.ToString();
            connection.Close();
        }

        private void loadPanelInfo(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string buttoncode = button.Text;

            connection.Open();
            string code = ("select firstname,lastname,documment,email from airplane where code=@code");
            SqlCommand command = new SqlCommand(code, connection);
            command.Parameters.AddWithValue("code", buttoncode);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string firstname = reader["firstname"].ToString();
                string lastname = reader["lastname"].ToString();
                string documment = reader["documment"].ToString();
                string email = reader["email"].ToString();

                btnpanel1.Text = firstname;
                btnpanel2.Text = lastname;
                btnpanel3.Text = documment;
                btnpanel4.Text = email;
            }
            reader.Close();
            connection.Close();
        }
        private void totalCount(object sender, EventArgs e)
        {
            connection.Open();
            string code = ("select count(isbusy) from airplane");
            SqlCommand command = new SqlCommand(code, connection);
            int count = (int)command.ExecuteScalar();
            btnna.Text = count.ToString();
            connection.Close();
        }

        private void btnc9_Click(object sender, EventArgs e)
        {
            btnassign.Tag = "co09";
            btnfree.Tag = "co09";
        }

        private void btnc10_Click(object sender, EventArgs e)
        {
            btnassign.Tag = "co10";
            btnfree.Tag = "co10";
        }

        private void btnc11_Click(object sender, EventArgs e)
        {
            btnassign.Tag = "co11";
            btnfree.Tag = "co11";
        }

        private void btnc12_Click(object sender, EventArgs e)
        {
            btnassign.Tag = "co12";
            btnfree.Tag = "co12";
        }

        private void btnc13_Click(object sender, EventArgs e)
        {
            btnassign.Tag = "co13";
            btnfree.Tag = "co13";
        }

        private void btnc14_Click(object sender, EventArgs e)
        {
            btnassign.Tag = "co14";
            btnfree.Tag = "co14";
        }

        private void btnc15_Click(object sender, EventArgs e)
        {
            btnassign.Tag = "co15";
            btnfree.Tag = "co15";
        }

        private void btnc16_Click(object sender, EventArgs e)
        {
            btnassign.Tag = "co16";
            btnfree.Tag = "co16";
        }

        private void txtClear(object sender, EventArgs e)
        {
            txtfisrtname.Clear();
            txtlastname.Clear();
            txtdocument.Clear();
            txtemail.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Has presionado el boton");
        }
    }
}
