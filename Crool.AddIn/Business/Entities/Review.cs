namespace Crool.AddIn.Business.Entities
{
    using System.Collections.Generic;

    public class Review
    {
        public int Id { get; set; }
        public int File_Id { get; set; }
        public int StartLine { get; set; }
        public int EndLine { get; set; }
        public string Text { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool Resolved { get; set; }

        public virtual File File { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
