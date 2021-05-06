using System;
using System.Collections.Generic;

namespace Homework_08
{
    /// <summary>
    /// Департамент
    /// </summary>
    public struct Department
    {
        /// <summary>
        /// Наименование департамента
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; }

        /// <summary>
        /// Кол-во сотрудников
        /// </summary>
        public int WorkerCount => Workers.Count;
        /// <summary>
        /// Сотрудники
        /// </summary>
        public Workers Workers;
        /// <summary>
        /// Вложенные департаменты
        /// </summary>
        public Departments Departments;

        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Наименование департамента</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Department(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CreateDate = DateTime.Now.Date;

            Workers = new Workers();
            Departments = new Departments();
            
            Workers.Initialization();
            Departments.Initialization();
        }

        public void Print()
        {
            Console.WriteLine($"Департамент: {Name}");
            Workers.Print();
            Departments.Print();
        }

        

        
        
    }
}