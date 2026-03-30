using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(BoundsCheck))]
public class ProjectileHero : MonoBehaviour
{
    private BoundsCheck bndcheck;
    private Renderer rend;

    [Header("Dynamic")]
    public Rigidbody rigid;
    [SerializeField]
    private eWeaponType _type;

    public eWeaponType type
    {
        get{return(_type);}
        set{SetType(value);}
    }

    // Start is called before the first frame update
    void Awake()
    {
        bndcheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bndcheck.LocIs(BoundsCheck.eScreenLocs.offUp))
        {
            Destroy(gameObject);
        }
        
    }

    public void SetType(eWeaponType eType)
    {
        _type = eType;
        
        // Safety check: If Awake hasn't run yet, grab the renderer now
        if (rend == null) rend = GetComponent<Renderer>();
        
        WeaponDefinition def = Main.GET_WEAPON_DEFINITION(_type);
        rend.material.color = def.projectileColor;
    }
    public Vector3 vel
    {
        get {
            if (rigid == null) rigid = GetComponent<Rigidbody>();
            return rigid.velocity; 
        }
        set {
            if (rigid == null) rigid = GetComponent<Rigidbody>();
            rigid.velocity = value; 
        }
    }
}
