2020-03-30 
Jag gjorde ett exempel p� hur vi kan skapa en klass med olika egenskaper och sedan skapa instans av klassen f�r olika spelaren och sedan l�gga det i en lista. D� kommer varje index i listan inneh�lla en spelar med olika egenskaper. Sedan runt klockan 20 blev alla online p� discord och d� b�rjade vi skriva programfl�det .

2020-03-31
Vi jobbade med flow chart och user story. Skrev flow chart f�r database och spelomg�ngen

2020-04-02
Vi jobbade vidare med flow chart och diskuterade olika design f�r databasen. Vi har delat uppgifter mellan oss.

2020-04-04
har gjort en databas design men sedan kom p� att det finns brister i designen. tog bort allting och b�rjade om.

2020-04-05
jobbar med databasen code first. installerar all extensions och f�rberedder context klassen och l�ser in json filen och skapar tabeller och f�rberedder f�r data inserting.

2020-04-06
jobbar vidare med databasen, skapar methoden som tar in parametrar och parametrarna tar emot players information och lagrar det i database.

sedan anv�nder olika linq och query f�r att joina tabeller och l�sa data fr�n dem.

2020-04-07
anv�nder linq f�r att joina tabeller och sedan h�mta all data f�r ett spel f�r att ladda den datan f�r f�rs�ttning av spelet.

skickar datan som list object, har testat listarray och Tuple men det som funkade b�st var list av typen object.
idag gjorde lite felhantering och snyggat till koden, nu �r allt f�rdig och ska pusha upp

2020-04-08
har f�rs�kt att skapa en lista med objekt f�r att lagra varje personens pj�ss i det. och sedan t�nkte g�r context.addrange(mylist);
men det blv fel eftersom jag har join tabell p� m�nga st�lle och m�nga foreign key �r beroende av andra primary key.  
d�rf�r blev det fel

man jag har gjort ett f�rs�k i alla fall. om jag hade lyckats med den programmet kunde snabbas mycket mer �n det �r nu. eftersom nu den sparar varje pj�ss separat. d�rf�r det blir m�nga context.saveChanges();  det tar mycket tid att spara varje pj�ss separat.


2020-04-10
har g�tt genom koden och kommenterat d�r det beh�vdes och �ven kopplat vissa metoder som kommer fr�n databas klassen till menu klassen.

2020-04-12
jobbade med att testa databasen i olika situationer. ett fel hittades. felet var att om man skrev ingen session namn d� fick man nekades det att skapa ny session men sedan s� lagrades player name och color i player tabellen. fick l�sa det med en kontroll.

2020-04-13
jobbade med att l�sa buggar och updatering av pj�s positioner. uppt�ckte fel att det h�mtade 4 player name av samma spelare detta p� grund av att en spelare har 4 pj�s.  fick l�sa det genom att separera pj�sen och andra egeneskaper sedan s� gjorde en group by och sedan s� f�rsvann problemet. 

2020-04-14
ska test k�ra spelet i olika situationer.till exempel skapa ny session , spela, spara, visa high score, mata in fel data,fel format, och ladda upp data fr�n DB osv..

ska prova generic host i projektet om tiden r�cker annars l�ter vi det som det �r.