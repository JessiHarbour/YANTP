using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject winTextUI;
    public Tile_Logic.TileInteraction tileInteraction;

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            Time.timeScale = 0f;

            if (tileInteraction != null)
                tileInteraction.gameStarted = false;

            if (winTextUI != null)
                winTextUI.SetActive(true);
        }
    }
}