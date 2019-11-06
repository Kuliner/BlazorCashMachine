using CashMachine.Data.Model;

namespace CashMachine.Data.Services
{
    public interface ICashoutService
    {
        Money Cashout(int? value);
    }
}