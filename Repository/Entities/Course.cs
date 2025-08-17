using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingSys.Core.Entities
{
    public class Course:BaseEntity<int>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int InstructorID { get; set; }

        [ForeignKey(nameof(InstructorID))]
        public User Instructor { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
