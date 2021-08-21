<script lang="ts">
    import {
        Button,
        ButtonSet,
        TextInput,
        TextArea
    } from 'carbon-components-svelte';
    import { createEventDispatcher } from 'svelte';
    import { celestialObjects } from './stores/celestialObjectStore';
    import type { ObservationModel } from './models/ObservationModel';

    const dispatch = createEventDispatcher();

    function handleCancel() {
        dispatch('cancel');
    }

    function handleSave() {
        // TODO: input validation :)

        const observationToSave: ObservationModel = {
            notes,
            dateTime: new Date(observationDate),
            celestialObject: $celestialObjects.find(obj => obj.id.toString() === selectedObjectId.toString()),
            id: observationToEdit?.id
        }

        dispatch('observationSave', observationToSave);
    }

    export let observationToEdit: ObservationModel;

    let observationDate: string = observationToEdit?.dateTime?.toLocaleString();
    let selectedObjectId: string = observationToEdit?.celestialObject?.id?.toString();
    let notes: string = observationToEdit?.notes;

    $: selectedObject = $celestialObjects.find(obj => obj.id.toString() === selectedObjectId?.toString());
</script>

<div>
    <label for="dateAndTime">Date and Time</label>
    <TextInput name="dateAndTime"
           type="datetime-local"
           id="dateAndTime"
           placeholder="Date and time"
           bind:value={observationDate} />
</div>

<div>
    <label for="celestialObject">
        Celestial Object
        {#if selectedObject}: {selectedObject.name}{/if}
    </label>
    <select
           name="celestialObject"
           id="celestialObject"
           placeholder="Celestial object"
           bind:value={selectedObjectId}>
           <option value="">Select an object&hellip;</option>
        {#each $celestialObjects as celestialObject}
            <option value="{celestialObject.id}">{celestialObject.name}</option>
        {/each}
    </select>
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