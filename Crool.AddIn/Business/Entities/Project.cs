namespace Crool.AddIn.Business.Entities
{
    using System.Collections.Generic;

    public class Project
    {
        public Project()
        {
            this.Files = new List<File>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CroolProject_Id { get; set; }

        public ICollection<File> Files { get; set; }
    }
}
