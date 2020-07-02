using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 mouseOffset = new Vector2((mousePos.x / screenSize.x) - 0.5f, (mousePos.y / screenSize.y) - 0.5f);
        Vector3 newPosition = Vector3.Lerp(transform.position, initialPosition + new Vector3(mouseOffset.x * 2, mouseOffset.y * 2, 0), Time.deltaTime);
        transform.position = newPosition;
    }

    public void Shake(float amount, float duration)
    {
        StartCoroutine(Shaker(amount, duration));
    }

    private IEnumerator Shaker(float amount, float duration)
    {
        float now = Time.time;
        Vector3 oldPosition = transform.position;
        while (true)
        {
            if (Time.time > now + duration)
            {
                break;
            }
            transform.position = oldPosition + new Vector3(UnityEngine.Random.Range(-amount, amount), UnityEngine.Random.Range(-amount, amount), 0);
            yield return null;
        }
        transform.position = oldPosition;
    }
}
