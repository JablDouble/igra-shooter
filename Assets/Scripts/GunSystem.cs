using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{

    public FixedButton FireButton;
	public FixedButton ReloadButton;

    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Transform player;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    public TextMeshProUGUI text;

    public AudioClip reloadAudio, shootAudio;
    protected AudioSource AudioSorcePlayer;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        AudioSorcePlayer = GetComponent<AudioSource>();
    }
    private void Update()
    {
        MyInput();

        //SetText
        text.SetText(bulletsLeft + " / " + magazineSize);
    }
    private void MyInput()
    {
        float x = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = player.transform.forward + new Vector3(x, 0, 0);

        //RayCast
        if (Physics.Raycast(player.transform.position + new Vector3(0, 1.4f, 0), direction, out rayHit, range, whatIsEnemy))
        {

            if (rayHit.collider.CompareTag("Enemy")) {
                if (rayHit.collider.GetComponent<Enemy>().health > 0) {
                    if (readyToShoot && !reloading && bulletsLeft > 0){
                        rayHit.collider.GetComponent<Enemy>().TakeDamage(damage);
                        bulletsShot = bulletsPerTap;
                        Shoot();
                    }
                }
            }
        }


        if ((ReloadButton.isPressed || bulletsLeft == 0) && !reloading) {
            Reload();
        }

    }
    private void Shoot()
    {
        readyToShoot = false;

        //Graphics
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.Euler(0, 160, 0));
        AudioSorcePlayer.PlayOneShot(shootAudio);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        player.gameObject.GetComponent<Animator>().SetTrigger("Reload");
        float playerSpeed = player.gameObject.GetComponent<PlayerController>().movementSpeed;
        player.gameObject.GetComponent<PlayerController>().movementSpeed = playerSpeed / 2;
        AudioSorcePlayer.PlayOneShot(reloadAudio);
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        float playerSpeed = player.gameObject.GetComponent<PlayerController>().movementSpeed;
        player.gameObject.GetComponent<PlayerController>().movementSpeed = playerSpeed * 2;
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
