using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyUfoController : MonoBehaviour
{
    public GameObject EnemyUfoBeamPrefab;
    public float MinX;
    public float MaxX;
    public float MinY;
    public float MaxY;
    public float OffsetY;
    public float AttackInterval;
    public float BeamActiveDuration;
    public float MaxOpacity;
    public float MinOpacity;
    public float opacityStep;

    Material material;
    GameObject player;
    GameObject beam;
    Material beamMaterial;
    Collider2D beamCollider;
    Vector3 targetPosition;
    int attackPhase;
    float targetOpacity;
    float targetBeamOpacity;
    float beamActiveTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        beam = Instantiate(EnemyUfoBeamPrefab, transform);
        beam.SetActive(false);
        beamMaterial = beam.GetComponent<SpriteRenderer>().material;
        beamCollider = beam.GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player");
        material.color = new Color(material.color.r, material.color.g, material.color.b,
                                   MinOpacity);
        GoOffScreen();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOpacity(material, targetOpacity, opacityStep);
        UpdateOpacity(beamMaterial, targetBeamOpacity, opacityStep);
        targetPosition = new Vector3(player.transform.position.x,
                                            player.transform.position.y + OffsetY,
                                            transform.position.z);
        switch (attackPhase)
        {
            case 0:
                if (targetPosition.x < MinX || targetPosition.x > MaxX ||
                    targetPosition.y < MinY || targetPosition.y > MaxY)
                {
                    GoOffScreen();
                    break;
                }
                targetOpacity = MaxOpacity;
                gameObject.SetActive(true);
                beam.SetActive(true);
                attackPhase++;
                break;
            case 1:
                transform.position = targetPosition;
                beam.transform.position = transform.position + OffsetY * Vector3.down;
                if (Mathf.Approximately(material.color.a, MaxOpacity)) attackPhase++;
                break;
            case 2:
                beamMaterial.color = new Color(beamMaterial.color.r, beamMaterial.color.g,
                                               beamMaterial.color.b, MinOpacity);
                targetBeamOpacity = MaxOpacity;
                attackPhase++;
                break;
            case 3:
                if (Mathf.Approximately(beamMaterial.color.a, MaxOpacity))
                {
                    beamCollider.enabled = true;
                    beamActiveTimer = 0f;
                    attackPhase++;
                }
                break;
            case 4:
                beamActiveTimer += Time.deltaTime;
                if (beamActiveTimer >= BeamActiveDuration)
                {
                    attackPhase++;
                }
                break;
            case 5:
                beamCollider.enabled = false;
                targetBeamOpacity = MinOpacity;
                attackPhase++;
                break;
            case 6:
                if (Mathf.Approximately(beamMaterial.color.a, MinOpacity))
                {
                    beam.SetActive(false);
                    attackPhase++;
                }
                break;
            case 7:
                targetOpacity = MinOpacity;
                attackPhase++;
                break;
            case 8:
                if (Mathf.Approximately(material.color.a, MinOpacity))
                {
                    GoOffScreen();
                }
                break;
            default:
                attackPhase = -1;
                Invoke(nameof(Attack), AttackInterval);
                break;
        }
    }

    void Attack()
    {
        if (attackPhase == -1)
            attackPhase = 0;
    }

    void GoOffScreen()
    {
        attackPhase = -1;
        targetOpacity = MinOpacity;
        targetBeamOpacity = MinOpacity;
        beamCollider.enabled = false;
        transform.position = new Vector3(0, 300, transform.position.z);
    }

    static void UpdateOpacity(Material material, float targetOpacity, float opacityStep)
    {
        float currentOpacity = material.color.a;
        float step = opacityStep * Time.deltaTime * 10;
        if (Mathf.Abs(currentOpacity - targetOpacity) < step)
        {
            currentOpacity = targetOpacity;
        }
        else if (currentOpacity < targetOpacity)
        {
            currentOpacity += step;
        }
        else if (currentOpacity > targetOpacity)
        {
            currentOpacity -= step;
        }
        material.color = new Color(material.color.r, material.color.g, material.color.b,
                                   currentOpacity);
    }
}
