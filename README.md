# SmartMapper
Copy property values from an object to another with a easy way.
  
```
Install-Package SmartMapper
```  
  
Methods
  
-------  
  
- [SmartMapper.Mapper.AutoLoad](#smartmappermapperautoload)  
  
---  
### SmartMapper.Mapper.AutoLoad  

Copy property values from an object to another.  
  
```
class Program
{
    static void Main(string[] args)
    {
        Dog dog1 = new Dog { Id = 22, Name = "Rex", Age = 3, Description = "Rex is strong!"};
        Cat cat1 = new Cat { };

        SmartMapper.Mapper.AutoLoad(dog1, cat1);

        Console.WriteLine($"dog1: Id:{dog1.Id}, Name:{dog1.Name}, Age:{dog1.Age}, Description:{dog1.Description}");
        Console.WriteLine($"cat1: Id:{cat1.Id}, Name:{cat1.Name}, Age:{cat1.Age}, Observation:{cat1.Observation}");

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
```  
Result will be:
```
dog1: Id:22, Name:Rex, Age:3, Description:Rex is strong!
cat1: Id:22, Name:Rex, Age:3, Observation:
```
---
  
### News  
    
- 0.0.1 Created the project, added *SmartMapper.Mapper.AutoLoad* method.
  
Danke  
  