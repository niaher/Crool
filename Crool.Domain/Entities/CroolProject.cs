namespace Crool.Domain.Entities
{
    using System.Collections.Generic;

    public class CroolProject
    {
        public CroolProject()
        {
            this.Projects = new List<Project>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
