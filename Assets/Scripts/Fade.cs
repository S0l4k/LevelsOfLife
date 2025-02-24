using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public float fadeDuration = 2f; // Czas trwania efektu fade
    private Image fadeImage; // Referencja do Image odpowiadającego za fade

    private void Start()
    {
        // Pobierz referencję do Image
        fadeImage = GetComponent<Image>();
        if (fadeImage == null)
        {
            Debug.LogError("Brak komponentu Image na tym obiekcie!");
        }
    }

    // Metoda do wykonania fade-out
    public void FadeOut()
    {
        StartCoroutine(FadeTo(1f)); // Zmiana przezroczystości na 1 (pełna czern)
    }

    // Metoda do wykonania fade-in
    public void FadeIn()
    {
        StartCoroutine(FadeTo(0f)); // Zmiana przezroczystości na 0 (cały widoczny)
    }

    // Coroutine do zmiany przezroczystości
    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a; // Aktualna przezroczystość
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.unscaledDeltaTime; // Używamy unscaledDeltaTime, aby nie zależało od pauzy gry
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            yield return null;
        }

        // Upewnij się, że końcowy stan jest precyzyjny
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
    }
}