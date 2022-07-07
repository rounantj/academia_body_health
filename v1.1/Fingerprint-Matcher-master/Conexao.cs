using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Odbc;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Fingerprint_Matcher
{
    class Conexao
    {

        String con = "SERVER=localhost; DATABASE=fingers; UID=root";
        MySqlConnection conexao = null;
        MySqlCommand comando;
        public Boolean status = false;

        public void verifica(string action)
        {

            string retorna = queryString(action);
            if(retorna != "erro")
            {
                status = true;
            }else
            {
                status = false;
            }

          
                    
                  
               
            
        }
        public int dataDiff(string data1, string data2)
        {

          
            DateTime nova = Convert.ToDateTime(data1);
            DateTime nova2 = Convert.ToDateTime(data2);
         
            int ano = nova.Year;
            int mes = nova.Month;
            int dia = nova.Day;
            DateTime dt2 = new DateTime(ano, mes, dia);
            TimeSpan ts1 = nova2.Subtract(dt2);
            int total =ts1.Days;
            return total;
        }
        

        public DataTable ExibirDados(String query)
        {
            Console.WriteLine(query);
            try
            {
            
                conexao = new MySqlConnection(con);
                comando = new MySqlCommand(query, conexao);
                MySqlDataAdapter Da = new MySqlDataAdapter();
                Da.SelectCommand = comando;
                DataTable Dt = new DataTable();
                Da.Fill(Dt);
                return Dt;
            }
            catch (Exception erro)
            {
                throw erro;
            }

        }

        public int ClienteEmDia(int num_identificacao)
        {
            string query = "select *, date_format(now(), '%Y-%m-%d') from clientes where num_identificador = '" + num_identificacao + "' and dataVencimento >= date_format(now(), '%Y-%m-%d')";

            Console.WriteLine(query);
            try
            {

                conexao = new MySqlConnection(con);
                comando = new MySqlCommand(query, conexao);
                MySqlDataAdapter Da = new MySqlDataAdapter();
                Da.SelectCommand = comando;
                DataTable Dt = new DataTable();
                Da.Fill(Dt);
                return Dt.Rows.Count; ;
            }
            catch (Exception erro)
            {
                throw erro;
            }

        }
        public int ClienteExiste(string nome)
        {
            string query = "select *, date_format(now(), '%Y-%m-%d') from clientes where nome = '" + nome + "' ";

            Console.WriteLine(query);
            try
            {

                conexao = new MySqlConnection(con);
                comando = new MySqlCommand(query, conexao);
                MySqlDataAdapter Da = new MySqlDataAdapter();
                Da.SelectCommand = comando;
                DataTable Dt = new DataTable();
                Da.Fill(Dt);
                return Dt.Rows.Count; ;
            }
            catch (Exception erro)
            {
                throw erro;
            }

        }

        public int diasParaVencer(int num_identificacao)
        {

            int diasParaVencer = dataDiff(DateTime.Now.ToString(), queryString("select dataVencimento from clientes where num_identificador = '" + num_identificacao + "'"));
            return diasParaVencer;

        }


        public Bitmap queryBlobGet(String query)
        {
            Console.WriteLine(query);
            try
            {

                conexao = new MySqlConnection(con);
                conexao.Open();
                comando = new MySqlCommand(query, conexao);
                MySqlDataAdapter Da = new MySqlDataAdapter();
                Da.SelectCommand = comando;
                MySqlDataReader reader = comando.ExecuteReader();
                reader.Read();

                byte[] foto = (byte[])reader["img"];
                Bitmap bmp;
                using (var ms = new MemoryStream(foto))
                {
                    bmp = new Bitmap(ms);
                }
                return bmp;

            }
            catch (Exception erro)
            {
                throw erro;
            }

        }




        public void queryVoid(String query)
        {
            Console.WriteLine(query);
            try
            {
                conexao = new MySqlConnection(con);
                conexao.Open();
                comando = new MySqlCommand(query, conexao);             
                comando.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                //throw erro;
            }
            finally
            {
                conexao.Close();
            }

        }
       

        public void queryVoidBlob(String query, Byte[] image)
        {
            Console.WriteLine(query);
            try
            {
                conexao = new MySqlConnection(con);
                conexao.Open();
                comando = new MySqlCommand(query, conexao);
                comando.Parameters.Add("@img", MySqlDbType.VarBinary);
              //  comando.Parameters["img"].SourceVersion = DataRowVersion.Current;
                comando.Parameters["@img"].Value = image;
                comando.ExecuteNonQuery();
            }
            catch (Exception erro)
            {
                Console.WriteLine(erro);
            }
            finally
            {
                conexao.Close();
            }

        }



        public string queryString(String query)
        {
            Console.WriteLine(query);
             string retorna;
            try
            {
                conexao = new MySqlConnection(con);
                conexao.Open();
                comando = new MySqlCommand(query, conexao);
                //comando.ExecuteNonQuery();

               retorna =  comando.ExecuteScalar().ToString();
            }
            catch (Exception erro)
            {
                //throw erro;
                retorna =  "erro";
            }
            finally
            {
                conexao.Close();
                
            }
            return retorna;

        }
    }
}





