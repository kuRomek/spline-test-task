using Input;
using Misc;
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
            _input.ClickedOnScreen += OnScreenClicked;
            _input.PressCanceled += CancelPulling;
        }

        public void Disable()
        {
            _input.ClickedOnScreen -= OnScreenClicked;
            _input.PressCanceled -= CancelPulling;
        }

        public void Update(float deltaTime)
        {
            if (_isPullingArrow)
            {
                _arrowPuller.transform.position = _input.MousePosition;
                PullingArrow?.Invoke(_arrowPuller.transform.position);
            }
        }

        private void OnScreenClicked(Vector2 mousePosition)
        {
            _isPullingArrow = _arrowPuller.IsTouching(mousePosition);

            if (_isPullingArrow)
                PullingStarted?.Invoke();
        }

        private void CancelPulling()
        {
            if (_isPullingArrow == false)
                return;

            _isPullingArrow = false;
            _arrowPuller.transform.position = _arrowPuller.OriginalPosition;
            PullingCanceled?.Invoke();
        }

        public void ThrowArrow(Vector2 velocity, Vector2 startPositino)
        {
            _arrow.transform.position = startPositino;
            _arrow.Throw(velocity);
        }
    }
}
