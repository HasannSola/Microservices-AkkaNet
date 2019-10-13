using Akka.Actor;
using Akka.Routing;
using MSA.Actors.Actors;
using MSA.Entities.Message;

namespace MSA.Actors.Routers
{
    public class ActorRouter : ReceiveActor
    {
        private readonly IActorRef _addActor = null;
        public ActorRouter(string addActorName)
        {
            _addActor = Context.ActorOf(Props.Create<ActorCrud>().WithRouter(FromConfig.Instance), addActorName);
            Receive<AddMessage>(message => Handle(message));
        }
        private void Handle(AddMessage message)
        {
            _addActor.Tell(message, Sender);
        }
    }
}
