using Unity.NetCode;

namespace _Game.Scripts {

	public struct CubeInput : ICommandData {

		public uint Tick { get; set; }
		public int horizontal;
		public int vertical;

	}

}