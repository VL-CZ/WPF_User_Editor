using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_MVVM_2_.Models
{
    class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public int Score { get; set; }

        public User() { }

        public User(string name, int age, int score)
        {
            Name = name;
            Age = age;
            Score = score;
        }
    }
}
