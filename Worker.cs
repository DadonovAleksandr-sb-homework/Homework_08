using System;

namespace Homework_08
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public struct Worker
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public int Age
        {
            get
            {
                var age = DateTime.Now.Year - Birthdate.Year;
                if (DateTime.Now.DayOfYear > Birthdate.DayOfYear)
                    age++;
                return age;
            }
        }

        /// <summary>
        /// Департамент
        /// </summary>
        public string Department;

        /// <summary>
        /// Зарплата
        /// </summary>
        public int Salary { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime Birthdate { get; set; }

        public void Print()
        {
            Console.WriteLine($"{FirstName, 20} {LastName, 30} {Age,5} годиков {Salary} руб");
        }
        
        public void Print(int num)
        {
            Console.WriteLine($"{num, 3} {FirstName, 20} {LastName, 30} {Age,5} годиков {Salary} руб");
        }

    }
}