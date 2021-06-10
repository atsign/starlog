<script lang="ts">
    import { createEventDispatcher } from 'svelte';
    import { celestialObjects } from './stores/celestialObjectStore';

    import {
        Button,
        FormGroup,
        Input,
        Label
    } from 'sveltestrap';
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

<h3>{observationToEdit ? 'Edit' : 'New'} Observation</h3>

<FormGroup>
    <Label for="dateAndTime">Date and Time</Label>
    <Input name="dateAndTime"
           type="datetime-local"
           id="dateAndTime"
           placeholder="Date and time"
           bind:value={observationDate} />
</FormGroup>

<FormGroup>
    <Label for="celestialObject">
        Celestial Object
        {#if selectedObject}: {selectedObject.name}{/if}
    </Label>
    <Input type="select"
           name="celestialObject"
           id="celestialObject"
           placeholder="Celestial object"
           bind:value={selectedObjectId}>
           <option value="">Select an object&hellip;</option>
        {#each $celestialObjects as celestialObject}
            <option value="{celestialObject.id}">{celestialObject.name}</option>
        {/each}
    </Input>
</FormGroup>

<FormGroup>
    <Label for="ntoes">Notes</Label>
    <Input type="textarea"
           name="notes"
           id="notes"
           bind:value={notes} />
</FormGroup>

<Button color="success" on:click={handleSave}>Save</Button>
<Button color="danger" outline on:click={handleCancel}>Cancel</Button>