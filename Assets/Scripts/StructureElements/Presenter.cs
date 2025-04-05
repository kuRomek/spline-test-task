using UnityEngine;

namespace StructureElements
{
    public class Presenter : MonoBehaviour
    {
        private Transformable _model;
        private View _view;
        private IUpdatable _updatable = null;
        private IActivatable _activatable = null;

        public Transformable Model => _model;
        public View View => _view;

        private void Update()
        {
            _model.MoveTo(transform.position, false);
            _updatable?.Update(Time.deltaTime);
        }

        private void OnEnable()
        {
            if (_model != null)
                _model.Moved += OnMoved;

            _activatable?.Enable();

            if (this is IActivatable activatable)
                activatable.Enable();
        }

        private void OnDisable()
        {
            if (_model != null)
                _model.Moved -= OnMoved;

            _activatable?.Disable();

            if (this is IActivatable activatable)
                activatable.Disable();
        }

        public void Init(Transformable model)
        {
            _model = model;
            _view = GetComponent<View>();

            if (_model is IUpdatable updatable)
                _updatable = updatable;

            if (_model is IActivatable activatable)
                _activatable = activatable;

            enabled = true;

            if (_view != null)
                _view.enabled = true;

            OnMoved();
        }

        private void OnMoved()
        {
            if (_model != null)
                transform.position = _model.Position;
        }
    }
}
