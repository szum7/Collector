<?php
class comments extends user {
    /**
     * Description of comments
     *
     * @author Szőcs Áron
     * username - szum7
     * address - Hungary, Budapest
     * email - szum7@hotmail.com, szocs.aa@gmail.com
     */
    
    private $stream;
    private $user;

    public function __construct($connection_id, $user) {
        $this->user = $user;
        $this->refreshStream($connection_id);
    }

    public function createComment($connection_id, $comment) {
        $comment = "<font color=\"" . $this->user->color . "\">" . $comment . "</font>";
        $query = "INSERT INTO comments (uid, comment, created) VALUES (" . $this->user->uid . ", '" . $comment . "', NOW());";
        $result = mysqli_query($connection_id, $query) or die("QUERY ERROR " . $query);
        if ($result) {
            $this->stream .= $comment;
            return true;
        } else {
            return false;
        }
    }

    public function refreshStream($connection_id) {
        $query = "
            SELECT comments.id cid, comment, created, nickname, color, users.id uid
            FROM comments
            INNER JOIN users ON users.id = comments.uid
            ORDER BY created ASC;";
        $result = mysqli_query($connection_id, $query) or die("QUERY ERROR " . $query);

        $this->stream = "";
        if (mysqli_num_rows($result) > 0) {
            $color = "dark";
            while ($row = mysqli_fetch_assoc($result)) {
                $this->stream .= "<div class='line " . $color . "' data-lineid='" . $row["cid"] . "'>";
                /* if(isset($_SESSION["user"]) && ($row["uid"] == $this->user->uid)){ } */
                $this->stream .= "<p style='color:#" . $row["color"] . ";'><strong>" . $row["nickname"] . ": </strong>";
                $this->stream .= $row["comment"];
                $this->stream .= "</p>";
                $this->stream .= "<p class='time'>" . timeFormat($row["created"]);
                if ($this->user->uid != 0) {
                    $this->stream .= " <a href='javascript:deleteThis(" . $row["cid"] . ");'>DEL</a>";
                }
                $this->stream .= "</p>";
                $this->stream .= "</div>";
                $color = ($color == "dark") ? "light" : "dark";
            }
        }
    }

    public function deleteComment($connection_id, $id) {
        $query = "DELETE FROM comments WHERE id = " . $id . ";";
        $result = mysqli_query($connection_id, $query) or die("QUERY ERROR " . $query);
        return $result;
    }

    public function setUser($n) {
        $this->user = $n;
    }

    public function getStream() {
        return $this->stream;
    }

}

function timeFormat($t) {
    $t = str_replace("-", ".", $t);
    $t = substr($t, 0, 16);
    return $t;
}

?>