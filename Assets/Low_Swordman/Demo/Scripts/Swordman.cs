using UnityEngine;

namespace Low_Swordman.Demo.Scripts
{
    public class Swordman : PlayerController
    {
        private void Start()
        {
            MCapsulleCollider = transform.GetComponent<CapsuleCollider2D>();
            MAnim = transform.Find("model").GetComponent<Animator>();
            mRigidbody = transform.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CheckInput();

            if (mRigidbody.velocity.magnitude > 30)
            {
                mRigidbody.velocity = new Vector2(mRigidbody.velocity.x - 0.1f, mRigidbody.velocity.y - 0.1f);
            }
        }

        public void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.S)) //아래 버튼 눌렀을때.
            {
                isSit = true;
                MAnim.Play("Sit");
            }
            else if (Input.GetKeyUp(KeyCode.S))
            {
                MAnim.Play("Idle");
                isSit = false;
            }

            // sit나 die일때 애니메이션이 돌때는 다른 애니메이션이 되지 않게 한다.
            if (MAnim.GetCurrentAnimatorStateInfo(0).IsName("Sit") ||
                MAnim.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (currentJumpCount < jumpCount) // 0 , 1
                    {
                        DownJump();
                    }
                }

                return;
            }

            MMoveX = Input.GetAxis("Horizontal");

            GroundCheckUpdate();

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

            if (Input.GetKey(KeyCode.Alpha1))
            {
                MAnim.Play("Die");
            }

            // 기타 이동 인풋.

            if (Input.GetKey(KeyCode.D))
            {
                if (isGrounded) // 땅바닥에 있었을때.
                {
                    if (MAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        return;
                    }

                    transform.transform.Translate(Vector2.right * MMoveX * moveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.transform.Translate(new Vector3(MMoveX * moveSpeed * Time.deltaTime, 0, 0));
                }

                if (MAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    return;
                }

                if (!Input.GetKey(KeyCode.A))
                {
                    Filp(false);
                }
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (isGrounded) // 땅바닥에 있었을때.
                {
                    if (MAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        return;
                    }

                    transform.transform.Translate(Vector2.right * MMoveX * moveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.transform.Translate(new Vector3(MMoveX * moveSpeed * Time.deltaTime, 0, 0));
                }

                if (MAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    return;
                }

                if (!Input.GetKey(KeyCode.D))
                {
                    Filp(true);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (MAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    return;
                }

                if (currentJumpCount < jumpCount) // 0 , 1
                {
                    if (!isSit)
                    {
                        PrefromJump();
                    }
                    else
                    {
                        DownJump();
                    }
                }
            }
        }

        protected override void LandingEvent()
        {
            if (!MAnim.GetCurrentAnimatorStateInfo(0).IsName("Run") &&
                !MAnim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                MAnim.Play("Idle");
            }
        }
    }
}
