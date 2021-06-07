using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SISTEMA.Conexion;
using SpreadsheetLight;

namespace SISTEMA.Formularios
{
    public partial class Reportes : Form
    {
        public Reportes()
        {
            InitializeComponent();
            this.datosBDParaCompararDependencias();
        }
        Conexion.Conectar con = new Conexion.Conectar();
        private List<Prueba> data = new List<Prueba>();

        private List<Categoria> catego = new List<Categoria>();
        private List<Zona> zona = new List<Zona>();
        private List<Provee> proveedor = new List<Provee>();
        private List<Usuario> user = new List<Usuario>();

        private void button1_Click(object sender, EventArgs e)
        {          

        }
        private void Conexionar_Load(object sender, EventArgs e)
        {

        }        
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
          
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }
        public void datosBDParaCompararDependencias()
        {
            var categorias = con.SentenciasFiltradas("select idC,nombreC from Categoria").ExecuteReader();
            while (categorias.Read())
            {
                catego.Add(new Categoria { idC=int.Parse(categorias["idC"].ToString()),nombreaC=categorias["nombreC"].ToString() });
            }
            var zonA = con.SentenciasFiltradas("select * from ZonaGuardada").ExecuteReader();
            while (zonA.Read())
            {
                zona.Add(new Zona { idZ = int.Parse(zonA["idZon"].ToString()), nombreZ = zonA["zonaubicacion"].ToString() });
            }
            var prove= con.SentenciasFiltradas("select idPrv,NombredeCompañia from Proveedor").ExecuteReader();
            while (prove.Read())
            {
                proveedor.Add(new Provee { idProve = int.Parse(prove["idPrv"].ToString()), nombreProve = prove["NombredeCompañia"].ToString() });
            }
            var us = con.SentenciasFiltradas("select id,Usuario from Usuario").ExecuteReader();
            while (us.Read())
            {
                user.Add(new Usuario { id = int.Parse(us["id"].ToString()), nombreU = us["Usuario"].ToString() });
            }
        }
        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            string cadenaCOnsulta = "insert into Producto (codigo,nombre,stock,preciocompra,precioventa,presentacion,sistemamedicion,peso,idCategoria,idZona,idProveedor,idUsuario,fechaRegistro) values ";

            foreach ( var item in data)
            {
                cadenaCOnsulta += String.Format("('{0}','{1}',{2},{3},{4},'{5}','{6}',{7},{8},{9},{10},{11},'{12}'),",item.id,item.ci,item.cadena1,item.cadena2.ToString().Replace(',','.'),
                                              item.cadena3.ToString().Replace(',','.'),item.cadena4,item.cadena5,item.cadena6.ToString().Replace(',','.'),returnIdCategoria(item.cadena7),returnIdZona(item.cadena8),returnIdProveedo(item.cadena9), returnIdUsuario(item.cadena10) ,item.cadena11);
            }
            con.SentenciasPuras(cadenaCOnsulta.Substring(0, cadenaCOnsulta.Length - 1));
            MessageBox.Show("DATOS AGREGADOS CORRECTAMENTE");
        }
        public int returnIdCategoria(string dato)
        {
            int dato1 = -1;
            foreach (var item in catego)
            {
                if (item.nombreaC.ToUpper()==dato.ToUpper())
                {
                    dato1= item.idC;
                }
            }
            return dato1;
        }
        public int returnIdZona(string dato)
        {
            int dato1 = -1;
            foreach (var item in zona)
            {
                if (item.nombreZ.ToUpper() == dato.ToUpper())
                {
                    dato1 = item.idZ;
                }
            }
            return dato1;
        }
        public int returnIdProveedo(string dato)
        {
            int dato1 = -1;
            foreach (var item in proveedor)
            {
                if (item.nombreProve.ToUpper() == dato.ToUpper())
                {
                    dato1 = item.idProve;
                }
            }
            return dato1;
        }
        public int returnIdUsuario(string dato)
        {
            int dato1 = -1;
            foreach (var item in user)
            {
                if (item.nombreU.ToUpper() == dato.ToUpper())
                {
                    dato1 = item.id;
                }
            }
            return dato1;
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            string name = "";
            OpenFileDialog cargar = new OpenFileDialog
            {
                Filter = "Excel | *.xls;*.xlsx",
                Title = "Seleccionar Archivo Excel"
            };
            if (cargar.ShowDialog() == DialogResult.OK)
            {
                name = cargar.FileName;
            }
            SLDocument datos = new SLDocument(name);


            int contador = 2;
            while (!string.IsNullOrEmpty(datos.GetCellValueAsString(contador, 1)))
            {
                Prueba val = new Prueba();
                val.id = datos.GetCellValueAsString(contador, 1);
                val.ci = datos.GetCellValueAsString(contador, 2);
                val.cadena1 = datos.GetCellValueAsString(contador, 3);
                val.cadena2 = datos.GetCellValueAsString(contador, 4);
                val.cadena3 = datos.GetCellValueAsString(contador, 5);
                val.cadena4 = datos.GetCellValueAsString(contador, 6);
                val.cadena5 = datos.GetCellValueAsString(contador, 7);
                val.cadena6 = datos.GetCellValueAsString(contador, 8);
                val.cadena7 = datos.GetCellValueAsString(contador, 9);
                val.cadena8 = datos.GetCellValueAsString(contador, 10);
                val.cadena9 = datos.GetCellValueAsString(contador, 11);
                val.cadena10 = datos.GetCellValueAsString(contador, 12);
                val.cadena11 = datos.GetCellValueAsDateTime(contador, 13).ToString();
                data.Add(val);
                contador++;
            }
            this.dataGridView1.DataSource = data;
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {

        }
    }
    class Prueba
    {
        public string id { get; set; }
        public string ci { get; set; }
        public string cadena1 { get; set; }
        public string cadena2 { get; set; }
        public string cadena3 { get; set; }
        public string cadena4 { get; set; }
        public string cadena5 { get; set; }
        public string cadena6 { get; set; }
        public string cadena7 { get; set; }
        public string cadena8 { get; set; }
        public string cadena9 { get; set; }
        public string cadena10 { get; set; }
        public string cadena11 { get; set; }        
    }

    class Categoria
    {
        public int idC { get; set; }
        public string nombreaC { get; set; }
    }

    class Zona
    {
        public int idZ { get; set; }
        public string nombreZ { get; set; }
    }

    class Provee
    {
        public int idProve { get; set; }
        public string nombreProve { get; set; }
    }
    class Usuario
    {
        public int id { get; set; }
        public string nombreU { get; set; }
    }

}
