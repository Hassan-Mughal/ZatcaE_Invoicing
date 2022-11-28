using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using ZatcaEInvoicing.Models;
using SDKNETFrameWorkLib.BLL;
using UblSharp;
using ZXing.QrCode;


namespace ZatcaEInvoicing.Controllers
{
    public class HomeController : Controller
    {
        #region PRMs
        private readonly string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\Text.xml";
        ZatcaAppService zatcaAppService = new ZatcaAppService();
        HashingValidator _IHashingValidator = new SDKNETFrameWorkLib.BLL.HashingValidator();
        QRValidator _IQRValidator = new SDKNETFrameWorkLib.BLL.QRValidator();
        EInvoiceValidator _IEInvoiceValidator = new SDKNETFrameWorkLib.BLL.EInvoiceValidator();
        EInvoiceSigningLogic _IEInvoiceSigningLogic = new SDKNETFrameWorkLib.BLL.EInvoiceSigningLogic();
        #endregion
        public async Task<ActionResult> Index()
        {
            try
            {
                var invoiceType = XMLGenerate.Create();
                CreateXMLFile(invoiceType);
                var invoiceBase64Encode = Base64Encode(System.IO.File.ReadAllText(filePath));
                if (System.IO.File.Exists(filePath) && zatcaAppService.ValidInvoice(invoiceBase64Encode))
                {
                    var invoiceHash = _IHashingValidator.GenerateEInvoiceHashing(filePath);
                    var credential = await zatcaAppService.ComplianceCertificate(new ComplianceInputDto());
                    var credential2 = await zatcaAppService.ProductionCertificate(credential);
                    var result = await zatcaAppService.ReportingInvoice(new CertificateDto
                    {
                        InvoiceHash=invoiceHash.ResultedValue,
                        UUID = invoiceType.UUID.Value,
                        Invoice= invoiceBase64Encode,
                        RequestId = credential2.RequestId,
                        BinarySecurityToken= credential2.BinarySecurityToken,
                        Secret= credential2.Secret
                    });
                }
                return View();
            }
            catch(Exception ex)
            {
                string result = ex.Message;
                Response.StatusCode = 404;
                return Json(result);
            }
        }

        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Private Methods
        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        private void CreateXMLFile(InvoiceType invoice)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(InvoiceType));
            if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
            using (TextWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, invoice);
                writer.Close();
            }
        }
        #endregion

    }
}