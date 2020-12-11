using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentPlayer
{
    Kanith,
    Aramele,
    Percival,
    Rolan
}

public class PlayerSwitch : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    [HideInInspector]
    public CurrentPlayer currentCharacter = CurrentPlayer.Kanith;
    public Vector2 prevDirection;

    private AudioSource noise;

    // Start is called before the first frame update
    void Start()
    {
        noise = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("swap character"))
        {
            PlayerSwap();
            noise.Play();
        }
    }

    private void PlayerSwap()
    {
        Vector3 charPosition = gameObject.transform.GetChild(0).position;
        prevDirection = gameObject.GetComponentInChildren<Player>().prevDirection;

        Destroy(transform.GetChild(0).gameObject);
        currentCharacter = currentCharacter + 1;
        if((int)currentCharacter > 3)
        {
            currentCharacter = 0;
        }
        GameObject newCharacter = playerPrefabs[(int)currentCharacter];
        GameObject refToPlayer = Instantiate(newCharacter, charPosition, Quaternion.identity);

        refToPlayer.transform.parent = transform;
    }
}
