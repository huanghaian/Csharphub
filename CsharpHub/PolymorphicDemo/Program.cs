using System;

namespace PolymorphicDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstClass first = new FirstClass();
            BaseClass baseClass = first;
            baseClass.Alert();
            first.Alert();
            //FirstClass first = new FirstClass();
            //BaseClass baseClass = first;
            //SecondClass second = (SecondClass)baseClass;
            //var labelUndo = new LabelUndo();
            //labelUndo.Undo();
            //((IUndo)labelUndo).Undo();
            //((TextUndo)labelUndo).Undo();
            var t = new TestClass();
            ((T1)((T2)t)).T();
        }
        #region class
        public class BaseClass
        {
            public virtual void Alert() =>Console.WriteLine("BaseClass.Alert");
        }
        public class FirstClass : BaseClass
        {
            public override void Alert() =>Console.WriteLine("FirstClass.Alert");

        }
        public class SecondClass : BaseClass
        {
            public new void Alert() =>Console.WriteLine("SecondClass.Alert");
        }
        #endregion

        #region interface
        public interface IUndo
        {
            void Undo();
        }
        public class TextUndo : IUndo
        {
            public virtual void Undo() => Console.WriteLine("TextUndo.Undo");
        }
        public class LabelUndo : TextUndo
        {
            public override void Undo()
            {
                Console.WriteLine("LabelUndo.Undo()");
            }
        }
        #endregion

        #region interface
        public interface T1 { void T(); }
        public interface T2 { void T(); }
        public class TestClass : T1, T2
        {
            public void T()
            {
                Console.WriteLine("这是T1.T");
            }
             void T2.T()
            {
                Console.WriteLine("这是T2.T");
            }

        }
        #endregion

    }
}
