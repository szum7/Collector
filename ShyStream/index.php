<?php
header("Content-Type: text/html; charset=utf-8");
$root = "";
require_once $root . 'db.php';
require_once $root . 'class/user.php';
require_once $root . 'class/comments.php';
require_once $root . 'class/chaters.php';
session_start();
?>
<!DOCTYPE html>
<html>
    <head>
        <!-- Created by Szõcs Áron, Budapest, Hungary, szocs.aa@gmail.com
        You may NOT use the following code in any way.
        -->
        <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
        <title>Stream</title>

        <link rel="shortcut icon" href="img/icon.png" />
        <link rel="stylesheet" type="text/css" href="css/index.css" />

        <script src="//code.jquery.com/jquery-1.11.0.min.js"></script>
        <script src="//code.jquery.com/jquery-migrate-1.2.1.min.js"></script>

        <script type="text/javascript" src="js/index.js"></script>
    </head>
    <body>
        <header>
            <div class="color">
                <div class="aligner">
                    <ul>
                        <?php if (!(isset($_SESSION["user"])) || (isset($_SESSION["user"]) && $_SESSION["user"]->getUid() == 0)) { ?>
                            <li class="btn" id="btn_register"><div class="menu">Register</div><div class="hidden" id="dd-register" data-width="289">
                                    <form action="actions.php" method="post" name="register" id="register" enctype="multipart/form-data">
                                        <input type="text" value="" placeholder="Nickname..." id="r-name" name="r-name" />
                                        <input type="text" value="" placeholder="Password..." id="r-password" name="r-password" />
                                        <input type="text" value="" placeholder="Color..." id="r-color" name="r-color" />
                                    </form>
                                </div>
                            </li><li class="btn" id="btn_login"><div class="menu">Log in</div><div class="hidden" id="dd-login" data-width="195">
                                    <form action="actions.php" method="post" name="login" id="login" enctype="multipart/form-data">
                                        <input type="text" value="" placeholder="Nickname..." id="l-name" name="l-name" />
                                        <input type="text" value="" placeholder="Password..." id="l-password" name="l-password" />
                                    </form>
                                </div>
                            </li>
                        <?php } ?>
                        <?php if (isset($_SESSION["user"]) && $_SESSION["user"]->getUid() != 0) { ?>
                            <li class="btn" id="btn_logout"><a href="logout.php"><div class="menu">Log out</div></a></li>
                        <?php } ?>
                    </ul>
                    <p class="copy">&copy; created by szum7</p>
                </div>
            </div>
        </header>
        <div id="main">
            <div class="ib" id="left">
                <div id="settings">

                </div>
                <div id="data">
                    <?php if (isset($_SESSION["user"]) && $_SESSION["user"]->getUid() != 0) { ?>
                        <form action="actions.php" method="post" name="modify" id="modify" enctype="multipart/form-data">
                            <div class="data"><?= $_SESSION["user"]->getUid(); ?></div><input type="text" value="<?= $_SESSION["user"]->getNickname(); ?>" placeholder="Nickname..." id="m-name" name="m-name" autocomplete="off" /><input type="text" value="<?= $_SESSION["user"]->getColor(); ?>" placeholder="Color..." id="m-color" name="m-color" autocomplete="off" /><div class="data last" title="<?= $_SESSION["user"]->getPassword(); ?>">Password</div>
                        </form>
                    <?php } ?>
                </div>
                <div id="chaters"></div>
            </div>

            <div class="ib" id="right">
                <div id="comments"></div>
                <?php if (isset($_SESSION["user"]) && $_SESSION["user"]->getUid() != 0) { ?>
                    <form action="actions.php" method="post" name="createcomment" id="createcomment" enctype="multipart/form-data">
                        <textarea id="comment" name="comment"></textarea>

                    </form>
                <?php } ?>
            </div>

        </div>

        <script>
            $(document).ready(function() {
                $("#chaters").load("ajax/chaters.php");
                $("#comments").load("ajax/comments.php");
            });
        </script>
    </body>
</html>