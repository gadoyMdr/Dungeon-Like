using Scripts.Refactor;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanBeThrown : MonoBehaviour
{

    private float range;

    private readonly float explodeRange = 2;

    private readonly float speed = 200;

    Rigidbody2D _rigidbody2D;

    private Vector3 startPos;
    private bool isThrown = false;

    private void Update()
    {
        

        if (isThrown)
        {
            CheckCollisions();

            if (CheckDistance())
                Explode();
        }
            
    }

    void CheckCollisions()
    {
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        if (Utils.FindBoardEmplacement(pos).boardElements.Any(x => x is UnMovableBoardElement))
            Explode();
    }

    public void Throw(Vector2 direction, float distance = 3)
    {
        range = distance;

        isThrown = true;
        startPos = transform.position;

        _rigidbody2D = AddAndConfigureRigidebody();
        _rigidbody2D.AddForce(direction * speed);

        transform.SetParent(null);
    }

    void Explode()
    {


        foreach(Health health in Physics2D.OverlapCircleAll(transform.position, explodeRange).Select(x => x.GetComponent<Health>()).OfType<Health>())
        {
            Debug.Log(health.gameObject.name);
            GetComponent<Potion>().potionEffect.ApplyEffect(health);
        }

        Instantiate(GetComponent<Potion>().itemData.particle, transform.position, Quaternion.identity, null);

        Destroy(gameObject);
    }

    bool CheckDistance()
    {
        return Vector3.Distance(startPos, transform.position) > range;
    }

    Rigidbody2D AddAndConfigureRigidebody()
    {
        Rigidbody2D _rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
        return _rigidbody2D;
        
    }
}
