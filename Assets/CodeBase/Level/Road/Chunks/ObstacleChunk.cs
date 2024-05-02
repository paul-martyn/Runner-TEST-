using UnityEngine;

namespace CodeBase.Level.Road.Chunks
{
    public class ObstacleChunk : IntermediateChunk
    {
        [SerializeField, Space(10f)]
        private ObstacleType _obstacleType;

        public ObstacleType ObstacleType => _obstacleType;
    }
}