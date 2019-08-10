using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    private float maxRaycastDepth = 200f; //TODO make it a function in zoom 
    private RaycastHit hit;

    private bool isSelecting;

    private Vector3 startingMousePosition;
    private Vector3 endingMousePosition;


    //draws selection box
    void OnGUI()
    {
        if (isSelecting)
        {
            var rect = Utils.GetScreenRect(startingMousePosition, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, Color.green);
        }
    }

    private void Update()
    {
        //mouse left-click processing
        if (Input.GetMouseButtonDown(0))
        {
            isSelecting = true;
            startingMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isSelecting = false;
            endingMousePosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, maxRaycastDepth);

            ActionHandler.LeftClick(startingMousePosition,endingMousePosition,hit);
        }
        
        //mouse right-click processing
        else if (Input.GetMouseButtonUp(1))
        {
            ActionHandler.RightClick();
        }
    }
}
