using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISTEMA.Formularios
{
    public partial class IncisioSesion : Form
    {
        public IncisioSesion()
        {
            InitializeComponent();
        }
        Conexion.Conectar con = new Conexion.Conectar();
        private void iconButton1_Click(object sender, EventArgs e)
        {
            //es para saber donde guaarda la base de datos aunque realmente esta en el mismo directorio
            string DBName = Path.Combine(Application.StartupPath, " ");
           

            string user = this.textBox1.Text;
            string pass = this.textBox2.Text;
            var result=con.SentenciasFiltradas(String.Format("select * from Usuario where Usuario='{0}' and contrasenia='{1}'",user,pass)).ExecuteReader();

            int id = -1;
            string us = "";
            while (result.Read())
            {
                id = int.Parse(result["id"].ToString());
                us = result["Usuario"].ToString();
            }
            if (id==-1)
            {
                this.label3.Visible = true;
            }
            else
            {                
                this.Hide();
                Form1 fom = new Form1(id,us);
                fom.Show();               
            }
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
