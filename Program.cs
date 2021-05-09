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
                        company.Clear();
                        company.Name = userAnswer;
                        break;
                    }
                } while (true);
            }
            company.Print();
        }

        private static void ProcessCompany(ref Company company)
        {
            company.Process(company.Name);
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