using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellCast : MonoBehaviour
{
    [SerializeField] private GameObject basicSpellProjectile;

    Camera playerCam;

    bool isCasting;
    float maxSpellRange;

    void Update()
    {
        isCasting = Input.GetKeyDown(KeyCode.Mouse0);

        if (isCasting)
        {
            CastSpell();
        }
    }

    void CastSpell()
    {
        RaycastHit spellDirection;

        Quaternion spellRotation = Quaternion.LookRotation(transform.forward);

        Vector3 spellTarget;
        if (Physics.Raycast(transform.position, spellRotation * Vector3.forward, out spellDirection))
        {
            spellTarget = spellDirection.point;
        }

        GameObject spellProjectile = Instantiate(basicSpellProjectile, transform.position, spellRotation);
    }
}
