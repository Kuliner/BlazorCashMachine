using System.Collections.Generic;

namespace CashMachine.Data.Model
{
    public class Money
    {
        public List<Banknote> Banknotes { get; set; } = new List<Banknote>();
    }
}
