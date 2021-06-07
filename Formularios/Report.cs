using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.IO.Image;
using System.Windows.Forms.DataVisualization.Charting;

namespace SISTEMA.Formularios
{
    public partial class Report : Form
    {
        public Report(string us)
        {
            InitializeComponent();
            this.llenarDatos();
            this.llenarChartCircle();
            this.usua = us;
        }
        public string usua = "";
        Conexion.Conectar conectado = new Conexion.Conectar();
        BindingList<Dato> _comboItems;
        private void chart1_Click(object sender, EventArgs e)
        {
        }
        public void llenarDatos()
        {
            var lector = conectado.SentenciasFiltradas("select * from Proveedor").ExecuteReader();
            _comboItems = new BindingList<Dato>();
            _comboItems.Add(new Dato { Name = "TODOS", Value = "-1" });
            while (lector.Read())
            {
                _comboItems.Add(new Dato { Name = "" + lector["NombredeCompañia"].ToString(), Value = "" + lector["idPrv"].ToString() });
            }            
            this.proveedor.DataSource = _comboItems;
            this.proveedor.DisplayMember = "Name";
            this.proveedor.ValueMember = "Value";
            
            lector = conectado.SentenciasFiltradas("select * from Categoria").ExecuteReader();
            _comboItems = new BindingList<Dato>();
            _comboItems.Add(new Dato { Name = "TODOS", Value = "-1" });
            while (lector.Read())
            {
                _comboItems.Add(new Dato { Name = "" + lector["nombreC"].ToString(), Value = "" + lector["idC"].ToString() });
                this.checkedListBox1.Items.Add( lector["nombreC"].ToString());
            }           
            this.categoria.DataSource = _comboItems;
            this.categoria.DisplayMember = "Name";
            this.categoria.ValueMember = "Value";

           
            
            lector = conectado.SentenciasFiltradas("select * from Usuario").ExecuteReader();
            _comboItems = new BindingList<Dato>();
            _comboItems.Add(new Dato { Name = "TODOS", Value = "-1" });
            while (lector.Read())
            {
                _comboItems.Add(new Dato { Name = "" + lector["Usuario"].ToString(), Value = "" + lector["id"].ToString() });
            }          
            this.usuario.DataSource = _comboItems;
            this.usuario.DisplayMember = "Name";
            this.usuario.ValueMember = "Value";
        }
        private class Dato
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string consulta = "select Pr.codigo,Pr.nombre,Pr.stock,Pr.preciocompra,Pr.precioventa,Pr.presentacion,Pr.sistemamedicion,Pr.peso,C.nombreC,Prov.NombredeCompañia,U.Usuario from Producto as Pr inner join Categoria as C on"+
             "                 Pr.idCategoria = C.idC inner join Proveedor AS Prov ON Pr.idProveedor = Prov.idPrv inner join Usuario as U on Pr.idUsuario = U.id";
            if (this.proveedor.Text!="TODOS")
            {
                consulta += " where Prov.idPrv="+int.Parse(this.proveedor.SelectedValue.ToString());
            }
            else
            {
                consulta += " where Prov.idPrv>-1";
            }
            if (this.categoria.Text!="TODOS")
            {
                consulta += " and C.idC=" + int.Parse(this.categoria.SelectedValue.ToString());
            }
            if (this.usuario.Text != "TODOS")
            {
                consulta += " and U.id=" + int.Parse(this.usuario.SelectedValue.ToString());
            }
            conectado.Sentencias(ref this.dataGridView1,consulta);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            PdfWriter pdf = new PdfWriter("Reporte.pdf");
            PdfDocument contenido = new PdfDocument(pdf);                       
            Document documento = new Document(contenido,PageSize.LETTER);
            documento.SetMargins(30,10,30,10);
            var parrafo = new Paragraph("INTARIUS MODUS");
            PdfFont colum =  PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            PdfFont fila = PdfFontFactory.CreateFont(StandardFonts.COURIER);

            var logo = new iText.Layout.Element.Image(ImageDataFactory.Create("../../ajuste.jpg")).SetWidth(70);
            documento.Add(new Paragraph("").Add(logo).SetTextAlignment(TextAlignment.LEFT));

            documento.Add(new Paragraph(" MODUS INVENTARIUS ").SetFontSize(24).SetFont(colum).SetFontColor(ColorConstants.BLACK)
                                       .SetTextAlignment(TextAlignment.CENTER));
            documento.Add(new Paragraph("Publicado por : "+usua).SetFontSize(12).SetFont(fila).SetTextAlignment(TextAlignment.LEFT).SetMarginTop(15));
            documento.Add(new Paragraph("Fecha de Publicacion : "+DateTime.Now.ToShortDateString()+" "+DateTime.Now.ToLongTimeString()).SetFontSize(12).SetFont(fila).SetTextAlignment(TextAlignment.LEFT).SetMarginBottom(15));


            string[] col = { "", "NOMBRE", "STOCK", "PRECIO COMPRA", "PRECIO VENTA", "PESO", "CATEGORIA", "PROVEEDOR", "USUARIO" };
            float[] tama = { 2, 3, 2, 3, 3,  4, 4, 4, 4 };
            Table tabla = new Table(UnitValue.CreatePercentArray(tama));
            tabla.SetWidth(UnitValue.CreatePercentValue(100));
            foreach (var item in col)
            {
                tabla.AddHeaderCell(new Cell().Add(new Paragraph(item).SetFont(colum)));
            }
            foreach (DataGridViewRow fi in  this.dataGridView1.Rows)
            {
                //Accede a la columna que quieras, o las recorres todas con otro foreach...
                if (fi.Cells[0].Value?.ToString() != null)
                {
                    tabla.AddCell(new Cell().Add(new Paragraph(fi.Cells[0].Value?.ToString()).SetFont(fila)));
                    tabla.AddCell(new Cell().Add(new Paragraph(fi.Cells[1].Value?.ToString()).SetFont(fila)));
                    tabla.AddCell(new Cell().Add(new Paragraph(fi.Cells[2].Value?.ToString()).SetFont(fila)));
                    tabla.AddCell(new Cell().Add(new Paragraph(fi.Cells[3].Value?.ToString()).SetFont(fila)));
                    tabla.AddCell(new Cell().Add(new Paragraph(fi.Cells[4].Value?.ToString()).SetFont(fila)));                    
                    tabla.AddCell(new Cell().Add(new Paragraph(fi.Cells[7].Value?.ToString()+" "+fi.Cells[6].Value?.ToString()).SetFont(fila)));
                    tabla.AddCell(new Cell().Add(new Paragraph(fi.Cells[8].Value?.ToString()).SetFont(fila)));
                    tabla.AddCell(new Cell().Add(new Paragraph(fi.Cells[9].Value?.ToString()).SetFont(fila)));
                    tabla.AddCell(new Cell().Add(new Paragraph(fi.Cells[10].Value?.ToString()).SetFont(fila)));
                }                               
            }
            documento.Add(tabla);
            documento.Close();
            MessageBox.Show("PDF GUARDADO");
        }


        public void llenarChartCircle()
        {
            string sentencia = "select Usuario.Usuario,count(*) as cantidad from Producto,Usuario where Usuario.id=Producto.idUsuario GROUP by (Producto.idUsuario)";
            var y = conectado.SentenciasFiltradas(sentencia).ExecuteReader();
            chart1.Titles.Add("USUARIOS");
            while (y.Read())
            {
                chart1.Series["USUARIOS"].Points.AddXY(""+y["Usuario"].ToString(),double.Parse(y["cantidad"].ToString()));
            }
            sentencia = "select Categoria.nombreC,count(*) as cantidad from Producto,Categoria where Categoria.idC=Producto.idCategoria GROUP by (Producto.idCategoria)";
            y = conectado.SentenciasFiltradas(sentencia).ExecuteReader();
            chart2.Titles.Add("CATEGORIAS");
           
            while (y.Read())
            {
                Series serie = chart2.Series.Add(y["nombreC"].ToString());
                serie.Label = "" + y["cantidad"].ToString();                
                serie.Points.Add(int.Parse(y["cantidad"].ToString()));       
                serie.LabelForeColor= System.Drawing.Color.White;
            }          
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.dataGridView1.Columns[e.ColumnIndex].Name == "stock")
            {
                e.CellStyle.ForeColor = System.Drawing.Color.Black;
                e.CellStyle.BackColor = System.Drawing.Color.Yellow;
            }
            else if (this.dataGridView1.Columns[e.ColumnIndex].Name == "preciocompra")
            {
                e.CellStyle.ForeColor = System.Drawing.Color.Yellow;
                e.CellStyle.BackColor = System.Drawing.Color.DarkRed;
            }
            else if (this.dataGridView1.Columns[e.ColumnIndex].Name == "precioventa")
            {
                e.CellStyle.ForeColor = System.Drawing.Color.White;
                e.CellStyle.BackColor = System.Drawing.Color.Green;
            }
        }
    }
}
