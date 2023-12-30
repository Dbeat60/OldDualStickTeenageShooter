using UnityEngine;

public class Key : MonoBehaviour
{

    [NaughtyAttributes.ValidateInput("KeyIdValid", "If id is empty will be ignored")]
    public string id;

    public bool KeyIdValid(string id)
    {
        return id != "";
    }

}
