using CodeBase.Level.Road.Connection;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(ConnectPoint))]
    public class ConnectPointEditor : UnityEditor.Editor
    {
        private const float Size = 0.05f;

        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(ConnectPoint connectPoint, GizmoType gizmoType)
        {
            Transform transform = connectPoint.transform;
            Vector3 position = transform.position;
            float size = Size;
            switch (connectPoint)
            {
                case InputPoint _:
                    Gizmos.color = Color.green;
                    Handles.color = Color.green;
                    size = 1.5f * Size;
                    break;
                case OutputPoint _:
                    Gizmos.color = Color.red;
                    Handles.color = Color.red;
                    break;
                default:
                    Gizmos.color = Color.white;
                    Handles.color = Color.white;
                    break;
            }
            
            Handles.ConeHandleCap(
                controlID: 0,
                position: position + transform.forward * size,
                rotation: transform.rotation,
                size: size * 2,
                eventType: EventType.Repaint
            );        
            
            Gizmos.DrawSphere(transform.position, size);
        }
    }
}