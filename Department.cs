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
        public string Name { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Кол-во работников в департаменте
        /// </summary>
        public int WorkerCount { get; set; }
        /// <summary>
        /// Родительский департамент
        /// </summary>
        public string ParentDepartment;
        /// <summary>
        /// Вывод информации о департаменте
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"{Name, 20} {CreateDate.ToShortDateString(), 10} {WorkerCount, 10} чел");
        }
        /// <summary>
        /// Вывод информации о департаменте
        /// </summary>
        /// <param name="num"></param>
        public void Print(int num)
        {
            Console.WriteLine($"{num, 3} {Name, 20} {CreateDate.ToShortDateString(), 10} {WorkerCount, 10} чел");
        }
        

        
        
    }
}