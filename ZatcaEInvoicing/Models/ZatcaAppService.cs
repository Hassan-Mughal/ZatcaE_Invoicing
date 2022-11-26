using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ZatcaEInvoicing.Models
{
    public interface IZatcaAppService
    {
        Task<CertificateDto> ComplianceCertificate(ComplianceInputDto input);
        Task<CertificateDto> ProductionCertificate(CertificateDto input);
    }
    public class ZatcaAppService
    {
        private readonly string baseUrl = "https://gw-apic-gov.gazt.gov.sa/e-invoicing/developer-portal";
        private static string binarySecurityToken = string.Empty;
        private static string secret = string.Empty;

        public async Task<CertificateDto> ComplianceCertificate(ComplianceInputDto input)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("compliance", Method.Post);
            request.AddHeader("Accept-Version", "V2");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("OTP", input.OTP);
            var body = new
            {
                csr = input.CSR
            };
            var convertJson = JsonConvert.SerializeObject(body);
            request.AddParameter("application/json", convertJson, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            var statusCode = (int?)response.StatusCode;
            if (response.IsSuccessful)
            {
                var mapped = JsonConvert.DeserializeObject<CertificateDto>(response.Content);
                mapped.StatusCode = statusCode;
                return mapped;
            }
            else
            {
                return new CertificateDto{ StatusCode = statusCode };
            }
        }

        public async Task<CertificateDto> ProductionCertificate(CertificateDto input)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("production/csids",Method.Post);
            request.AddHeader("Accept-Version", "V2");
            request.AddHeader("Authorization", "Basic " + Base64Encode(input.BinarySecurityToken + ":" + input.Secret));
            request.AddHeader("Content-Type", "application/json");
            var body = new
            {
                compliance_request_id = input.RequestId
            };
            var convertJson = JsonConvert.SerializeObject(body);
            request.AddParameter("application/json", convertJson, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            var statusCode = (int?)response.StatusCode;
            if (response.IsSuccessful)
            {
                var mapped = JsonConvert.DeserializeObject<CertificateDto>(response.Content);
                mapped.StatusCode = statusCode;
                secret = mapped.Secret;
                binarySecurityToken = mapped.BinarySecurityToken;
                return mapped;
            }
            else
            {
                return new CertificateDto { StatusCode = statusCode };
            }
        }
        public bool ValidInvoice(string input)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("validation/service", Method.Post);
            request.AddHeader("Content-Type", "text/plain");

            request.AddParameter("text/plain", input, ParameterType.RequestBody);
            var response = client.Execute(request);
            var statusCode = (int?)response.StatusCode;
            if (response.IsSuccessful)
            {
                return (bool)JObject.Parse(response.Content)["valid"];
            }
            else
            {
                return false;
            }
        }
        public async Task<dynamic> ReportingInvoice(CertificateDto input)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest("invoices/reporting/single", Method.Post);
            request.AddHeader("Accept-Version", "V2");
            request.AddHeader("Clearance-Status", "0");
            request.AddHeader("accept-language", "en");
            request.AddHeader("Authorization", "Basic " + Base64Encode(input.BinarySecurityToken + ":" + input.Secret));
            request.AddHeader("Content-Type", "application/json");
            var body = new
            {
                invoiceHash = input.InvoiceHash,
                uuid = input.UUID,
                invoice = input.Invoice
            };
            var convertJson = JsonConvert.SerializeObject(body);
            request.AddParameter("application/json", convertJson, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            var statusCode = (int?)response.StatusCode;
            if (response.IsSuccessful)
            {
                var mapped = JsonConvert.DeserializeObject(response.Content);
                return mapped;
            }
            else
            {
                return new { StatusCode = statusCode };
            }
        }


        #region Encode/Decode
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
        #endregion

    }
}