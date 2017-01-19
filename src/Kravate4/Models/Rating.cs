using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kravate4.Models
{
    public class Rating
    {
        public Guid ID { get; set; }

        public Guid PoliticianID { get; set; }

        public virtual Politician Politician { get; set; }

        public Guid UserID { get; set; }

        public short Value { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public Rating()
        {
            ID = new Guid();
        }

        public Rating(short value)
        {
            ID = new Guid();

            Value = value;


        }
    }
}
