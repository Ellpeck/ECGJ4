using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour {

    public float speed;
    public float jumpForce;
    public Transform groundCheck;
    public LayerMask groundLayers;
    public string leftInput;
    public string rightInput;
    public string jumpInput;

    private Rigidbody2D body;

    private bool jump;
    private float horizontalMovement;

    private void Start() {
        this.body = this.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        this.horizontalMovement = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(this.jumpInput))
            this.jump = true;
    }

    private void FixedUpdate() {
        this.body.AddForce(new Vector2(this.horizontalMovement * this.speed, 0));

        if (this.jump) {
            var ground = Physics2D.OverlapCircle(this.groundCheck.position, 0.2F, this.groundLayers);
            if (ground) {
                this.body.AddForce(new Vector2(0, this.jumpForce));
            }
            this.jump = false;
        }
    }

}