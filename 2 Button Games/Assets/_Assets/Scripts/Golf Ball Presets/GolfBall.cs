using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Golf Ball Preset", menuName = "Scriptable Obejct/New Golf Ball Preset")]
public class GolfBall : ScriptableObject
{
    [Header("Rigidbody Settings")]
    public float mass = 0.045f;
    public float drag = 0.09f;
    public float angularDrag = 5f;
    public CollisionDetectionMode detectionMode = CollisionDetectionMode.ContinuousDynamic;

    [Header("Sphere Collider Settings")]
    public PhysicMaterial physicMaterial;
    public float radius = 0.05f;
}
