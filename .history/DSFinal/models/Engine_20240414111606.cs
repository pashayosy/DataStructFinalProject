namespace DB.Models;

using System;
using System.Collections.Generic;

public class Engine<T> where T : class
{
    private Table<T> table;
    private UniqueIndex<Guid, T> uniqueIndex;
    private NonUniqueIndex<string, T> nonUniqueIndex; // Example index by a string property, e.g., Name
    private RangeIndex<DateTime, T> rangeIndex; // Example for a range index, assuming a DateTime property

    public Engine()
    {
        table = new Table<T>();
        uniqueIndex = new UniqueIndex<Guid, T>();
        nonUniqueIndex = new NonUniqueIndex<string, T>();
        rangeIndex = new RangeIndex<DateTime, T>();
    }

    // Add a record to the table and all indices
    public void AddRecord(Guid id, T record, string nameKey, DateTime dateKey)
    {
        table.Add(id, record);
        uniqueIndex.Add(id, record);
        nonUniqueIndex.Add(nameKey, record);
        rangeIndex.Add(dateKey, record);
    }

    // Update a record in the table and all indices
    public void UpdateRecord(Guid id, T updatedRecord, string oldNameKey, string newNameKey, DateTime oldDateKey, DateTime newDateKey)
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
        T record = table.Get(id);
        if (record != null)
        {
            table.Remove(id);
            uniqueIndex.Remove(id);
            nonUniqueIndex.Remove(nameKey, record);
            rangeIndex.Remove(dateKey, record);
        }
    }
}
