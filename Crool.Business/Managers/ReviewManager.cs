namespace Crool.Business.Managers
{
    using System.Collections.Generic;
    using Crool.DataAccess;
    using Crool.Domain.Entities;

    public class ReviewManager : BaseManager
    {
        public ReviewManager(CroolContext context)
            : base(context)
        {
        }

        public ReviewManager()
        {
        }

        public IList<Review> GetByFileId(int fileId)
        {
            return this.ReviewRepository.GetMany(r => r.File_Id == fileId);
        }
    }
}
