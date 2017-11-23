using System;
namespace DatStore.Model
{
    public class User
    {
        private string name;
        private string surname;
        private string dob;
        private string gender;

        public User()
        {
        }

        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Dob { get => dob; set => dob = value; }
        public string Gender { get => gender; set => gender = value; }
    }
}
