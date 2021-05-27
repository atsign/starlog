import type { ObservationModel } from './ObservationModel';

export interface ObservationListModel {
    date: Date;
    observations: ObservationModel[]
}