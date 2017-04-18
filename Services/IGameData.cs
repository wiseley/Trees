using Trees.Models;
using Trees.Models.Events;
using Trees.Models.Stateful;

namespace Trees.Services
{
    public interface IGameData
    {
        Deck<Tree> Trees { get; }

        Deck<BaseEvent> Events { get; }

        Deck<Land> Lands { get; }
    }
}
