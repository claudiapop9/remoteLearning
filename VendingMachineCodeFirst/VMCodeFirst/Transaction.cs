using System;


namespace VendingMachineCodeFirst
{
    class Transaction
    {
        public int TransactionId { get; set; }
        public string Type{ get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
    }
}
