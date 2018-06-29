<?php

$dbCamHost = "HOST"; 
$dbCamUsuario = "USER";
$dbCamPassword = "PASSWORD";
$dbCamDB = "DB";


function camSetCookie($Name, $Value = '', $MaxAge = 0, $Path = '', $Domain = '', $Secure = false, $HTTPOnly = false) { 
  header('Set-Cookie: ' . rawurlencode($Name) . '=' . rawurlencode($Value) 
                        . (empty($MaxAge) ? '' : '; Max-Age=' . $MaxAge) 
                        . (empty($Path)   ? '' : '; path=' . $Path) 
                        . (empty($Domain) ? '' : '; domain=' . $Domain) 
                        . (!$Secure       ? '' : '; secure') 
                        . (!$HTTPOnly     ? '' : '; HttpOnly'), false); 
}  

// Leer un valor de una cookie
function camLeerCookie($nombre, &$valor) {
	if (isset($_COOKIE["radAR" . $nombre])) 
		$valor = $_COOKIE["radAR" . $nombre];
}

// Almacenar un valor en una cookie de CAM
function camGuardarCookie($nombre, $valor) {
	camSetCookie("radAR" . $nombre, $valor, 2147483647);
}


function camPuntoEnPoligono($point, $polygon){
    if($polygon[0] != $polygon[count($polygon)-1])
        $polygon[count($polygon)] = $polygon[0];
    $j = 0;
    $oddNodes = false;
    $x = $point[1];
    $y = $point[0];
    $n = count($polygon);
    for ($i = 0; $i < $n; $i++)
    {
        $j++;
        if ($j == $n)
        {
            $j = 0;
        }
        if ((($polygon[$i][0] < $y) && ($polygon[$j][0] >= $y)) || (($polygon[$j][0] < $y) && ($polygon[$i][0] >=
            $y)))
        {
            if ($polygon[$i][1] + ($y - $polygon[$i][0]) / ($polygon[$j][0] - $polygon[$i][0]) * ($polygon[$j][1] -
                $polygon[$i][1]) < $x)
            {
                $oddNodes = !$oddNodes;
            }
        }
    }
    return $oddNodes;
}
	

// Convertir los grupos de SHAPE a array
function camWKTaArray($wkt_str) {
  $ret_arr = array();
  $matches = array();
  preg_match('/\)\s*,\s*\(/', $wkt_str, $matches);
  if(empty($matches))
    $polys = array(trim($wkt_str));
  else
    $polys = explode($matches[0], trim($wkt_str));
  foreach($polys as $poly)
    $ret_arr[] = str_replace('(','',str_replace(')','',substr($poly, stripos($poly,'(')+2, stripos($poly,')')-2))); 
  return $ret_arr;
}

// realizar una consulta SQL a la base de datos
function camConsultarDB($sql) {
	global $dbCamHost; 
	global $dbCamUsuario;
	global $dbCamPassword;
	global $dbCamDB;
	$dbHandle = mysql_connect($dbCamHost, $dbCamUsuario, $dbCamPassword) or die("Error al conectar a MySQL");
	mysql_set_charset('utf8', $dbHandle);
	$dbSelect = mysql_select_db($dbCamDB, $dbHandle) or die("Error al conectar a la base de datos CAM");
	$query = mysql_query($sql);
	$dataSet = array();
	while($row = mysql_fetch_array($query)) $dataSet[] = $row;
	mysql_close($dbHandle);
	return $dataSet;
}

function camEjecutarDB($sql) {
	global $dbCamHost; 
	global $dbCamUsuario;
	global $dbCamPassword;
	global $dbCamDB;
	$dbHandle = mysql_connect($dbCamHost, $dbCamUsuario, $dbCamPassword) or die("Error al conectar a MySQL");
	mysql_set_charset('utf8', $dbHandle);
	$dbSelect = mysql_select_db($dbCamDB, $dbHandle) or die("Error al conectar a la base de datos CAM");
	$result = mysql_query($sql);
	mysql_close($dbHandle);
	return $result;
}


// Obtener el radar que corresponde con la sigla
function camObtenerRadarSigla($sigla) {
	return camConsultarDB("SELECT r.idRadares, r.Sigla, r.Nombre, r.Modelo, r.Fuente, p.Sigla as SiglaProvincia, pa.Pais, " .
	"r.Latitud, r.Longitud, p.nprov as Provincia " .
	"FROM radsat.radares r LEFT OUTER JOIN radsat.provincias p ON (r.idProvincias = p.idProvincias) " .
	"INNER JOIN paises pa ON (r.idPaises = pa.idPaises) WHERE r.Activo = 1 AND " . 
	"r.Sigla LIKE '" . $sigla . "'");
}

// Obtener el radar que corresponde con la sigla
function camObtenerRadarID($id) {
	return camConsultarDB("SELECT r.idRadares, r.Sigla, r.Nombre, r.Modelo, r.Fuente, p.Sigla as SiglaProvincia, pa.Pais, " .
	"r.Latitud, r.Longitud, p.nprov as Provincia " .
	"FROM radsat.radares r LEFT OUTER JOIN provincias p ON (r.idProvincias = p.OGR_FID) " .
	"INNER JOIN paises pa ON (r.idPaises = pa.idPaises) WHERE r.Activo = 1 AND " . 
	"r.idRadares = " . $id);
}

// Obtener el radar mas cercano a un punto
function camObtenerRadarProximo($lat, $lng) {
	return camConsultarDB("SELECT r.idRadares, r.Sigla, r.Nombre, r.Modelo, r.Fuente, p.Sigla as SiglaProvincia, pa.Pais, " .
	"r.Latitud, r.Longitud, p.nprov as Provincia, " .
	"111.045 * DEGREES(ACOS(COS(RADIANS(" . $lat . ")) " .
    "* COS(RADIANS(r.Latitud)) " .
    "* COS(RADIANS(" . $lng . ") - RADIANS(r.Longitud)) " .
    "+ SIN(RADIANS(" . $lat . ")) " . 
    "* SIN(RADIANS(r.Latitud)))) as distancia " . 
	"FROM radsat.radares r LEFT OUTER JOIN provincias p ON (r.idProvincias = p.OGR_FID) " .
	"INNER JOIN paises pa ON (r.idPaises = pa.idPaises) WHERE r.Activo = 1 ORDER BY distancia LIMIT 1");
}

// Obtener el listado de radares activos
function camObtenerRadares() {
	return camConsultarDB("SELECT r.idRadares, r.Sigla, r.Nombre, r.Modelo, r.Fuente, p.Sigla as SiglaProvincia, pa.Pais, " .
	"r.Latitud, r.Longitud, p.nprov as Provincia " .
	"FROM radsat.radares r LEFT OUTER JOIN provincias p ON (r.idProvincias = p.OGR_FID) " .
	"INNER JOIN paises pa ON (r.idPaises = pa.idPaises) WHERE r.Activo = 1 ORDER BY r.orden");
}

// Obtener el listado de vistas para un ID de radar
function camObtenerRadarVistas($id, $tipo, $dist) {
	$sql = "SELECT idRadaresVistas, FechaActualizacion, rv.idTiposRadaresProcesos, rv.idTiposRadaresCortes, " .
	"Rango, Elevacion, CONCAT(r.Ruta, '/', rv.Ruta) as Ruta, " . 
	"LatitudMin, LongitudMin, LatitudMax, LongitudMax, rp.Descripcion AS ProcesoDesc, " .
	"rp.Sigla as ProcesoSigla, rc.Descripcion as CorteDesc, rc.Sigla as CorteSigla, FechaProcesamiento, " .
	"ElevacionDesc, rv.idRadares FROM radsat.radaresvistas rv, " .
	"radsat.radares r, radsat.tiposradaresprocesos rp, radsat.tiposradarescortes rc " .
	"WHERE rv.idTiposRadaresProcesos = rp.idTiposRadaresProcesos AND rv.idRadares = " . $id . " " .
	"AND rv.idTiposRadaresCortes = rc.idTiposRadaresCortes AND r.idRadares = rv.idRadares AND ";
	if ($tipo != null)
		$sql .= "rp.Sigla LIKE '%" . $tipo . "%' AND ";
	if ($dist != null)
	{
		if ($dist > 240)
			$sql .= "Rango >= " . $dist . " AND ";
		else
			$sql .= "Rango <= 250 AND ";
	}
	$sql .= "rv.Activo = 1 ORDER BY rv.idRadares, rv.idTiposRadaresProcesos, rv.idTiposRadaresCortes DESC, rv.Rango";
	return camConsultarDB($sql);
}

?>
