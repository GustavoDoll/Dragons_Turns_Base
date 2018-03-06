using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	// pega o GameObject do Player e do Inimigo;
	public GameObject player, enemy;

	// Flags para verificação de turno
	public static bool isPlayerTurn, isEnemyPhase, haveAtacked1 , morreu;

	// textos da UI referentes ao feedback
	public Text feedback1, feedback2, feedback3;

	void Start () {

		// Seta as flags
		isPlayerTurn = true;
		haveAtacked1 = false;
		isEnemyPhase = false;

	}
		
	void Update () {
	
		// atualiza o feedback
		feedback1.text = "Player's HP: " + Player.playerLife.ToString();
		feedback2.text = "Enemy's HP: " + Enemy.enemyLife.ToString();

		// feedback de quem é o turno
		if (isPlayerTurn && !isEnemyPhase) {
			feedback3.text = "Player's Turn";
		} else {
			feedback3.text = "Enemy's Turn";
		}


	}

	// metodo do primeiro ataque
	public void ATK_1(){
		StartCoroutine (CO_ATK_1 ());
	}

	// mmetodo do segundo ataque
	public void ATK_2(){
		StartCoroutine (CO_ATK_2 ());
	}

	// metodo do comando de defesa
	public void GUARD(){
		StartCoroutine (CO_GUARD ());
	}

	// metodo do comando de passar o turno
	public void PASS(){
		StartCoroutine (CO_PASS ());
	}

	// Coroutine do primeiro ataque
	IEnumerator CO_ATK_1(){
		
		// verifica se o player pode atacar
		if (isPlayerTurn && !isEnemyPhase) {

			// seta um valor de ataque randomico somado aos ataque base do player
			int atk1 = Player.playerATK * Random.Range (2, 4);

			// calcula o dano no inimigo
			Enemy.enemyLife = Enemy.enemyLife - ((Enemy.enemyARMOR + Enemy.enemyDEF) - atk1);

			// Executa a animação de ataque
			Animation animAtk1 = player.GetComponent<Animation>();
			animAtk1.Play("atk1");

			// espera para dar sequencia
			yield return new WaitForSeconds(1.5f);

			// seta as flags para a troca de turno
			isPlayerTurn = false;
			isEnemyPhase = true;
			haveAtacked1 = true;

			// chama o turno do inimigo
			StartCoroutine (CoEnemyPhase ());
		}
	}

	// Coroutine do segundo ataque
	IEnumerator CO_ATK_2(){

		// verifica se o segundo ataque esta disponivel
		if (haveAtacked1 && isPlayerTurn && !isEnemyPhase) {
			
			// seta um valor de ataque randomico somado aos ataque base do player
			int atk2 = Player.playerATK * Random.Range (5, 8);

			// calcula o dano no inimigo
			Enemy.enemyLife = Enemy.enemyLife - ((Enemy.enemyARMOR + Enemy.enemyDEF) - atk2);

			Animation animAtk2 = player.GetComponent<Animation>();
			animAtk2.Play("atk2");

			// espera para dar sequencia
			yield return new WaitForSeconds(2.5f);

			// seta as flags para a troca de turno
			isPlayerTurn = false;
			isEnemyPhase = true;

			// chama turno do inimigo
			StartCoroutine (CoEnemyPhase ());
		} else {

			// espera para dar sequencia
			yield return new WaitForSeconds(1.5f);

			// seta as flags para a troca de turno
			isPlayerTurn = false;
			isEnemyPhase = true;

			// chama turno do inimigo
			StartCoroutine (CoEnemyPhase ());
		}
	}

	// Coroutine da defesa
	IEnumerator CO_GUARD(){

		// calcula um acrescimo para a defesa
		Player.guardDEF = Player.playerDEF + Player.playerARMOR * 3;

		Animation animDef = player.GetComponent<Animation>();
		animDef.Play("def");

		// espera para dar sequencia
		yield return new WaitForSeconds(2.5f);

		// seta a flag para mudança de turno
		isPlayerTurn = false;
		isEnemyPhase = true;
		StartCoroutine (CoEnemyPhase ());

	}

	// Coroutine da passada de turno
	IEnumerator CO_PASS(){

		// espera para dar sequencia
		yield return new WaitForSeconds(2.5f);

		// flag para a mudança de turno e chama o turno do inimigo
		isPlayerTurn = false;
		isEnemyPhase = true;
		StartCoroutine (CoEnemyPhase ());
	}

	// Coroutine do turno do inimigo
	IEnumerator CoEnemyPhase(){

		// verifica se o turno do inimigo está ativo
		if (!isPlayerTurn && isEnemyPhase) {

			// randomiza uma ação para o inimigo
			int atkRandomizer = Random.Range (0, 2);

			// espera para dar sequencia
			yield return new WaitForSeconds(2.5f);

			// decide o que cada ação escolhida faz
			switch (atkRandomizer) {
			case 0:

				// calcula o dano a ser aplicado no player
				int damage_1 = (Player.playerDEF + Player.playerARMOR + Player.guardDEF) - (Enemy.enemyATK * Random.Range (2, 4));

				// verifica se o dano 1 esta negativo
				if(damage_1 <= 0){
					damage_1 *= -1;
				}

				// seta um dano na vida do player
				Player.playerLife = Player.playerLife - damage_1;

				// reseta o valor do acrescimo da defesa
				Player.guardDEF = 0;

				// flag para mudança de turno
				isPlayerTurn = true;
				isEnemyPhase = false;

				break;
		
			case 1:

				// calcula o dano do segundo ataque a ser aplicado no player
				int damage_2 = (Player.playerDEF + Player.playerARMOR + Player.guardDEF) - (Enemy.enemyATK * Random.Range (4, 6));
				int damage_3 = (Player.playerDEF + Player.playerARMOR + Player.guardDEF) - (Enemy.enemyATK * Random.Range (6, 8));

				// verifica se o dano 2 esta negativo
				if (damage_2 <= 0) {
					damage_2 *= -1;
				}

				// verifica se o dano 3 está negativo
				if (damage_3 <= 0) {
					damage_3 *= -1;
				}

				// seta um ataque duplo para o inimigo
				Player.playerLife = Player.playerLife - damage_2;
				Player.playerLife = Player.playerLife - damage_3;

				// reseta o valor do acrescimo da defesa
				Player.guardDEF = 0;

				// flag para a mudança de turno
				isPlayerTurn = true;
				isEnemyPhase = false;

				break;

			case 2:

				// faz o inimigo recuperar vida
				Enemy.enemyLife = Enemy.enemyLife + Random.Range (100, 600);

				// flag para a mudança de turno
				isPlayerTurn = true;
				isEnemyPhase = false;

				break;
			}
		}
	}
}
