using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Inscribed")]
    public Transform playerTrans;
    public Transform[] panels;
    [Tooltip("Speed at which the panels will move in response to the player's movement. Higher values will result in faster movement.")]
    public float motionMult = 0.25f;

    private float panelHt;
    private float depth;
    // Start is called before the first frame update
    void Start()
    {
        panelHt = panels[0].localScale.y;
        depth = panels[0].position.z;
        panels[0].position = new Vector3(0, 0, depth);
        panels[1].position = new Vector3(0, panelHt, depth);
    }

    // Update is called once per frame
    void Update()
    {
        float tY = 0, tX=0;
        if (playerTrans != null)
        {
            //tY += -playerTrans.position.y * motionMult;
            tX = -playerTrans.position.x * motionMult;
        }
        panels[0].position = new Vector3(tX, tY, depth);
        if(tY >=0)
        {
            panels[1].position = new Vector3(tX, tY - panelHt, depth);
        }
        else
        {
            panels[1].position = new Vector3(tX, tY + panelHt, depth);
        }
        
    }
}
