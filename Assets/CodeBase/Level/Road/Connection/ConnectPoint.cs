using UnityEngine;

namespace CodeBase.Level.Road.Connection
{
    public abstract class ConnectPoint : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public Vector3 LocalPosition => transform.localPosition;
        public Quaternion Rotation => transform.rotation;
    }
}