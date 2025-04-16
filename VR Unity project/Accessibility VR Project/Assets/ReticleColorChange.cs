using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ReticleColorChange : MonoBehaviour
{
    public Image reticleImage;
    public Color defaultColor = Color.white;//default
    public Color hoverColor = Color.green;//interaction color

    public float rayLength = 10f;
    public LayerMask interactableLayer; // track collision
 

    private Camera mainCam;
    private GameObject currentTarget;

    void Start()
    {
        mainCam = Camera.main;

        if (reticleImage != null)
        {
            reticleImage.color = defaultColor;
        }
    }

    void Update()
    {
        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward);
        RaycastHit hit;

        //if ray hits
        if (Physics.Raycast(ray, out hit, rayLength, interactableLayer))
        {   //change color
            reticleImage.color = hoverColor;
            currentTarget = hit.collider.gameObject;
        }
        else
        {   //reset color back to default
            reticleImage.color = defaultColor;
            currentTarget = null;
        }
    }

    
}
