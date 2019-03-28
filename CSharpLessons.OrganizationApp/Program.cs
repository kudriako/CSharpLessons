using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using Newtonsoft.Json;
using CSharpLessons.OrganizationModel;
using CSharpLessons.OrganizationModel.Offices;

namespace CSharpLessons.OrganizationApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Start();

            var alice = new Employee() { Name = "Alice", Title = "Test Engineer" };
            var bruce = new Employee() { Name = "Bruce", Title = "Developer" };
            var chloe = new Manager() { Name = "Chloe", Title = "Program Manager" };
            var doris = new Employee() { Name = "Doris", Title = "Build Engineer" };
            var ethan = new Manager() { Name = "Ethan", Title = "Release Manager" };
            var frank = new Manager() { Name = "Frank", Title = "Director" };

            var organization = new Organization("DoodleSoft");

            var taxOffice = new TaxOffice();
            var insuranceOffice = new InsuranceOffice();
            var pensionFundOffice = new PensionFundOffice();

            organization.AddEmployee(alice, chloe);
            organization.AddEmployee(bruce, chloe);
            organization.AddEmployee(chloe, frank);
            organization.AddEmployee(doris, ethan);
            organization.AddEmployee(ethan, frank);
            organization.AddEmployee(frank, null);
            organization.Director = frank;

            // 1. Сериализация
            // IFormatter
            // https://docs.microsoft.com/ru-ru/dotnet/api/system.runtime.serialization.iformatter?view=netframework-4.7.2
            
            // Formatter
            // https://docs.microsoft.com/ru-ru/dotnet/api/system.runtime.serialization.formatter?view=netframework-4.7.2

            // SerializableAttribute
            // https://docs.microsoft.com/ru-ru/dotnet/api/system.serializableattribute?view=netframework-4.7.2

           
            // 2.
            // Форматтеры
            // BinaryFormatter

            // using System.Runtime.Serialization.Formatters.Binary;
            // using System.IO;
            /*Console.Write("=============== BINARY ===================");
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream("organization.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, organization);
            }

            using (FileStream fs = new FileStream("organization.dat", FileMode.Open))
            {
                var org2 = (Organization)formatter.Deserialize(fs);
                Console.Write(org2);
            }*/

            // SoapFormatter
            // То же самое, только XML c декларацией типов
            // <SOAP-ENV:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
            // xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:SOAP-ENC="http://schemas.xmlsoap.org/soap/encoding/" 
            // xmlns:SOAP-ENV="http://schemas.xmlsoap.org/soap/envelope/" xmlns:clr="http://schemas.microsoft.com/soap/encoding/clr/1.0" 
            // SOAP-ENV:encodingStyle="http://schemas.xmlsoap.org/soap/encoding/">

            // 3.
            // XmlSerializer
            // using System.Xml.Serialization;
            
            /*Console.Write("=============== XML ===================");
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(Organization));

            using (FileStream fs = new FileStream("organization.xml", FileMode.OpenOrCreate))
            {
                xmlFormatter.Serialize(fs, organization);
            }

            using (FileStream fs = new FileStream("organization.xml", FileMode.OpenOrCreate))
            {
                Organization org3 = (Organization)xmlFormatter.Deserialize(fs);
                Console.Write(org3);
            }*/

            // Newtonsoft Json
            // dotnet add CSharpLessons.OrganizationApp package Newtonsoft.Json
            // dotnet add CSharpLessons.OrganizationModel package Newtonsoft.Json
            // using Newtonsoft.Json;

            Console.Write("=============== JSON ===================");
            ///var json = JsonConvert.SerializeObject(organization);
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            serializerSettings.Formatting = Formatting.Indented;
            var json = JsonConvert.SerializeObject(organization, serializerSettings);
            
            File.WriteAllText("organization.json", json);

            var json2 = File.ReadAllText("organization.json");
            var org4 = JsonConvert.DeserializeObject(json2);
            

            End();
        }

        static void Start()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void End()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
