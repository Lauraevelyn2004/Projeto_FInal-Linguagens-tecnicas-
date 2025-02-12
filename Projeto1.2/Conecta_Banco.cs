using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Projeto1._2
{
    public class Cliente
    {
        private int Cliente_ID;
        private string nome;
        private string email;
        private string telefone;
        private string endereco;
        private string senha;

        
        public string Nome { get => nome; set => nome = value; }
        public string Email { get => email; set => email = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public string Senha { get => senha; set => senha = value; }
        public int Cliente_ID1 { get => Cliente_ID; set => Cliente_ID = value; }
    }
    internal class Conecta_Banco
    {

        
        MySqlConnection conexao = new MySqlConnection("server=localhost;user id=root;password=;database=casaracao");
        public string mensagem;

        // Insere um novo cliente no banco de dados
        public bool InsereCliente(Cliente novoCliente)
        {
            try
            {
                conexao.Open();

                // Preparar o comando
                MySqlCommand cmd = new MySqlCommand("sp_inserecliente", conexao)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Adicionar os parâmetros necessários para o procedimento armazenado
                cmd.Parameters.AddWithValue("nome", novoCliente.Nome);
                cmd.Parameters.AddWithValue("email", novoCliente.Email);
                cmd.Parameters.AddWithValue("telefone", novoCliente.Telefone);
                cmd.Parameters.AddWithValue("endereco", novoCliente.Endereco);
                cmd.Parameters.AddWithValue("senha", novoCliente.Senha); // A senha já está criptografada

                // Executa o comando
                cmd.ExecuteNonQuery();

                return true; // Retorna true se a inserção for bem-sucedida
            }
            catch (MySqlException erro)
            {
                // Exibe a mensagem de erro no console se algo der errado
                Console.WriteLine($"Erro ao inserir registro: {erro.Message}");
                return false; // Retorna false se ocorrer um erro
            }
        }


        public bool AlteraCliente(Cliente novoCliente)
        {
            try
            {
                conexao.Open();

                // Preparar o comando
                MySqlCommand cmd = new MySqlCommand("sp_alteraCliente", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cliente_id", novoCliente.Cliente_ID1);
                cmd.Parameters.AddWithValue("nome", novoCliente.Nome);
                cmd.Parameters.AddWithValue("Email", novoCliente.Email);
                cmd.Parameters.AddWithValue("telefone", novoCliente.Telefone);
                cmd.Parameters.AddWithValue("endereco", novoCliente.Endereco);
                cmd.Parameters.AddWithValue("senha", novoCliente.Senha);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException erro)
            {
                // Mensagem de erro
                Console.WriteLine($"Erro ao alterar registro: {erro.Message}");
                return false;
            }

        }










    }
}
