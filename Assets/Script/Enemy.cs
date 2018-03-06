using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
	public GameObject hud;
	public Canvas hudAtks;
	// variaveis para os atributos do inimigo
	public static int enemyLife, enemyDEF, enemyARMOR, enemyATK, enemyMAXLIFE;

	// referencia ao slider que é a barra de vida
	public Slider enemyHealthBar;

	void Start () {

		// atribui uma vida randomica ao inimigo
		if (enemyLife == 0) {
			enemyLife = Random.Range (100, 200);
			enemyMAXLIFE = enemyLife;
		}

		// atribui valor ás propriedades do player
		enemyARMOR = 12;
		enemyATK = 4;
		enemyDEF = 9;

		// atribui valor a barra de vida
		enemyHealthBar.maxValue = enemyLife;
		
	}

	void Update () {
		if (enemyLife == 1) {
			enemyLife = Random.Range (100, 200);
			enemyMAXLIFE = enemyLife;
		}



		// verifica para que o player não exeda o limite de vida
		if (enemyLife > enemyMAXLIFE) {
			enemyLife = enemyMAXLIFE;
		}

		// atualiza a barra de vida
		enemyHealthBar.value = enemyLife;

		// verificação de morte
		if (enemyLife <= 0) {
			hudAtks.enabled = false;
			hud.SetActive (true);
			enemyLife = 0;
			Time.timeScale = 0;
			if (Input.GetKeyDown(KeyCode.R) ) {
				hudAtks.enabled = true;
				hud.SetActive (false);
				Time.timeScale = 1;
				enemyLife = 1;
				MenuManager.isEnemyPhase = false;
				MenuManager.isPlayerTurn = true;
				MenuManager.haveAtacked1 = true;

			}
		}

	}
}
