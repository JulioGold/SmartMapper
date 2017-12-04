using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Run
{
    class Program
    {
        static void Main(string[] args)
        {
            Dog dog1 = new Dog { Id = 22, Name = "Rex", Age = 3, Description = "Rex is strong!"};
            Cat cat1 = new Cat { };

            SmartMapper.Mapper.AutoLoad(dog1, cat1);

            Console.WriteLine($"dog1: Id:{dog1.Id}, Name:{dog1.Name}, Age:{dog1.Age}, Description:{dog1.Description}");
            Console.WriteLine($"cat1: Id:{cat1.Id}, Name:{cat1.Name}, Age:{cat1.Age}, Observation:{cat1.Observation}");

            //////////////////

            Dog dogXml = new Dog { Id = 42, Name = "Hunt", Age = 11, Description = "Hunt is dog in german!" };
            Cat catXml = new Cat { };

            XElement dataSample = XElement.Load(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "DataSample.xml"));

            var dados = dataSample.Elements().ToList();

            SmartMapper.Mapper.LoadFromXML(dogXml, nameof(dogXml.Id), dataSample, x => x.Elements().FirstOrDefault().Attributes().FirstOrDefault(f => f.Name == "id"));
            SmartMapper.Mapper.LoadFromXML(dogXml, nameof(dogXml.Name), dataSample, x => x.Elements().FirstOrDefault().Elements().FirstOrDefault(f => f.Name == "name"));
            SmartMapper.Mapper.LoadFromXML(dogXml, nameof(dogXml.Age), dataSample, x => x.Elements().FirstOrDefault().Elements().FirstOrDefault(f => f.Name == "age"));
            SmartMapper.Mapper.LoadFromXML(dogXml, nameof(dogXml.Description), dataSample, x => x.Elements().FirstOrDefault().Elements().FirstOrDefault(f => f.Name == "description"));

            SmartMapper.Mapper.LoadFromXML(catXml, nameof(catXml.Id), dataSample, x => x.Elements().FirstOrDefault().Attributes().FirstOrDefault(f => f.Name == "id"));
            SmartMapper.Mapper.LoadFromXML(catXml, nameof(catXml.Name), dataSample, x => x.Elements().FirstOrDefault().Elements().FirstOrDefault(f => f.Name == "name"));
            SmartMapper.Mapper.LoadFromXML(catXml, nameof(catXml.Age), dataSample, x => x.Elements().FirstOrDefault().Elements().FirstOrDefault(f => f.Name == "age"));
            SmartMapper.Mapper.LoadFromXML(catXml, nameof(catXml.Observation), dataSample, x => x.Elements().FirstOrDefault().Elements().FirstOrDefault(f => f.Name == "description"));

            Console.ReadKey();
        }
    }

    public class Dog {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public int Thing { get; set; }
    }

    public class Cat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Observation { get; set; }
        public string Thing { get; set; }
    }
}
