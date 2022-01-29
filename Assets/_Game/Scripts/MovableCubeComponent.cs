using Unity.Entities;
using Unity.NetCode;

namespace _Game.Scripts {

	[GenerateAuthoringComponent]
	public class MovableCubeComponent : IComponentData {

		[GhostField]
		public int ExampleValue;

	}

}
