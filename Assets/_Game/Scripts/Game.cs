using Unity.Entities;
using Unity.NetCode;
using Unity.Networking.Transport;
using UnityEngine;

namespace _Game.Scripts {
	
	public struct EnableClientGame : IComponentData {}

	[UpdateInWorld(UpdateInWorld.TargetWorld.Default)]
	public class Game : ComponentSystem {

		// Singleton component to trigger connections once from a control system
		struct InitGameComponent : IComponentData {}

		protected override void OnCreate() {
			
			RequireSingletonForUpdate<InitGameComponent>();
			// Create singleton, require singleton for update so system runs once
			EntityManager.CreateEntity(typeof(InitGameComponent));

		}

		private ushort _port;


		protected override void OnUpdate() {
			
			// Destroy singleton to prevent system from running again
			EntityManager.DestroyEntity(GetSingletonEntity<InitGameComponent>());

			foreach (var world in World.All) {

				var network = world.GetExistingSystem<NetworkStreamReceiveSystem>();
				
				world.EntityManager.CreateEntity(typeof(EnableClientGame));
				
				if (world.GetExistingSystem<ClientSimulationSystemGroup>() != null) {
					
					Debug.Log("Client game started");
					
					// Client worlds automatically connect to localhost
					NetworkEndPoint ep = NetworkEndPoint.LoopbackIpv4;
					ep.Port = this._port;
					network.Connect(ep);

				}
				
#if UNITY_EDITOR
				
				else if (world.GetExistingSystem<ServerSimulationSystemGroup>() != null) {
					
					Debug.Log("Server game started");
					
					// Server world automatically listens for connections from any host
					NetworkEndPoint ep = NetworkEndPoint.AnyIpv4;
					ep.Port = this._port;
					network.Listen(ep);

				}

#endif

			}
			
		}

	}

}