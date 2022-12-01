<?php
header("Content-Type: text/html; charset=utf8");
include 'db.php';

$query = "
    CREATE TABLE comments (
	cid			BIGINT NOT NULL auto_increment,
	comment		VARCHAR(500) NOT NULL,
	user		VARCHAR(100) NOT NULL,
	created		TIMESTAMP DEFAULT CURRENT_TIMESTAMP(),
	PRIMARY KEY(cid)
);";

$result = mysqli_query($connection_id, $query) or die("Rossz lekérdezés " . $query);

?>
