<script lang="ts">
    import { createEventDispatcher } from 'svelte';
    import type { ObservationModel } from './models/ObservationModel';

    const timeFormatter = new Intl.DateTimeFormat('en', {
        hour12: true,
        hour: 'numeric',
        minute: '2-digit'
    });

    export let observation: ObservationModel;

    const dispatch = createEventDispatcher();

    function handleDeleteClick() {
        dispatch('observationDelete', { observationId: observation.id });
    }

    function dispatchEvent(eventName: string) {
        dispatch(eventName, { observationId: observation.id });
    }
</script>

<time>{timeFormatter.format(observation.dateTime)}</time>
{observation.celestialObject.name}
<div>
    <strong>R.A.</strong> {observation.celestialObject.rightAscension}
    <br/>
    <strong>Dec.</strong> {observation.celestialObject.declination}<br/>
</div>
<p>{observation.notes}</p>

<button on:click={() => dispatchEvent('observationEdit')}>
    Edit
</button>
<button on:click={() => dispatchEvent('observationDelete')}>
    Delete
</button>
<style>
    
</style>