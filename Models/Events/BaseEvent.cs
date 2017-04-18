using System.Collections.Generic;
using Trees.Models.EventFilters;
using Trees.Models.Stateful;
using Trees.Services;

namespace Trees.Models.Events
{
    /// <summary>
    /// Supports an event killing plantings on the table via several possible filter combinations
    /// </summary>
    public abstract class BaseEvent
    {
        /// <summary>
        /// List of filters that specify what plantings should be killed
        /// </summary>
        List<IFilter> _filters = new List<IFilter>();

        /// <summary>
        /// Default event constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public BaseEvent(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Implements any actions to be taken for this event
        /// </summary>
        /// <param name="table"></param>
        public abstract void Execute(GamePlay game, Table table);
        
        /// <summary>
        /// Sets the stage for showing what the event will do, setting flags, etc. Happens on Draw
        /// </summary>
        /// <param name="table"></param>
        public abstract void Stage(GamePlay game, Table table);

        /// <summary>
        /// Specifies a filter on whether planting belongs to the current player or not
        /// </summary>
        /// <param name="current">true to filter on current player</param>
        /// <returns></returns>
        public virtual BaseEvent WherePlayer(bool current)
        {
            _filters.Add(new PlayerFilter(current));
            return this;
        }
    }
}