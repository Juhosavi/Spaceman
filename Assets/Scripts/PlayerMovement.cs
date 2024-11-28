using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public GameObject ammoSpawn;
    public GameObject ammo;
    public GameManager manager;
    public bool canshoot;
    public Animator animator;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manager = FindAnyObjectByType<GameManager>();
        canshoot = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Liikutetaan pelaajaa vaakasuunnassa
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 newPosition = transform.position + new Vector3(horizontalInput * moveSpeed * Time.deltaTime, 0, 0);

        // Rajoitetaan pelaajan liike x-koordinaattien -8 ja +8 v‰lille
        newPosition.x = Mathf.Clamp(newPosition.x, -8f, 8f);

        // P‰ivitet‰‰n pelaajan sijainti
        transform.position = newPosition;

        // K‰‰nnet‰‰n hahmo oikeaan suuntaan liikesuunnan mukaan
        if (horizontalInput > 0)
        {
            // Oikealle suuntaan, k‰‰nnet‰‰n hahmo katsomaan oikealle (positiivinen x)
            transform.localScale = new Vector3(-1, 1, 1); // Varmista, ett‰ t‰m‰ on oikea suunta.
        }
        else if (horizontalInput < 0)
        {
            // Vasemmalle suuntaan, k‰‰nnet‰‰n hahmo katsomaan vasemmalle (negatiivinen x)
            transform.localScale = new Vector3(1, 1, 1); // Varmista, ett‰ t‰m‰ on vasemmalle.
        }
        animator.SetBool("Run", horizontalInput != 0);

        // Ammutaan ammus
        ShootAmmo();
    }


    void ShootAmmo()
    {
        if(Input.GetKeyDown(KeyCode.Space) && canshoot == true) 
        {
            Instantiate(ammo, ammoSpawn.transform.position, Quaternion.identity);
            canshoot = false;
            animator.SetTrigger("realShoot");
        }
    }


   
}
