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

        const newObservation: ObservationModel = {
            notes,
            dateTime: new Date(observatinDate),
            celestialObject: $celestialObjects.find(obj => obj.id.toString() === selectedObjectId.toString())
        }

        dispatch('observationSave', newObservation);
    }

    let observatinDate: string;
    let selectedObjectId: string;
    let notes: string;
</script>

<h3>New Observation</h3>

<FormGroup>
    <Label for="dateAndTime">Date and Time</Label>
    <Input name="dateAndTime"
           type="datetime-local"
           id="dateAndTime"
           placeholder="Date and time"
           bind:value={observatinDate} />
</FormGroup>

<FormGroup>
    <Label for="celestialObject">Celestial Object</Label>
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