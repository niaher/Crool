namespace Crool.DataAccess.Repositories
{
    using Crool.Domain.Entities;

    public class ReviewRepository : BaseRepository<Review>
    {
        public ReviewRepository(CroolContext context)
            : base(context)
        {
        }

        public ReviewRepository()
            : base(new CroolContext())
        {
        }
    }
}
