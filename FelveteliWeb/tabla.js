"use strict"
var rendezesiSorrendek = {}; // Objektum a rendezési sorrendek tárolására minden oszlophoz

// Függvény a tábla generálásához
function tablatGeneralo(adatok) {
  // Táblaelem létrehozása vagy meglévő tábla lekérése
  var tabla = document.querySelector('table') || document.createElement('table');
  tabla.innerHTML = '<thead><tr>' +
    '<th onclick="tablaRendezese(0)">OM Azonosito</th>' +
    '<th onclick="tablaRendezese(1)">Neve</th>' +
    '<th onclick="tablaRendezese(2)">Ertesitesi Cime</th>' +
    '<th onclick="tablaRendezese(3)">Email</th>' +
    '<th onclick="tablaRendezese(4)">Szuletesi Datum</th>' +
    '<th onclick="tablaRendezese(5)">Matematika</th>' +
    '<th onclick="tablaRendezese(6)">Magyar</th>' +
    '</tr></thead><tbody></tbody>';

  var tbody = tabla.querySelector('tbody');

  // Adatok feltöltése a táblába
  adatok.forEach(function (diak) {
    var sor = tbody.insertRow();
    for (var tulajdonsag in diak) {
      var cella = sor.insertCell();
      cella.textContent = diak[tulajdonsag];
    }
  });

  // Tábla cseréje vagy új hozzáadása a DOM-hoz
  var regiTabela = document.querySelector('table');
  if (regiTabela) {
    regiTabela.parentNode.replaceChild(tabla, regiTabela);
  } else {
    document.body.appendChild(tabla);
  }
}

// Függvény a tábla rendezéséhez
function tablaRendezese(oszlopIndex) {
  var tabla, sorok, cserelgetes, i, x, y, kellCserelni;
  tabla = document.querySelector('table');
  cserelgetes = true;

  // Oszlop rendezési sorrendjének megadása alaphelyzetben 
  rendezesiSorrendek[oszlopIndex] = rendezesiSorrendek[oszlopIndex] || 'asc';

  while (cserelgetes) {
    cserelgetes = false;
    sorok = tabla.rows;

    for (i = 1; i < sorok.length - 1; i++) {
      kellCserelni = false;
      x = sorok[i].getElementsByTagName('td')[oszlopIndex].textContent.toLowerCase();
      y = sorok[i + 1].getElementsByTagName('td')[oszlopIndex].textContent.toLowerCase();

      // "Matematika" és "Magyar" oszlopok esetén konvertálás számmá
      if (oszlopIndex === 5 || oszlopIndex === 6) {
        x = parseFloat(x);
        y = parseFloat(y);
      }

      if (rendezesiSorrendek[oszlopIndex] === 'asc') {
        // Növekvő sorrend
        if (x > y || (isNaN(x) && !isNaN(y))) {
          kellCserelni = true;
          break;
        }
      } else {
        // Csökkenő sorrend
        if (x < y || (!isNaN(x) && isNaN(y))) {
          kellCserelni = true;
          break;
        }
      }
    }
// Mr.KálóBone bemutatkozom mi vagyunk a nyolcasok teliberakom a mikrofont szanaszét szedem sakkmattot kaptatok verebek vagytok ti kavartatok
// mondjátok meg mit akartatok mert tudod én vagyok az aki majd titetket odarak tetsziknemtetszik bumm meghúzom én a ravaszt
    if (kellCserelni) {
      sorok[i].parentNode.insertBefore(sorok[i + 1], sorok[i]);
      cserelgetes = true;
    }
  }

  // Oszlop rendezési sorrendjének váltása
  rendezesiSorrendek[oszlopIndex] = rendezesiSorrendek[oszlopIndex] === 'asc' ? 'desc' : 'asc';

  // Nyilak eltávolítása az összes fejlécről
  var fejlecok = document.querySelectorAll('th');
  fejlecok.forEach(function (fejlec) {
    fejlec.innerHTML = fejlec.textContent.replace(' ▲', '').replace(' ▼', '');
  });

  // Nyil hozzáadása a kattintott fejléchez
  var nyil = rendezesiSorrendek[oszlopIndex] === 'asc' ? ' ▲' : ' ▼';
  fejlecok[oszlopIndex].innerHTML += nyil;
}

function filterStudents() {
  var minMagyar = parseInt(document.getElementById("minMagyar").value) || 0;
  var minMatek = parseInt(document.getElementById("minMatek").value) || 0;

  var filteredStudents = adatok.filter(function (diak) {
    return diak.Magyar >= minMagyar && diak.Matematika >= minMatek;
  });

  tablatGeneralo(filteredStudents);
}


function createCSV(data) {
  var csvContent = "OM Azonosito,Neve,Ertesitesi Cime,Email,Szuletesi Datum,Matematika,Magyar\n";

  data.forEach(function (diak) {
    csvContent += Object.values(diak).join(",") + "\n";
  });

  return csvContent;
}

function displayCSV(data) {
  var csv = createCSV(data);

  var textarea = document.getElementById("csvTextarea");
  if (!textarea) {
    textarea = document.createElement("textarea");
    textarea.id = "csvTextarea";
    document.body.appendChild(textarea);
  }

  // Beállítás: Minimum magasság, sötét stílus és szélesség
  textarea.style.minHeight = "50vh";
  textarea.style.backgroundColor = "#333";  // Választhatod az általad kívánt sötét színt
  textarea.style.color = "#fff";  // Szöveg színe
  textarea.style.width = document.querySelector('table').offsetWidth + 'px';

  textarea.value = csv;
}


// Módosított filterStudents függvény
function filterStudents() {
  var minMagyar = parseInt(document.getElementById("minMagyar").value) || 0;
  var minMatek = parseInt(document.getElementById("minMatek").value) || 0;

  var filteredStudents = adatok.filter(function (diak) {
    return diak.Magyar >= minMagyar && diak.Matematika >= minMatek;
  });

  tablatGeneralo(filteredStudents);

  // Hozzáadás: CSV megjelenítése a textarea-ban
  displayCSV(filteredStudents);
}

// Az input mezők referenciái
var minMagyarInput = document.getElementById("minMagyar");
var minMatekInput = document.getElementById("minMatek");

// Az input mezők oninput eseményéhez tartozó eseménykezelő
minMagyarInput.oninput = function () {
  filterAndDisplay();
};

minMatekInput.oninput = function () {
  filterAndDisplay();
};

// Inicializációs függvény, amely a táblázatot és a textarea-t létrehozza betöltéskor
function initialize() {
  tablatGeneralo(adatok);
  filterAndDisplay(); // A filterAndDisplay hívásával a textarea is létrejön az alapértelmezett értékekkel
}

// A filterAndDisplay függvény, amely a táblázatot és a textarea-t létrehozza az input mezők értékei alapján
function filterAndDisplay() {
  var minMagyar = parseInt(minMagyarInput.value) || 0;
  var minMatek = parseInt(minMatekInput.value) || 0;

  var filteredStudents = adatok.filter(function (diak) {
    return diak.Magyar >= minMagyar && diak.Matematika >= minMatek;
  });

  tablatGeneralo(filteredStudents);
  displayCSV(filteredStudents);
}

// Az oldal betöltésekor hívódik meg
window.onload = function () {
  tablatGeneralo(adatok);
  initialize();
};


