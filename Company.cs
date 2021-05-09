using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Homework_08
{
    public struct Company
    {
        public string Name { get; set; }
        public List<Department> Departments;
        public List<Worker> Workers;
        
        private string _fileName;

        public Company(string fileName)
        {
            Name = string.Empty;
            Departments = new List<Department>();
            Workers = new List<Worker>();
            
            _fileName = fileName;
        }
        
        public Company(string name, string fileName)
        {
            Name = name;
            Departments = new List<Department>();
            Workers = new List<Worker>();
            
            _fileName = fileName;
        }

        public void Clear()
        {
            Name = string.Empty;
            Departments = new List<Department>();
            Workers = new List<Worker>();
        }
        
        public void PrintDepartment(string department)
        {
            int index = Departments.FindIndex(x => x.Name == department);
            if(index<0)
                return;
            Console.WriteLine($"Департамент {Departments[index].Name}:");
            PrintWorkers(department);
            PrintDepartments(department);
        }

        public void PrintWorkers(string department)
        {
            Console.WriteLine("Сотрудники:");
            int i = 0;
            foreach (var worker in Workers)
            {
                if (worker.Department == department)
                {
                    worker.Print(++i);
                }
            }
            if (i == 0)
            {
                Console.WriteLine("\tИнформация отсутсвует");
            }
        }
        
        public void PrintDepartments(string department)
        {
            Console.WriteLine("Вложенные департаменты:");
            int i = 0;
            foreach (var dep in Departments)
            {
                if (dep.ParentDepartment == department)
                {
                    dep.Print(++i);   
                }
            }
            if (i == 0)
            {
                Console.WriteLine("\tИнформация отсутсвует");
            }
        }
        
        public void Print()
        {
            Console.WriteLine($"Компания: {Name}");
            PrintDepartments(Name);
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
        
        public void Process(string department)
        {
            PrintDepartment(department);
            var userAnwer = 0;
            var exit = false;
            do
            {
                Console.Write("Выберете желаемое действие (1-перейти к списку работников; 2-перейти к списку департаментов; 3-перейти к вышестоящему департаменту; 5-выход): ");
                if (int.TryParse(Console.ReadLine().Trim(), out userAnwer))
                {
                    var curDepIndex = -1;
                    switch(userAnwer)
                    {
                        case 1:
                            // curDepIndex = Departments.IndexOf(Departments.SingleOrDefault(x => x.Name == department));
                            // if(curDepIndex<0)
                            // {
                            //     Console.WriteLine($"Департамент {department} отсутствует в системе");
                            //     break;
                            // }
                            WorkersProcess(department);
                            break;
                        case 2:
                            // curDepIndex = Departments.IndexOf(Departments.SingleOrDefault(x => x.Name == department));
                            // if(curDepIndex<0)
                            // {
                            //     Console.WriteLine($"Департамент {department} отсутствует в системе");
                            //     break;
                            // }
                            DepartmentsProcess(department);
                            break;
                        case 3:
                            if (department == Name)
                            {
                                Console.WriteLine("Вы уже находитесь в корневом элементе (уровень компании)");
                                break;
                            }
                            curDepIndex = Departments.IndexOf(Departments.SingleOrDefault(x => x.Name == department));
                            if(curDepIndex<0)
                            {
                                Console.WriteLine($"Департамент {department} отсутствует в системе");
                                break;
                            }
                            Process(Departments[curDepIndex].ParentDepartment);
                            break;
                        case 5: // выход
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Невозможно распознать ответ");
                            break;
                    }  
                }
            } while (!exit);
        }
        
        private void DepartmentsProcess(string department)
        {
            PrintDepartments(department);
            var userAnwer = 0;
            var exit = false;
            do
            {
                Console.Write("Выберете желаемое действие (1-добавить; 2-удалить; 3-редактировать; 4-сортировать; 5-выход): ");
                if (int.TryParse(Console.ReadLine().Trim(), out userAnwer))
                {
                    switch(userAnwer)
                    {
                        case 1: // добавить
                            AddDepartment(department);
                            break;
                        case 2: // удалить
                            DelDepartment(department);
                            break;
                        case 3: // редактировать
                            EditDepartment(department);
                            break;
                        case 4: // сортировать
                            SortDepartment(department);
                            break;
                        case 5: // выход
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Невозможно распознать ответ");
                            break;
                    }  
                }
            } while (!exit);
        }

        private void WorkersProcess(string department)
        {
            PrintWorkers(department);
            var userAnwer = 0;
            var exit = false;
            do
            {
                Console.Write("Выберете желаемое действие (1-добавить; 2-удалить; 3-редактировать; 4-сортировать; 5-выход): ");
                if (int.TryParse(Console.ReadLine().Trim(), out userAnwer))
                {
                    switch(userAnwer)
                    {
                        case 1: // добавить
                            AddWorker(department);
                            break;
                        case 2: // удалить
                            DelWorker(department);
                            break;
                        case 3: // редактировать
                            EditWorker(department);
                            break;
                        case 4: // сортировать
                            SortWorekers(department);
                            break;
                        case 5: // выход
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Невозможно распознать ответ");
                            break;
                    }  
                }
            } while (!exit);
        }

        private void AddDepartment(string parentDepartment)
        {
            Department newDepartment = new Department();
            var userAnswer = string.Empty;
            do
            {
                Console.Write("Введите наименование департамента: ");
                userAnswer = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(userAnswer) && !string.IsNullOrWhiteSpace(userAnswer))
                {
                    newDepartment.Name = userAnswer;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);

            newDepartment.CreateDate = DateTime.Now.Date;
            newDepartment.ParentDepartment = parentDepartment;
            Departments.Add(newDepartment);
            Console.Write($"Добавлен департамент: ");
            newDepartment.Print();
        }

        private void DelDepartment(string department)
        {
            var userAnswer = string.Empty;
            do
            {
                Console.Write("Введите наименование департамента для его удаления или \"exit\" для выхода: ");
                userAnswer = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(userAnswer) && !string.IsNullOrWhiteSpace(userAnswer))
                {
                    if (userAnswer == "exit" || Departments.Remove(Departments.SingleOrDefault(x => x.Name == department)))
                    {
                        break;    
                    }
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
        }
        
        private void EditDepartment(string department)
        {
            var userAnswer = string.Empty;
            var depIndex = Departments.IndexOf(Departments.SingleOrDefault(x => x.Name == department));
            if (depIndex < 0)
            {
                Console.WriteLine($"Невозможно найти департамент {department}");
                return;
            }   
            do
            {
                Console.Write("Введите новое название для департамента: ");
                userAnswer = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(userAnswer) && !string.IsNullOrWhiteSpace(userAnswer))
                {
                    var oldName = Departments[depIndex].Name;
                    var newDep = Departments[depIndex];
                    Departments.RemoveAt(depIndex);
                    newDep.Name = userAnswer;
                    Departments.Add(newDep);
                    foreach (var dep in Departments)
                    {
                        if (dep.ParentDepartment == oldName)
                        {
                            newDep = dep;
                            Departments.Remove(dep);
                            newDep.ParentDepartment = userAnswer;
                            Departments.Add(newDep);
                        }
                    }
                    Worker newWorker;
                    foreach (var worker in Workers)
                    {
                        if (worker.Department == oldName)
                        {
                            newWorker = worker;
                            Workers.Remove(worker);
                            newWorker.Department = userAnswer;
                            Workers.Add(newWorker);
                        }
                    }
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
        }
        
        private void SortDepartment(string department)
        {
            throw new NotImplementedException();
        }
        
        public void AddWorker(string department)
        {
            Worker newWorker = new Worker();
            var userAnswer = string.Empty;

            newWorker.Id = Workers.Count + 1;
            newWorker.Department = department;
            
            do
            {
                Console.Write("Введите имя сотрудника: ");
                userAnswer = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(userAnswer) && !string.IsNullOrWhiteSpace(userAnswer))
                {
                    newWorker.FirstName = userAnswer;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            do
            {
                Console.Write("Введите фамилию сотрудника: ");
                userAnswer = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(userAnswer) && !string.IsNullOrWhiteSpace(userAnswer))
                {
                    newWorker.LastName = userAnswer;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            do
            {
                Console.Write("Введите дату рождения сотрудника: ");
                userAnswer = Console.ReadLine().Trim();
                if (DateTime.TryParse(userAnswer, out var bithdate))
                {
                    newWorker.Birthdate = bithdate;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            do
            {
                Console.Write("Введите зарплату сотрудника: ");
                userAnswer = Console.ReadLine().Trim();
                if (int.TryParse(userAnswer, out var salary))
                {
                    newWorker.Salary = salary;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            
            Workers.Add(newWorker);
            var depIndex = Departments.IndexOf(Departments.SingleOrDefault(x => x.Name == department));
            Departments[depIndex].WorkerCount++;
            Console.Write($"Добавлен сотрудник: ");
            newWorker.Print();
        }

        private void DelWorker(string department)
        {
            var userAnswer = string.Empty;
            do
            {
                Console.Write("Введите идентификатор работника для его удаления или \"exit\" для выхода: ");
                userAnswer = Console.ReadLine().Trim();
                if (int.TryParse(userAnswer, out var id))
                {
                    Workers.Remove(Workers.Single(x => x.Id == id));
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
        }
        
        private void EditWorker(string department)
        {
            var userAnswer = string.Empty;
            var workerIndex = 0;
            do
            {
                Console.Write("Введите идентификационный номер сотрудника: ");
                userAnswer = Console.ReadLine().Trim();
                if (int.TryParse(userAnswer, out var id))
                {
                    workerIndex = Workers.IndexOf(Workers.Single(x => x.Id == id));
                    if (workerIndex < 0)
                    {
                        Console.WriteLine($"Невозможно найти сотрудника с идентификаторм {id}");
                        return;
                    }   
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);

            var newWorker = Workers[workerIndex];
            Workers.RemoveAt(workerIndex);
            do
            {
                Console.Write("Введите имя сотрудника: ");
                userAnswer = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(userAnswer) && !string.IsNullOrWhiteSpace(userAnswer))
                {
                    newWorker.FirstName = userAnswer;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            do
            {
                Console.Write("Введите фамилию сотрудника: ");
                userAnswer = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(userAnswer) && !string.IsNullOrWhiteSpace(userAnswer))
                {
                    newWorker.LastName = userAnswer;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            do
            {
                Console.Write("Введите дату рождения сотрудника: ");
                userAnswer = Console.ReadLine().Trim();
                if (DateTime.TryParse(userAnswer, out var bithdate))
                {
                    newWorker.Birthdate = bithdate;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            do
            {
                Console.Write("Введите зарплату сотрудника: ");
                userAnswer = Console.ReadLine().Trim();
                if (int.TryParse(userAnswer, out var salary))
                {
                    newWorker.Salary = salary;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            
            Workers.Add(newWorker);
        }
        
        private void SortWorekers(string department)
        {
            throw new NotImplementedException();
        }
        
        
        
        
        
        
        
        
        
        
    }
}