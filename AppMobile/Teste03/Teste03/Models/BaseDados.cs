using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Xamarin.Forms;
using Teste03.Models;

namespace Teste03.Models
{
    public static class BaseDados
    {
        //private static readonly string conexao = @"data source=NOTEI3\SQLEXPRESS01;initial catalog=MeTransporta;user id=sara;password=vando100;Connect Timeout=";
        // private static tring conexao = @"data source=NOTEI3\SQLEXPRESS01;initial catalog=MeTransporta;user id=sara;password=vando100;Connect Timeout=";
        // public static string conexao = @"data source=NOTEI3\SQLEXPRESS01;initial catalog=MeTransporta;user id=sara;password=vando100;Connect Timeout=";

        /*
        public static List<Teste> ListaTeste()
        {
            List<Teste> lista = new List<Teste>();
            string sql = "SELECT * FROM Teste";
            
            using (SqlConnection con = new SqlConnection(conexao))
            {
                con.Open();

                using (SqlCommand comando = new SqlCommand(sql, con))
                {
                    using (SqlDataReader reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Teste teste = new Teste()
                            {
                                ID      = reader.GetInt32(0),
                                Nombre  = reader.GetString(1),
                                Salario = reader.GetDecimal(2)
                            };

                            lista.Add(teste);
                        }
                    }
                }

                con.Close();

                return lista;
            }
        }

        public static void AdicionaTeste(Teste teste)
        {
            string sql = "INSERT INTO Teste (Nombre,Salario) VALUES(@nombre, @salario)";

            using (SqlConnection con = new SqlConnection(conexao))
            {
                con.Open();

                using (SqlCommand comando = new SqlCommand(sql, con))
                {
                    comando.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = teste.Nombre;
                    comando.Parameters.Add("@salario", SqlDbType.Decimal).Value     = teste.Salario;
                    comando.CommandType = CommandType.Text;
                    comando.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public static void AlteraTeste(Teste teste)
        {
            string sql = "UPDATE Teste set Nombre = @nombre, Salario = @salario WHERE ID = @id";

            try
            {
                using (SqlConnection con = new SqlConnection(conexao))
                {
                    con.Open();

                    using (SqlCommand comando = new SqlCommand(sql, con))
                    {
                        comando.Parameters.Add("@nombre",  SqlDbType.VarChar, 100).Value = teste.Nombre;
                        comando.Parameters.Add("@salario", SqlDbType.Decimal).Value = teste.Salario;
                        comando.Parameters.Add("@id",      SqlDbType.Int).Value     = teste.ID;
                        comando.CommandType = CommandType.Text;
                        comando.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void ExcluiTeste(Teste teste)
        {
            string sql = "DELETE FROM Teste WHERE ID = @id";

            using (SqlConnection con = new SqlConnection(conexao))
            {
                con.Open();

                using (SqlCommand comando = new SqlCommand(sql, con))
                {
                    comando.Parameters.Add("@id", SqlDbType.Int).Value = teste.ID;
                    comando.CommandType = CommandType.Text;
                    comando.ExecuteNonQuery();
                }
                con.Close();
            }
        }*/
    } 
}
