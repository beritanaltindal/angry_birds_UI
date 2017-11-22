using UnityEngine;
using System.Collections;

public class Resetter : MonoBehaviour {

	public Rigidbody2D projectile;			//	The rigidbody of the projectile
	public float resetSpeed = 0.025f;		//	The angular velocity threshold of the projectile, below which our game will reset
	
	private float resetSpeedSqr;			//	The square value of Reset Speed, for efficient calculation
	private SpringJoint2D spring;			//	The SpringJoint2D component which is destroyed when the projectile is launched

    private GameControl control;

	void Start ()
	{
		//	Calculate the Reset Speed Squared from the Reset Speed
		resetSpeedSqr = resetSpeed * resetSpeed;
        control = FindObjectOfType<GameControl>();
	}
	
	void Update () 
    {
        if (projectile)
        {
            if(!spring)
                spring = projectile.GetComponent<SpringJoint2D>();

            CheckForReset();
        }
        else
        {
            TryFindProjectile();
        }
	}

    void CheckForReset()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            control.CurrLevel.Currentscore += 10000;
            Reset();
        }

        //	If the spring had been destroyed (indicating we have launched the projectile) and our projectile's velocity is below the threshold...
        if (spring == null && projectile.velocity.sqrMagnitude < resetSpeedSqr)
        {
            Reset();
        }
    }
	void OnTriggerExit2D (Collider2D other)
    {
		if (projectile && other.GetComponent<Rigidbody2D>() == projectile)
        {
			Reset ();
		}
	}
    void TryFindProjectile()
    {
        if (GameObject.FindGameObjectWithTag("Projectile"))
        {
            projectile = GameObject.FindGameObjectWithTag("Projectile").GetComponent<Rigidbody2D>();
            spring = projectile.GetComponent<SpringJoint2D>();
        }
    }
	public void Reset ()
    {
        Destroy(projectile.gameObject);
	}
}
