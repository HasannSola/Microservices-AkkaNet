using Akka.Actor;
using Akka.Configuration;
using MSA.Actors.Routers;
using MSA.Core.Configs;

namespace MSA.Router
{
    class Program
    {
        static void Main(string[] args)
        {
            string config = AkkaConfig.configRouter;
            foreach (string item in args) 
            {
                string[] configParams = item.Split('=');
                config = config.Replace($"##{configParams[0]}##", configParams[1]);
            }
            Config clusterConfig = ConfigurationFactory.ParseString(config);
            ActorSystem _actorSystem = ActorSystem.Create("MSA", clusterConfig);
            _actorSystem.ActorOf(Props.Create<ActorRouter>(), "MServis");// Tanımlı Actor ile bağlantı  , Sadece AddActor tanımlı
           /// _actorSystem.ActorOf(Props.Create<UntypedActorRouter>(), "MServis");
            _actorSystem.WhenTerminated.Wait();
        }
    }
}
