using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SISTEMA.Formularios
{
    public partial class EliminarP : Form
    {
        public EliminarP()
        {
            InitializeComponent();
            this.llenardatagrid();
        }
        Conexion.Conectar conectado = new Conexion.Conectar();
        private void EliminarP_Load(object sender, EventArgs e)
        {
        }
        public void llenardatagrid()
        {
            DataGridViewCheckBoxColumn tuColumna = new DataGridViewCheckBoxColumn();
            this.dataGridView1.Columns.Add(tuColumna);
            conectado.Sentencias(ref dataGridView1, "select * from Producto");
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[0];
                    this.dataGridView1.BeginEdit(true);
                }
            }
            catch (Exception ee)
            {
            }
                     
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            try
            {
                int row = this.dataGridView1.Rows.Count - 1;
                string sentencia = "delete  from Producto where ";
                int na = 0;
                for (int i = 0; i < row; i++)
                {
                    bool isChecked = (bool)dataGridView1[0, i].EditedFormattedValue;
                    if (isChecked == true)
                    {
                        na++;
                        sentencia += "idPro=" + this.dataGridView1.Rows[i].Cells[1].Value.ToString() + " OR ";
                    }
                }
                conectado.SentenciasPuras(sentencia.Substring(0, sentencia.Length - 3));
                MessageBox.Show("LOS REGISTROS FUERON ELIMINADOS CORRECTAMENTE");
                this.llenardatagrid();
            }
            catch (Exception)
            {
            }
           
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "stock")
            {
                e.CellStyle.ForeColor = Color.Black;
                e.CellStyle.BackColor = Color.Yellow;
            }else if (this.dataGridView1.Columns[e.ColumnIndex].Name == "preciocompra")
            {
                e.CellStyle.ForeColor = Color.Yellow;
                e.CellStyle.BackColor = Color.DarkRed;
            }else if (this.dataGridView1.Columns[e.ColumnIndex].Name == "precioventa")
            {
                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.BackColor = Color.Green;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string TextToSearch = this.textBox1.Text;
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            bs.Filter = $" [codigo] LIKE '%{TextToSearch}%' OR [nombre] LIKE '%{TextToSearch}%'";
            dataGridView1.DataSource = bs;
        }
    }
}
