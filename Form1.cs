using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FontAwesome.Sharp;
using SISTEMA.Formularios;

using System.Data.SQLite;

namespace SISTEMA
{
    public partial class Form1 : Form
    {
        private IconButton currentbtn;
        private Panel leftborderbtn;
        private Form childrenform;
        public int idU = -1;
        public string user = "";
        public Form1(int id,string us)
        {
            InitializeComponent();
            leftborderbtn = new Panel();
            leftborderbtn.Size=new Size(7,54);
            menu.Controls.Add(leftborderbtn);
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            childrenform = new Form();
            this.idU = id;
            this.user = us;
            this.iconButton2.Text = us.ToUpper();
            //FormBorderStyle = FormBorderStyle.None;
            TopMost = false;
            WindowState = FormWindowState.Maximized;
        }

        public struct Colores
        {

            public static Color colo1 = Color.FromArgb(172,126,241);
            public static Color colo2 = Color.FromArgb(248,118,176);
            public static Color colo3 = Color.FromArgb(  253,138,114);
            public static Color colo4 = Color.FromArgb(95,77,211);
            public static Color colo5 = Color.FromArgb( 249,88,145);
            public static Color colo6 = Color.FromArgb( 24,161,251);

        }

        private void ActiveButton(object senderBtn,Color color,Image icono)
        {
            if (senderBtn!=null)
            {

                DisableBtn();

                currentbtn = (IconButton)senderBtn;
                currentbtn.BackColor = Color.FromArgb(37,36,81);
                currentbtn.ForeColor = color;
                currentbtn.TextAlign = ContentAlignment.MiddleCenter;
                currentbtn.IconColor = color;
                currentbtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentbtn.ImageAlign = ContentAlignment.MiddleRight;

                this.iconPictureBox1.Image = icono;

                leftborderbtn.BackColor = color;
                leftborderbtn.Location = new Point(0,currentbtn.Location.Y);
                leftborderbtn.Visible = true;
                leftborderbtn.BringToFront();
            }
        }
        
        private void DisableBtn()
        {
            if (currentbtn != null)
            {
                currentbtn.BackColor = Color.Transparent;
                currentbtn.ForeColor = Color.White;
                currentbtn.TextAlign = ContentAlignment.MiddleCenter;
                currentbtn.IconColor = Color.White;
                currentbtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentbtn.ImageAlign = ContentAlignment.MiddleLeft;
                this.iconPictureBox1.Image = null;
                panelventana.Controls.Remove(childrenform);
                currentbtn.Refresh();
            }           
        }

        public void ChildrenForm(Form children)
        {
            if (childrenform!=null)
            {
                //cerramos antes  el formulario
                childrenform.Close();
                childrenform = children;
                children.TopLevel = false;
                children.FormBorderStyle = FormBorderStyle.None;
                children.Dock = DockStyle.Fill;
                panelventana.Controls.Add(children);
                panelventana.Tag = children;
                children.BringToFront();
                children.Show();
            }
        }
        private void iconButton2_Click(object sender, EventArgs e)
        {
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            IconButton f = (IconButton)sender;
            ActiveButton(sender, Colores.colo1,f.Image);
            ChildrenForm(new SectionUsuario());
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            IconButton f = (IconButton)sender;
            ActiveButton(sender, Colores.colo2, f.Image);
            //ChildrenForm(new Formula2());
            this.panel3.Visible = true;
            this.label1.Text = "PRODUCTOS";
        }

        private void iconButton2_Click_1(object sender, EventArgs e)
        {
            IconButton f = (IconButton)sender;
            ActiveButton(sender, Colores.colo3, f.Image);
            ChildrenForm(new Categorias());
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            IconButton f = (IconButton)sender;
            ActiveButton(sender, Colores.colo4, f.Image);
            ChildrenForm(new Proveedor());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            IconButton f = (IconButton)sender;
            ActiveButton(sender, Colores.colo5, f.Image);
            ChildrenForm(new EdittarP());
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {            

        }
        private void Reset()
        {
            DisableBtn();
            leftborderbtn.Visible = false;
        }
        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle,0x112,0xf012,0);
        }
        private void panelventana_Paint(object sender, PaintEventArgs e)
        {

        }
        private void iconButton5_Click(object sender, EventArgs e)
        {
           
        }

        private void iconButton10_Click(object sender, EventArgs e)
        {
            IconButton f = (IconButton)sender;
            ActiveButton(sender, Colores.colo5, f.Image);
            ChildrenForm(new Reportes());
            this.panel3.Visible =  false;
        }

        private void iconButton11_Click(object sender, EventArgs e)
        {
            IconButton f = (IconButton)sender;
            ActiveButton(sender, Colores.colo3, f.Image);
            ChildrenForm(new Categorias());
            this.panel3.Visible = false;
            this.label1.Text = "CATEGORIAS";
        }

        private void iconButton12_Click(object sender, EventArgs e)
        {
            IconButton f = (IconButton)sender;
            ActiveButton(sender, Colores.colo4, f.Image);
            ChildrenForm(new Proveedor());
            this.panel3.Visible = false;
            this.label1.Text = "PROVEEDORES";
        }

        private void iconButton13_Click(object sender, EventArgs e)
        {
            IconButton f = (IconButton)sender;
            ActiveButton(sender, Colores.colo1, f.Image);
            ChildrenForm(new SectionUsuario());
            this.panel3.Visible =  false;
            this.label1.Text = "USUARIOS";
        }

        private void iconButton14_Click(object sender, EventArgs e)
        {
            IconButton f = (IconButton)sender;
            ActiveButton(sender, Colores.colo4, f.Image);
            ChildrenForm(new Report(user));
            this.panel3.Visible =  false;
            this.label1.Text = "REPORTES";
        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            IconButton f = (IconButton)sender;
            ActiveButton(this.iconButton6, Colores.colo5, f.Image);
            ChildrenForm(new NuevoP(this.idU));
            this.panel3.Visible = false;
            this.label1.Text = "NUEVO PRODUCTOS";
        }

        private void iconButton8_Click(object sender, EventArgs e)
        {
            IconButton f = (IconButton)sender;
            ActiveButton(this.iconButton6, Colores.colo5, f.Image);
            ChildrenForm(new EdittarP());
            this.panel3.Visible =  false;
            this.label1.Text = "EDITAR PRODUCTOS";
        }

        private void iconButton9_Click(object sender, EventArgs e)
        {
            IconButton f = (IconButton)sender;
            ActiveButton(this.iconButton6, Colores.colo4, f.Image);
            ChildrenForm(new EliminarP());
            this.panel3.Visible =  false;
            this.label1.Text = "ELIMINAR PRODUCTOS";
        }

        private void iconButton1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.label1.Text = "";
            DisableBtn();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label4.Text = DateTime.Now.ToLongTimeString();
        }
    }
}
