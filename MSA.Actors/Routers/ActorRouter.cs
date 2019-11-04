using Akka.Actor;
using Akka.Routing;
using MSA.Actors.Actors;
using MSA.Entities.Message;

namespace MSA.Actors.Routers
{
    public class ActorRouter : ReceiveActor
    {
        //Worker ile ayağa kalkan Actor'e bağlan ,Tanımalan Actor
        private readonly IActorRef _addActor = null;
        private readonly IActorRef _getAllActor = null;
        public ActorRouter()
        {
            _addActor = Context.ActorOf(Props.Create<ActorCrud>().WithRouter(FromConfig.Instance), "Add");//  User/MServis/AddActor
            _getAllActor = Context.ActorOf(Props.Create<ActorCrud>().WithRouter(FromConfig.Instance), "GetAll");//  User/MServis/AddActor
            Receive<AddMessage>(message => Handle(message));
            Receive<GetAllMessage>(message => Handle(message));
        }
        private void Handle(AddMessage message)
        {
            _addActor.Tell(message, Sender);
        }

        private void Handle(GetAllMessage message)
        {
            _getAllActor.Tell(message, Sender);
        }
    }
}
