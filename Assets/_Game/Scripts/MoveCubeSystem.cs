using Unity.Entities;
using Unity.NetCode;
using Unity.Transforms;

namespace _Game.Scripts {

	[UpdateInGroup(typeof(GhostPredictionSystemGroup))]
	public class MoveCubeSystem : ComponentSystem {

		protected override void OnUpdate() {

			var group = World.GetExistingSystem<GhostPredictionSystemGroup>();
			var tick = group.PredictingTick;
			var deltaTime = Time.DeltaTime;
			
			Entities.ForEach((DynamicBuffer<CubeInput> inputBuffer, ref Translation translation,
				ref PredictedGhostComponent prediction) => {

				if (!GhostPredictionSystemGroup.ShouldPredict(tick, prediction)) return;

				CubeInput input;
				inputBuffer.GetDataAtTick(tick, out input);

				if (input.horizontal > 0) translation.Value.x += deltaTime;
				if (input.horizontal < 0) translation.Value.x -= deltaTime;
				if (input.vertical > 0) translation.Value.z += deltaTime;
				if (input.vertical < 0) translation.Value.z -= deltaTime;

			});

		}

	}

}