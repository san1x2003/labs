using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SerializeBasic
{

    public class Zakaz
    {
        public int? Id_post { get; set; }

        public int? Id_sklada { get; set; }

        public int? Id_sotrydnik { get; set; }

        public int? Id_product { get; set; }

        public int? Id_zakaza { get; set; }

        public int? Number_contract { get; set; }

        public DateTime? Date_of_execution { get; set; }

        public string? Adress { get; set; }

        public klient Id_klient { get; set; }

        public Zakaz() { }

        public Zakaz (int id_post, int id_sklada, int id_sotrydnik, int id_product, int id_zakaza, int number_contract, DateTime date_of_execution, string adress)
        {
            Id_post = id_post;
            Id_sklada = id_sklada;
            Id_sotrydnik = id_sotrydnik;
            Id_product = id_product;
            Id_zakaza = id_zakaza;
            Number_contract = number_contract;
            Date_of_execution = date_of_execution;
            Adress = adress;
            

        }
    }
    public class klient
    {
        public int Id_klient { get; set; }
        public string Email { get; set; }
        public string Phone_number { get; set; }
        public string Adress { get; set; }

        public klient() { }

        public klient (int id_klient, string email, string phone_number, string adress)
        {
            Id_klient = id_klient;
            Email = email;
            Phone_number = phone_number;
            Adress = adress;
        }
    }
    public class JsonHandler<T> where T : class
    {
        private string fileName;
        JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };


        public JsonHandler() { }

        public JsonHandler(string fileName)
        {
            this.fileName = fileName;
        }


        public void SetFileName(string fileName)
        {
            this.fileName = fileName;
        }

        public void Write(List<T> list)
        {
            string jsonString = JsonSerializer.Serialize(list, options);

            if (new FileInfo(fileName).Length == 0)
            {
                File.WriteAllText(fileName, jsonString);
            }
            else
            {
                Console.WriteLine("Specified path file is not empty");
            }
        }

        public void Delete()
        {
            File.WriteAllText(fileName, string.Empty);
        }

        public void Rewrite(List<T> list)
        {
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(fileName, jsonString);
        }

        public void Read(ref List<T> list)
        {
            if (File.Exists(fileName))
            {
                if (new FileInfo(fileName).Length != 0)
                {
                    string jsonString = File.ReadAllText(fileName);
                    list = JsonSerializer.Deserialize<List<T>>(jsonString);
                }
                else
                {
                    Console.WriteLine("Specified path file is empty");
                }
            }
        }

        public void OutputJsonContents()
        {
            string jsonString = File.ReadAllText(fileName);

            Console.WriteLine(jsonString);
        }

        public void OutputSerializedList(List<T> list)
        {
            Console.WriteLine(JsonSerializer.Serialize(list, options));
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Zakaz> partsList = new List<Zakaz>();

            JsonHandler<Zakaz> partsHandler = new JsonHandler<Zakaz>("partsFile.json");

            partsList.Add(new Zakaz(12,21,34,25,65,20324,"(2,3,2022)","byryzova_21", new klient(21, "popov@mail.ru", "89913456789", "byryzova_34")));
            partsList.Add(new Zakaz(22, 31, 34, 25, 66, 54344, "(5,2,2022)", "sobornya_54", new klient(21, "solidn@mail.ru", "89975656789", "byryzova_21")));
            partsList.Add(new Zakaz(42, 41, 76, 56, 35, 32334, "(1,5,2022)", "lenina_2",  new klient(21, "vasyliy@mail.ru", "89913454785", "byryzova_6")));
            partsList.Remove(new Zakaz(62, 41, 14, 55, 75, 53324, "(5,1,2022)", "gorkova_6", new klient(21, "tri254@mail.ru", "89975646789", "konova_4")));


            partsHandler.Rewrite(partsList);
            partsHandler.OutputJsonContents();
        }

    }

}