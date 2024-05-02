using UnityEngine;

namespace CodeBase.Level.Road.Chunks
{
    public class RoadChunk : MonoBehaviour
    {
        public Directions OutputDirections => _outputDirections;
        [SerializeField] private Directions _outputDirections = Directions.Forward;

        public Transform Center => _center;
        [Space(10f)]
        [SerializeField] protected Transform _center;
        
        public void Destroy() => 
            Destroy(gameObject);
    }
}
