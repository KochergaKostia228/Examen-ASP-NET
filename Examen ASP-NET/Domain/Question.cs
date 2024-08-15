using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen_ASP_NET.Domain
{
    public class Question
    {

        public Question() 
        {
            Answers = new List<Answer>();
        }   
        public int Id { get; set; }
        public string Text { get; set; }
        public string? Image { get; set; }

        public ICollection<Answer>? Answers { get; set; }

    }
}
