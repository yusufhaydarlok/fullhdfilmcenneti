using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fullhdfilmcenneti_core.Models
{
    public class Comment : BaseEntity
    {
        public string Username { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public Guid MovieId { get; set; }
    }
}
