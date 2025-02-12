using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Projeto1._2
{
    internal class Funcoes
    {
        public string server = "server=localhost;user id=root;password=;database=casaracao";

        public static DataTable DtLogin;


        private static string connectionString = "server=localhost;user id=root;password=;database=casaracao";

        public static void ExecuteQuery(string ComandoSql, params MySqlParameter[] parametros)
        {
            using (MySqlConnection conexao = new MySqlConnection(connectionString))
            {
                conexao.Open();
                using (MySqlCommand cmd = conexao.CreateCommand())
                {
                    cmd.CommandText = ComandoSql;
                    cmd.Parameters.AddRange(parametros);
                    cmd.ExecuteNonQuery(); // Executa o comando SQL
                }
            }
        }


        //Memsagem de erro
        public static void MsgErro(string Msg)
        {
            MessageBox.Show(Msg, "Cadastro de clientes", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //Funcoes.MsgErro("");
        }//Fim MsgErro


        //Mensagem de alerta
        public static void MsgAlerta(string Msg)
        {
            MessageBox.Show(Msg, "Cadastro de clientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //Funcoes.MsgAlerta("");
        }//Fim MsgAlerta

        //Mensagem de informação
        public static void MsgOK(string Msg)
        {
            MessageBox.Show(Msg, "Cadastro de clientes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //Funcoes.MsgOK("");
        }//Fim MsgOK

        //Mensagem de pergunta
        public static bool Pergunta(string Msg)
        {
            if (MessageBox.Show(Msg, "Cadastro de clientes", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
                //Funcoes.Pergunta("");
            }
        }//Fim Pergunta

        //primeira letra maiuscula
        public static void PriMaiuscula(Control ctr)
        {
            TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;

            String nome = ctr.Text;
            nome = textInfo.ToTitleCase(nome);

            nome = nome.Replace("Das", "das")
                       .Replace("Da", "da")
                       .Replace("Dos", "dos")
                       .Replace("Do", "do")
                       .Replace("De", "de");

            ctr.Text = nome;

            if (ctr is TextBox txt)
            {
                txt.SelectionStart = txt.Text.Length;
            }
            else if (ctr is ComboBox cb)
            {
                cb.SelectionStart = cb.Text.Length;
            }

            // Funcoes.PriMaiuscula(txt ou cb);

        }//Fim PriMaiuscula



        public static DataTable BuscaSql(string ComandoSql)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conexao = new MySqlConnection(@"server=localhost;user id=root;password=;database=casaracao"))
            {

                conexao.Open();
                using MySqlCommand cmd = conexao.CreateCommand();
                {
                    cmd.CommandText = ComandoSql;

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(dt);

                        return dt;
                    }



                }
            }
        }

        public static void CarregarCombobox(ComboBox cb, string tabela, string campo)
        {

            cb.DataSource = Funcoes.BuscaSql("SELECT DISTINCT " + campo + "  FROM " + tabela + "WHERE " + campo + "<> ''");
            cb.DisplayMember = campo;
            cb.SelectedIndex = -1;


        }

        public static string CriptografarSenha(string senha)
        {
            // Criptografia simples com SHA256
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
                return Convert.ToBase64String(bytes); // Retorna a senha criptografada em Base64
            }
        }





    }

}
