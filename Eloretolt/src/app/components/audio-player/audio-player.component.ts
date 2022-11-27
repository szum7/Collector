import { Component, Input, OnInit } from '@angular/core';
import { AudioFile } from '../track-list/track-list.component';
import { TimeConverter } from 'src/app/utilities/time-converter.util';

@Component({
    selector: 'app-audio-player-component',
    templateUrl: './audio-player.component.html',
    styleUrls: ['./audio-player.component.scss']
})
export class AudioPlayerComponent implements OnInit {

    @Input() track: AudioFile;
    progress: number;
    progressInterval;
    player;

    get isInited(): boolean {
        return !!this.track && !this.track.isEmpty();
    }

    get progressBar(): number {
        return (this.player.currentTime / this.player.duration) * 100;
    }

    set progressBar(value: number) {
        this.player.currentTime = this.player.duration * (value / 100);
    }

    get currentTime(): string {
        return TimeConverter.secondsToString(this.player.currentTime);
    }

    get duration(): string {
        return TimeConverter.secondsToString(this.player.duration);
    }

    constructor() {        
    }

    ngOnInit(): void {
        this.player = new Audio();
        this.player.src = this.track.src;
        this.player.title = this.track.title;

        this.progressInterval = setInterval(() => {
            this.progress = (this.player.currentTime / this.player.duration) * 100;
        }, 100);
    }

    play(): void {
        this.player.play();
    }

    pause(): void {
        this.player.pause();
    }

    stop(): void {
        this.player.pause();
        this.player.currentTime = 0;
    }
}
