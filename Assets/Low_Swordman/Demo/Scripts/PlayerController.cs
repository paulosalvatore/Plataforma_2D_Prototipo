using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Low_Swordman.Demo.Scripts
{
    public abstract class PlayerController : MonoBehaviour
    {
        [FormerlySerializedAs("IsSit")]
        public bool isSit;
        public int currentJumpCount;
        public bool isGrounded;
        [FormerlySerializedAs("OnceJumpRayCheck")]
        public bool onceJumpRayCheck;

        [FormerlySerializedAs("Is_DownJump_GroundCheck")]
        public bool isDownJumpGroundCheck; // 다운 점프를 하는데 아래 블록인지 그라운드인지 알려주는 불값
        protected float MMoveX;
        [FormerlySerializedAs("m_rigidbody")]
        public Rigidbody2D mRigidbody;
        protected CapsuleCollider2D MCapsulleCollider;
        protected Animator MAnim;

        [FormerlySerializedAs("MoveSpeed")]
        [Header("[Setting]")]
        public float moveSpeed = 6;

        [FormerlySerializedAs("JumpCount")]
        public int jumpCount = 2;
        public float jumpForce = 15f;

        protected void AnimUpdate()
        {
            if (!MAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    MAnim.Play("Attack");
                }
                else
                {
                    if (MMoveX == 0)
                    {
                        if (!onceJumpRayCheck)
                        {
                            MAnim.Play("Idle");
                        }
                    }
                    else
                    {
                        MAnim.Play("Run");
                    }
                }
            }
        }

        protected void Filp(bool bLeft)
        {
            transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1);
        }

        protected void PrefromJump()
        {
            MAnim.Play("Jump");

            mRigidbody.velocity = new Vector2(0, 0);

            mRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            onceJumpRayCheck = true;
            isGrounded = false;

            currentJumpCount++;
        }

        protected void DownJump()
        {
            if (!isGrounded)
            {
                return;
            }

            if (!isDownJumpGroundCheck)
            {
                MAnim.Play("Jump");

                mRigidbody.AddForce(-Vector2.up * 10);
                isGrounded = false;

                MCapsulleCollider.enabled = false;

                StartCoroutine(GroundCapsulleColliderTimmerFuc());
            }
        }

        private IEnumerator GroundCapsulleColliderTimmerFuc()
        {
            yield return new WaitForSeconds(0.3f);

            MCapsulleCollider.enabled = true;
        }

        //////바닥 체크 레이케스트
        private Vector2 _rayDir = Vector2.down;

        private float _pretmpY;
        private float _groundCheckUpdateTic;
        private float _groundCheckUpdateTime = 0.01f;

        protected void GroundCheckUpdate()
        {
            if (!onceJumpRayCheck)
            {
                return;
            }

            _groundCheckUpdateTic += Time.deltaTime;

            if (_groundCheckUpdateTic > _groundCheckUpdateTime)
            {
                _groundCheckUpdateTic = 0;

                if (_pretmpY == 0)
                {
                    _pretmpY = transform.position.y;

                    return;
                }

                var reY = transform.position.y - _pretmpY; //    -1  - 0 = -1 ,  -2 -   -1 = -3

                if (reY <= 0)
                {
                    if (isGrounded)
                    {
                        LandingEvent();
                        onceJumpRayCheck = false;
                    }
                    else
                    {
                        Debug.Log("안부딪힘");
                    }
                }

                _pretmpY = transform.position.y;
            }
        }

        protected abstract void LandingEvent();
    }
}
