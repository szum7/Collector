<?php
header("Content-Type: text/html; charset=utf-8");
$root = "";
require_once $root . 'db.php';
require_once $root . 'class/user.php';
require_once $root . 'class/comments.php';
require_once $root . 'class/chaters.php';
session_start();

if (isset($_POST["type"])) {
    switch ($_POST["type"]) {
        case "setTyping":
            if (isset($_POST["istyping"], $_SESSION["user"])) {
                if ($_SESSION["user"]->getUid() != 0) {

                    if ($response = $_SESSION["user"]->setTyping($connection_id, $_POST["istyping"])) {
                        echo 'success';
                    } else {
                        echo 'failure';
                    }
                } else {
                    // ERROR: visitor typing
                    echo 'failure';
                }
            }
            break;
        case "setOnline":
            if (isset($_POST["isonline"], $_SESSION["user"])) {
                if ($_SESSION["user"]->getUid() != 0) {

                    if ($response = $_SESSION["user"]->setOnline($connection_id, $_POST["isonline"])) {
                        echo 'success';
                    } else {
                        echo 'failure';
                    }
                } else {
                    // ERROR: visitor typing
                    echo 'failure';
                }
            }
            break;
        case "createComment":
            if (isset($_POST["comment"], $_SESSION["user"])) {
                if ($_SESSION["user"]->getUid() != 0) {
                    $comment = htmlspecialchars($_POST["comment"]);
                    if (isset($_SESSION["stream"])) {
                        if ($response = $_SESSION["stream"]->createComment($connection_id, $comment)) {
                            echo 'success';
                        } else {
                            echo 'failure';
                        }
                    }
                }
            }
            break;
            case "deleteComment":
            if (isset($_POST["id"], $_SESSION["user"])) {
                if ($_SESSION["user"]->getUid() != 0) {
                    if (isset($_SESSION["stream"])) {
                        if ($response = $_SESSION["stream"]->deleteComment($connection_id, $_POST["id"])) {
                            echo 'success';
                        } else {
                            echo 'failure';
                        }
                    }
                }
            }
            break;
        case "checkOnlineChaters":
            if(isset($_SESSION["chaters"])){
                $_SESSION["chaters"]->checkOnlineChaters($connection_id);
            }
            break;

        case "setUserLastactivity":
            if(isset($_SESSION["user"])){
                $_SESSION["user"]->setLastactivity($connection_id);
            }
            break;

        default:
            break;
    }
} else if (isset($_POST["r-name"], $_POST["r-password"], $_POST["r-color"])) { // registration
    $name = $_POST["r-name"];
    $password = $_POST["r-password"];
    $color = $_POST["r-color"];

    $query = "INSERT INTO users (nickname, password, isonline, istyping, lastactivity, color)
        VALUES ('" . $name . "', '" . ($password) . "', 1, 0, NOW(), '" . $color . "')";
    $result = mysqli_query($connection_id, $query) or die("QUERY ERROR " . $query);

    if ($result) {
        $uid = mysqli_insert_id($connection_id);
        unset($_SESSION["user"]);
        $_SESSION["user"] = new user($uid, $name, $password, $color);
        if (isset($_SESSION["stream"])) {
            $_SESSION["stream"]->setUser($_SESSION["user"]);
        }
        header("Location:index.php?response=registersuccess");
    } else {
        header("Location:index.php?response=registerfail");
    }
} else if (isset($_POST["l-name"], $_POST["l-password"])) { // login
    $_SESSION["user"] = new user(0, $_POST["l-name"], $_POST["l-password"], "unset");
    $response = $_SESSION["user"]->login($connection_id, $_POST["l-password"]);

    if ($response) {
        if (isset($_SESSION["stream"])) {
            $_SESSION["stream"]->setUser($_SESSION["user"]);
        }
        header("Location:index.php?response=loginsuccess");
    } else {
        header("Location:index.php?response=loginfail");
    }
} else if (isset($_POST["m-name"], $_POST["m-color"])) { // modify
    if (isset($_SESSION["user"])) {

        $uid = $_SESSION["user"]->getUid();
        $name = $_POST["m-name"];
        $color = $_POST["m-color"];

        if ($uid != 0) {

            $response = $_SESSION["user"]->updateUser($connection_id, $name, $color);

            if ($response) {
                header("Location:index.php?response=modifysuccess");
            } else {
                header("Location:index.php?response=modifyfail");
            }
        } else {
            header("Location:index.php?response=modifyvisitor");
        }
    } else {
        echo 'no session';
    }
}
?>