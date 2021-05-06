using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Homework_08
{
    public struct Company
    {
        public string Name { get; set; }
        public Departments Departments;
        public int WorkerCount => GetWorkerCount(Departments);

        private string _fileName;

        public Company(string fileName)
        {
            Name = string.Empty;
            Departments = new Departments();
            Departments.Initialization();
            _fileName = fileName;
        }
        
        public Company(string name, string fileName)
        {
            Name = name;
            Departments = new Departments();
            Departments.Initialization();
            _fileName = fileName;
        }
        
        public void AddDepartment(string name)
        {
            Departments.Add(name);
        }
        
        private int GetWorkerCount(Departments departments)
        {
            var count = 0;
            for (int i = 0; i < departments.Count; i++)
            {
                count += departments[i].WorkerCount;
                count += GetWorkerCount(departments[i].Departments);
            }
            return count;
        }

        public void Print()
        {
            Console.WriteLine($"Компания: {Name}");
            for (int i = 0; i < Departments.Count; i++)
            {
                Departments[i].Print();
            }
        }

        public bool Load()
        {
            
            if(!File.Exists(_fileName))
                return false;
            // JSON
            string jsonStr = File.ReadAllText(_fileName, Encoding.UTF8);
            var company = JsonConvert.DeserializeObject<Company>(jsonStr);
            Name = company.Name;
            Departments = company.Departments;
            return true;
            // XML
        }

        public void Save()
        {
            // JSON
            string jsonStr = JsonConvert.SerializeObject(this);
            File.WriteAllText(_fileName, jsonStr, Encoding.UTF8);

        }
        
    }
}