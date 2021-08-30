<script lang="ts">
    import {
        Button,
        ButtonSet,
        TextArea
    } from 'carbon-components-svelte';
    import AutoComplete from 'simple-svelte-autocomplete';
    import Flatpickr from 'svelte-flatpickr/src/Flatpickr.svelte';
    import 'flatpickr/dist/flatpickr.css';
    import 'flatpickr/dist/themes/dark.css';
    import { createEventDispatcher } from 'svelte';
    import type { ObservationModel } from './models/ObservationModel';
    import type { CelestialObjectModel } from './models/CelestialObjectModel';
    import { timeFormatter, dateFormatter } from './DateTimeFormatters';

    const dispatch = createEventDispatcher();

    function handleCancel() {
        dispatch('cancel');
    }

    function handleSave() {
        const observationToSave: ObservationModel = {
            notes,
            dateTime: new Date(observationDate),
            celestialObject: selectedObject,
            id: observationToEdit?.id
        }

        dispatch('observationSave', observationToSave);
    }

    function searchCelestialObjects(term: Event) {
        return fetch(`/api/CelestialObjects?search=${term}`)
            .then(response => response.json())
            .catch(err => console.error('Error fetching celestial objects', err));
    }

    function getCelestialObjectLabelName(celestialObject: CelestialObjectModel) {
        return celestialObject.commonNames?.length
            ? `${celestialObject.name} (${celestialObject.commonNames.slice(0, 3).join(', ')})`
            : celestialObject.name;
    }

    function handleDateChange(event) {
        const [ selectedDates ] = event.detail;
        observationDate = selectedDates[0];
    }

    export let observationToEdit: ObservationModel;

    let observationDate: Date = observationToEdit?.dateTime;
    let observationDateStr = observationDate ? `${dateFormatter.format(observationDate)} ${timeFormatter.format(observationDate)}` : null;
    let notes: string = observationToEdit?.notes;
    let selectedObject = observationToEdit?.celestialObject;
    let flatpickrOptions = {
        enableTime: true,
        dateFormat: 'F j, Y h:i K'
    };

    $: observationIsValid = observationDate != null && selectedObject != null;
</script>

<div id="observation-datetime-picker">
    <label for="dateAndTime">Date and Time</label>
    <Flatpickr bind:value={observationDateStr}
        options={flatpickrOptions}
        on:change={handleDateChange} />
</div>

<div id="celestial-object-autocomplete">
    <label for="celestialObject">Celestial Object</label>
    <AutoComplete searchFunction={searchCelestialObjects}
        showClear={true}
        showLoadingIndicator={true}
        hideArrow={true}
        minCharactersToSearch=3
        delay=400
        localFiltering={false}
        valueFieldName="id"
        labelFunction={getCelestialObjectLabelName}
        bind:selectedItem={selectedObject} />
</div>
 
<div>
    <label for="ntoes">Notes</label>
    <TextArea name="notes"
           id="notes"
           bind:value={notes} />
</div>

<ButtonSet>
    <Button on:click={handleSave} disabled={!observationIsValid}>Save</Button>
    <Button kind="secondary" on:click={handleCancel}>Cancel</Button>
</ButtonSet>

<style>
    #celestial-object-autocomplete {
        margin-bottom: 20px;
    }
</style>