using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FixCollisionPosition : MonoBehaviour
{
    private List<ContactPoint2D> _contactPoints = new List<ContactPoint2D>();

    private List<Collider2D> _colliders = new List<Collider2D>();

    [SerializeField]
    private Rigidbody2D rb;

    private void FixedUpdate()
    {
        rb.GetContacts(_contactPoints);

        var contactPoint2D = _contactPoints.FirstOrDefault(contactPoint => _colliders.Contains(contactPoint.collider));

        if (contactPoint2D.normal.x == 0
            && contactPoint2D.normal.y > 0
            && contactPoint2D.enabled
            && Mathf.Abs(rb.velocity.y) < 0.02f)
        {
            var posY = contactPoint2D.collider.transform.position.y;
            var scaleY = contactPoint2D.collider.transform.lossyScale.y;

            var pointExpected = posY + scaleY / 2;

            var diff = pointExpected - contactPoint2D.point.y;

            if (Mathf.Abs(diff) > 0)
            {
                rb.velocity = new Vector3(
                    rb.velocity.x,
                    0
                );

                transform.position = new Vector2(
                    transform.position.x,
                    transform.position.y + diff
                );
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.enabled)
        {
            _colliders.Add(other.collider);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (_colliders.Contains(other.collider))
        {
            _colliders.Remove(other.collider);
        }
    }
}
