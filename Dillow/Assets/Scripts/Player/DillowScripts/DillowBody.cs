﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MoveDel(bool move, Vector3 dir, int jump, int action);
public delegate void EndDel();
public delegate void TransformDel(bool ball);

[RequireComponent(typeof(Rigidbody))]
public class DillowBody : Body
{

    #region DILLOW
    [Header("Dillow")]
    float dillow_speed = 150f;
    float turn_speed = 1f;
    float dillow_jump_power = 20f;
    float dillow_drag = 20f;
    CapsuleCollider dillow_collider;
    #endregion

    #region BALL
    [Header("Rolling")]
    float ball_power = 200f;
    float airball_power = 5f;
    float ball_drag = 5f;

    float max_ball_speed = 50f;

    float ball_jump_power = 10f;
    float speed_jump_threshold = 20f;

    public SphereCollider ball_collider;
    #endregion

    #region JUMPING
    [Header("Jumping")]
    public float jump_power;

    public float jump_hold_time = 1f;
    private float jump_hold_timer;

    private float jump_cooldown_time = 1f;
    private bool jump_cooling_down;

    private JumpDetector jump_dectector;

    private float jump_multiplier = 3f; 
    private float fall_multiplier = 3.5f;
    private Vector3 jump_vector;
    [HideInInspector] public Vector3 speed_vector;

    public bool jump_ready;
    [HideInInspector] public bool mid_air;
    [HideInInspector] public bool air_ready;
    #endregion

    #region MOVEMENT
    float max_speed = 50f;
    public event MoveDel MoveEvent;
    public event EndDel EndEvent;
    public event TransformDel TransformEvent;
    [HideInInspector ]public bool can_move = true;
    int priority = 0;
    public bool ball = true;
    #endregion

    [Header("FX")]
    public GameObject impact_fx;
    public GameObject jump_sound;
    public GameObject death_sound;

    [HideInInspector] public GameObject lock_enemy;

    public bool ready;
    private int curl_hash, speed_hash, fall_hash;

    private float anim_multiplier = 4f;

    // Start is called before the first frame update
    protected override void Start()
    {
        #region MOVEMENT_INITIALIZATION
        base.Start();
        //dillow_collider = GetComponent<CapsuleCollider>();
        ball_collider = GetComponent<SphereCollider>();

        rb.maxAngularVelocity = max_ball_speed;

        jump_vector = Vector3.up;
        speed_vector = Vector3.zero;

        jump_dectector = transform.parent.GetComponentInChildren<JumpDetector>();
        jump_dectector.CanJumpEvent += OnGround;
        jump_dectector.StopJumpEvent += OnAir;

        OnGround();
        #endregion

        #region INITIALIZATION
        anim = transform.parent.GetComponentInChildren<DillowModel>().GetComponentInChildren<Animator>();
        curl_hash = Animator.StringToHash("Curled");
        speed_hash = Animator.StringToHash("Speed");
        fall_hash = Animator.StringToHash("Falling");

        MoveEvent += OnDash;
        MoveEvent += OnJumpHold;
        TransformToBall(true);

        damager = GetComponent<Damager>();
        damager.StunEvent += OnStun;
        damager.StunEndEvent += OnStunEnd;
        damager.DamageAllowEvent += OnDamageAllow;
        damager.DamageEndEvent += OnDamageEnd;
        ready = true;
        #endregion
    }

    #region DAMAGE

    public void OnStun()
    {
        can_move = false;
    }

    public void OnStunEnd()
    {
        can_move = true;
    }

    public void OnDamageAllow()
    {
        next_hit_kills = true;
    }

    public void OnDamageEnd()
    {
        next_hit_kills = false;
    }

    public override void Collide(Vector3 pos, List<Tag> tags = null, TagHandler t = null, Vector3? direction = null, Vector3? impact = null)
    {
        Vector3 dir = (Vector3)((direction == null) ? Vector3.up : direction);
        Vector3 imp = (Vector3)((impact == null) ? Vector3.zero : impact);
        if (tags == null)
            tags = t.tagList;

        if ( (tags.Contains(Tag.Damage) || tags.Contains(Tag.SuperDamage)))
        {
            Damage(dir, pos);
        }
    }

    void Damage(Vector3 dir, Vector3 pos)
    {
        if (!dead)
        {
            if (next_hit_kills)
            {
                dead = true;
                next_hit_kills = false;
                anim.SetBool(fall_hash, true);
                Fx_Spawner.instance.SpawnFX(death_sound, transform.position, Vector3.up);
                GameManager.instance.Respawn();
            }
            else
            {
                TransformToBall();
            }
        }
        damager.Damage(dir, pos);
    }

    public void Live()
    {
        dead = false;
        next_hit_kills = false;
        anim.SetBool(fall_hash, false);
    }

    #endregion

    #region INVOLUNTARY MOVEMENT
    private void Update()
    {
        speed_vector = rb.velocity;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity.magnitude > max_speed)
        {
            rb.velocity = rb.velocity.normalized * max_speed;
        }

        float speed_rate = (rb.velocity.magnitude / max_speed) * anim_multiplier;
        anim.SetFloat(speed_hash, speed_rate);

        OnFall();
    }

    public override void OnCollisionEnter(Collision c)
    {
        base.OnCollisionEnter(c);

        if (impact_fx && (c.collider.CompareTag("Ground") || c.collider.CompareTag("Ground Terrain")))
            if (c.impulse.magnitude > 10f)
            {
                float vol = Mathf.Clamp01(c.impulse.magnitude / 40f);
                Fx_Spawner.instance.SpawnFX(impact_fx, c.contacts[0].point, c.contacts[0].normal, vol);
            }
    }

    private void OnFall()
    {
        if (!rb.useGravity)
            return;
        if (rb.velocity.y < 0f)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fall_multiplier - 1f) * Time.deltaTime;
        }
        if (rb.velocity.y > 0f)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (jump_multiplier - 1f) * Time.deltaTime;
        }
    }

    private void OnJumpHold(bool move, Vector3 dir, int jump, int action)
    {
        if (mid_air && !air_ready)
        {
            if (jump == 1 && jump_hold_timer > 0f && rb.velocity.y > 0f)
            {
                rb.AddForce(jump_vector * jump_power);
                jump_hold_timer -= Time.deltaTime;
            }
            else if (jump == -1)
            {
                air_ready = true;
            }
        }
    }

    public void Input(bool move, Vector3 dir, int jump, int action)
    {
       if (can_move && MoveEvent != null)
        {
            MoveEvent(move, dir, jump, action);
        }
    }

    public void End()
    {
        EndEvent?.Invoke();
    }

    public bool CheckPriority(int priority, bool set = false)
    {
        if (priority >= this.priority) {
            if (set)
                this.priority = priority;
            return true;
        }
        return false;
    }

    public void ResetPriority()
    {
        priority = 0;
    }
    #endregion

    #region BALL MOVEMENT

    private void OnBallMove(bool move, Vector3 dir, int jump, int action)
    {
       if (move)
        {
            if (mid_air)
            {
                rb.AddTorque(new Vector3(dir.z, 0, -dir.x) * airball_power);
                if (CheckPriority(1))
                    rb.AddForce(dir * airball_power);
            }
            else
            {
                rb.AddTorque(new Vector3(dir.z, 0, -dir.x) * ball_power);
            }
        }
        //Calculate jump direction
        if (rb.velocity.magnitude > speed_jump_threshold)
        {
            if (Vector3.Cross(rb.angularVelocity, rb.velocity).y > 0f)
                jump_vector = (-rb.velocity.normalized + Vector3.up * 2f).normalized;
        }
        else
            jump_vector = Vector3.up;
    }

    private void OnBallJump(bool move, Vector3 dir, int jump, int action)
    {
        if (jump == 2 && jump_ready && !jump_cooling_down && CheckPriority(1))
        {
            Fx_Spawner.instance.SpawnFX(jump_sound, transform.position, Vector3.up);
            rb.AddForce(jump_vector * ball_jump_power, ForceMode.Impulse);
            TransformToDillow();
            StartCoroutine(JumpCD());
        }
    }

    #endregion

    #region DILLOW MOVEMENT
    private void OnDillowMove(bool move, Vector3 dir, int jump, int action)
    {
        if (move)
        {
            if (mid_air)
            {
                rb.AddTorque(new Vector3(dir.z, 0, -dir.x) * airball_power);
                if (CheckPriority(1))
                    rb.AddForce(dir * airball_power);
            }
            else
            {
                rb.AddTorque(new Vector3(dir.z, 0, -dir.x) * dillow_speed);
            }
        }
    }

    private void OnDillowJump(bool move, Vector3 dir, int jump, int action)
    {
        if (jump == 2 && jump_ready && !jump_cooling_down && CheckPriority(1))
        {
            Fx_Spawner.instance.SpawnFX(jump_sound, transform.position, Vector3.up);
            rb.AddForce(Vector3.up * jump_power, ForceMode.Impulse);
            StartCoroutine(JumpCD());
        }
    }
    #endregion

    #region PHYSICS FLAGS

    private void OnGround()
    {
        jump_ready = true;
        jump_hold_timer = jump_hold_time;
        mid_air = false;
        air_ready = false;
    }

    private void OnAir()
    {
        jump_ready = false;
        mid_air = true;
        air_ready = true;
    }

    IEnumerator JumpCD()
    {
        jump_cooling_down = true;
        yield return new WaitForSeconds(jump_cooldown_time);
        jump_cooling_down = false;
    }

    #endregion

    #region TRANSFORM

    private void OnDash(bool move, Vector3 dir, int jump, int action)
    {
        if (action == 2)
        {
            TransformToBall();
        }
    }

    public void TransformToBall(bool first = false)
    {
        if (!ball || first)
        {
            anim.SetBool(curl_hash, true);

            ball = true;
            jump_power = ball_jump_power;

            MoveEvent += OnBallMove;
            MoveEvent += OnBallJump;
            MoveEvent -= OnDillowMove;
            MoveEvent -= OnDillowJump;

            //ball_collider.enabled = true;
            //dillow_collider.enabled = false;

            rb.angularDrag = ball_drag;
            //rb.constraints = RigidbodyConstraints.None;

            TransformEvent?.Invoke(true);
        }
    }

    public void TransformToDillow(bool first = false)
    {
        if (ball || first)
        {
            anim.SetBool(curl_hash, false);

            ball = false;
            jump_power = dillow_jump_power;

            MoveEvent += OnDillowMove;
            MoveEvent += OnDillowJump;
            MoveEvent -= OnBallMove;
            MoveEvent -= OnBallJump;

            //ball_collider.enabled = false;
            //dillow_collider.enabled = true;

            rb.angularDrag = dillow_drag;
            //rb.constraints = RigidbodyConstraints.FreezeRotation;

            TransformEvent?.Invoke(false);
        }
    }

    #endregion
}