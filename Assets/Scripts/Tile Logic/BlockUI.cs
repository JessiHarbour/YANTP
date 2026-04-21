using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Tile_Logic
{
    public class BlockUI : MonoBehaviour
    {
        public Image[] blockImages;

        public Color availableColor = Color.white;
        public Color usedColor = new Color(0f, 0f, 0f, 0.5f);

        public float popScale = 0.8f;
        public float popDuration = 0.13f;

        public void UpdateUI(int blocksUsed)
        {
            for (int i = 0; i < blockImages.Length; i++)
            {
                if (i < blocksUsed)
                {
                    // if block just used animate it
                    if (blockImages[i].color != usedColor)
                    {
                        StartCoroutine(Pop(blockImages[i].transform));
                    }

                    blockImages[i].color = usedColor;
                }
                else
                {
                    blockImages[i].color = availableColor;
                }
            }
        }

        IEnumerator Pop(Transform target)
        {
            Vector3 originalScale = Vector3.one;
            Vector3 targetScale = Vector3.one * popScale;

            float time = 0f;

            // scale up
            while (time < popDuration)
            {
                target.localScale = Vector3.Lerp(originalScale, targetScale, time / popDuration);
                time += Time.deltaTime;
                yield return null;
            }

            time = 0f;

            // scale  down
            while (time < popDuration)
            {
                target.localScale = Vector3.Lerp(targetScale, originalScale, time / popDuration);
                time += Time.deltaTime;
                yield return null;
            }

            target.localScale = originalScale;
        }
    }
}