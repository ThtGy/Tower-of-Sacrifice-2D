using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public AudioSource intro, loop, bossIntro, bossLoop;

    private bool introStarted, bossIntroStarted;

    // Start is called before the first frame update
    void Start()
    {
        introStarted = false;
        bossIntroStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(introStarted == false && transform.position.y >= 0)
        {
            intro.Play();
            introStarted = true;
        }
        if (!intro.isPlaying && !loop.isPlaying && introStarted)
        {
            loop.Play();
        }

        //if camera is in boss room
        if(transform.position.x >= 44.75f && transform.position.x <= 48.25f
            && transform.position.y >= 17 && transform.position.y <= 27)
        {
            if (intro.isPlaying)
                intro.Stop();
            if (loop.isPlaying)
                loop.Stop();

            if(bossIntroStarted == false)
            {
                bossIntro.Play();
                bossIntroStarted = true;
            }
            else if(!bossIntro.isPlaying && !bossLoop.isPlaying)
            {
                bossLoop.Play();
            }
        }
    }
}
