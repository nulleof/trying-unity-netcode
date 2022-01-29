using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace _Game.Scripts {

	// When server receives go in game request, go in game and delete request
	[UpdateInGroup(typeof(ServerSimulationSystemGroup))]
	public class GoInGameServerSystem : ComponentSystem {

		protected override void OnUpdate() {
			
			Entities.WithNone<SendRpcCommandRequestComponent>().ForEach((Entity entity, ref GoInGameRequest req, ref ReceiveRpcCommandRequestComponent reqSrc) => {
				
				PostUpdateCommands.AddComponent<NetworkStreamInGame>(reqSrc.SourceConnection);
				
				Debug.Log($"Server setting connection {EntityManager.GetComponentData<NetworkIdComponent>(reqSrc.SourceConnection)} to in game");

				var ghostCollection = GetSingletonEntity<GhostPrefabCollectionComponent>();
				var prefab = Entity.Null;
				var prefabs = EntityManager.GetBuffer<GhostPrefabBuffer>(ghostCollection);

				foreach (var ghostPrefab in prefabs) {

					if (EntityManager.HasComponent<MovableCubeComponent>(ghostPrefab.Value)) prefab = ghostPrefab.Value;

				}

				var player = EntityManager.Instantiate(prefab);
				
				EntityManager.SetComponentData(player, new GhostOwnerComponent() {
					NetworkId = EntityManager.GetComponentData<NetworkIdComponent>(reqSrc.SourceConnection).Value,
				});

				PostUpdateCommands.AddBuffer<CubeInput>(player);
				
				PostUpdateCommands.SetComponent(reqSrc.SourceConnection, new CommandTargetComponent() {
					targetEntity = player,
				});
				
				PostUpdateCommands.DestroyEntity(entity);

			});
			
		}

	}

}