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
    public partial class Categorias : Form
    {
        public Categorias()
        {
            InitializeComponent();
            this.llenarcheckvbox();
            MostrarCategorias();
            this.btneliminar.Visible = false;
        }
        Conexion.Conectar conectar = new Conexion.Conectar();
        public int idregistro = 0;
        private void iconButton1_Click(object sender, EventArgs e)
        {
            string nombre = this.textBox1.Text;
            string desc = this.textBox2.Text;
            conectar.SentenciasPuras(String.Format("insert into Categoria (nombreC,descripcion,fecha) values ('{0}','{1}','{2}')",nombre,desc,DateTime.Now));
            MostrarCategorias();
        }
        private void Categorias_Load(object sender, EventArgs e)
        {
        }
        public void llenarcheckvbox()
        {
            DataGridViewCheckBoxColumn tuColumna = new DataGridViewCheckBoxColumn();
            this.dataGridView1.Columns.Add(tuColumna);
        }
        public void MostrarCategorias()
        {            
            conectar.Sentencias(ref dataGridView1, "select *  from Categoria");
            //this.dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.Rows.]);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[0];
                this.dataGridView1.BeginEdit(true);               
                idregistro = -1;
                this.btneliminar.Visible = true;
            }
            else
            {
                idregistro=int.Parse( this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                this.btneliminar.Visible = false;
            }
              
            this.textBox1.Text = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            this.textBox2.Text = this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void btneliminar_Click(object sender, EventArgs e)
        {
            int row = this.dataGridView1.Rows.Count - 1;            
            for (int i = 0; i < row; i++)
            {
                bool isChecked = (bool)dataGridView1[0, i].EditedFormattedValue;
                if (isChecked == true)
                {
                    conectar.SentenciasPuras("delete from Categoria where idC="+this.dataGridView1.Rows[i].Cells[1].Value.ToString());
                }
            }
            this.MostrarCategorias();
        }
        private void btneditar_Click(object sender, EventArgs e)
        {
            string nombre = this.textBox1.Text.ToString();
            string descripcion = this.textBox2.Text.ToString();
            string sentencia = String .Format("update Categoria set nombreC='{0}', descripcion='{1}',fecha='{2}' where idC={3} ",nombre,descripcion,DateTime.Now,idregistro);
            conectar.SentenciasPuras(sentencia);
            MessageBox.Show("DATO ACTUALIZADO");           
            this.MostrarCategorias();

        }
    }
}
