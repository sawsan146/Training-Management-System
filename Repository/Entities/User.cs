using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingSys.Core.Entities
{
    public class User:BaseEntity<int>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
        
        public ICollection<Course> Courses { get; set; }
        public ICollection<Grade> Grades { get; set; }

    }
}
