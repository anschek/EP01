using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfessionalAdvancementCourses.Models
{    public partial class Teacher
    {
        public override string ToString()
        {
            return this.IdNavigation.ToString();
        }
    }

}
