import { Subscriber, Unsubscriber, Writable, writable } from "svelte/store";
import type { ObservationModel } from "../models/ObservationModel";

export interface IObservationStore {
    addObservation(observation: ObservationModel): void;
    subscribe(subscriber: Subscriber<ObservationModel[]>): Unsubscriber;
}

export class ObservationStore implements IObservationStore {
    private _store: Writable<ObservationModel[]>;

    constructor() {
        this._store = writable([]);
    }

    addObservation(observation: ObservationModel): void {
        this._store.update(observations => this.updateObservationStore(observation, observations));
    }

    subscribe(subscriber: Subscriber<ObservationModel[]>): Unsubscriber {
        return this._store.subscribe(subscriber);
    }

    private updateObservationStore(observation: ObservationModel, observations: ObservationModel[]): ObservationModel[] {
        observations.push(observation);

        return this.sortObservations(observations);
    }

    private sortObservations(observations: ObservationModel[]): ObservationModel[] {
        return observations.sort((a, b) => a.dateTime > b.dateTime ? -1 : 1);
    }
}