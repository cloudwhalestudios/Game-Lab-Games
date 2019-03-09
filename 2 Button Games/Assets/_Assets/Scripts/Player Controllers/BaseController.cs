using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Input;
using UnityEngine.Experimental.Input.Plugins.PlayerInput;


public abstract class BaseController : MonoBehaviour
{
    [Header("_______________", order = 0)]
    [Header("Base Controller", order = 1)]
    [Header("--------------------", order = 2)]

    protected Player owner;

    public virtual void Awake()
    {
        if (owner == null)
        {
            owner = GetComponent<Player>();
            if (owner == null)
            {
                Debug.LogWarning("Trying to add a controller without an owner!");
                enabled = false;
            }
        }
    }
}
