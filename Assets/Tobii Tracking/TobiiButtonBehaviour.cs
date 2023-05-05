using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TobiiButtonBehaviour : MonoBehaviour
{
    private Canvas cv;
    private float timeOnFocus = 0;
    [SerializeField] float focusTime;
    [SerializeField] GameObject tobiiCursor;
    [SerializeField] UnityEvent onClick;
    private bool animating = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 bot_left = transform.TransformPoint(sr.sprite.bounds.min);
        Debug.Log(bot_left);
        Vector3 top_right = transform.TransformPoint(sr.sprite.bounds.max);
        Debug.Log(top_right);
        Vector3 wp = TobiiCursorBehaviour.world_point;
        Debug.Log(wp);
        //if you want you can use direct sight on the button but it will shake and may cause a misclick
        //against it using cursor will improve stability
        //Vector3 wp = TobiiHelper.getWorldPoint();
        if (wp.y >= bot_left.y && wp.y <= top_right.y && wp.x >= bot_left.x && wp.x <= top_right.x)
        {
            if (timeOnFocus == 0)
            {
                animating = true;
                tobiiCursor.GetComponent<TobiiCursorBehaviour>().animateCursor(focusTime);
            }

            timeOnFocus += Time.deltaTime;
        }
        else
        {
            timeOnFocus = 0;
            if (animating)
            {
                tobiiCursor.GetComponent<TobiiCursorBehaviour>().stopAnimateCursor();
                animating = false;
            }
        }

        if (timeOnFocus >= focusTime)
        {
            animating = false;
            tobiiCursor.GetComponent<TobiiCursorBehaviour>().stopAnimateCursor();
            Debug.Log("Button pressed");
            onClick.Invoke();
            timeOnFocus = 0;
        }
    }
}
