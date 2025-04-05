using UnityEngine;
using UnityEngine.U2D;

namespace ArrowControl
{
    public class ArrowTrajectory
    {
        private readonly SpriteShapeController _spriteShape;

        private float _rangeMultiplier = 5f;

        public ArrowTrajectory(SpriteShapeController spriteShape)
        {
            _spriteShape = spriteShape;
        }

        public void Update(Vector2 velocity)
        {
            if (_spriteShape.gameObject.activeSelf == false)
                _spriteShape.gameObject.SetActive(true);

            for (int i = 1; i < _spriteShape.spline.GetPointCount(); i++)
            {
                velocity += _rangeMultiplier * Time.fixedDeltaTime * Physics2D.gravity;

                _spriteShape.spline.SetPosition(
                    i,
                    (Vector2)_spriteShape.spline.GetPosition(i - 1) + _rangeMultiplier * Time.fixedDeltaTime * velocity);
            }
        }
    }
}
