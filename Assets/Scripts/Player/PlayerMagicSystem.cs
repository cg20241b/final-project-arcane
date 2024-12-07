using UnityEngine;

public class PlayerMagicSystem : MonoBehaviour
{
    [SerializeField] private Spell spellToCast;

    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana;
    [SerializeField] private float manaRechargeRate = 2f;
    [SerializeField] private float timeBetweenCast = 0.25f;
    private float currentCastTimer;

    [SerializeField] private Transform castPoint;

    private bool castingMagic = false;

    private PlayerControl playerControl;

    private void Awake() 
    {
        playerControl = new PlayerControl();
    }

    private void OnEnable() 
    {
        playerControl.Enable();
    }

    private void OnDisable() 
    {
        playerControl.Disable();
    }

    private void Update() 
    {
        bool isSpellCastHeldDown = playerControl.Controls.SpellCast.ReadValue<float>() > 0.1;

        if(!castingMagic && isSpellCastHeldDown)
        {
            castingMagic = true;
            currentCastTimer = 0;
            CastSpell();
        }

        if(castingMagic)
        {
            currentCastTimer += Time.deltaTime;

            if(currentCastTimer > timeBetweenCast)
            {
                castingMagic = false;
            }
        }
    }

    void CastSpell()
    {
        Instantiate(spellToCast, castPoint.position, castPoint.rotation);
    }


}
