using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    public enum eType{center,inset,outset};
    [System.Flags]
    public enum eScreenLocs
    {
        onScreen = 0, //0000 in binary
        offRight = 1, //0001
        offLeft  = 2, //0010
        offUp    = 4, //0100
        offDown  = 8, //1000
    }
    [Header("Inscribed")]
    public eType boundsType = eType.center;
    public float radius = 1f;
    public bool keepOnScreen;

    [Header("Dynamic")]
    public eScreenLocs screenLocs = eScreenLocs.onScreen;
    //public bool isOnScreen;
    public float camWidth;
    public float camHeight;

    // Start is called before the first frame update
    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth  = camHeight* Camera.main.aspect;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float checkRadius=0;
        if(boundsType==eType.inset) checkRadius = -radius;
        if(boundsType==eType.outset) checkRadius = radius;

        Vector3 pos = transform.position;
        screenLocs = eScreenLocs.onScreen;//isOnScreen = true;

        //restrict the X position to camWidth
        if(pos.x > camWidth + checkRadius)
        {
            pos.x = camWidth + checkRadius;
            screenLocs |= eScreenLocs.offRight;//isOnScreen = false;
        }
        if(pos.x < -camWidth - checkRadius)
        {
            pos.x = -camWidth - checkRadius;
            screenLocs |= eScreenLocs.offLeft;//isOnScreen = false;
        }

        //Restrict the Y position to camHeight
        if(pos.y > camHeight + checkRadius)
        {
            pos.y = camHeight + checkRadius;
            screenLocs |= eScreenLocs.offUp;//isOnScreen = false;
        }
        if(pos.y < -camHeight - checkRadius)
        {
            pos.y = -camHeight - checkRadius;
            screenLocs |= eScreenLocs.offDown;//isOnScreen = false;
        }
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            screenLocs = eScreenLocs.onScreen;//isOnScreen = true;
        }
        
    }
    public bool isOnScreen {  get { return (screenLocs == eScreenLocs.onScreen); } }
    public bool LocIs(eScreenLocs checkLoc)
    {
        if (checkLoc == eScreenLocs.onScreen) return isOnScreen;
        return ((screenLocs & checkLoc) == checkLoc);
    }
}
