using Akka.Actor;
using MSA.Bll.Abstract;
using MSA.Bll.Concrete;
using MSA.Dal.Concrete.EntityFramework;
using MSA.Entities.Entities;
using MSA.Entities.Message;
using System.Diagnostics;

namespace MSA.Actors.Actors
{
    public class ActorCrud : ReceiveActor
    {
        private IProductManager _productManager;
        public ActorCrud()
        {
            _productManager = new ProductManager(new EfProductDal());
            Receive<AddMessage>(message => Handle(message));
        }
        private void Handle(AddMessage message)
        {
            Debugger.Launch(); 

            var _sender = Sender;
            var result = _productManager.Add((Product)message.Value);
            _sender.Tell(result);
        }
    }
}
