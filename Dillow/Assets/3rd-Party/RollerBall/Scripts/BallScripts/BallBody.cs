using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MoveDel(bool move, Vector3 dir, int jump, int action);
public delegate void EndDel();

[RequireComponent(typeof(Rigidbody))]
public class BallBody : Body
{

    #region MOVEMENT VARIABLES
    [Header("Rolling")]

    public float roll_power = 200f;
    public float move_power = 10f;

    public float max_roll_speed = 50f;
    public float max_speed = 50f;

    [Header("Jumping")]
    public float jump_power = 10f;

    public float jump_hold_time = 0.5f;
    private float jump_hold_timer;

    private float jump_cooldown_time = 0.5f;
    private bool jump_cooling_down;

    public float speed_jump_threshold = 20f;

    public JumpDetector jump_dectector;

    private float jump_multiplier = 3f; 
    private float fall_multiplier = 3.5f;
    private Vector3 jump_vector;
    [HideInInspector] public Vector3 speed_vector;

    public event MoveDel MoveEvent;
    public event EndDel EndEvent;
    [HideInInspector ]public bool can_move = true;
    private int priority = 0;

    public bool jump_ready;
    [HideInInspector] public bool mid_air;
    [HideInInspector] public bool air_ready;
    #endregion

    [Header("Locking")]
    public GameObject lock_enemy;

    // Start is called before the first frame update
    protected override void Start()
    {
        #region MOVEMENT_INITIALIZATION
        base.Start();
        rb.maxAngularVelocity = max_roll_speed;

        jump_vector = Vector3.up;
        speed_vector = Vector3.zero;

        MoveEvent += OnMove;
        MoveEvent += OnJump;

        jump_dectector = transform.parent.GetComponentInChildren<JumpDetector>();
        jump_dectector.CanJumpEvent += OnGround;
        jump_dectector.StopJumpEvent += OnAir;

        OnGround();
        #endregion

        damager = GetComponent<Damager>();
        damager.StunEvent += OnStun;
        damager.StunEndEvent += OnStunEnd;
        damager.DamageAllowEvent += OnDamage;
        damager.DamageEndEvent += OnDamageEnd;
    }

    public void OnStun()
    {
        can_move = false;
    }

    public void OnStunEnd()
    {
        can_move = true;
    }

    public void OnDamage() { } //not written

    public void OnDamageEnd() { } //not written

    public override void Collide(List<Tag> tags = null, TagHandler t = null, Vector3? direction = null, Vector3? impact = null)
    {
        Vector3 dir = (Vector3)((direction == null) ? Vector3.up : direction);
        Vector3 imp = (Vector3)((impact == null) ? Vector3.zero : impact);
        if (tags == null)
            tags = t.tagList;

        if ( (tags.Contains(Tag.Damage) || tags.Contains(Tag.SuperDamage)))
        {
            damager.Damage(dir);
        }
    }

    #region MOVEMENT
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
        OnFall();
    }

    private void OnFall()
    {
        if (rb.velocity.y < 0f)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fall_multiplier - 1f) * Time.deltaTime;
        }
        if (rb.velocity.y > 0f)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (jump_multiplier - 1f) * Time.deltaTime;
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

    private void OnMove(bool move, Vector3 dir, int jump, int action)
    {
       if (move)
        {
            if (mid_air)
            {
                rb.AddTorque(new Vector3(dir.z, 0, -dir.x) * move_power);
                if (CheckPriority(1))
                    rb.AddForce(dir * move_power);
            }
            else
            {
                rb.AddTorque(new Vector3(dir.z, 0, -dir.x) * roll_power);
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

    private void OnJump(bool move, Vector3 dir, int jump, int action)
    {
        if (jump == 2 && jump_ready && !jump_cooling_down && CheckPriority(1))
        {
            rb.AddForce(jump_vector * jump_power, ForceMode.Impulse);
            StartCoroutine(JumpCD());
        }

        if (mid_air && !air_ready) {
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
}