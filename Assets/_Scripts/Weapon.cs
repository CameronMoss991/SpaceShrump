using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eWeaponType{none, blaster, spread, phaser, missile, laser, shield}
[System.Serializable]
public class WeaponDefinition{
    public eWeaponType type = eWeaponType.none;
    [Tooltip("Letter to show on the PowerUp Cube, e.g. B for Blaster")]
    public string letter;
    [Tooltip("The color of the PowerUp Cube and the letter")]
    public Color powerupColor = Color.white;
    [Tooltip("Prefab of weapon model attached to ship")]
    public GameObject weaponModelPrefab;
    [Tooltip("Prefab of the projectile to be fired")]
    public GameObject projectilePrefab;
    [Tooltip("Color of the projectile fired")]
    public Color projectileColor = Color.white;
    [Tooltip("Damage when the projectile hits a target")]
    public float damageOnHit = 0f;
    [Tooltip("Damage caused by the projectile per second while in contact with a target, e.g. for a laser")]
    public float damagePerSec = 0f;
    [Tooltip("Seconds to delay between shots")]
    public float delayBetweenShots = 0f;
    [Tooltip("Velocity of the projectile")]
    public float velocity = 50f;
}
public class Weapon : MonoBehaviour
{
    static public Transform PROJECTILE_ANCHOR;

    [Header("Dynamic")]
    [SerializeField]
    [Tooltip("Setting this manually while playing does not work properly")]
    private eWeaponType _type = eWeaponType.none;
    public WeaponDefinition def;
    public float nextShotTime;

    private GameObject weaponModel;
    private Transform shotPointTrans;

    void Awake() // Add Awake to handle the transform reference
    {
        shotPointTrans = transform; 
    }
    void Start()
    {
        if (PROJECTILE_ANCHOR == null)
        {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
            PROJECTILE_ANCHOR.position = Vector3.zero; //added
        }
        shotPointTrans = transform; //added
        SetType(_type);
        
        Hero hero = GetComponent<Hero>();
        if(Hero.S != null) Hero.S.fireEvent += Fire;
    }

    public eWeaponType type
    {
        get { return(_type);}
        set { SetType(value);}
    }

    public void SetType(eWeaponType wt)
    {
        _type = wt;
        if(type == eWeaponType.none)
        {
            this.gameObject.SetActive(false);
            return;
        } else
        {
            this.gameObject.SetActive(true);
        }

        def = Main.GET_WEAPON_DEFINITION(_type);
        if(weaponModel != null) Destroy(weaponModel);
        weaponModel = Instantiate<GameObject>(def.weaponModelPrefab, transform);
        weaponModel.transform.localPosition = Vector3.zero;
        weaponModel.transform.localScale = Vector3.one;

        nextShotTime = 0;
    }

    private void Fire()
    {
        if(!gameObject.activeInHierarchy) return;
        if(Time.time < nextShotTime) return;
        ProjectileHero p;
        Vector3 vel = Vector3.up * def.velocity;

        switch (type)
        {
            case eWeaponType.blaster:
                p=MakeProjectile();
                p.vel = vel;
                break;
            case eWeaponType.spread:
                p = MakeProjectile();
                p.vel = vel; 
                p = MakeProjectile();
                p.transform.rotation = Quaternion.AngleAxis(10,Vector3.back);
                p.vel = p.transform.rotation * vel;
                p = MakeProjectile();
                p.transform.rotation = Quaternion.AngleAxis(-10, Vector3.back);
                p.vel = p.transform.rotation * vel;
                break;
        }
    }

    private ProjectileHero MakeProjectile()
    {
        GameObject go;
        go = Instantiate<GameObject>(def.projectilePrefab,PROJECTILE_ANCHOR);
        ProjectileHero p = go.GetComponent<ProjectileHero>();

        Vector3 pos = shotPointTrans.position;
        pos.z = 0;
        p.transform.position = pos;

        p.type = type;
        nextShotTime = Time.time + def.delayBetweenShots;
        return(p);
    }

}
