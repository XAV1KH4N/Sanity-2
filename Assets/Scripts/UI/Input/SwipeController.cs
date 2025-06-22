using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour
{
    [SerializeField]
    private float threshold = 50f; // Changed in unity editor 
    private Vector2 previousTouchPosition;
    private bool isTouching;
    public Action<Vector2> OnLook;

    void Update()
    {
        // Won't work with mouse, only looking at touch screen input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 currentTouchPosition = touch.position;

            // Only respond to touches on the RIGHT half of the screen
            if (currentTouchPosition.x < Screen.width / 2)
                return;

            if (touch.phase == TouchPhase.Began)
            {
                previousTouchPosition = currentTouchPosition;
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Moved && isTouching)
            {
                Vector2 direction = currentTouchPosition - previousTouchPosition;
                Debug.Log("Mag " + direction.magnitude + " Threshold " + threshold);
                if (direction.magnitude >= threshold)
                {
                    previousTouchPosition = currentTouchPosition;
                    handleSwipe(direction);
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isTouching = false;
            }
        }
    }
    private void handleSwipe(Vector2 swipeDelta)
    {
        swipeDelta.Normalize();
        OnLook?.Invoke(swipeDelta);
    }
}
