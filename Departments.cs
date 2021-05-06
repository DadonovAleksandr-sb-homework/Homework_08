using System;
using System.Collections.Generic;

namespace Homework_08
{
    public struct Departments
    {
        /// <summary>
        /// Список департаментов
        /// </summary>
        private List<Department> _departments;
        /// <summary>
        /// Кол-во департаментов
        /// </summary>
        public int Count => _departments.Count;

        public void Initialization()
        {
            _departments = new List<Department>();
        }

        public void Add(string name)
        {
            _departments.Add(new Department(name));
        }
        
        public void Print()
        {
            Console.WriteLine($"Вложенные департаменты: ");
            if (_departments.Count == 0)
            {
                Console.WriteLine("    департаменты отсутствуют");
            }
            for (int i = 0; i < _departments.Count; i++)
            {
                Console.WriteLine($"    {i+1} - {_departments[i].Name}");
            }
        }

        /// <summary>
        /// Индексатор
        /// </summary>
        /// <param name="index"></param>
        public Department this[int index]
        {
            get
            {
                return _departments[index];
            }
            set
            {
                _departments[index] = value;
            }
        }
    }
}