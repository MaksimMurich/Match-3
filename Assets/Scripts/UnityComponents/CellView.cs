using Leopotam.Ecs;
using UnityEngine;

namespace Match3.UnityComponents
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CellView : MonoBehaviour
    {
        public EcsEntity Entity;
        public new Transform transform { get; private set; }

        private void Awake()
        {
            transform = base.transform;
        }
    }
}