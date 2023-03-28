using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Vector2 parallaxEffect;
    public Transform camTransform;
    public bool horizInfinity;
    public bool VertInfinity; 
    private Vector3 lastCamPosition;
    private Sprite sprite; 
    private Texture2D texture;
    private float textureUnitSizeX;
    private float textureUnitSizeY;







    void Start()
    {
        lastCamPosition = camTransform.position;

        sprite = GetComponent<SpriteRenderer>().sprite;
        texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
        textureUnitSizeY = texture.height / sprite.pixelsPerUnit;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 deltaMovement = camTransform.position - lastCamPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffect.x, deltaMovement.y * parallaxEffect.y);
        lastCamPosition = camTransform.position; 

        if (horizInfinity)
        {
            if (Mathf.Abs(camTransform.position.x - transform.position.x) >= textureUnitSizeX)
            {
                float offsetPositionX = (camTransform.position.x - transform.position.x) % textureUnitSizeX;
                transform.position = new Vector3(camTransform.position.x + offsetPositionX, transform.position.y);
            }
        }

        if (VertInfinity)
        {
            if (Mathf.Abs(camTransform.position.y - transform.position.y) >= textureUnitSizeY)
            {
                float offsetPositionY = (camTransform.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(transform.position.x, camTransform.position.y + offsetPositionY);
            }

        }
    }
}
