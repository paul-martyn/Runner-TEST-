using System.Collections.Generic;
using CodeBase.Level.Road.Chunks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Data.Static
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "StaticData/Level static data")]
    public class LevelStaticData : ScriptableObject
    {
        [SerializeField, Range(10, 100)]
        private int _roadChunksCount = 10;
        
        [SerializeField, Range(2, 5), Space(10f)]
        private int _minChunksBetweenObstacles = 2;
        
        [SerializeField, AssetsOnly, Required, Space(10f)]
        private StartingChunk _startingChunk;
        
        [SerializeField, AssetsOnly, Space(10f), TableList(AlwaysExpanded = true, DrawScrollView = false), RequiredListLength(1, null)]
        private List<IntermediateChunkConfig> _intermediateChunks;
        
        [SerializeField, AssetsOnly, Required, Space(10f)]
        private FinalChunk _finalChunk;

        public int RoadChunksCount => _roadChunksCount;
        public int MinChunksBetweenObstacles => _minChunksBetweenObstacles;
        public StartingChunk StartingChunk => _startingChunk;
        public IReadOnlyList<IntermediateChunkConfig> IntermediateChunks => _intermediateChunks;
        public FinalChunk FinalChunk => _finalChunk;
    }
}
