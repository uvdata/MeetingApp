# Meeting App

* Din opgave er at implementere en applikation hvori man kan oprette, redigere, slette og læse mødelokaler. Løsningen skal både inkludere en detalje læsning ud fra lokalets ID samt en listevisning der inkluderer navn og lokation.
* Et mødelokale har felterne Id, Navn, Lokation og Pladsantal.
* Du bestemmer selv om du vil lave en web applikation, nogle REST services, en konsol applikation eller en kombination, da det ikke er brugergrænsefladen der er det vigtige.
* Du skal anvende følgende teknologier:
	* En relationel database som f.eks PostgreSQL, MySQL eller Maria DB. Dog er det et krav at databasen har et Docker image (se det sidste punkt).
	* C# / .NET Core
	* Docker (Der skal laves en Docker fil som kan bygge og køre projektet) 
* Du skal lave en Docker Compose fil som kan starte projektet. Den skal bygge Docker imaget for applikationen og linke det til en instans af databasen hvorefter applikationen skal startes op og være klar til brug. En bonus vil være også at inkluderer et eller andet UI værktøj til at browse databasens indhold i docker compose filen. I så fald vil kommandoen docker-compose up starte alle afhængighederne for din applikation, så du kan demonstrere den.
* Vær kreativ - gode idéer er velkomne.

## Aflevering

* Aflever gerne en delvis løsning, hvis du ikke kan lave den helt færdig af den ene eller anden grund.
* Du kan vælge at forke dette repo og sende løsningen ind som et Pull Request eller alternativt at zippe løsningen og sende den som mail (adresse udleveres seperat)

