//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Core.FSM;
//using Core.FSM;
//using UnityEngine;

//namespace Core.Controllers
//{
//    public class GamePad : MonoBehaviour
//    {
        
//        //[SerializeField]
//        //private Layout layout;

//        [SerializeField]
//        private float axesLerpAmount = .1f;

//        [SerializeField]
//        private float axesInfluence = .5f;


//        public override Core.FSM.State Create(object[] context)
//        {
//            return new State(context, this);
//        }


//        public class State : FSM.State
//        {

//            private float targetHorizontal = 0;
//            private float targetVertical = 0;

//            new protected GamePad Resource { get { return (GamePad)resource; } }

//            public State(object[] context, Core.FSM.Resource resource) : base(context, resource) { }
            
            
//            public override void BeginTick()
//            {
//                if (Controller.IsEnabled)
//                {
//                    ////////// MENU
                
//                    if (Input.GetKeyDown(KeyCode.Escape))
//                    { if (Controller.OnMenuPressedEvent != null) Controller.OnMenuPressedEvent.Invoke(); }

//                    if (Input.GetKeyUp(KeyCode.Escape))
//                    { if (Controller.OnMenuReleasedEvent != null) Controller.OnMenuReleasedEvent.Invoke(); }

//                    Controller.menuChecked = Input.GetKey(KeyCode.Escape);

//                    ////////// FOCUS

//                    if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return) ||
//                        Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
//                    { if (Controller.onFocusPressed != null) Controller.onFocusPressed.Invoke(); }

//                    if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.Return) ||
//                    Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
//                    { if (Controller.onFocusReleased != null) Controller.onFocusReleased.Invoke(); }

//                    Controller.focusChecked = Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.KeypadEnter) ||
//                    Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);


//                    ////////// ACTION

//                    if (Input.GetKeyDown(KeyCode.Alpha1))
//                    { if (Controller.onActionPressed != null) Controller.onActionPressed.Invoke(0); }
//                    if (Input.GetKeyUp(KeyCode.Alpha1))
//                    { if (Controller.onActionReleased != null) Controller.onActionReleased.Invoke(0); }


//                    if (Input.GetKeyDown(KeyCode.Alpha2))
//                    { if (Controller.onActionPressed != null) Controller.onActionPressed.Invoke(1); }
//                    if (Input.GetKeyUp(KeyCode.Alpha2))
//                    { if (Controller.onActionReleased != null) Controller.onActionReleased.Invoke(1); }


//                    if (Input.GetKeyDown(KeyCode.Alpha3))
//                    { if (Controller.onActionPressed != null) Controller.onActionPressed.Invoke(2); }
//                    if (Input.GetKeyUp(KeyCode.Alpha3))
//                    { if (Controller.onActionReleased != null) Controller.onActionReleased.Invoke(2); }

               
//                    if (Input.GetKeyDown(KeyCode.Alpha4))
//                    { if (Controller.onActionPressed != null) Controller.onActionPressed.Invoke(3); }
//                    if (Input.GetKeyUp(KeyCode.Alpha4))
//                    { if (Controller.onActionReleased != null) Controller.onActionReleased.Invoke(3); }


//                    // Controller.actionChecked = Input.GetKey(KeyCode.Alpha1);


//                    ////////// JUMP

//                    if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space))
//                    { if (Controller.onJumpPressed != null) Controller.onJumpPressed.Invoke(); }

//                    if (Input.GetKeyUp(KeyCode.X) || Input.GetKeyUp(KeyCode.Space))
//                    { if (Controller.onJumpReleased != null) Controller.onJumpReleased.Invoke(); }

//                    Controller.jumpChecked = Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Space);


//                    ////////// LEFT

//                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
//                    { if (Controller.onLeftPressed != null) Controller.onLeftPressed.Invoke(); }
//                    Controller.leftChecked = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);

//                    if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
//                    { if (Controller.onLeftReleased != null) Controller.onLeftReleased.Invoke(); }

//                    Controller.leftChecked = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);


//                    ////////// RIGHT

//                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
//                    { if (Controller.onRightPressed != null) Controller.onRightPressed.Invoke(); }
//                    Controller.rightChecked = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);


//                    if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
//                    { if (Controller.onRightReleased != null) Controller.onRightReleased.Invoke(); }

//                    Controller.rightChecked = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);

//                    ////////// UP

//                    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
//                    { if (Controller.onUpPressed != null) Controller.onUpPressed.Invoke(); }

//                    if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
//                    { if (Controller.onUpReleased != null) Controller.onUpReleased.Invoke(); }

//                    Controller.upChecked = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

//                    ////////// DOWN

//                    if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
//                    { if (Controller.onDownPressed != null) Controller.onDownPressed.Invoke(); }


//                    if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
//                    { if (Controller.onDownReleased != null) Controller.onDownReleased.Invoke(); }

//                    Controller.downChecked = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

//                    //////////

//                    targetHorizontal = 0;
//                    targetVertical = 0;
//                    targetVertical = Controller.upChecked ? 1 : targetVertical;
//                    targetVertical = Controller.downChecked ? -1 : targetVertical;
//                    targetVertical = Controller.upChecked && Controller.downChecked ? 0 : targetVertical;
//                    targetHorizontal = Controller.rightChecked ? 1 : targetHorizontal;
//                    targetHorizontal = Controller.leftChecked ? -1 : targetHorizontal;
//                    targetHorizontal = Controller.leftChecked && Controller.rightChecked ? 0 : targetHorizontal;
//                    targetHorizontal += Input.GetAxis("Horizontal") * Resource.axesInfluence;
//                    targetVertical += Input.GetAxis("Vertical") * Resource.axesInfluence;
//                    Controller.axes.Left.x = Mathf.Lerp(Controller.axes.Left.x, targetHorizontal, Resource.axesLerpAmount);
//                    Controller.axes.Left.y = Mathf.Lerp(Controller.axes.Left.y, targetVertical, Resource.axesLerpAmount);
//                    Controller.axes.Right.x = Input.GetAxis("Mouse X");
//                    Controller.axes.Right.y = Input.GetAxis("Mouse Y");

//                }

//            }
//        }
//    }

//}
