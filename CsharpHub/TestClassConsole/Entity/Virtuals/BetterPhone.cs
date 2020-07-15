using System;
using System.Collections.Generic;
using System.Text;

namespace TestClassConsole.Entity.Virtuals
{
    public class BetterPhone:Phone
    {
        public new void Dial()
        {
            Console.WriteLine("BetterPhone.Dial");
            EsatablishConnection();
            base.Dial();
        }
        protected virtual void EsatablishConnection()
        {
            Console.WriteLine(nameof(BetterPhone)+"Connection");
        }
    }
}
