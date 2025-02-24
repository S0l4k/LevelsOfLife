using UnityEngine;
using System.Collections;

public class GraveManager : MonoBehaviour
{
    public GameObject F; 
    public static bool isInRange; 
    private DziadController movePlayer; 
    public GameObject Flowers; 
    public FadeManager fadeManager; 
    public GameObject nextObject; 
    public GameObject EndScreen;

    private bool hasInteracted = false; 

    private void Start()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player"); 
        if (player != null)
        {
            movePlayer = player.GetComponent<DziadController>(); 
        }
        else
        {
            Debug.LogError("Gracz o tagu 'Player' nie został znaleziony!");
        }

        
        if (Flowers != null)
        {
            Flowers.SetActive(false);
        }
        else
        {
            Debug.LogError("Obiekt 'Flowers' nie jest przypisany!");
        }

        
        if (fadeManager == null)
        {
            Debug.LogError("FadeManager nie jest przypisany!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            F.SetActive(true); 
        }
        isInRange = true; 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            F.SetActive(false); 
        }
        isInRange = false; 
        hasInteracted = false; 
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F) && isInRange && !hasInteracted)
        {
            if (movePlayer != null)
            {
                movePlayer.initialMovementSpeed = 0f;
            }
            else
            {
                Debug.LogError("Skrypt 'DziadController' nie został znalezione na graczu!");
            }

            if (Flowers != null)
            {
                Flowers.SetActive(true); 
            }
            else
            {
                Debug.LogError("Obiekt 'Flowers' nie jest przypisany!");
            }

           
            StartCoroutine(FadeOutAndTransition());
            hasInteracted = true; 
        }
    }

    private IEnumerator FadeOutAndTransition()
    {
        
        if (fadeManager != null)
        {
            fadeManager.FadeOut(); 
            yield return new WaitForSeconds(fadeManager.fadeDuration); 
        }

       
        if (movePlayer != null)
        {
            movePlayer.gameObject.SetActive(false);
        }

        
        yield return new WaitForSeconds(2f);
        if (nextObject != null)
        {
            nextObject.SetActive(true);
        }
       
        if (fadeManager != null)
        {
            fadeManager.FadeIn(); 
            yield return new WaitForSeconds(fadeManager.fadeDuration); 
        }

        yield return new WaitForSeconds(5);
        EndScreen.SetActive(true);

        yield return new WaitForSeconds(5);

        Application.Quit();
        

    }
}