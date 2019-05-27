using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum AxisOptions
{
    Both,
    Horizontal,
    Vertical
}

public class Joystick_L1 : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    //Dynamic joystick

    [SerializeField] private float move_threshold = 1;
    [SerializeField] private float dead_zone = 0;
    [SerializeField] private AxisOptions axis_options = AxisOptions.Both;

    [SerializeField] private RectTransform circle_handler = null;
    [SerializeField] private RectTransform handle = null;
    private RectTransform base_rect = null;
    private Vector2 original_pos;

    private Canvas canvas;

    private Vector2 input_direction = Vector2.zero;

    void Start()
    {
        base_rect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        Vector2 center = new Vector2(0.5f, 0.5f);

        circle_handler.pivot = center;
        original_pos = circle_handler.anchoredPosition;

        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        return input_direction.x;
    }

    public float Vertical()
    {
        return input_direction.y;
    }

    public Vector2 Direction()
    {
        return new Vector2(Horizontal(), Vertical());
    }           

    public void OnPointerDown(PointerEventData event_data)
    {
        circle_handler.anchoredPosition = ScreenPointToAnchoredPosition(event_data.position);

        OnDrag(event_data);
    }

    public void OnPointerUp(PointerEventData event_data)
    {
        input_direction = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;

        circle_handler.anchoredPosition = original_pos;
    }

    public void OnDrag(PointerEventData event_data)
    {
        Vector2 position = circle_handler.position;
        Vector2 radius = circle_handler.sizeDelta / 2;

        input_direction = (event_data.position - position) / (radius * canvas.scaleFactor);

        FormatInput();

        HandleInput(input_direction.magnitude, input_direction.normalized, radius);
        handle.anchoredPosition = input_direction * radius;
    }

    void HandleInput(float magnitude, Vector2 normalised, Vector2 radius)
    {
        if (magnitude > move_threshold)
        {
            Vector2 difference = normalised * (magnitude - move_threshold) * radius;
            circle_handler.anchoredPosition += difference;
        }

        if (magnitude > dead_zone)
        {
            if (magnitude > 1)
            {
                input_direction = normalised;
            }
        }
        else
        {
            input_direction = Vector2.zero;
        }
    }

    void FormatInput()
    {
        if (axis_options == AxisOptions.Horizontal)
        {
            input_direction = new Vector2(input_direction.x, 0f);
        }
        else if (axis_options == AxisOptions.Vertical)
        {
            input_direction = new Vector2(0f, input_direction.y);
        }
    }

    Vector2 ScreenPointToAnchoredPosition(Vector2 screen_position)
    {
        Vector2 local_point = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(base_rect, screen_position, null, out local_point))
        {
            Vector2 pivot_offset = base_rect.pivot * base_rect.sizeDelta;
            return local_point - (circle_handler.anchorMax * base_rect.sizeDelta) + pivot_offset;
        }
        return Vector2.zero;
    }
}