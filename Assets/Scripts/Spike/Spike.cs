using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    // kill player 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit spike!");
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // when spike should break
    public void BreakSpike()
    {
        Debug.Log("Spike destroyed: " + gameObject.name);
        Destroy(gameObject);
    }
}