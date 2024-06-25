using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool rotateLeft; //If true will rotate left.
    float moveSpeed = 0.1f;

    // Use this for initialization
    void Start () {
        rotateLeft = true;
    }
   
    // Update is called once per frame
    void Update ()
    {
        if(!FindAnyObjectByType<GameManager>().IsBallCatched()) {
            FollowMouse();
            GloveSwaying();
        }
    }

    //Glove will sway left and right.
    private void GloveSwaying()
    {
        //Animating the speed and max angle for the glove.
        float rotationSpeed = 100.0f;
        float rotationAngle = 30.0f;
        
        Vector3 currentRotation = transform.eulerAngles;
        if (rotateLeft)
            transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward, Space.Self);
        else
            transform.Rotate(-rotationSpeed * Time.deltaTime * Vector3.forward, Space.Self);

        if (currentRotation.z > rotationAngle && currentRotation.z < rotationAngle + 10f)
            rotateLeft = false;
        if (currentRotation.z < 360f - rotationAngle && currentRotation.z > 360f - rotationAngle - 10f)
            rotateLeft = true;
    }

    //Glove object will follow cursor.
    private void FollowMouse()
    {
        Vector3 mousePosition;

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
    }

    public void PlayCatchAnimation()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("glove_10_closed");
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
}
