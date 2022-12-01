<?php
class chaters {
    /**
     * Description of chaters
     *
     * @author Szőcs Áron
     * username - szum7
     * address - Hungary, Budapest
     * email - szum7@hotmail.com, szocs.aa@gmail.com
     */
    private $chaters;

    public function __construct($connection_id) {

        $query = "
            SELECT nickname, color
            FROM users
            WHERE isonline = 1
            ORDER BY id DESC;";
        $result = mysqli_query($connection_id, $query) or die("QUERY ERROR " . $query);

        if (mysqli_num_rows($result) > 0) {
            while ($row = mysqli_fetch_assoc($result)) {
                $this->chaters .= "<div class='chater'>";
                $this->chaters .= "<div class='color' title='" . $row["color"] . "' style='background-color:#" . $row["color"] . ";'></div>";
                $this->chaters .= "<p>" . $row["nickname"] . " </p>";
                $this->chaters .= "</div>";
            }
        }
    }

    public function refreshChaters($connection_id) {
        $query = "
            SELECT nickname, istyping, color
            FROM users
            WHERE isonline = 1
            ORDER BY id DESC;";
        $result = mysqli_query($connection_id, $query) or die("QUERY ERROR " . $query);

        $this->chaters = "";
        if (mysqli_num_rows($result) > 0) {
            while ($row = mysqli_fetch_assoc($result)) {
                $this->chaters .= "<div class='chater'>";
                $this->chaters .= "<div class='color' title='" . $row["color"] . "' style='background-color:#" . $row["color"] . ";'></div>";
                $this->chaters .= "<p>" . $row["nickname"] . " " . (($row["istyping"] == true || $row["istyping"] == "1") ? " is typing..." : "") . "</p>";
                $this->chaters .= "</div>";
            }
        }
    }

    public function checkOnlineChaters($connection_id) {
        $query = "UPDATE users SET isonline = false WHERE lastactivity < SUBDATE(NOW(), INTERVAL 5 MINUTE);";
        $result = mysqli_query($connection_id, $query) or die("QUERY ERROR " . $query);
        return $result;
    }

    public function getChaters() {
        return $this->chaters;
    }

}

?>