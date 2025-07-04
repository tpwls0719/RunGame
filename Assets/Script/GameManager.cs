using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening; // DoTween 네임스페이스 추가

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤 인스턴스

    public bool isGameOver = false; // 게임 오버 상태
    public TextMeshProUGUI scoreText; // 점수 텍스트 UI
    public GameObject gameOverUI; // 게임 오버 UI 패널

    private int score = 0; // 현재 점수

    //게임 시작과 동시에 싱글톤을 구성
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject); // 이미 인스턴스가 존재하면 현재 게임 오브젝트를 파괴
        }
    }

    void Start()
    {
        SetUIAlpha(scoreText, 0f);
        SetUIAlpha(gameOverUI, 0f);
        scoreText.DOFade(1f, 1f); // 점수 텍스트 1초 동안 서서히 보이기
        gameOverUI.SetActive(false); // 게임 오버 UI는 처음엔 숨김
    }

    void Update()
    {
        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            //게임오버 상태에서 마우스 왼쪽 버튼을 클릭하면 현재 씬 재시작
            SceneManager.LoadScene(0);
        }
    }

    // 점수 텍스트 알파값 설정
    void SetUIAlpha(TextMeshProUGUI text, float alpha)
    {
        var color = text.color;
        color.a = alpha;
        text.color = color;
    }

    // 게임 오버 UI 알파값 설정 (CanvasGroup 필요)
    void SetUIAlpha(GameObject obj, float alpha)
    {
        var canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = obj.AddComponent<CanvasGroup>();
        canvasGroup.alpha = alpha;
    }

    //점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        //게임 오버가 아니라면
        if (!isGameOver)
        {
            //점수를 증가
            score += newScore;
            scoreText.text = "Score: " + score; // UI 업데이트
        }
    }

    //플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        //현재 상태를 게임 오버 상태로 변경
        isGameOver = true;
        gameOverUI.SetActive(true); // 게임 오버 UI 활성화
        var canvasGroup = gameOverUI.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameOverUI.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, 1f); // 1초 동안 서서히 보이게
    }
}
