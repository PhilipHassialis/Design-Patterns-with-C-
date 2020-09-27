using System;
using System.ComponentModel;

namespace ViewModel
{
    // model
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    // view = ui

    // viewmodel
    public class PersonViewModel:INotifyPropertyChanged
    {
        private readonly Person person;

        public PersonViewModel(Person person)
        {
            this.person = person;
        }

        public string FirstName
        {
            get => person.FirstName;
            set
            {
                if (person.FirstName == value) return;
                person.FirstName = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(FirstName)));
            }
        }

        public string LastName
        {
            get => person.LastName;
            set
            {
                if (person.LastName == value) return;
                person.LastName = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(LastName)));
            }

        }

        public string FullName
        {
            get => $"{FirstName} {LastName}".Trim();
            set
            {
                if (value == null) { FirstName = LastName = null; return; }
                var items = value.Split();
                if (items.Length > 0) FirstName = items[0];
                if (items.Length > 1) LastName = items[1];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public override string ToString()
        {
            return $"PersonViewModel for Person {LastName} {FirstName}";
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var pvm = new PersonViewModel(new Person() { FirstName = "Philip", LastName = "Hassialis" });
            pvm.PropertyChanged += (obj, args) =>
            {
                Console.WriteLine(obj.ToString());
            };
            pvm.FirstName = "Philip - Alexander";
        }
    }
}
