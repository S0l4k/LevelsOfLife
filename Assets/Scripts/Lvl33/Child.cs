using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using TMPro; 

public class Child : MonoBehaviour
{
    public float stressLevel = 0f; 
    public float maxStressLevel = 100f; 
    public float stressIncreaseRate = 1f; 
    public bool hasReceivedCake = false; 

    private SpriteRenderer spriteRenderer; 
    
    public Image uiChildImage; 
    public TMP_Text uiStressText; 
    public TMP_Text loseMessageText; 

    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer nie zosta� znaleziony na obiekcie dziecka!");
        }

        
        if (loseMessageText != null)
        {
            loseMessageText.enabled = false;
        }
    }

    void Update()
    {
        if (!hasReceivedCake)
        {
            
            stressLevel += stressIncreaseRate * Time.deltaTime;

            
            stressLevel = Mathf.Min(stressLevel, maxStressLevel);

            
            if (spriteRenderer != null)
            {
                Color newColor = Color.Lerp(Color.white, Color.red, stressLevel / maxStressLevel);
                spriteRenderer.color = newColor;
            }

            
            UpdateUI();

            
            if (stressLevel >= maxStressLevel)
            {
                Debug.LogError("Dziecko osi�gn�o maksymalny poziom zdenerwowania!");
                GameOver();
            }
        }
    }

    
    public void DeliverCake()
    {
        if (!hasReceivedCake)
        {
            hasReceivedCake = true;
            Debug.Log("Tort dostarczony! Dziecko jest szcz�liwe!");
            stressLevel = 0f; 

            
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.white;
            }

            
            UpdateUI();
        }
    }

    
    private void GameOver()
    {
        Debug.LogError("Dziecko jest zdenerwowane! Przegra�e�!");

        
        enabled = false;

        
        ShowLoseMessage();

        
        Invoke("ReloadScene", 2f); 
    }

    
    private void UpdateUI()
    {
        if (uiChildImage != null)
        {
            
            Color newColor = Color.Lerp(Color.white, Color.red, stressLevel / maxStressLevel);
            uiChildImage.color = newColor;
        }

        if (uiStressText != null)
        {
            
            uiStressText.text = "Wkurzenie: " + Mathf.RoundToInt(stressLevel) + "%";
        }
    }

    
    private void ShowLoseMessage()
    {
        if (loseMessageText != null)
        {
            loseMessageText.enabled = true; 
            loseMessageText.text = "PRZEGRA�E�! Dzieci umar�y"; 
        }
    }

    
    private void ReloadScene()
    {
        
        if (loseMessageText != null)
        {
            loseMessageText.enabled = false;
        }

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}