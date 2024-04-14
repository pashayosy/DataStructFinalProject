namespace DB.Models;

using System;
using System.Collections.Generic;

public class Engine<TValue, TNonUniq, TRange> where TValue : class
{
    private Table<TValue> table;
    private UniqueIndex<Guid, TValue> uniqueIndex;
    private NonUniqueIndex<TNonUniq, TValue> nonUniqueIndex; // Example index by a string property, e.g., Name
    private RangeIndex<TRange, TValue> rangeIndex; // Example for a range index, assuming a DateTime property

    public Engine()
    {
        table = new Table<TValue>();
        uniqueIndex = new UniqueIndex<Guid, TValue>();
        nonUniqueIndex = new NonUniqueIndex<TNonUniq, TValue>();
        rangeIndex = new RangeIndex<TRange, TValue>();
    }

    // Add a record to the table and all indices
    public void AddRecord(Guid id, TValue record, string nameKey, DateTime dateKey)
    {
        table.Add(id, record);
        uniqueIndex.Add(id, record);
        nonUniqueIndex.Add(nameKey, record);
        rangeIndex.Add(dateKey, record);
    }

    // Update a record in the table and all indices
    public void UpdateRecord(Guid id, TValue updatedRecord, string oldNameKey, string newNameKey, DateTime oldDateKey, DateTime newDateKey)
    {
        table.Update(id, updatedRecord);
        uniqueIndex.Update(id, updatedRecord);

        if (oldNameKey != newNameKey)
        {
            nonUniqueIndex.Remove(oldNameKey, table.Get(id));
            nonUniqueIndex.Add(newNameKey, updatedRecord);
        }
        else
        {
            nonUniqueIndex.Update(newNameKey, table.Get(id), updatedRecord);
        }

        if (oldDateKey != newDateKey)
        {
            rangeIndex.Update(oldDateKey, newDateKey, table.Get(id), updatedRecord);
        }
    }

    // Remove a record from the table and all indices
    public void RemoveRecord(Guid id, string nameKey, DateTime dateKey)
    {
        TValue record = table.Get(id);
        if (record != null)
        {
            table.Remove(id);
            uniqueIndex.Remove(id);
            nonUniqueIndex.Remove(nameKey, record);
            rangeIndex.Remove(dateKey, record);
        }
    }

    // Get record by unique ID
    public TValue GetRecordById(Guid id)
    {
        return uniqueIndex.Get(id);
    }

    // Get records by name (non-unique key)
    public IEnumerable<TValue> GetRecordsByName(string name)
    {
        return nonUniqueIndex.Get(name);
    }

    // Get records by a date range
    public IEnumerable<TValue> GetRecordsByDateRange(DateTime start, DateTime end)
    {
        return rangeIndex.QueryRange(start, end);
    }
}
