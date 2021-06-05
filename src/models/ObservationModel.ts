import type { CelestialObjectModel } from "./CelestialObjectModel";

export interface ObservationModel {
    dateTime: Date;
    celestialObject: CelestialObjectModel;
    notes: string;
    id?: number;
}