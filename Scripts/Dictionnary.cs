using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Dictionnary : MonoBehaviour {

	Dictionary<string, string> sentences = new Dictionary<string, string>();
	string selectedLanguage = "FR";

	void Start(){
		sentences.Add("FR_HEALTHBAR_LIFE", "Points de vie");
		sentences.Add("FR_HEALTHBAR_ENERGY", "Energie");
        sentences.Add("FR_HEALTHBAR_ARMOR", "Armure");
        sentences.Add("FR_HEALTHBAR_NEXTROUND", "Prochain tour");
        sentences.Add("FR_ACTIONBAR_ATTACK", "A");
        sentences.Add("FR_ACTIONBAR_MAGICMISSILE", "P");
        sentences.Add("FR_ACTIONBAR_FIREBALL", "B");
        sentences.Add("FR_ACTIONBAR_LIGTHNING", "E");
        sentences.Add("FR_ACTIONBAR_ARMOR", "Ar");
        sentences.Add("FR_ACTIONBAR_TELEKINESIE", "Tk");
        sentences.Add("FR_ACTIONBAR_TELEPORTATION", "Te");
		sentences.Add("FR_ACTIONBAR_RELOAD", "R");
		sentences.Add("FR_ACTIONBAR_MAINMENU", "M");

        sentences.Add("EN_HEALTHBAR_LIFE", "Hit Point");
		sentences.Add("EN_HEALTHBAR_ENERGY", "Energy");
        sentences.Add("EN_HEALTHBAR_ARMOR", "Armor");
        sentences.Add("EN_HEALTHBAR_NEXTROUND", "Next Round");
        sentences.Add("EN_ACTIONBAR_ATTACK", "A");
        sentences.Add("EN_ACTIONBAR_MAGICMISSILE", "M");
        sentences.Add("EN_ACTIONBAR_FIREBALL", "F");
        sentences.Add("EN_ACTIONBAR_LIGTHNING", "L");
        sentences.Add("EN_ACTIONBAR_ARMOR", "Ar");
        sentences.Add("EN_ACTIONBAR_TELEKINESIE", "Tk");
        sentences.Add("EN_ACTIONBAR_TELEPORTATION", "Te");
		sentences.Add("EN_ACTIONBAR_RELOAD", "R");
		sentences.Add("EN_ACTIONBAR_MAINMENU", "M");
    }

	public string getSentence(string key) {
		if (sentences.ContainsKey (selectedLanguage + "_" + key)) {
			return sentences [selectedLanguage + "_" + key];
		} else {
			return "";
		}
	}
}
