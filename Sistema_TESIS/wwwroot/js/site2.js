// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
var map = L.map('map').setView([-7.157106, -78.517489], 13);
L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token={accessToken}', {
	attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
	maxZoom: 18,
	id: 'mapbox/streets-v11',
	tileSize: 512,
	zoomOffset: -1,
	accessToken: 'pk.eyJ1IjoicGVkcm9hcXMwNSIsImEiOiJja2ZpZ29iaWowZDVwMnBsM3M2MGxkaWQxIn0.DYp69EtGMZKWNDF6dzuzmQ'
}).addTo(map);

var layer;
//----------------------Obtener latitud y longitud directamente del mapa--------------------------------
function onMapClick(e) {
	if (layer!=null) {
		layer.remove();
    }
	$("#Lat").val(e.latlng.lat.toString());
	$("#Long").val(e.latlng.lng.toString());
	layer = L.marker(e.latlng).addTo(map);
	layer.addTo(map);
}
map.on('click', onMapClick);

var capalim;
function Limites() {
	if (capalim != null) {
		capalim.remove();
	}
	var dist = $('#Dist').val();
	const limitepin = new XMLHttpRequest();
	limitepin.open('GET', '/Limites/' + dist + '.json', true);
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

document.querySelector('#Dist').addEventListener('change', (event) => { Limites() });
