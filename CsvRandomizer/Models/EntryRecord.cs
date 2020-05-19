using CsvHelper.Configuration;

namespace CsvRandomizer.Models
{
    public class EntryRecord
    {
        public int EntryTypeId { get; set; }
        public string Participant { get; set; }
    }

    public class EntryRecordClassMap : ClassMap<EntryRecord>
    {
        public EntryRecordClassMap()
        {
            Map(m => m.EntryTypeId).Name("EntryTypeId");
            Map(m => m.Participant).Name("Participant");
        }
    }
}