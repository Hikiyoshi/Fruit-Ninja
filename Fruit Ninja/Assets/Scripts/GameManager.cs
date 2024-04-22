using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Image fadeImage;
    //Khai báo điểm
    private int score;

    private Blade blade;
    private Spawner spawner;

    private void Awake()
    {
        //Lấy lớp Blade và Spawner
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }
    //Khi bắt đầu sẽ gọi hàm NewGame()
    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        //Chỉnh thời gian chạy game lại bình thường
        Time.timeScale = 1f;
        //enable cho blade và spawner
        blade.enabled = true;
        spawner.enabled = true;
        //Reset điểm và đặt lên màn hình
        score = 0;
        scoreText.text = score.ToString();
        //Gọi hàm Clear
        ClearSence();
    }

    private void ClearSence()
    {
        //Lấy tất cả đối tượng Fruit đang có trên Scene và xoá chúng
        Fruit[] fruits = FindObjectsOfType<Fruit>();
        foreach(Fruit fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }
        //Lấy tất cả đối tượng Bomb  đang có trên Scene và xoá chúng
        Bomb[] bombs = FindObjectsOfType<Bomb>();
        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }
    //Hàm tăng điểm và in điểm lên màn hình
    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }
    //Hàm xử lý khi chém bom
    public void Explode()
    {
        //disable blade và spawner để dừng game
        blade.enabled = false;
        spawner.enabled = false;
        //Gọi hàm ExplodeSequence bằng Coroutine
        StartCoroutine(ExplodeSequence());
    }

    public IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;
        //Code xử lý để flash màn hình trắng
        while(elapsed < duration)
        {
            //Tính toán tỉ lệ thời gian trôi qua trong giới hạn 0 đến 1
            float t = Mathf.Clamp01(elapsed / duration);
            //Hàm chỉnh ảnh để nó chuyển dần theo tỉ lệ thời gian (t)(theo độ trong suốt)
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
        //Đợi 1 giây sau bắt để bắt đầu trò chơi
        yield return new WaitForSecondsRealtime(1f);
        //Gọi hàm NewGame để Reset lại điểm và timeScale
        NewGame();

        elapsed = 0f;
        //Xử lý màn hình trắng trở lại màn hình game như cũ
        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }
}
