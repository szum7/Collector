export class TimeConverter {

    public static secondsToString(durationInSeconds: number): string {
        let duration: number = durationInSeconds;
        let hours = Math.floor(duration / (60 * 60));
        duration -= (hours * 60 * 60);
        let minutes = Math.floor(duration / 60);
        duration -= (minutes * 60);
        let seconds = Math.floor(duration);

        let s: string = "";
        if (hours > 0) {
            if (hours < 10) {
                s += "0";
            }
            s += hours + ":";
        }
        if (minutes < 10) {
            s += "0";
        }
        s += minutes + ":";
        if (seconds < 10) {
            s += "0";
        }
        s += seconds;

        return s;
    }

}