using UnityEngine;
using System.Collections;

public class TargetDamage : MonoBehaviour {

	public int hitPoints = 2;					
	public Sprite damagedSprite;				
	public float damageImpactSpeed;			
	
	
	private int currentHitPoints;				
	private float damageImpactSpeedSqr;			
	private SpriteRenderer spriteRenderer;

    private GameControl control;	
	
	void Start () {
		spriteRenderer = GetComponent <SpriteRenderer> ();
        control = FindObjectOfType<GameControl>(); ;
        currentHitPoints = hitPoints;

		damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.collider.tag != "Damager")
			return;
		
		if (collision.relativeVelocity.sqrMagnitude < damageImpactSpeedSqr)
			return;

        if (spriteRenderer)
        {
            spriteRenderer.sprite = damagedSprite;
            currentHitPoints--;

            if (currentHitPoints <= 0)
                Kill();
        }

	}
	
	void Kill () {
		spriteRenderer.enabled = false;
        control.CurrLevel.Currentscore += 8000;
		GetComponent<Collider2D>().enabled = false;
		GetComponent<Rigidbody2D>().isKinematic = true;
        Destroy(this.gameObject,.5f);
		
		GetComponent<ParticleSystem>().Play();
	}
}
