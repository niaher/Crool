namespace Crool.Business.Managers
{
    using Crool.DataAccess;
    using Crool.DataAccess.Repositories;

    public class BaseManager
    {
        public BaseManager(CroolContext context)
        {
            this.Context = context;

            this.ReviewRepository = new ReviewRepository(context);
            this.CroolProjectRepository = new CroolProjectRepository(context);
        }

        public BaseManager()
            : this(new CroolContext())
        {
        }

        protected ReviewRepository ReviewRepository { get; set; }
        protected CroolProjectRepository CroolProjectRepository { get; set; }

        private CroolContext Context { get; set; }
    }
}
