using System;
using UnityEngine.UI;

////TODO: have updateBindingUIEvent receive a control path string, too (in addition to the device layout name)

namespace UnityEngine.InputSystem.Samples.RebindUI {
    /// <summary>
    /// This is an example for how to override the default display behavior of bindings. The component
    /// hooks into <see cref="RebindActionUI.updateBindingUIEvent"/> which is triggered when UI display
    /// of a binding should be refreshed. It then checks whether we have an icon for the current binding
    /// and if so, replaces the default text display with an icon.
    /// </summary>
    public class GamepadIconsExample : MonoBehaviour {
        public GamepadIcons xbox;
        public GamepadIcons ps4;
        public GamepadIcons switchController;
        public KeyboardIcons keyboard;
        public MouseIcons mouse;

        protected void OnEnable() {
            // Hook into all updateBindingUIEvents on all RebindActionUI components in our hierarchy.
            var rebindUIComponents = transform.GetComponentsInChildren<RebindActionUI>();
            foreach (var component in rebindUIComponents) {
                component.updateBindingUIEvent.AddListener(OnUpdateBindingDisplay);
                component.UpdateBindingDisplay();
            }
        }

        public void UpdateBindings() {
            var rebindUIComponents = transform.GetComponentsInChildren<RebindActionUI>();
            foreach (var component in rebindUIComponents) {
                component.updateBindingUIEvent.AddListener(OnUpdateBindingDisplay);
                component.UpdateBindingDisplay();
            }
        }

        protected void OnUpdateBindingDisplay(RebindActionUI component, string bindingDisplayString,
            string deviceLayoutName, string controlPath) {
            if (string.IsNullOrEmpty(deviceLayoutName) || string.IsNullOrEmpty(controlPath))
                return;

            var icon = default(Sprite);
            if (InputSystem.IsFirstLayoutBasedOnSecond(deviceLayoutName, "DualShockGamepad"))
                icon = ps4.GetSprite(controlPath);
            else if (InputSystem.IsFirstLayoutBasedOnSecond(deviceLayoutName, "Gamepad"))
                icon = xbox.GetSprite(controlPath);
            else if (InputSystem.IsFirstLayoutBasedOnSecond(deviceLayoutName, "Switch")) // Might be the wrong name
                icon = switchController.GetSprite(controlPath);
            else if (InputSystem.IsFirstLayoutBasedOnSecond(deviceLayoutName, "Keyboard")) {
                icon = keyboard.GetSprite(controlPath);
            }
            
            if (InputSystem.IsFirstLayoutBasedOnSecond(deviceLayoutName, "Mouse"))
                icon = mouse.GetSprite(controlPath);


            var textComponent = component.bindingText;

            // Grab Image component.
            var imageGO = textComponent.transform.parent.Find("ActionBindingIcon");
            var imageComponent = imageGO.GetComponent<Image>();

            if (icon != null) {
                textComponent.gameObject.SetActive(false);
                imageComponent.sprite = icon;
                imageComponent.gameObject.SetActive(true);
            }
            else {
                textComponent.gameObject.SetActive(true);
                imageComponent.gameObject.SetActive(false);
            }
        }

        [Serializable]
        public struct GamepadIcons {
            public Sprite buttonSouth;
            public Sprite buttonNorth;
            public Sprite buttonEast;
            public Sprite buttonWest;
            public Sprite startButton;
            public Sprite selectButton;
            public Sprite leftTrigger;
            public Sprite rightTrigger;
            public Sprite leftShoulder;
            public Sprite rightShoulder;
            public Sprite dpad;
            public Sprite dpadUp;
            public Sprite dpadDown;
            public Sprite dpadLeft;
            public Sprite dpadRight;
            public Sprite leftStick;
            public Sprite rightStick;
            public Sprite leftStickPress;
            public Sprite rightStickPress;

            public Sprite GetSprite(string controlPath) {
                // From the input system, we get the path of the control on device. So we can just
                // map from that to the sprites we have for gamepads.
                switch (controlPath) {
                    case "buttonSouth": return buttonSouth;
                    case "buttonNorth": return buttonNorth;
                    case "buttonEast": return buttonEast;
                    case "buttonWest": return buttonWest;
                    case "start": return startButton;
                    case "select": return selectButton;
                    case "leftTrigger": return leftTrigger;
                    case "rightTrigger": return rightTrigger;
                    case "leftShoulder": return leftShoulder;
                    case "rightShoulder": return rightShoulder;
                    case "dpad": return dpad;
                    case "dpad/up": return dpadUp;
                    case "dpad/down": return dpadDown;
                    case "dpad/left": return dpadLeft;
                    case "dpad/right": return dpadRight;
                    case "leftStick": return leftStick;
                    case "rightStick": return rightStick;
                    case "leftStickPress": return leftStickPress;
                    case "rightStickPress": return rightStickPress;
                }

                return null;
            }
        }

        [Serializable]
        public struct KeyboardIcons {
            public Sprite leftMouseButton;
            public Sprite rightMouseButton;
            public Sprite middleMouseButton;
            public Sprite mouseScroll;
            public Sprite mouseButtonForward;
            public Sprite mouseButtonBack;
            public Sprite one;
            public Sprite two;
            public Sprite three;
            public Sprite four;
            public Sprite five;
            public Sprite six;
            public Sprite seven;
            public Sprite eight;
            public Sprite nine;
            public Sprite zero;
            public Sprite A;
            public Sprite B;
            public Sprite C;
            public Sprite D;
            public Sprite E;
            public Sprite F;
            public Sprite G;
            public Sprite H;
            public Sprite I;
            public Sprite J;
            public Sprite K;
            public Sprite L;
            public Sprite M;
            public Sprite N;
            public Sprite O;
            public Sprite P;
            public Sprite Q;
            public Sprite R;
            public Sprite S;
            public Sprite T;
            public Sprite U;
            public Sprite V;
            public Sprite W;
            public Sprite X;
            public Sprite Y;
            public Sprite Z;
            public Sprite Enter;
            public Sprite TAB;
            public Sprite CAPS;
            public Sprite shift;
            public Sprite Alt;
            public Sprite backSpace;
            public Sprite num1;
            public Sprite num2;
            public Sprite num3;
            public Sprite num4;
            public Sprite num5;
            public Sprite num6;
            public Sprite num7;
            public Sprite num8;
            public Sprite num9;
            public Sprite num0;
            public Sprite F1;
            public Sprite F2;
            public Sprite F3;
            public Sprite F4;
            public Sprite F5;
            public Sprite F6;
            public Sprite F7;
            public Sprite F8;
            public Sprite F9;
            public Sprite F10;
            public Sprite F11;
            public Sprite F12;
            public Sprite HOME;
            public Sprite END;
            public Sprite INS;
            public Sprite DEL;
            public Sprite PGUP;
            public Sprite PGDOWN;
            public Sprite arrowDown;
            public Sprite arrowUp;
            public Sprite arrowRight;
            public Sprite arrowLeft;
            public Sprite comma;
            public Sprite ctrl;
            public Sprite escape;
            public Sprite numLock;
            public Sprite numPadEnter;
            public Sprite numPadPlus;
            public Sprite numPadMinus;
            public Sprite numPadDivide;
            public Sprite numPadMultiply;
            public Sprite period;
            public Sprite printScreen;
            public Sprite quote;
            public Sprite space;
            public Sprite tilde;
            public Sprite mouseScrollDown;
            public Sprite mouseScrollUp;
            public Sprite mouseDelta;

            public Sprite GetSprite(string controlPath) {
                switch (controlPath) {
                    case "leftButton": return leftMouseButton;
                    case "rightButton": return rightMouseButton;
                    case "middleButton": return middleMouseButton;
                    case "scroll": return mouseScroll;
                    case "forwardButton": return mouseButtonForward;
                    case "backButton": return mouseButtonBack;
                    case "1": return one;
                    case "2": return two;
                    case "3": return three;
                    case "4": return four;
                    case "5": return five;
                    case "6": return six;
                    case "7": return seven;
                    case "8": return eight;
                    case "9": return nine;
                    case "0": return zero;
                    case "a": return A;
                    case "b": return B;
                    case "c": return C;
                    case "d": return D;
                    case "e": return E;
                    case "f": return F;
                    case "g": return G;
                    case "h": return H;
                    case "i": return I;
                    case "j": return J;
                    case "k": return K;
                    case "l": return L;
                    case "m": return M;
                    case "n": return N;
                    case "o": return O;
                    case "p": return P;
                    case "q": return Q;
                    case "r": return R;
                    case "s": return S;
                    case "t": return T;
                    case "u": return U;
                    case "v": return V;
                    case "w": return W;
                    case "x": return X;
                    case "y": return Y;
                    case "z": return Z;
                    case "enter": return Enter;
                    case "tab": return TAB;
                    case "capsLock": return CAPS;
                    case "shift": return shift;
                    case "alt": return Alt;
                    case "backspace": return backSpace;
                    case "numpad1": return num1;
                    case "numpad2": return num2;
                    case "numpad3": return num3;
                    case "numpad4": return num4;
                    case "numpad5": return num5;
                    case "numpad6": return num6;
                    case "numpad7": return num7;
                    case "numpad8": return num8;
                    case "numpad9": return num9;
                    case "numpad0": return num0;
                    case "f1": return F1;
                    case "f2": return F2;
                    case "f3": return F3;
                    case "f4": return F4;
                    case "f5": return F5;
                    case "f6": return F6;
                    case "f7": return F7;
                    case "f8": return F8;
                    case "f9": return F9;
                    case "f10": return F10;
                    case "f11": return F11;
                    case "f12": return F12;
                    case "home": return HOME;
                    case "end": return END;
                    case "insert": return INS;
                    case "delete": return DEL;
                    case "pageUp": return PGUP;
                    case "pageDown": return PGDOWN;
                    case "downArrow": return arrowDown;
                    case "upArrow": return arrowUp;
                    case "rightArrow": return arrowRight;
                    case "leftArrow": return arrowLeft;
                    case "comma": return comma;
                    case "ctrl": return ctrl;
                    case "escape": return escape;
                    case "numLock": return numLock;
                    case "numpadEnter": return numPadEnter;
                    case "numpadPlus": return numPadPlus;
                    case "numpadMinus": return numPadMinus;
                    case "numpadDivide": return numPadDivide;
                    case "numpadMultiply": return numPadMultiply;
                    case "period": return period;
                    case "printScreen": return printScreen;
                    case "quote": return quote;
                    case "space": return space;
                    case "backquote": return tilde;
                    case "scroll/down": return mouseScrollDown;
                    case "scroll/up": return mouseScrollUp;
                    case "delta": return mouseDelta;
                }

                return null;
            }
        }

        [Serializable]
        public struct MouseIcons {
            public Sprite leftMouseButton;
            public Sprite rightMouseButton;
            public Sprite middleMouseButton;
            public Sprite mouseScroll;
            public Sprite mouseButtonForward;
            public Sprite mouseButtonBack;
            public Sprite mouseScrollDown;
            public Sprite mouseScrollUp;
            public Sprite mouseDelta;

            public Sprite GetSprite(string controlPath) {
                switch (controlPath) {
                    case "leftButton": return leftMouseButton;
                    case "rightButton": return rightMouseButton;
                    case "middleButton": return middleMouseButton;
                    case "scroll": return mouseScroll;
                    case "forwardButton": return mouseButtonForward;
                    case "backButton": return mouseButtonBack;
                    case "scroll/down": return mouseScrollDown;
                    case "scroll/up": return mouseScrollUp;
                    case "delta": return mouseDelta;
                }

                return null;
            }
        }
    }
}