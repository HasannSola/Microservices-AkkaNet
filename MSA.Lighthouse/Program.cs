using Akka.Actor;
using Akka.Configuration;
using MSA.Core.Configs;

namespace MSA.Lighthouse
{
    class Program
    {
        static void Main(string[] args)
        {
            string config = AkkaConfig.configLighthouse;
            foreach (string item in args)
            {
                string[] configParams = item.Split('=');
                config = config.Replace($"##{configParams[0]}##", configParams[1]);
            }
            Config clusterConfig = ConfigurationFactory.ParseString(config);
            ActorSystem _actorSystem = ActorSystem.Create("MSA", clusterConfig);
            _actorSystem.WhenTerminated.Wait();
        }
    }
}