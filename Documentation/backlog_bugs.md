# Backlog bugs

Här skrivs buggar och ev. saknad funktionalitet ner. När de är klara markeras de med "OK" innan början av texten. För att skapa en backlog av buggar att åtgärda

======================================================================

Huvudmeny- Välja färg, upptagen färg skall försvinna

GameBoard - int DecidePlayerStart(int pAmount): saknas kod för att kontrollera att flera spelare slår samma högsta siffra på tärningen och måste slå om tills någon vinner.

GameBoard - gameloop, efter en spelare flyttat pjäs, och den  loopar igenom igen, verkar det som att samma spelare får slå igen.

GameBoard - när spelare Blå, grön gul nått en bit på sin egen pjäs spelplan, går den gemensamma gameboarden out of index pga att pjäser även förflyttas där. Måste, när pjäsen når sista index på commongameboard flytta den till index 0 (förutom röd, som börjar på index 0).

GameBoard - SetPlayOrder, flyttar inte alla positioner enl. färgordning beroende vilken färg som börjar i DecidePlayerStart

GameBoard - DecidePlayerStart - Om två eller fler spelare slår samma slag, väljs någon bara som får börja. Borde vara dom slår om, eller att det randomiseras.

