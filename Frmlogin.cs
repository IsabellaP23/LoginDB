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
    public partial class FrmLogin : Form
    {
        int cont = 0;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            bool isAuthenticated = Autenticacion();

            if (isAuthenticated)
            {
                frmBienvenido frmbienvenido = new frmBienvenido();
                frmbienvenido.lblUser.Text = txtUsuario.Text;
                frmbienvenido.Show();
                this.Hide();
            }
            else
            {
                cont++;
                MessageBox.Show("Usuario o contraseña incorrectos");

                // Si hay demasiados intentos fallidos, cierra el formulario
                if (cont == 3)
                {
                    MessageBox.Show("Demasiados intentos incorrectos, inténtelo más tarde :)");
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkRegistro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmRegistro frmRegistro = new FrmRegistro();
            frmRegistro.Show();
            this.Hide();
        }

        private bool Autenticacion()
        {
            SQLiteConnection conexion_sqlite = null;
            SQLiteCommand cmd_sqlite = null;
            SQLiteDataReader datareader_sqlite = null;

            try
            {
                conexion_sqlite = new SQLiteConnection("Data Source=Archivo.db;Version=3;");
                conexion_sqlite.Open();

                string user = txtUsuario.Text;
                string password = txtContraseña.Text;

                cmd_sqlite = new SQLiteCommand("SELECT id, user, password FROM tblUsuario WHERE User = @User AND password = @Password", conexion_sqlite);
                cmd_sqlite.Parameters.Add(new SQLiteParameter("@User", DbType.String) { Value = user });
                cmd_sqlite.Parameters.Add(new SQLiteParameter("@Password", DbType.String) { Value = password });

                datareader_sqlite = cmd_sqlite.ExecuteReader();

                if (datareader_sqlite.Read())
                {
                    MessageBox.Show("Autenticación exitosa.");

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la operación: " + ex.Message);
                return false;
            }
            finally
            {
                datareader_sqlite?.Close();
                conexion_sqlite?.Close();
            }

        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtContraseña.Focus();
            }
        }
        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnOk.Focus();
            }
        }

        private void btnOk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnOk.PerformClick();
            }
        }

        
    }
}
