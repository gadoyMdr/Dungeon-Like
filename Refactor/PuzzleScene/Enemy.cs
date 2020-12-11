using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Scripts.Refactor
{
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        RandomGOInSet attackPotions;

        [SerializeField]
        private float healCoolDown = 5;

        [SerializeField]
        private float rateOfFire = 2;

        [SerializeField]
        private float rangeDetection = 3;

        [SerializeField]
        private int maxHealings = 2;

        private Health health;

        private readonly float randomRange = 0.8f;
        private readonly float healthThreshold = 50;
        private readonly float healing = 15;

        private int healCount = 0;
        private bool canFire = true;
        private bool canHeal = true;
        private void Awake()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (GetPlayer(out Transform player) && canFire)
                Shoot(player);

            if (health.currentHealth < healthThreshold && canHeal && healCount <= maxHealings)
                Heal();
        }

        bool GetPlayer(out Transform player)
        {
            player = Physics2D.OverlapCircleAll(transform.position, rangeDetection).Where(x => x.name.Contains("Pusher"))?.FirstOrDefault()?.transform;

            return player != null;
        }

        void Shoot(Transform player)
        {
            Instantiate(attackPotions.GetRandomObject(), transform.position, Quaternion.identity, transform ).GetComponent<CanBeThrown>().Throw((player.transform.position - transform.position).normalized);
            
            StartCoroutine(FireCoolDown());
        }

        void Heal()
        {
            healCount++;
            health.currentHealth += healing;
            StartCoroutine(HealCoolDown());
        }

        IEnumerator HealCoolDown()
        {
            canHeal = false;
            yield return new WaitForSeconds(healCoolDown * Random.Range(MagicNumbers.one - randomRange, MagicNumbers.one + randomRange));
            canHeal = true;
        }

        IEnumerator FireCoolDown()
        {
            canFire = false;
            yield return new WaitForSeconds(rateOfFire);
            canFire = true;
        }
    }
}


