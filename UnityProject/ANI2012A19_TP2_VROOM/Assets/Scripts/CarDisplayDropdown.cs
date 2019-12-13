using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class CarDisplayDropdown : MonoBehaviour
{
    public Car Car;

    private const string FIXE_VALUE = "Fixe";
    private const string ROTATION_VALUE = "Rotation";
    private const string DECOMPOSE_VALUE = "Décomposé";
   
    private Dropdown _dropdownComponent;
    private int _value;
    private string[] _labels = { FIXE_VALUE, ROTATION_VALUE, DECOMPOSE_VALUE };


    public void OnValueChanged()
    {
        if (!this._dropdownComponent)
        {
            return;
        }

        this._value = this._dropdownComponent.value;

        switch (this._value)
        {
            case CarStates.ROTATION:
                this.Car.SetState(CarStates.ROTATION);
                break;
            case CarStates.SHOW_PIECES:
                this.Car.SetState(CarStates.SHOW_PIECES);
                break;
            default:
                this.Car.SetState(CarStates.DEFAULT);
                break;
        }
    }

    public void ResetToInitalValue()
    {
        this._dropdownComponent.value = 0;
        this.OnValueChanged();
    }


    private void Start()
    {
        this._dropdownComponent = this.gameObject.GetComponent<Dropdown>();

        if (this._dropdownComponent)
        {
            this._dropdownComponent.ClearOptions();

            List<Dropdown.OptionData> Items = new List<Dropdown.OptionData>();

            foreach (var value in this._labels)
            {
                Items.Add(new Dropdown.OptionData(value));
            }

            this._dropdownComponent.AddOptions(Items);
            this._dropdownComponent.value = 0;
        }
        
    }
}
