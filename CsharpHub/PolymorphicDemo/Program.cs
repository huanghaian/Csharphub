using System;
using System.Net;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace PolymorphicDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MqttClient client = new MqttClient("10.67.2.55");
                client.Connect("test", "sammy", "123@abc");
                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

                client.Subscribe(new string[] { "test-msg" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                client.Publish("test", Encoding.UTF8.GetBytes("sdf"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           

        }


        
        private static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine("有推送");
            Console.WriteLine($"主题{e.Topic}");
            Console.WriteLine($"{System.Text.Encoding.UTF8.GetString(e.Message)}");
            
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
