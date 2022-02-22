using System.Collections.Generic;

namespace Telegram.Bot.Tests.Integ.Framework.Fixtures;

public class EntitiesFixture<TEntity>
{
    public List<TEntity> Entities { get; set; }
}