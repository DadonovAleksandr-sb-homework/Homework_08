using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Homework_08
{
    /// <summary>
    /// Методы сортировки департаментов
    /// </summary>
    public enum DepartmentSortMode { Name, CreateDate, WorkerCount}
    /// <summary>
    /// Методы сортировки сотрудников
    /// </summary>
    public enum WorkerSortMode { FirstName, LastName, Age, Salary}
    /// <summary>
    /// Структура компании
    /// </summary>
    public struct Company
    {
        /// <summary>
        /// Наименование компании
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Список департаментов
        /// </summary>
        public List<Department> Departments;
        /// <summary>
        /// Список сотрудников
        /// </summary>
        public List<Worker> Workers;
        /// <summary>
        /// Файловое хранилище
        /// </summary>
        private string _fileName;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="fileName"></param>
        public Company(string fileName)
        {
            Name = string.Empty;
            Departments = new List<Department>();
            Workers = new List<Worker>();
            
            _fileName = fileName;
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fileName"></param>
        public Company(string name, string fileName)
        {
            Name = name;
            Departments = new List<Department>();
            Workers = new List<Worker>();
            
            _fileName = fileName;
        }
        /// <summary>
        /// Очистка данных о компании
        /// </summary>
        public void Clear()
        {
            Name = string.Empty;
            Departments = new List<Department>();
            Workers = new List<Worker>();
        }
        /// <summary>
        /// Вывод информации о департаменте
        /// </summary>
        /// <param name="department"></param>
        public void PrintDepartment(string department)
        {
            int index = Departments.FindIndex(x => x.Name == department);
            if(index<0)
                return;
            Console.WriteLine($"Департамент {Departments[index].Name}:");
            PrintWorkers(department);
            PrintDepartments(department);
        }
        /// <summary>
        /// Вывод информации о списке сотрудников
        /// </summary>
        /// <param name="department"></param>
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
        /// <summary>
        /// Вывод информации о списке сотрудников в упорядоченном виде
        /// </summary>
        /// <param name="department"></param>
        /// <param name="sortMode"></param>
        public void PrintWorkers(string department, WorkerSortMode sortMode)
        {
            Console.WriteLine("Сотрудники:");
            var workers= Workers.Select(x=>x).Where(x=>x.Department==department);;
            switch (sortMode)
            {
                case WorkerSortMode.FirstName:
                    workers = workers.OrderBy(x => x.FirstName);
                    break;
                case WorkerSortMode.LastName:
                    workers = workers.OrderBy(x => x.LastName);
                    break;
                case WorkerSortMode.Age:
                    workers = workers.OrderBy(x=>x.Age);
                    break;
                case WorkerSortMode.Salary:
                    workers = workers.OrderBy(x=>x.Salary);
                    break;
            }
            int i = 0;
            foreach (var worker in workers)
            {
                worker.Print(++i);
            }
            if(i == 0)
            {
                Console.WriteLine("\tИнформация отсутсвует");
            }
        }
        /// <summary>
        /// Вывод информации о списке департаментов
        /// </summary>
        /// <param name="department"></param>
        public void PrintDepartments(string department)
        {
            Console.WriteLine("Вложенные департаменты:");
            var deps = Departments.Select(x=>x).Where(x=>x.ParentDepartment==department);
            int i = 0;
            foreach (var dep in deps)
            {
                dep.Print(++i);
            }
            if(i == 0)
            {
                Console.WriteLine("\tИнформация отсутсвует");
            }
        }
        /// <summary>
        /// Вывод информации о списке департаментов в упорядоченном виде
        /// </summary>
        /// <param name="department"></param>
        /// <param name="sort"></param>
        public void PrintDepartments(string department, DepartmentSortMode sort)
        {
            Console.WriteLine("Вложенные департаменты:");
            var deps= Departments.Select(x=>x).Where(x=>x.ParentDepartment==department);;
            switch (sort)
            {
                case DepartmentSortMode.Name:
                    deps = deps.OrderBy(x => x.Name);
                    break;
                case DepartmentSortMode.CreateDate:
                    deps = deps.OrderBy(x => x.CreateDate);
                    break;
                case DepartmentSortMode.WorkerCount:
                    deps = deps.OrderBy(x=>x.WorkerCount);
                    break;
            }
            int i = 0;
            foreach (var dep in deps)
            {
                dep.Print(++i);
            }
            if(i == 0)
            {
                Console.WriteLine("\tИнформация отсутсвует");
            }
        }
        /// <summary>
        /// Вывод информации о компании
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"Компания: {Name}");
            PrintDepartments(Name);
        }
        /// <summary>
        /// Процесс работы с компанией
        /// </summary>
        /// <param name="department"></param>
        public void Process(string department)
        {
            PrintDepartment(department);
            var userAnwer = string.Empty;
            var exit = false;
            do
            {
                Console.Write("Выберете желаемое действие (1-перейти к списку работников; 2-перейти к списку департаментов; 3-перейти к вышестоящему департаменту; exit-выход): ");
                userAnwer = Console.ReadLine().Trim();
                switch(userAnwer)
                {
                    case "1":
                        WorkersProcess(department);
                        break;
                    case "2":
                        DepartmentsProcess(department);
                        break;
                    case "3":
                        if (department == Name)
                        {
                            Console.WriteLine("Вы уже находитесь в корневом элементе (уровень компании)");
                            break;
                        }
                        var curDepIndex = Departments.IndexOf(Departments.SingleOrDefault(x => x.Name == department));
                        if(curDepIndex<0)
                        {
                            Console.WriteLine($"Департамент {department} отсутствует в системе");
                            break;
                        }
                        Process(Departments[curDepIndex].ParentDepartment);
                        break;
                    case "exit": // выход
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невозможно распознать ответ");
                        break;
              
                }
            } while (!exit);
        }
        /// <summary>
        /// Процесс работы со списком департаментов
        /// </summary>
        /// <param name="department"></param>
        private void DepartmentsProcess(string department)
        {
            PrintDepartments(department);
            var userAnwer = string.Empty;
            var exit = false;
            List<Department> deps;
            do
            {
                Console.Write("Выберете желаемое действие (1-добавить; 2-удалить; 3-редактировать; 4-сортировать; 5-перейти к департаменту; exit-выход): ");
                userAnwer = Console.ReadLine();
                deps= Departments.Where(x=>x.Name==department).ToList();
                switch(userAnwer)
                {
                    case "1": // добавить
                        AddDepartment(department);
                        break;
                    case "2": // удалить
                        if (deps.Count == 0)
                        {
                            Console.WriteLine($"В департаменте {department} нет вложенных департаментов.");
                            break;
                        }
                        DelDepartment(department);
                        break;
                    case "3": // редактировать
                        if (deps.Count == 0)
                        {
                            Console.WriteLine($"В департаменте {department} нет вложенных департаментов.");
                            break;
                        }
                        EditDepartment(department);
                        break;
                    case "4": // сортировать
                        if (deps.Count == 0)
                        {
                            Console.WriteLine($"В департаменте {department} нет вложенных департаментов.");
                            break;
                        }
                        SortDepartment(department);
                        break;
                    case "5": // перейти к департаменту
                        GoToDepartment(department);
                        break;
                    case "exit": // выход
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невозможно распознать ответ");
                        break;
                }  
            } while (!exit);
        }
        /// <summary>
        /// Процесс работы со списком работников
        /// </summary>
        /// <param name="department"></param>
        private void WorkersProcess(string department)
        {
            PrintWorkers(department);
            var userAnwer = string.Empty;
            var exit = false;
            List<Worker> workers;
            do
            {
                Console.Write("Выберете желаемое действие (1-добавить; 2-удалить; 3-редактировать; 4-сортировать; exit-выход): ");
                userAnwer = Console.ReadLine();
                workers= Workers.Select(x=>x).Where(x=>x.Department==department).ToList();
                switch(userAnwer)
                {
                    case "1": // добавить
                        AddWorker(department);
                        break;
                    case "2": // удалить
                        if (workers.Count == 0)
                        {
                            Console.WriteLine($"В департаменте {department} нет сотрудников.");
                            break;
                        }
                        DelWorker(department);
                        break;
                    case "3": // редактировать
                        if (workers.Count == 0)
                        {
                            Console.WriteLine($"В департаменте {department} нет сотрудников.");
                            break;
                        }
                        EditWorker(department);
                        break;
                    case "4": // сортировать
                        if (workers.Count == 0)
                        {
                            Console.WriteLine($"В департаменте {department} нет сотрудников.");
                            break;
                        }
                        SortWorekers(department);
                        break;
                    case "exit": // выход
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невозможно распознать ответ");
                        break;
                }
            } while (!exit);
        }
        /// <summary>
        /// Добавление департамента
        /// </summary>
        /// <param name="parentDepartment"></param>
        private void AddDepartment(string parentDepartment)
        {
            Department newDepartment = new Department();
            var userAnswer = string.Empty;
            do
            {
                Console.Write("Введите наименование департамента (exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                if (userAnswer == "exit")
                {
                    return;
                }
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
        /// <summary>
        /// Удаление департамента
        /// </summary>
        /// <param name="department"></param>
        private void DelDepartment(string department)
        {
            var userAnswer = string.Empty;
            do
            {
                Console.Write("Введите наименование департамента для его удаления (exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                if (userAnswer == "exit")
                {
                    return;
                }
                var deps= Departments.Select(x=>x).Where(x=>x.Name==department).ToList();
                if (deps.Count == 0)
                {
                    Console.WriteLine($"В департаменте {department} нет вложенных департаментов.");
                    return;
                }
                if (!string.IsNullOrEmpty(userAnswer) && !string.IsNullOrWhiteSpace(userAnswer))
                {
                    var depIndex = Departments.IndexOf(Departments.SingleOrDefault(x => x.Name == userAnswer && x.ParentDepartment == department));
                    if (depIndex < 0)
                    {
                        Console.WriteLine($"Департамент {userAnswer} не найден");
                        continue;
                    }
                    var dep = Departments[depIndex];
                    Departments.RemoveAt(depIndex);
                    Console.Write($"Удален департамент: ");
                    dep.Print();
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
        }
        /// <summary>
        /// Редактирование департамента
        /// </summary>
        /// <param name="department"></param>
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
                Console.Write("Введите новое название для департамента (exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                if (userAnswer == "exit")
                {
                    return;
                }
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
        /// <summary>
        /// Сортировка списка департаментов
        /// </summary>
        /// <param name="department"></param>
        private void SortDepartment(string department)
        {
            var userAnswer = string.Empty;
            var exit = false;
            do
            {
                Console.Write($"Введите параметр для сортировки (1-наименование, 2-дата создания, 3-кол-во сотрудников, exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                switch (userAnswer)
                {
                    case "1":
                        PrintDepartments(department, DepartmentSortMode.Name);
                        exit = true;
                        break;
                    case "2":
                        PrintDepartments(department, DepartmentSortMode.CreateDate);
                        exit = true;
                        break;
                    case "3":
                        PrintDepartments(department, DepartmentSortMode.WorkerCount);
                        exit = true;
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невозможно распознать ответ");
                        break;
                }
            } while (!exit);
        }
        /// <summary>
        /// Переход к департаменту по имени
        /// </summary>
        /// <param name="department"></param>
        private void GoToDepartment(string department)
        {
            var userAnswer = string.Empty;
            var depIndex = -1;
            do
            {
                Console.Write("Введите наименование департамента (exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                if (userAnswer == "exit")
                {
                    return;
                }
                depIndex = Departments.IndexOf(Departments.SingleOrDefault(x => x.Name == userAnswer));
                if (depIndex>=0)
                {
                    Process(Departments[depIndex].Name);
                    break;
                }
                Console.WriteLine($"Невозможно найти департамент {userAnswer}");
            } while (true);
        }
        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="department"></param>
        public void AddWorker(string department)
        {
            Worker newWorker = new Worker();
            var userAnswer = string.Empty;

            newWorker.Id = Workers.Count + 1;
            newWorker.Department = department;
            
            do
            {
                Console.Write("Введите имя сотрудника (exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                if (userAnswer == "exit")
                {
                    return;
                }
                if (!string.IsNullOrEmpty(userAnswer) && !string.IsNullOrWhiteSpace(userAnswer))
                {
                    newWorker.FirstName = userAnswer;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            do
            {
                Console.Write("Введите фамилию сотрудника (exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                if (userAnswer == "exit")
                {
                    return;
                }
                if (!string.IsNullOrEmpty(userAnswer) && !string.IsNullOrWhiteSpace(userAnswer))
                {
                    newWorker.LastName = userAnswer;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            do
            {
                Console.Write("Введите дату рождения сотрудника (exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                if (userAnswer == "exit")
                {
                    return;
                }
                if (DateTime.TryParse(userAnswer, out var bithdate))
                {
                    newWorker.Birthdate = bithdate;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            do
            {
                Console.Write("Введите зарплату сотрудника (exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                if (userAnswer == "exit")
                {
                    return;
                }
                if (int.TryParse(userAnswer, out var salary))
                {
                    newWorker.Salary = salary;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            
            Workers.Add(newWorker);
            var depIndex = Departments.IndexOf(Departments.SingleOrDefault(x => x.Name == department));
            if(depIndex>=0)
            {
                var dep = Departments[depIndex];
                dep.WorkerCount++;
                Departments.RemoveAt(depIndex);
                Departments.Add(dep);
            }
            Console.Write($"Добавлен сотрудник: ");
            newWorker.Print();
        }
        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="department"></param>
        private void DelWorker(string department)
        {
            var userAnswer = string.Empty;
            do
            {
                Console.Write("Введите идентификатор работника для его удаления (exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                if (userAnswer == "exit")
                {
                    return;
                }
                var workers= Workers.Select(x=>x).Where(x=>x.Department==department).ToList();
                if (workers.Count == 0)
                {
                    Console.WriteLine($"В департаменте {department} нет сотрудников.");
                    return;
                }
                if (int.TryParse(userAnswer, out var id))
                {
                    var workerIndex = Workers.IndexOf(Workers.SingleOrDefault(x => x.Id == id && x.Department == department));
                    if (workerIndex < 0)
                    {
                        Console.WriteLine($"Сотрудник с идентификатором {id} не найден");
                    }
                    var worker = Workers[workerIndex];
                    Workers.RemoveAt(workerIndex);
                    var depIndex = Departments.IndexOf(Departments.SingleOrDefault(x => x.Name == department));
                    if(depIndex>=0)
                    {
                        var dep = Departments[depIndex];
                        dep.WorkerCount--;
                        Departments.RemoveAt(depIndex);
                        Departments.Add(dep);
                    }
                    Console.Write($"Удален сотрудник: ");
                    worker.Print();
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
        }
        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        /// <param name="department"></param>
        private void EditWorker(string department)
        {
            var workers= Workers.Select(x=>x).Where(x=>x.Department==department).ToList();
            if (workers.Count == 0)
            {
                Console.WriteLine($"В департаменте {department} нет сотрудников.");
                return;
            }
            var userAnswer = string.Empty;
            var workerIndex = 0;
            do
            {
                Console.Write("Введите идентификационный номер сотрудника (exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                if (userAnswer == "exit")
                {
                    return;
                }
                if (int.TryParse(userAnswer, out var id))
                {
                    workerIndex = Workers.IndexOf(Workers.SingleOrDefault(x => x.Id == id));
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
                Console.Write("Введите имя сотрудника (exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                if (userAnswer=="exit")
                {
                    return;
                }
                if (!string.IsNullOrEmpty(userAnswer) && !string.IsNullOrWhiteSpace(userAnswer))
                {
                    newWorker.FirstName = userAnswer;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            do
            {
                Console.Write("Введите фамилию сотрудника (exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                if (userAnswer=="exit")
                {
                    return;
                }
                if (!string.IsNullOrEmpty(userAnswer) && !string.IsNullOrWhiteSpace(userAnswer))
                {
                    newWorker.LastName = userAnswer;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            do
            {
                Console.Write("Введите дату рождения сотрудника (exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                if (userAnswer=="exit")
                {
                    return;
                }
                if (DateTime.TryParse(userAnswer, out var bithdate))
                {
                    newWorker.Birthdate = bithdate;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            do
            {
                Console.Write("Введите зарплату сотрудника (exit-для выхода): ");
                userAnswer = Console.ReadLine().Trim();
                if (userAnswer=="exit")
                {
                    return;
                }
                if (int.TryParse(userAnswer, out var salary))
                {
                    newWorker.Salary = salary;
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
            
            Workers.Add(newWorker);
        }
        /// <summary>
        /// Сортировка списка сотрудников
        /// </summary>
        /// <param name="department"></param>
        private void SortWorekers(string department)
        {
            var userAnswer = string.Empty;
            var exit = false;
            do
            {
                Console.Write($"Введите параметр для сортировки (1-имя, 2-фамилия, 3-возраст, 4-зарплата, exit-выход): ");
                userAnswer = Console.ReadLine().Trim();
                switch (userAnswer)
                {
                    case "1":
                        PrintWorkers(department, WorkerSortMode.FirstName);
                        exit = true;
                        break;
                    case "2":
                        PrintWorkers(department, WorkerSortMode.LastName);
                        exit = true;
                        break;
                    case "3":
                        PrintWorkers(department, WorkerSortMode.Age);
                        exit = true;
                        break;
                    case "4":
                        PrintWorkers(department, WorkerSortMode.Salary);
                        exit = true;
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невозможно распознать ответ");
                        break;
                }
            } while (!exit);
        }
        /// <summary>
        /// Загрузка информации о компании в формате JSON и XML
        /// </summary>
        /// <returns></returns>
        public bool Load()
        {
            var userAnswer = string.Empty;
            var filePath = string.Empty;
            var exit = false;
            Company company;
            do
            {
                Console.Write("Выберите данные для десериализации(1-json, 2-xml, exit-выход): ");
                userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "1":   // JSON
                        filePath = _fileName + ".json";
                        if (!File.Exists(filePath))
                        {
                            Console.WriteLine($"Файл {filePath} не найден");
                            return false;
                        }
                        string jsonStr = File.ReadAllText(filePath, Encoding.UTF8);
                        company = JsonConvert.DeserializeObject<Company>(jsonStr);
                        Name = company.Name;
                        Departments = company.Departments;
                        Workers = company.Workers;
                        exit = true;
                        break;
                    case "2":   // XML
                        filePath = _fileName + ".xml";
                        if (!File.Exists(filePath))
                        {
                            Console.WriteLine($"Файл {filePath} не найден");
                            return false;
                        }
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Company));
                        using (Stream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            company = (Company)xmlSerializer.Deserialize(fs);
                            Name = company.Name;
                            Departments = company.Departments;
                            Workers = company.Workers;
                        }
                        exit = true;
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Невозможно распознать ответ");
                        break;
                }
            } while (!exit);
            return true;
        }
        /// <summary>
        /// Сохранение информации о компании в формате JSON и XML
        /// </summary>
        public void Save()
        {
            // JSON
            string jsonStr = JsonConvert.SerializeObject(this);
            jsonStr = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(_fileName+".json", jsonStr, Encoding.UTF8);
            // XML
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Company));
            using (Stream fs = new FileStream(_fileName+".xml", FileMode.Create, FileAccess.Write))
            {
                xmlSerializer.Serialize(fs, this);
            }
        }

        
        
        
    }
}