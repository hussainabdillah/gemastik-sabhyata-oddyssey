using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimation : MonoBehaviour
{
    public float blinkInterval = 1f; // Interval berkedip awal dalam detik
    public float blinkSpeedupMultiplier = 2f; // Faktor percepatan berkedip ketika layar ditekan

    private float currentBlinkInterval; // Interval berkedip saat ini
    private bool isScreenPressed; // Apakah layar ditekan saat ini

    private float timer; // Waktu yang terlewati sejak animasi terakhir kali berkedip

    // Start is called before the first frame update
    void Start()
    {
        currentBlinkInterval = blinkInterval;
        timer = 0f;

        // Memulai animasi berkedip
        StartCoroutine(BlinkText());
    }

    // Update is called once per frame
    void Update()
    {
        // Memeriksa apakah layar ditekan
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            isScreenPressed = true;
        }
        else
        {
            isScreenPressed = false;
        }

        // Mengubah kecepatan berkedip berdasarkan keadaan layar yang ditekan
        if (isScreenPressed)
        {
            currentBlinkInterval = blinkInterval / blinkSpeedupMultiplier;
        }
        else
        {
            currentBlinkInterval = blinkInterval;
        }
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            // Menghitung waktu yang terlewati sejak animasi terakhir kali berkedip
            timer += Time.deltaTime;

            // Memeriksa apakah sudah waktunya untuk berkedip
            if (timer >= currentBlinkInterval)
            {
                // Mengubah status teks (aktif/mati)
                gameObject.SetActive(!gameObject.activeSelf);

                // Mengatur ulang timer
                timer = 0f;
            }

            yield return null;
        }
    }
}
