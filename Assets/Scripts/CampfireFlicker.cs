using UnityEngine;

public class CampfireFlicker : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;

    public float switchTime = 0.2f;

    private SpriteRenderer sr;
    private float timer;
    private bool toggle;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchTime)
        {
            timer = 0f;
            toggle = !toggle;

            sr.sprite = toggle ? sprite1 : sprite2;
        }
    }
}