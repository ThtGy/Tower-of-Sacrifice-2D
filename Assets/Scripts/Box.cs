using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Animator ani;
    private AudioSource openSound;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        openSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        if (ani.GetBool("Open") == false)
        {
            openSound.Play();
            ani.SetBool("Open", true);
        }
    }
}
