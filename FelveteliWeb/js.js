"use strict"
function Keres() {
  // Az input elem értékének lekérése
  var om = document.getElementById("omAzon").value;

  // Táblázat elemeinek lekérése
  var tablazat = document.getElementById("tablazat");

  // HTML táblázat kezdése
  var htmlTabla = "<table id='tablazat'>";
  htmlTabla += "<tr>";

  // Fejlécek hozzáadása a táblázathoz
  for (var key in adatok[0]) {
    htmlTabla += "<th>" + key + "</th>";
  }

  htmlTabla += "</tr>";

  // Adatok végigiterálása
  adatok.forEach(diak => {
    // OM azonosító alapján szűrés
    if (!isNaN(om) && diak.OM_Azonosito.includes(om)) {
      for (var i = 0; i < 1; i++) {
        // Sor hozzáadása a táblázathoz
        htmlTabla += "<tr>";
        for (var key in adatok[i]) {
          htmlTabla += "<td>" + diak[key] + "</td>";
        }
        htmlTabla += "</tr>";
      }
    } else if (diak.OM_Azonosito == om) {
      // Ha egyedi OM azonosítóra történik keresés
      for (var i = 0; i < 1; i++) {
        // Sor hozzáadása a táblázathoz
        htmlTabla += "<tr>";
        for (var key in adatok[i]) {
          htmlTabla += "<td>" + diak[key] + "</td>";
        }
        htmlTabla += "</tr>";
      }
    }
  });

  // HTML táblázat lezárása
  htmlTabla += "</table>";

  // Az adatok megjelenítése a HTML-ben
  document.getElementById("dataDiv").innerHTML = htmlTabla;
}

// Funkció az adatok kiírására táblázatba
function adatokTablazatba() {
  // HTML táblázat kezdése
  var htmlTabla = "<table id='tablazat'>";
  htmlTabla += "<tr>";

  // Fejlécek hozzáadása a táblázathoz
  for (var key in adatok[0]) {
    htmlTabla += "<th>" + key + "</th>";
  }

  htmlTabla += "</tr>";

  // Adatok végigiterálása
  for (var i = 0; i < adatok.length; i++) {
    // Sor hozzáadása a táblázathoz
    htmlTabla += "<tr>";
    for (var key in adatok[i]) {
      htmlTabla += "<td>" + adatok[i][key] + "</td>";
    }
    htmlTabla += "</tr>";
  }

  // HTML táblázat lezárása
  htmlTabla += "</table>";

  // Az adatok megjelenítése a HTML-ben
  document.getElementById("dataDiv").innerHTML = htmlTabla;
}

// Az oldal betöltésekor elindítandó műveletek
window.onload = function () {
  // Adatok kiírása táblázatba
  adatokTablazatba();

  // Az input elem eseményfigyelése (keresés az input változásakor)
  var inputBox = document.getElementById("omAzon");
  inputBox.addEventListener("input", function () {
    Keres();
  });
};