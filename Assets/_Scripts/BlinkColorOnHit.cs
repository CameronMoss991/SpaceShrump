using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkColorOnHit : MonoBehaviour
{
    private static float blinkDuration=0.1f; // Duration of the blink effect in seconds
    private static Color blinkColor=Color.red; // Color to blink to when hit
    [Header("Dynamic")]
    private bool showingColor=false; // Flag to indicate if the blink color is currently being shown
    public float blinkCompleteTime=0f; // Time at which the blink effect should end

    private Material[] materials; // Array to hold the materials of the object
    private Color[] originalColors; // Array to hold the original colors of the materials
    private BoundsCheck bndCheck;

    // Awake is called when the script instance is first loaded
    void Awake()
    {
        bndCheck = GetComponentInParent<BoundsCheck>();    

        materials = Utils.GetAllMaterials(gameObject); // Get all materials of the object
        originalColors = new Color[materials.Length]; // Initialize the array to hold original colors
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color; // Store the original color of each material
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(showingColor && Time.time > blinkCompleteTime) RevertColors();
    }

    void OnCollisionEnter(Collision coll)
    {
        ProjectileHero p = coll.gameObject.GetComponent<ProjectileHero>(); // Check if the colliding object is a projectile
        if (p != null)
        {
            if(bndCheck != null && bndCheck.isOnScreen)
            {
                return; // If the object is on screen, do not show the blink color
            }
            SetColors(); // Show the blink color when hit by a projectile
        }
        
    }
    void SetColors()
    {
        foreach(Material m in materials)
        {
            m.color = blinkColor; // Set each material's color to the blink color
        }
        showingColor = true; // Set the flag to indicate that the blink color is being shown
        blinkCompleteTime = Time.time + blinkDuration; // Set the time at which the blink
    }
    void RevertColors()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i]; // Revert each material's color to its original color
        }
        showingColor = false; // Reset the flag to indicate that the blink color is no longer being shown
    }
}
