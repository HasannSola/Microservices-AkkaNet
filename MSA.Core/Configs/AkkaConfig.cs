namespace MSA.Core.Configs
{
    public static class AkkaConfig
    {
        public static string configLighthouse =
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

        public static string configRouter =
        @"akka {
	        actor { 
		        serializers { 
			        json = ""Akka.Serialization.HyperionSerializer, Akka.Serialization.Hyperion""
		        }
                serialization-bindings { 
	    		        ""System.Object"" = json
                }
                 provider = ""Akka.Cluster.ClusterActorRefProvider, Akka.Cluster""
                deployment {
                        /MServis/Add{
                          router = round-robin-pool
                          nr-of-instances = 10
                          cluster {
                              enabled = on
                              max-nr-of-instances-per-node = 2
                              use-role =  AddActor
                          }
                   }
              }
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
    	        roles = [Router]
    	        downing-provider-class = ""Akka.Cluster.SplitBrainResolver, Akka.Cluster""
    	        split-brain-resolver {
    		        stable-after = 20s
                    active-strategy = keep-majority
    	        }
            }
            log-config-on-start = on
        }";

        public static string configWorker =
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
        public static string configApi =
@"akka {
	actor { 
		serializers { 
			json = ""Akka.Serialization.HyperionSerializer, Akka.Serialization.Hyperion""
		}
        serialization-bindings { 
	    		""System.Object"" = json
        }
        provider = ""Akka.Cluster.ClusterActorRefProvider, Akka.Cluster""
        deployment {
        /MSRouterConfigName{
            router = round-robin-group
            routees.paths = [""/user/MServis""]
            cluster {
            enabled = on
            allow-local-routees = off
            use-role = Router
            }
        }
    }
    }
    remote {
        maximum-payload-bytes = 83886080 bytes
    	dot-netty.tcp {
    	    hostname = ""##hostname##""
    		port = 0
            maximum-frame-size = 83886080b
    	}
    }
    cluster {
    seed-nodes = [""akka.tcp://MSA@##hostname##:##port##""]
    	roles = [MSApi]
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
