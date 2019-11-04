using Akka.Actor;
using Akka.Routing;
using MSA.Actors.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSA.Actors.Routers
{
    public class UntypedActorRouter : UntypedActor
    {
        //Her sorguda yeniden actor oluştur. Belirsiz acore bağlan ,MSA.Worker'ı çalıştırmaya gerek yok.
        protected override void OnReceive(object message)
        {
            IActorRef addActor = Context.ActorOf(Props.Create<ActorCrud>());
            addActor.Tell(message, Sender);
        }
    }
}
