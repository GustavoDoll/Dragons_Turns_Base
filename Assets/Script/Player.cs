using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
	public GameObject hud;
	public Canvas hudAtks;

	// variaveis para os atributos do player
	public static int playerLife, playerDEF, playerATK, playerARMOR, guardDEF, playerMAXLIFE;

	// referencia ao slider que é a barra de vida
	public Slider playerHealthBar;

	void Start () {

		// atribui uma vida randomica ao player
		if (playerLife == 0) {
			playerLife = Random.Range (100, 200);
			playerMAXLIFE = playerLife;
		}

		// atribui valor ás propriedades do player
		playerARMOR = 3;
		playerATK = 2;
		playerDEF = 5;
		guardDEF = 0;

		// atribui valor a barra de vida
		playerHealthBar.maxValue = playerMAXLIFE;

	}

	void Update () {
		
		if (playerLife == 1) {
			playerLife = Random.Range (100, 200);
			playerMAXLIFE = playerLife;
		}

		// verifica para que o player não exeda o limite de vida
		if (playerLife > playerMAXLIFE) {
			playerLife = playerMAXLIFE;
		}

		// atualiza a barra de vida
		playerHealthBar.value = playerLife;

		if (Player.playerLife <= 0 ) {
			hudAtks.enabled = false;
			hud.SetActive (true);
			playerLife = 0;
			Time.timeScale = 0;
		

			if (Input.GetKeyDown(KeyCode.R) ) {
				hudAtks.enabled = true;
				hud.SetActive (false);
				Time.timeScale = 1;
				playerLife = 1;
				MenuManager.isEnemyPhase = false;
				MenuManager.isPlayerTurn = true;
				MenuManager.haveAtacked1 = true;
			
			}
		}


		
	}


}
