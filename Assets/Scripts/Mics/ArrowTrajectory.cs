using UnityEngine;
using UnityEngine.U2D;

public class ArrowTrajectory
{
    private readonly SpriteShapeController _spriteShape;

    public ArrowTrajectory(SpriteShapeController spriteShape)
    {
        _spriteShape = spriteShape;
    }

    public void Update(Vector2 direction)
    {
        if (_spriteShape.gameObject.activeSelf == false)
            _spriteShape.gameObject.SetActive(true);

        for (int i = 0; i < _spriteShape.spline.GetPointCount(); i++)
        {
            direction += 9.8f * Time.fixedDeltaTime * Vector2.down;
            _spriteShape.spline.SetPosition(i, 2f * i * Time.fixedDeltaTime * direction);
        }
    }
}
