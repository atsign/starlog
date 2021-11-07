import type { ObservationModel } from "$lib/models/ObservationModel";

export const observationHttpService = {
    getObservations(): Promise<ObservationModel[]> {
        return fetch(`/api/Observations`)
            .then(result => result.json());
    },

    saveObservation(observation: ObservationModel): Promise<string> {
        return fetch('/api/InsertObservation', {
            method: 'POST',
            body: JSON.stringify(observation)
        })
        .then(res => {
            if (res.ok) {
                return res.text();
            } else {
                throw new Error(res.statusText);
            }
        });
    },

    deleteObservation(id: string): Promise<boolean> {
        return fetch(`/api/DeleteObservation?id=${id}`, { method: 'POST' })
            .then(res => res.ok)
            .catch(() => false);
    },

    updateObservation(observation: ObservationModel): Promise<boolean> {
        return fetch('/api/UpdateObservation', {
            method: 'POST',
            body: JSON.stringify(observation)
        })
        .then(res => {
            if (res.ok) {
                return true;
            } else {
                throw new Error(res.statusText);
            }
        });
    }
}