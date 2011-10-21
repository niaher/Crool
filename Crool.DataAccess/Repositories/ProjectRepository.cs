namespace Crool.DataAccess.Repositories
{
    using Crool.Domain.Entities;

    public class CroolProjectRepository : BaseRepository<CroolProject>
    {
        public CroolProjectRepository(CroolContext context)
            : base(context)
        {
        }

        public CroolProjectRepository()
            : base(new CroolContext())
        {
        }
    }
}
