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
            Dictionary<String, String> parameters = new Dictionary<String, String> { { "Key", key }, { "IV", iv } };
            List<String> results = EncryptAesManaged(plaintext, "e", parameters);
            // ViewBag.Message = "Your application encrypt page.";
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
            // ViewBag.Message = "Your application encrypt page.";
            Dictionary<String, String> parameters = new Dictionary<String, String> { { "Key", key }, { "IV", iv } };
            List<String> results = EncryptAesManaged(ciphertext, "d", parameters);
            AESModel encryptModel = new AESModel();
            encryptModel.plaintext = results[2];
            encryptModel.ciphertext = ciphertext;
            encryptModel.key = results[0];
            encryptModel.iv = results[1];
            return View(encryptModel);
        }

        List<String> EncryptAesManaged(String data, String action, Dictionary<String, String> kwargs = null)
        {
            List<String> key_iv = new List<String>();

            try
            {
                using (Aes myAes = Aes.Create())
                {
                    if (kwargs != null)
                    {
                        string key = kwargs["Key"];
                        string iv = kwargs["IV"];

                        HashAlgorithm hash = MD5.Create();

                        key_iv.Add(key);
                        key_iv.Add(iv);

                        myAes.Key = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(key));
                        myAes.IV = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(iv));

                    }

                    //Console.WriteLine($"Encryption key: {Convert.ToBase64String(myAes.Key)}");
                    //Console.WriteLine($"Encryption IV: {Convert.ToBase64String(myAes.IV)}");


                    if (action == "e")
                    {
                        // Encrypt string    
                        byte[] encrypted = Encrypt(data, myAes.Key, myAes.IV);
                        // Print encrypted string    
                        //Console.WriteLine($"Encrypted data:{Convert.ToBase64String(encrypted)}");
                        key_iv.Add(Convert.ToBase64String(encrypted));
                    }
                    else
                    {
                        // Decrypt string
                        string decrypted = Decrypt(Convert.FromBase64String(data), myAes.Key, myAes.IV);
                        // Print decrypted string  
                        // Console.WriteLine($"Decrypted data: {decrypted}");
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
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    // Use a stream that links data streams to cryptographic transformations.
                    // Create a CryptoStream, pass it the msEncrypt, and encrypt it with the Aes class encryptor. 
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        // Create a StreamWriter for easy writing to the stream.
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return encrypted data    
            return encrypted;
        }

        string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    // Use a stream that links data streams to cryptographic transformations.
                    // Create a CryptoStream, pass it the msDecrypt, and dencrypt it with the Aes class decryptor. 
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        // Create a StreamRader for easy writing to the stream.
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            // Return decrypted data  
            return plaintext;
        }
    }
}