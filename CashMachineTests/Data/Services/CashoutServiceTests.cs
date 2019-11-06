using CashMachine.Data.Exceptions;
using CashMachine.Data.Services;
using System.Linq;
using Xunit;

namespace CashMachine.Data.Model.Tests
{
    public class CashoutServiceTests
    {
        [Theory()]
        [InlineData(30, new int[] { 20, 10 })]
        [InlineData(80, new int[] { 50, 20, 10 })]
        public void ProperNotesCashoutTest(int moneyToCashout, int[] notesExpected)
        {
            var atm = new CashoutService();
            var money = atm.Cashout(moneyToCashout);
            var notes = money.Banknotes.Select(x => x.Value).ToArray();
            var sum = money.Banknotes.Sum(x => x.Value);

            Assert.Equal(notesExpected, notes);
            Assert.Equal(moneyToCashout, sum);
        }

        [Fact()]
        public void ImproperNotesCashoutTest()
        {
            var atm = new CashoutService();
            void act() => atm.Cashout(125);

            Assert.Throws<NoteUnavailableException>(act);
        }

        [Fact()]
        public void NegativeSumCashoutTest()
        {
            var atm = new CashoutService();
            void act() => atm.Cashout(-130);

            Assert.Throws<InvalidArgumentException>(act);
        }

        [Fact()]
        public void NullSumCashoutTest()
        {
            var atm = new CashoutService();
            var money = atm.Cashout(null);
            Assert.Empty(money.Banknotes);
        }
    }
}