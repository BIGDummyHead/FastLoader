using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valve.VR;

namespace FastandLow.Modding.Utilities
{
    /// <summary>
    /// Detection for Input from a VR system
    /// </summary>
    public static class VRInput
    {
        /// <summary>
        /// Happens when a VR Button is Pressed Down Once
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool GetButtonDown(Input input)
        {
            bool returnValue = false;

            switch (input)
            {
                case Input.A:
                    {
                        returnValue = SteamVR_Actions.vrtk.ButtonTwoClick[SteamVR_Input_Sources.RightHand].stateDown;
                        break;
                    }
                case Input.B:
                    {
                        returnValue = SteamVR_Actions.vrtk.ButtonOneClick[SteamVR_Input_Sources.RightHand].stateDown;
                        break;
                    }
                case Input.X:
                    {
                        returnValue = SteamVR_Actions.vrtk.ButtonTwoClick[SteamVR_Input_Sources.LeftHand].stateDown;
                        break;
                    }
                case Input.Y:
                    {
                        returnValue = SteamVR_Actions.vrtk.ButtonOneClick[SteamVR_Input_Sources.LeftHand].stateDown;
                        break;
                    }
                case Input.LeftTrigger:
                    {
                        returnValue = SteamVR_Actions.vrtk.TriggerButton[SteamVR_Input_Sources.LeftHand].stateDown;
                        break;
                    }
                case Input.RightTrigger:
                    {
                        returnValue = SteamVR_Actions.vrtk.TriggerButton[SteamVR_Input_Sources.RightHand].stateDown;
                        break;
                    }
                case Input.LeftGrip:
                    {
                        returnValue = SteamVR_Actions.vrtk.GripButton[SteamVR_Input_Sources.LeftHand].stateDown;
                        break;
                    }
                case Input.RightGrip:
                    {
                        returnValue = SteamVR_Actions.vrtk.GripButton[SteamVR_Input_Sources.RightHand].stateDown;
                        break;
                    }
            }

            return returnValue;
        }

        /// <summary>
        /// Happens after a button is pressed down and Held for a period of time
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool GetButtonHeld(Input input)
        {
            bool returnValue = false;

            switch (input)
            {
                case Input.A:
                    {
                        returnValue = SteamVR_Actions.vrtk.ButtonTwoClick[SteamVR_Input_Sources.RightHand].state;
                        break;
                    }
                case Input.B:
                    {
                        returnValue = SteamVR_Actions.vrtk.ButtonOneClick[SteamVR_Input_Sources.RightHand].state;
                        break;
                    }
                case Input.X:
                    {
                        returnValue = SteamVR_Actions.vrtk.ButtonTwoClick[SteamVR_Input_Sources.LeftHand].state;
                        break;
                    }
                case Input.Y:
                    {
                        returnValue = SteamVR_Actions.vrtk.ButtonOneClick[SteamVR_Input_Sources.LeftHand].state;
                        break;
                    }
                case Input.LeftTrigger:
                    {
                        returnValue = SteamVR_Actions.vrtk.TriggerButton[SteamVR_Input_Sources.LeftHand].state;
                        break;
                    }
                case Input.RightTrigger:
                    {
                        returnValue = SteamVR_Actions.vrtk.TriggerButton[SteamVR_Input_Sources.RightHand].state;
                        break;
                    }
                case Input.LeftGrip:
                    {
                        returnValue = SteamVR_Actions.vrtk.GripButton[SteamVR_Input_Sources.LeftHand].state;
                        break;
                    }
                case Input.RightGrip:
                    {
                        returnValue = SteamVR_Actions.vrtk.GripButton[SteamVR_Input_Sources.RightHand].state;
                        break;
                    }
            }

            return returnValue;
        }

        /// <summary>
        /// Happens when a VR Button is released from either being Held and or pushed Down
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        
        public static bool GetButtonUp(Input input)
        {
            bool returnValue = false;

            switch (input)
            {
                case Input.A:
                    {
                        returnValue = SteamVR_Actions.vrtk.ButtonTwoClick[SteamVR_Input_Sources.RightHand].stateUp;
                        break;
                    }
                case Input.B:
                    {
                        returnValue = SteamVR_Actions.vrtk.ButtonOneClick[SteamVR_Input_Sources.RightHand].stateUp;
                        break;
                    }
                case Input.X:
                    {
                        returnValue = SteamVR_Actions.vrtk.ButtonTwoClick[SteamVR_Input_Sources.LeftHand].stateUp;
                        break;
                    }
                case Input.Y:
                    {
                        returnValue = SteamVR_Actions.vrtk.ButtonOneClick[SteamVR_Input_Sources.LeftHand].stateUp;
                        break;
                    }
                case Input.LeftTrigger:
                    {
                        returnValue = SteamVR_Actions.vrtk.TriggerButton[SteamVR_Input_Sources.LeftHand].stateUp;
                        break;
                    }
                case Input.RightTrigger:
                    {
                        returnValue = SteamVR_Actions.vrtk.TriggerButton[SteamVR_Input_Sources.RightHand].stateUp;
                        break;
                    }
                case Input.LeftGrip:
                    {
                        returnValue = SteamVR_Actions.vrtk.GripButton[SteamVR_Input_Sources.LeftHand].stateUp;
                        break;
                    }
                case Input.RightGrip:
                    {
                        returnValue = SteamVR_Actions.vrtk.GripButton[SteamVR_Input_Sources.RightHand].stateUp;
                        break;
                    }
            }

            return returnValue;
        }

        /// <summary>
        /// Input for the several methods in <see cref="VRInput"/>
        /// </summary>
        public enum Input
        {
            /// <summary>
            /// Button A - 10
            /// </summary>
            A = 10,
            /// <summary>
            /// Button B - 20
            /// </summary>
            B = 20,
            /// <summary>
            /// Button X - 30
            /// </summary>
            X = 30,
            /// <summary>
            /// Button Y - 40
            /// </summary>
            Y = 40,
            /// <summary>
            /// Left Trigger - 1
            /// </summary>
            LeftTrigger = 1,
            /// <summary>
            /// Right Trigger - 2
            /// </summary>
            RightTrigger = 2,
            /// <summary>
            /// Left Grip - 3
            /// </summary>
            LeftGrip = 3,
            /// <summary>
            /// Right Grip - 4
            /// </summary>
            RightGrip = 4
        }
    }
}
