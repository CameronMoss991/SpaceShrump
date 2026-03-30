using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
public class PowerUp : MonoBehaviour
{
    [Header("Inscribed")]
    [Tooltip("x holds a minimum, y a maximum for Random.Range() for the rotation speed of the PowerUp")]
    public Vector2 rotMinMax = new Vector2(4f, 8f);
    [Tooltip("x holds a minimum, y a maximum for Random.Range() for the lifeTime of the PowerUp")]
    public Vector2 driftMinMax = new Vector2(6f, 10f);
    public float lifeTime = 6f;
    public float fadeTime = 4f;
    [Header("Dynamic")]
    public eWeaponType _type;
    public GameObject cube;
    public TextMesh letter;
    public Vector3 rotPerSecond;
    public float birthTime;
    private Rigidbody rigid;
    private BoundsCheck bndCheck;
    private Material cubeMat;


    // Start is called before the first frame update
    void Awake()
    {
        cube = transform.GetChild(0).gameObject;
        letter = GetComponent<TextMesh>();
        rigid = GetComponent<Rigidbody>();
        bndCheck = GetComponent<BoundsCheck>();
        cubeMat = cube.GetComponent<Renderer>().material;
        Vector3 vel = Random.onUnitSphere;
        vel.z = 0;
        vel.Normalize();

        transform.rotation = Quaternion.identity;
        rotPerSecond = new Vector3(Random.Range(rotMinMax[0], rotMinMax[1]), Random.Range(rotMinMax[0], rotMinMax[1]), Random.Range(rotMinMax[0], rotMinMax[1]));
        birthTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        cube.transform.rotation = Quaternion.Euler(rotPerSecond * Time.time);
        float u = (Time.time - (birthTime + lifeTime)) / fadeTime;
        if (u >= 1)
        {
            Destroy(this.gameObject);
            return;
        }
        if (u > 0)
        {
            Color c = cubeMat.color;
            c.a = 1f - u;
            cubeMat.color = c;
            c = letter.color;
            c.a = 1f - u;
            letter.color = c;
        }
        if(!bndCheck.isOnScreen)
        {
            Destroy(gameObject);
        }
        
    }
    public eWeaponType type {get{return _type;} set{SetType(value);}}
    public void SetType(eWeaponType wt)
    {
        WeaponDefinition def = Main.GET_WEAPON_DEFINITION(wt);
        cubeMat.color = def.powerupColor;
        letter.text = def.letter;
        type = wt;

    }

    public void AbsorbedBy(GameObject target)
    {
        Destroy(this.gameObject);
    }

}
