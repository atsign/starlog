import { Readable, readable, Subscriber } from "svelte/store";
import type { CelestialObjectModel } from "../models/CelestialObjectModel";

const ENDPOINT_URL = 'http://localhost:7071/api/CelestialObjects';

export const celestialObjects: Readable<CelestialObjectModel[]> = readable([], (set: Subscriber<CelestialObjectModel[]>) => {
    fetch(ENDPOINT_URL)
        .then(response => response.json())
        .then((data: CelestialObjectModel[]) => set(data))
        .catch(err => console.error('Error fetching celestial objects', err));
});