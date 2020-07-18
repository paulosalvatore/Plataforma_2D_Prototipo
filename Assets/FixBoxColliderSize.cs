using UnityEngine;

public class FixBoxColliderSize : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        FixSize();
    }

    private void FixSize()
    {
        var edge = boxCollider2D.edgeRadius;
        var scale = transform.lossyScale;

        var originalSize = boxCollider2D.size;

        var scaleRatioX = scale.x;
        var scaleRatioY = scale.y;

        boxCollider2D.size = new Vector2(
            originalSize.x - (edge * 2 / scaleRatioX),
            originalSize.y - (edge * 2 / scaleRatioY)
        );
    }
}
