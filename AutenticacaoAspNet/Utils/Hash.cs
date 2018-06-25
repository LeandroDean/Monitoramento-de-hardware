using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;   
using System.Text;
using System.Web;

namespace AutenticacaoAspNet.Utils
{
    public class Hash
    {
        // Galera essa é a classe responsavel pela criptografia da senha que vai estar no bd
        // eu coloquei essa biblioteca na aplicação para entender como funciona criptografia
        // Se vocês optarem por não utilizar posso retirar tranquilamente
        // mas acho valido pelo aprendizado
        // Aqui estou utilizando um algoritmos de criptgrafia o SHA256 (Secure Hash Algorithm)

        public static string GerarHash(string texto)  // recebe o texto
        {
            SHA256 sha256 = SHA256Managed.Create();  
            byte[] bytes = Encoding.UTF8.GetBytes(texto);  // converto o texto em bytes
            byte[] hash = sha256.ComputeHash(bytes);   // gerando hash apartir desses bytes, e tbm um array
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)  // percorer array e convertendo o byte para Alfanumérico
            {
                result.Append(hash[i].ToString("X"));
            }
            return result.ToString();  // retorandno conteudo xD
        }
    }
}