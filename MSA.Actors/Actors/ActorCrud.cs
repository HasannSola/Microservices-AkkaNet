using Akka.Actor;
using MSA.Bll.Abstract;
using MSA.Bll.Concrete;
using MSA.Dal.Concrete.EntityFramework;
using MSA.Entities.Entities;
using MSA.Entities.Message;
using System.Diagnostics;
using System.Threading;

namespace MSA.Actors.Actors
{
    public class ActorCrud : ReceiveActor
    {
        private IProductManager _productManager;
        public ActorCrud()
        {
            _productManager = new ProductManager(new EfProductDal());
            Receive<AddMessage>(message => Handle(message));
            Receive<GetAllMessage>(message => Handle(message));
        }
        private void Handle(AddMessage message)
        {
            //Debugger.Launch(); 
            string result = "";
            var _sender = Sender;
             result = _productManager.Add((Product)message.Value);
            if (!string.IsNullOrEmpty(result))
            {//Ard arda  3 tane worker actor gönderildiğinde actor çalışmakta , 4. actor de ise kuyrukta beklemekte.
                Thread.Sleep(3 * 1000);
            }
            _sender.Tell(result);
        }
        private void Handle(GetAllMessage message)
        {
            string result = "";
            var _sender = Sender;
             result = _productManager.GetAll();
            _sender.Tell(result);
        }
    }
}
