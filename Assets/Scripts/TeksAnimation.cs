using UnityEngine;

public class TextBlink : MonoBehaviour
{
    public float blinkInterval = 1f; // Interval waktu berkedip lambat (dalam detik)
    public float fastBlinkInterval = 0.2f; // Interval waktu berkedip cepat setelah menyentuh layar (dalam detik)

    private float nextBlinkTime; // Waktu berikutnya untuk berkedip
    private bool isFastBlinking; // Apakah sedang berkedip cepat setelah menyentuh layar

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        nextBlinkTime = Time.time + blinkInterval;
    }

    private void Update()
    {
        // Cek apakah saatnya untuk berkedip
        if (Time.time >= nextBlinkTime)
        {
            // Toggle keadaan visibility teks
            canvasGroup.alpha = 1 - canvasGroup.alpha;

            // Mengatur waktu berikutnya untuk berkedip
            if (isFastBlinking)
                nextBlinkTime = Time.time + fastBlinkInterval;
            else
                nextBlinkTime = Time.time + blinkInterval;
        }

        // Cek input sentuhan layar
        if (Input.touchCount > 0)
        {
            // Cek apakah ada sentuhan pertama di layar
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Toggle keadaan berkedip cepat
                isFastBlinking = !isFastBlinking;

                // Mengatur waktu berikutnya untuk berkedip
                nextBlinkTime = Time.time + (isFastBlinking ? fastBlinkInterval : blinkInterval);
            }
        }
    }
}
