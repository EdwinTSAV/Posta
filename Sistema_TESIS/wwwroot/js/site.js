// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
$(document).ready(function () {
	var height = $(window).height();
	$('.map-row').height(height * 0.90);
	$('.footer').removeClass('footer');
	$('.footer').addClass('footer');
	var map = L.map('map').setView([-7.157106, -78.517489], 13);
	L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
		attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
		maxZoom: 18,
		id: 'mapbox/streets-v11',
		tileSize: 512,
		zoomOffset: -1,
		accessToken: 'pk.eyJ1IjoicGVkcm9hcXMwNSIsImEiOiJja2ZpZ29iaWowZDVwMnBsM3M2MGxkaWQxIn0.DYp69EtGMZKWNDF6dzuzmQ'
	}).addTo(map);
	// Write your JavaScript code.
	// variables de conteo;
	let p = 0, r = 0, n = 0, f = 0;
	//-----------------------Crear Mapa-----------------


	//------------------Creamos circulos-----------------
	var circulos = [];
	function CrearCir() {
		$.ajax({
			url: '/Home/Cords'
		}).done(function (x) {
			let cord = JSON.parse(x);
			for (let persona of cord) {
				var color;
				var opacidad;
				switch (persona.Estado) {
					case 'Positivo':
						color = '#f03';
						opacidad = 0.5;
						p++;
						break;
					case 'Recuperado':
						color = '#0006FF';
						opacidad = 0.5;
						r++;
						break;
					case 'Fallecido':
						color = '#000000';
						opacidad = 1;
						f++;
						break;
					case 'Negativo':
						color = '#000000';
						opacidad = 1;
						n++;
						break;
				}
				if (persona.Estado == "Positivo" || persona.Estado == "Recuperado") {
					var circle = L.circle([persona.Ubicacion.Lat, persona.Ubicacion.Long], {
						fillColor: color,
						fillOpacity: opacidad,
						radius: 500,
						stroke: false
					}).addTo(map);
					circulos.push(circle);

				}
			}
			agre(p, r, n, f);
		});
	}
	CrearCir()
	//------------------El tamaño d elos circulos segun el zoom--------------

	//map.on('zoomend', function () {
	//	var currentZoom = map.getZoom();
	//	console.log(currentZoom);
	//	for (let item of circulos) {
	//		item.setRadius(getRadius()+currentZoom);
	//	}
	//});

	//-----------------Crear limites de distrito----------
	var capalim;
	function Limites(lim) {
		const limitepin = new XMLHttpRequest();
		limitepin.open('GET', '/Limites/' + lim + '.json', true);
		limitepin.send();
		limitepin.onreadystatechange = function () {
			if (this.readyState == 4 && this.status == 200) {
				let x = JSON.parse(this.responseText);
				//console.log(this.responseText);
				capalim = L.geoJSON(x);
				capalim.addTo(map);
			}
		}
	}
	let limp = '0';
	Limites(limp);

	//---------------------Aplicar filtro-----------------

	function filtro() {
		for (let item of circulos) {
			item.remove();
		}
		map.setZoom(10);
		capalim.remove();
		circulos.length = 0;
		var dist = $('#Dist').val();
		var tipo = $('#Tipo').val();
		$.ajax({
			url: '/Home/Pintar?tipo=' + tipo
		}).done(function (resultpint) {
			let datos = JSON.parse(resultpint);
			Limites(dist);
			for (let persona of datos) {
				if (persona.Ubicacion.DistritoId == dist) {
					var color;
					var opacidad;
					switch (persona.Estado) {
						case 'Positivo':
							color = '#f03';
							opacidad = 0.5;
							break;
						case 'Recuperado':
							color = '#0006FF';
							opacidad = 0.5;
							break;
						case 'Fallecido':
							color = '#0006FF';
							opacidad = 0.9;
							break;
						case 'Negativo':
							color = '#111111';
							opacidad = 1.0;
							break;
					}
					if (persona.Estado == "Positivo" || persona.Estado == "Recuperado") {
						var circle = L.circle([persona.Ubicacion.Lat, persona.Ubicacion.Long], {
							fillColor: color,
							fillOpacity: opacidad,
							radius: 1500,
							stroke: false
						}).addTo(map);
						circulos.push(circle);
                    }
				}
			}
		});
	}
	document.querySelector('#Dist').addEventListener('change', (event) => { filtro(); leyenda(); });
	document.querySelector('#Tipo').addEventListener('change', (event) => { filtro() });


	//----------------------Obtener latitud y longitud directamente del mapa--------------------------------
	var popup = L.popup();

	function onMapClick(e) {
		popup
			.setLatLng(e.latlng) // Sets the geographical point where the popup will open.
			.setContent("Has hecho click en la coordenada:<br> " + e.latlng.lat.toString() + "," + e.latlng.lng.toString()) // Sets the HTML content of the popup.
			.openOn(map); // Adds the popup to the map and closes the previous one. 
		var lat = document.querySelector('latitud');
		lat.nodeValue(e.latlng.lat.toString())
	}
	map.on('click', onMapClick);

	//---------------------Prueba para eliminar capas----------------------------
	$('#delCap').on('click', function (e) {
		e.preventDefault();
		for (let item of circulos) {
			item.remove();
		}
		limp = '0';
		capalim.remove();
		Limites(limp);
	});

	//----------prueba 1
	function elim() {
		$('.card-body').remove();
	}
	function agre(positivos, recuperados, negativos, fallecidos) {
		$('#cardLeyenda').append('<div class="card-body">' +
			'<div class="d-flex py-1 align-items-center">' +
			'<div>' +
			'	<span>Positivos</span>' +
			'</div>' +
			'<div class="justify-content-end ml-auto">' +
			'<span id="Positivos" class="badge badge-danger p-1">' + positivos + '</span>' +
			'</div>' +
			'</div>' +
			'<div class="d-flex py-1 align-items-center">' +
			'<div>' +
			'	<span>Recuperados</span>' +
			'</div>' +
			'<div class="justify-content-end ml-auto">' +
			'	<span id="Recuperados" class="badge badge-primary p-1">' + recuperados + '</span>' +
			'</div>' +
			'</div>' +
			'<div class="d-flex py-1 align-items-center">' +
			'<div>' +
			'	<span>Negativos</span>' +
			'</div>' +
			'<div class="justify-content-end ml-auto">' +
			'	<span id="Negativos" class="badge badge-success p-1">' + negativos + '</span>' +
			'</div>' +
			'</div>' +
			'<div class="d-flex py-1 align-items-center">' +
			'<div>' +
			'	<span>Fallecidos</span>' +
			'</div>' +
			'<div class="justify-content-end ml-auto">' +
			'<span id="Fallecidos" class="badge badge-dark p-1">' + fallecidos + '</span>' +
			'</div>' +
			'	</div>' +
			'</div>');
	}

	//----------------Cantidad de casos---------------------------
	function leyenda() {
		elim();
		$.ajax({
			url: '/Home/Cords'
		}).done(function (resultpint) {
			let cantp = 0; cantr = 0; cantn = 0; cantf = 0;
			let datos = JSON.parse(resultpint);
			for (let persona of datos) {
				if (persona.Ubicacion.DistritoId == $('#Dist').val()) {
					var color;
					var opacidad;
					switch (persona.Estado) {
						case 'Positivo':
							color = '#f03';
							opacidad = 0.5;
							cantp++;
							break;
						case 'Recuperado':
							color = '#0006FF';
							opacidad = 0.5;
							cantr++;
							break;
						case 'Fallecido':
							color = '#000000';
							opacidad = 1;
							cantn++;
							break;
						case 'Negativo':
							color = '#000000';
							opacidad = 1;
							cantf++;
							break;
						default:
							
							break;
					}
				} 
			}
			agre(cantp, cantr, cantn, cantf);
		});
	}


	//------------Tamaño de circulos-----
	var myZoom = { start: map.getZoom(), end: map.getZoom() };
	map.on('zoomstart', function (e) { myZoom.start = map.getZoom(); });
	map.on('zoomend', function (e) {
	myZoom.end = map.getZoom();
		var diff = myZoom.start - myZoom.end;
		if (diff > 0) {
			for (let item of circulos) {
				item.setRadius(item.getRadius() * 2);
			}

		} else if (diff < 0) {
			for (let item of circulos) {
				item.setRadius(item.getRadius() / 2);
			}
		}
	});
});




