using Akka.Actor;
using MSA.Entities.Message;

namespace MSA.Actors.Actors
{
    public class ActorCrud : ReceiveActor
    {
        public ActorCrud()
        {
            Receive<AddMessage>(message => Handle(message));
        }
        private void Handle(AddMessage message)
        {
            var _sender = Sender;
            _sender.Tell("Add Message Alındı.");
        }
    }
}
