using Akka;
using Akka.Actor;
using Akka.Configuration;
using MSA.Core.Configs;
using System;
using System.Threading.Tasks;

namespace MSA.Worker
{
    class Program
    {
        private static ActorSystem _actorSystem = null;
        static void Main(string[] args)
        {
            string config = AkkaConfig.configWorker;
            foreach (string item in args)
            {
                string[] configParams = item.Split('=');
                if (configParams[0] != "roles")
                {
                    config = config.Replace($"##{configParams[0]}##", configParams[1]);
                }
                else if (configParams[0] == "roles")
                {
                    string[] role = configParams[1].Split(',');
                    for (int i = 0; i < role.Length; i++)
                    {
                        Config clusterConfig = ConfigurationFactory.ParseString(config.Replace("##roles##", role[i]));
                        _actorSystem = ActorSystem.Create("MSA", clusterConfig);
                    }
                }
            }

            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            Console.CancelKeyPress += Console_CancelKeyPress;

            _actorSystem.WhenTerminated.Wait();
        }
        private static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            StopActorSystem();
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            StopActorSystem();
        }
        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            StopActorSystem();
        }

        private static void StopActorSystem()
        {
            Task<Done> shutdownTask = CoordinatedShutdown.Get(_actorSystem).Run(CoordinatedShutdown.ClrExitReason.Instance);
            shutdownTask.Wait();
        }
    }
}
