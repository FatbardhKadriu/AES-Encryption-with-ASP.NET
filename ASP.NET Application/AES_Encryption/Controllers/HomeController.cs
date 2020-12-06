using AES_Encryption.Models;
using AES_Encryption.Services;
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
        EncryptService encryptService = new EncryptService();
        DecryptService decyptService = new DecryptService();

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
            AESModel encryptModel = new AESModel();
            if (plaintext != "")
            {
                List<String> results = new List<string>();
                results = EncryptAesManaged(plaintext, "e", key, iv);

                encryptModel.plaintext = plaintext;
                encryptModel.ciphertext = results[2];
                encryptModel.key = results[0];
                encryptModel.iv = results[1];
                encryptModel.plainError = "";
            }
            else
            {
                encryptModel.plainError = "Plaintext field should not be empty!";
            }
            return View(encryptModel);
        }

        public ActionResult Decryption()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Decryption(String ciphertext, String key, String iv)
        {
            AESModel encryptModel = new AESModel();
            encryptModel.ciphertext = ciphertext;
            encryptModel.key = key;
            if (ciphertext == "")
            {
                encryptModel.cipherError = "Ciphertext field should not be empty!";
            }
            else if (key == "")
            {
                encryptModel.keyError = "Key field should not be empty!";
            }
            else if (iv == "")
            {
                encryptModel.ivError = "IV field should not be empty!";
            }
            else
            {
                try
                {
                    List<String> results = EncryptAesManaged(ciphertext, "d", key, iv);
                    encryptModel.plaintext = results[2];
                    encryptModel.key = results[0];
                    encryptModel.iv = results[1];
                    encryptModel.cipherError = "";
                    encryptModel.keyError = "";
                    encryptModel.ivError = "";
                    encryptModel.error = "";
                }
                catch (Exception)
                {
                    encryptModel.error = "Invalid Key or IV!";
                    encryptModel.key = "";
                }

            }
            return View(encryptModel);
        }

        List<String> EncryptAesManaged(String data, String action, String key, String iv)
        {
            List<String> key_iv = new List<String>();
            try
            {
                using (Aes myAes = Aes.Create())
                {
                    HashAlgorithm hashmd5 = MD5.Create();
                    HashAlgorithm hashsha256 = SHA256.Create();

                    // encrypt
                    if (action == "e")
                    {
                        // Only plaintext is given
                        if (key == "" && iv == "")
                        {
                            key_iv.Add(Convert.ToBase64String(myAes.Key));
                            key_iv.Add(Convert.ToBase64String(myAes.IV));
                            myAes.Key = hashsha256.ComputeHash(myAes.Key);
                            myAes.IV = hashmd5.ComputeHash(myAes.IV);
                        }
                        // Plaintext and encryption key are given
                        else if (key != "" && iv == "")
                        {
                            key_iv.Add(key);
                            key_iv.Add(Convert.ToBase64String(myAes.IV));
                            myAes.Key = hashsha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
                            myAes.IV = hashmd5.ComputeHash(myAes.IV);
                        }
                        // Plaintext, key and initialization vector are given
                        else
                        {
                            key_iv.Add(key);
                            key_iv.Add(iv);
                            myAes.Key = hashsha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
                            myAes.IV = hashmd5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(iv));
                        }

                        byte[] encrypted = encryptService.Encrypt(data, myAes.Key, myAes.IV);
                        key_iv.Add(Convert.ToBase64String(encrypted));
                    }
                    // decrypt 
                    else
                    {
                        key_iv.Add(key);
                        key_iv.Add(iv);

                        try
                        {
                            myAes.Key = hashsha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
                            myAes.IV = hashmd5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(iv));
                            string decrypted = decyptService.Decrypt(Convert.FromBase64String(data), myAes.Key, myAes.IV);
                            key_iv.Add(decrypted);
                        }
                        catch
                        {
                            try
                            {
                                myAes.Key = hashsha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
                                myAes.IV = hashmd5.ComputeHash(Convert.FromBase64String(iv));
                                string decrypted = decyptService.Decrypt(Convert.FromBase64String(data), myAes.Key, myAes.IV);
                                key_iv.Add(decrypted);
                            }
                            catch
                            {
                                myAes.Key = hashsha256.ComputeHash(Convert.FromBase64String(key));
                                myAes.IV = hashmd5.ComputeHash(Convert.FromBase64String(iv));
                                string decrypted = decyptService.Decrypt(Convert.FromBase64String(data), myAes.Key, myAes.IV);
                                key_iv.Add(decrypted);
                            }
                        }
                    }
                }
            }
            catch (Exception exp) { }
            return key_iv;
        }


    }

}