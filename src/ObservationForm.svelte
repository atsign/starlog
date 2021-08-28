<script lang="ts">
    import {
        Button,
        ButtonSet,
        TextInput,
        TextArea
    } from 'carbon-components-svelte';
    import AutoComplete from 'simple-svelte-autocomplete';
    import { createEventDispatcher } from 'svelte';
    import type { ObservationModel } from './models/ObservationModel';
    import type { CelestialObjectModel } from './models/CelestialObjectModel';

    const dispatch = createEventDispatcher();

    function handleCancel() {
        dispatch('cancel');
    }

    function handleSave() {
        // TODO: input validation :)

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

    export let observationToEdit: ObservationModel;

    let observationDate: string = observationToEdit?.dateTime?.toLocaleString();
    let notes: string = observationToEdit?.notes;
    let selectedObject = observationToEdit?.celestialObject;
</script>

<div>
    <label for="dateAndTime">Date and Time</label>
    <TextInput name="dateAndTime"
           type="datetime-local"
           id="dateAndTime"
           placeholder="Date and time"
           bind:value={observationDate} />
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
    <Button on:click={handleSave}>Save</Button>
    <Button kind="secondary" on:click={handleCancel}>Cancel</Button>
</ButtonSet>
