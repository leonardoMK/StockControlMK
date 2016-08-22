﻿using MaryKey.Dominio;
using MaryKey.Dominio.Repositorio;
using MaryKey.Dominio.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaryKey.Repositorio.EF.Servicos
{
    public class ServicoCriptografia : IServicoCriptografia
    {
        public string CriptografarSenha(string senha)
        {
            senha += "%InesMK%";
            return GerarMd5(senha);
        }

        private string GerarMd5(string texto)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(texto);
            byte[] hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
