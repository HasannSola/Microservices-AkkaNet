using Akka.Actor;
using Akka.Routing;
using MSA.Actors.Actors;
using MSA.Entities.Message;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MSA.Actors.Handlers
{
    public class ActorHandler : ReceiveActor
    {
        IActorRef _addActor = null;
        public ActorHandler(string addActorName)
        {

            Debugger.Launch();       
            _addActor = Context.ActorOf(Props.Create<ActorCrud>().WithRouter(FromConfig.Instance), addActorName);
            Receive<AddMessage>(message => Handle(message));
        }
        private void Handle(AddMessage message)
        {
            _addActor.Tell(message, Sender);
        }
    }
}
