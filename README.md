# Proiect MDS  
## Joc 2D RPG  

🔗 [Trello Board](https://trello.com/b/PFv4H5ZT/proiect-mds)

[Gameplay Demo (YouTube)](https://www.youtube.com/watch?v=XlhrTKNnZNw)

## User Stories
<details>
Game Concept User Stories
Puzzle Gameplay

As a player, I want the game to include challenging puzzles, so that I can enjoy solving them and feel a sense of accomplishment.

Engaging Storyline

As a player, I want an interesting and immersive story, so that I feel motivated to progress through the game.

In-Game Shop

As a player, I want a shop where I can buy useful items, so that I can customize my gameplay and improve my character.

Challenging Enemies

As a player, I want enemies that provide a fair but tough challenge, so that combat feels exciting and rewarding.

Bug-Free Experience

As a player, I want a smooth and bug-free game, so that I can enjoy it without frustrating interruptions.

Great Background Music

As a player, I want high-quality background music, so that the atmosphere of the game feels more engaging and immersive.

Platforming Levels

As a player, I want well-designed platforming levels, so that I can enjoy precise jumps and exploration.

Interesting Backgrounds

As a player, I want visually appealing and varied backgrounds, so that the game world feels alive and dynamic.

Lag-Free Performance

As a player, I want the game to run smoothly without lag, so that my experience is seamless and enjoyable.

Player Inventory

As a player, I want an inventory system to store and manage my items, so that I can easily access and use them when needed.
</details>

## Diagrama UML
![UML](https://github.com/user-attachments/assets/2e19e2cd-a9e9-47b8-9bbf-18ce4b65d1ab)

## Bug report
  Exemple: (https://github.com/FuriousMonk22/Proiect-MDS/issues/5, https://github.com/FuriousMonk22/Proiect-MDS/issues/4)

## Comentarii Cod
  Codul din comentarii este in principal adaugat de Copilot/GPT.

## Design Patterns
  Jocul foloseste Design Patterns de OOP pentru implementarea inamicilor (Ex: functia TakeDamage) si atacurile lor (interactiune cu player). [HomingFlame](https://github.com/FuriousMonk22/Proiect-MDS/blob/main/Assets/Scripts/FlameHoming.cs) [Flame](https://github.com/FuriousMonk22/Proiect-MDS/blob/main/Assets/Scripts/Flame.cs)

## Prompt engineering:
<details>
  In timpul dezvoltarii jocului 2D in Unity, am utilizat diverse tool-uri de AI (precum ChatGPT si GitHub Copilot) pentru a-mi optimiza fluxul de lucru, a rezolva erori si a genera cod mai eficient. Aceste instrumente m-au ajutat sa automatizez sarcini repetitive, sa gasesc solutii rapide la probleme si sa invat tehnici noi.

ChatGPT:

Utilizare:

Generarea de cod rapid - Am folosit ChatGPT pentru a corecta scripturile din C# pentru care primeam eroare:
De exemplu, i-am trimis script-ul de la PuzzleManagerUI in care avem urmatoarea problema: dupa ce rezolvam puzzle-ul de la nivelul 2, jucatorul ramanea langa semn, iar daca reintra in trigger, UI-ul puzzle-ului se deschidea din nou( desi puzzle-ul fusese deja completat)
a clarificat cauza deoarece nu aveam o verificare pentru starea puzzle-ului(puzzleCompleted).
ChatGPT a sugerat sa adaug o conditie care verifica daca puzzle-ul a fost completat inainte de a afisa UI-ul si mi-a recomandat pentru ajutorul meu sa adaug niste Debug-uri ca sa vedem daca functioneaza bine toate comenzile.
In acest fel, metoda aceasta a functionat, iar, dupa terminarea puzzle-ului jocul nu ma punea sa fac iar puzzle-ul la nesfarsit.
Prompt: “De ce UI-ul puzzle-ului meu se redeschide cand ma intorc in trigger, chiar daca l-am rezolvat deja?”

Cautare erori - Am folosit o captura de ecran pe care am facut-o la ecranul meu ca sa imi spuna ce este gresit:
De exemplu, in joc am implementat un sistem de afisare a damage-ului cand inamicul primeste hit de la jucatorul nostru, insa jocul arunca o eroare NullReferenceException, iar damage-ul nu aparea.
Cu ajutorul lui, am realizat ca in campul text mash din script nu era nimic, Unity neputand sa gaseasca referinta la obiectul care contine textul.
Prefab-ul avea structura corecta(un GameObject copil cu componenta TextMashProUGUI), dar legatura nu era facuta manual.
Tool-ul m-a ajutat spunandu-mi sa selectez FloatingDamageText in Hierarchy. Din copilul sau(componenta de text), o sa trag acel GameObject in campul Text Mash din scriptul atasat parintelui. Apoi salvez prefab-ul.
AI-ul m-a ajutat sa inteleg eroarea NullReferenceException in legatura cu un camp nesetat si pentru a primi instructiuni vizuale despre cum sa conectez corect componentele in Unity
Dupa ce am urmat pasii, Sistemul de damage a functionat perfect.
Captura de ecran:



Idei creative - Am folosit ChatGPT pentru a genera idei pentru jocul nostru.
Un exemplu ar fi cand l-am rugat sa imi genereze niste idei creative pentru capacne in joc si pentru a intelege cum sa le implementez tehnic in Unity.
Acesta a sugerat o lista variata de capcane potrivite pentru un joc 2D(platformer/padure), cum ar fi capcane clasice(Spikes,gropi, sageti), capcane dinamic(busteni care cad, pietre care se rostogolesc, mine explozive) sau chiar si capcane cu timing(tepi retractibili sau sageti cu trigger)
Prompt:”Ce capcane pot adauga intr-un joc 2D cu tema de padure/platformer?”

GitHub Copilot:

In timpul dezvoltarii jocului 2D in Unity, am folosit GitHub Copilor ca asistent de codare pentru a:
- a adauga comentarii si explicatii pentru codul scris
- completa automat portiuni repetitive

Exemple concrete:
Adnotari si comentarii
Am scris o functie pentru a cumpara un item din Shop, iar Copilot a adaugat comentarii explicative:
public void BuyItem() // metoda pentru a cumpara item-ul 
{
 if (player.TryBuy(price)) 
{ 
int stock = PlayerPrefs.GetInt(PrefKey) + 1; 
PlayerPrefs.SetInt(PrefKey, stock); 
Debug.Log($"Ai cumparat: {itemType} (acum ai {stock})"); 
} 
}
Completarea codului
Am inceput sa scriu o functie pentru ShopTrigger si de cand am scris numele clasei OnTriggerEnter2D, GitHub Copilot mi-a recomandat cum as putea sa completez clasa.
private void OnTriggerEnter2D(Collider2D other) // Check if the player enters the trigger area 
{ 
if (other.CompareTag("Player")) 
{ 
shopUI.SetActive(true); 
} 
}
</details>
