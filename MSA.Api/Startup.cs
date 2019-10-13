using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka;
using Akka.Actor;
using Akka.Configuration;
using Akka.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MSA.Api
{
    public class Startup
    {
        private ActorSystem _actorSystem;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static string[] Args { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            applicationLifetime.ApplicationStarted.Register(HandleStarted);
            applicationLifetime.ApplicationStopped.Register(HandleStopped);
        }
        private void HandleStarted()
        {
            string config = configStr;
            foreach (string item in Args)
            {
                string[] configParams = item.Split('=');
                config = config.Replace($"##{configParams[0]}##", configParams[1]);
            }

            Config clusterConfig = ConfigurationFactory.ParseString(config);
            _actorSystem = ActorSystem.Create("MSA", clusterConfig);
            Globals.Router = _actorSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "MSRouterConfigName");
        }

        private void HandleStopped()
        {
            if (_actorSystem != null)
            {
                Task<Done> shutdownTask = CoordinatedShutdown.Get(_actorSystem).Run(CoordinatedShutdown.ClrExitReason.Instance);
                shutdownTask.Wait();
            }
        }
        private readonly string configStr =
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
