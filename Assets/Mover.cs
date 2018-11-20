using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public Rail rail;
    public PlayMode mode;

    public float speed = 2.5f;
    public bool isReversed;
    public bool isLooping;
    public bool pingPong;

    private int currentSeg;
    private float transistion;
    private bool isCompleted;

    private void Update()
    {
        if(!rail)
        {
            return;
        }

        if(!isCompleted)
        {
            Play(!isReversed);
        }
    }

    //plays transisition from the start with the amount of speed it will move
    private void Play(bool forward = true)
    {
        float m = (rail.nodes[currentSeg + 1].position - rail.nodes[currentSeg].position).magnitude;
        float s = (Time.deltaTime * 1 / m) * speed;
        transistion += (forward) ? s : -s;
        if(transistion > 1)
        {
            transistion = 0;
            currentSeg++;
            if(currentSeg == rail.nodes.Length - 1)
            {
                if(isLooping)
                {
                    if(pingPong)
                    {
                        transistion = 1;
                        currentSeg = rail.nodes.Length - 2;
                        isReversed = !isReversed;
                    }
                    else 
                    {
                        currentSeg = 0;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }
        }

        else if(transistion < 0)
        {
            transistion = 1;
            currentSeg--;

            if (currentSeg == -1)
            {
                if (isLooping)
                {
                    if (pingPong)
                    {
                        transistion = 0;
                        currentSeg = 0;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSeg = rail.nodes.Length - 2;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }
        }

        transform.position = rail.PositionOnRail(currentSeg, transistion, mode);
        transform.rotation = rail.Orientation(currentSeg, transistion);
    }
}
