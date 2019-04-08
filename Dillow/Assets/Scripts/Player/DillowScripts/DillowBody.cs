using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MoveDel(bool move, Vector3 dir, int jump, int action, int lockon, int lockswap);
public delegate void EndDel();
public delegate void TransformDel(bool ball);

[RequireComponent(typeof(Rigidbody))]
public class DillowBody : Body
{

    #region DILLOW
    [Header("Dillow")]
    //public float dillow_speed = 20f;
    public float dillow_power = 175f;
    public float dillow_roll_drag = 20f;

    public float dillow_static_threshold = 5f;

    public float d_staticFriction = 5;
    public float d_dynoFriction = 5;
    float d_bounciness = 0f;

    float turn_speed = 1f;
    float dillow_jump_power = 20f;
    #endregion

    #region BALL
    [Header("Rolling")]
    public float ball_power = 200f;
    public float ball_drag = 15f;

    public float b_staticFriction = 5;
    public float b_dynoFriction = 5;
    float b_bounciness = 0.5f;

    float airball_power = 10f;
    float max_ball_speed = 50f;

    float ball_jump_power = 10f;
    float speed_jump_threshold = 20f;
    #endregion

    #region JUMPING
    [Header("Jumping")]
    float jump_power = 10f;

    float jump_hold_time = 1f;
     float jump_hold_timer;

     float jump_cooldown_time = 1f;
     bool jump_cooling_down;

     [HideInInspector] public JumpDetector jump_dectector;
     Vector3 jump_vector;
    [HideInInspector] public Vector3 speed_vector;

    [HideInInspector] public bool jump_ready;
    [HideInInspector] public bool mid_air;
    [HideInInspector] public bool air_ready;
    #endregion

    #region MOVEMENT
    SphereCollider coll;
    PhysicMaterial physMat;
    float max_speed = 50f;
    public event MoveDel MoveEvent;
    public event EndDel EndEvent;
    public event TransformDel TransformEvent;
    [HideInInspector ]public bool can_move = true;
    [HideInInspector] public bool ball = true;
    int priority = 0;
    #endregion

    #region ANIMS AND FX
    [Header("FX")]
    public GameObject jump_sound;
    public GameObject death_sound;

    [HideInInspector] public GameObject lock_enemy;
    private int curl_hash, speed_hash, fall_hash;

    private float anim_multiplier = 4f;
    #endregion

    #region DEBUG
    [Header("DEBUG")]
    public bool INVINCIBILITY;
    #endregion

    [HideInInspector] public bool ready;
    private Locker locker;

    // Start is called before the first frame update
    protected override void Start()
    {
        #region MOVEMENT_INITIALIZATION
        base.Start();
        coll = GetComponent<SphereCollider>();
        physMat = coll.material;

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

        locker = transform.parent.GetComponentInChildren<Locker>();

        MoveEvent += OnDash;
        MoveEvent += OnJumpHold;
        MoveEvent += OnLock;
        TransformToBall(true);

        damager = GetComponent<Damager>();
        damager.StunEvent += OnStun;
        damager.StunEndEvent += OnStunEnd;
        damager.DamageAllowEvent += OnDamageAllow;
        damager.DamageEndEvent += OnDamageEnd;
        ready = true;

        if (INVINCIBILITY)
            tagH.Add(Tag.Invincible);
        #endregion
    }

    #region BALL MOVEMENT

    private void OnBallMove(bool move, Vector3 dir, int jump, int action, int lockon, int lockswap)
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

    private void OnBallJump(bool move, Vector3 dir, int jump, int action, int lockon, int lockswap)
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
    private void OnDillowMove(bool move, Vector3 dir, int jump, int action, int lockon, int lockswap)
    {
        if (move)
        {
            if (mid_air)
            {
                if (CheckPriority(1))
                    rb.AddForce(dir * airball_power);
            }
            else
            {
                rb.AddTorque(new Vector3(dir.z, 0, -dir.x) * dillow_power);
                //rb.AddForce(dir * dillow_speed);
            }
        }
        else if (!mid_air && rb.velocity.magnitude < dillow_static_threshold)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnDillowJump(bool move, Vector3 dir, int jump, int action, int lockon, int lockswap)
    {
        if (jump == 2 && jump_ready && !jump_cooling_down && CheckPriority(1))
        {
            Fx_Spawner.instance.SpawnFX(jump_sound, transform.position, Vector3.up);
            rb.AddForce(Vector3.up * jump_power, ForceMode.Impulse);
            StartCoroutine(JumpCD());
        }
    }
    #endregion

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

    public override void Collide(Vector3 pos, List<Tag> tags, GameObject obj, Vector3 direction, Vector3 impact)
    {
        base.Collide(pos, tags, obj, direction, impact);

        if ((tags.Contains(Tag.Damage) || tags.Contains(Tag.SuperDamage)) && !tagH.HasTag(Tag.Dashing))
        {
            if (!tagH.HasTag(Tag.Invincible))
                Damage(direction, pos);
        }
    }

    void Damage(Vector3 dir, Vector3 pos)
    {
        if (next_hit_kills)
        { Die(); }
        else if (!dead)
        { TransformToBall(); }
        damager.Damage(dir, pos);
    }

    public override void Die()
    {
        if (!dead)
        {
            jump_dectector.ResetJump();
            dead = true;
            next_hit_kills = false;
            anim.SetBool(fall_hash, true);
            Fx_Spawner.instance.SpawnFX(death_sound, transform.position, Vector3.up);
            //AudioManager.PlayMusic("Death", false, true, false, 0.25f, false);
            GameManager.instance.Respawn();
        }
    }

    public void Live()
    {
        dead = false;
        next_hit_kills = false;
        anim.SetBool(fall_hash, false);
        //AudioManager.PlayMusic("", false, true, false, 0.25f, false);
    }

    #endregion

    #region INVOLUNTARY MOVEMENT
    private void Update()
    {
        speed_vector = rb.velocity;
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        if (rb.velocity.magnitude > max_speed)
        {
            rb.velocity = rb.velocity.normalized * max_speed;
        }

        float speed_rate = rb.velocity.magnitude / max_speed * anim_multiplier;
        speed_rate = Mathf.Clamp01(speed_rate);
        anim.SetFloat(speed_hash, speed_rate);

        base.FixedUpdate();
    }

    private void OnJumpHold(bool move, Vector3 dir, int jump, int action, int lockon, int lockswap)
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

    public void OnLock(bool move, Vector3 dir, int jump, int action, int lockon, int lockswap)
    {
        if (lockon == 2)
        {
            locker.Lock(!locker.locked);
        }
        else if (lockswap == 2)
            locker.Lock(true);
    }

    public void Input(bool move, Vector3 dir, int jump, int action, int lockon, int lockswap)
    {
        if (can_move && MoveEvent != null)
        {
            MoveEvent(move, dir, jump, action, lockon, lockswap);
        }
    }

    public void End()
    {
        EndEvent?.Invoke();
    }

    public bool CheckPriority(int priority, bool set = false)
    {
        if (priority >= this.priority)
        {
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

    private void OnDash(bool move, Vector3 dir, int jump, int action, int lockon, int lockswap)
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

            tagH.Add(Tag.Attacking);

            ball = true;
            jump_power = ball_jump_power;

            physMat.staticFriction = b_staticFriction;
            physMat.dynamicFriction = b_dynoFriction;
            physMat.bounciness = b_bounciness;

            MoveEvent += OnBallMove;
            MoveEvent += OnBallJump;
            MoveEvent -= OnDillowMove;
            MoveEvent -= OnDillowJump;

            rb.angularDrag = ball_drag;

            TransformEvent?.Invoke(true);
        }
    }

    public void TransformToDillow(bool first = false)
    {
        if (ball || first)
        {
            anim.SetBool(curl_hash, false);

            tagH.Remove(Tag.Attacking);

            ball = false;
            jump_power = dillow_jump_power;

            physMat.staticFriction = d_staticFriction;
            physMat.dynamicFriction = d_dynoFriction;
            physMat.bounciness = d_bounciness;

            MoveEvent += OnDillowMove;
            MoveEvent += OnDillowJump;
            MoveEvent -= OnBallMove;
            MoveEvent -= OnBallJump;

            rb.angularDrag = dillow_roll_drag;

            TransformEvent?.Invoke(false);
        }
    }

    #endregion
}