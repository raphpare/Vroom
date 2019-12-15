using System.Collections.Generic;
using UnityEngine;

public class CarTransitionStates
{
    public const int DEFAULT_TO_ROTATION = 0;
    public const int DEFAULT_TO_PIECES = 1;
    public const int ROTATION_TO_DEFAULT = 2;
    public const int ROTATION_TO_PIECES = 3;
    public const int PIECES_TO_DEFAULT = 4;
    public const int PIECES_TO_ROTATION = 5;
}

public class CarStates
{
    public const int DEFAULT = 0;
    public const int ROTATION = 1;
    public const int SHOW_PIECES = 2;
}

[RequireComponent(typeof(Rigidbody))]
public class Car : MonoBehaviour
{
    static public int BodyColorId;
    static public bool IsInStartMethodeForFirtTime = true;
    static public bool LightsOpen;
    public const string BODY_COLOR_ID = "CarBodyColorId";

    public GameObject Body;
    public GameObject CarContainer;
    public ColorButtonGroup ColorButtonGroup;
    public Material[] CarColors;
    public List<Light> Lights;
    public bool RotationMode = false;
    public Texture2D HoverCursorDark;
    public Texture2D HoverCursorLight;

    private const string TRANSITION_MODE = "trasitionMode";
    private int _state;
    private Animator _stateAnimator;

    private Vector3 _initalPosition;
    private Vector3 _initalRotation;
    private float _force = 500.0f;
    private bool _isDrag;
    private float _sensitivityForDrag = 0.2f;
    private Vector3 _mouseReferenceForDrag;
    private Vector3 _mouseOffsetForDrag = Vector3.zero;
    private Vector3 _rotationForDrag;
    private Rigidbody _rigidbody;
    private bool _isCarHover;

    public void SaveActiveMaterialInPlayerPrefs()
    {
        PlayerPrefs.SetInt(BODY_COLOR_ID, Car.BodyColorId);
    }

    public void SetToInitialPositionAndRotation()
    {
        this.gameObject.transform.position = this._initalPosition;
        this.gameObject.transform.eulerAngles = this._initalRotation;
    }

    public void ResetToInitalValue()
    {
        this.SetToInitialPositionAndRotation();
        Car.BodyColorId = PlayerPrefs.GetInt(BODY_COLOR_ID);
        this.SetBodyColor(Car.BodyColorId);
    }

    public void SetLights(bool lightsOpen)
    {
        LightsOpen = lightsOpen;

        MeshRenderer meshRendererComponent = this.Body.GetComponent<MeshRenderer>();

        if (LightsOpen)
        {
            meshRendererComponent.materials[3].SetColor("_Color", new Color(0.14f, 0.14f, 0.24f));
            meshRendererComponent.materials[4].SetColor("_EmissionColor", new Color(0.3f, 0.0f, 0.0f));
            meshRendererComponent.materials[5].SetColor("_EmissionColor", new Color(0.2f, 0.0f, 0.0f));
            meshRendererComponent.materials[6].SetColor("_EmissionColor", Color.white);
            meshRendererComponent.materials[8].SetColor("_EmissionColor", new Color(0.8f, 0.8f, 0.6f));
        }
        else
        {
            meshRendererComponent.materials[3].SetColor("_Color", Color.white);
            meshRendererComponent.materials[4].SetColor("_EmissionColor", Color.black);
            meshRendererComponent.materials[5].SetColor("_EmissionColor", Color.black);
            meshRendererComponent.materials[6].SetColor("_EmissionColor", new Color(0.25f, 0.25f, 0.25f));
            meshRendererComponent.materials[8].SetColor("_EmissionColor", new Color(0.0f, 0.0f, 0.0f));
        }

        foreach (Light carLight in this.Lights)
        {
            carLight.intensity = LightsOpen ? 2.0f : 0.01f;
        }
    }

    public void SetBodyColor(int bodyColorId)
    {
        Car.BodyColorId = bodyColorId;
        this.Body.GetComponent<Renderer>().sharedMaterial = this.CarColors[Car.BodyColorId];
    }

    public void SetState(int newState)
    {
        if (this._state == newState)
        {
            return;
        }

        if (this._state == CarStates.DEFAULT)
        {
            switch (newState)
            {
                case CarStates.ROTATION:
                    this._stateAnimator.SetInteger(TRANSITION_MODE, CarTransitionStates.DEFAULT_TO_ROTATION);
                    break;
                case CarStates.SHOW_PIECES:
                    this._stateAnimator.SetInteger(TRANSITION_MODE, CarTransitionStates.DEFAULT_TO_PIECES);
                    break;
            }
        }


        if (this._state == CarStates.ROTATION)
        {
            switch (newState)
            {
                case CarStates.DEFAULT:
                    this._stateAnimator.SetInteger(TRANSITION_MODE, CarTransitionStates.ROTATION_TO_DEFAULT);
                    break;
                case CarStates.SHOW_PIECES:
                    this._stateAnimator.SetInteger(TRANSITION_MODE, CarTransitionStates.ROTATION_TO_PIECES);
                    break;
            }
        }

        if (this._state == CarStates.SHOW_PIECES)
        {

            switch (newState)
            {
                case CarStates.DEFAULT:
                    this._stateAnimator.SetInteger(TRANSITION_MODE, CarTransitionStates.PIECES_TO_DEFAULT);
                    break;
                case CarStates.ROTATION:
                    this._stateAnimator.SetInteger(TRANSITION_MODE, CarTransitionStates.PIECES_TO_ROTATION);
                    break;
            }
        }

        this._state = newState;
    }

    private void Start()
    {
        this._stateAnimator = this.CarContainer.GetComponent<Animator>();
        this._state = CarStates.DEFAULT;


        this._rigidbody = this.gameObject.GetComponent<Rigidbody>();
        this._rigidbody.useGravity = !this.RotationMode;

        this._initalPosition = transform.position;
        this._initalRotation = transform.eulerAngles;

        if (Car.IsInStartMethodeForFirtTime)
        {
            Car.BodyColorId = PlayerPrefs.GetInt(BODY_COLOR_ID);
            Car.IsInStartMethodeForFirtTime = false;
        }

        this.SetBodyColor(Car.BodyColorId);
        this.SetLights(LightsOpen);

        if (this.ColorButtonGroup)
        {
            this.ColorButtonGroup.UpdateColorsButtons(Car.BodyColorId);
        }
    }

    private void Update()
    {
        this._updateRotationMode();
    }

    private void OnMouseDown()
    {
        this._lauchIntoAir();

        if (this.RotationMode)
        {
            this._isDrag = true;
            this._mouseReferenceForDrag = Input.mousePosition; 
        }
        this._setCursor();

    }

    private void OnMouseUp()
    {
        if (this._isDrag)
        {
            this._isDrag = false;
        }

        this._setCursor();
    }

    private void OnMouseEnter()
    {
        this._isCarHover = true;
        this._setCursor();
    }

    private void OnMouseExit()
    {
        this._isCarHover = false;
        this._setCursor();        
    }

    private void _setCursor()
    {
        if (this._isCarHover || this._isDrag)
        {
            Texture2D cursor = (LightsOpen && this.RotationMode) ? this.HoverCursorLight : this.HoverCursorDark;
            Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        } else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    } 

    private void _updateRotationMode()
    {
        if (!this.RotationMode)
        {
            return;
        }

        if (this._isDrag)
        {

            this._mouseOffsetForDrag = (Input.mousePosition - this._mouseReferenceForDrag);

            this._rotationForDrag.y = -(this._mouseOffsetForDrag.x) * this._sensitivityForDrag;

            this._rotationForDrag.x = -(this._mouseOffsetForDrag.y) * this._sensitivityForDrag;

            transform.eulerAngles += _rotationForDrag;
            this._mouseReferenceForDrag = Input.mousePosition;
        }

        if (Input.GetKey("left"))
        {
            this._rotateAround(Vector3.up);
        }

        if (Input.GetKey("right"))
        {
            this._rotateAround(Vector3.down);
        }

        if (Input.GetKey("up"))
        {
            this._rotateAround(Vector3.right);
        }

        if (Input.GetKey("down"))
        {
            this._rotateAround(Vector3.left);
        }

    }

    private void _lauchIntoAir()
    {
        if (this.RotationMode)
        {
            return;
        }

        this._rigidbody.AddForce(this._rigidbody.transform.up * this._force, ForceMode.Acceleration);
    }

    private void _rotateAround(Vector3 position)
    {
        if (!this.RotationMode)
        {
            return;
        }
        transform.RotateAround(transform.position, position, 30 * Time.deltaTime);
    }
}
