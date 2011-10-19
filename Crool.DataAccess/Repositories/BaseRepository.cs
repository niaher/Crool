namespace Crool.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class BaseRepository<T> where T : class
    {
        public BaseRepository(CroolContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<T>();
        }

        protected BaseRepository()
            : this(new CroolContext())
        {
        }

        private CroolContext Context { get; set; }
        private IDbSet<T> DbSet { get; set; }

        public IList<T> GetMany(Expression<Func<T, bool>> where)
        {
            return this.DbSet.Where(where).ToList();
        }

        public IList<T> GetAll()
        {
            return this.DbSet.ToList();
        }
    }
}
