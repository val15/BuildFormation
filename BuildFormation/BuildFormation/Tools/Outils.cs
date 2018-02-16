using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BuildFormation.Tools
{
    public static class Outils
    {
        public static string EncodeMd5(string motDePasse)
        {
            string motDePasseSel = "BuildFormation" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }
    }
}