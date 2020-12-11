using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 minCameraChange, maxCameraChange;
    public Vector3 playerChange;
    public bool showMoveText;
    public string locationName;
    public GameObject text;
    public Text locationText;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            cam.minPosition += minCameraChange;
            cam.maxPosition += maxCameraChange;
            collision.transform.position += playerChange;

            if(showMoveText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        locationText.text = locationName;
        yield return new WaitForSeconds(3f);
        text.SetActive(false);
    }
}
