2020-03-30 
Jag gjorde ett exempel på hur vi kan skapa en klass med olika egenskaper och sedan skapa instans av klassen för olika spelaren och sedan lägga det i en lista. Då kommer varje index i listan innehålla en spelar med olika egenskaper. Sedan runt klockan 20 blev alla online på discord och då började vi skriva programflödet .

2020-03-31
Vi jobbade med flow chart och user story. Skrev flow chart för database och spelomgången

2020-04-02
Vi jobbade vidare med flow chart och diskuterade olika design för databasen. Vi har delat uppgifter mellan oss.

2020-04-04
har gjort en databas design men sedan kom på att det finns brister i designen. tog bort allting och började om.

2020-04-05
jobbar med databasen code first. installerar all extensions och förberedder context klassen och läser in json filen och skapar tabeller och förberedder för data inserting.

2020-04-06
jobbar vidare med databasen, skapar methoden som tar in parametrar och parametrarna tar emot players information och lagrar det i database.

sedan använder olika linq och query för att joina tabeller och läsa data från dem.

2020-04-07
använder linq för att joina tabeller och sedan hämta all data för ett spel för att ladda den datan för försättning av spelet.

skickar datan som list object, har testat listarray och Tuple men det som funkade bäst var list av typen object.
idag gjorde lite felhantering och snyggat till koden, nu är allt färdig och ska pusha upp

2020-04-08
har försökt att skapa en lista med objekt för att lagra varje personens pjäss i det. och sedan tänkte gör context.addrange(mylist);
men det blv fel eftersom jag har join tabell på många ställe och många foreign key är beroende av andra primary key.  
därför blev det fel

man jag har gjort ett försök i alla fall. om jag hade lyckats med den programmet kunde snabbas mycket mer än det är nu. eftersom nu den sparar varje pjäss separat. därför det blir många context.saveChanges();  det tar mycket tid att spara varje pjäss separat.


2020-04-10
har gått genom koden och kommenterat där det behövdes och även kopplat vissa metoder som kommer från databas klassen till menu klassen. 