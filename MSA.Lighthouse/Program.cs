using Akka;
using Akka.Actor;
using Akka.Configuration;
using System;
using System.Threading.Tasks;

namespace MSA.Lighthouse
{
    class Program
    {
        private static ActorSystem _actorSystem = null;
        static void Main(string[] args)
        {
            string config = configStr;
            foreach (string item in args)
            {
                string[] configParams = item.Split('=');
                config = config.Replace($"##{configParams[0]}##", configParams[1]);
            }
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            Console.CancelKeyPress += Console_CancelKeyPress;

            Config clusterConfig = ConfigurationFactory.ParseString(config);
            _actorSystem = ActorSystem.Create("MSA", clusterConfig);
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
    		    port = ##port##
    	    }
        }
        cluster {
    	    seed-nodes = [""akka.tcp://MSA@##hostname##:##port##""]
    	    roles = [Lighthouse]
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