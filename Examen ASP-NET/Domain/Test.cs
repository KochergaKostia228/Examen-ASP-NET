using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_ASP_NET.Domain
{
    public class Test
    {
        public Test()
        {
            Questions = new List<Question>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Question>? Questions { get; set; }

    }
}
