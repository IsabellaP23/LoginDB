using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Finisar.SQLite;

namespace LoginV1
{
    public partial class FrmRegistro : Form
    {
        int contId = 0;


        public FrmRegistro()
        {
            InitializeComponent();
        }

        private void lnkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            this.Hide();
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= 48 && e.KeyChar <= 57 || e.KeyChar == 8)
            {

                if (e.KeyChar == 8)
                {
                    if (contId > 0)
                        contId--;
                }
                else if (contId >= 10)
                {
                    e.KeyChar = '\0';
                    MessageBox.Show("No se permiten más de 10 caracteres");
                }
                else
                {
                    contId++;
                }
            }
            else if (e.KeyChar == 13)
            {
                txtUser.Focus();
            }
            else
            {
                e.KeyChar = '\0';
                MessageBox.Show("Solo se permiten números");
            }
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnOk.Focus();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Conectar();
            MessageBox.Show("Usuario Registrado Correctamente");

            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Conectar() {


            SQLiteConnection conexion_sqlite;
            SQLiteCommand cmd_sqlite;

            conexion_sqlite = new SQLiteConnection("Data Source=Archivo.db;Version=3;Compress=True;");

            conexion_sqlite.Open();
            cmd_sqlite = conexion_sqlite.CreateCommand();

            //cmd_sqlite.CommandText = "CREATE TABLE tblUsuario(id integer primary key, User varchar(10), password varchar(12))";
            //cmd_sqlite.ExecuteNonQuery();

            string id = txtId.Text;
            string user = txtUser.Text;
            string password = txtPassword.Text;

            cmd_sqlite.CommandText = "INSERT INTO tblUsuario(id, User, password) VALUES (@ID, @User, @Password)";

            cmd_sqlite.Parameters.Add(new SQLiteParameter("@ID", DbType.String) { Value = id });
            cmd_sqlite.Parameters.Add(new SQLiteParameter("@User", DbType.String) { Value = user });
            cmd_sqlite.Parameters.Add(new SQLiteParameter("@Password", DbType.String) { Value = password });

            cmd_sqlite.ExecuteNonQuery();

            conexion_sqlite.Close();
        } 
    }
}
