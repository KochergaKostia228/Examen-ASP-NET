using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_ASP_NET.Domain
{
    public class Result
    {
        public int Id { get; set; }
        public Test? Test { get; set; }
        public User? User { get; set; }
        public int Score { get; set; }

        public DateTime StartTime { get; set; }

        public int Second {  get; set; }

    }
}
