using UnityEngine;
using UnityEngine.U2D;

public class ArrowTrajectory
{
    private readonly SpriteShapeController _spriteShape;

    private float _rangeMultiplier = 3f;

    public ArrowTrajectory(SpriteShapeController spriteShape)
    {
        _spriteShape = spriteShape;
    }

    public void Update(Vector2 velocity)
    {
        if (_spriteShape.gameObject.activeSelf == false)
            _spriteShape.gameObject.SetActive(true);

        for (int i = 0; i < _spriteShape.spline.GetPointCount(); i++)
        {
            velocity += _rangeMultiplier * Time.fixedDeltaTime * Physics2D.gravity;
            _spriteShape.spline.SetPosition(i, _rangeMultiplier * 2.175f * i * Time.fixedDeltaTime * velocity);
        }
    }
}
