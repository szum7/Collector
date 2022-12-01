<?php
header("Content-Type: text/html; charset=utf8");
include 'db.php';

if(isset($_POST["cid"])){
    $cid = $_POST["cid"];
    
    $query = "DELETE FROM comments WHERE cid = $cid";
    $result = mysqli_query($connection_id, $query) or die("Rossz lekérdezés " . $query);
}

?>
