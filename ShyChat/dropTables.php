<?php
header("Content-Type: text/html; charset=utf8");
include 'db.php';

$query = "";

$result = mysqli_query($connection_id, $query) or die("Rossz lekérdezés " . $query);

?>
