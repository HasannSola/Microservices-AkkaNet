using Akka;
using Akka.Actor;
using Akka.Configuration;
using System;
using System.Threading.Tasks;

namespace MSA.Worker
{
    class Program
    {
        private static ActorSystem _actorSystem = null;
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            Console.CancelKeyPress += Console_CancelKeyPress;

            string config = configStr;
            foreach (string item in args)
            {
                string[] configParams = item.Split('=');
                if (configParams[0] != "roles")
                    config = config.Replace($"##{configParams[0]}##", configParams[1]);
            }

            foreach (string item in args)
            {
                string[] configParams = item.Split('=');
                if (configParams[0] == "roles")
                {
                    string[] role = configParams[1].Split(',');
                    for (int i = 0; i < role.Length; i++)
                    {
                        Config clusterConfig = ConfigurationFactory.ParseString(config.Replace("##roles##", role[i]));
                        _actorSystem = ActorSystem.Create("MSA", clusterConfig);
                    }
                }
            }
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

        private static readonly string configStr =
            @"akka {
	actor { 
		serializers { 
			json = ""Akka.Serialization.HyperionSerializer, Akka.Serialization.Hyperion""
		}
        serialization-bindings { 
	    		""System.Object"" = json
        }
        provider = ""Akka.Cluster.ClusterActorRefProvider, Akka.Cluster""
    }
    remote {
    	dot-netty.tcp {
    		transport-class = ""Akka.Remote.Transport.DotNetty.TcpTransport, Akka.Remote""
    		applied-adapters = []
            transport-protocol = tcp
            public-hostname = ""##publichostname##""
    		hostname = ""##hostname##""
    		port = 0
    	}
    }
    cluster {
        seed-nodes = [""akka.tcp://MSA@##hostname##:##port##""]
    	roles = [##roles##]
    	downing-provider-class = ""Akka.Cluster.SplitBrainResolver, Akka.Cluster""
    	split-brain-resolver {
    		stable-after = 20s
            active-strategy = keep-majority
    	}
    }
    log-config-on-start = on
}";
    }
}
