using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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

            organization.AddEmployee(alice, chloe);
            organization.AddEmployee(bruce, chloe);
            organization.AddEmployee(chloe, frank);
            organization.AddEmployee(doris, ethan);
            organization.AddEmployee(ethan, frank);
            organization.AddEmployee(frank, null);
            organization.Director = frank;

            // ТЕОРИЯ

            // 1. ILDASM / dotPeek
            // Манифест
            // Метаданные
            // Программный код
            // Ресурсы

            // 2. Assembly и Module

            // С ЧЕГО НАЧАТЬ РАБОТУ 
            // using System.Reflection
            
            // 3. Type

            Type type = typeof(Organization);
            Console.WriteLine(type.FullName);

            // 4. MemberInfo
            // MethodInfo
            // FieldInfo
            // EventInfo
            // PropertyInfo
            // ConstructorInfo

            foreach(var member in type.GetMembers())
            {
                Console.WriteLine($"{member.Name} [{member.MemberType}]");
            }
            Console.WriteLine();

            Console.WriteLine(typeof(EmployeeBase).FullName);
            foreach(var method in typeof(EmployeeBase).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)) // BindingFlags.Instance | BindingFlags.Public
            {
                Console.WriteLine($"{method.Name} [{method.MemberType}] {(method.IsPrivate?"private ":"")} {(method.IsPublic?"public ":"")} {(method.IsAbstract?"abstract ":"")}");
            }

            
            // ParameterInfo
            foreach(var constructor in typeof(Organization).GetConstructors())
            {
                Console.Write("Constructor with parameters: ");
                foreach(var param in constructor.GetParameters())
                {
                    Console.WriteLine($"{param.Name} [{param.ParameterType}]");
                }
            }

            // 5. CustomAttributeData
            // Load assembly

            Assembly assembly = Assembly.LoadFile(@"C:\Source\CSharpLessons\CSharpLessons.OrganizationApp\bin\Debug\netcoreapp2.2\CSharpLessons.OrganizationApp.dll");
            // class Activator
            
            // Create Instance
            var org = (Organization)Activator.CreateInstance(typeof(Organization), "Empire");
            org.Director = new Employee() { Name = "Alexander", Title = "The Great" };

            // Invoke member
            MethodInfo minfo = typeof(Organization).GetMethod("ToString");
            string result = (string)minfo.Invoke(org, null);
            Console.WriteLine(result);


            // ЕЩЕ ТЕОРИЯ
            // 6. Отражения и безопасность

            // 7. Генерирование кода

            // Задание
            // Посчитать, количество типов в основной сборке typeof(Object).GetAssembly()
            // Посчитать, сколько из типов являются классам, структурами, энумами, интерфейсами и аттрибутами
            // Посчитать, сколько из типов являются обобщенными
            // Посчитать, сколько из типов помечены аттрибутом Serializable
            // Посчитать, сколько из типов не могут иметь наследников

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
