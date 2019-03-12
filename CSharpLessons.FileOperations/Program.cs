using System;
using System.Data;
using System.IO;
using System.Xml;

namespace CSharpLessons.FileOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // DriveInfo
            var drives = DriveInfo.GetDrives();
            foreach(var drive in drives)
            {
                Console.WriteLine($"Drive {drive.Name} {drive.DriveType} ({drive.VolumeLabel})");
                Console.WriteLine($"Drive {drive.TotalSize}");
                Console.WriteLine($"Drive {drive.TotalFreeSpace}");
                Console.WriteLine($"Drive {drive.AvailableFreeSpace}");
                Console.WriteLine();
            }

            // DirectoryInfo

            var dirInfo = new DirectoryInfo(@".");
            Console.WriteLine($"Directories in {dirInfo}:");
            foreach(var dir in dirInfo.EnumerateDirectories())
            {
                Console.WriteLine(dir);
            }
            Console.WriteLine($"Files in {dirInfo}:");
            foreach(var file in dirInfo.EnumerateFiles())
            {
                Console.WriteLine(file);
            }
            


            // Directory

            Console.WriteLine($"Directories in .:");
            foreach(var dir in Directory.EnumerateDirectories("."))
            {
                Console.WriteLine(dir);
            }
            Console.WriteLine($"Files in .:");
            foreach(var file in Directory.EnumerateFiles("."))
            {
                Console.WriteLine(file);
            }
            Console.WriteLine();




            // FileInfo
            var fileName = "CSharpLessons.FileOperations.csproj";
            Console.WriteLine($"File {fileName}");
            var fileInfo = new FileInfo(fileName);
            Console.WriteLine($"Size {fileInfo.Length}");



            // File
            var tempFileName = Path.Combine(Path.GetTempPath(), fileName);
            File.Copy(fileName, tempFileName, true);
            Console.WriteLine($"Copied file to temp dir....");
            var tempFileInfo = new FileInfo(tempFileName);
            Console.WriteLine($"File {tempFileName}");
            Console.WriteLine($"Size {tempFileInfo.Length}");
            Console.WriteLine();



            // Reading text from file
            Console.WriteLine("Content:");
            var content = File.ReadAllText(tempFileName);
            Console.WriteLine(content);



            // Append text to file
            using (StreamWriter sw = File.AppendText(tempFileName)) 
            {
                 sw.WriteLine("<!--");
                 sw.WriteLine("Some extra text");
                 sw.WriteLine("-->");
            }



            Console.WriteLine($"Length is still {tempFileInfo.Length}");
            tempFileInfo.Refresh();
            Console.WriteLine($"Actual length is {tempFileInfo.Length}");
            Console.WriteLine();


            byte[] binaryContent = null;
            using(FileStream fileStream = tempFileInfo.Open(FileMode.Open))
            {
                using (BinaryReader reader = new BinaryReader(fileStream))
                {
                    binaryContent = reader.ReadBytes((int)tempFileInfo.Length);
                }
            }


            Array.Reverse(binaryContent);
            using(FileStream fileStream = tempFileInfo.Open(FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
                    writer.Write(binaryContent);
                }
            }



            Array.Reverse(binaryContent);
            using(FileStream fileStream = tempFileInfo.Open(FileMode.Create))
            {
                using (BinaryWriter reader = new BinaryWriter(fileStream))
                {
                    reader.Write(binaryContent);
                }
            }


            // XmlDocument
            XmlDocument document = null;
            using(FileStream fileStream = tempFileInfo.Open(FileMode.Open))
            {
                document = new XmlDocument();
                document.Load(fileStream);
            }



            XmlNode node = document.SelectSingleNode("//OutputType");
            Console.WriteLine($"OutputType {node.InnerText}");
            node.AppendChild(document.CreateTextNode("Some text"));



            using (var stringWriter = new StringWriter())
            {
                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    document.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    Console.WriteLine(stringWriter.GetStringBuilder().ToString());
                    Console.WriteLine();
                }
            }




            XmlDocument orgDocument = new XmlDocument();
            
            XmlElement organizationElement = orgDocument.CreateElement("organization");
            organizationElement.SetAttribute("name", "ORGANIZATION");
            orgDocument.AppendChild(organizationElement);

            XmlElement employeeElement = orgDocument.CreateElement("employee");
            employeeElement.SetAttribute("name", "Alice");
            organizationElement.AppendChild(employeeElement);

            XmlElement employeeElement2 = orgDocument.CreateElement("employee");
            employeeElement2.SetAttribute("name", "Bob");
            employeeElement.AppendChild(employeeElement2);
           

            using (var stringWriter = new StringWriter())
            {
                using (var xmlTextWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                {
                    orgDocument.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    Console.WriteLine(stringWriter.GetStringBuilder().ToString());
                    Console.WriteLine();
                }
            }

             Console.Read();
        }
    }
}
