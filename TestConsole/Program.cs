using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
namespace TestConsole
{
    /*TEST COMMENT*/
    class Program
    {
        static void Main(string[] args)
        {
            Test3();
            Console.Read();
        }
        private static void Test() 
        {
            ServiceReference1.FaturaUBLTRServiceSoapClient client = new ServiceReference1.FaturaUBLTRServiceSoapClient("FaturaUBLTRServiceSoap");

            ServiceReference1.EFaturaFirmalariUBLTRGirdi girdi = new ServiceReference1.EFaturaFirmalariUBLTRGirdi();
            girdi.Kimlik = new ServiceReference1.Kimlik();
            girdi.Kimlik.Kullanici = "akleasetest";
            girdi.Kimlik.Sifre = "Test@1234";
            girdi.SorgulamaZamani = DateTime.Now.AddDays(-10);
            ServiceReference1.EFaturaFirmalariUBLTRCikti cikti = client.EFaturaFirmalari(girdi);

            System.IO.MemoryStream msSample = new System.IO.MemoryStream(cikti.Firmalar);

            System.IO.Compression.ZipArchive zip = new ZipArchive(msSample);
            System.IO.Compression.DeflateStream mszip = (System.IO.Compression.DeflateStream)zip.Entries[0].Open();
            System.IO.MemoryStream msSample2 = new System.IO.MemoryStream();
            mszip.CopyTo(msSample2);
            byte[] bunzip = msSample2.ToArray();
            string fuck = System.Text.Encoding.UTF8.GetString(bunzip);
        }
        private static void Test2() 
        {
            string time = string.Format("{0:HH:mm:ss}", DateTime.Now);
                        
        }
        private static void Test3() 
        {
            string xml = System.IO.File.ReadAllText("c:/test/dc26950f-4fb1-4cfa-8ab4-1c7920a6f811_AP.xml");
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();            
            doc.LoadXml(xml);
            System.Xml.XmlNamespaceManager man = new System.Xml.XmlNamespaceManager(doc.NameTable);
            int a = doc.SelectNodes("/ApplicationResponse/cbc:UUID").Count;

        }

    }
}
