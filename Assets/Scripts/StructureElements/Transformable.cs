using System;
using UnityEngine;

namespace StructureElements
{
    public abstract class Transformable
    {
        public Transformable(Vector2 position = default)
        {
            Position = position;
        }

        public event Action Moved;

        public Vector2 Position { get; private set; }

        public void MoveTo(Vector2 position, bool isPresenterMoving = true)
        {
            Position = position;

            if (isPresenterMoving)
                Moved?.Invoke();
        }
    }
}