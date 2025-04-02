using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float moveSpeed = 5f; // Velocidad de movimiento
	private Rigidbody2D rb;
	private Vector2 movement;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		// Capturar inputs de movimiento
		float horizontal = Input.GetAxisRaw("Horizontal"); // Flechas izquierda/derecha o A/D
		float vertical = Input.GetAxisRaw("Vertical");     // Flechas arriba/abajo o W/S

		// Evitar movimiento diagonal: priorizar horizontal o vertical
		if (horizontal != 0)
		{
			vertical = 0; // Si hay movimiento horizontal, cancelar el vertical
		}

		movement = new Vector2(horizontal, vertical); // Actualizar el vector de movimiento
	}

	void FixedUpdate()
	{
		// Movimiento del personaje
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
	}
}