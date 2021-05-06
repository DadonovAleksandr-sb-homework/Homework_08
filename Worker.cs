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
        public string DepartmentName { get; set; }

        /// <summary>
        /// Зарплата
        /// </summary>
        public int Salary { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <param name="firstName">Имя</param>
        /// <param name="lastName">Фамилия</param>
        /// <param name="birthdate">Дата рождения</param>
        /// <param name="departmentName">Наименование департамента</param>
        /// <param name="salary">Зарплата</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Worker(int id, string firstName, string lastName, DateTime birthdate, string departmentName, int salary)
        {
            Id = id;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Birthdate = birthdate;
            DepartmentName = departmentName ?? throw new ArgumentNullException(nameof(firstName));
            Salary = salary;
        }

    }
}