using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tombstone : MonoBehaviour
{
    public GameObject dialogBox;
    public GameObject holePrefab;
    public Text dialogText;
    public string dialog;
    public bool showDialog;
    public bool triggerFall;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("interact") && showDialog)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                if(triggerFall)
                {
                    CreateHole();
                }
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            showDialog = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            showDialog = false;
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
                if(triggerFall)
                {
                    CreateHole();
                }
            }
        }
    }

    private void CreateHole()
    {
        GameObject playerRef = GameObject.FindWithTag("Player");
        Vector3 playerPos = playerRef.transform.position;
        Vector3 holePos = new Vector3(playerPos.x, playerPos.y - 0.5f, playerPos.z);
        GameObject hole = Instantiate(holePrefab, holePos, Quaternion.identity);
    }
}
