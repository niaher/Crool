namespace Crool.Domain.Entities
{
    using System;

    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime PostedOn { get; set; }
        public User PostedBy { get; set; }
        public int Review_Id { get; set; }
    }
}
