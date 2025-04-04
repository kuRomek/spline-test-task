using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    private bool _hasGrounded;

    private void FixedUpdate()
    {
        if (_hasGrounded == false)
            transform.right = _rigidbody.velocity;
    }

    private void OnEnable()
    {
        _hasGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_hasGrounded)
            return;

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            _hasGrounded = true;
    }

    public void Throw(Vector2 direction)
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        _rigidbody.velocity = Vector3.zero;

        _rigidbody.AddForce(direction, ForceMode2D.Impulse);
        transform.right = _rigidbody.velocity;
    }
}
