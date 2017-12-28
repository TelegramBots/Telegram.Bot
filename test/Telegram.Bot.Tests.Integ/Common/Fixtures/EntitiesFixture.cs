using System.Collections.Generic;

namespace Telegram.Bot.Tests.Integ.Common.Fixtures
{
    public class EntitiesFixture<TEntity>
    {
        public List<TEntity> Entities { get; set; }
    }
}
