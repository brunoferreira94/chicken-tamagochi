using UnityEngine;
using System.Collections;

public class Actions : MonoBehaviour {
	float galinhaVelocity = 0.5f;
	float StatusFomeVelocity = 0.2f;
	float StatusHigieneVelocity = 0.05f;
	float StatusEnergiaVelocity = 0.05f;

	Vector3 pipocaPos;
	Vector3 galinhaPos;
	GameObject galinha;
	GameObject galinhaMarcador;
	Animator galinhaAnimator;
	AudioSource galinhaSom, camaSom, pipocaSom, banheiraSom;
	GameObject pipoca, cama, banheira;
	GameObject statusFome, statusHigiene, statusEnergia;
	bool isAlive = false;

	void Start () {

		pipoca = GameObject.FindGameObjectWithTag ("Food");
		cama = GameObject.Find ("Cama");
		banheira = GameObject.Find ("Banheira");
		galinha = GameObject.FindGameObjectWithTag ("Character");
		galinhaMarcador = GameObject.Find ("Personagem Tracked Object");
		galinhaAnimator = galinha.GetComponent<Animator>();
		galinhaSom = GameObject.Find ("Camera/Galinha").GetComponent<AudioSource>();
		camaSom = GameObject.Find ("Camera/Dormir").GetComponent<AudioSource>();
		pipocaSom = GameObject.Find ("Camera/Morder").GetComponent<AudioSource>();
		banheiraSom = GameObject.Find ("Camera/Banho").GetComponent<AudioSource>();

		statusFome = GameObject.Find ("Fome/Cylinder");
		statusHigiene = GameObject.Find ("Higiene/Cylinder");
		statusEnergia = GameObject.Find ("Energia/Cylinder");
	}
	
	// Update is called once per frame
	void Update () {
		if (statusFome.transform.localScale.y > 0 && statusHigiene.transform.localScale.y > 0 && statusEnergia.transform.localScale.y > 0) {
			statusFome.transform.localScale -= new Vector3 (0, StatusFomeVelocity * Time.deltaTime, 0);
			statusHigiene.transform.localScale -= new Vector3 (0, StatusHigieneVelocity * Time.deltaTime, 0);
			statusEnergia.transform.localScale -= new Vector3 (0, StatusEnergiaVelocity * Time.deltaTime, 0);
		} else {
			galinhaAnimator.SetBool ("Death",true);
			isAlive = false;
		}

		if (galinha.activeInHierarchy && (pipoca.activeInHierarchy || cama.activeInHierarchy || banheira.activeInHierarchy)) {
			galinhaAnimator.SetBool ("Andar",true);
			if (pipoca.activeInHierarchy) {
				galinha.transform.position -= (galinha.transform.position - pipoca.transform.position) * galinhaVelocity * Time.deltaTime;
				galinha.transform.LookAt (pipoca.transform);
				if (Vector3.Distance (pipoca.transform.position, galinha.transform.position) < 0.03f) {
					statusFome.transform.localScale = new Vector3 (2,3,2);
					pipoca.SetActive (false);
					pipocaSom.Play ();
					galinhaSom.Play ();
				}
			} else if (banheira.activeInHierarchy) {
				galinha.transform.position -= (galinha.transform.position - banheira.transform.position) * galinhaVelocity * Time.deltaTime;
				galinha.transform.LookAt (banheira.transform);
				if (Vector3.Distance (banheira.transform.position, galinha.transform.position) < 0.03f) {
					statusHigiene.transform.localScale = new Vector3 (2, 3, 2);
					banheira.SetActive (false);
					banheiraSom.Play ();
					galinhaSom.Play ();
					}
				}
			else if (cama.activeInHierarchy) {
				galinha.transform.position -= (galinha.transform.position - cama.transform.position) * galinhaVelocity * Time.deltaTime;
				galinha.transform.LookAt (cama.transform);
				if (Vector3.Distance (cama.transform.position, galinha.transform.position) < 0.03f) {
					statusEnergia.transform.localScale = new Vector3 (2, 3, 2);
					cama.SetActive (false);
					camaSom.Play ();
					galinhaSom.Play ();
						}
					}
		} else {
			galinha.transform.position -= (galinha.transform.position - Vector3.zero) * galinhaVelocity * Time.deltaTime;
			galinha.transform.LookAt (galinhaMarcador.transform);
		}
	}
}
