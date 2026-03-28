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

}
