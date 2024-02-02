"use strict";

// Az oldal betöltésekor hívódik meg
window.onload = function () {
    kereses();  // Kezdeti keresés oldalbetöltéskor
};

// Keresési logika
function kereses() {
    // Keresési szövegek lekérése az input mezőkből
    var keresesiSzovegOM = document.getElementById('keresOM').value.toLowerCase();
    var keresesiSzovegNev = document.getElementById('keresNev').value.toLowerCase();
    
    // Az adatokat tartalmazó div lekérése
    var adatokDiv = document.getElementById('adatok');
    adatokDiv.innerHTML = "";

    // Változók inicializálása kártyák számának, matek és magyar összpontszám számolásához
    var cardCount = 0;
    var matekSum = 0;
    var magyarSum = 0;

    // Adatokon való végigiterálás
    for (var i = 0; i < adatok.length; i++) {
        // Az OM azonosító és a név lekérése
        var omAzonosito = adatok[i].OM_Azonosito.toLowerCase();
        var nev = adatok[i].Neve.toLowerCase();

        // Keresési feltételek: az OM azonosítóban vagy névben való kezdődés
        if ((omAzonosito.startsWith(keresesiSzovegOM) || keresesiSzovegOM === "") &&
            (nev.startsWith(keresesiSzovegNev) || keresesiSzovegNev === "")) {

            // Kártya adatainak frissítése
            cardCount++;

            matekSum += parseFloat(adatok[i].Matematika);
            magyarSum += parseFloat(adatok[i].Magyar);

            // Kártya létrehozása
            var card = document.createElement('div');
            card.classList.add('card');

            // Név megjelenítése h2 címkével
            var h2 = document.createElement('h2');
            h2.textContent = adatok[i].Neve;
            card.appendChild(h2);

            // Az OM azonosító, értesítési cím, email, születési dátum, matek átlag, magyar átlag megjelenítése p-ben
            var p1 = document.createElement('p');
            p1.textContent = 'OM Azonosító: ' + adatok[i].OM_Azonosito;
            card.appendChild(p1);

            var p2 = document.createElement('p');
            p2.textContent = 'Ertesitesi Cím: ' + adatok[i].ErtesitesiCime;
            card.appendChild(p2);

            var p3 = document.createElement('p');
            p3.textContent = 'Email: ' + adatok[i].Email;
            card.appendChild(p3);

            var p4 = document.createElement('p');
            p4.textContent = 'Születési Dátum: ' + adatok[i].SzuletesiDatum;
            card.appendChild(p4);

            var p5 = document.createElement('p');
            p5.textContent = 'Matek Átlag: ' + adatok[i].Matematika;
            card.appendChild(p5);

            var p6 = document.createElement('p');
            p6.textContent = 'Magyar Átlag: ' + adatok[i].Magyar;
            card.appendChild(p6);

            // Kártya hozzáadása az adatok div-hez
            adatokDiv.appendChild(card);
        }
    }

    // Kártyák számának megjelenítése
    document.getElementById('tetelekSzama').textContent = cardCount;

    // Matek átlag számolása és megjelenítése
    var matekAtlag = matekSum / cardCount || 0;
    document.getElementById('matekatlagErtek').textContent = matekAtlag.toFixed(2);

    // Magyar átlag számolása és megjelenítése
    var magyarAtlag = magyarSum / cardCount || 0;
    document.getElementById('magyaratlagErtek').textContent = magyarAtlag.toFixed(2);

    // Összesített átlag számolása és megjelenítése
    var osszAtlag = (matekSum + magyarSum) / (2 * cardCount) || 0;
    document.getElementById('osszatlagErtek').textContent = osszAtlag.toFixed(2);
}
