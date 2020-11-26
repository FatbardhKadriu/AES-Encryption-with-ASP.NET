using AES_Encryption.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace AES_Encryption.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

 
        public ActionResult Encryption()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Encryption(String plaintext, String key, String iv)
        {
            List<String> results = new List<string>();
            results = EncryptAesManaged(plaintext, "e", key, iv);

            AESModel encryptModel = new AESModel();
            encryptModel.plaintext = plaintext;
            encryptModel.ciphertext = results[2];
            encryptModel.key = results[0];
            encryptModel.iv = results[1];
            return View(encryptModel);
        }

        public ActionResult Decryption()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Decryption(String ciphertext, String key, String iv)
        {
            List<String> results = EncryptAesManaged(ciphertext, "d", key, iv);
            
            AESModel encryptModel = new AESModel();
            encryptModel.plaintext = results[2];
            encryptModel.ciphertext = ciphertext;
            encryptModel.key = results[0];
            encryptModel.iv = results[1];
            return View(encryptModel);
        }

        List<String> EncryptAesManaged(String data, String action, String key, String iv)
        {
            List<String> key_iv = new List<String>();
            try
            {
                using (Aes myAes = Aes.Create())
                {
                    if (key != "")
                    {
                        if (iv == "") iv = Convert.ToBase64String(myAes.IV);
                        key_iv.Add(key);
                        key_iv.Add(iv);

                        if (key.Length != 44)
                        {
                            HashAlgorithm hash = MD5.Create();

                            myAes.Key = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
                            if (iv.Length != 24) myAes.IV = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(iv));
                            else myAes.IV = Convert.FromBase64String(iv);
                        }
                        else
                        {
                            myAes.Key = Convert.FromBase64String(key);
                            myAes.IV = Convert.FromBase64String(iv);
                        }
                    }
                    else
                    {
                        key_iv.Add(Convert.ToBase64String(myAes.Key));
                        key_iv.Add(Convert.ToBase64String(myAes.IV));
                    }                 

                    if (action == "e")
                    {
                        byte[] encrypted = Encrypt(data, myAes.Key, myAes.IV);
                        key_iv.Add(Convert.ToBase64String(encrypted));
                    }
                    else
                    {
                        string decrypted = Decrypt(Convert.FromBase64String(data), myAes.Key, myAes.IV);
                        key_iv.Add(decrypted);
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
            return key_iv;
        }

        byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }

        string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}