//THIS FILE IS AUTOGENERATED BY GHOSTCOMPILER. DON'T MODIFY OR ALTER.
using AOT;
using Unity.Burst;
using Unity.Networking.Transport;
using Unity.Entities;
using Unity.Collections;
using Unity.NetCode;
using Unity.Transforms;
using Unity.Mathematics;
using _Game.Scripts;


namespace Assembly_CSharp.Generated
{
    public struct _GameScriptsCubeInputSerializer : ICommandDataSerializer<_Game.Scripts.CubeInput>
    {
        public void Serialize(ref DataStreamWriter writer, in _Game.Scripts.CubeInput data)
        {
            writer.WriteInt((int) data.horizontal);
            writer.WriteInt((int) data.vertical);
        }

        public void Deserialize(ref DataStreamReader reader, ref _Game.Scripts.CubeInput data)
        {
            data.horizontal = (int) reader.ReadInt();
            data.vertical = (int) reader.ReadInt();
        }

        public void Serialize(ref DataStreamWriter writer, in _Game.Scripts.CubeInput data, in _Game.Scripts.CubeInput baseline, NetworkCompressionModel compressionModel)
        {
            writer.WritePackedIntDelta((int) data.horizontal, (int) baseline.horizontal, compressionModel);
            writer.WritePackedIntDelta((int) data.vertical, (int) baseline.vertical, compressionModel);
        }

        public void Deserialize(ref DataStreamReader reader, ref _Game.Scripts.CubeInput data, in _Game.Scripts.CubeInput baseline, NetworkCompressionModel compressionModel)
        {
            data.horizontal = (int) reader.ReadPackedIntDelta((int) baseline.horizontal, compressionModel);
            data.vertical = (int) reader.ReadPackedIntDelta((int) baseline.vertical, compressionModel);
        }
    }
    public class _GameScriptsCubeInputSendCommandSystem : CommandSendSystem<_GameScriptsCubeInputSerializer, _Game.Scripts.CubeInput>
    {
        [BurstCompile]
        struct SendJob : IJobEntityBatch
        {
            public SendJobData data;
            public void Execute(ArchetypeChunk chunk, int orderIndex)
            {
                data.Execute(chunk, orderIndex);
            }
        }
        protected override void OnUpdate()
        {
            var sendJob = new SendJob{data = InitJobData()};
            ScheduleJobData(sendJob);
        }
    }
    public class _GameScriptsCubeInputReceiveCommandSystem : CommandReceiveSystem<_GameScriptsCubeInputSerializer, _Game.Scripts.CubeInput>
    {
        [BurstCompile]
        struct ReceiveJob : IJobEntityBatch
        {
            public ReceiveJobData data;
            public void Execute(ArchetypeChunk chunk, int orderIndex)
            {
                data.Execute(chunk, orderIndex);
            }
        }
        protected override void OnUpdate()
        {
            var recvJob = new ReceiveJob{data = InitJobData()};
            ScheduleJobData(recvJob);
        }
    }
}
