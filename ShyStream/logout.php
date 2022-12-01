<?php
require_once 'db.php';
require_once 'class/user.php';
session_start();
if(isset($_SESSION["user"])){
    $_SESSION["user"]->setOnline($connection_id, 0);
}
session_unset();
session_destroy();
session_write_close();
setcookie(session_name(), '', 0, '/');
session_regenerate_id(true);
header('Location: index.php');
?>