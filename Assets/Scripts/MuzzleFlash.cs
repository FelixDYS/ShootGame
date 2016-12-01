using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour
{
    public GameObject flashHolder;
    public Sprite[] flashSprites;

    public SpriteRenderer[] spriteRenders;
    public float flashTime;

    void Start()
    {
        Deactivate();
    }
    public void Activate()
    {
        flashHolder.SetActive(true);

        int flashSpriteIndex = Random.Range(0, flashSprites.Length);
        for (int i = 0; i < spriteRenders.Length; i++)
        {
            spriteRenders[i].sprite = flashSprites[flashSpriteIndex];
        }
        Invoke("Deactivate", flashTime);
    }

    void Deactivate()
    {
        flashHolder.SetActive(false);
    }
}
