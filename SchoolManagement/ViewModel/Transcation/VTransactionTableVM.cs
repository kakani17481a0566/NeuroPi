namespace SchoolManagement.ViewModel.Transaction
{
    public class TableHeader
    {
        public string Key { get; set; }     // e.g., "Col1"
        public string Label { get; set; }   // e.g., "TrxId"
    }

    public class VTransactionTableVM
    {
        public List<TableHeader> Headers { get; set; }               // Column definitions (Col1 → TrxId, etc.)
        public List<Dictionary<string, object>> TData { get; set; }  // Actual row values keyed as Col1, Col2, ...
    }
}
