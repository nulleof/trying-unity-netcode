using Unity.Entities;
using Unity.NetCode;

namespace _Game.Scripts {

	public struct GoInGameRequest : IRpcCommand {}

	// When client has a connection with network id, go in game and tell server to also go in game
	[UpdateInGroup(typeof(ClientSimulationSystemGroup))]
	public class GoInGameClientSystem : ComponentSystem {

		protected override void OnCreate() {
			
			RequireSingletonForUpdate<EnableClientGame>();
			
		}

		protected override void OnUpdate() {
			
			Entities.WithNone<NetworkStreamInGame>().ForEach((Entity entity, ref NetworkIdComponent id) => {
				
				PostUpdateCommands.AddComponent<NetworkStreamInGame>(entity);
				var req = PostUpdateCommands.CreateEntity();
				PostUpdateCommands.AddComponent<GoInGameRequest>(req);
				PostUpdateCommands.AddComponent(req, new SendRpcCommandRequestComponent() {
					TargetConnection = entity,
				});

			});
			
		}

	}

}