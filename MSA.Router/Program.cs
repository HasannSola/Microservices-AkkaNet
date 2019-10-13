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
            foreach (string item in args)
            {
                string[] configParams = item.Split('=');
                AkkaConfig.configRouter = AkkaConfig.configRouter.Replace($"##{configParams[0]}##", configParams[1]);
            }
            Config clusterConfig = ConfigurationFactory.ParseString(AkkaConfig.configRouter);
            ActorSystem _actorSystem = ActorSystem.Create("MSA", clusterConfig);
            _actorSystem.ActorOf(Props.Create<ActorRouter>("Add"), "MServis");
            _actorSystem.WhenTerminated.Wait();
        }
    }
}
