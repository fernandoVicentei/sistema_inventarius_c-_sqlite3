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
    public partial class SectionUsuario : Form
    {
        Conexion.Conectar clase = new Conexion.Conectar();
        public int idusuario = -1;
        public SectionUsuario()
        {
            InitializeComponent();
            this.llenarComboboxDatagrid();
            MostrarUsuarios();
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {            
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {            
        }
        public void llenarComboboxDatagrid()
        {
            DataGridViewCheckBoxColumn tuColumna = new DataGridViewCheckBoxColumn();
            this.dataGridView1.Columns.Add(tuColumna);
        }
        public void MostrarUsuarios()
        {
            clase.Sentencias(ref dataGridView1, "select *  from Usuario");
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            string nombre = this.textBox1.Text;
            string contra = this.textBox3.Text;
            clase.SentenciasPuras( String.Format(" insert into Usuario(Usuario, contrasenia) values('{0}', '{1}')",nombre,contra));
            MostrarUsuarios();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                this.dataGridView1.CurrentCell = this.dataGridView1.Rows[e.RowIndex].Cells[0];
                this.dataGridView1.BeginEdit(true);               
            }
            idusuario=int.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            this.textBox1.Text= this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            this.textBox3.Text= this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }
        private void iconButton3_Click(object sender, EventArgs e)
        {
            string nombre = this.textBox1.Text;
            string contra = this.textBox3.Text;
            clase.SentenciasPuras(String.Format("update Usuario set Usuario='{0}', contrasenia='{1}' where id={2}", nombre, contra,idusuario));
            MessageBox.Show("USUARIO ACTUALIZADO");
            MostrarUsuarios();
        }
        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            int row = this.dataGridView1.Rows.Count - 1;
            for (int i = 0; i < row; i++)
            {
                bool isChecked = (bool)dataGridView1[0, i].EditedFormattedValue;
                if (isChecked == true)
                {
                    clase.SentenciasPuras("delete from Usuario where id=" + this.dataGridView1.Rows[i].Cells[1].Value.ToString());
                }
            }
            MessageBox.Show("REGISTROS ELIMINADOS");
            this.MostrarUsuarios();
        }
    }
}
