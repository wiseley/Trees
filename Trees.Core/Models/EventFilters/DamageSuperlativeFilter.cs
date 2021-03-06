using System;
using System.Collections.Generic;
using Trees.Core.Models.Stateful;

namespace Trees.Core.Models.EventFilters
{
    /// <summary>
    /// Specifies a filter on the higest or lowest damage values
    /// </summary>
    class DamageSuperlativeFilter : IFilter
    {
        /// <param name="field">Damage field to filter on</param>
        /// <param name="value">Positive number for the highest N, negative for the lowest N</param>
        public DamageSuperlativeFilter(DamageField field, int value)
        {
            Field = field; Value = value;
        }
        DamageField Field { get; set; }
        int Value { get; set; }
        public void Filter(Player currentPlayer, List<Planting> plantings)
        {
            var sortList = (new List<Planting>(plantings));
            // if Value > 0, sort descending (biggest first), else sort ascending (smallest first)
            sortList.Sort((x, y) => Value > 0 ? 
                y.Tree.Damage.ValueOf(Field).CompareTo(x.Tree.Damage.ValueOf(Field)) : 
                x.Tree.Damage.ValueOf(Field).CompareTo(y.Tree.Damage.ValueOf(Field)));
            // remove plantings that don't fall within the first <Value> items in the list
            sortList.RemoveRange(0, Math.Min(Math.Abs(Value), sortList.Count));
            // remove the remainders from the filter list
            sortList.ForEach(p => plantings.Remove(p));
        }
    }
}