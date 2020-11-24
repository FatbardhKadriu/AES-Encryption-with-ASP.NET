using AES_Encryption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            ViewBag.Message = "Your application encrypt page.";
            AESModel encryptModel = new AESModel();
            encryptModel.plaintext = plaintext;
            encryptModel.ciphertext = "cipheri";
            encryptModel.key = key;
            encryptModel.iv = iv;
            return View(encryptModel);
        }

        public ActionResult Decryption()
        {
            ViewBag.Message = "Your decrypt page.";

            return View();
        }

        [HttpPost]
        public ActionResult Decryption(String ciphertext, String key, String iv)
        {
            ViewBag.Message = "Your application encrypt page.";
            AESModel encryptModel = new AESModel();
            encryptModel.plaintext = "plaini";
            encryptModel.ciphertext = ciphertext;
            encryptModel.key = key;
            encryptModel.iv = iv;
            return View(encryptModel);
        }

    }
}