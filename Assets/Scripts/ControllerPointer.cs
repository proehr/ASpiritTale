// Source: https://www.youtube.com/watch?v=F60UIo7Y1YY

using SymbolDrawing;
using UnityEngine;

public class ControllerPointer : MonoBehaviour
{
    //line renderer to draw on ray
    [SerializeField] LineRenderer pointerLine = null;
    [SerializeField] bool alwaysTriggered = false;
    public float lineMaxLength = 1f;

    //right hand index trigger input
    private float pointerTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
    public bool triggered = false;

    private Symbol activeSymbol = null;

    // Start is called before the first frame update
    void Start()
    {
        pointerLine = gameObject.GetComponent<LineRenderer>();
        Vector3[] startLinePosition = new Vector3[2] {Vector3.zero, Vector3.zero};
        pointerLine.SetPositions(startLinePosition);
        pointerLine.numCornerVertices = 100;
        pointerLine.numCapVertices = 100;
        pointerLine.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (UIEnvironment.paused)
        {
            return;
        }
        
        //set pointerTrigger always to the current right index trigger input
        pointerTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        //only if trigger is pressed all the way
        if (pointerTrigger > 0.9 || alwaysTriggered)
        {
            triggered = true;
            pointerLine.enabled = true;
        }
        else
        {
            triggered = false;
            pointerLine.enabled = false;
        }

        if (triggered)
        {
            DrawPointer();
        }
        else if (activeSymbol)
        {
            activeSymbol.ActiveSymbol = false;
        }
    }

    void DrawPointer()
    {
        //ray from right hand controller
        RaycastHit hit;
        Ray pointerOut = new Ray(transform.position, transform.forward);

        // default length of line
        Vector3 endPosition = transform.position + (lineMaxLength * transform.forward);

        if (Physics.Raycast(pointerOut, out hit))
        {
            //length extended to object that is hit with ray
            endPosition = hit.point;

            //set to object that is hit
            GameObject collidingObject = hit.collider.gameObject;
            if (collidingObject != null && collidingObject.transform.parent != null)
            {
                Symbol pointedSymbol = collidingObject.transform.parent.GetComponent<Symbol>();
                //check if object is a "symbol"
                if (pointedSymbol != null)
                {
                    pointedSymbol.PointedAt(endPosition);
                    activeSymbol = pointedSymbol;
                }
            }
            
        }
        else
        {
            //if no symbol hit, reset last hit symbol
            if (activeSymbol)
            {
                activeSymbol.ActiveSymbol = false;
            }
        }

        //draw line from right hand controller to object
        pointerLine.SetPosition(0, transform.position);
        pointerLine.SetPosition(1, endPosition);
    }
}