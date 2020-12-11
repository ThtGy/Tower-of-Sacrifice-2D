using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallInHole : MonoBehaviour
{
    public AudioSource fallingSound;

    private CameraMovement cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().FallInHole(fallingSound);
            StartCoroutine(WaitForFall());
        }
    }

    private IEnumerator WaitForFall()
    {
        yield return new WaitForSeconds(1f);

        cam.maxPosition = new Vector2(48.25f, 27);
        cam.minPosition = new Vector2(44.75f, 17);
    }
}
