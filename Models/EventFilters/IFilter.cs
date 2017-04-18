using System.Collections.Generic;
using Trees.Models.Stateful;

namespace Trees.Models.EventFilters
{
    /// <summary>
    /// /// Interface for all filter types
    /// </summary>
    public interface IFilter
    {
        void Filter(Player currentPlayer, List<Planting> plantings);
    }

}
