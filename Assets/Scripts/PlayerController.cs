using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;
    [Header("Health")]
    private Animator anim;
    private float PlayerMove;
    [SerializeField] float speed;
  
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance == this)
        {
            Destroy(gameObject);
        }
        anim = GetComponentInChildren<Animator>();
    }
    
    void Update()
    {
        MovePlayer();        
    }

    private void MovePlayer()
    {
        float movemment = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        transform.position += new Vector3(movemment, 0);
    }
    
   

}
