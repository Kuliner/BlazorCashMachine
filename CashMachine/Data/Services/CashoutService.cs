using CashMachine.Data.Exceptions;
using CashMachine.Data.Model;

namespace CashMachine.Data.Services
{
    public class CashoutService : ICashoutService
    {
        private readonly int[] _notesAvailable = new int[] { 100, 50, 20, 10 };

        public Money Cashout(int? sum)
        {
            var money = new Money();
            var noteCounter = new int[_notesAvailable.Length];

            if (!sum.HasValue)
                return money;

            if (sum.Value < 0)
                throw new InvalidArgumentException();

            int rest = FetchBanknotes(sum.Value, money, noteCounter);
            if (rest != 0)
                throw new NoteUnavailableException();

            return money;
        }

        private int FetchBanknotes(int amount, Money money, int[] noteCounter)
        {
            for (int i = 0; i < _notesAvailable.Length; i++)
            {
                if (amount >= _notesAvailable[i])
                {
                    noteCounter[i] = amount / _notesAvailable[i];
                    amount -= noteCounter[i] * _notesAvailable[i];

                    for (int k = 0; k < noteCounter[i]; k++)
                        money.Banknotes.Add(new Banknote() { Value = _notesAvailable[i] });
                }
            }

            return amount;
        }
    }
}
