using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Data;
using System.IO;

namespace SISTEMA.Conexion
{
    class Conectar
    {
        private  string DBName = Path.Combine(Application.StartupPath, "Archivos\\prueba.db"); 
        private static bool IsDbRecentlyCreated = false;

        SQLiteConnection con;
        public SQLiteConnection ConectionBD()
        {
            //string ru= Path.Combine(Environment.CurrentDirectory, "Archivos\\prueba.db");
            if (File.Exists(Path.GetFullPath(DBName)))
            {
                con = new SQLiteConnection(String.Format("Data Source={0};Version=3;", DBName));
                con.Open();
            }
            
            return con;
        }
        public void CrearDDBB()
        {
            //DBName= Path.Combine(Application.StartupPath, "Archivos\\prueba.db");
            int d = DBName.LastIndexOf("bin");

            DBName=d>0?DBName.Substring(0,d)+"Archivos\\prueba.db":DBName;

            if (!File.Exists(Path.GetFullPath(DBName)))
            {
                SQLiteConnection.CreateFile(DBName);
                IsDbRecentlyCreated = true;
            }
            using (SQLiteConnection ctx =ConectionBD())
            {
                if (IsDbRecentlyCreated)
                {
                    string sql = "CREATE TABLE Usuario  ('id' INTEGER PRIMARY KEY AUTOINCREMENT,'Usuario'   TEXT,'contrasenia'   VARCHAR(10))";

                    SQLiteCommand command = new SQLiteCommand(sql, ctx);
                    command.ExecuteNonQuery();
                    sql = "insert into Usuario (Usuario,contrasenia) values ('Admin','123456')";
                    command = new SQLiteCommand(sql, ctx);
                    command.ExecuteNonQuery();
                    
                }
                ctx.Close();
            }
        }
        public void Sentencias(ref DataGridView d, string sentencia)
        {
            CrearDDBB();
            var cn = ConectionBD();
            DataTable ddD = new DataTable();
            SQLiteDataAdapter lector = new SQLiteDataAdapter(sentencia, cn);
            lector.Fill(ddD);
            d.DataSource = ddD;
            cn.Close();
        }
        public void SentenciasPuras(string sentencia)
        {
            CrearDDBB();
            var cn = ConectionBD();
            SQLiteCommand lector = new SQLiteCommand(sentencia, cn);
            lector.ExecuteNonQuery();
            cn.Close();
        }
        public SQLiteCommand SentenciasFiltradas(string sentencia)
        {
            CrearDDBB();
            var cn = ConectionBD();
            SQLiteCommand com = new SQLiteCommand(sentencia, cn);              
            return com;
        }
    }
}
