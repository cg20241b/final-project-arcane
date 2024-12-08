using UnityEngine;

public class PlayerMagicSystem : MonoBehaviour
{
    [SerializeField] private Spell[] spellsToCast; // Array untuk menyimpan beberapa spell
    private int currentSpellIndex = 0;

    [SerializeField] private Spell tornadoSpell; // Spell untuk Tornado
    [SerializeField] private Spell aoeLightningSpell; // Spell untuk AOE Lightning

    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float currentMana;
    [SerializeField] private float manaRechargeRate = 2f;
    [SerializeField] private float timeBetweenCast = 0.25f;
    private float currentCastTimer;

    [SerializeField] private Transform castPoint; // Lokasi awal spell
    [SerializeField] private Camera playerCamera; // Kamera pemain untuk raycast
    [SerializeField] private float maxCastRange = 50f; // Jarak maksimum spell

    private bool castingMagic = false;

    private PlayerControl playerControl;

    private void Awake()
    {
        playerControl = new PlayerControl();
        currentMana = maxMana;
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
        // Ganti spell dengan input angka
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentSpellIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) currentSpellIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) currentSpellIndex = 2;

        bool isSpellCastHeldDown = playerControl.Controls.SpellCast.ReadValue<float>() > 0.1;
        Spell currentSpell = spellsToCast[currentSpellIndex]; // Spell yang aktif
        bool hasEnoughMana = currentMana - currentSpell.spellToCast.ManaCost >= 0f;

        if (!castingMagic && isSpellCastHeldDown && hasEnoughMana)
        {
            castingMagic = true;
            currentMana -= currentSpell.spellToCast.ManaCost;
            currentCastTimer = 0;
            CastSpell(currentSpell);
        }

        if (castingMagic)
        {
            currentCastTimer += Time.deltaTime;

            if (currentCastTimer > timeBetweenCast)
            {
                castingMagic = false;
            }
        }

        if (currentMana < maxMana && !castingMagic && !isSpellCastHeldDown)
        {
            currentMana += manaRechargeRate * Time.deltaTime;
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
        }

        // Tombol E untuk Tornado
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryCastSpecialSpell(tornadoSpell);
        }

        // Tombol Q untuk AOE Lightning
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TryCastSpecialSpell(aoeLightningSpell);
        }
    }

    void CastSpell(Spell spell)
    {
        // Lakukan raycast dari kamera
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)); // Arah tengah layar
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit, maxCastRange))
        {
            // Jika raycast mengenai sesuatu, arahkan ke titik tersebut
            targetPoint = hit.point;
        }
        else
        {
            // Jika tidak ada yang terkena, arahkan ke posisi maksimum jarak
            targetPoint = ray.GetPoint(maxCastRange);
        }

        // Hitung arah dari castPoint ke targetPoint
        Vector3 direction = (targetPoint - castPoint.position).normalized;

        // Buat spell dan arahkan ke target
        Spell newSpell = Instantiate(spell, castPoint.position, Quaternion.LookRotation(direction));
    }

    void TryCastSpecialSpell(Spell specialSpell)
    {
        // Cek apakah cukup mana untuk spell khusus
        if (currentMana >= specialSpell.spellToCast.ManaCost)
        {
            currentMana -= specialSpell.spellToCast.ManaCost;
            CastSpell(specialSpell);
        }
        else
        {
            Debug.Log("Not enough mana for special spell!");
        }
    }
}
