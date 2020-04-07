Dagbok

2020-03-30
Tagit oss an projektbeskrivningen och startat med en övergripande flowchart för att få koll på den övergripande bilden(highlevel) över hur olika moment och händelser hänger ihop i programmet

2020-03-31
[01:26] Lite lättare projektledning av planeringen hur vi går vidare över discord till projektmedlemmarna medan de sov. inför morgondagen, så de ser det när de vaknar. 

[Dagtid] Skapat userstories baserade på den övergripande flowcharten och laddat upp till Git. Gå igenom dessa med Pontus och Samir när de kommer online. Skrivit en början till projektprocessen. Länk till Google Drive där flowchart finns, ligger med i projektprocessen.

Har tillsammans med gruppen Samir och Pontus skapat lowlevel flowchart för databashantering samt Initieringsfas inför spelstart. Även uppdaterat med några nya userstories i userstory spelomgång

2020-04-01

[natt]Har skrivit klart samtliga userstories. Lagt upp egen förkortad version av spelregler.

[dag] Gemensam genomgång av att göra klart CRC-Cards och databasdesign. Delat upp i ansvarsområden och påbörjat kodning. Gjort meny för spelet. Alla metoder inte klara då databas behöver vara klart först.

2020-04-02

Gjort Fluent API för GameSession samt påbörjat GameBoard, GamePlayer och GamePiece klasser.

2020-04-03 to 2020-04-04

Fortsatt justering av GameSession samt GameBoard, GamePlayer & GamePice hur de skall hänga ihop med varandra och hämta data. Även påbörjat arbetet av koordinatsystemet för GamePiece samt GameBoard.

Skapat diceklass,  lagt till metoder i GameBoard. Gjort Unit Test på Gameboard

2020-04-05

Jobbat med en turodningsmetod som bestämmer vilken ordning spelarna skall spela (medsols), beroende på vilken färg som börjar slå. Gjort unit-test, samt påbörjat gameplay

2020-04-06

Refakturerat gameplay. Gjort metoder kring regler då spelare slår 1-5. Kvarstår 6

2020-04-07

Klar med 6ans nummer, och kollissioner. Tror det kommer bli ett par buggar här och där, men det blir lättare att upptäcka dem när drawing klass och meny klass är på plats som kommer att hjälpa till UI. Menyklassen är fixad, så nu går det att anropa olika menyer som visas  horisontellt eller vertikalt på olika val spelaren får göra i spelet.

Påbörjat DrawUI