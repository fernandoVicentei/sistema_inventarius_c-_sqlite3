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
    public partial class EdittarP : Form
    {
        public EdittarP()
        {
            InitializeComponent();
            this.llenardatagrid();
            this.llenarCombobox();
        }
        Conexion.Conectar conectado = new Conexion.Conectar();
        BindingList<Dato> _comboItems;
        public int fila = -1;
        private void EdittarP_Load(object sender, EventArgs e)
        {
        }
        public void llenardatagrid()
        {
            conectado.Sentencias(ref this.dataGridView1, "select * from  Producto");
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string TextToSearch = this.textBox1.Text;
            BindingSource bs = new BindingSource();
            bs.DataSource = dataGridView1.DataSource;
            bs.Filter = $" [codigo] LIKE '%{TextToSearch}%' OR [nombre] LIKE '%{TextToSearch}%'";
            dataGridView1.DataSource = bs;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                fila = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                this.cod.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.nombre.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                this.stock.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                this.compra.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                this.venta.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                this.presentacion.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                this.peso.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                this.medicion.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                this.zona.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                this.categoria.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                this.proveedor.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            }
            catch (Exception)
            {

            }
          
        }
        public void llenarCombobox()
        {
            //medicion
            this.medicion.Items.Add("Litro");
            this.medicion.Items.Add("Mililitro");
            this.medicion.Items.Add("Gramo");
            this.medicion.Items.Add("Kilogramo");
            this.medicion.Items.Add("Metro");
            this.medicion.Items.Add("Centimetro");

            var lector = conectado.SentenciasFiltradas("select * from Proveedor").ExecuteReader();
            _comboItems = new BindingList<Dato>();
            while (lector.Read())
            {
                _comboItems.Add(new Dato { Name = "" + lector["NombredeCompañia"].ToString(), Value = "" + lector["idPrv"].ToString() });
            }
            this.proveedor.DataSource = _comboItems;
            this.proveedor.DisplayMember = "Name";
            this.proveedor.ValueMember = "Value";

            lector = conectado.SentenciasFiltradas("select * from ZonaGuardada").ExecuteReader();
            _comboItems = new BindingList<Dato>();
            while (lector.Read())
            {
                _comboItems.Add(new Dato { Name = "" + lector["zonaubicacion"].ToString(), Value = "" + lector["idZon"].ToString() });
            }
            this.zona.DataSource = _comboItems;
            this.zona.DisplayMember = "Name";
            this.zona.ValueMember = "Value";

            lector = conectado.SentenciasFiltradas("select * from Categoria").ExecuteReader();
            _comboItems = new BindingList<Dato>();
            while (lector.Read())
            {
                _comboItems.Add(new Dato { Name = "" + lector["nombreC"].ToString(), Value = "" + lector["idC"].ToString() });

            }
            this.categoria.DataSource = _comboItems;
            this.categoria.DisplayMember = "Name";
            this.categoria.ValueMember = "Value";

        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            string cod = this.cod.Text.ToString();
            string nomb = this.nombre.Text.ToString();
            int stock = this.stock.Text.ToString() == "" ? 0 : int.Parse(this.stock.Text.ToString());
            double compra = this.compra.Text.ToString() == "" ? 0 : double.Parse(this.compra.Text.ToString());
            double venta = this.venta.Text.ToString() == "" ? 0 : double.Parse(this.venta.Text.ToString());
            string presnet = this.presentacion.Text.ToString();
            string medi = this.medicion.Text.ToString();
            double peso = this.peso.Text.ToString() == "" ? 0 : double.Parse(this.peso.Text.ToString());
            int cate = int.Parse(this.categoria.SelectedValue.ToString());
            int zona = int.Parse(this.zona.SelectedValue.ToString());
            int provee = int.Parse(this.proveedor.SelectedValue.ToString());
            string sentencia= String.Format("update Producto SET codigo='{0}',nombre='{1}',stock={2},preciocompra={3},precioventa={4},presentacion='{5}',sistemamedicion='{6}',peso={7}," +
                "idCategoria={8},idZona={9},idProveedor={10},idUsuario={11},fechaRegistro='{12}' where idPro = {13} ", cod, nomb, stock, compra.ToString().Replace(',', '.'), venta.ToString().Replace(',', '.'), 
                                                                                        presnet, medi, peso.ToString().Replace(',', '.'), cate, zona, provee, 1, DateTime.Now,fila);
            conectado.SentenciasPuras(sentencia);
            MessageBox.Show("REGISTRO ACTUALIZACO");
            this.llenardatagrid();
        }
        private class Dato
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "stock")
            {
                e.CellStyle.ForeColor = Color.Black;
                e.CellStyle.BackColor = Color.Yellow;
            }
            else if (this.dataGridView1.Columns[e.ColumnIndex].Name == "preciocompra")
            {
                e.CellStyle.ForeColor = Color.Yellow;
                e.CellStyle.BackColor = Color.DarkRed;
            }
            else if (this.dataGridView1.Columns[e.ColumnIndex].Name == "precioventa")
            {
                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.BackColor = Color.Green;
            }
        }
    }
}
