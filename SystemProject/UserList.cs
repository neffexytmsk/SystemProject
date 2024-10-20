using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemProject
{
    public enum Role
    {
        Управляющий, Сотрудник
    }
    public class User
    {
        public int ID { get; set; }
        public string FIO {  get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
    public class UserList
    {
        private string PATH = "user.json";
        private List<User> users;

        public UserList()
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            if (File.Exists(PATH))
            {
                var json = File.ReadAllText(PATH);
                users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
            }
            else
            {
                users = new List<User>();
            }
        }

        public User Authenticate(string login, string pass)
        {
            if (login == "admin" && pass == "admin")
            {
                if (!users.Any(u => u.Login == login))
                {
                    users.Add(new User { Login = login, Password = pass, Role = Role.Управляющий });
                    SaveUsers();
                }
                return new User { Login = login, Password = pass, Role = Role.Управляющий };
            }
            return users.FirstOrDefault(x => x.Login == login && x.Password == pass);
        }
        public void RegisterUser(string fio,string login, string pass)
        {
            
            if (users.Any(x => x.Login == login))
                throw new Exception("Пользователь с таким логином уже существует.");
            int newId = users.Count > 0 ? users.Max(user => user.ID) + 1 : 1;
            users.Add(new User {ID = newId, FIO = fio, Login = login, Password = pass, Role = Role.Сотрудник });
            
            SaveUsers();
        }

        public List<User> GetAllUsers() => users;

        private void SaveUsers()
        {
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(PATH, json);
        }
    }
}
