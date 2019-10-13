using Akka.Actor;
using Akka.Configuration;
using MSA.Core.Configs;

namespace MSA.Lighthouse
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string item in args)
            {
                string[] configParams = item.Split('=');
                AkkaConfig.configLighthouse = AkkaConfig.configLighthouse.Replace($"##{configParams[0]}##", configParams[1]);
            }
            Config clusterConfig = ConfigurationFactory.ParseString(AkkaConfig.configLighthouse);
            ActorSystem _actorSystem = ActorSystem.Create("MSA", clusterConfig);
            _actorSystem.WhenTerminated.Wait();
        }
    }
}