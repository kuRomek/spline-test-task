using Input;
using ArrowControl;
using StructureElements;
using System;
using UnityEngine;

namespace ArcherControl
{
    public class Archer : Transformable, IActivatable, IUpdatable
    {
        private readonly PlayerInputController _input;
        private readonly Arrow _arrow;
        private readonly ArrowPuller _arrowPuller;
        private bool _isPullingArrow = false;

        public Archer(PlayerInputController input, ArrowPuller arrowPuller, Arrow arrow, Vector2 position) : base(position)
        {
            _input = input;
            _arrow = arrow;
            _arrowPuller = arrowPuller;
            _arrow.gameObject.SetActive(false);
        }

        public event Action PullingStarted;
        public event Action<Vector2> PullingArrow;
        public event Action PullingCanceled;

        public void Enable()
        {
            _input.ClickedOnScreen += TryPullArrow;
            _input.PressCanceled += OnMouseReleased;
        }

        public void Disable()
        {
            _input.ClickedOnScreen -= TryPullArrow;
            _input.PressCanceled -= OnMouseReleased;
        }

        public void Update(float deltaTime)
        {
            if (_isPullingArrow)
            {
                _arrowPuller.transform.position = _input.MousePosition;
                PullingArrow?.Invoke(_arrowPuller.transform.position);
            }
        }

        private void TryPullArrow(Vector2 mousePosition)
        {
            _isPullingArrow = _arrowPuller.IsTouching(mousePosition);

            if (_isPullingArrow)
                PullingStarted?.Invoke();
        }

        private void OnMouseReleased()
        {
            if (_isPullingArrow == false)
                return;

            _isPullingArrow = false;
            _arrowPuller.transform.position = _arrowPuller.OriginalPosition;
            PullingCanceled?.Invoke();
        }

        public void ShootArrow(Vector2 direction, Vector2 startPositino)
        {
            _arrow.transform.position = startPositino;
            _arrow.AddForce(direction);
        }
    }
}
