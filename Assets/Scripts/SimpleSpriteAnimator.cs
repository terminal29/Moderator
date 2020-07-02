using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SimpleSpriteAnimator : MonoBehaviour
{
    private Image imageComponent;
    public List<Sprite> images;
    int spriteIndex;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        imageComponent = GetComponent<Image>();
        StartCoroutine(AnimateSprite());
    }

    private IEnumerator AnimateSprite()
    {
        while (true)
        {
            imageComponent.sprite = images[spriteIndex];
            spriteIndex++;
            spriteIndex %= images.Count;
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
