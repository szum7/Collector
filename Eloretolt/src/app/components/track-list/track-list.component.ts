import { Component } from '@angular/core';

export class AudioFile {
    title: string;
    src: string;

    constructor(title: string, src: string) {
        this.title = title;
        this.src = src;
    }

    isEmpty(): boolean {
        return !this.title && !this.src;
    }
}

@Component({
    selector: 'app-track-list-component',
    templateUrl: './track-list.component.html',
    styleUrls: ['./track-list.component.scss']
})
export class TrackListComponent {

    public tracks: Array<AudioFile>;

    constructor(){
        this.tracks = [
            new AudioFile("Aeriform - Secret, 2006", "../../../assets/tracks/aeriform_Secret-160.mp3"),
            new AudioFile("Horsey", "../../../assets/tracks/horse.ogv")
        ];
    }
}
