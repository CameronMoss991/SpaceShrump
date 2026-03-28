using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoundsCheck))]
public class ProjectileHero : MonoBehaviour
{
    private BoundsCheck bndcheck;
    // Start is called before the first frame update
    void Awake()
    {
        bndcheck = GetComponent<BoundsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bndcheck.LocIs(BoundsCheck.eScreenLocs.offUp))
        {
            Destroy(gameObject);
        }
        
    }
}
