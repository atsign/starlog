import { Subscriber, Unsubscriber, Writable, writable } from "svelte/store";
import type { ObservationModel } from "../models/ObservationModel";

export interface IObservationStore {
    addObservation(observation: ObservationModel): void;
    editObservation(observation: ObservationModel): void;
    subscribe(subscriber: Subscriber<ObservationModel[]>): Unsubscriber;
}

let defaultObservations: ObservationModel[] = [
    {
        dateTime: new Date("2021-08-20 10:30 PM"),
        id: 1,
        celestialObject: {
            name: "Messier 13",
            rightAscension: "16hr 42m 28s",
            declination: "+36deg 25' 08\"",
            id: 1
        },
        notes: "Donec nec elementum velit, a tristique enim. Mauris mollis fringilla tortor, nec consectetur diam lacinia consequat. Aliquam at felis at felis posuere venenatis. Quisque sollicitudin a augue ut elementum. Praesent finibus, nisl eu sagittis elementum, felis risus condimentum lectus, vel tincidunt magna orci in odio. Sed ac mollis quam. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia curae; Fusce gravida sagittis varius. Aliquam dignissim mauris elit, eu varius sem placerat eu. Mauris eu massa dui. Duis in mi in diam facilisis varius et ut magna. Donec scelerisque egestas ante, eget condimentum mi. Cras euismod sed sem et sagittis. In eu dolor sagittis, aliquam libero id, convallis nulla."
    },
    {
        dateTime: new Date("2021-08-20 10:45 PM"),
        id: 2,
        celestialObject: {
            name: "Coma Star Cluster",
            rightAscension: "12hr 23m 34s",
            declination: "+25deg 45' 33\"",
            id: 2
        },
        notes: "Consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Viverra nam libero justo laoreet sit. Eleifend mi in nulla posuere sollicitudin aliquam ultrices. Aliquet enim tortor at auctor urna nunc id cursus metus. Enim eu turpis egestas pretium aenean pharetra magna ac placerat. Nibh venenatis cras sed felis eget velit aliquet. Curabitur gravida arcu ac tortor. Dictum varius duis at consectetur. Viverra aliquet eget sit amet. Sem fringilla ut morbi tincidunt augue. Suspendisse ultrices gravida dictum fusce ut placerat orci nulla pellentesque. Odio ut sem nulla pharetra diam sit amet. Sapien nec sagittis aliquam malesuada bibendum arcu vitae elementum. A lacus vestibulum sed arcu non. Nisl vel pretium lectus quam id leo. Sagittis aliquam malesuada bibendum arcu vitae elementum. Tortor id aliquet lectus proin nibh."
    },
    {
        dateTime: new Date("2021-08-21 12:45 AM"),
        id: 3,
        celestialObject: {
            name: "Coma Star Cluster",
            rightAscension: "12hr 23m 34s",
            declination: "+25deg 45' 33\"",
            id: 2
        },
        notes: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Malesuada bibendum arcu vitae elementum. Dolor magna eget est lorem ipsum dolor. Blandit turpis cursus in hac habitasse platea dictumst quisque. Habitant morbi tristique senectus et netus et. Faucibus turpis in eu mi bibendum. Adipiscing elit duis tristique sollicitudin nibh sit amet commodo nulla. Duis at consectetur lorem donec. Nisl suscipit adipiscing bibendum est ultricies. In nulla posuere sollicitudin aliquam ultrices sagittis orci a. Cum sociis natoque penatibus et magnis dis parturient montes nascetur. Velit sed ullamcorper morbi tincidunt. Urna condimentum mattis pellentesque id nibh tortor id aliquet lectus. Et egestas quis ipsum suspendisse ultrices gravida. Dictum non consectetur a erat nam at lectus urna duis. Et ligula ullamcorper malesuada proin libero nunc consequat. Viverra adipiscing at in tellus integer feugiat scelerisque varius. Volutpat maecenas volutpat blandit aliquam. Lacinia at quis risus sed vulputate odio ut enim."
    }
];

export class ObservationStore implements IObservationStore {
    private _store: Writable<ObservationModel[]>;
    private _currentId: number = 1;

    constructor() {
        this._store = writable(this.sortObservations(defaultObservations));
    }

    addObservation(observation: ObservationModel): void {
        this._store.update(observations => this.addObservationToStore(observation, observations));
    }

    editObservation(observation: ObservationModel): void {
        this._store.update(observations => this.editObservationInStore(observation, observations));
    }

    deleteObservation(observationId: number): void {
        this._store.update(observations => this.deleteObservationFromStore(observationId, observations));
    }

    subscribe(subscriber: Subscriber<ObservationModel[]>): Unsubscriber {
        return this._store.subscribe(subscriber);
    }

    private addObservationToStore(observation: ObservationModel, observations: ObservationModel[]): ObservationModel[] {
        observation.id = this._currentId++; // Temporary solution. These should be unique identifiers.
        observations.push(observation);

        return this.sortObservations(observations);
    }

    private editObservationInStore(observation: ObservationModel, observations: ObservationModel[]): ObservationModel[] {
        observations = observations.map(thisObservation => {
            if (thisObservation.id === observation.id) {
                return observation;
            } else {
                return thisObservation;
            }
        });

        return this.sortObservations(observations);
    }

    private deleteObservationFromStore(observationId: number, observations: ObservationModel[]): ObservationModel[] {
        const indexToDelete = observations.findIndex(observation => observation.id === observationId);
        observations.splice(indexToDelete, 1);
        return observations;
    }

    private sortObservations(observations: ObservationModel[]): ObservationModel[] {
        return observations.sort((a, b) => a.dateTime > b.dateTime ? -1 : 1);
    }
}