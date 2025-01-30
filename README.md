# WorkTracker

## Uputstvo za upotrebu

### Uvod
WorkTracker predstavlja sistem koji je namjenjen kompanijama za praćenje radnog vremena njihovih zaposlenih, upravljanjem radnih zadataka zapolsenih, uvid u ostvarene rezultate na različitim nivoima kao i lakšu organizaciju rada u samoj kompaniji.

#### Tipovi naloga:
1. Administratori
2. Menadžeri sektora
3. Radnici

### Prijava na sistem
Kada se pokrene aplikacija, korisnicima se omogućava prijava na sistem kroz jednostavnu formu gdje se unose korisničko ime i lozinka. Same kredencijale svaki od korisnika sistema dobija fizičkim putem od administratora sistema koji i kreira sve naloge na sistemu. Prilikom prijave, vrijednost lozinke je prekrivena, ali se može prikazati klikom na dugme pored vrijednosti(ikonica oka), kao i ukloniti unesena lozinka(ikonica x). Nakon unosa kredencijala, korisnik treba da klikne na dugme "Prijavi se" nakon čega sistem obrađuje zahtjev. U slučaju uspješne prijave korisniku se otvara odgovarajući interfejs u zavisnosti od tipa njegovog naloga.
Na samoj formi za prijavu nalaze se mogućnosti izbora između tamne i svijetle teme na sistemu (gornji lijevi ugao, svijetla tema ikonica sunce, tamna mjesec) te mogućnost izbora jezika klikom na zastavicu svakog od jezika koji su ponuđeni u donjem desnom uglu prijavne forme.


![image](https://github.com/user-attachments/assets/3b880539-22c8-4d74-b525-89dbdda9e635)

Izgled prijavne forme u tamnoj temi

![image](https://github.com/user-attachments/assets/7c22a5e6-055b-462d-bac6-4c6620749144)


Izgled prijavne forme u svijetloj temi

## Administratorski interfejs
Nakon što se korisnik na sistem prijavi administratorskim nalogom, prikazuje mu se interfejs kao na slici ispod.
![image](https://github.com/user-attachments/assets/71dbe191-5593-4785-8357-586ef61a2198)

Na samom centralnom dijelu ekrana nalaze se osnovne informacije o broju naloga po svakom tipu naloga na ssitemu, pri čemu se prikazuje i broj deaktiviranih naloga za svaki tip. Takođe, prikazuje se i broj sektora na sistemu kao i broj slobodnih radnika, odnosno radnika koji nisu pridruženi niti jednom sektoru. Administratoru se prikazuju korisničko ime i njegova fotografija na sistemu, a omogućen je i pregled kalendara (informativno, klasični sistemski kalendar, gornji lijevi ugao).
Sa lijeve strane nalazi se glavni meni koji sadrži sve opcije koje administratorski interfejs nudi, a može da bude minimizovan kao na prethodnoj slici ili proširen kao na narednoj slici.
![image](https://github.com/user-attachments/assets/2c573907-3808-4d6e-b23f-b81a53abd6c3)

Proširivanje/skupljanje se vrši klikom na logo aplikacije. Trentno izabrana stranica je označena zelenim vertikalnim markerom.
### Kreiranje naloga

Osnovni zadatak administratora jeste kreiranje novih korisničkih naloga na sistemu (administratorskih, menadžerskih i radničkih). Kreiranje novih naloga se vrši kroz interfejs na sledećoj slici.
![image](https://github.com/user-attachments/assets/02512a66-3042-40d9-a6d5-f8e9efc10303)
Administrator popunjava polja forme i bira tip naloga,a svaka od unesenih vrijednosti se provjerava, tako da administrator mora da unese validnu vrijednost za svako od polja (broj telefona, email,šifra,dovoljno sigurna lozinka, jedinstveno korisničko ime). Nakon popunjavanja validnim parametrima novi nalog će biti kreiran. Primjer nevalidne lozinke
![image](https://github.com/user-attachments/assets/fd589406-41e7-4f53-9c52-3fbe4cacbfcc)

### Upravljanje nalozima

Administratori imaju mogućnost pregleda i upravljanja nad svim neadministratorskim nalozima na sistemu (radnici, menadžeri sektora). Administratori mogu da aktiviraju i deaktiviraju svaki od naloga klikom na dugme u desnom dijelu svake od korisničkihh kartica. Moguće je filtriranje prikaza kroz izbor tipa naloga koji želimo da se prikaže kao i kroz mogućnost pretrage na osnovu imena, prezime, ili korisničkog imena. Interfejs za upravljanje nalozima prikazan je na sledećoj slici.

![image](https://github.com/user-attachments/assets/68b87899-9c8a-471e-885b-5a5642d50187)

### Kreiranje sektora

Administrator je zadužen i za kreiranje sektora unutar kompanije. Kreiranje sektora vrši se popunjavanjem sledeće forme

![image](https://github.com/user-attachments/assets/135e4b9f-1b3c-43c2-b4d6-287f76297176)

Prilikom kreiranja sektora, administrator popunjava prikazanu formu, a takođe neophodno je da iz liste u desnom dijelu forme izabere minimalno jednog menadžera koji će biti menadžer novokreiranog sektora. Biranje menadžera vrši se klikom na dugme sa kvačicom kraj svakog menadžera, a ukoliko u toku kreiranja želimo da uklonimo nekog od izabranih menadžera to radimo klikom na X dugme.Jedan menadžer može da bude menadžer u više sektora.

![image](https://github.com/user-attachments/assets/d3f9cf4c-f9fa-430c-8530-55c6b7666776)
Izgled ispunjene forme.

## Interfejs menadžera sektora

Nakon što se korisnik na sistem prijavi menadžerskim nalogom, prikazuje mu se interfejs kao na slici ispod. 

![image](https://github.com/user-attachments/assets/d88e4788-58d2-479b-9958-94c5dd1264af)

U lijevom dijelu centralnog interfejsa nalaze se podaci vezani za broj sektora u kome je prijavljeni korisnik menadžer kao i broj radnika u svim sektorima u kojima je prijavljeni korisnik menadžer. 
U desnom dijeli nalazi se kartica koja omogućava pregled zadataka koji su dodjeljeni radnicima kojima je prijavljeni korisnik menadžer. Zadaci su organizovani u četri grupe na osnovu njihovog statusa a pregled grupa se mijenja izborom svakog od pojedinačnih tabova. Zadaci koji su označeni za rad (inicijalno stanje nakon kreiranja) se mogu brisati klikom na ikonicu kante. Ostali zadaci koji su u drugim stanjima se ne mogu brisati. Zadaci koji su označeni sa Isteklo predstavljaju zadatke koji nisu završeni u roku koji je predviđen. 

![image](https://github.com/user-attachments/assets/1fb07578-e080-4ed0-acc5-ff5696c7bac4)
Prikaz zadataka u radu.

Klikom na dugme označeno + ikonicom vrši se kreiranje novog zadatka. Interfejs za krairanje novog zadatka prikazan je na sledećoj slici.

![image](https://github.com/user-attachments/assets/8a7439a3-4e64-41ce-9b84-76cb45d3e843)

Menadzer popunjava sve informacije o zadatku koji kreira. Datum i vrijeme dospjeća zadatka mogu da se biraju klikom na ikonice kalendara odnosno sata u sklopu polja za unos ili da se direktno unose u odgovarajućem formatu. Menadžer mora da izabere tačno jednog radnika, iz liste radnika svih sektora u kojima je prijavljeni korisnik menadžer, u desnom dijelu interfejsa kome će novokreirani zadatak biti dodjeljen, a izbor vrši klikom na dugme na kartici željenog radnika. 

![image](https://github.com/user-attachments/assets/b08d6139-867f-4b53-a52a-bdabcfd3ee66)
### Upravljanje sektorima

Menadžer ima mogućnost pregleda i upravljanjem sektorima u kojima je menadžer. Menadžeru se prikazuju svi sektori u kojima je menadžer u formi liste pri čemu se prikazuju neki osnovni podaci o sektoru poput naziva sektora i broja radnika. Postoji mogućnost i pretrage sektora po nazivu sektora.

![image](https://github.com/user-attachments/assets/26b45f3d-99a9-44e6-a53b-f62548e284f0)

Za prikaz detalalja korisnik treba da klikne dugme "Prikaz detalja". Izgled interfejsa koji se prikazuje dat je na narednoj slici.

![image](https://github.com/user-attachments/assets/33f48853-0d11-45f2-88c0-cba5be31eeeb)

Menadžer kroz prikazani interfejs ima mogućnost izmjene satnice i satnice za prekovremeni rad unutar izabranog sektora. Kako bi to izvršio neopohodno je da klikne na na ikonicu olovke kako bi mu se omogućila izmjena. Nakon izmjene neophodno je da klikne dugme "Sačuvaj promjene". Osim izmjene satnica, menadžer sa desne strane ima listu radnika koji pripadaju sektoru, i ima mogućnost uklanjanja radnika iz sektora. Kako bi uklonio radnika iz sektora korisnik mora da klikne dugme X kraj željenog radnika nakon čega mu se pojavljuje modalni prozor za potvrdu te akcije. 

![image](https://github.com/user-attachments/assets/be6bd7f4-6d72-45f6-985c-2359470a6796)

Za dodavanje novih radnika u sektor korisnik mora da klikne na dugme plus(gornji desni ugao). Interfejs za dodavanje novih radnika dat je na sledećoj slici.

![image](https://github.com/user-attachments/assets/07c1648c-9f7d-4cf7-b8e4-8f5ef9c2f868)

Menadžer dobija prikaz svih slobodnih radnika(radnika koji ne pripadaju ni jednom sektoru) te ima mogućnost izbora jednog od njih. Takodje, moguće je pretraživanje po imenu, prezimenu ili korisničkom imenu. Nakon izbora radnika neophodno je kliknuti dugme "Dodajte radnika u sektor" nakon čega će se izabrani radnik dodati u sektor.

### Statistika radnika

Menadžer ima mogućnost pregleda statistike, aktivnosti, efikasnosti i zarade svakog od radnika unutar svojih sektora. Interfejs za prikaz statistike dat je na sledećoj slici.

![image](https://github.com/user-attachments/assets/591157f1-2d2a-4ac7-87f9-1db6156ca588)

Kako bi pregledao statistiku svakog od radnika, menader treba da klikne na dugme "Pregled statistike". Nakon klika na to dugme otvara mu se sledeći interfejs.

![image](https://github.com/user-attachments/assets/ed72937d-572f-434b-a54e-ffe8c2f2ac0f)

U gornjem dijelu interfejsa nalaze se kartice o aktivnosti, efikasnosti i prihodu radnika redom. Unutar kartice za pregled aktivnosti imamo mogućnost izbora na sedmičnom ili dnevnom nivou a podaci koji se prikazuju predstavljaju zapravo izvršenost normi u vidu broja sati i broja zadataka koji za taj dan trebaju da budu zaršeni.
U kartici efikasnosti prikazuje se procenat uspješnosti izvršenja zadataka koji predstavlja odnos broja zadataka koji su uspješno završeni i broja zadataka koji su dodjeljeni tom radniku tokom njegove karijere.
Kartica pregleda prihoda omogućava pregled ukupnih prihoda za svaki mjesec izabrane godine. Korisnik bira godinu i mjesec od interesa a zatim mu se ispisuje ukupan prihod radnika za izabrane parametre.

U donjem dijelu interfejsa nalaze se izvještaji o svim radnim sesijama za izabrani period. Korisnik bira mjesec i godinu od interesa a zatim mu se prikazuju sve radne sesije koje je radnik imao u tom mjesecu. Menadžer ima mogućnost sortiranja zapisa po datumu radne sesije i ostvarenom prihodu za tu sesiju u opadajućem i rastućem poretku. Za sortiranje vrši izbor unutar comboBox elementa u desnom dijelu kartice. Za prikaz detalja o svakoj sesiji korisnik treba da klikne dugme "Prikaz detalja" za željenu sesiju. Nakon klika, prikazuje mu se modalni prozor sa informacijama koji je dat na narednoj slici.

![image](https://github.com/user-attachments/assets/9b947025-54d4-480b-9b50-19c8faf9db6f)

U izvještaju se nalaze svi podaci o radnim satima, prekovremenim satima, cijenama rada u trenutku završavanja sesije, ostvareni prihod radnika, kao i zapisi o svim pauzama koje je radnik pravio u toku te sesije.

## Interfejs radnika

















