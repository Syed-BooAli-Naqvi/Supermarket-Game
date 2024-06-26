using UnityEngine;

namespace Flexalon.Samples
{
    // Simple camera controller.
    // Use WASD or arrows to move. Rotate with right mouse button.
    // Pan with mouse wheel button.
    public class FlexalonSampleCamera : MonoBehaviour
    {
        public float Speed = 0.2f;
        public float RotateSpeed = 0.2f;
        public float InterpolationSpeed = 20.0f;

        private Vector3 _targetPosition;
        private Quaternion _targetRotation;
        private float _alpha;
        private float _beta;
        private Vector3 _mousePos;

        void Start()
        {
            _targetPosition = transform.position;
            _targetRotation = transform.rotation;
            var euler = _targetRotation.eulerAngles;
            _alpha = euler.y;
            _beta = euler.x;
        }

        void Update()
        {
#if UNITY_GUI
            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject)
            {
                return;
            }
#endif

            if (ControlFreak2.CF2Input.GetKey(KeyCode.UpArrow) || ControlFreak2.CF2Input.GetKey(KeyCode.W))
            {
                _targetPosition += transform.forward * Speed;
            }

            if (ControlFreak2.CF2Input.GetKey(KeyCode.LeftArrow) || ControlFreak2.CF2Input.GetKey(KeyCode.A))
            {
                _targetPosition += -transform.right * Speed;
            }

            if (ControlFreak2.CF2Input.GetKey(KeyCode.RightArrow) || ControlFreak2.CF2Input.GetKey(KeyCode.D))
            {
                _targetPosition += transform.right * Speed;
            }

            if (ControlFreak2.CF2Input.GetKey(KeyCode.DownArrow) || ControlFreak2.CF2Input.GetKey(KeyCode.S))
            {
                _targetPosition += -transform.forward * Speed;
            }

            if (ControlFreak2.CF2Input.GetMouseButtonDown(1) || ControlFreak2.CF2Input.GetMouseButtonDown(2))
            {
                _mousePos = ControlFreak2.CF2Input.mousePosition;
            }

            if (ControlFreak2.CF2Input.GetMouseButton(1))
            {
                var delta = ControlFreak2.CF2Input.mousePosition - _mousePos;
                _alpha += delta.x * RotateSpeed;
                _beta -= delta.y * RotateSpeed;
                _targetRotation = Quaternion.Euler(_beta, _alpha, 0);
                _mousePos = ControlFreak2.CF2Input.mousePosition;
            }

            if (ControlFreak2.CF2Input.GetMouseButtonDown(2))
            {
                _mousePos = ControlFreak2.CF2Input.mousePosition;
            }

            if (ControlFreak2.CF2Input.GetMouseButton(2))
            {
                var delta = ControlFreak2.CF2Input.mousePosition - _mousePos;
                _targetPosition -= delta.y * transform.up * Speed;
                _targetPosition -= delta.x * transform.right * Speed;
                _mousePos = ControlFreak2.CF2Input.mousePosition;
            }

            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * InterpolationSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * InterpolationSpeed);
        }
    }
}