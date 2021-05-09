using System;
using System.Collections.Generic;

namespace Homework_08
{
    // public struct Workers
    // {
    //     public const int MAX_WORKERS_IN_DEP = 1_000_000;
    //     /// <summary>
    //     /// Список сотрудников
    //     /// </summary>
    //     private List<Worker> _workers;
    //     /// <summary>
    //     /// Кол-во сотрудников
    //     /// </summary>
    //     public int Count => _workers.Count;
    //
    //     public void Initialization()
    //     {
    //         _workers = new List<Worker>();
    //     }
    //     
    //     public void Add(int id, string firstName, string lastName, DateTime birthdate, string departmentName, int salary)
    //     {
    //         if (_workers.Count >= MAX_WORKERS_IN_DEP)
    //         {
    //             Console.WriteLine($"Кол-во работников в департаменте не может превышать {MAX_WORKERS_IN_DEP}");
    //             return;
    //         }
    //         _workers.Add(
    //             new Worker(
    //                 id,
    //                 firstName, 
    //                 lastName,
    //                 birthdate,
    //                 departmentName,
    //                 salary));
    //     }
    //     
    //     public void Add(Worker worker)
    //     {
    //         if (_workers.Count >= MAX_WORKERS_IN_DEP)
    //         {
    //             Console.WriteLine($"Кол-во работников в департаменте не может превышать {MAX_WORKERS_IN_DEP}");
    //             return;
    //         }
    //         _workers.Add(worker);
    //     }
    //     
    //     public void Print()
    //     {
    //         Console.WriteLine($"Сотрудники: ");
    //         if (_workers.Count == 0)
    //         {
    //             Console.WriteLine("    сотрудники отсутствуют");
    //         }
    //         for (int i = 0; i < _workers.Count; i++)
    //         {
    //             Console.WriteLine($"    {i+1} - {_workers[i].FirstName, 25} {_workers[i].LastName, 50} {_workers[i].Age, 20} лет {_workers[i].Salary, 10} руб");
    //         }
    //     }
    //
    //     public Worker this[int index]
    //     {
    //         get
    //         {
    //             return _workers[index];
    //         }
    //         set
    //         {
    //             _workers[index] = value;
    //         }
    //     }
    //     
    // }
}