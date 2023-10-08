using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour
{

    float normalJumpVel, normalVel;

    public AudioClip mudSFX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player p = other.GetComponent<Player>();

            normalJumpVel = p.getJumpVel();
            p.setJumpVel(normalJumpVel - 2);

            normalVel = p.velHorizontal;
            p.velHorizontal = 1;
            if (SoundManager.Instance.AudioIsPlaying())
            {
                if(SoundManager.Instance.AudioClip() != mudSFX)
                {
                    SoundManager.Instance.SelectAudio(3, 0.5f);
                }
            }
            else SoundManager.Instance.SelectAudio(3, 0.5f);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player p = other.GetComponent<Player>();
            p.velHorizontal = normalVel;
            p.setJumpVel(normalJumpVel);
        }
    }
}
