using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userList = new UserList();
            var taskList = new TaskList();
            
            while (true)
            {
                Console.WriteLine("Для входа в систему укажите логин и пароль!");
                Console.Write("Логин: ");
                string login = Console.ReadLine();
                Console.Write("Пароль: ");
                string pass = Console.ReadLine();


                var user = userList.Authenticate(login, pass);
                if (user != null)
                {
                    Console.WriteLine($"Добро пожаловать, {user.Role} {user.FIO}!");
                    System.Threading.Thread.Sleep(1500);
                    Console.WriteLine("Загрузка системы");
                    System.Threading.Thread.Sleep(1500);
                    Console.Clear();

                    if (user.Role == Role.Управляющий)
                    {
                        while (true)
                        {
                            Console.WriteLine("1. Создать задачу\n2. Зарегистровавать сотрудника\n3. Выход");
                            Console.Write("Выберите: ");
                            int managerChoice = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();

                            if (managerChoice == 1)
                            {
                                Console.Write("Введите название задачи: ");
                                var nameTask = Console.ReadLine();
                                Console.Write("Введите описание задачи: ");
                                var description = Console.ReadLine();
                                Console.Write("Назначить задачу на пользователя: ");
                                int assignedTo = Convert.ToInt32(Console.ReadLine());

                                taskList.CreateTask(new Task
                                {
                                    Name = nameTask,
                                    Description = description,
                                    Status = null,
                                    AssignedTo = assignedTo
                                });

                                Console.WriteLine("Задача создана.");
                            }
                            else if (managerChoice == 2)
                            {
                                Console.Write("Введите ФИО пользователя: ");
                                var fio = Console.ReadLine();
                                Console.Write("Введите логин: ");
                                var loginUser = Console.ReadLine();
                                Console.Write("Введите пароль: ");
                                var password = Console.ReadLine();

                                    try
                                    {
                                        userList.RegisterUser(fio, loginUser, password);
                                        Console.WriteLine("Пользователь зарегистрирован.");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    Console.WriteLine();
                                    }
                            }
                            else break;
                        }
                    }
                    else
                    {
                        while (true)
                        {
                            Console.WriteLine("1. Просмотреть задачи\n2. Изменить статус задачи\n3. Выход");
                            Console.Write("Выберите: ");
                            int employeeChoice = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();

                            if (employeeChoice == 1)
                            {
                                var tasks = taskList.GetTasksByUser(user.ID);
                                foreach (var task in tasks)
                                {
                                    Console.WriteLine($"{task.ID}: {task.Name} - {task.Status}");
                                }
                                Console.WriteLine();
                                
                            }
                            else if (employeeChoice == 2)
                            {
                                Console.Write("Введите ID задачи для изменения статуса: ");
                                if (int.TryParse(Console.ReadLine(), out int taskProjectID))
                                {
                                    Console.Write("Введите новый статус (To do, In Progress, Done): ");
                                    var status = Console.ReadLine();
                                    taskList.UpdateTaskStatus(taskProjectID, status);
                                }
                                else
                                {
                                    Console.WriteLine("Некорректный ID задачи.");
                                }
                                Console.WriteLine();
                            }
                            else break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Неверный логин или пароль.\nПовторите попытку через 3 секунды..");
                    System.Threading.Thread.Sleep(3000);
                    Console.Clear();
                }
            }
        }
    }
}
