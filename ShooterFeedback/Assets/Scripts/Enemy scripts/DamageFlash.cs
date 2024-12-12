using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private DamageFlash flashEffect;
    [SerializeField] private KeyCode flashKey;

    private void Update()
    {
        if (Input.GetKeyDown(flashKey))
        {
            flashEffect.Update();
        }
    }

}
