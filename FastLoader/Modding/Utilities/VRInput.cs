using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valve.VR;

namespace FastandLow.Modding.Utilities
{
    public static class VRInput
    {
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

        public enum Input
        {
            A = 10,
            B = 20,
            X = 30,
            Y = 40,
            LeftTrigger = 0,
            RightTrigger = 1,
            LeftGrip = 2,
            RightGrip = 3
        }
    }
}
