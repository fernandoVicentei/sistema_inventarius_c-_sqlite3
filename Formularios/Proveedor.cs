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
    public partial class Proveedor : Form
    {
        public Proveedor()
        {
            InitializeComponent();
            this.ComboboxDatagrid();
            this.MOstrarProveedor();
            this.btneditar.Visible = false;
        }
        Conexion.Conectar conexion = new Conexion.Conectar();
        List<string> marc = new List<string>();
        public int idproveedor = 0;
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }
        public void ComboboxDatagrid()
        {
            DataGridViewCheckBoxColumn tuColumna = new DataGridViewCheckBoxColumn();
            this.dataGridView1.Columns.Add(tuColumna);
        }
        public void MOstrarProveedor()
        {
           
            conexion.Sentencias(ref dataGridView1, "select  *  from Proveedor");
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
            string nombre = this.textBox1.Text;
            int telefono= int.Parse(this.textBox2.Text);          
            conexion.SentenciasPuras(String.Format(" insert into Proveedor(NombredeCompañia, Telefono,Marcas) values('{0}', {1},'{2}')", nombre, telefono,this.llenarmarcas()));
            MessageBox.Show(" NUEVO CPROVEEDOR AGREGADO");
            MOstrarProveedor();
        }
        public string llenarmarcas()
        {
            string marca = "";
            foreach (var item in this.comboBox1.Items)
            {
                if (item != " ")
                {
                    marca += item.ToString().ToUpper() + ",";
                }
            }
       
             return marca.Substring(0, marca.Length - 1); ;
        }
        private void comboBox1_Enter(object sender, EventArgs e)
        {            
        }
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {            
            if (e.KeyChar ==(char)Keys.Enter)
            {                
                string text = this.comboBox1.Text;
                marc.Add(text.ToUpper());
                this.comboBox1.Items.Add(text.ToUpper());
                this.comboBox1.Text = " ";
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
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
                this.comboBox1.Items.Clear();
                this.textBox1.Text = this.dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.textBox2.Text = this.dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                string[] ca = this.dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString().Split(',');
                 this.comboBox1.Items.AddRange(ca);
                //this.comboBox1.DataSource = ca;
                this.btneditar.Visible = true;
                idproveedor = int.Parse(this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            catch (Exception)
            {
            }
           
        }
        private void iconButton3_Click(object sender, EventArgs e)
        {
            string nombre = this.textBox1.Text;
            int telefono = int.Parse(this.textBox2.Text);
            conexion.SentenciasPuras(String.Format("update Proveedor set NombredeCompañia='{0}',Telefono={1},Marcas='{2}' where idPrv={3}", nombre, telefono, this.llenarmarcas(),idproveedor));
            MessageBox.Show("PROVEEDOR ACTUALIZADO");
            MOstrarProveedor();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            int row = this.dataGridView1.Rows.Count - 1;
            for (int i = 0; i < row; i++)
            {
                bool isChecked = (bool)dataGridView1[0, i].EditedFormattedValue;
                if (isChecked == true)
                {
                    conexion.SentenciasPuras("delete from Proveedor where idPrv=" + this.dataGridView1.Rows[i].Cells[1].Value.ToString());
                }
            }
            MessageBox.Show("REGISTROS ELIMINADOS");
            this.MOstrarProveedor();
        }

        private void iconButton3_Click_1(object sender, EventArgs e)
        {
            string cad = this.comboBox1.Text;
            this.comboBox1.Items.Remove(cad);
            this.iconButton3.Visible = false;
         /*   object c = this.comboBox1.Items;
            string d = this.comboBox1.Items.ToString();*/

            
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.iconButton3.Visible = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
