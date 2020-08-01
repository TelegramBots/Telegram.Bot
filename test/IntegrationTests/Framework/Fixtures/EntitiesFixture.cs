using System.Collections.Generic;

namespace IntegrationTests.Framework.Fixtures
{
    public class EntitiesFixture<TEntity>
    {
        public List<TEntity> Entities { get; set; }
    }
}
