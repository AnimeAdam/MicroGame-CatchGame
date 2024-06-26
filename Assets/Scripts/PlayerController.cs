using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameDifficulty = Difficulty.difficulty;

public class PlayerController : MonoBehaviour
{
    bool rotateLeft; //If true will rotate left.
    float moveSpeed = 0.1f;

    // Use this for initialization
    void Start () {
        rotateLeft = true;
        InitialiseGlove();
    }
   
    // Update is called once per frame
    void Update ()
    {
        if(!FindAnyObjectByType<GameManager>().IsTheGloveFreezen()) {
            FollowMouse();
            GloveSwaying();
        }
    }

    //Set glove size based on Difficulty.
    private void InitialiseGlove()
    {
        switch (GameManager.Instance.gameDifficulty)
        {
            case GameDifficulty.Easy:
                transform.localScale = Vector3.one * 0.5f;
            break;
            case GameDifficulty.Medium:
                transform.localScale = Vector3.one * 0.4f;
            break;
            case GameDifficulty.Hard:
                transform.localScale = Vector3.one * 0.25f;
            break;
            default:
            Debug.Log("Something has gone wrong with difficulty settings at " + gameObject.name);
            break;
        }
        GameManager.Instance.UnFreezeTheGlove();
    }

    //Glove will sway left and right.
    private void GloveSwaying()
    {
        //Animating the speed and max angle for the glove.
        float rotationSpeed = 200.0f;
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
        gameObject.GetComponent<AudioSource>().Play();
    }
}
