<script lang="ts">
    import {
        Tile,
        Button,
        ButtonSet
    } from 'carbon-components-svelte';
    import Edit16 from 'carbon-icons-svelte/lib/Edit16';
    import Delete16 from 'carbon-icons-svelte/lib/Delete16';
    import { createEventDispatcher } from 'svelte';
    import type { ObservationModel } from './models/ObservationModel';
    import { timeFormatter } from './DateTimeFormatters';

    export let observation: ObservationModel;

    const dispatch = createEventDispatcher();

    function dispatchEvent(eventName: string) {
        dispatch(eventName, { observationId: observation.id });
    }
</script>

<Tile light={true}>
    <h3>
        <strong>{observation.celestialObject.name}</strong> -
        <time>{timeFormatter.format(observation.dateTime)}</time>
    </h3>
    
    {#if observation.celestialObject.commonNames?.length}
    <h4>{observation.celestialObject.commonNames.join(', ')}</h4>
    {/if}

    <div class="location">
        <strong>R.A.</strong> {observation.celestialObject.rightAscension}
        <strong>Dec.</strong> {observation.celestialObject.declination}<br/>
    </div>
    
    <p class="notes">{observation.notes}</p>

    <ButtonSet>
        <Button on:click={() => dispatchEvent('observationEdit')} icon={Edit16}>
            Edit
        </Button>
        <Button kind="danger" on:click={() => dispatchEvent('observationDelete')} icon={Delete16}>
            Delete
        </Button>
    </ButtonSet>
</Tile>

<style>
    .location {
        margin-bottom: 20px;
    }

    .notes {
        margin-bottom: 20px;
    }
</style>