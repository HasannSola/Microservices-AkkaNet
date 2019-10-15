namespace MSA.Core.Configs
{
    public static class AkkaConfig
    {
        public static string configLighthouse =
           @"akka {
	            actor { 
                    provider = ""Akka.Cluster.ClusterActorRefProvider, Akka.Cluster""
                }
                remote {
    	            dot-netty.tcp {
                        transport-protocol = tcp
                        public-hostname = ""##publichostname##""
    		            hostname = ""##hostname##""
    		            port = ##port##
    	            }
                }
                cluster {
    	            seed-nodes = [""akka.tcp://MSA@##hostname##:##port##""]
    	            roles = [Lighthouse]
                }
                log-config-on-start = on   #Cmd ekranında config file gözükemsi için
            }";

        public static string configRouter =
        @"akka {
	        actor { 
                 provider = ""Akka.Cluster.ClusterActorRefProvider, Akka.Cluster""
                deployment {
                        /MServis/Add{
                          router = round-robin-pool
                          nr-of-instances = 10
                          cluster {
                              enabled = on
                              max-nr-of-instances-per-node = 3
                              use-role =  AddActor
                          }
                   }
              }
            }
            remote {
    	        dot-netty.tcp {
                    transport-protocol = tcp
                    public-hostname = ""##publichostname##""
    		        hostname = ""##hostname##""
    		        port = 0
    	        }
            }
            cluster {
    	        seed-nodes = [""akka.tcp://MSA@##hostname##:##port##""]
    	        roles = [Router]
            }
        }";

        public static string configWorker =
         @"akka {
	          actor { 
                        provider = ""Akka.Cluster.ClusterActorRefProvider, Akka.Cluster""
                    }
                    remote {
    	                dot-netty.tcp {
                            transport-protocol = tcp
                            public-hostname = ""##publichostname##""
    		                hostname = ""##hostname##""
    		                port = 0
    	                }
                    }
                    cluster {
                        seed-nodes = [""akka.tcp://MSA@##hostname##:##port##""]
    	                roles = [##roles##]
                    }
                }";

        public static string configApi =
           @"akka {
	                 actor { 
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
    	                    dot-netty.tcp {
    	                        hostname = ""##hostname##""
    		                    port = 0
    	                    }
                        }

                        cluster {
                            seed-nodes = [""akka.tcp://MSA@##hostname##:##port##""]
    	                    roles = [MSApi]
                        }
                }";
    }
}
