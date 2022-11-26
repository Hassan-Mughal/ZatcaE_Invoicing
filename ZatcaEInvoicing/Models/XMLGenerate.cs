using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using UblSharp;
using UblSharp.CommonAggregateComponents;
using UblSharp.CommonExtensionComponents;
using UblSharp.UnqualifiedDataTypes;
using UblSharp.XmlDigitalSignature;

namespace ZatcaEInvoicing.Models
{
    public static class XMLGenerate
    {
        public static InvoiceType Create()
        {
            var date = "2022-03-13";
            var time = "14:40:40";
            var doc = new InvoiceType
            {
                ProfileID = "reporting:1.0",
                ID = "SME00062",
                UUID = Guid.NewGuid().ToString(),
                IssueDate = date,
                IssueTime = time,
                InvoiceTypeCode = new CodeType
                {
                    name = "0111010",
                    Value = "388"
                },
                DocumentCurrencyCode = new CodeType
                {
                    Value = "SAR"
                },
                TaxCurrencyCode = new CodeType
                {
                    Value = "SAR"
                },
                AdditionalDocumentReference = new List<DocumentReferenceType>()
                {
                    new DocumentReferenceType
                    {
                        ID = "ICV",
                        UUID="62"
                    },
                    new DocumentReferenceType
                    {
                        ID = "PIH",
                        Attachment = new AttachmentType
                        {
                            EmbeddedDocumentBinaryObject = new BinaryObjectType
                            {
                                mimeCode = "text/plain",
                                Value = Convert.FromBase64String("NWZlY2ViNjZmZmM4NmYzOGQ5NTI3ODZjNmQ2OTZjNzljMmRiYzIzOWRkNGU5MWI0NjcyOWQ3M2EyN2ZiNTdlOQ==")
                            }
                        }
                    },
                    //new DocumentReferenceType
                    //{
                    //    ID = "QR",
                    //    Attachment = new AttachmentType
                    //    {
                    //        EmbeddedDocumentBinaryObject = new BinaryObjectType
                    //        {
                    //            mimeCode = "text/plain",
                    //            Value = Convert.FromBase64String("UjBsR09EbGhjZ0dTQUxNQUFBUUNBRU1tQ1p0dU1GUXhEUzhi")
                    //        }
                    //    }
                    //}
                },
                AccountingSupplierParty = new SupplierPartyType
                {
                    Party = new PartyType
                    {
                        PartyIdentification = new List<PartyIdentificationType>()
                        {
                            new PartyIdentificationType
                            {
                                ID = new IdentifierType
                                {
                                    schemeID = "CRN",
                                    Value = "454634645645654"
                                }
                            }
                        },
                        PostalAddress = new AddressType
                        {
                            StreetName= "test",
                            BuildingNumber = "3454",
                            CitySubdivisionName = "test",
                            CityName = "Riyadh",
                            PostalZone = "12345",
                            
                            Country = new CountryType
                            {
                                IdentificationCode = new CodeType
                                {
                                    Value = "SA"
                                }
                            }
                        },
                        PartyTaxScheme = new List<PartyTaxSchemeType>()
                        {
                            new PartyTaxSchemeType
                            {
                                CompanyID = new IdentifierType
                                {
                                    Value = "300075588700003"
                                },
                                TaxScheme = new TaxSchemeType
                                {
                                    ID = new IdentifierType
                                    {
                                        Value = "VAT"
                                    }
                                }
                            }
                        },
                        PartyLegalEntity = new List<PartyLegalEntityType>()
                        {
                            new PartyLegalEntityType
                            {
                                RegistrationName = "Ahmed Mohamed AL Ahmady",
                            }
                        },
                    }
                },
                AccountingCustomerParty = new CustomerPartyType
                {
                    Party = new PartyType
                    {
                        PartyIdentification = new List<PartyIdentificationType>()
                        {
                            new PartyIdentificationType
                            {
                                ID = new IdentifierType
                                {
                                    schemeID = "NAT",
                                    Value = "2345"
                                }
                            }
                        },
                        PostalAddress = new AddressType
                        {
                            StreetName = "test",
                            BuildingNumber = "3454",
                            CitySubdivisionName = "test",
                            CityName = "Riyadh",
                            PostalZone = "12345",
                            Country = new CountryType
                            {
                                IdentificationCode = new CodeType
                                {
                                    Value = "SA"
                                }
                            }
                        },
                        PartyLegalEntity = new List<PartyLegalEntityType>()
                        {
                            new PartyLegalEntityType
                            {
                                RegistrationName = "sdsa",
                            }
                        },

                    }
                },
                Delivery = new List<DeliveryType>()
                {
                    new DeliveryType
                    {
                        ActualDeliveryDate = "2022-03-13",
                        LatestDeliveryDate ="2022-03-15"
                    }
                },
                PaymentMeans = new List<PaymentMeansType>()
                {
                    new PaymentMeansType
                    {
                        PaymentMeansCode = new CodeType
                        {
                            Value = "10"
                        },
                    }
                },
                AllowanceCharge = new List<AllowanceChargeType>()
                {
                    new AllowanceChargeType
                    {
                        ChargeIndicator = false,
                        AllowanceChargeReason = new List<TextType>()
                        {
                            new TextType
                            {
                                Value = "discount"
                            }
                        },
                        Amount = new AmountType
                        {
                            currencyID = "SAR",
                            Value = 2
                        },
                        TaxCategory=new List<TaxCategoryType>()
                        {
                            new TaxCategoryType
                            {
                                    ID = new IdentifierType
                                    {
                                        schemeID = "UN/ECE 5305",
                                        schemeAgencyID = "6",
                                        Value = "S"
                                    },
                                    Percent = 15,
                                    TaxScheme = new TaxSchemeType
                                    {
                                        ID = new IdentifierType
                                        {
                                            schemeID = "UN/ECE 5153",
                                            schemeAgencyID = "6",
                                            Value = "VAT"
                                        }
                                    }
                            }
                        }
                    },
                },
                TaxTotal = new List<TaxTotalType>()
                {
                    new TaxTotalType
                    {
                        TaxAmount = new AmountType
                        {
                            currencyID = "SAR",
                            Value = 144.9M
                        },
                        TaxSubtotal = new List<TaxSubtotalType>()
                        {
                            new TaxSubtotalType
                            {
                                TaxableAmount = new AmountType
                                {
                                    currencyID = "SAR",
                                    Value = 966.00M
                                },
                                TaxAmount = new AmountType
                                {
                                    currencyID = "SAR",
                                    Value = 144.90M
                                },
                                TaxCategory = new TaxCategoryType
                                {
                                    ID = new IdentifierType
                                    {
                                        schemeID = "UN/ECE 5305",
                                        schemeAgencyID = "6",
                                        Value = "S"
                                    },
                                    Percent = 15.00M,
                                    TaxScheme = new TaxSchemeType
                                    {
                                        ID = new IdentifierType
                                        {
                                            schemeID = "UN/ECE 5153",
                                            schemeAgencyID = "6",
                                            Value = "VAT"
                                        }
                                    }
                                }
                            },
                        },
                    },
                    new TaxTotalType
                    {
                        TaxAmount = new AmountType
                        {
                            currencyID = "SAR",
                            Value = 144.9M
                        }
                    }
                },
                LegalMonetaryTotal = new MonetaryTotalType
                {
                    LineExtensionAmount = new AmountType
                    {
                        currencyID = "SAR",
                        Value = 966.00M
                    },
                    TaxExclusiveAmount = new AmountType
                    {
                        currencyID = "SAR",
                        Value = 964.00M
                    },
                    TaxInclusiveAmount = new AmountType
                    {
                        currencyID = "SAR",
                        Value = 1108.90M
                    },
                    AllowanceTotalAmount = new AmountType
                    {
                        currencyID = "SAR",
                        Value = 2.00M
                    },
                    PrepaidAmount = new AmountType
                    {
                        currencyID = "SAR",
                        Value = 0.00M
                    },
                    PayableAmount = new AmountType
                    {
                        currencyID = "SAR",
                        Value = 1108.90M
                    }
                },
                InvoiceLine = new List<InvoiceLineType>()
                {
                    new InvoiceLineType
                    {
                        ID = "1",
                        InvoicedQuantity = new QuantityType
                        {
                            unitCode = "PCE",
                            Value = 44.000000M
                        },
                        LineExtensionAmount = new AmountType
                        {
                            currencyID = "SAR",
                            Value = 966.00M
                        },
                        TaxTotal = new List<TaxTotalType>()
                        {
                            new TaxTotalType
                            {
                                TaxAmount = new AmountType
                                {
                                    currencyID = "SAR",
                                    Value = 144.90M
                                },
                                RoundingAmount= new AmountType
                                {
                                    currencyID = "SAR",
                                    Value = 1110.90M
                                }
                            }
                        },
                        Item = new ItemType
                        {
                            Name = "dsd",
                            ClassifiedTaxCategory = new List<TaxCategoryType>()
                            {

                                new TaxCategoryType
                                {
                                    ID = new IdentifierType
                                    {
                                        Value = "S"
                                    },
                                    Percent = 15.00M,
                                    TaxScheme = new TaxSchemeType
                                    {
                                        ID = new IdentifierType
                                        {
                                            Value = "VAT"
                                        }
                                    }
                                }
                            }
                        },
                        Price = new PriceType
                        {
                            PriceAmount = new AmountType
                            {
                                currencyID = "SAR",
                                Value = 22.00M
                            },
                            AllowanceCharge = new List<AllowanceChargeType>()
                            {
                                new AllowanceChargeType
                                {
                                    ChargeIndicator = false,
                                    AllowanceChargeReason = new List<TextType>()
                                    {
                                        new TextType
                                        {
                                            Value = "discount"
                                        }
                                    },
                                    Amount = new AmountType
                                    {
                                        currencyID = "SAR",
                                        Value = 2.00M
                                    }
                                }
                            }
                        }
                    }
                }
            };
            doc.Xmlns = new System.Xml.Serialization.XmlSerializerNamespaces(new[]
            {
                new XmlQualifiedName("cac","urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"),
                new XmlQualifiedName("cbc","urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"),
                new XmlQualifiedName("ext","urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"),
            });
            return doc;
        }

    }
}