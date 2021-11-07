import { observationHttpService } from "$lib/services/observationHttpService";
import { writable, readable } from "svelte/store";
import type { Subscriber, Unsubscriber, Writable } from "svelte/store";
import type { ObservationModel } from "$lib/models/ObservationModel";

export interface IObservationStore {
    addObservation(observation: ObservationModel): void;
    editObservation(observation: ObservationModel): void;
    subscribe(subscriber: Subscriber<ObservationModel[]>): Unsubscriber;
}

let _setIsLoading: Subscriber<boolean>;

export const isLoading = readable(true, (set: Subscriber<boolean>) => {
    _setIsLoading = set;
})

export class ObservationStore implements IObservationStore {
    private _store: Writable<ObservationModel[]>;

    constructor() {
        this._store = writable([]);

        _setIsLoading(true);

        observationHttpService
            .getObservations()
            .then((observations: ObservationModel[]) => {
                this._store.set(observations.map(this.reviveDate));
            })
            .finally(() => _setIsLoading(false));
    }

    addObservation(observation: ObservationModel): void {
        _setIsLoading(true);

        observationHttpService
            .saveObservation(observation)
            .then((itemId) => {
                observation.id = itemId;
                this._store.update(observations => this.addObservationToStore(observation, observations));
            })
            .catch((err) => console.error(err))
            .finally(() => _setIsLoading(false));
    }

    editObservation(observation: ObservationModel): void {
        _setIsLoading(true);

        observationHttpService
            .updateObservation(observation)
            .then(success => {
                if (success) {
                    this._store.update(observations => this.editObservationInStore(observation, observations));
                } else {
                    throw new Error(`Observation with ID ${observation.id} could not be updated`);
                }
            })
            .catch(err => console.log(err))
            .finally(() => _setIsLoading(false));
    }

    deleteObservation(observationId: string): void {
        _setIsLoading(true);

        observationHttpService
            .deleteObservation(observationId)
            .then(success => {
                if (success) {
                    this._store.update(observations => this.deleteObservationFromStore(observationId, observations));
                } else {
                    throw new Error(`Observation with ID ${observationId} could not be deleted`);
                }
            })
            .catch(err => console.error(err))
            .finally(() => _setIsLoading(false));
    }

    subscribe(subscriber) {
        return this._store.subscribe(subscriber);
    }

    private addObservationToStore(observation: ObservationModel, observations: ObservationModel[]): ObservationModel[] {
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

    private deleteObservationFromStore(observationId: string, observations: ObservationModel[]): ObservationModel[] {
        const indexToDelete = observations.findIndex(observation => observation.id === observationId);
        observations.splice(indexToDelete, 1);
        return observations;
    }

    private sortObservations(observations: ObservationModel[]): ObservationModel[] {
        return observations.sort((a, b) => a.dateTime > b.dateTime ? -1 : 1);
    }

    private reviveDate(observation: ObservationModel): ObservationModel {
        observation.dateTime = new Date(observation.dateTime);
        return observation;
    }
}