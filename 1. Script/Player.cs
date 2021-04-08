using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어의 움직임(이동, 점프)을 제어하는 Player.cs 스크립트
    
    static Player instance;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    public float maxSpeed = 10;
    public float jumpPower = 14;

    bool isJumping;

    private void Awake() {
        // DontDestroyOnLoad로 실행하여 모든 스테이지에서 동일한 플레이어 캐릭터를 사용할 수 있도록 함
        // 첫 번째 실행 시 인스턴스 설정
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);

        // 인스턴스가 이전에 사용하던 것과 다른 경우 이전 인스턴스를 제거한 후 새로운 인스턴스 설정
        } else if (instance != this) {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        isJumping = false;
    }

    private void Update() {
        // 좌, 우 이동 제어
        if (Input.GetButtonUp("Horizontal"))
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        if (Input.GetButton("Horizontal") && !(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))) {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;
        }

        // 점프 제어
        if (Input.GetButtonDown("Jump") && !isJumping) {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void FixedUpdate() {
        // 좌, 우 이동
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed * (-1))
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        // 점프, 레이캐스트를 사용해 콜라이더 충돌 감지
        if (rigid.velocity.y < 0) {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(1, 0, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2f, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null) {
                if (rayHit.distance < 1.5f) {
                    // Debug.Log(rayHit.distance);
                    isJumping = false;
                }
            }
        }
    }

    // 다른 스크립트에서 사용할 수 있는 Getter, Setter
    public float GetSpeed() {
        return maxSpeed;
    }
    public float GetJumpPower() {
        return jumpPower;
    }
    public void SetSpeed(float speed) {
        maxSpeed = speed;
    }
    public void SetJumpPower(float power) {
        jumpPower = power;
    }
}
