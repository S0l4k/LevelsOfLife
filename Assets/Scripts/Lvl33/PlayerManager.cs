using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    public float moveSpeed = 5f; 
    public GameObject cakePrefab; 
    private GameObject currentCake; 

    
    public Child[] children;

    
    public TMP_Text winMessageText;

    private void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        transform.Translate(movement.normalized * moveSpeed * Time.deltaTime);

        
        if (Input.GetKeyDown(KeyCode.Space) && currentCake == null)
        {
            SpawnCake();
        }

        
        if (Input.GetKeyDown(KeyCode.E) && currentCake != null)
        {
            DeliverCake();
        }
    }

    
    private void SpawnCake()
    {
        if (cakePrefab != null)
        {
            currentCake = Instantiate(cakePrefab, transform.position, Quaternion.identity);
        }
    }

    
    private void DeliverCake()
    {
        if (currentCake != null)
        {
            Destroy(currentCake); 
            currentCake = null;

            
            foreach (Child child in children)
            {
                if (Vector2.Distance(transform.position, child.transform.position) < 1f && !child.hasReceivedCake)
                {
                    child.DeliverCake(); 
                    CheckIfAllChildrenAreHappy(); 
                    break;
                }
            }
        }
    }

    
    private void CheckIfAllChildrenAreHappy()
    {
        bool allHappy = true;
        foreach (Child child in children)
        {
            if (!child.hasReceivedCake)
            {
                allHappy = false;
                break;
            }
        }

        if (allHappy)
        {
            GameOver(true); 
        }
    }

    
    private void GameOver(bool isWin)
    {
        enabled = false; 

        if (isWin)
        {
            
            ShowWinMessage();

            
            Invoke("LoadNextScene", 3f);
        }
        else
        {
            Debug.Log("Przegrałeś!");
        }
    }

    
    private void ShowWinMessage()
    {
        if (winMessageText != null)
        {
            winMessageText.enabled = true; 
            winMessageText.text = "WYGRAŁEŚ! Wszystkie dzieci są szczęśliwe!"; 
        }
    }

    
    private void LoadNextScene()
    {
       
        SceneManager.LoadScene(6);
    }
}