import { Readable, readable, Subscriber } from "svelte/store";
import type { CelestialObjectModel } from "../models/CelestialObjectModel";

const fakeCelestialObjects: CelestialObjectModel[] = [
    {
        name: "Messier 13",
        rightAscension: "16hr 42m 28s",
        declination: "+36deg 25' 08\"",
        id: 1
    },
    {
        name: "Coma Star Cluster",
        rightAscension: "12hr 23m 34s",
        declination: "+25deg 45' 33\"",
        id: 2
    }
];

export const celestialObjects: Readable<CelestialObjectModel[]> = readable([], (set: Subscriber<CelestialObjectModel[]>) => {
    set(fakeCelestialObjects);
});