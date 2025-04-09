using UnityEngine;

public class Pillar : MonoBehaviour
{

    public Player player;
    public float midDistance;
    public float maxDistance;
    private MeshRenderer mr;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player=GameManager.Instance().player;
        mr = GetComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Shader Graphs/PillarOfLight"));
        mr.material.SetColor("_Color", new Color(191f/255,19f/255,182f/255)*60f);
        mr.material.SetFloat("_Alpha", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        player=GameManager.Instance().player;
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance > maxDistance)
        {
            mr.material.SetFloat("_Alpha", 0f);
            return;
        }

        float newAlpha = 0f;
        if (distance < maxDistance)
        {
            newAlpha = Mathf.Lerp(0f, 0.2f, 1-(distance / maxDistance));
        }
        
        if (distance < midDistance)
        {
            newAlpha = Mathf.Lerp(0.2f, 0f, 1-((distance-150) / midDistance));
        }
        mr.material.SetFloat("_Alpha",newAlpha);
    }
}
