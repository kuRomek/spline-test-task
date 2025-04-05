using UnityEngine;

namespace ArrowControl
{
    [RequireComponent(typeof(Collider2D))]
    public class ArrowPuller : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;

        public Vector2 OriginalPosition { get; private set; }

        private void Awake() =>
            OriginalPosition = transform.position;

        public bool IsTouching(Vector2 point) => 
            _collider.OverlapPoint(point);
    }
}
