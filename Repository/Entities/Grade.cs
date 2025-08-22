using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingSys.Core.Entities
{
    public class Grade:BaseEntity<int>
    {
        public float Value { get; set; }

        public int  TraineeId { get; set; }

        [ForeignKey(nameof(TraineeId))]
        public User Trainee { get; set; }

        public int  SessionId { get; set; }

        [ForeignKey(nameof(SessionId))]
        public Session Session { get; set; }


    }
}
