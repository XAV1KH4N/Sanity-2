using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{

    private RectTransform joystick_transform;
    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;
    public event Action OnStop;

    [SerializeField]
    public float drag_threshold = 0.6f;

    [SerializeField]
    public int drag_movement_distance = 45;

    [SerializeField]
    public int drag_offset_distance = 75;

    [SerializeField]
    protected float look_dist = 0.25f;

    public void OnDrag(PointerEventData event_data)
    {
        /*
        * Function is called when the joystick is dragger. It triggers an event to move the player
        */
        Vector2 offset;
            // cacluate the relative drag from the centre joystick
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystick_transform, event_data.position, null, out offset);

        offset = Vector2.ClampMagnitude(offset, drag_offset_distance) / drag_offset_distance; // limits the range from 0 to 1
        joystick_transform.anchoredPosition = offset * drag_movement_distance; // Update display before it gets rounded for smoother animation
            //offset = new Vector2(Mathf.Round(offset.x), Mathf.Round(offset.y)); // Rounds vectors to 1 or 0
        if (offset.sqrMagnitude > look_dist)
        {
            OnMove?.Invoke(offset);
        }
        else
        {
            OnLook?.Invoke(offset);
        }


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        try
        {
            joystick_transform.anchoredPosition = new Vector2(0, 0);
            OnStop?.Invoke();
        }
        catch (NullReferenceException) { }
    }

    private void Awake()
    {
        try
        {
            joystick_transform = (RectTransform)transform;
        }
        catch (InvalidCastException)
        {
            // Stops its from complaining even when it works
        }
    }
}
