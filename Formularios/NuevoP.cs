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
    public partial class NuevoP : Form
    {
        Conexion.Conectar conectado = new Conexion.Conectar();
        BindingList<Dato> _comboItems;
        public int idU = -1;
        public NuevoP(int id)
        {
            InitializeComponent();
            this.idU = id;
            retornar();
            retornarCatego();
        }
        private void NuevoP_Load(object sender, EventArgs e)
        {
        }
        public void retornar()
        {
            var lector = conectado.SentenciasFiltradas("select * from Proveedor").ExecuteReader();
            _comboItems = new BindingList<Dato>();
            while (lector.Read())
            {                
                _comboItems.Add(new Dato { Name = "" + lector["NombredeCompañia"].ToString(), Value = "" + lector["idPrv"].ToString() });
            }
            this.comboProveedor.DataSource = _comboItems;
            this.comboProveedor.DisplayMember = "Name";
            this.comboProveedor.ValueMember = "Value";

            lector = conectado.SentenciasFiltradas("select * from ZonaGuardada").ExecuteReader();
            _comboItems = new BindingList<Dato>();
            while (lector.Read())
            {                
                _comboItems.Add(new Dato { Name = "" + lector["zonaubicacion"].ToString(), Value = "" + lector["idZon"].ToString() });
            }
            this.combozona.DataSource = _comboItems;
            this.combozona.DisplayMember = "Name";
            this.combozona.ValueMember = "Value";
        }
        public void retornarCatego()
        {
            var lector = conectado.SentenciasFiltradas("select * from Categoria").ExecuteReader();
            _comboItems = new BindingList<Dato>();
            while (lector.Read())
            {               
                _comboItems.Add(new Dato { Name = ""+lector["nombreC"].ToString(), Value = ""+lector["idC"].ToString() });
      
            }
            //this.comoCategoria.Items.Add(lector["nombreC"].ToString());
            this.comoCategoria.DataSource = _comboItems;
            this.comoCategoria.DisplayMember = "Name";
            this.comoCategoria.ValueMember = "Value";

            //comboboxmedicion
            this.comboMedicion.Items.Add("Litro");
            this.comboMedicion.Items.Add("Mililitro");
            this.comboMedicion.Items.Add("Gramo");
            this.comboMedicion.Items.Add("Kilogramo");
            this.comboMedicion.Items.Add("Metro");
            this.comboMedicion.Items.Add("Centimetro");
            /*this.comboMedicion.Items.Add();
            this.comboMedicion.Items.Add();
            this.comboMedicion.Items.Add();
            this.comboMedicion.Items.Add();
            this.comboMedicion.Items.Add();
            this.comboMedicion.Items.Add();*/
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {            

        }
        private class Dato
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {           

            string cod = this.textcod.Text.ToString();
            string nomb = this.textnombre.Text.ToString();
            int stock = this.textstock.Text.ToString() == "" ? 0 : int.Parse(this.textstock.Text.ToString());
            double compra = this.textprecio.Text.ToString() == "" ? 0 : double.Parse(this.textprecio.Text.ToString());
            double venta = this.textventa.Text.ToString() == "" ? 0 : double.Parse(this.textventa.Text.ToString());
            string presnet = this.textpresent.Text.ToString();
            string medi = this.comboMedicion.SelectedItem.ToString();
            double peso = this.textpeso.Text.ToString() == "" ? 0 : double.Parse(this.textpeso.Text.ToString());
            int cate =int.Parse( this.comoCategoria.SelectedValue.ToString() );
            int zona =int.Parse( this.combozona.SelectedValue.ToString() );
            int provee = int.Parse(this.comboProveedor.SelectedValue.ToString() );
            string sentencia =String.Format( "insert into Producto (codigo,nombre,stock,preciocompra,precioventa,presentacion,sistemamedicion,peso,idCategoria,idZona,idProveedor,idUsuario,fechaRegistro) values " +
                "('{0}','{1}',{2},{3},{4},'{5}','{6}',{7},{8},{9},{10},{11},'{12}')",cod,nomb,stock,compra.ToString().Replace(',','.'),venta.ToString().Replace(',','.'),presnet,medi,peso.ToString().Replace(',','.'),cate,zona,provee,this.idU,DateTime.Now);
            conectado.SentenciasPuras(sentencia);
            MessageBox.Show(" PRODUCTO NUEVO");

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            Reportes cargar = new Reportes();
            cargar.ShowDialog();         
        }
    }
    
    
}
