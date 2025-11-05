using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;


[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = GameManager.GetInstance().playerSpeed;

    [Header("Jump State")]
    public float jumpVelocity = 30f;


    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    public float distBetweenAfterImages = 0.5f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;

    [Header("Shoot Variables")]
    public float spellVelocity = 20f;
    public float spellCooldown = 0.5f;

    [Header("Player Health")]
    public int maxHealth = 8;
}
