using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class BaseGolfPlayerController : BaseController, IPlayActions
{
    [Header("_______________", order = 0)]
    [Header("Base Golf Player Controller", order = 1)]
    [Header("--------------------", order = 2)]

    public GolfBall golfBallPreset;
    public bool applyPreset = false;

    [Header("Debug")]
    public bool isOnGreen = false;

    protected Rigidbody rb;
    protected SphereCollider sphereCollider;

    public override void Awake()
    {
        base.Awake();
        JoinPlay();
    }
    public virtual void OnDestroy()
    {
        ExitPlay();
    }

    public void OnValidate()
    {
        if (golfBallPreset != null && applyPreset)
        {
            applyPreset = false;

            rb = GetComponent<Rigidbody>();
            sphereCollider = GetComponent<SphereCollider>();

            rb.mass = golfBallPreset.mass;
            rb.drag = golfBallPreset.drag;
            rb.angularDrag = golfBallPreset.angularDrag;
            rb.collisionDetectionMode = golfBallPreset.detectionMode;

            sphereCollider.material = golfBallPreset.physicMaterial;
            sphereCollider.radius = golfBallPreset.radius;
        }
    }

    void JoinPlay()
    {
        //pID = GameManager.Instance.playStateController.AddPlayer();
        rb = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    void ExitPlay()
    {
        //GameManager.Instance.playStateController.RemovePlayer(pID);
    }


    public void Undo()
    {
        //GameManager.Instance.playStateController.RevertStateChange();
    }
    public void Pause()
    {
        // ???
    }


    public void HitBall(Vector3 _direction, float _force)
    {
        rb.AddForce(_direction * _force, ForceMode.Impulse);
    }
    public void FinishTurn()
    {
        //GameManager.Instance.playStateController.EndTurn(pID);
    }

    public bool IsOnTheGreen()
    {
        // TODO Check underlying collider or component
        return isOnGreen;
    }
}
