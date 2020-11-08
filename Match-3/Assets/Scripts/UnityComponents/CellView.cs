using Leopotam.Ecs;
using Match3.Assets.Scripts.Services;
using UnityEngine;

namespace Match3.UnityComponents
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class CellView : MonoBehaviour, IClone<CellView>
    {
        public EcsEntity Entity;
        private CellView Original;

        CellView IClone<CellView>.GetOriginal()
        {
            return Original;
        }

        public void SetOriginal(CellView value)
        {
            Original = value;
        }

        public void Reset()
        {
            transform.localScale = Original.transform.localScale;
        }
    }
}