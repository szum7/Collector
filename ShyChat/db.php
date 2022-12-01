<?php
header("Content-Type: text/html; charset=utf8");
$connection_id = new mysqli("localhost", "root", "emerald", "AAchat");
//$connection_id = new mysqli("kiszolgáló neve", "felhasználónév", "jelszó", "adatbázis neve");
//$connection_id = new mysqli("mysql3.000webhost.com", "a5323024_user", "user_pass", "a5323024_SZD");
if ($connection_id->connect_errno) {
    echo "Failed to connect to MySQL: (" . $connection_id->connect_errno . ") " . $connection_id->connect_errno;
}
$connection_id->set_charset("utf8");
?>