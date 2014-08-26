using SharpDX.XInput;
using System;

namespace Drones.Input
{
    public class XBox360Input : InputBase
    {
        // @Events
        public event Action StartPressed;
        public event Action BackPressed;
        public event Action APressed;
        public event Action BPressed;
        public event Action XPressed;
        public event Action YPressed;
        public event Action LeftPressed;
        public event Action UpPressed;
        public event Action RightPressed;
        public event Action DownPressed;
        public event Action LBPressed;
        public event Action RBPressed;
        public event Action LStickressed;
        public event Action RStickressed;
        public event Action LTPressed;
        public event Action RTPressed;

        // @Properties
        public override bool IsConnected
        {
            get 
            {
                return _controller.IsConnected;
            }
        }


        // @Public
        public XBox360Input()
            : base("XBox360 controller")
        {
            _controller = new Controller(0);
        }

        public void Update()
        {
            float pitch = 0;
            float roll = 0;
            float yaw = 0;
            float gaz = 0;

            // Verifications.
            if (!_controller.IsConnected || !_controller.GetState(out _controllerState))
            {
                _failCounter++;
                if (_failCounter > _failCounterMax)
                {
                    Pitch = 0;
                    Roll = 0;
                    Gaz = 0;
                    Yaw = 0;

                    // Avoid overflow.
                    _failCounter = _failCounterMax;
                }
                return;
            }

            // Buttons.
            var buttons = _controllerState.Gamepad.Buttons;
            
            if (StartPressed != null && buttons.HasFlag(GamepadButtonFlags.Start))
            {
                StartPressed();
            }
            if (BackPressed != null && buttons.HasFlag(GamepadButtonFlags.Back))
            {
                BackPressed();
            }
            if (APressed != null && buttons.HasFlag(GamepadButtonFlags.A))
            {
                APressed();
            }
            if (BPressed != null && buttons.HasFlag(GamepadButtonFlags.B))
            {
                BPressed();
            }
            if (XPressed != null && buttons.HasFlag(GamepadButtonFlags.X))
            {
                XPressed();
            }
            if (YPressed != null && buttons.HasFlag(GamepadButtonFlags.Y))
            {
                YPressed();
            }
            if (LeftPressed != null && buttons.HasFlag(GamepadButtonFlags.DPadLeft))
            {
                LeftPressed();
            }
            if (UpPressed != null && buttons.HasFlag(GamepadButtonFlags.DPadUp))
            {
                UpPressed();
            }
            if (RightPressed != null && buttons.HasFlag(GamepadButtonFlags.DPadRight))
            {
                RightPressed();
            }
            if (DownPressed != null && buttons.HasFlag(GamepadButtonFlags.DPadDown))
            {
                DownPressed();
            }
            if (LBPressed != null && buttons.HasFlag(GamepadButtonFlags.LeftShoulder))
            {
                LBPressed();
            }
            if (RBPressed != null && buttons.HasFlag(GamepadButtonFlags.RightShoulder))
            {
                RBPressed();
            }
            if (LStickressed != null && buttons.HasFlag(GamepadButtonFlags.LeftThumb))
            {
                LStickressed();
            }
            if (RStickressed != null && buttons.HasFlag(GamepadButtonFlags.RightThumb))
            {
                RStickressed();
            }

            // Triggers.
            if (LTPressed != null && _controllerState.Gamepad.LeftTrigger > 200)
            {
                LTPressed();
            }
            if (RTPressed != null && _controllerState.Gamepad.RightTrigger > 200)
            {
                RTPressed();
            }

            if (_controllerState.PacketNumber <= _controllerPreviousState.PacketNumber)
            {
                return;
            }

            // Thumbs.
            var leftThumb = NormalizeInput(_controllerState.Gamepad.LeftThumbX, _controllerState.Gamepad.LeftThumbY, Convert.ToInt16(SharpDX.XInput.Gamepad.LeftThumbDeadZone * 1.1), _joystickRange);
            if (leftThumb.NormalizedMagnitude > 0)
            {
                roll = (float)_controllerState.Gamepad.LeftThumbX * _rollThrottle / _joystickRange;
                pitch = (float)_controllerState.Gamepad.LeftThumbY * _pitchThrottle / _joystickRange;
            }
            var rightThumb = NormalizeInput(_controllerState.Gamepad.RightThumbX, _controllerState.Gamepad.RightThumbY, Convert.ToInt16(SharpDX.XInput.Gamepad.RightThumbDeadZone * 1.1), _joystickRange);
            if (rightThumb.NormalizedMagnitude > 0)
            {
                yaw = (float)_controllerState.Gamepad.RightThumbX * _yawThrottle / _joystickRange;
                gaz = (float)_controllerState.Gamepad.RightThumbY * _gazThrottle / _joystickRange;
            }

            _failCounter = 0;
            Pitch = -pitch;
            Roll = roll;
            Gaz = gaz;
            Yaw = yaw;

            _controllerPreviousState = _controllerState;
        }


        // @Private
        Controller _controller;
        State _controllerState;
        State _controllerPreviousState;
        float _pitchThrottle = 1;
        float _rollThrottle = 1;
        float _yawThrottle = 1;
        float _gazThrottle = 1;
        short _joystickRange = 32767;
        int _failCounter = 0;
        int _failCounterMax = 5;

        static JoystickState NormalizeInput(short X, short Y, short inputDeadZone, short joystickRange)
        {
            // From http://msdn.microsoft.com/en-us/library/windows/desktop/ee417001(v=vs.85).aspx.
            var result = new JoystickState();

            // Determine how far the controller is pushed.
            result.Magnitude = (float)Math.Sqrt(X * X + Y * Y);

            // Determine the direction the controller is pushed.
            result.NormalizedX = X / result.Magnitude;
            result.NormalizedY = Y / result.Magnitude;
            result.NormalizedMagnitude = 0;

            // Check if the controller is outside a circular dead zone.
            if (result.Magnitude > inputDeadZone)
            {
                // Clip the magnitude at its expected maximum value.
                if (result.Magnitude > joystickRange)
                    result.Magnitude = joystickRange;

                // Adjust magnitude relative to the end of the dead zone.
                result.Magnitude -= inputDeadZone;

                // Optionally normalize the magnitude with respect to its expected range.
                // Giving a magnitude value of 0.0 to 1.0.
                result.NormalizedMagnitude = result.Magnitude / (joystickRange - inputDeadZone);
            }
            else // If the controller is in the deadzone zero out the magnitude.
            {
                result.Magnitude = 0;
                result.NormalizedMagnitude = 0;
            }
            return result;
        }
    }
}
