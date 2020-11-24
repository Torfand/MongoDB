using System;
using System.Reflection;

public class Animal
{
    public long Id;
	public string Name;
	public int Age;
	public string Happiness;
    public string Type;

    public Animal(long id, string name, int age, string happiness, string type)
    {
        Id = id;
        Name = name;
        Age = age;
        Happiness = happiness;
        Type = type;
    }
}
