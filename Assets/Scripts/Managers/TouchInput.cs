using UnityEngine;

public class TouchInput : MonoBehaviour
{

    [SerializeField] private float deadZone = 2f;
    private static float _lastFrameFingerPositionX;
    private static float _lastFrameFingerPositionY;
    private static float _moveFactorX;
    private static float _moveFactorY;

    public static float MoveFactorX => _moveFactorX;
    public static float MoveFactorY => _moveFactorY;
    public static float MoveDirectionX => _moveFactorX != 0f ? Mathf.Sign(_moveFactorX) : 0f;
    public static float MoveDirectionY => _moveFactorY != 0f ? Mathf.Sign(_moveFactorY) : 0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
            _lastFrameFingerPositionY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButton(0))
        {
            _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
            _moveFactorY = Input.mousePosition.y - _lastFrameFingerPositionY;

            _lastFrameFingerPositionX = Input.mousePosition.x;
            _lastFrameFingerPositionY = Input.mousePosition.y;

            if (Mathf.Abs(_moveFactorX) <= deadZone)
            {
                _moveFactorX = 0f;
            }
            if (Mathf.Abs(_moveFactorY) <= deadZone)
            {
                _moveFactorY = 0f;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _moveFactorX = 0f;
            _moveFactorY = 0f;
        }
    }
}