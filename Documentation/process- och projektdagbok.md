# Process

## Planerandefasen

Det första vi började med vid projektstart var att skapa en google drive katalog (lärdom från föregående projekt), innehållandes sammanfattning av regler för spelet, flowchart ev. kladddoc för CRC-Cards. 

Följande länk går till Google drive: https://drive.google.com/open?id=1R0MgfCOqnN7V_ZcALHXTZ099sGgxA1Uu

Vi började att skissa upp en flowchart; ett programflöde. Som första version gjorde vi endast utefter ett väldigt övergripande perspektiv; Vi gick mer in på händelser och flödet genom hela programmet från början till slut, snarare än detaljerna i händelserna.

När vi sedan itererar detta kommer vi gå ner mer på detaljnivå.

Med flowcharten som mall, började vi göra upp userstories indelade i Epics, Userstories & Substories som ger oss mer detaljerad information kring funktionalitet. Resultaten av dessa har vi sedan vänt tillbaka till flowcharten, för att mappas mot ner detaljerade iterationer i flowcharten, där vi delar upp den i mindre delar, med funktionalitet, eventuella variabler, metoder, klasser mm.

Målet är i slutändan att de skall resultera i CRC-cards innehållandes tillräcklig information för att kunna börja koda. Sedan allt eftersom projektet fortgår, kan dessa komma att behöva uppdateras, då exempelvis metoder läggs till, namn ändras, fler user-stories läggs till mm.

Vi kände att göra på det här viset och denna ordning, gav ett naturligt flöde i processen, där alla delar (flowchart, userstories samt CRC-cards) kunde komplettera varandra och ge värde till projektet. Att arbeta (som tidigare övningar) med mindmap +  CRC-cards, inte längre för oss då det inte bidrog med tillräckligt med värde. Varken mellan varandra eller till projektet.  Det naturliga flödet mellan de olika verktygen såväl som programmet saknades också.

**<u>I korthet processen:</u>**
*Flowchart (Highlevel) -> Userstories -> Flowchart (Lowlevel) -> CRC-Cards -> Koda -> Iterera dokumentation -> koda os*v.

## Genomförandefasen

** Nedan löper kontinuerliga uppdateringar för projektets status, samlat över en eller flera dagar. Datum indikerar då senaste uppdatering skett för att på så vis får spårbarhet i projektet.

**[Update 1 - 2020-04-01]**

Flowcaharts Lolevel är klara såväl som CRC-cards (första version) för samtliga delar såväl som databasdesign. Databasdesignen gjordes i MSSMS och utifrån den blir det lättare att bygga databasen mha Code First.

Vi har initialt delat upp arbetet i olika ansvarsområden och påbörjat kodning. Micael generellt, gameplay och startmeny, Pontus Unit-test och Samir databashantering.

**[Update 2 - 2020-04-06]**

Mycket har hänt sedan senaste uppdateringen. Meny till större delen gjord, väntar på databashanteringen som skall bli klar innan resterande delar, såsom Highscore och Loadgame kan bli klara. Grovt gameplay (programmeringsmässigt) från början till slut är klart. En hel del debugging samt UnitTester av metoder och behövs, eftersom det fortfarande saknas något slags grafiskt gränssnitt (svårt att bara skriva ut olika positioner på skärmen och hålla reda på dem till fullo). 

Det har även visat sig vara svårt att hitta på Unittester utifrån planeringen, då metoder på exakt funktionalitet att testa inte funnits tillgänglig. Nu finns dock de flesta metoderna, redo att kunna testas både med Unit-test, såväl som med Functional och Integrationtest även om det behöver refraktureras delar av koden.

En hjälpklass (CreateInteractable) har skapats som abstraherar och simulerar optionmenyer och ensamma "knappar" för att få någon form av interaktivitet av spelaren utan att denne skall behöva skriva kommandon genom Console.Readline(). Ytterligare en klass: DrawUI kommer att skapas för att kunna få grafik till spelet. När denna också är implementerad kommer det lättare lättare att kunna följa spelflödet och hålla reda på vad som händer i spelet och att det fungerar korrekt.

För övrigt har vi ungefär så gott som hållit och arbetat utefter våra flowcharts, CRC-cards samt Userstories även om viss funktionalitet och metoder kanske flyttats till andra klasser.

Det ser dock ut att bli tight med presentationen och få klart spelet samt testerna i en tillfredställande kvalitet som följer projektbeskrivningen, så vi kommer troligtvis få gå in i varandras ansvarsområden och stötta upp. 

**[Update 3 - 2020-04-10]**

Databashanteringen är mer eller mindre klar. Måste bara implementera det i spelet, men avvaktar med detta tills gameplay är klart, så det går snabbt att komma in i spelet och debugga, utan att det skall skriva till databasen hela tiden. UI är på plats, även om det behövs en del justeringar. Massvis med refakturering av kod är gjort samt flera hjälpklasser och funktioner har lagts till. Behovet uppkom i samband med att det behövdes mer kontroll över hur det renderas ut UI element och text i consolfönstret. Det går numera även att flytta runt pjäserna visuellt på spelplan till viss del. Alla tärningsslag är inte gjorda helt ännu. 

En del buggar har också upptäckts som vi försökt logga i vår backlogg och en del har rättats till direkt utan att hamna i backloggen. Viss felhantering har skett i form av att visuella menyer med fasta returvärden har ersatt Console.Readline. På så vis minskar chanserna att användaren skriver fel eller gör något oväntat.

Fortsatt arbete behövs göras för att få allt på plats, men någon gång i helgen tror jag (micael) vi skall vara klara med allt.