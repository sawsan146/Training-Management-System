using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingSys.Core.Entities
{
    public class Session:BaseEntity<int>
    {

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course Course { get; set; }

        public ICollection<Grade> Grades { get; set; }

    }
}
