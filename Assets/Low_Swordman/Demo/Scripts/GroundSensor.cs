using UnityEngine;
using UnityEngine.Serialization;

namespace Low_Swordman.Demo.Scripts
{
    public class GroundSensor : MonoBehaviour
    {
        [FormerlySerializedAs("m_root")]
        public PlayerController mRoot;

        // Use this for initialization
        private void Start()
        {
            mRoot = transform.root.GetComponent<PlayerController>();
        }

        private ContactPoint2D[] _contacts = new ContactPoint2D[1];

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Ground") || other.CompareTag("Block"))
            {
                if (other.CompareTag("Ground"))
                {
                    mRoot.isDownJumpGroundCheck = true;
                }
                else
                {
                    mRoot.isDownJumpGroundCheck = false;
                }

                if (mRoot.mRigidbody.velocity.y <= 0)
                {
                    mRoot.isGrounded = true;
                    mRoot.currentJumpCount = 0;
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            mRoot.isGrounded = false;
        }
    }
}
