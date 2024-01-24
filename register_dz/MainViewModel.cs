using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace register_dz
{
    public class MainViewModel : INotifyPropertyChanged
    {
        static int id = 0;

        string name = "";
        int age;

        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        public BindingList<Person> People { get; }

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Age
        {
            get => age;
            set
            {
                if (age != value)
                {
                    age = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainViewModel()
        {
            People = new()
            {
                new Person { Id=++id, Name = "Someone", Age = 7},
                new Person { Id=++id, Name = "Unknown", Age = 15},
                new Person { Id=++id, Name = "No name", Age = 29},
            };

            AddCommand = new MainCommand( _ =>
            {
                if (this.Name != "")
                {
                    People.Add(new Person { Id = ++id, Name = this.Name, Age = this.Age });
                    Name = ""; Age = 0;
                }
            });
            RemoveCommand = new MainCommand( obj =>
            {
                if(obj is int id)
                {
                    var person = People.FirstOrDefault(p => p.Id == id);
                    if(person != null) { People.Remove(person); }
                }
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}