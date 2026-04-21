using UnityEngine;

public class SimpleSpriteAnimation : MonoBehaviour
{
    public Sprite[] frames;
    public float frameRate = 0.2f;

    private SpriteRenderer sr;
    private int index = 0;
    private float timer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (frames.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= frameRate)
        {
            timer = 0f;

            index++;
            if (index >= frames.Length)
                index = 0;

            sr.sprite = frames[index];
        }
    }
}