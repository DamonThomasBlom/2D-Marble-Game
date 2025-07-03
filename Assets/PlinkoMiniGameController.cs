using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlinkoMiniGameController : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI NumberOfBallsText;
    public BallGun BallGun;
    public Transform SlotsParent;

    [Header("Ball Settings")]
    public GameObject ballPrefab;
    public Transform spawnAreaTopLeft;
    public Transform spawnAreaTopRight;

    [Header("Drop Settings")]
    public float respawnDelay = 0.5f;

    [Header("Slot Triggers")]
    public List<PlinkoSlot> slots; // Assign these manually or find them via tag
    public Vector3 targetScale = new Vector3(1.5f, 1f, 1f); // Final desired scale
    public float durationInMinutes = 10f;

    [HideInInspector]
    public int BallCount = 1;

    void Start()
    {
        AddBall();
        StartCoroutine(ScaleOverTime());
    }

    IEnumerator ScaleOverTime()
    {
        float duration = durationInMinutes * 60f; // convert minutes to seconds
        Vector3 initialScale = SlotsParent.localScale;
        Vector3 finalScale = new Vector3(targetScale.x, initialScale.y, initialScale.z);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float newX = Mathf.Lerp(initialScale.x, finalScale.x, t);
            SlotsParent.localScale = new Vector3(newX, initialScale.y, initialScale.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        SlotsParent.localScale = finalScale; // Ensure it ends exactly at target
    }

    [Button]
    public void AddBall()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(spawnAreaTopLeft.position.x, spawnAreaTopRight.position.x),
            spawnAreaTopLeft.position.y,
            0f
        );

        var currentBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        PlinkoBall ball = currentBall.GetComponent<PlinkoBall>();
        ball.controller = this;
    }

    public void ResetBallPosition(GameObject ball)
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(spawnAreaTopLeft.position.x, spawnAreaTopRight.position.x),
            spawnAreaTopLeft.position.y,
            0f
        );

        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        ball.transform.position = spawnPos;
    }

    public void Release()
    {
        BallGun.ShootBall(BallCount);
        SetBallCount(1);
    }

    public void DoubleBallCount()
    {
        SetBallCount(BallCount * 2);
    }

    public void TripleBallCount()
    {
        SetBallCount(BallCount * 3);
    }

    void SetBallCount(int count)
    {
        BallCount = count;
        if (NumberOfBallsText)
            NumberOfBallsText.text = count.ToString();

        if (BallCount > 1000)
            Release();
    }

    public void Die()
    {
        foreach (var slot in slots) { slot.Die(); }
    }

    [Button]
    void DebugStats()
    {
        int total = 0;
        foreach (var slot in slots)
            total += slot.count;

        if (total == 0)
        {
            Debug.Log("Total count is 0 — cannot compute percentages.");
            return;
        }

        string debug = string.Empty;
        foreach (var slot in slots)
        {
            float percent = (slot.count / (float)total) * 100f;
            debug += $"{slot.name}: {percent:F1}%    ";
        }

        Debug.Log(debug);
    }
}
