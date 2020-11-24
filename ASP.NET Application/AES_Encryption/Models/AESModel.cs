using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AES_Encryption.Models
{
    public class AESModel
    {
        public string plaintext { get; set; }

        public string ciphertext { get; set; }

        public string key { get; set; }

        public string iv { get; set; }

    }
}