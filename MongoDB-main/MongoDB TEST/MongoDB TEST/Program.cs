using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoDB_TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            //MongoDB
            MongoClient dbClient = new MongoClient("mongodb+srv://Test:Test@cluster0.gj22s.mongodb.net/AnimalDB?retryWrites=true&w=majority");
            var database = dbClient.GetDatabase("Animals_Database");
            var collection = database.GetCollection<Animal>("Animals_Collection");
            //Start
            string animalAmount;
            Console.WriteLine("What would you like to do?");
            var input = Console.ReadLine();
            if (input == "Add") Add(collection);
            if (input == "Print") Print(collection);
            if (input == "List") ListItems(collection);


        }

        private static void ListItems(IMongoCollection<Animal> collection)
        {
            var animals = collection.Find(_ => true).ToList();
            foreach (var item in animals)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Name :" + item.Name);
                Console.WriteLine("Age :" + item.Age);
                Console.WriteLine("Happiness :" + item.Happiness);
                Console.WriteLine("Type :" + item.Type);
                Console.WriteLine("--------------------------------------");
            }
        }

        private static void Print(IMongoCollection<Animal> collection)
        {
            Console.WriteLine("Insert id");
            var input = Console.ReadLine();
            var id = Int32.Parse(input);
            var filter = Builders<Animal>.Filter.Eq("_id", id);
            var result = collection.Find<Animal>(filter).ToList();
            foreach (var item in result)
            {
                Console.WriteLine("Name :" + item.Name);
                Console.WriteLine("Age :" + item.Age);
                Console.WriteLine("Happiness :" + item.Happiness);
                Console.WriteLine("Type :" + item.Type);
            }
        }

        private static void Add(IMongoCollection<Animal> collection)
        {
            string animalAmount;
            Console.WriteLine("How many animals do you want to add?");
            animalAmount = Console.ReadLine();
            int intAnimalAmount = Convert.ToInt32(animalAmount);
            //Loop
            try
            {
                var animalId = collection.EstimatedDocumentCount();
                for (int i = 0; i < intAnimalAmount; i++)
                {
                    animalId++;
                    Console.WriteLine("What type is it?"); //Type
                    string type = Console.ReadLine(); //Temp

                    Console.WriteLine("What is the animals name?"); //Name
                    string name = Console.ReadLine();

                    Console.WriteLine("How old is it?"); //Age
                    string age = Console.ReadLine();
                    int ageInt = Int32.Parse(age);
                    Console.WriteLine("Rate it's happiness from 0-100%"); //Happiness
                    string happiness = Console.ReadLine();

                    var animal = new Animal(animalId, name, ageInt, happiness, type);

                    collection.InsertOne(animal);
                    Console.WriteLine("Animal Added");
                    Console.WriteLine(animal.GetType());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
