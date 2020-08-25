using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    [Header("Movement Speed")]
    public float panSpeed = 20f;

    [Header("Mouse Borders")]
    public float panBorderThickness = 10f;

    [Header("Screen Limits")]
    public Vector2 panLimit;

    [Header("Mouse Scrolling")]
    public float scrollSpeed = 2f;
    public float minScroll = 4.5f;
    public float maxScroll = 7f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        GetComponent<Camera>().orthographicSize -= scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize, minScroll, maxScroll);

        transform.position = pos;
    }
}
