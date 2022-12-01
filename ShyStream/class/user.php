<?php
class user {
    /*
     * @author Szőcs Áron
     * username - szum7
     * address - Hungary, Budapest
     * email - szum7@hotmail.com, szocs.aa@gmail.com
     */
    protected $uid;
    protected $nickname;
    protected $color;
    protected $istyping;
    protected $isonline;
    protected $lastactivity;
    private $password;

    public function __construct($uid, $name, $password, $color) {
        $this->uid = $uid;
        $this->nickname = $name;
        $this->password = $password;
        $this->color = $color;
        $this->istyping = 0;
        $this->isonline = 1;
        $this->lastactivity = date("Y-m-d h:m:s");
    }

    public function login($connection_id, $password) {

        $query = "
            SELECT id, password, nickname, color
            FROM users
            WHERE password = '" . ($password) . "' 
            AND nickname = '" . $this->nickname . "';";

        $result = mysqli_query($connection_id, $query) or die("QUERY ERROR " . $query);

        if (mysqli_num_rows($result) > 0) {
            $row = mysqli_fetch_assoc($result);
            $this->color = $row["color"];
            $this->uid = $row["id"];
            $this->password = $row["password"];

            return true;
        } else {
            return false;
        }
    }

    public function setTyping($connection_id, $n) {
        $this->istyping = $n;
        $query = "
            UPDATE users
            SET istyping = " . $this->istyping . " 
            WHERE id = " . $this->uid . ";";
        $result = mysqli_query($connection_id, $query) or die("QUERY ERROR " . $query);
        return $result;
    }

    public function setOnline($connection_id, $n) {
        $this->isonline = $n;
        $query = "
            UPDATE users
            SET isonline = " . $this->isonline . " 
            WHERE id = " . $this->uid . ";";
        $result = mysqli_query($connection_id, $query) or die("QUERY ERROR " . $query);
        return $result;
    }

    public function setLastactivity($connection_id) {
        $query = "UPDATE users SET lastactivity = NOW() WHERE id = " . $this->uid . ";";
        $result = mysqli_query($connection_id, $query) or die("QUERY ERROR " . $query);
        return $result;
    }

    public function updateUser($connection_id, $name, $color) {
        if ($name != "" || $color != "") {
            $query = "";
            if (isset($name) && $name != "") {
                $query .= " nickname = '" . $name . "' ";
            }
            if (isset($color) && $color != "") {
                $query .= ($query != "") ? "," : "";
                $query .= " color = '" . $color . "' ";
            }
            $query = "UPDATE users SET " . $query . " WHERE id = " . $this->uid . ";";
            $result = mysqli_query($connection_id, $query) or die("QUERY ERROR " . $query);
            if ($result) {
                $this->nickname = $name;
                $this->color = $color;
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    public function getPassword() {
        return $this->password;
    }

    public function setNickname($n) {
        $this->nickname = $n;
    }

    public function setColor($n) {
        $this->color = $n;
    }

    public function getNickname() {
        return $this->nickname;
    }

    public function getUid() {
        return $this->uid;
    }

    public function getColor() {
        return $this->color;
    }

}

?>