using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IovationBlackBoxDecrypt.DecryptClasses
{
    public class StarBucksSite
    {

        public static Dictionary<string, string> Decrypt(string encrypted)
        {
            byte[] deskey = new byte[] { 124, 76, 69, 0, 99, 2, (byte)200, (byte)163 };
            DESCryptoServiceProvider desProvider = new DESCryptoServiceProvider();
            desProvider.Mode = CipherMode.ECB;
            desProvider.Padding = PaddingMode.Zeros;
            desProvider.Key = deskey;
            using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(encrypted)))
            {
                using (CryptoStream cs = new CryptoStream(stream, desProvider.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs, Encoding.ASCII))
                    {
                        Dictionary<string, string> Deserialize(string serialized)
                        {
                            //if (!serialized.StartsWith("0500")) return null;
                            var dictionary = new Dictionary<string, string>();
                            for (var i = 4; i < serialized.Length;)
                            {
                                try
                                {
                                    var pp = serialized.Substring(i, 4);
                                    if (pp.Contains("\0")) break;

                                    var length = int.Parse(pp, NumberStyles.AllowHexSpecifier);
                                    var key = serialized.Substring(i += 4, length);
                                    length = int.Parse(serialized.Substring(i += length, 4), NumberStyles.AllowHexSpecifier);
                                    var value = serialized.Substring(i += 4, length);
                                    dictionary.Add(key, value);
                                    i += length;
                                }
                                catch
                                {
                                    break;
                                }
                            }
                            return dictionary;
                        }

                        var s = sr.ReadToEnd();
                        return Deserialize(s);
                    }
                }
            }

        }
    }
}
