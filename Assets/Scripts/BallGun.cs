using Sirenix.OdinInspector;
using System.Collections;
using TMPro;
using UnityEngine;

public class BallGun : MonoBehaviour
{
    public Transform BallSpawnPoint;
    public float ShootSpeed;
    public float ShootIterations = 0.2f;
    public ObjectPooler Pooler;
    public TextMeshProUGUI BallCountTxt;
    public PlinkoMiniGameController MiniGameController;

    [Header("Turret Sweep")]
    public float sweepAngle = 45f; // How far it turns left/right
    public float sweepSpeed = 1f;  // How fast it sweeps

    private float startRotation;

    void Start()
    {
        startRotation = transform.eulerAngles.z;
    }

    void Update()
    {
        float angle = Mathf.PingPong(Time.time * sweepSpeed, sweepAngle * 2) - sweepAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, startRotation + angle);
    }

    int ballcount = 0;

    void UpdateBallText(int ballCount)
    {
        if (BallCountTxt == null) return;

        this.ballcount = ballCount;
        BallCountTxt.text = ballCount.ToString();
    }

    [Button]
    public void ShootBall(int numberOfBalls)
    {
        UpdateBallText(ballcount + numberOfBalls);
        StartCoroutine(ShootBallsSlowly(numberOfBalls));
    }

    public float randomnessAngle = .1f;

    IEnumerator ShootBallsSlowly(int numberOfBalls)
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            var ball = Pooler.GetFromPool(BallSpawnPoint.position, Quaternion.identity);

            // Add slight randomness to the shoot direction
            Vector3 randomOffset = new Vector3(
                Random.Range(-randomnessAngle, randomnessAngle),
                Random.Range(-randomnessAngle, randomnessAngle),
                Random.Range(-randomnessAngle, randomnessAngle)
            );

            Vector3 shootDirection = (BallSpawnPoint.transform.forward + randomOffset).normalized;

            ball.GetComponent<Rigidbody2D>().AddForce(shootDirection * ShootSpeed, ForceMode2D.Impulse);

            ballcount--;
            UpdateBallText(ballcount);

            yield return new WaitForSeconds(ShootIterations);
        }
    }

    public void Die()
    {
        MiniGameController.Die();
        gameObject.SetActive(false);
    }
}
