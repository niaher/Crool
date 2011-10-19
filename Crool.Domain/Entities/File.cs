namespace Crool.Domain.Entities
{
    using System.Collections.Generic;

    public class File
    {
        public File()
        {
            this.Reviews = new List<Review>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }
        public int Project_Id { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual Project Project { get; set; }
    }
}
