using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustOrganize.TeamService.Models
{
    public class Team
    {
        public string Name { get; set; }

        public Guid Id { get; set; }

        public ICollection<Member> Members { get; set; }
        public Team()
        {
            this.Members = new List<Member>();
        }

        public Team(string name):this()
        {
            this.Name = name;
        }

        public Team(string name, Guid id) : this(name)
        {
            this.Name = name;
            this.Id = id;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
