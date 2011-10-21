namespace Crool.Business.Managers
{
    using System.Collections.Generic;
    using Crool.DataAccess;
    using Crool.Domain.Entities;

    public class CroolProjectManager : BaseManager
    {
        public CroolProjectManager(CroolContext context)
            : base(context)
        {
        }

        public CroolProjectManager()
        {
        }

        public IList<CroolProject> GetAll()
        {
            return this.CroolProjectRepository.GetAll();
        }
    }
}
