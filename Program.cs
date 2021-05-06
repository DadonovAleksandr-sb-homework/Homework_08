using System;
using System.Collections.Generic;

namespace Homework_08
{
    class Program
    {
        static void Main(string[] args)
        {
            // Цель домашнего задания
            // Научиться:
            //  - свободно пользоваться структурами и свойствами,
            //  - сохранять данные в форматах JSON и XML.
            //  - Закрепить знания рекурсии.
            
            // Что нужно сделать
            // Создайте прототип информационной системы для организации.
            // Организация состоит из департаментов. В департаментах работают сотрудники.
            // Помимо этого, каждый департамент содержит вложенные департаменты, в которых также работают сотрудники, и так далее.
            // Уровень вложенности департаментов не ограничен, однако каждый отдельно взятый департамент может содержать только до миллиона сотрудников.
            
            // Каждый департамент обладает как минимум следующими полями:
            // - Наименование.
            // - Дата создания.
            // - Количество сотрудников.
            
            // Каждый сотрудник обладает как минимум следующими полями:
            // - Фамилия.
            // - Имя.
            // - Возраст.
            // - Департамент, в котором он работает.
            // - Идентификатор.
            // - Размер заработной платы.

            // Пользователь информационной системы может добавлять, удалять и редактировать департаменты и сотрудников в нём.
            // Ещё заказчик хочет, чтобы пользователь мог сортировать сотрудников в выбранном департаменте по нескольким полям.
            // Для сортировки пользователь может выбрать любое поле. Все эти действия пользователь хочет делать с помощью выбора пунктов меню.

            // Все данные организации можно сохранить на диск в форматах JSON или XML.
            
            // Что оценивается
            // - Создана структура для департамента.
            // - Создана структура для сотрудника.
            // - Департамент обладает как минимум следующими полями:
            //   - Наименование.
            //   - Дата создания.
            //   - Количество сотрудников.
            // - Сотрудник обладает как минимум следующими полями:
            //   - Фамилия.
            //   - Имя.
            //   - Возраст.
            //   - Департамент, в котором он работает.
            //   - Идентификатор.
            //   - Размер заработной платы.
            // - Сотрудников и департаменты можно добавлять, удалять и редактировать.
            // - Производится сортировка по произвольному полю.
            // - Реализовано меню.

            Console.WriteLine("Информационная структура предприятия");
            var company = new Company("company");
            InitCompany(ref company);
            ProcessCompany(ref company);
            SaveCompany(ref company);
            Console.ReadLine();
        }

        private static void InitCompany(ref Company company)
        {
            var userAnswer = string.Empty;
            var result = false;
            
            if (company.Load())
            {
                do
                {
                    Console.Write($"Желаете продолжить работу с предприятием {company.Name}? (y/n): ");
                    userAnswer = Console.ReadLine().Trim().ToLower();
                    if (userAnswer == "y")
                    {
                        result = true;
                        break;
                    }

                    if (userAnswer == "n")
                    {
                        break;
                    }
                    Console.WriteLine("Невозможно распознать ответ");
                    
                } while (true);
            }

            if (!result)
            {
                do
                {
                    Console.Write("Введите название компании: ");
                    userAnswer = Console.ReadLine();
                    if (!string.IsNullOrEmpty(userAnswer) && !string.IsNullOrWhiteSpace(userAnswer))
                    {
                        company.Name = userAnswer;
                        company.Departments = new Departments();
                        break;
                    }
                } while (true);
            }
            company.Print();
        }

        private static void ProcessCompany(ref Company company)
        {
            
        }

        private static void ProcessDepartment(Department department)
        {
            department.Print();
            var userAnswer = 0;
            var exit = false;
            do
            {
                Console.Write("Выберете желаемое действие (1-перейти к списку работников; 2-перейти к списку департаментов): ");
                if (int.TryParse(Console.ReadLine().Trim(), out userAnswer))
                {
                    switch (userAnswer)
                    {
                        case 1:
                            WorkersProcess(ref department.Workers, department.Name, );
                            break;
                        case 2:
                            WorkersProcess(ref department.Workers, department.Name, );
                            break;
                            
                            
                    }    
                }
                else
                {
                    Console.WriteLine("Невозможно распознать ответ");
                }
            } while (!exit);
        }

        private static void WorkersProcess(ref Workers workers, string departmentName, int workersCount)
        {
            var userAnwer = 0;
            var exit = false;
            workers.Print();
            do
            {
                Console.Write("Выберете желаемое действие (1-добавить; 2-удалить; 3-редактировать; 4-сортировать; 5-выход): ");
                if (int.TryParse(Console.ReadLine().Trim(), out userAnwer))
                {
                    switch(userAnwer)
                    {
                        case 1: // добавить сотрудника
                            AddWorker(ref workers, departmentName, workersCount);
                            break;
                        case 2: // удалить сотрудника
                            DelWorker(ref workers);
                            break;
                        case 3: // редактировать сотрудника
                            EditWorker(ref workers);
                            break;
                        case 4: // сортировать сотрудников
                            SortWorekers(ref workers);
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
        
        private static void AddWorker(ref Workers workers, string departmentName, int workersCount)
        {
            Worker newWorker = new Worker();
            var userAnswer = string.Empty;

            newWorker.Id = workersCount + 1;
            newWorker.DepartmentName = departmentName;
            
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
            
            workers.Add(newWorker);
        }

        private static void DelWorker(ref Workers workers)
        {
            throw new NotImplementedException();
        }
        
        private static void EditWorker(ref Workers workers)
        {
            throw new NotImplementedException();
        }

        private static void SortWorekers(ref Workers workers)
        {
            throw new NotImplementedException();
        }

        private static void SaveCompany(ref Company company)
        {
            var userAnswer = string.Empty;
            do
            {
                Console.Write("Желаете сохранить внесенные изменения? (y/n): ");
                userAnswer = Console.ReadLine().Trim().ToLower();
                if (userAnswer == "y")
                {
                    company.Save();
                    break;
                }
                if (userAnswer == "n")
                {
                    break;
                }
                Console.WriteLine("Невозможно распознать ответ");
            } while (true);
        }
        
        
    }
}