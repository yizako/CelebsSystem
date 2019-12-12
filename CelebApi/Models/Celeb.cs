using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CelebsApi.Models
{
    public class Celeb
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string birthDate;
        public string BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        private string gender = "Male";
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        private string role;
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
        private string image;
        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        private string htmlPath;
        public string HtmlPath
        {
            get { return htmlPath; }
            set { htmlPath = value; }
        }
    }

    public enum Gender
    {
        MALE, FEMALE
    }
}