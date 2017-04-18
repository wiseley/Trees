using System.Collections.Generic;
using Trees.Models.EventFilters;
using Trees.Models.Stateful;

namespace Trees.Models.Events
{
    /// <summary>
    /// Supports an event killing plantings on the table via several possible filter combinations
    /// </summary>
    public abstract class PlantingFilterBaseEvent : BaseEvent
    {

        /// <summary>
        /// Default event constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        public PlantingFilterBaseEvent(string name, string description) : base(name, description){ }

        /// <summary>
        /// List of filters that specify what plantings should be killed
        /// </summary>
        public List<IFilter> Filters { get; set; } = new List<IFilter>();

        /// <summary>
        /// Applies all the filters to the table and returns the results for the child event to act on
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public List<Planting> RunFilters(Table table)
        {
            // flatten all plantings into list for filtering
            List<Planting> plantings = new List<Planting>();
            foreach (Grove grove in table.Groves)
            {
                plantings.AddRange(grove.Plantings);
            }
            // run filters
            Player currentPlayer = table.GetCurrentPlayer();
            foreach (IFilter filter in Filters)
            {
                filter.Filter(currentPlayer, plantings);
            }
            return plantings;
        }

        /// <summary>
        /// Specifies a Land filter for single land
        /// </summary>
        /// <param name="landName">Land.Name to match on</param>
        /// <param name="matches">Use false to match plantings without this land instead of with this land</param>
        /// <returns></returns>
        public BaseEvent WhereLand(string landName, bool matches = true)
        {
            Filters.Add(new LandFilter(new string[] { landName }, matches));
            return this;
        }
        /// <summary>
        /// Specifies a Land filter for multiple lands
        /// </summary>
        /// <param name="landName"></param>
        /// <param name="matches"></param>
        /// <returns></returns>
        public BaseEvent WhereLand(string[] landNames, bool matches = true)
        {
            Filters.Add(new LandFilter(landNames, matches));
            return this;
        }

        /// <summary>
        /// Specifies a filter on a habitat value
        /// </summary>
        /// <param name="field">Habitat field to filter on</param>
        /// <param name="condition">Operator to apply in filter</param>
        /// <param name="value">Value to apply in filter</param>
        /// <returns></returns>
        public BaseEvent WhereHabitat(HabitatField field, Conditions.Operators op, int value)
        {
            Filters.Add(new HabitatFilter(field, op, value));
            return this;
        }

        /// <summary>
        /// Specifies a filter on the lowest or highest N habitat values
        /// </summary>
        /// <param name="field">Habitat field to filter on</param>
        /// <param name="value">Negative filters on lowest N values, positive filters on highest N values</param>
        /// <returns></returns>
        public BaseEvent WhereHabitat(HabitatField field, int value)
        {
            Filters.Add(new HabitatSuperlativeFilter(field, value));
            return this;
        }

        /// <summary>
        /// Specifies a filter on a damage value
        /// </summary>
        /// <param name="field">Damage field to filter on</param>
        /// <param name="op">Operator to apply in filter</param>
        /// <param name="value">Value to apply in filter</param>
        /// <returns></returns>
        public BaseEvent WhereDamage(DamageField field, Conditions.Operators op, int value)
        {
            Filters.Add(new DamageFilter(field, op, value));
            return this;
        }

        /// <summary>
        /// Specifies a filter on the lowest or highest N damage values
        /// </summary>
        /// <param name="field">Damage field to filter on</param>
        /// <param name="value">Negative filters on lowest N values, positive filters on highest N values</param>
        /// <returns></returns>
        public BaseEvent WhereDamage(DamageField field, int value)
        {
            Filters.Add(new DamageSuperlativeFilter(field, value));
            return this;
        }

        /// <summary>
        /// Specifies a filter on a planting's habitat match score
        /// </summary>
        /// <param name="op">Operator to apply in filter</param>
        /// <param name="value">Value to apply in filter</param>
        /// <returns></returns>
        public BaseEvent WhereHabitatScore(Conditions.Operators op, int value)
        {
            Filters.Add(new HabitatScoreFilter(op, value));
            return this;
        }

        /// <summary>
        /// Specifies a filter on the lowest or highest N planting scores
        /// </summary>
        /// <param name="value">Negative filters on lowest N values, positive filters on highest N values</param>
        /// <returns></returns>
        public BaseEvent WhereHabitatScore(int value)
        {
            Filters.Add(new HabitatScoreSuperlativeFilter(value));
            return this;
        }

        /// <summary>
        /// Specifies a superlative filter on a planting's age
        /// </summary>
        /// <param name="value">Positive number for the oldest N, negative for the youngest N</param>
        /// <returns></returns>
        public BaseEvent WhereAge(int value)
        {
            Filters.Add(new AgeSuperlativeFilter(value));
            return this;
        }

        /// <summary>
        /// Specifies a filter on the Genus of a planting
        /// </summary>
        /// <param name="genus"></param>
        /// <param name="matches">Use False to filter on plantings without this genus</param>
        /// <returns></returns>
        public BaseEvent WhereGenus(string genus, bool matches = true)
        {
            Filters.Add(new GenusFilter(new string[] { genus }, matches));
            return this;
        }
        public BaseEvent WhereGenus(string[] genus, bool matches = true)
        {
            Filters.Add(new GenusFilter(genus, matches));
            return this;
        }

    }
}