using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip deathClip; 

    public float jumpForce = 700f;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private AudioSource playerAudio;
    
    void Start()
    {
        //게임 오브젝트로부터 사용할 컴포넌트들을 가져와 변수에 할당
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            //사망 시 처리를 더 이상 진행하지 않고 종료
            return;
        }

        //마우스 왼쪽 버튼을 눌르고 최대 점프 횟수에 도달하지 않았다면
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            //점프 횟수 증가
            jumpCount++;
            //점프 직전에 속도를 순간적으로 제로로 변경
            playerRigidbody.linearVelocity = Vector2.zero;
            //리지드바디에 위쪽으로 힘을 주기
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            //오디오 소스 재생
            playerAudio.Play();
        }
        else if (Input.GetMouseButtonUp(0) && playerRigidbody.linearVelocity.y > 0)
        {
            //마우스 왼쪽 버튼에서 손을 떼는 순간 그리고 속도의 y 값이 양수라면 (위로 상승)
            //현재 속도를 절반으로 변경
            playerRigidbody.linearVelocity = playerRigidbody.linearVelocity * 0.5f; // Reduce upward velocity when mouse button is released
        }
        //애니메이터의 Grounded 파라미터를 isGrounded 값으로 갱신
        animator.SetBool("Grounded", isGrounded);
        
    }
}
