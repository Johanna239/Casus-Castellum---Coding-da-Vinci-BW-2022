using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private Animator animator;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private float speed = 0.12f;


    public DialogueUI DialogueUI => dialogueUI;

    public bool StopMovement { get => stopMovement; set => stopMovement = value; }

    private Vector3 mousePosWorld;
    private Vector2 mousePosWorld2D;
    private RaycastHit2D hit;
    private Vector2 targetPos;
    private bool isMoving;
    private bool stopMovement;
    private FMOD.Studio.EventInstance footstepInstance;


    // Update is called once per frame
    void Update()
    {
        // do not do anything else if a dialogue is open and disable setting a new point while still walking
        if (dialogueUI.IsOpen || isMoving || stopMovement) return;

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            // mousePosWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosWorld = GetWorldPositionOnPlane(Input.mousePosition, 0);
            mousePosWorld2D = new Vector2(mousePosWorld.x, mousePosWorld.y);
            hit = Physics2D.Raycast(mousePosWorld2D, Vector2.zero);

            // if click was on walkableArea, reorient character and start moving
            if (hit && hit.collider.gameObject.tag == "Ground")
            {
                targetPos = new Vector2(hit.point.x, player.transform.position.y);
                isMoving = true;
                CheckSpriteFlip();
            }
        }
    }

    public void updatePosition(Transform targetPosition) {
        targetPos = new Vector2(targetPosition.position.x, player.transform.position.y);
        isMoving = true;
        CheckSpriteFlip();
    }

    private void FixedUpdate()
    {
        // start animation while player is walking towards destination
        if (isMoving)
        {
            animator.SetBool("IsWalking", true);
            player.transform.position = Vector3.MoveTowards(player.transform.position, targetPos, speed);

            // stop animation if player arrives at destination
            if (player.transform.position.x == targetPos.x)
            {
                isMoving = false;
                animator.SetBool("IsWalking", false);
            }
        }

    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

    void CheckSpriteFlip()
    {
        // click left and scale -1 or click right and scale 1
        if ((player.transform.position.x > targetPos.x && player.transform.localScale.x <= 0) || (player.transform.position.x < targetPos.x && player.transform.localScale.x >= 0))
        {
            player.transform.localScale = new Vector3(player.transform.localScale.x * -1, player.transform.localScale.y, player.transform.localScale.z);
        }

    }
    public void PlayFootstep()
    {
        footstepInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps");
        footstepInstance.start();
        footstepInstance.release();
    }
}
