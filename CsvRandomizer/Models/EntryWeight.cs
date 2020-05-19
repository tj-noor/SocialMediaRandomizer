using CsvHelper.Configuration;

namespace CsvRandomizer.Models
{
    public class EntryWeight
    {
        public int Id { get; set; }
        public string Source { get; set; }
        public string TypeOfEntry { get; set; }
        public int NumberOfEntries { get; set; }
    }

    public class EntryWeightClassMap : ClassMap<EntryWeight>
    {
        public EntryWeightClassMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.Source).Name("Source");
            Map(m => m.TypeOfEntry).Name("Type Of Entry");
            Map(m => m.NumberOfEntries).Name("Number Of Entries");
        }
    }
}