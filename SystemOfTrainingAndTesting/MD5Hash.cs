using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SystemOfTrainingAndTesting
{
    class MD5Hash
    {
        internal static string Compute(string str)
        {
            byte[] byteArr;
            string HashStr = "";
            byteArr = Encoding.ASCII.GetBytes(str);
            var md5Hash = MD5Cng.Create().ComputeHash(byteArr);
            HashStr = BitConverter.ToString(md5Hash).Replace("-", String.Empty).ToLower();
            return HashStr;
        }
    }
}
